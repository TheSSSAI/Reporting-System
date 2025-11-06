using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReportingSystem.Service.Api.Dtos;

/// <summary>
/// Data Transfer Object for updating an existing transformation script.
/// Used in the API layer to represent the update request payload (e.g., for a PUT request).
/// Fulfills part of REQ-FUNC-DTR-004.
/// </summary>
/// <param name="Name">The new name for the transformation script.</param>
/// <param name="Description">The new optional description for the script.</param>
/// <param name="ScriptContent">The new JavaScript (ES6) content for the script.</param>
public record UpdateScriptDto(
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