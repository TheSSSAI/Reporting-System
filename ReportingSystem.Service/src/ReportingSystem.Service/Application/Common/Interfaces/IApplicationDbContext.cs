using Microsoft.EntityFrameworkCore;
using ReportingSystem.Core.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace ReportingSystem.Service.Application.Common.Interfaces;

/// <summary>
/// Defines the contract for the application's database context.
/// This interface serves as the primary abstraction for data access,
/// combining the Repository and Unit of Work patterns.
/// </summary>
public interface IApplicationDbContext
{
    /// <summary>
    /// Gets or sets the collection of Users in the database.
    /// </summary>
    DbSet<User> Users { get; }

    /// <summary>
    /// Gets or sets the collection of Roles in the database.
    /// </summary>
    DbSet<Role> Roles { get; }

    /// <summary>
    /// Gets or sets the collection of UserRoles (join entity) in the database.
    /// </summary>
    DbSet<UserRole> UserRoles { get; }

    /// <summary>
    /// Gets or sets the collection of Transformation Scripts in the database.
    /// </summary>
    DbSet<TransformationScript> TransformationScripts { get; }

    /// <summary>
    /// Gets or sets the collection of Transformation Script Versions in the database.
    /// </summary>
    DbSet<TransformationScriptVersion> TransformationScriptVersions { get; }

    /// <summary>
    /// Gets or sets the collection of Report Configurations in the database.
    /// </summary>
    DbSet<ReportConfiguration> ReportConfigurations { get; }

    /// <summary>
    /// Gets or sets the collection of Job Execution Logs in the database.
    /// </summary>
    DbSet<JobExecutionLog> JobExecutionLogs { get; }

    /// <summary>
    /// Gets or sets the collection of JSON Schemas in the database.
    /// </summary>
    DbSet<JsonSchema> JsonSchemas { get; }

    /// <summary>
    /// Gets or sets the collection of application-wide configuration settings in the database.
    /// </summary>
    DbSet<ApplicationConfiguration> ApplicationConfigurations { get; }

    /// <summary>
    /// Gets or sets the collection of Audit Log entries in the database.
    /// </summary>
    DbSet<AuditLog> AuditLogs { get; }
    
    /// <summary>
    /// Gets or sets the collection of Security Violation Log entries in the database.
    /// </summary>
    DbSet<SecurityViolationLog> SecurityViolationLogs { get; }

    /// <summary>
    /// Asynchronously saves all changes made in this context to the underlying database.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous save operation. The task result contains the
    /// number of state entries written to the database.
    /// </returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}