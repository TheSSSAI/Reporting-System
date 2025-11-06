using ReportingSystem.Core.Domain.Enums;
using ReportingSystem.Core.Domain.Exceptions;

namespace ReportingSystem.Core.Domain.Entities
{
    /// <summary>
    /// Represents the log of a single execution of a report job.
    /// This entity encapsulates the state and outcome of a report run.
    /// </summary>
    public class JobExecutionLog
    {
        /// <summary>
        /// Gets the unique identifier for this job execution.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the identifier of the ReportConfiguration that this job is an instance of.
        /// </summary>
        public Guid ReportConfigurationId { get; private set; }
        
        /// <summary>
        /// Gets the time when the job execution was started.
        /// </summary>
        public DateTimeOffset StartTime { get; private set; }

        /// <summary>
        /// Gets the time when the job execution finished. Null if still running or queued.
        /// </summary>
        public DateTimeOffset? EndTime { get; private set; }

        /// <summary>
        /// Gets the current status of the job execution.
        /// </summary>
        public JobStatus Status { get; private set; }

        /// <summary>
        /// Gets metadata about the output, such as a file path or an external URL.
        /// </summary>
        public string? Output { get; private set; }

        /// <summary>
        /// Gets detailed error information if the job failed.
        /// </summary>
        public string? ErrorDetails { get; private set; }

        // Private constructor for EF Core
        private JobExecutionLog() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="JobExecutionLog"/> class.
        /// </summary>
        /// <param name="reportConfigurationId">The ID of the parent report configuration.</param>
        public JobExecutionLog(Guid reportConfigurationId)
        {
            if (reportConfigurationId == Guid.Empty)
                throw new ArgumentException("ReportConfigurationId cannot be empty.", nameof(reportConfigurationId));

            Id = Guid.NewGuid();
            ReportConfigurationId = reportConfigurationId;
            Status = JobStatus.Queued;
            StartTime = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Marks the job as running.
        /// </summary>
        public void MarkAsRunning()
        {
            if (Status != JobStatus.Queued)
                throw new BusinessRuleValidationException($"Cannot start job. Current status is '{Status}'.");
            
            Status = JobStatus.Running;
        }

        /// <summary>
        /// Marks the job as successfully completed.
        /// </summary>
        /// <param name="output">Metadata about the generated report output.</param>
        public void MarkAsSucceeded(string output)
        {
            if (Status != JobStatus.Running)
                throw new BusinessRuleValidationException($"Cannot succeed job. Current status is '{Status}'.");

            Status = JobStatus.Succeeded;
            Output = output;
            EndTime = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Marks the job as failed.
        /// </summary>
        /// <param name="errorDetails">Detailed information about the failure.</param>
        public void MarkAsFailed(string errorDetails)
        {
            if (Status != JobStatus.Running && Status != JobStatus.Queued)
                throw new BusinessRuleValidationException($"Cannot fail job. Current status is '{Status}'.");
            
            if (string.IsNullOrWhiteSpace(errorDetails))
                throw new ArgumentException("Error details must be provided for a failed job.", nameof(errorDetails));

            Status = JobStatus.Failed;
            ErrorDetails = errorDetails;
            EndTime = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Marks the job as cancelled.
        /// </summary>
        /// <param name="reason">The reason for cancellation.</param>
        public void MarkAsCancelled(string reason)
        {
            if (Status != JobStatus.Queued && Status != JobStatus.Running)
                throw new BusinessRuleValidationException($"Cannot cancel job. Current status is '{Status}'.");
            
            Status = JobStatus.Cancelled;
            ErrorDetails = $"Job was manually cancelled. Reason: {reason}";
            EndTime = DateTimeOffset.UtcNow;
        }
    }
}