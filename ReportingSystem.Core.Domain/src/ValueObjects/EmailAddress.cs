using ReportingSystem.Core.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace ReportingSystem.Core.Domain.ValueObjects;

/// <summary>
/// Represents an email address as a Value Object.
/// Encapsulates validation logic and ensures that an email address within the domain is always in a valid state.
/// This follows Domain-Driven Design principles to use Value Objects for descriptive aspects of the domain.
/// </summary>
public sealed record EmailAddress
{
    private static readonly Regex EmailRegex = new(
        @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

    /// <summary>
    /// Gets the string value of the email address.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAddress"/> class.
    /// </summary>
    /// <param name="value">The email address string.</param>
    /// <exception cref="DomainException">Thrown if the email address is null, empty, or has an invalid format.</exception>
    public EmailAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Email address cannot be null or empty.");
        }

        value = value.Trim();

        if (!EmailRegex.IsMatch(value))
        {
            throw new DomainException($"Email address '{value}' is not in a valid format.");
        }

        Value = value;
    }

    /// <summary>
    /// Provides an implicit conversion from an EmailAddress to a string.
    /// </summary>
    /// <param name="email">The EmailAddress object.</param>
    public static implicit operator string(EmailAddress email) => email.Value;

    /// <summary>
    /// Returns the string representation of the email address.
    /// </summary>
    /// <returns>The email address string.</returns>
    public override string ToString() => Value;
}