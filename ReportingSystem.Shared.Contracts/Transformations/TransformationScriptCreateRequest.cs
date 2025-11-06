using System.ComponentModel.DataAnnotations;

namespace ReportingSystem.Shared.Contracts.Transformations
{
    /// <summary>
    /// Specifies the data required to create a new transformation script, as per US-043.
    /// </summary>
    /// <param name="Name">The unique name for the script.</param>
    /// <param name="Content">The JavaScript code for the script.</param>
    public record TransformationScriptCreateRequest(
        [Required]
        [StringLength(100, MinimumLength = 3)]
        string Name,

        [Required]
        string Content);
}