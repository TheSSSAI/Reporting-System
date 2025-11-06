using System.ComponentModel.DataAnnotations;

namespace ReportingSystem.Shared.Contracts.Auth
{
    /// <summary>
    /// Specifies the request body for user authentication, as required by US-087.
    /// </summary>
    /// <param name="Username">The user's username.</param>
    /// <param name="Password">The user's password.</param>
    public record LoginRequest(
        [Required] string Username,
        [Required] string Password);
}