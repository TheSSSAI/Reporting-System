using System.Text.Json.Serialization;

namespace ReportingSystem.Service.Api.Dtos;

/// <summary>
/// Represents the standardized structure for returning script execution errors via the API.
/// Fulfills the contract defined in REQ-FUNC-DTR-003.
/// </summary>
/// <param name="Error">Details of the script execution error.</param>
public record ErrorResponseDto(
    [property: JsonPropertyName("error")]
    ErrorDetails Error
);

/// <summary>
/// Contains the specific details of a script execution error.
/// </summary>
/// <param name="Message">The error message from the script engine.</param>
/// <param name="StackTrace">The stack trace from the script, if available.</param>
/// <param name="LineNumber">The line number in the script where the error occurred, if available.</param>
public record ErrorDetails(
    [property: JsonPropertyName("message")]
    string Message,

    [property: JsonPropertyName("stackTrace")]
    string? StackTrace,

    [property: JsonPropertyName("lineNumber")]
    int? LineNumber
);