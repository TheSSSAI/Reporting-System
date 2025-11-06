using ReportingSystem.Core.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace ReportingSystem.Service.Application.Common.Interfaces;

/// <summary>
/// Defines the contract for logging security-sensitive audit events.
/// This interface decouples application logic from the specific audit persistence mechanism.
/// </summary>
public interface IAuditLogger
{
    /// <summary>
    /// Asynchronously logs a structured audit event.
    /// </summary>
    /// <param name="auditEvent">A domain object representing the structured event to be logged.</param>
    /// <returns>A task that represents the asynchronous log operation.</returns>
    Task LogAuditEventAsync(AuditLog auditEvent);
    
    /// <summary>
    /// Asynchronously logs a structured security violation event.
    /// </summary>
    /// <param name="violationEvent">A domain object representing the security violation to be logged.</param>
    /// <returns>A task that represents the asynchronous log operation.</returns>
    Task LogSecurityViolationEventAsync(SecurityViolationLog violationEvent);
}