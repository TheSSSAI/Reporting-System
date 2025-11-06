using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReportingSystem.Shared.Contracts.Connectors
{
    /// <summary>
    /// Specifies the request body for updating a data connector configuration.
    /// ConnectorType is immutable and not included.
    /// </summary>
    /// <param name="Name">The user-defined name for the connector.</param>
    /// <param name="Configuration">A dictionary of configuration key-value pairs specific to the connector type.</param>
    public record ConnectorConfigurationUpdateRequest(
        [Required]
        [StringLength(100)]
        string Name,

        [Required]
        Dictionary<string, object> Configuration);
}