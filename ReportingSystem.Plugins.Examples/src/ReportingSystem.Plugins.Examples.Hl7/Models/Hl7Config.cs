using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReportingSystem.Plugins.Examples.Hl7.Models
{
    /// <summary>
    /// Provides a strongly-typed, immutable representation of the JSON configuration for the Hl7Connector.
    /// This record is used to deserialize the configuration string provided to the IConnector methods,
    /// ensuring type safety and improving code clarity for file-based HL7 ingestion.
    /// </summary>
    /// <param name="FilePath">The full local or UNC path to the HL7 v2 message file. This field is required.</param>
    /// <param name="Encoding">The text encoding of the file (e.g., "UTF-8", "ISO-8859-1"). Defaults to "UTF-8". This field is required.</param>
    public record Hl7Config(
        [property: JsonPropertyName("filePath")]
        [Required]
        string FilePath,

        [property: JsonPropertyName("encoding")]
        [Required]
        string Encoding
    );
}