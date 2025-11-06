using System;

namespace ReportingSystem.Shared.Contracts.Reports
{
    /// <summary>
    /// Specifies a summary of a job execution for display in the monitoring dashboard, as required by US-070.
    /// </summary>
    /// <param name="JobId">The unique identifier of the job execution.</param>
    /// <param name="ReportName">The name of the report that was executed.</param>
    /// <param name="Status">The final or current status of the job.</param>
    /// <param name="StartTime">When the job execution started.</param>
    /// <param name="EndTime">When the job execution ended. Null if still running.</param>
    /// <param name="Duration">The total duration of the job execution.</param>
    public record JobExecutionLogSummaryDto(
        Guid JobId,
        string ReportName,
        string Status,
        DateTimeOffset StartTime,
        DateTimeOffset? EndTime,
        TimeSpan? Duration);
}