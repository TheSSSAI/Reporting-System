using System.Globalization;
using System.Text;

namespace ReportingSystem.Shared.Common.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="string"/> class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a string from PascalCase or camelCase to snake_case.
        /// Handles acronyms correctly (e.g., "HTTPRequest" becomes "http_request").
        /// </summary>
        /// <param name="input">The input string to convert.</param>
        /// <returns>The snake_cased string, or the original input if it is null, empty, or whitespace.</returns>
        public static string? ToSnakeCase(this string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            var builder = new StringBuilder(input.Length + 5);
            var previousCharIsLetterOrDigit = false;

            for (var i = 0; i < input.Length; i++)
            {
                var currentChar = input[i];

                if (char.IsUpper(currentChar))
                {
                    // Add underscore if it's not the first character and the previous was a letter/digit
                    // or if it's part of an acronym (e.g., the 'R' in 'HTTPRequest')
                    if (i > 0 &&
                        (previousCharIsLetterOrDigit ||
                         (i + 1 < input.Length && char.IsLower(input[i + 1]))))
                    {
                        builder.Append('_');
                    }

                    builder.Append(char.ToLower(currentChar, CultureInfo.InvariantCulture));
                }
                else
                {
                    builder.Append(currentChar);
                }

                previousCharIsLetterOrDigit = char.IsLetterOrDigit(currentChar);
            }

            return builder.ToString();
        }
    }
}