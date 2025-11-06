using System.ComponentModel.DataAnnotations;

namespace ReportingSystem.Shared.Contracts.Users
{
    /// <summary>
    /// Specifies the data allowed for updating an existing user account, as per US-020.
    /// Username is immutable and not included in the update request.
    /// </summary>
    /// <param name="Email">The user's email address.</param>
    /// <param name="FullName">The user's full name.</param>
    /// <param name="Role">The user's assigned role.</param>
    /// <param name="IsActive">The user's active status.</param>
    public record UserUpdateRequest(
        [Required]
        [EmailAddress]
        string Email,

        [Required]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
        string FullName,

        [Required]
        string Role,

        [Required]
        bool IsActive);
}