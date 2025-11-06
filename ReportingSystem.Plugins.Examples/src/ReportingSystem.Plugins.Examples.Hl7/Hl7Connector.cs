using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using NHapi.Base.Model;
using NHapi.Base.Parser;
using NHapi.Base.Util;
using ReportingSystem.Plugins.Sdk;
using ReportingSystem.Plugins.Examples.Hl7.Models;

namespace ReportingSystem.Plugins.Examples.Hl7;

/// <summary>
/// A reference implementation of an IConnector for parsing HL7 v2 message files.
/// This connector demonstrates reading a file from a specified path, parsing the HL7 messages,
/// and transforming the complex, pipe-delimited structure into a structured JSON format.
/// It is intended as a guide for System Integrators creating custom file-based connectors.
/// </summary>
public class Hl7Connector : IConnector
{
    private static readonly JsonSerializerOptions SerializerOptions = new() { PropertyNameCaseInsensitive = true };
    
    /// <summary>
    /// Gets the display name of the connector.
    /// </summary>
    /// <returns>The connector's user-friendly name.</returns>
    public string GetName() => "HL7 v2 File Connector";

    /// <summary>
    /// Gets the JSON schema that defines the configuration fields for this connector.
    /// </summary>
    /// <returns>A string containing a valid JSON schema.</returns>
    public string GetConfigurationSchema()
    {
        return """
               {
                 "type": "object",
                 "properties": {
                   "filePath": {
                     "type": "string",
                     "title": "File Path",
                     "description": "The full local or UNC path to the HL7 message file (e.g., C:\\data\\hl7.txt or \\\\server\\share\\messages.hl7)."
                   },
                   "encoding": {
                     "type": "string",
                     "title": "File Encoding",
                     "description": "The text encoding of the file.",
                     "default": "UTF-8"
                   }
                 },
                 "required": ["filePath", "encoding"]
               }
               """;
    }

