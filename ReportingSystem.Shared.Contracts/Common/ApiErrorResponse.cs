using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ReportingSystem.Shared.Contracts.Common
{
    /// <summary>
    /// Specifies a standardized structure for returning error details from the API, used for 4xx and 5xx responses.
    /// This ensures a consistent error handling contract for all API consumers.
    /// </summary>
    /// <param name="StatusCode">Specifies the HTTP status code of the error.</param>
    /// <param name="Message">Specifies a user-friendly error message.</param>
    /// <param name="Errors">Specifies a collection of validation errors, where the key is the field name and the value is an array of error messages for that field. This property is nullable.</param>
    public record ApiErrorResponse(
        [property: JsonPropertyName("statusCode")] int StatusCode,
        [property: JsonPropertyName("message")] string Message,
        [property: JsonPropertyName("errors"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] IReadOnlyDictionary<string, string[]>? Errors = null
    );
}