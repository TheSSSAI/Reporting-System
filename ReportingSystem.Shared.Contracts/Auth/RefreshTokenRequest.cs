using System.ComponentModel.DataAnnotations;

namespace ReportingSystem.Shared.Contracts.Auth
{
    /// <summary>
    /// Specifies the request body for refreshing an access token, as required by US-088.
    /// </summary>
    /// <param name="RefreshToken">The long-lived refresh token used to obtain a new access token.</param>
    public record RefreshTokenRequest(
        [Required] string RefreshToken);
}