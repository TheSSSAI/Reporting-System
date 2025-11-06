namespace ReportingSystem.Shared.Contracts.Transformations
{
    /// <summary>
    /// Specifies the structured error object for a failed script execution, as per REQ-FUNC-DTR-003.
    /// </summary>
    /// <param name="Message">The error message from the script engine.</param>
    /// <param name="StackTrace">The stack trace from the script, if available.</param>
    /// <param name="LineNumber">The line number where the error occurred, if available.</param>
    public record ScriptErrorDetailDto(
        string Message,
        string? StackTrace,
        int? LineNumber);
}