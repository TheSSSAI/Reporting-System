using System;

namespace ReportingSystem.Shared.Contracts.Reports
{
    /// <summary>
    /// Specifies the response contract for polling the status of an asynchronous job, as per US-095.
    /// </summary>
    /// <param name="JobId">The unique identifier of the job.</param>
    /// <param name="Status">The current status of the job (e.g., 'Queued', 'Running', 'Succeeded', 'Failed').</param>
    /// <param name="StartTime">The time the job started processing. Null if queued.</param>
    /// <param name="EndTime">The time the job finished processing. Null if not finished.</param>
    /// <param name="ResultUrl">The URL to retrieve the report artifact. Only present on success.</param>
    /// <param name="ErrorDetails">Details of the failure. Only present on failure.</param>
    public record JobStatusResponse(
        Guid JobId,
        string Status,
        DateTimeOffset? StartTime,
        DateTimeOffset? EndTime,
        string? ResultUrl,
        object? ErrorDetails);
}