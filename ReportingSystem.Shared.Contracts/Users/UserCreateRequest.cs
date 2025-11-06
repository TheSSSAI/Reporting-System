using System.ComponentModel.DataAnnotations;

namespace ReportingSystem.Shared.Contracts.Users
{
    /// <summary>
    /// Specifies the data required to create a new user account, as per US-018.
    /// </summary>
    /// <param name="Username">The new user's unique username.</param>
    /// <param name="Email">The new user's unique email address.</param>
    /// <param name="Password">The new user's initial password. Password policy is enforced server-side.</param>
    /// <param name="FullName">The new user's full name.</param>
    /// <param name="Role">The role to assign to the new user (e.g., 'Administrator', 'Viewer').</param>
    public record UserCreateRequest(
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        string Username,

        [Required]
        [EmailAddress]
        string Email,

        [Required]
        string Password,

        [Required]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
        string FullName,

        [Required]
        string Role);
}