namespace ReportingSystem.Shared.Contracts.Auth
{
    /// <summary>
    /// Specifies the response body for a successful user authentication, as required by US-087 and US-088.
    /// </summary>
    /// <param name="AccessToken">Specifies the short-lived JWT access token.</param>
    /// <param name="RefreshToken">Specifies the long-lived refresh token.</param>
    /// <param name="ExpiresIn">Specifies the lifetime of the access token in seconds.</param>
    public record LoginResponse(
        string AccessToken,
        string RefreshToken,
        int ExpiresIn);
}