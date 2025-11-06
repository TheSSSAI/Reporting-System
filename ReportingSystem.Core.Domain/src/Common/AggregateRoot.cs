using ReportingSystem.Core.Domain.Events;

namespace ReportingSystem.Core.Domain.Common
{
    /// <summary>
    /// Base class for aggregate roots in the domain.
    /// An aggregate root is an entity that is the entry point to an aggregate,
    /// ensuring the consistency of all changes to objects within the aggregate.
    /// </summary>
    /// <typeparam name="TId">The type of the aggregate root's identifier.</typeparam>
    public abstract class AggregateRoot<TId>
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        /// <summary>
        /// Gets the identifier of the aggregate root.
        /// </summary>
        public TId Id { get; protected set; }

        /// <summary>
        /// Gets the collection of domain events that have occurred.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot{TId}"/> class.
        /// </summary>
        /// <param name="id">The identifier of the aggregate root.</param>
        protected AggregateRoot(TId id)
        {
            Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot{TId}"/> class.
        /// Protected constructor for ORM frameworks.
        /// </summary>
        protected AggregateRoot() { }

        /// <summary>
        /// Adds a domain event to the aggregate root.
        /// </summary>
        /// <param name="domainEvent">The domain event to add.</param>
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Removes a domain event from the aggregate root.
        /// </summary>
        /// <param name="domainEvent">The domain event to remove.</param>
        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        /// <summary>
        /// Clears all domain events from the aggregate root.
        /// This is typically called after the events have been dispatched.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}