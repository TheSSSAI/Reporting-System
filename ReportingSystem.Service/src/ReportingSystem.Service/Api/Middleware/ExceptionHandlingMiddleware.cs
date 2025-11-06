using System.Text.Json;
using Jint.Runtime;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ReportingSystem.Service.Application.Common.Exceptions;
using ReportingSystem.Service.Api.Dtos;

namespace ReportingSystem.Service.Api.Middleware;

/// <summary>
/// Global exception handling middleware for the API pipeline.
/// Catches unhandled exceptions, logs them, and returns a standardized JSON error response.
/// This fulfills requirements REQ-FUNC-DTR-003 and REQ-REL-DTR-001.
/// </summary>
public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    /// <summary>
    /// Invokes the middleware to handle exceptions in the request pipeline.
    /// </summary>
    /// <param name="context">The HttpContext for the current request.</param>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <returns>A Task that represents the completion of request processing.</returns>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred while processing the request for {Path}", context.Request.Path);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        int statusCode = StatusCodes.Status500InternalServerError;
        object response;

        switch (exception)
        {
            case ValidationException validationException:
                statusCode = StatusCodes.Status400BadRequest;
                response = new { errors = validationException.Errors };
                break;

            case NotFoundException notFoundException:
                statusCode = StatusCodes.Status404NotFound;
                response = new { error = notFoundException.Message };
                break;

            case JavaScriptException jsException:
                statusCode = StatusCodes.Status400BadRequest;
                var errorDetails = new ErrorDetails(
                    jsException.Message,
                    _env.IsDevelopment() ? jsException.StackTrace : null, // Only show stack trace in development
                    jsException.LineNumber.ToString());
                response = new ErrorResponseDto(errorDetails);
                _logger.LogWarning("A JavaScript execution error occurred: {Message} at line {LineNumber}", jsException.Message, jsException.LineNumber);
                break;

            case OperationCanceledException:
                statusCode = StatusCodes.Status400BadRequest; // Treat client-side timeout as a bad request
                response = new { error = "The operation was cancelled, likely due to a timeout." };
                _logger.LogWarning("Request was cancelled, possibly due to a timeout for {Path}", context.Request.Path);
                break;
                
            case UnauthorizedAccessException:
                statusCode = StatusCodes.Status401Unauthorized;
                response = new { error = "Unauthorized." };
                break;
                
            // A custom ForbiddenAccessException could be created in Application/Common/Exceptions
            // For now, handling it via a more specific case or letting it fall to 500 is an option.
            // case ForbiddenAccessException:
            //     statusCode = StatusCodes.Status403Forbidden;
            //     response = new { error = "Forbidden." };
            //     break;

            default:
                statusCode = StatusCodes.Status500InternalServerError;
                response = new { error = _env.IsDevelopment()
                    ? $"An unexpected error occurred: {exception.Message}"
                    : "An unexpected internal server error has occurred." };
                break;
        }

        context.Response.StatusCode = statusCode;
        
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
    }
}