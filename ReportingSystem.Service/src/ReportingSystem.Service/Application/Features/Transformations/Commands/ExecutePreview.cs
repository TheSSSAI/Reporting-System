using FluentValidation;
using MediatR;
using ReportingSystem.Service.Application.Common.Exceptions;
using ReportingSystem.Service.Application.Common.Interfaces;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ReportingSystem.Service.Application.Features.Transformations.Commands.ExecutePreview
{
    // REQ-INTG-DTR-001: The system shall expose a RESTful endpoint POST /api/v1/transformations/preview
    // REQ-PERF-DTR-001: The transformation preview API endpoint shall enforce a configurable, server-side execution timeout
    // US-046: Preview transformation output with sample data
    // US-047: Preview transformation output with live data
    // US-048: Receive a timeout error for long-running script previews
    public class ExecutePreview
    {
        public record Command : IRequest<JsonNode>
        {
            public string ScriptContent { get; init; } = string.Empty;
            public JsonNode? SampleData { get; init; }
            public Guid? ConnectorId { get; init; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(v => v.ScriptContent)
                    .NotEmpty().WithMessage("Script content cannot be empty.");

                RuleFor(v => v)
                    .Must(v => v.SampleData != null || v.ConnectorId.HasValue)
                    .WithMessage("Either sample data or a connector ID must be provided.")
                    .Must(v => !(v.SampleData != null && v.ConnectorId.HasValue))
                    .WithMessage("Sample data and a connector ID cannot be provided simultaneously.");
            }
        }

        public class Handler : IRequestHandler<Command, JsonNode>
        {
            private readonly ITransformationEngine _transformationEngine;
            // Assuming an IConnectorService exists to abstract away connector loading and execution.
            // This would be part of the application layer's defined interfaces.
            private readonly IConnectorService _connectorService;
            private readonly ILogger<Handler> _logger;

            public Handler(ITransformationEngine transformationEngine, IConnectorService connectorService, ILogger<Handler> logger)
            {
                _transformationEngine = transformationEngine;
                _connectorService = connectorService;
                _logger = logger;
            }

            public async Task<JsonNode> Handle(Command request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("Executing transformation preview.");

                JsonNode inputData;

                if (request.SampleData != null)
                {
                    _logger.LogDebug("Using provided sample data for preview.");
                    inputData = request.SampleData;
                }
                else if (request.ConnectorId.HasValue)
                {
                    _logger.LogDebug("Fetching live sample data from connector {ConnectorId} for preview.", request.ConnectorId.Value);
                    try
                    {
                        inputData = await _connectorService.FetchSampleDataAsync(request.ConnectorId.Value, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to fetch data from connector {ConnectorId}.", request.ConnectorId.Value);
                        throw new ValidationException($"Failed to fetch data from connector: {ex.Message}");
                    }
                }
                else
                {
                    // This case should be caught by the validator, but we handle it for robustness.
                    throw new InvalidOperationException("No data source provided for preview.");
                }
                
                // For a preview, we might use a specific set of constraints, possibly from configuration.
                // US-048 specifies a 15-second timeout for previews.
                var constraints = new ScriptConstraints
                {
                    Timeout = TimeSpan.FromSeconds(15), 
                    MaxStatements = 10000, // Default constraint
                    MaxMemory = 4 * 1024 * 1024 // 4MB default memory limit for preview
                };

                try
                {
                    var result = await _transformationEngine.ExecuteAsync(request.ScriptContent, inputData, constraints, cancellationToken);

                    if (result.IsSuccess && result.Output != null)
                    {
                        _logger.LogInformation("Transformation preview executed successfully.");
                        return result.Output;
                    }
                    else
                    {
                        _logger.LogWarning("Transformation preview failed with error: {Error}", result.Error?.Message);
                        throw new ScriptExecutionException(result.Error?.Message ?? "Unknown script execution error.", result.Error?.StackTrace, result.Error?.LineNumber);
                    }
                }
                catch (OperationCanceledException)
                {
                    _logger.LogWarning("Transformation preview timed out after {Timeout} seconds.", constraints.Timeout.TotalSeconds);
                    throw new ScriptExecutionException($"Execution timed out. The script took longer than {constraints.Timeout.TotalSeconds} seconds to complete.", null, null);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An unexpected error occurred during script preview execution.");
                    // Re-throw script execution exceptions, otherwise wrap.
                    if (ex is ScriptExecutionException) throw;
                    throw new ScriptExecutionException($"An unexpected error occurred: {ex.Message}", ex.StackTrace, null);
                }
            }
        }
    }
}