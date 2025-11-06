namespace ReportingSystem.Shared.Contracts.Connectors
{
    /// <summary>
    /// Specifies the response from a connector connection test, as per US-038.
    /// </summary>
    /// <param name="IsSuccess">Indicates whether the connection test was successful.</param>
    /// <param name="Message">A user-friendly message describing the outcome of the test.</param>
    public record TestConnectionResponse(
        bool IsSuccess,
        string Message);
}