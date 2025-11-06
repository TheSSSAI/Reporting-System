namespace ReportingSystem.Core.Domain.Entities
{
    /// <summary>
    /// Represents a configured instance of a data connector.
    /// This entity stores the metadata and specific settings required to connect to a data source.
    /// </summary>
    public class ConnectorConfiguration
    {
        /// <summary>
        /// Gets the unique identifier for the connector configuration.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets the user-defined name for this connector instance.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the type of the connector (e.g., "SQLServer", "CsvFile", "CustomFhirConnector").
        /// This corresponds to the Name property of an IConnector implementation.
        /// </summary>
        public string ConnectorType { get; private set; }

        /// <summary>
        /// Gets or sets the JSON-serialized string containing the specific configuration settings
        /// for this connector type (e.g., connection string, file path, API URL).
        /// </summary>
        public string ConfigurationJson { get; set; }
        
        // Private constructor for EF Core
        private ConnectorConfiguration() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectorConfiguration"/> class.
        /// </summary>
        /// <param name="name">The name of the connector instance.</param>
        /// <param name="connectorType">The type of the connector.</param>
        /// <param name="configurationJson">The JSON configuration string.</param>
        public ConnectorConfiguration(string name, string connectorType, string configurationJson)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Connector name cannot be empty.", nameof(name));
            if (string.IsNullOrWhiteSpace(connectorType))
                throw new ArgumentException("Connector type cannot be empty.", nameof(connectorType));
            if (string.IsNullOrWhiteSpace(configurationJson))
                throw new ArgumentException("Configuration JSON cannot be empty.", nameof(configurationJson));

            Id = Guid.NewGuid();
            Name = name;
            ConnectorType = connectorType;
            ConfigurationJson = configurationJson;
        }
    }
}