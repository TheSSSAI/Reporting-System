using System.Text.Json.Serialization;

namespace ReportingSystem.Service.Api.Dtos;

/// <summary>
/// Data Transfer Object representing a specific version of a transformation script.
/// Used as a response model in the API layer, for example, when fetching a script's details or history.
/// Fulfills parts of REQ-FUNC-DTR-005.
/// </summary>
/// <param name="Id">The unique identifier of this specific script version.</param>
/// <param name="VersionNumber">The sequential version number.</param>
/// <param name="Content">The JavaScript content of this version.</param>
/// <param name="CreatedAt">The timestamp when this version was created.</param>
/// <param name="CreatedByUsername">The username of the user who created this version.</param>
public record ScriptVersionDto(
    [property: JsonPropertyName("id")]
    Guid Id,

    [property: JsonPropertyName("versionNumber")]
    int VersionNumber,

    [property: JsonPropertyName("content")]
    string Content,

    [property: JsonPropertyName("createdAt")]
    DateTime CreatedAt,

    [property: JsonPropertyName("createdByUsername")]
    string CreatedByUsername
);