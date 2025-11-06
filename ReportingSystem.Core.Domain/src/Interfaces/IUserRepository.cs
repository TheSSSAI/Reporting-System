using ReportingSystem.Core.Domain.Aggregates.UserAggregate;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace ReportingSystem.Core.Domain.Interfaces
{
    /// <summary>
    /// Defines the contract for a repository that handles data persistence for the User aggregate root.
    /// This interface extends the generic IRepository for standard CRUD operations and adds methods specific
    /// to the business requirements of user management and authentication.
    /// </summary>
    public interface IUserRepository : IRepository<User, Guid>
    {
        /// <summary>
        /// Finds a user by their unique username, performing a case-insensitive search.
        /// </summary>
        /// <param name="username">The username to search for.</param>
        /// <param name="cancellationToken">A token to support operation cancellation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the found User or null if not found.</returns>
        Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);

        /// <summary>
        /// Finds a user by their unique email address, performing a case-insensitive search.
        /// </summary>
        /// <param name="email">The email address to search for.</param>
        /// <param name="cancellationToken">A token to support operation cancellation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the found User or null if not found.</returns>
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a list of all users, including their assigned role information.
        /// This is typically used for user management interfaces.
        /// </summary>
        /// <param name="cancellationToken">A token to support operation cancellation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a read-only list of all User entities with their roles eagerly loaded.</returns>
        Task<IReadOnlyList<User>> ListAllWithRolesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a role by its name.
        /// </summary>
        /// <param name="roleName">The name of the role.</param>
        /// <param name="cancellationToken">A token to support operation cancellation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the found Role or null if not found.</returns>
        Task<Role?> GetRoleByNameAsync(string roleName, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Retrieves a list of all defined roles in the system.
        /// </summary>
        /// <param name="cancellationToken">A token to support operation cancellation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a read-only list of all Role entities.</returns>
        Task<IReadOnlyList<Role>> GetAllRolesAsync(CancellationToken cancellationToken = default);
    }
}