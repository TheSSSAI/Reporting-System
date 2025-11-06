using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using ReportingSystem.Plugins.Sdk;
using ReportingSystem.Plugins.Examples.Fhir.Models;

namespace ReportingSystem.Plugins.Examples.Fhir;

/// <summary>
/// A reference implementation of an IConnector for a FHIR R4 compliant server.
/// This connector demonstrates connecting to a FHIR API, executing a query,
/// and transforming the resulting FHIR resources into the system's standard JsonNode format.
/// It is intended as a guide for System Integrators creating custom connectors.
/// </summary>
public class FhirConnector : IConnector
{
    private static readonly JsonSerializerOptions SerializerOptions = new() { PropertyNameCaseInsensitive = true };
    private static readonly FhirJsonSerializer FhirSerializer = new(new SerializerSettings { Pretty = false });

    /// <summary>
    /// Gets the display name of the connector.
    /// </summary>
    /// <returns>The connector's user-friendly name.</returns>
    public string GetName() => "FHIR R4 Connector";

    /// <summary>
    /// Gets the JSON schema that defines the configuration fields for this connector.
    /// The main application's UI will use this schema to dynamically render a configuration form.
    /// </summary>
    /// <returns>A string containing a valid JSON schema.</returns>
    public string GetConfigurationSchema()
    {
        // This schema defines the fields the user will see in the Control Panel.
        // "title", "description", "type" are standard JSON Schema properties.
        // The "format": "password" property is a hint to the UI to render a masked input field.
        return """
               {
                 "type": "object",
                 "properties": {
                   "baseUrl": {
                     "type": "string",
                     "title": "FHIR Server Base URL",
                     "description": "The base URL of the FHIR R4 server (e.g., https://fhir.example.com/r4)."
                   },
                   "fhirQuery": {
                     "type": "string",
                     "title": "FHIR Query",
                     "description": "The FHIR resource query to execute (e.g., Patient?name=smith)."
                   },
                   "bearerToken": {
                     "type": "string",
                     "title": "Bearer Token (Optional)",
                     "description": "An optional bearer token for OAuth 2.0 authentication.",
                     "format": "password"
                   }
                 },
                 "required": ["baseUrl", "fhirQuery"]
               }
               """;
    }

    /// <summary>
    /// Tests the connection to the FHIR server using the provided configuration.
    /// It attempts to fetch the server's CapabilityStatement as a lightweight validation check.
    /// </summary>
    /// <param name="configJson">A JSON string containing the user-provided configuration.</param>
    /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
    /// <returns>A ValidationResult indicating success or failure with a descriptive message.</returns>
    public async Task<ValidationResult> TestConnectionAsync(string configJson, CancellationToken cancellationToken)
    {
        try
        {
            var config = JsonSerializer.Deserialize<FhirConfig>(configJson, SerializerOptions);
            
            // Basic validation on the deserialized configuration object.
            var validationContext = new ValidationContext(config!);
            var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            if (!Validator.TryValidateObject(config!, validationContext, validationResults, true))
            {
                var errors = string.Join("; ", validationResults.Select(r => r.ErrorMessage));
                return ValidationResult.Failure($"Invalid configuration: {errors}");
            }

            var settings = new FhirClientSettings
            {
                Timeout = 15_000, // 15 second timeout for tests
                VerifyFhirVersion = true
            };

            var fhirClient = new FhirClient(config!.BaseUrl, settings);
            
            if (!string.IsNullOrWhiteSpace(config.BearerToken))
            {
                fhirClient.OnBeforeRequest += (sender, e) =>
                {
                    e.RawRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", config.BearerToken);
                };
            }

            // A lightweight way to test the connection and verify it's a valid FHIR endpoint.
            await fhirClient.CapabilityStatementAsync(cancellationToken);
            
            return ValidationResult.Success("Connection to FHIR server successful.");
        }
        catch (JsonException ex)
        {
            return ValidationResult.Failure($"Configuration is not valid JSON. Details: {ex.Message}");
        }
        catch (FhirOperationException ex)
        {
            // This often indicates an authentication or authorization issue (e.g., 401, 403).
            return ValidationResult.Failure($"FHIR server returned an error: {ex.StatusCode}. Message: {ex.Message}");
        }
        catch (HttpRequestException ex)
        {
            // This typically indicates a network issue (DNS, firewall, server down).
            return ValidationResult.Failure($"Network error: Could not connect to the FHIR server. Details: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Catch-all for any other unexpected errors.
            return ValidationResult.Failure($"An unexpected error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Fetches data from the FHIR server based on the provided configuration.
    /// It executes the configured FHIR query and transforms the results into a JsonArray.
    /// </summary>
    /// <param name="configJson">A JSON string containing the validated, saved configuration.</param>
    /// <param name="cancellationToken">A token to observe for cancellation requests from the host application.</param>
    /// <returns>A JsonNode containing a JsonArray of the fetched FHIR resources.</returns>
    /// <exception cref="ConnectorFetchDataException">Thrown when any error occurs during data fetching or processing.</exception>
    public async Task<JsonNode> FetchDataAsync(string configJson, CancellationToken cancellationToken)
    {
        FhirConfig? config;
        try
        {
            config = JsonSerializer.Deserialize<FhirConfig>(configJson, SerializerOptions);
            if (config == null)
            {
                throw new ConnectorFetchDataException("Failed to parse FHIR connector configuration.");
            }
        }
        catch (JsonException ex)
        {
            throw new ConnectorFetchDataException("Configuration is not valid JSON.", ex);
        }

        try
        {
            var fhirClient = new FhirClient(config.BaseUrl);
            
            if (!string.IsNullOrWhiteSpace(config.BearerToken))
            {
                fhirClient.OnBeforeRequest += (sender, e) =>
                {
                    e.RawRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", config.BearerToken);
                };
            }

            // Execute the search query provided in the configuration.
            var bundle = await fhirClient.SearchAsync(config.FhirQuery, cancellationToken);
            
            var results = new JsonArray();

            // NOTE FOR INTEGRATORS: This example does not implement paging. For production use,
            // you should check bundle.NextLink and continue fetching pages until all results are retrieved.
            if (bundle != null)
            {
                foreach (var entry in bundle.Entry)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    if (entry.Resource != null)
                    {
                        // Serialize the FHIR resource to a JSON string, then parse it into a JsonNode.
                        // This two-step process ensures a clean conversion to the standard System.Text.Json model.
                        string resourceJson = FhirSerializer.SerializeToString(entry.Resource);
                        JsonNode? resourceNode = JsonNode.Parse(resourceJson);
                        if (resourceNode != null)
                        {
                            results.Add(resourceNode);
                        }
                    }
                }
            }
            
            return results;
        }
        catch (OperationCanceledException)
        {
            // Re-throw to allow the host application to handle cancellation gracefully.
            throw;
        }
        catch (Exception ex)
        {
            // Wrap any other exception in the SDK-defined exception type.
            // This provides a consistent error handling mechanism for the host application.
            throw new ConnectorFetchDataException($"Failed to fetch data from FHIR server. Details: {ex.Message}", ex);
        }
    }
}