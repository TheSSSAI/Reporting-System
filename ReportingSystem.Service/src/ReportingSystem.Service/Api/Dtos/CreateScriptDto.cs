using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReportingSystem.Service.Api.Dtos;

/// <summary>
/// Data Transfer Object for creating a new transformation script.
/// Used in the API layer to represent the creation request payload.
/// Fulfills part of REQ-FUNC-DTR-004.
/// </summary>
/// <param name="Name">The unique name for the transformation script.</param>
/// <param name="Description">An optional description of the script's purpose.</param>
/// <param name="ScriptContent">The JavaScript (ES6) content of the script.</param>
public record CreateScriptDto(
    [property: JsonPropertyName("name")]
    [Required(ErrorMessage = "Script name is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
    string Name,

    [property: JsonPropertyName("description")]
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    string? Description,

    [property: JsonPropertyName("scriptContent")]
    [Required(ErrorMessage = "Script content is required.")]
    string ScriptContent
);