using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace ReportingSystem.Service.Api.Dtos;

/// <summary>
/// Represents the request body for the transformation script preview endpoint.
/// Fulfills the contract required by REQ-INTG-DTR-001.
/// The request must contain either sample data or a connector ID, but not both.
/// This is enforced by a FluentValidator at the application layer.
/// </summary>
/// <param name="ScriptContent">The JavaScript (ES6) transformation script to be executed.</param>
/// <param name="SampleData">The sample JSON data to be used as input for the transformation. This is mutually exclusive with ConnectorId.</param>
/// <param name="ConnectorId">The ID of a configured connector to fetch a live data sample from. This is mutually exclusive with SampleData.</param>
public record PreviewRequestDto(
    [property: JsonPropertyName("scriptContent")]
    [Required(ErrorMessage = "Script content is required.")]
    string ScriptContent,

    [property: JsonPropertyName("sampleData")]
    JsonNode? SampleData,

    [property: JsonPropertyName("connectorId")]
    Guid? ConnectorId
);