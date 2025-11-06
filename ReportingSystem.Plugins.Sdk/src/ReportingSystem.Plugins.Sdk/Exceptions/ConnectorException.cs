// Copyright (c) 2024-present, All rights reserved.
//-
//- THIS SOURCE CODE IS LICENSED UNDER THE APPLICABLE LICENSE AGREEMENT
//- FOR THE STARTED REPOSITORY.
//-

using System;
using System.Runtime.Serialization;

namespace ReportingSystem.Plugins.Sdk.Exceptions;

/// <summary>
/// Represents the base class for all exceptions thrown by a data connector plugin.
/// </summary>
/// <remarks>
/// The host application can use this base exception type to catch any error originating from a plugin,
/// allowing for robust and standardized error handling without needing to know the specifics of each plugin's implementation.
/// Plugin developers should throw more specific exceptions derived from this class where possible.
/// </remarks>
[Serializable]
public class ConnectorException : Exception
{
  /// <summary>
  /// Initializes a new instance of the <see cref="ConnectorException"/> class.
  /// </summary>
  public ConnectorException()
    : base("An error occurred within the connector.")
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ConnectorException"/> class with a specified error message.
  /// </summary>
  /// <param name="message">The message that describes the error.</param>
  public ConnectorException(string message)
    : base(message)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ConnectorException"/> class with a specified error message
  /// and a reference to the inner exception that is the cause of this exception.
  /// </summary>
  /// <param name="message">The error message that explains the reason for the exception.</param>
  /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
  public ConnectorException(string message, Exception innerException)
    : base(message, innerException)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ConnectorException"/> class with serialized data.
  /// </summary>
  /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
  /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
  protected ConnectorException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}