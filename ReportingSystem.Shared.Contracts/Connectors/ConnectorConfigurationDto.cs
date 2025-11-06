using System;
using System.Collections.Generic;

namespace ReportingSystem.Shared.Contracts.Connectors
{
    /// <summary>
    /// Specifies the public representation of a data connector configuration.
    /// </summary>
    /// <param name="Id">The unique identifier for the connector.</param>
    /// <param name="Name">The user-defined name for the connector.</param>
    /// <param name="ConnectorType">The type of the connector (e.g., 'SQLServer', 'CSV').</param>
    /// <param name="Configuration">A dictionary of configuration key-value pairs. Sensitive values are omitted.</param>
    public record ConnectorConfigurationDto(
        Guid Id,
        string Name,
        string ConnectorType,
        IReadOnlyDictionary<string, object> Configuration);
}