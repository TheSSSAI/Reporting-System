using System.ComponentModel.DataAnnotations;

namespace ReportingSystem.Infrastructure.Scripting
{
    /// <summary>
    /// Represents the configuration options for the Jint JavaScript engine sandbox.
    /// This class is designed to be populated from the application's configuration (e.g., appsettings.json)
    /// using the .NET Options pattern. It directly supports the requirements of REQ-SEC-DTR-001.
    /// </summary>
    public sealed class JintEngineOptions
    {
        /// <summary>
        /// The name of the configuration section in appsettings.json.
        /// </summary>
        public const string ConfigurationSectionName = "JintEngine";

        /// <summary>
        /// Gets or sets the maximum number of statements a script is allowed to execute.
        /// Helps prevent infinite loops and denial-of-service attacks.
        /// Default is 10,000.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "MaxStatements must be a positive integer.")]
        public int MaxStatements { get; set; } = 10000;

        /// <summary>
        /// Gets or sets the memory limit for the Jint engine in megabytes (MB).
        /// Prevents scripts from consuming excessive memory.
        /// Default is 128 MB.
        /// </summary>
        [Range(1, 4096, ErrorMessage = "MemoryLimitMb must be a positive integer, typically not exceeding 4096 MB.")]
        public long MemoryLimitMb { get; set; } = 128;

        /// <summary>
        /// Gets or sets the execution timeout for a script in seconds.
        /// Prevents long-running scripts from tying up server resources.
        /// Default is 10 seconds.
        /// </summary>
        [Range(1, 300, ErrorMessage = "TimeoutSeconds must be a positive integer, typically not exceeding 300 seconds.")]
        public int TimeoutSeconds { get; set; } = 10;
    }
}