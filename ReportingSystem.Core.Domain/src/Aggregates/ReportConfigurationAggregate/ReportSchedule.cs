using ReportingSystem.Core.Domain.Exceptions;

namespace ReportingSystem.Core.Domain.Aggregates.ReportConfigurationAggregate
{
    /// <summary>
    /// Represents the schedule for a report as a Value Object.
    /// It is immutable and defined by its properties.
    /// </summary>
    public record ReportSchedule
    {
        /// <summary>
        /// Gets the CRON expression for the schedule.
        /// </summary>
        public string CronExpression { get; }

        /// <summary>
        /// Gets a value indicating whether the schedule is enabled.
        /// </summary>
        public bool IsEnabled { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportSchedule"/> class.
        /// </summary>
        /// <param name="cronExpression">The CRON expression. Can be empty if schedule is disabled.</param>
        /// <param name="isEnabled">A flag indicating if the schedule is active.</param>
        public ReportSchedule(string cronExpression, bool isEnabled)
        {
            if (isEnabled && string.IsNullOrWhiteSpace(cronExpression))
            {
                throw new BusinessRuleValidationException("An enabled schedule must have a valid CRON expression.");
            }

            // Further CRON validation would happen in the Application layer or a domain service,
            // as pure syntax validation can be complex and might require external libraries,
            // which we want to avoid in a pure value object. This constructor enforces the core invariant.

            CronExpression = isEnabled ? cronExpression : string.Empty;
            IsEnabled = isEnabled;
        }

        /// <summary>
        /// Creates a disabled schedule.
        /// </summary>
        /// <returns>A new instance of a disabled schedule.</returns>
        public static ReportSchedule Disabled() => new(string.Empty, false);
    }
}