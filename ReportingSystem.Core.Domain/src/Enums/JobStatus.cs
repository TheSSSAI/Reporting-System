namespace ReportingSystem.Core.Domain.Enums;

/// <summary>
/// Represents the lifecycle status of a report generation job.
/// As required by US-071, this provides clear states for monitoring.
/// </summary>
public enum JobStatus
{
    /// <summary>
    /// The job has been created and is waiting for an available executor.
    /// </summary>
    Queued = 0,

    /// <summary>
    /// The job is currently being processed by an executor.
    /// </summary>
    Running,

    /// <summary>
    /// The job completed all its steps successfully.
    /// </summary>
    Succeeded,

    /// <summary>
    /// The job failed at some point during its execution.
    /// </summary>
    Failed,

    /// <summary>
    /// The job was manually cancelled by an administrator.
    /// </summary>
    Cancelled
}