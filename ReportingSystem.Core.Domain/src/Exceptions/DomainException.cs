using System;
using System.Runtime.Serialization;

namespace ReportingSystem.Core.Domain.Exceptions;

/// <summary>
/// Base class for custom exceptions thrown by the Domain layer.
/// This allows the Application layer to catch domain-specific errors and handle them
/// differently from system or infrastructure exceptions, providing clearer error signaling.
/// </summary>
[Serializable]
public abstract class DomainException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainException"/> class.
    /// </summary>
    protected DomainException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    protected DomainException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    protected DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainException"/> class with serialized data.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
    protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}