using System.ComponentModel.DataAnnotations;

namespace ReportingSystem.Shared.Contracts.Transformations
{
    /// <summary>
    /// Specifies the data allowed for updating an existing transformation script, as per US-044.
    /// </summary>
    /// <param name="Name">The name of the script.</param>
    /// <param name="Content">The JavaScript code for the script.</param>
    public record TransformationScriptUpdateRequest(
        [Required]
        [StringLength(100, MinimumLength = 3)]
        string Name,

        [Required]
        string Content);
}