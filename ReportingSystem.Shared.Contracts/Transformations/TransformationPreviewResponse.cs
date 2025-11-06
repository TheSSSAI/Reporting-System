using System.Text.Json.Serialization;

namespace ReportingSystem.Shared.Contracts.Transformations
{
    /// <summary>
    /// Specifies the response for a transformation script preview operation.
    /// This DTO is designed to be mutually exclusive: either the ResultJson is populated on success,
    /// or the Error property is populated on failure.
    /// </summary>
    /// <param name="ResultJson">The transformed JSON output as a string if the script execution was successful. Null on error.</param>
    /// <param name="Error">A structured error object containing details if the script execution failed. Null on success.</param>
    public record TransformationPreviewResponse(
        [property: JsonPropertyName("resultJson")]
        [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        string? ResultJson,

        [property: JsonPropertyName("error")]
        [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        ScriptErrorDetailDto? Error
    )
    {
        /// <summary>
        /// Gets a value indicating whether the preview was successful.
        /// </summary>
        [JsonIgnore]
        public bool IsSuccess => Error is null && ResultJson is not null;

        /// <summary>
        /// Creates a successful preview response.
        /// </summary>
        /// <param name="resultJson">The successfully transformed JSON string.</param>
        /// <returns>A new instance of <see cref="TransformationPreviewResponse"/> representing success.</returns>
        public static TransformationPreviewResponse Success(string resultJson) =>
            new(resultJson, null);

        /// <summary>
        /// Creates a failed preview response.
        /// </summary>
        /// <param name="error">The structured error details.</param>
        /// <returns>A new instance of <see cref="TransformationPreviewResponse"/> representing failure.</returns>
        public static TransformationPreviewResponse Failure(ScriptErrorDetailDto error) =>
            new(null, error);
    }
}