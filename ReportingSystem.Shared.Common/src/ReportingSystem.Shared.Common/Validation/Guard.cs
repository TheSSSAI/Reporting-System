using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace ReportingSystem.Shared.Common.Validation
{
    /// <summary>
    /// Provides a set of static methods for runtime argument validation (guard clauses).
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the specified argument is null.
        /// </summary>
        /// <param name="argument">The argument to check.</param>
        /// <param name="paramName">The name of the parameter, captured automatically by the compiler.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="argument"/> is null.</exception>
        public static void AgainstNull([NotNull] object? argument, [CallerArgumentExpression("argument")] string? paramName = null)
        {
            if (argument is null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the specified string argument is null,
        /// or an <see cref="ArgumentException"/> if it is empty or consists only of white-space characters.
        /// </summary>
        /// <param name="argument">The string argument to check.</param>
        /// <param name="paramName">The name of the parameter, captured automatically by the compiler.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="argument"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="argument"/> is empty or whitespace.</exception>
        public static void AgainstNullOrWhiteSpace([NotNull] string? argument, [CallerArgumentExpression("argument")] string? paramName = null)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                // Throw ArgumentNullException for null to be consistent with BCL behavior.
                if (argument is null)
                {
                    throw new ArgumentNullException(paramName);
                }
                
                // Throw ArgumentException for empty or whitespace.
                throw new ArgumentException("Argument cannot be empty or consist only of white-space characters.", paramName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if an integer is outside the specified range.
        /// </summary>
        /// <param name="argument">The integer argument to check.</param>
        /// <param name="min">The minimum allowed value (inclusive).</param>
        /// <param name="max">The maximum allowed value (inclusive).</param>
        /// <param name="paramName">The name of the parameter, captured automatically by the compiler.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the argument is less than min or greater than max.</exception>
        public static void AgainstOutOfRange(int argument, int min, int max, [CallerArgumentExpression("argument")] string? paramName = null)
        {
            if (argument < min || argument > max)
            {
                throw new ArgumentOutOfRangeException(paramName, $"Value must be between {min} and {max}.");
            }
        }
    }
}