using System.Runtime.Serialization;

namespace ReportingSystem.Core.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when a requested entity cannot be found.
    /// This provides a specific exception type for repository or service layers
    /// to indicate that a resource was not found by its identifier.
    /// </summary>
    [Serializable]
    public class EntityNotFoundException : DomainException
    {
        /// <summary>
        /// Gets the name of the entity that was not found.
        /// </summary>
        public string EntityName { get; }

        /// <summary>
        /// Gets the identifier of the entity that was not found.
        /// </summary>
        public object Id { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
        /// </summary>
        public EntityNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class with a default message.
        /// </summary>
        /// <param name="entityName">The name of the entity.</param>
        /// <param name="id">The identifier of the entity.</param>
        public EntityNotFoundException(string entityName, object id)
            : base($"Entity '{entityName}' with ID '{id}' was not found.")
        {
            EntityName = entityName;
            Id = id;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public EntityNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        protected EntityNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            EntityName = info.GetString(nameof(EntityName));
            Id = info.GetValue(nameof(Id), typeof(object));
        }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="SerializationInfo"/> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(EntityName), EntityName);
            info.AddValue(nameof(Id), Id);
        }
    }
}