namespace ReportingSystem.Core.Domain.Aggregates.ReportConfigurationAggregate
{
    /// <summary>
    /// Represents a configured delivery destination for a report.
    /// This is a child entity within the ReportConfiguration aggregate.
    /// </summary>
    public class DeliveryDestination
    {
        /// <summary>
        /// Gets the unique identifier for the delivery destination.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the identifier of the parent ReportConfiguration.
        /// </summary>
        public Guid ReportConfigurationId { get; private set; }

        /// <summary>
        /// Gets the type of the destination (e.g., "Email", "S3", "LocalFile").
        /// </summary>
        public string DestinationType { get; private set; }

        /// <summary>
        /// Gets the JSON-serialized configuration specific to this destination type.
        /// </summary>
        public string ConfigurationJson { get; private set; }

        // Private constructor for EF Core
        private DeliveryDestination() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryDestination"/> class.
        /// </summary>
        /// <param name="destinationType">The type of the destination.</param>
        /// <param name="configurationJson">The JSON configuration for the destination.</param>
        public DeliveryDestination(string destinationType, string configurationJson)
        {
            if (string.IsNullOrWhiteSpace(destinationType))
                throw new ArgumentException("Destination type cannot be empty.", nameof(destinationType));

            if (string.IsNullOrWhiteSpace(configurationJson))
                throw new ArgumentException("Configuration JSON cannot be empty.", nameof(configurationJson));

            Id = Guid.NewGuid();
            DestinationType = destinationType;
            ConfigurationJson = configurationJson;
        }
        
        /// <summary>
        /// Updates the configuration of the delivery destination.
        /// </summary>
        /// <param name="newConfigurationJson">The new JSON configuration.</param>
        public void UpdateConfiguration(string newConfigurationJson)
        {
            if (string.IsNullOrWhiteSpace(newConfigurationJson))
                throw new ArgumentException("Configuration JSON cannot be empty.", nameof(newConfigurationJson));
                
            ConfigurationJson = newConfigurationJson;
        }
    }
}