    /// <summary>
    /// Tests the connection by verifying the file exists, is readable, and appears to contain valid HL7 data.
    /// It reads and attempts to parse only the first message to ensure a quick and lightweight test.
    /// </summary>
    /// <param name="configJson">A JSON string containing the user-provided configuration.</param>
    /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
    /// <returns>A ValidationResult indicating success or failure with a descriptive message.</returns>
    public async Task<ValidationResult> TestConnectionAsync(string configJson, CancellationToken cancellationToken)
    {
        try
        {
            var config = JsonSerializer.Deserialize<Hl7Config>(configJson, SerializerOptions);

            var validationContext = new ValidationContext(config!);
            var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            if (!Validator.TryValidateObject(config!, validationContext, validationResults, true))
            {
                var errors = string.Join("; ", validationResults.Select(r => r.ErrorMessage));
                return ValidationResult.Failure($"Invalid configuration: {errors}");
            }
            
            if (!File.Exists(config!.FilePath))
            {
                return ValidationResult.Failure($"File not found at the specified path: {config.FilePath}");
            }

            var encoding = Encoding.GetEncoding(config.Encoding);

            // Read just enough of the file to test parsing.
            // This avoids reading a potentially huge file during a simple connection test.
            string fileContentSample;
            using (var stream = new FileStream(config.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new StreamReader(stream, encoding))
            {
                var buffer = new char[4096];
                int bytesRead = await reader.ReadAsync(buffer, cancellationToken);
                fileContentSample = new string(buffer, 0, bytesRead);
            }

            if (string.IsNullOrWhiteSpace(fileContentSample))
            {
                return ValidationResult.Success("Connection successful. File is accessible but empty.");
            }

            var parser = new PipeParser();
            await parser.ParseAsync(fileContentSample, cancellationToken);

            return ValidationResult.Success("Connection successful. File is accessible and appears to be valid HL7.");
        }
        catch (JsonException ex)
        {
            return ValidationResult.Failure($"Configuration is not valid JSON. Details: {ex.Message}");
        }
        catch (FileNotFoundException)
        {
            return ValidationResult.Failure("File not found at the specified path.");
        }
        catch (UnauthorizedAccessException)
        {
            return ValidationResult.Failure("Access denied. The service lacks permissions to read the file.");
        }
        catch (Exception ex) when (ex is NHapi.Base.HL7Exception or ArgumentException)
        {
             return ValidationResult.Failure($"Parsing failed. The file may not be a valid HL7 v2 message file. Details: {ex.Message}");
        }
        catch (Exception ex)
        {
            return ValidationResult.Failure($"An unexpected error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Fetches data by reading and parsing the entire specified HL7 file.
    /// Each message in the file is transformed into a corresponding JsonObject.
    /// </summary>
    /// <param name="configJson">A JSON string containing the validated, saved configuration.</param>
    /// <param name="cancellationToken">A token to observe for cancellation requests.</param>
    /// <returns>A JsonNode containing a JsonArray of the parsed HL7 messages.</returns>
    /// <exception cref="ConnectorFetchDataException">Thrown when any error occurs during file reading or parsing.</exception>
    public async Task<JsonNode> FetchDataAsync(string configJson, CancellationToken cancellationToken)
    {
        Hl7Config? config;
        try
        {
            config = JsonSerializer.Deserialize<Hl7Config>(configJson, SerializerOptions);
            if (config == null)
            {
                throw new ConnectorFetchDataException("Failed to parse HL7 connector configuration.");
            }
        }
        catch (JsonException ex)
        {
            throw new ConnectorFetchDataException("Configuration is not valid JSON.", ex);
        }

        try
        {
            var encoding = Encoding.GetEncoding(config.Encoding);
            var fileContent = await File.ReadAllTextAsync(config.FilePath, encoding, cancellationToken);

            if (string.IsNullOrWhiteSpace(fileContent))
            {
                return new JsonArray();
            }

            var parser = new PipeParser();
            var messages = parser.ParseAll(fileContent);

            var results = new JsonArray();
            foreach (var message in messages)
            {
                cancellationToken.ThrowIfCancellationRequested();
                results.Add(Hl7MessageToJson(message));
            }

            return results;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new ConnectorFetchDataException($"Failed to fetch or parse HL7 data from '{config.FilePath}'. Details: {ex.Message}", ex);
        }
    }
    
    /// <summary>
    /// Transforms an NHapi IMessage object into a System.Text.Json.Nodes.JsonObject.
    /// </summary>
    private JsonObject Hl7MessageToJson(IMessage message)
    {
        var messageJson = new JsonObject();
        var allSegments = message.GetAllSegments();

        foreach (var segment in allSegments)
        {
            var segmentJson = new JsonObject();
            for (int i = 1; i <= segment.NumFields(); i++)
            {
                var field = segment.GetField(i);
                var fieldNode = Terser.Get(segment, segment.GetStructureName(), i, 1, 1, 1) != null
                    ? FieldToJson(field)
                    : null;
                
                if (fieldNode != null)
                {
                    segmentJson.Add(i.ToString(), fieldNode);
                }
            }
            // In HL7, some segments can repeat (e.g., NK1). This logic handles that.
            if (messageJson.ContainsKey(segment.GetStructureName()))
            {
                if (messageJson[segment.GetStructureName()] is JsonArray array)
                {
                    array.Add(segmentJson);
                }
                else // It was an object, convert to array
                {
                    var existing = messageJson[segment.GetStructureName()];
                    messageJson[segment.GetStructureName()] = new JsonArray(existing!.DeepClone(), segmentJson);
                }
            }
            else
            {
                messageJson.Add(segment.GetStructureName(), segmentJson);
            }
        }
        return messageJson;
    }

    /// <summary>
    /// Transforms an NHapi IType (Field, Component, etc.) into a JsonNode.
    /// This is a recursive helper to handle the nested structure of HL7.
    /// </summary>
    private JsonNode? FieldToJson(IType field)
    {
        if (field is Varies varies)
        {
            return FieldToJson(varies.Data);
        }
        
        if (field is IPrimitive primitive)
        {
            return JsonValue.Create(primitive.Value);
        }

        if (field is IComposite composite)
        {
            var compositeJson = new JsonObject();
            for (int i = 1; i <= composite.GetNumberOfComponents(); i++)
            {
                var component = composite.GetComponent(i);
                var componentNode = FieldToJson(component);
                if (componentNode != null)
                {
                    compositeJson.Add(i.ToString(), componentNode);
                }
            }
            return compositeJson.Count > 0 ? compositeJson : null;
        }
        
        return null;
    }
}