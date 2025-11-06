using ReportingSystem.Core.Domain.Entities;
using System.Text.Json.Nodes;

namespace ReportingSystem.Core.Domain.Interfaces;

/// <summary>
/// Defines the contract for a data connector plugin.
/// All custom connectors must implement this interface to be discoverable and usable by the system.
/// This interface is the cornerstone of the system's data ingestion extensibility.
/// </summary>
public interface IConnector
{
    /// <summary>
    /// Gets the unique, user-friendly name of the connector type.
    /// This name is displayed in the Control Panel UI.
    /// e.g., "Microsoft SQL Server", "CSV File", "My Custom HR System"
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Returns a JSON schema string that defines the configuration UI for this connector.
    /// The frontend uses this schema to dynamically render the appropriate form fields.
    /// This enables 'plug-and-play' UI for custom connectors without frontend code changes.
    /// </summary>
    /// <returns>A string containing a valid JSON schema.</returns>
    string GetConfigurationSchema();

    /// <summary>
    /// Performs a live test of the connection using the provided configuration.
    /// This method is called when an Administrator clicks the 'Test Connection' button in the UI.
    /// It should validate all aspects of the configuration: network connectivity, authentication, permissions, and existence of the target resource (e.g., database, file path).
    /// </summary>
    /// <param name="config">The connector configuration entity containing all settings to be tested.</param>
    /// <param name="cancellationToken">A token to support cancellation of the connection test, especially for network-bound operations.</param>
    /// <returns>A task that represents the asynchronous test operation.</returns>
    /// <exception cref="Exceptions.BusinessRuleValidationException">Thrown when the configuration is fundamentally invalid before a connection is attempted.</exception>
    /// <exception cref="System.Net.Http.HttpRequestException">Thrown for network-related failures.</exception>
    /// <exception cref="System.Security.Authentication.AuthenticationException">Thrown for authentication failures (e.g., bad username/password).</exception>
    /// <exception cref="System.UnauthorizedAccessException">Thrown for authorization/permission failures.</exception>
    /// <exception cref="System.OperationCanceledException">Thrown if the operation is cancelled via the cancellation token.</exception>
    Task TestConnectionAsync(ConnectorConfiguration config, CancellationToken cancellationToken = default);

    /// <summary>
    /// Connects to the data source defined in the configuration, fetches the data,
    /// and returns it in a standardized JSON format.
    /// This is the primary data ingestion method called by the report generation engine.
    /// </summary>
    /// <param name="config">The connector configuration entity containing all settings for this execution.</param>
    /// <param name="cancellationToken">A token to support cancellation of long-running data fetch operations.</param>
    /// <returns>
    /// A task that represents the asynchronous data fetch operation. The task result is a
    /// <see cref="JsonNode"/> representing the fetched data. This should typically be a
    /// JsonArray of JsonObjects, where each object represents a row of data.
    /// </returns>
    /// <exception cref="Exceptions.DomainException">Implementations should throw domain-specific exceptions for various failure modes to be handled by the job engine.</exception>
    /// <exception cref="System.OperationCanceledException">Thrown if the operation is cancelled via the cancellation token.</exception>
    Task<JsonNode> FetchDataAsync(ConnectorConfiguration config, CancellationToken cancellationToken = default);
}