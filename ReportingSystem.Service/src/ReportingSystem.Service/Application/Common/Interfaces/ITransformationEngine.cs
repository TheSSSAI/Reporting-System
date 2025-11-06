using System.Text.Json.Nodes;
using ReportingSystem.Core.Domain.Entities;

namespace ReportingSystem.Service.Application.Common.Interfaces;

/// <summary>
/// Defines the contract for the JavaScript transformation engine.
/// This interface abstracts the underlying script execution mechanism (e.g., Jint).
/// </summary>
public interface ITransformationEngine
{
    /// <summary>
    /// Asynchronously executes a JavaScript transformation script against provided JSON data within a secure sandbox.
    /// </summary>
    /// <param name="script">The JavaScript (ECMAScript) code to execute.</param>
    /// <param name="jsonData">The input data for the script, represented as a flexible JsonNode.</param>
    /// <param name="constraints">The security sandbox constraints to apply during execution.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests, used for enforcing timeouts.</param>
    /// <returns>A task that represents the asynchronous execution operation. The task result contains a <see cref="TransformationResult"/> with either the transformed data or error details.</returns>
    Task<TransformationResult> ExecuteAsync(
        TransformationScriptVersion script,
        JsonNode jsonData,
        ScriptConstraints constraints,
        CancellationToken cancellationToken);
}

/// <summary>
/// Represents the security and resource constraints for a script execution.
/// </summary>
/// <param name="Timeout">The maximum execution time allowed for the script.</param>
/// <param name="MaxMemoryBytes">The maximum memory allocation in bytes allowed for the script.</param>
/// <param name="MaxStatements">The maximum number of statements the script is allowed to execute.</param>
/// <param name="AllowClr">A flag indicating whether Common Language Runtime (CLR) access is permitted.</param>
public record ScriptConstraints(
    TimeSpan Timeout,
    long MaxMemoryBytes,
    int MaxStatements,
    bool AllowClr);

/// <summary>
/// Represents the outcome of a transformation script execution.
/// This is a discriminated union-like class to hold either a success or failure result.
/// </summary>
public class TransformationResult
{
    /// <summary>
    /// Gets the transformed JSON data if the execution was successful. Null otherwise.
    /// </summary>
    public JsonNode? TransformedData { get; }

    /// <summary>
    /// Gets the error details if the execution failed. Null otherwise.
    /// </summary>
    public TransformationError? Error { get; }

    /// <summary>
    /// Gets a value indicating whether the transformation was successful.
    /// </summary>
    public bool IsSuccess => Error is null;

    private TransformationResult(JsonNode? transformedData, TransformationError? error)
    {
        TransformedData = transformedData;
        Error = error;
    }

    /// <summary>
    /// Creates a success result.
    /// </summary>
    /// <param name="data">The successfully transformed JSON data.</param>
    /// <returns>A new <see cref="TransformationResult"/> instance representing success.</returns>
    public static TransformationResult Success(JsonNode data) => new(data, null);

    /// <summary>
    /// Creates a failure result.
    /// </summary>
    /// <param name="error">The details of the transformation error.</param>
    /// <returns>A new <see cref="TransformationResult"/> instance representing failure.</returns>
    public static TransformationResult Failure(TransformationError error) => new(null, error);
}

/// <summary>
/// Represents the details of an error that occurred during script transformation.
/// </summary>
/// <param name="Type">The type of the error (e.g., 'SyntaxError', 'RuntimeException', 'Timeout', 'MemoryLimit').</param>
/// <param name="Message">The descriptive error message.</param>
/// <param name="StackTrace">The stack trace from the script engine, if available.</param>
/// <param name="LineNumber">The line number in the script where the error occurred, if available.</param>
public record TransformationError(
    string Type,
    string Message,
    string? StackTrace,
    int? LineNumber);