using ReportingSystem.Core.Domain.Aggregates.ReportConfigurationAggregate;
using ReportingSystem.Core.Domain.Aggregates.UserAggregate;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace ReportingSystem.Core.Domain.Interfaces
{
    /// <summary>
    /// Defines the contract for a repository that handles data persistence for the ReportConfiguration aggregate root.
    /// This interface extends the generic IRepository for standard CRUD operations and adds methods specific
    /// to the business requirements of report configurations.
    /// </summary>
    public interface IReportConfigurationRepository : IRepository<ReportConfiguration, Guid>
    {
        /// <summary>
        /// Finds a report configuration by its unique name, performing a case-insensitive search.
        /// </summary>
        /// <param name="name">The name of the report configuration to find.</param>
        /// <param name="cancellationToken">A token to support operation cancellation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the found ReportConfiguration or null if not found.</returns>
        Task<ReportConfiguration?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a list of all report configurations that a specific user is authorized to view, based on their user ID and assigned roles.
        /// Administrators are implicitly granted access to all reports.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="roleIds">A list of role IDs assigned to the user.</param>
        /// <param name="cancellationToken">A token to support operation cancellation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a read-only list of accessible ReportConfiguration entities.</returns>
        Task<IReadOnlyList<ReportConfiguration>> ListForUserAsync(Guid userId, IReadOnlyList<int> roleIds, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the total count of report configurations that have an active schedule.
        /// This is used to enforce business rules, such as limitations in a trial mode.
        /// </summary>
        /// <param name="cancellationToken">A token to support operation cancellation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the integer count of active scheduled reports.</returns>
        Task<int> GetActiveScheduledCountAsync(CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Retrieves a report configuration with its full aggregate details, including delivery destinations and schedule.
        /// </summary>
        /// <param name="id">The unique identifier of the report configuration.</param>
        /// <param name="cancellationToken">A token to support operation cancellation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the fully loaded ReportConfiguration or null if not found.</returns>
        Task<ReportConfiguration?> GetByIdWithChildrenAsync(Guid id, CancellationToken cancellationToken = default);
    }
}