using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReportingSystem.Shared.Contracts.Connectors
{
    /// <summary>
    /// Specifies the request body for testing a connector's configuration, as per US-038.
    /// </summary>
    /// <param name="ConnectorType">The type of the connector to test.</param>
    /// <param name="Configuration">The configuration parameters to test.</param>
    public record TestConnectionRequest(
        [Required]
        string ConnectorType,

        [Required]
        Dictionary<string, object> Configuration);
}