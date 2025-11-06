using FluentValidation.Results;

namespace ReportingSystem.Service.Application.Common.Exceptions;

/// <summary>
/// Represents an exception that occurs during validation of application layer requests.
/// This exception is used to propagate structured validation errors to the presentation layer.
/// </summary>
public class ValidationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class.
    /// </summary>
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class with a collection of validation failures.
    /// </summary>
    /// <param name="failures">A collection of validation failures from FluentValidation.</param>
    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    /// <summary>
    /// Gets the dictionary of validation errors, where the key is the property name and the value is an array of error messages.
    /// </summary>
    public IDictionary<string, string[]> Errors { get; }
}