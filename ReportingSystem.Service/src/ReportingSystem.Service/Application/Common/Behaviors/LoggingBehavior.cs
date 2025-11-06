using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;

namespace ReportingSystem.Service.Application.Common.Behaviors;

/// <summary>
/// A MediatR pipeline behavior that logs information about incoming requests and their processing.
/// It logs the request name before handling and logs the completion status (success or failure) and duration afterwards.
/// This provides a consistent, structured logging approach for all CQRS operations.
/// </summary>
/// <typeparam name="TRequest">The type of the request being handled.</typeparam>
/// <typeparam name="TResponse">The type of the response from the handler.</typeparam>
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoggingBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="logger">The logger instance, injected by the DI container.</param>
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Handles the request by logging entry and exit points, measuring duration, and logging any exceptions.
    /// </summary>
    /// <param name="request">The request to handle.</param>
    /// <param name="next">The delegate for the next action in the pipeline.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation, containing the handler's response.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        
        _logger.LogInformation("Handling request {RequestName}. Request data: {@Request}", requestName, request);

        var stopwatch = Stopwatch.StartNew();

        try
        {
            var response = await next();
            stopwatch.Stop();
            
            _logger.LogInformation("Handled request {RequestName} successfully in {ElapsedMilliseconds}ms.", requestName, stopwatch.ElapsedMilliseconds);
            
            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(ex, "Request {RequestName} failed after {ElapsedMilliseconds}ms. Error: {ErrorMessage}", requestName, stopwatch.ElapsedMilliseconds, ex.Message);
            throw;
        }
    }
}