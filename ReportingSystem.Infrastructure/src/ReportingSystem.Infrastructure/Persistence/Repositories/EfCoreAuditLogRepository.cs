using Microsoft.EntityFrameworkCore;
using ReportingSystem.Application.Interfaces;
using ReportingSystem.Domain.Entities;
using System.Linq.Expressions;

namespace ReportingSystem.Infrastructure.Persistence.Repositories;

/// <summary>
/// EF Core implementation for IAuditLogger and ISecurityViolationLogger.
/// This repository is write-optimized and provides methods to persist audit and security events.
/// </summary>
public class EfCoreAuditLogRepository : IAuditLogger, ISecurityViolationLogger
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="EfCoreAuditLogRepository"/> class.
    /// </summary>
    /// <param name="context">The application's database context.</param>
    public EfCoreAuditLogRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Logs a general audit event to the persistent store.
    /// </summary>
    /// <param name="auditEvent">The audit event to log.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task LogAuditEventAsync(AuditLog auditEvent, CancellationToken cancellationToken = default)
    {
        await _context.AuditLogs.AddAsync(auditEvent, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Logs a security violation event to the persistent store.
    /// </summary>
    /// <param name="violationEvent">The security violation event to log.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task LogSecurityViolationAsync(SecurityViolationLog violationEvent, CancellationToken cancellationToken = default)
    {
        await _context.SecurityViolationLogs.AddAsync(violationEvent, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Finds audit log entries based on a predicate.
    /// This method is designed to be flexible for querying the audit log from an admin interface.
    /// </summary>
    /// <param name="predicate">The filter predicate.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A collection of matching audit log entries.</returns>
    public async Task<IEnumerable<AuditLog>> FindAsync(Expression<Func<AuditLog, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.AuditLogs
            .Where(predicate)
            .AsNoTracking()
            .OrderByDescending(log => log.Timestamp)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves a paginated list of all audit logs.
    /// </summary>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A paginated list of audit logs.</returns>
    public async Task<IEnumerable<AuditLog>> GetAllAuditLogsAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _context.AuditLogs
            .AsNoTracking()
            .OrderByDescending(log => log.Timestamp)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}