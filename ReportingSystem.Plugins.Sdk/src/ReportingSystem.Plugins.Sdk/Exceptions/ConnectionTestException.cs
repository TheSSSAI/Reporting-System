//-----------------------------------------------------------------------
// <copyright file="ConnectionTestException.cs" company="Global Enterprises">
//     Copyright (c) Global Enterprises. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ReportingSystem.Plugins.Sdk.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents an error that occurs during the connection test of a custom data connector.
    /// This exception should be thrown by a plugin's implementation of <see cref="IConnector.TestConnectionAsync"/>
    /// for predictable, specific failures such as invalid credentials, unreachable hosts, or insufficient permissions.
    /// The host application can specifically catch this exception to provide targeted feedback to the user.
    /// </summary>
    [Serializable]
    public class ConnectionTestException : ConnectorException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionTestException"/> class.
        /// </summary>
        public ConnectionTestException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionTestException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ConnectionTestException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionTestException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ConnectionTestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionTestException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        protected ConnectionTestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}