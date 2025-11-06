//-----------------------------------------------------------------------
// <copyright file="DataFetchException.cs" company="Global Enterprises">
//     Copyright (c) Global Enterprises. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ReportingSystem.Plugins.Sdk.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents an error that occurs during the data fetching process of a custom data connector.
    /// This exception should be thrown by a plugin's implementation of <see cref="IConnector.FetchDataAsync"/>
    /// when it fails to retrieve data from the source for a known reason, such as a malformed query,
    /// a file not found at runtime, or an API returning an error. The host application's job execution
    /// engine will catch this exception to fail the report job gracefully and log the specific error.
    /// </summary>
    [Serializable]
    public class DataFetchException : ConnectorException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataFetchException"/> class.
        /// </summary>
        public DataFetchException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataFetchException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DataFetchException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataFetchException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public DataFetchException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataFetchException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        protected DataFetchException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}