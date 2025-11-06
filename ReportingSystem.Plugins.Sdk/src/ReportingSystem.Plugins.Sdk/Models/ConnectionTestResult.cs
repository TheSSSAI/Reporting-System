// Copyright (c) 2024-present, All rights reserved.
//-
//- THIS SOURCE CODE IS LICENSED UNDER THE APPLICABLE LICENSE AGREEMENT
//- FOR THE STARTED REPOSITORY.
//-

namespace ReportingSystem.Plugins.Sdk.Models;

/// <summary>
/// Represents the result of a connection test for a data connector.
/// This is an immutable record, ensuring that the result of a test is a clear, unchangeable fact.
/// </summary>
/// <remarks>
/// This record is returned by the <c>IConnector.TestConnectionAsync</c> method to provide standardized feedback to the user interface.
/// </remarks>
/// <param name="IsSuccess">Indicates whether the connection test was successful.</param>
/// <param name="Message">Provides a descriptive message about the outcome of the test. On failure, this should contain actionable information for the user.</param>
public record ConnectionTestResult(bool IsSuccess, string Message)
{
  /// <summary>
  /// Creates a success result with a default message.
  /// </summary>
  /// <returns>A new <see cref="ConnectionTestResult"/> instance indicating success.</returns>
  public static ConnectionTestResult Success()
  {
    return new ConnectionTestResult(true, "Connection test successful.");
  }

  /// <summary>
  /// Creates a success result with a custom message.
  /// </summary>
  /// <param name="message">The custom success message.</param>
  /// <returns>A new <see cref="ConnectionTestResult"/> instance indicating success.</returns>
  public static ConnectionTestResult Success(string message)
  {
    if (string.IsNullOrWhiteSpace(message))
    {
      throw new ArgumentException("Success message cannot be null or whitespace.", nameof(message));
    }
    return new ConnectionTestResult(true, message);
  }

  /// <summary>
  /// Creates a failure result with a specific error message.
  /// </summary>
  /// <param name="errorMessage">The error message describing the reason for failure.</param>
  /// <returns>A new <see cref="ConnectionTestResult"/> instance indicating failure.</returns>
  public static ConnectionTestResult Failure(string errorMessage)
  {
    if (string.IsNullOrWhiteSpace(errorMessage))
    {
      throw new ArgumentException("Error message cannot be null or whitespace.", nameof(errorMessage));
    }
    return new ConnectionTestResult(false, errorMessage);
  }
}