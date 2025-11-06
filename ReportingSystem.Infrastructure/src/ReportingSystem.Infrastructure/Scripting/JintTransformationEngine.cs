using Jint;
using Jint.Native;
using Jint.Native.Json;
using Jint.Runtime;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReportingSystem.Application.Common.Exceptions;
using ReportingSystem.Application.Common.Interfaces;
using ReportingSystem.Domain.Entities;
using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace ReportingSystem.Infrastructure.Scripting
{
    public class JintTransformationEngine : ITransformationEngine
    {
        private readonly ILogger<JintTransformationEngine> _logger;
        private readonly IAuditLogger _auditLogger;
        private readonly JintEngineOptions _options;

        public JintTransformationEngine(
            ILogger<JintTransformationEngine> logger,
            IOptions<JintEngineOptions> options,
            IAuditLogger auditLogger)
        {
            _logger = logger;
            _auditLogger = auditLogger;
            _options = options.Value;
        }

        public async Task<TransformationResult> TransformAsync(string script, JsonNode input, CancellationToken cancellationToken)
        {
            var engine = new Engine(cfg =>
            {
                // REQ-SEC-DTR-001: Enforce security constraints
                cfg.AllowClr(false); // Disable CLR access
                cfg.MaxStatements(_options.MaxStatements);
                cfg.MemoryLimit(_options.MemoryLimitMb * 1024 * 1024); // Convert MB to bytes
                cfg.TimeoutInterval(TimeSpan.FromSeconds(_options.TimeoutSeconds));
                cfg.CancellationToken(cancellationToken);
                cfg.Strict(); // Enforce strict mode for better error checking
            });

            try
            {
                // Set the input data into the script's scope
                var jsonParser = new JsonParser(engine);
                engine.SetValue("data", jsonParser.Parse(input.ToJsonString()));

                // TODO: Inject and expose helper functions (US-050)
                // engine.SetValue("helpers", new ScriptHelpers());

                var result = engine.Evaluate(script);

                var outputJsonString = new JsonSerializer(engine).Serialize(result, JsValue.Undefined, "  ");
                var outputNode = JsonNode.Parse(outputJsonString);

                if (outputNode == null)
                {
                    _logger.LogWarning("Jint transformation script returned null or undefined. Input script: {script}", script);
                    return TransformationResult.Failure(new ScriptRuntimeException("Transformation script returned a null or undefined value."));
                }
                
                return TransformationResult.Success(outputNode);
            }
            // REQ-SEC-DTR-002: Catch sandbox violations and log them
            catch (StatementsCountOverflowException ex)
            {
                _logger.LogWarning(ex, "Jint sandbox violation: Statement count overflow.");
                await _auditLogger.LogSecurityViolationAsync("STATEMENT_COUNT_VIOLATION", ex.Message, cancellationToken);
                return TransformationResult.Failure(new ScriptResourceLimitExceededException("Script executed too many statements.", ex));
            }
            catch (MemoryLimitExceededException ex)
            {
                _logger.LogWarning(ex, "Jint sandbox violation: Memory limit exceeded.");
                await _auditLogger.LogSecurityViolationAsync("MEMORY_LIMIT_VIOLATION", ex.Message, cancellationToken);
                return TransformationResult.Failure(new ScriptResourceLimitExceededException("Script exceeded memory limit.", ex));
            }
            catch (RecursionDepthOverflowException ex)
            {
                _logger.LogWarning(ex, "Jint sandbox violation: Recursion depth overflow.");
                await _auditLogger.LogSecurityViolationAsync("RECURSION_DEPTH_VIOLATION", ex.Message, cancellationToken);
                return TransformationResult.Failure(new ScriptResourceLimitExceededException("Script exceeded maximum recursion depth.", ex));
            }
            catch (OperationCanceledException) // Catches both Jint timeout and external CancellationToken
            {
                _logger.LogWarning("Jint sandbox violation: Execution timed out or was canceled.");
                await _auditLogger.LogSecurityViolationAsync("TIMEOUT_VIOLATION", $"Script exceeded {_options.TimeoutSeconds} seconds execution limit.", cancellationToken);
                return TransformationResult.Failure(new ScriptTimeoutException($"Script execution timed out after {_options.TimeoutSeconds} seconds."));
            }
            // Catch script runtime errors
            catch (JavaScriptException ex)
            {
                _logger.LogInformation(ex, "A JavaScript runtime error occurred during transformation.");
                return TransformationResult.Failure(new ScriptRuntimeException(ex.Message, ex.StackTrace, ex.LineNumber, ex));
            }
            // REQ-REL-DTR-001: Catch any other unexpected engine crashes
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected, critical error occurred within the Jint transformation engine.");
                return TransformationResult.Failure(new ApplicationException("The transformation engine encountered a critical failure.", ex));
            }
        }
    }
}