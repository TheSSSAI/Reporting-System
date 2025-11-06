using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReportingSystem.Shared.Common.Serialization
{
    /// <summary>
    /// Provides a centralized, pre-configured singleton instance of <see cref="JsonSerializerOptions"/>
    /// for use across the application, ensuring consistent JSON serialization behavior.
    /// This fulfills requirement REQ-DATA-DTR-001.
    /// </summary>
    public static class DefaultJsonSerializerOptions
    {
        private static readonly JsonSerializerOptions _instance = CreateOptions();

        /// <summary>
        /// Gets the singleton instance of the configured <see cref="JsonSerializerOptions"/>.
        /// This instance is configured for camelCase naming, string enum conversion, and ignoring null values.
        /// </summary>
        public static JsonSerializerOptions Instance => _instance;

        private static JsonSerializerOptions CreateOptions()
        {
            var options = new JsonSerializerOptions
            {
                // Convert property names to camelCase (e.g., PropertyName -> propertyName).
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

                // Ignore properties with null values when writing JSON.
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,

                // Allow for more lenient parsing, such as trailing commas and comments (useful for configuration files).
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true,

                // Write indented JSON for better readability where appropriate (can be overridden at serialization call site).
                WriteIndented = false,
            };

            // Add a converter to serialize enums as strings instead of their integer values.
            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            
            return options;
        }
    }
}