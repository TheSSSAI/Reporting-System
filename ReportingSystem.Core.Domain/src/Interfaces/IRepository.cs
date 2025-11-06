namespace ReportingSystem.Core.Domain.Interfaces;

/// <summary>
/// Represents a marker interface for a repository in Domain-Driven Design.
/// A repository is a collection-like interface that the domain layer uses to access
/// data persistence, abstracting the underlying data storage mechanism.
/// Specific repository interfaces (e.g., IUserRepository) will inherit from this
/// and define their own aggregate-specific methods.
/// </summary>
/// <typeparam name="T">The type of the aggregate root this repository manages.</typeparam>
public interface IRepository<T> where T : class
{
    // This is a marker interface.
    // Methods specific to aggregates (e.g., GetById, Add, Update) will be defined
    // in the specific repository interfaces like IUserRepository, IReportConfigurationRepository, etc.
    // This enforces the "repository per aggregate" pattern.
}