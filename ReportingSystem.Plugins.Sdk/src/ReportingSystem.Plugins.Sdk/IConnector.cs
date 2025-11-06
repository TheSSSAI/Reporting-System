using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using ReportingSystem.Plugins.Sdk.Exceptions;
using ReportingSystem.Plugins.Sdk.Models;

namespace ReportingSystem.Plugins.Sdk
{
    /// <summary>
    /// Defines the public contract for a custom data connector plugin.
    /// Implement this interface to create a new data source for the Reporting System.
    /// Implementations must have a public, parameterless constructor for dynamic loading.
    /// </summary>
    /// <remarks>
    /// All implementations of this interface must be stateless and thread-safe. The host application
    /// may reuse a single instance of a connector to execute multiple operations concurrently.
    /// </remarks>
    public interface IConnector
    {
        /// <summary>
        /// Gets the user-friendly, display name of the connector.
        /// This name will appear in the Control Panel UI when an Administrator selects a connector type.
        /// </summary>
        /// <returns>A non-empty string representing the name of the connector (e.g., "Microsoft SQL Server", "FHIR API").</returns>
        /// <remarks>This method should be a simple property-like getter and must not throw exceptions.</remarks>
        string GetName();

        /// <summary>
        /// Gets a JSON schema that defines the configuration fields required by this connector.
        /// The schema is used by the Control Panel to dynamically generate a configuration UI for the Administrator.
        /// </summary>
        /// <returns>A valid JSON string that conforms to the schema expected by the dynamic form renderer.</returns>
        /// <remarks>
        /// The schema defines fields, their types (text, password, checkbox, etc.), labels, and validation rules.
        /// This method should be a simple property-like getter and must not throw exceptions.
        /// </remarks>
        string GetConfigurationSchema();

        /// <summary>
        /// Performs a live test of the connection to the data source using the provided configuration.
        /// This method is called when an Administrator clicks the 'Test Connection' button in the Control Panel.
        /// It should validate connectivity, authentication, permissions, and any other prerequisites.
        /// </summary>
        /// <param name="configuration">A JsonNode object containing the configuration values entered by the user, matching the structure defined by <see cref="GetConfigurationSchema"/>.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests. The test operation should be aborted if cancellation is requested.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> that represents the asynchronous operation. The task result contains a
        /// <see cref="ConnectionTestResult"/> indicating success or failure with a descriptive message.
        /// </returns>
        /// <exception cref="ConnectionTestException">Thrown for predictable, user-correctable errors during the test (e.g., authentication failure, invalid path).</exception>
        /// <exception cref="ConnectorException">Thrown for other unexpected, connector-specific errors during the test.</exception>
        /// <exception cref="System.OperationCanceledException">Thrown if the operation is canceled via the cancellationToken.</exception>
        Task<ConnectionTestResult> TestConnectionAsync(JsonNode configuration, CancellationToken cancellationToken);

        /// <summary>
        /// Fetches data from the source using the provided configuration. This is the primary data ingestion method
        /// used during a report job's execution.
        /// </summary>
        /// <param name="configuration">A JsonNode object containing the saved configuration for this connector instance.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests. The data fetch operation should be aborted if the job is cancelled.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> that represents the asynchronous operation. The task result contains a
        /// <see cref="JsonNode"/>, which should typically be a <see cref="System.Text.Json.Nodes.JsonArray"/> of <see cref="System.Text.Json.Nodes.JsonObject"/>s
        /// representing the retrieved dataset.
        /// </returns>
        /// <remarks>
        /// For preview operations (e.g., live data preview in the UI), the host application may add a property to the configuration object
        /// (e.g., "isPreview": true) to signal that a limited data sample is required. It is the connector's responsibility to honor this if present.
        /// </remarks>
        /// <exception cref="DataFetchException">Thrown for predictable, runtime errors during data retrieval (e.g., query execution failure, file not found).</exception>
        /// <exception cref="ConnectorException">Thrown for other unexpected, connector-specific errors during data fetching.</exception>
        /// <exception cref="System.OperationCanceledException">Thrown if the operation is canceled via the cancellationToken.</exception>
        Task<JsonNode> FetchDataAsync(JsonNode configuration, CancellationToken cancellationToken);
    }
}