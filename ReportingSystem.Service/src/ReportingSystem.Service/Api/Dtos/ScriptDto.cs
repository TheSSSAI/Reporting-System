using System.Text.Json.Serialization;

namespace ReportingSystem.Service.Api.Dtos;

/// <summary>
/// Data Transfer Object representing a transformation script, including its metadata and active version number.
/// Used as a response model in the API layer.
/// Fulfills parts of REQ-FUNC-DTR-004 and REQ-FUNC-DTR-005.
/// </summary>
/// <param name="Id">The unique identifier of the script.</param>
/// <param name="Name">The name of the script.</param>
/// <param name="Description">The description of the script.</param>
/// <param name="ActiveVersionNumber">The version number of the currently active script version.</param>
/// <param name="CreatedAt">The timestamp when the script was originally created.</param>
/// <param name="UpdatedAt">The timestamp when the script was last updated.</param>
public record ScriptDto(
    [property: JsonPropertyName("id")]
    Guid Id,

    [property: JsonPropertyName("name")]
    string Name,

    [property: JsonPropertyName("description")]
    string? Description,

    [property: JsonPropertyName("activeVersionNumber")]
    int ActiveVersionNumber,

    [property: JsonPropertyName("createdAt")]
    DateTime CreatedAt,

    [property: JsonPropertyName("updatedAt")]
    DateTime UpdatedAt
);