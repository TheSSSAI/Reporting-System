using System.Runtime.Serialization;

namespace ReportingSystem.Core.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when a business rule or entity invariant is violated.
    /// This exception is used to communicate domain-level validation failures
    /// to the application layer.
    /// </summary>
    [Serializable]
    public class BusinessRuleValidationException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessRuleValidationException"/> class.
        /// </summary>
        public BusinessRuleValidationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessRuleValidationException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BusinessRuleValidationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessRuleValidationException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public BusinessRuleValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessRuleValidationException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        protected BusinessRuleValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}