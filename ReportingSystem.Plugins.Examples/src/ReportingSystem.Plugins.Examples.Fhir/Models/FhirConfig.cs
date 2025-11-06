using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReportingSystem.Plugins.Examples.Fhir.Models
{
    /// <summary>
    /// Provides a strongly-typed, immutable representation of the JSON configuration for the FhirConnector.
    /// This record is used to deserialize the configuration string provided to the IConnector methods,
    /// ensuring type safety and improving code clarity.
    /// </summary>
    /// <param name="BaseUrl">The base URL of the FHIR R4 server (e.g., "https://fhir.example.com/r4"). This field is required.</param>
    /// <param name="FhirQuery">The FHIR resource query string to execute (e.g., "Patient?name=smith"). This field is required.</param>
    /// <param name="BearerToken">An optional Bearer token for authentication with the FHIR server. If provided, it will be included in the Authorization header of requests.</param>
    public record FhirConfig(
        [property: JsonPropertyName("baseUrl")]
        [Required]
        string BaseUrl,

        [property: JsonPropertyName("fhirQuery")]
        [Required]
        string FhirQuery,

        [property: JsonPropertyName("bearerToken")]
        string? BearerToken
    );
}