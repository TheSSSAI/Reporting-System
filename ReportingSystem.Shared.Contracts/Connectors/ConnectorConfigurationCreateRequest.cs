using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReportingSystem.Shared.Contracts.Connectors
{
    /// <summary>
    /// Specifies the request body for creating a new data connector configuration.
    /// </summary>
    /// <param name="Name">The user-defined name for the connector.</param>
    /// <param name="ConnectorType">The type of the connector (e.g., 'SQLServer', 'CSV').</param>
    /// <param name="Configuration">A dictionary of configuration key-value pairs specific to the connector type.</param>
    public record ConnectorConfigurationCreateRequest(
        [Required]
        [StringLength(100)]
        string Name,

        [Required]
        string ConnectorType,

        [Required]
        Dictionary<string, object> Configuration);
}