using ReportingSystem.Core.Domain.Events;
using System;

namespace ReportingSystem.Core.Domain.Events
{
    /// <summary>
    /// Represents a domain event that is raised when a new user account is created.
    /// This event can be handled by other parts of the system to perform actions
    /// such as sending a welcome email or creating an audit log entry, without
    /// tightly coupling them to the user creation process.
    /// </summary>
    /// <param name="UserId">The unique identifier of the newly created user.</param>
    public record UserCreatedEvent(Guid UserId) : IDomainEvent;
}