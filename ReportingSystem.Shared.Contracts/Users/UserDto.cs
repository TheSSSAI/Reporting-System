using System;

namespace ReportingSystem.Shared.Contracts.Users
{
    /// <summary>
    /// Specifies the public representation of a user account, returned by GET requests.
    /// CRITICAL: This DTO must not expose any sensitive security-related information.
    /// </summary>
    /// <param name="Id">The unique identifier for the user.</param>
    /// <param name="Username">The user's unique username.</param>
    /// <param name="Email">The user's email address.</param>
    /// <param name="FullName">The user's full name.</param>
    /// <param name="Role">The user's assigned role (e.g., 'Administrator', 'Viewer').</param>
    /// <param name="IsActive">Indicates if the user account is active.</param>
    public record UserDto(
        Guid Id,
        string Username,
        string Email,
        string FullName,
        string Role,
        bool IsActive);
}