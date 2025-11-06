namespace ReportingSystem.Core.Domain.Entities
{
    /// <summary>
    /// Represents an immutable audit log entry for a security-sensitive event.
    /// As per DDD, this entity is immutable after creation to ensure the integrity of the audit trail.
    /// </summary>
    public class AuditLog
    {
        /// <summary>
        /// Gets the unique identifier for the audit log entry.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Gets the timestamp of when the event occurred.
        /// </summary>
        public DateTimeOffset Timestamp { get; private set; }

        /// <summary>
        /// Gets the ID of the user who performed the action. Can be null for system events.
        /// </summary>
        public Guid? UserId { get; private set; }

        /// <summary>
        /// Gets the type of action performed (e.g., "USER_LOGIN_FAILED", "REPORT_CONFIG_UPDATED").
        /// </summary>
        public string ActionType { get; private set; }

        /// <summary>
        /// Gets the type of the entity that was affected (e.g., "User", "ReportConfiguration").
        /// </summary>
        public string? EntityType { get; private set; }
        
        /// <summary>
        /// Gets the identifier of the entity that was affected.
        /// </summary>
        public string? EntityId { get; private set; }

        /// <summary>
        /// Gets the outcome of the action (e.g., "Success", "Failure").
        /// </summary>
        public string Outcome { get; private set; }

        /// <summary>
        /// Gets the details of the change, typically as a JSON string representing old and new values.
        /// </summary>
        public string? ChangeDetails { get; private set; }

        /// <summary>
        /// Gets the source IP address from which the action was initiated.
        /// </summary>
        public string? SourceIpAddress { get; private set; }

        // Private constructor for EF Core
        private AuditLog() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditLog"/> class.
        /// </summary>
        /// <param name="userId">The ID of the user performing the action.</param>
        /// <param name="actionType">The type of action.</param>
        /// <param name="outcome">The outcome of the action.</param>
        /// <param name="entityType">The type of the affected entity.</param>
        /// <param name="entityId">The ID of the affected entity.</param>
        /// <param name="changeDetails">JSON details of the changes.</param>
        /// <param name="sourceIpAddress">The source IP address.</param>
        public AuditLog(Guid? userId, string actionType, string outcome, string? entityType, string? entityId, string? changeDetails, string? sourceIpAddress)
        {
            if (string.IsNullOrWhiteSpace(actionType))
                throw new ArgumentException("ActionType cannot be empty.", nameof(actionType));

            if (string.IsNullOrWhiteSpace(outcome))
                throw new ArgumentException("Outcome cannot be empty.", nameof(outcome));

            Timestamp = DateTimeOffset.UtcNow;
            UserId = userId;
            ActionType = actionType;
            EntityType = entityType;
            EntityId = entityId;
            Outcome = outcome;
            ChangeDetails = changeDetails;
            SourceIpAddress = sourceIpAddress;
        }
    }
}