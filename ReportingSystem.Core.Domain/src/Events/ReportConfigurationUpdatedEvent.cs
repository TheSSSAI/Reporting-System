using ReportingSystem.Core.Domain.Events;
using System;

namespace ReportingSystem.Core.Domain.Events
{
    /// <summary>
    /// Represents a domain event that is raised when a report configuration is updated.
    /// This event is used to decouple the update operation from side effects like audit logging.
    /// </summary>
    /// <param name="ReportConfigurationId">The unique identifier of the report configuration that was updated.</param>
    /// <param name="UpdatedByUserId">The unique identifier of the user who performed the update.</param>
    public record ReportConfigurationUpdatedEvent(
        Guid ReportConfigurationId,
        Guid UpdatedByUserId) : IDomainEvent;
}