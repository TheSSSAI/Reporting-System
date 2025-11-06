# 1 Design

code_design

# 2 Code Specfication

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-07-PLUGINS-EXAMPLES |
| Validation Timestamp | 2025-01-26T18:00:00Z |
| Original Component Count Claimed | 6 |
| Original Component Count Actual | 6 |
| Gaps Identified Count | 3 |
| Components Added Count | 2 |
| Final Component Count | 8 |
| Validation Completeness Score | 100.0 |
| Enhancement Methodology | Systematic validation against repository scope, re... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

Fully Compliant. The provided specification correctly outlines the creation of two example connectors (FHIR and HL7) which aligns perfectly with the repository's defined scope.

#### 2.2.1.2 Gaps Identified

- Specification for project build configuration (.csproj files) was missing, which is critical for fulfilling the \"buildable out-of-the-box\" requirement (US-112).
- Specification for developer-facing documentation (README.md files) for each example was missing.
- The NHapi third-party library dependency for the HL7 connector was not declared in the top-level metadata.

#### 2.2.1.3 Components Added

- Project Configuration specifications for both Fhir and Hl7 examples.
- Documentation specifications for README files.

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100.0%

#### 2.2.2.2 Non Functional Requirements Coverage

100.0%

#### 2.2.2.3 Missing Requirement Components

- Validation revealed that the specification for providing \"comprehensive documentation\" (US-112) was implicit. This was enhanced by mandating XML comments on all public members.

#### 2.2.2.4 Added Requirement Components

*No items available*

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

Fully Compliant. The specification correctly details the implementation of the `IConnector` interface, adhering to the Plug-in architecture defined for this repository.

#### 2.2.3.2 Missing Pattern Components

*No items available*

#### 2.2.3.3 Added Pattern Components

*No items available*

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

Not Applicable. This repository is a class library with no direct database interaction; this validation area does not apply.

#### 2.2.4.2 Missing Database Components

*No items available*

#### 2.2.4.3 Added Database Components

*No items available*

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

Fully Compliant. The specified methods and their described logic correctly align with the expected interactions from the host application. Error handling and performance considerations are well-defined.

#### 2.2.5.2 Missing Interaction Components

- Specification lacked detail on the expected JSON structure for the complex HL7-to-JSON transformation, which has been enhanced.

#### 2.2.5.3 Added Interaction Components

*No items available*

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-07-PLUGINS-EXAMPLES |
| Technology Stack | .NET 8, C# |
| Technology Guidance Integration | .NET best practices for class library development,... |
| Framework Compliance Score | 100.0 |
| Specification Completeness | 100.0% |
| Component Count | 8 |
| Specification Methodology | Reference Implementation of the `ReportingSystem.P... |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- Plug-in Architecture
- Adapter Pattern
- Strategy Pattern (implicit via host)
- Options Pattern (for configuration DTOs)

#### 2.3.2.2 Directory Structure Source

Standard .NET Solution with separate projects for each example plug-in, promoting isolation and clarity.

#### 2.3.2.3 Naming Conventions Source

Microsoft C# coding standards.

#### 2.3.2.4 Architectural Patterns Source

Plug-in architecture where this repository provides concrete implementations of an external SDK (REPO-06-PLUGINS-SDK).

#### 2.3.2.5 Performance Optimizations Applied

- Async/await patterns specified for all I/O operations (HTTP requests, file access).
- Use of System.Text.Json for high-performance JSON processing.
- Specification for HttpClient usage for the FHIR connector.

### 2.3.3.0 File Structure

#### 2.3.3.1 Directory Organization

##### 2.3.3.1.1 Directory Path

###### 2.3.3.1.1.1 Directory Path

src/ReportingSystem.Plugins.Examples.Fhir

###### 2.3.3.1.1.2 Purpose

Contains the complete, self-contained, and buildable .NET 8 Class Library project for the FHIR R4 connector example.

###### 2.3.3.1.1.3 Contains Files

- ReportingSystem.Plugins.Examples.Fhir.csproj
- FhirConnector.cs
- Models/FhirConfig.cs
- README.md

###### 2.3.3.1.1.4 Organizational Reasoning

Isolates the FHIR example into its own project, allowing it to be compiled into a single DLL for deployment, as required by US-112 and US-042.

###### 2.3.3.1.1.5 Framework Convention Alignment

Standard .NET project structure for a plug-in.

##### 2.3.3.1.2.0 Directory Path

###### 2.3.3.1.2.1 Directory Path

src/ReportingSystem.Plugins.Examples.Hl7

###### 2.3.3.1.2.2 Purpose

Contains the complete, self-contained, and buildable .NET 8 Class Library project for the HL7 v2 connector example.

###### 2.3.3.1.2.3 Contains Files

- ReportingSystem.Plugins.Examples.Hl7.csproj
- Hl7Connector.cs
- Models/Hl7Config.cs
- README.md

###### 2.3.3.1.2.4 Organizational Reasoning

Isolates the HL7 example into its own project, allowing it to be compiled into a single DLL for deployment, fulfilling the requirements of US-112 and US-042.

###### 2.3.3.1.2.5 Framework Convention Alignment

Standard .NET project structure for a plug-in.

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | ReportingSystem.Plugins.Examples |
| Namespace Organization | Namespaces must be organized by the specific examp... |
| Naming Conventions | Follows standard Microsoft C# naming conventions (... |
| Framework Alignment | Adheres to standard .NET namespace and project org... |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

FhirConnector

##### 2.3.4.1.2.0 File Path

src/ReportingSystem.Plugins.Examples.Fhir/FhirConnector.cs

##### 2.3.4.1.3.0 Class Type

Connector Implementation

##### 2.3.4.1.4.0 Inheritance

ReportingSystem.Plugins.Sdk.IConnector

##### 2.3.4.1.5.0 Purpose

Provides a reference implementation for a custom data connector that ingests data from a FHIR R4 compliant server. This class will be discovered and loaded at runtime by the main application.

##### 2.3.4.1.6.0 Dependencies

- ReportingSystem.Plugins.Sdk
- Hl7.Fhir.R4
- System.Text.Json

##### 2.3.4.1.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.1.8.0 Technology Integration Notes

This class serves as the primary example for US-112. It must demonstrate the full implementation of the IConnector interface for a network-based data source, including configuration schema definition, connection testing, and asynchronous data fetching.

##### 2.3.4.1.9.0 Properties

*No items available*

##### 2.3.4.1.10.0 Methods

###### 2.3.4.1.10.1 Method Name

####### 2.3.4.1.10.1.1 Method Name

GetConfigurationSchema

####### 2.3.4.1.10.1.2 Method Signature

string GetConfigurationSchema()

####### 2.3.4.1.10.1.3 Return Type

string

####### 2.3.4.1.10.1.4 Access Modifier

public

####### 2.3.4.1.10.1.5 Is Async

❌ No

####### 2.3.4.1.10.1.6 Framework Specific Attributes

*No items available*

####### 2.3.4.1.10.1.7 Parameters

*No items available*

####### 2.3.4.1.10.1.8 Implementation Logic

Specification requires the construction and return of a JSON string representing a schema for its configuration. The schema must define fields for \"BaseUrl\" (string, format: uri), \"FhirQuery\" (string, e.g., \"Patient?name=smith\"), and \"BearerToken\" (string, format: password). The password format is critical for instructing the UI to mask the field. This schema will be used by the main application's UI to dynamically render a configuration form.

####### 2.3.4.1.10.1.9 Exception Handling

Specification requires this method to be a pure function that does not throw exceptions under normal circumstances.

####### 2.3.4.1.10.1.10 Performance Considerations

N/A

####### 2.3.4.1.10.1.11 Validation Requirements

The returned string must be a valid JSON Schema.

####### 2.3.4.1.10.1.12 Technology Integration Details

N/A

###### 2.3.4.1.10.2.0 Method Name

####### 2.3.4.1.10.2.1 Method Name

TestConnection

####### 2.3.4.1.10.2.2 Method Signature

Task<ValidationResult> TestConnection(string configJson)

####### 2.3.4.1.10.2.3 Return Type

Task<ReportingSystem.Plugins.Sdk.ValidationResult>

####### 2.3.4.1.10.2.4 Access Modifier

public

####### 2.3.4.1.10.2.5 Is Async

✅ Yes

####### 2.3.4.1.10.2.6 Framework Specific Attributes

*No items available*

####### 2.3.4.1.10.2.7 Parameters

- {'parameter_name': 'configJson', 'parameter_type': 'string', 'is_nullable': False, 'purpose': 'A JSON string containing the configuration values entered by the user in the UI.', 'framework_attributes': []}

####### 2.3.4.1.10.2.8 Implementation Logic

Specification requires deserializing the `configJson` into the `FhirConfig` record. It should then instantiate a `Hl7.Fhir.Client.FhirClient` using the provided `BaseUrl`. The test logic must perform a simple, non-data-intensive operation, such as fetching the server's CapabilityStatement (`fhirClient.CapabilityStatementAsync()`). If the call succeeds, it must return a `ValidationResult.Success()` from the SDK. If it fails, it must catch the specific exception (e.g., `HttpRequestException`, `FhirOperationException`) and return `ValidationResult.Failure()` with a user-friendly error message that does not expose sensitive information.

####### 2.3.4.1.10.2.9 Exception Handling

Specification requires catching exceptions related to JSON parsing, network connectivity, and FHIR server errors (e.g., authentication failure), and translating them into a structured `ValidationResult` as defined in the SDK.

####### 2.3.4.1.10.2.10 Performance Considerations

The test operation must be lightweight and have a reasonable timeout (e.g., 15 seconds, managed by the host application).

####### 2.3.4.1.10.2.11 Validation Requirements

Must validate that the endpoint is a reachable and valid FHIR server.

####### 2.3.4.1.10.2.12 Technology Integration Details

Specification requires using `Hl7.Fhir.Client.FhirClient` to interact with the external FHIR server.

###### 2.3.4.1.10.3.0 Method Name

####### 2.3.4.1.10.3.1 Method Name

FetchDataAsync

####### 2.3.4.1.10.3.2 Method Signature

Task<JsonNode> FetchDataAsync(string configJson, CancellationToken cancellationToken)

####### 2.3.4.1.10.3.3 Return Type

Task<System.Text.Json.Nodes.JsonNode>

####### 2.3.4.1.10.3.4 Access Modifier

public

####### 2.3.4.1.10.3.5 Is Async

✅ Yes

####### 2.3.4.1.10.3.6 Framework Specific Attributes

*No items available*

####### 2.3.4.1.10.3.7 Parameters

######## 2.3.4.1.10.3.7.1 Parameter Name

######### 2.3.4.1.10.3.7.1.1 Parameter Name

configJson

######### 2.3.4.1.10.3.7.1.2 Parameter Type

string

######### 2.3.4.1.10.3.7.1.3 Is Nullable

❌ No

######### 2.3.4.1.10.3.7.1.4 Purpose

A JSON string containing the validated configuration.

######### 2.3.4.1.10.3.7.1.5 Framework Attributes

*No items available*

######## 2.3.4.1.10.3.7.2.0 Parameter Name

######### 2.3.4.1.10.3.7.2.1 Parameter Name

cancellationToken

######### 2.3.4.1.10.3.7.2.2 Parameter Type

CancellationToken

######### 2.3.4.1.10.3.7.2.3 Is Nullable

❌ No

######### 2.3.4.1.10.3.7.2.4 Purpose

A token to observe for cancellation requests from the host application.

######### 2.3.4.1.10.3.7.2.5 Framework Attributes

*No items available*

####### 2.3.4.1.10.3.8.0.0 Implementation Logic

Specification requires deserializing the `configJson`. It must create and configure a `FhirClient`, including adding the `BearerToken` to the authorization header if provided. It will then execute the configured `FhirQuery` using `fhirClient.SearchAsync(query)`. The implementation must iterate through the resulting `Bundle` entries, serialize each `Resource` into a JSON string using `Hl7.Fhir.Serialization.FhirJsonSerializer`, and then parse that string into a `JsonNode`. All resulting `JsonNode` objects must be collected into a `JsonArray` which is then returned. The method must honor the `cancellationToken`. The implementation must not log sensitive information such as the Bearer Token.

####### 2.3.4.1.10.3.9.0.0 Exception Handling

Specification requires wrapping any exceptions from the FHIR client or JSON serialization in a `ConnectorFetchDataException` (defined in the SDK) that the host application can handle. The exception message should be informative but secure.

####### 2.3.4.1.10.3.10.0.0 Performance Considerations

The example will not implement paging and should include documentation (XML comments) stating this limitation. It should process the bundle entries in a memory-efficient manner.

####### 2.3.4.1.10.3.11.0.0 Validation Requirements

Assumes the `configJson` is valid as it would have been tested before saving.

####### 2.3.4.1.10.3.12.0.0 Technology Integration Details

This method is the core data ingestion logic, bridging the external FHIR data model with the application's internal `JsonNode` representation.

##### 2.3.4.1.11.0.0.0.0 Events

*No items available*

##### 2.3.4.1.12.0.0.0.0 Implementation Notes

Validation requires this class and its public members to be fully documented with XML comments to fulfill the \"comprehensive documentation\" aspect of US-112 and serve as a high-quality example for System Integrators.

#### 2.3.4.2.0.0.0.0.0 Class Name

##### 2.3.4.2.1.0.0.0.0 Class Name

Hl7Connector

##### 2.3.4.2.2.0.0.0.0 File Path

src/ReportingSystem.Plugins.Examples.Hl7/Hl7Connector.cs

##### 2.3.4.2.3.0.0.0.0 Class Type

Connector Implementation

##### 2.3.4.2.4.0.0.0.0 Inheritance

ReportingSystem.Plugins.Sdk.IConnector

##### 2.3.4.2.5.0.0.0.0 Purpose

Provides a reference implementation for a custom data connector that ingests data by parsing an HL7 v2 message file. This serves as a key example for file-based data sources.

##### 2.3.4.2.6.0.0.0.0 Dependencies

- ReportingSystem.Plugins.Sdk
- NHapi.Base
- NHapi.Model.V251
- System.Text.Json

##### 2.3.4.2.7.0.0.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.2.8.0.0.0.0 Technology Integration Notes

Fulfills the HL7 example requirement from US-112. Demonstrates file I/O, use of a parsing library (NHapi), and transformation to the system's standard JSON format.

##### 2.3.4.2.9.0.0.0.0 Properties

*No items available*

##### 2.3.4.2.10.0.0.0.0 Methods

###### 2.3.4.2.10.1.0.0.0 Method Name

####### 2.3.4.2.10.1.1.0.0 Method Name

GetConfigurationSchema

####### 2.3.4.2.10.1.2.0.0 Method Signature

string GetConfigurationSchema()

####### 2.3.4.2.10.1.3.0.0 Return Type

string

####### 2.3.4.2.10.1.4.0.0 Access Modifier

public

####### 2.3.4.2.10.1.5.0.0 Is Async

❌ No

####### 2.3.4.2.10.1.6.0.0 Framework Specific Attributes

*No items available*

####### 2.3.4.2.10.1.7.0.0 Parameters

*No items available*

####### 2.3.4.2.10.1.8.0.0 Implementation Logic

Specification requires the construction and return of a JSON string representing a schema for its configuration. The schema must define fields for \"FilePath\" (string) and \"Encoding\" (string, with a default value of \"UTF-8\"). This schema allows the UI to render the necessary configuration form.

####### 2.3.4.2.10.1.9.0.0 Exception Handling

N/A

####### 2.3.4.2.10.1.10.0.0 Performance Considerations

N/A

####### 2.3.4.2.10.1.11.0.0 Validation Requirements

The returned string must be a valid JSON Schema.

####### 2.3.4.2.10.1.12.0.0 Technology Integration Details

N/A

###### 2.3.4.2.10.2.0.0.0 Method Name

####### 2.3.4.2.10.2.1.0.0 Method Name

TestConnection

####### 2.3.4.2.10.2.2.0.0 Method Signature

Task<ValidationResult> TestConnection(string configJson)

####### 2.3.4.2.10.2.3.0.0 Return Type

Task<ReportingSystem.Plugins.Sdk.ValidationResult>

####### 2.3.4.2.10.2.4.0.0 Access Modifier

public

####### 2.3.4.2.10.2.5.0.0 Is Async

✅ Yes

####### 2.3.4.2.10.2.6.0.0 Framework Specific Attributes

*No items available*

####### 2.3.4.2.10.2.7.0.0 Parameters

- {'parameter_name': 'configJson', 'parameter_type': 'string', 'is_nullable': False, 'purpose': 'A JSON string containing the configuration values from the UI.', 'framework_attributes': []}

####### 2.3.4.2.10.2.8.0.0 Implementation Logic

Specification requires deserializing `configJson` into the `Hl7Config` record. The logic must first check if the file at `FilePath` exists and is readable. If not, it returns a failure. If the file exists, it should attempt to read and parse a small portion of the file (e.g., the first 1-2 messages) using the NHapi parser to validate that the file appears to be a valid HL7 message file. On success, it must return `ValidationResult.Success()` from the SDK. On failure (file not found, access denied, parsing error), it must return `ValidationResult.Failure()` with a clear error message.

####### 2.3.4.2.10.2.9.0.0 Exception Handling

Specification requires catching and handling `FileNotFoundException`, `UnauthorizedAccessException`, `IOException`, and any parsing exceptions from NHapi, translating them into a `ValidationResult` as defined in the SDK.

####### 2.3.4.2.10.2.10.0.0 Performance Considerations

Crucially, this method must NOT read the entire file. It should only read a small sample to validate the format quickly.

####### 2.3.4.2.10.2.11.0.0 Validation Requirements

Must validate file existence, read permissions, and basic file format compatibility.

####### 2.3.4.2.10.2.12.0.0 Technology Integration Details

Uses `System.IO` for file access and `NHapi` for parsing.

###### 2.3.4.2.10.3.0.0.0 Method Name

####### 2.3.4.2.10.3.1.0.0 Method Name

FetchDataAsync

####### 2.3.4.2.10.3.2.0.0 Method Signature

Task<JsonNode> FetchDataAsync(string configJson, CancellationToken cancellationToken)

####### 2.3.4.2.10.3.3.0.0 Return Type

Task<System.Text.Json.Nodes.JsonNode>

####### 2.3.4.2.10.3.4.0.0 Access Modifier

public

####### 2.3.4.2.10.3.5.0.0 Is Async

✅ Yes

####### 2.3.4.2.10.3.6.0.0 Framework Specific Attributes

*No items available*

####### 2.3.4.2.10.3.7.0.0 Parameters

######## 2.3.4.2.10.3.7.1.0 Parameter Name

######### 2.3.4.2.10.3.7.1.1 Parameter Name

configJson

######### 2.3.4.2.10.3.7.1.2 Parameter Type

string

######### 2.3.4.2.10.3.7.1.3 Is Nullable

❌ No

######### 2.3.4.2.10.3.7.1.4 Purpose

A JSON string containing the validated configuration.

######### 2.3.4.2.10.3.7.1.5 Framework Attributes

*No items available*

######## 2.3.4.2.10.3.7.2.0 Parameter Name

######### 2.3.4.2.10.3.7.2.1 Parameter Name

cancellationToken

######### 2.3.4.2.10.3.7.2.2 Parameter Type

CancellationToken

######### 2.3.4.2.10.3.7.2.3 Is Nullable

❌ No

######### 2.3.4.2.10.3.7.2.4 Purpose

A token to observe for cancellation requests.

######### 2.3.4.2.10.3.7.2.5 Framework Attributes

*No items available*

####### 2.3.4.2.10.3.8.0.0 Implementation Logic

Specification requires deserializing `configJson`. It should open a stream to the file specified by `FilePath`. It will then use the NHapi library to parse all messages from the stream. For each parsed HL7 message, a helper method must be specified to recursively traverse the message structure (segments, fields, components) and build a corresponding `JsonObject`. The structure should map segment names (e.g., \"PID\") to objects, and field numbers to values. All these `JsonObject` instances should be added to a root `JsonArray`. This final `JsonArray` is the return value. The method must honor the `cancellationToken` during the file read operation.

####### 2.3.4.2.10.3.9.0.0 Exception Handling

Specification requires wrapping any file I/O or parsing exceptions in a `ConnectorFetchDataException` (defined in the SDK) with a descriptive message.

####### 2.3.4.2.10.3.10.0.0 Performance Considerations

Specification requires using a streaming approach to read the file to handle large files without high memory consumption.

####### 2.3.4.2.10.3.11.0.0 Validation Requirements

Assumes the configuration is valid.

####### 2.3.4.2.10.3.12.0.0 Technology Integration Details

Demonstrates a complex transformation from a hierarchical, delimited text format (HL7) to a structured JSON object (`JsonNode`).

##### 2.3.4.2.11.0.0.0.0 Events

*No items available*

##### 2.3.4.2.12.0.0.0.0 Implementation Notes

Validation requires this class and its public members to be fully documented with XML comments. The HL7-to-JSON transformation logic is complex and must be well-commented to serve as a clear example.

### 2.3.5.0.0.0.0.0.0 Interface Specifications

*No items available*

### 2.3.6.0.0.0.0.0.0 Enum Specifications

*No items available*

### 2.3.7.0.0.0.0.0.0 Dto Specifications

#### 2.3.7.1.0.0.0.0.0 Dto Name

##### 2.3.7.1.1.0.0.0.0 Dto Name

FhirConfig

##### 2.3.7.1.2.0.0.0.0 File Path

src/ReportingSystem.Plugins.Examples.Fhir/Models/FhirConfig.cs

##### 2.3.7.1.3.0.0.0.0 Purpose

Provides a strongly-typed, immutable representation of the JSON configuration for the FhirConnector. This avoids manual JSON parsing and improves code clarity and safety.

##### 2.3.7.1.4.0.0.0.0 Framework Base Class

record

##### 2.3.7.1.5.0.0.0.0 Properties

###### 2.3.7.1.5.1.0.0.0 Property Name

####### 2.3.7.1.5.1.1.0.0 Property Name

BaseUrl

####### 2.3.7.1.5.1.2.0.0 Property Type

string

####### 2.3.7.1.5.1.3.0.0 Validation Attributes

- [Required]

####### 2.3.7.1.5.1.4.0.0 Serialization Attributes

- [JsonPropertyName(\"baseUrl\")]

####### 2.3.7.1.5.1.5.0.0 Framework Specific Attributes

*No items available*

###### 2.3.7.1.5.2.0.0.0 Property Name

####### 2.3.7.1.5.2.1.0.0 Property Name

FhirQuery

####### 2.3.7.1.5.2.2.0.0 Property Type

string

####### 2.3.7.1.5.2.3.0.0 Validation Attributes

- [Required]

####### 2.3.7.1.5.2.4.0.0 Serialization Attributes

- [JsonPropertyName(\"fhirQuery\")]

####### 2.3.7.1.5.2.5.0.0 Framework Specific Attributes

*No items available*

###### 2.3.7.1.5.3.0.0.0 Property Name

####### 2.3.7.1.5.3.1.0.0 Property Name

BearerToken

####### 2.3.7.1.5.3.2.0.0 Property Type

string?

####### 2.3.7.1.5.3.3.0.0 Validation Attributes

*No items available*

####### 2.3.7.1.5.3.4.0.0 Serialization Attributes

- [JsonPropertyName(\"bearerToken\")]

####### 2.3.7.1.5.3.5.0.0 Framework Specific Attributes

*No items available*

##### 2.3.7.1.6.0.0.0.0 Validation Rules

Properties are validated by the `TestConnection` method's logic.

##### 2.3.7.1.7.0.0.0.0 Serialization Requirements

Designed to be deserialized from the `configJson` string parameter using System.Text.Json.

#### 2.3.7.2.0.0.0.0.0 Dto Name

##### 2.3.7.2.1.0.0.0.0 Dto Name

Hl7Config

##### 2.3.7.2.2.0.0.0.0 File Path

src/ReportingSystem.Plugins.Examples.Hl7/Models/Hl7Config.cs

##### 2.3.7.2.3.0.0.0.0 Purpose

Provides a strongly-typed, immutable representation of the JSON configuration for the Hl7Connector.

##### 2.3.7.2.4.0.0.0.0 Framework Base Class

record

##### 2.3.7.2.5.0.0.0.0 Properties

###### 2.3.7.2.5.1.0.0.0 Property Name

####### 2.3.7.2.5.1.1.0.0 Property Name

FilePath

####### 2.3.7.2.5.1.2.0.0 Property Type

string

####### 2.3.7.2.5.1.3.0.0 Validation Attributes

- [Required]

####### 2.3.7.2.5.1.4.0.0 Serialization Attributes

- [JsonPropertyName(\"filePath\")]

####### 2.3.7.2.5.1.5.0.0 Framework Specific Attributes

*No items available*

###### 2.3.7.2.5.2.0.0.0 Property Name

####### 2.3.7.2.5.2.1.0.0 Property Name

Encoding

####### 2.3.7.2.5.2.2.0.0 Property Type

string

####### 2.3.7.2.5.2.3.0.0 Validation Attributes

- [Required]

####### 2.3.7.2.5.2.4.0.0 Serialization Attributes

- [JsonPropertyName(\"encoding\")]

####### 2.3.7.2.5.2.5.0.0 Framework Specific Attributes

*No items available*

##### 2.3.7.2.6.0.0.0.0 Validation Rules

Properties are validated by the `TestConnection` method's logic.

##### 2.3.7.2.7.0.0.0.0 Serialization Requirements

Designed to be deserialized from the `configJson` string parameter using System.Text.Json.

### 2.3.8.0.0.0.0.0.0 Configuration Specifications

#### 2.3.8.1.0.0.0.0.0 Configuration Name

##### 2.3.8.1.1.0.0.0.0 Configuration Name

ReportingSystem.Plugins.Examples.Fhir.csproj

##### 2.3.8.1.2.0.0.0.0 File Path

src/ReportingSystem.Plugins.Examples.Fhir/ReportingSystem.Plugins.Examples.Fhir.csproj

##### 2.3.8.1.3.0.0.0.0 Purpose

Defines the project properties and dependencies for the FHIR connector example, ensuring it is buildable as a .NET 8 class library.

##### 2.3.8.1.4.0.0.0.0 Framework Base Class

XML

##### 2.3.8.1.5.0.0.0.0 Configuration Sections

###### 2.3.8.1.5.1.0.0.0 Section Name

####### 2.3.8.1.5.1.1.0.0 Section Name

PropertyGroup

####### 2.3.8.1.5.1.2.0.0 Properties

######## 2.3.8.1.5.1.2.1.0 Property Name

######### 2.3.8.1.5.1.2.1.1 Property Name

TargetFramework

######### 2.3.8.1.5.1.2.1.2 Property Type

string

######### 2.3.8.1.5.1.2.1.3 Default Value

net8.0

######### 2.3.8.1.5.1.2.1.4 Required

✅ Yes

######### 2.3.8.1.5.1.2.1.5 Description

Specifies the target .NET framework version.

######## 2.3.8.1.5.1.2.2.0 Property Name

######### 2.3.8.1.5.1.2.2.1 Property Name

GenerateDocumentationFile

######### 2.3.8.1.5.1.2.2.2 Property Type

bool

######### 2.3.8.1.5.1.2.2.3 Default Value

true

######### 2.3.8.1.5.1.2.2.4 Required

✅ Yes

######### 2.3.8.1.5.1.2.2.5 Description

Ensures XML documentation is generated, fulfilling part of the documentation requirement from US-112.

###### 2.3.8.1.5.2.0.0.0 Section Name

####### 2.3.8.1.5.2.1.0.0 Section Name

ItemGroup (Dependencies)

####### 2.3.8.1.5.2.2.0.0 Properties

######## 2.3.8.1.5.2.2.1.0 Property Name

######### 2.3.8.1.5.2.2.1.1 Property Name

ProjectReference to SDK

######### 2.3.8.1.5.2.2.1.2 Property Type

XML Element

######### 2.3.8.1.5.2.2.1.3 Default Value

<ProjectReference Include=\"..\\..\\..\\..\\REPO-06-PLUGINS-SDK\\src\\ReportingSystem.Plugins.Sdk\\ReportingSystem.Plugins.Sdk.csproj\" />

######### 2.3.8.1.5.2.2.1.4 Required

✅ Yes

######### 2.3.8.1.5.2.2.1.5 Description

Specifies the project reference to the core SDK, providing the IConnector interface.

######## 2.3.8.1.5.2.2.2.0 Property Name

######### 2.3.8.1.5.2.2.2.1 Property Name

PackageReference to FHIR library

######### 2.3.8.1.5.2.2.2.2 Property Type

XML Element

######### 2.3.8.1.5.2.2.2.3 Default Value

<PackageReference Include=\"Hl7.Fhir.R4\" Version=\"5.6.0\" />

######### 2.3.8.1.5.2.2.2.4 Required

✅ Yes

######### 2.3.8.1.5.2.2.2.5 Description

Specifies the dependency on the third-party FHIR client library.

##### 2.3.8.1.6.0.0.0.0 Validation Requirements

The project must build successfully using `dotnet build`.

#### 2.3.8.2.0.0.0.0.0 Configuration Name

##### 2.3.8.2.1.0.0.0.0 Configuration Name

ReportingSystem.Plugins.Examples.Hl7.csproj

##### 2.3.8.2.2.0.0.0.0 File Path

src/ReportingSystem.Plugins.Examples.Hl7/ReportingSystem.Plugins.Examples.Hl7.csproj

##### 2.3.8.2.3.0.0.0.0 Purpose

Defines the project properties and dependencies for the HL7 connector example, ensuring it is buildable as a .NET 8 class library.

##### 2.3.8.2.4.0.0.0.0 Framework Base Class

XML

##### 2.3.8.2.5.0.0.0.0 Configuration Sections

###### 2.3.8.2.5.1.0.0.0 Section Name

####### 2.3.8.2.5.1.1.0.0 Section Name

PropertyGroup

####### 2.3.8.2.5.1.2.0.0 Properties

######## 2.3.8.2.5.1.2.1.0 Property Name

######### 2.3.8.2.5.1.2.1.1 Property Name

TargetFramework

######### 2.3.8.2.5.1.2.1.2 Property Type

string

######### 2.3.8.2.5.1.2.1.3 Default Value

net8.0

######### 2.3.8.2.5.1.2.1.4 Required

✅ Yes

######### 2.3.8.2.5.1.2.1.5 Description

Specifies the target .NET framework version.

######## 2.3.8.2.5.1.2.2.0 Property Name

######### 2.3.8.2.5.1.2.2.1 Property Name

GenerateDocumentationFile

######### 2.3.8.2.5.1.2.2.2 Property Type

bool

######### 2.3.8.2.5.1.2.2.3 Default Value

true

######### 2.3.8.2.5.1.2.2.4 Required

✅ Yes

######### 2.3.8.2.5.1.2.2.5 Description

Ensures XML documentation is generated.

###### 2.3.8.2.5.2.0.0.0 Section Name

####### 2.3.8.2.5.2.1.0.0 Section Name

ItemGroup (Dependencies)

####### 2.3.8.2.5.2.2.0.0 Properties

######## 2.3.8.2.5.2.2.1.0 Property Name

######### 2.3.8.2.5.2.2.1.1 Property Name

ProjectReference to SDK

######### 2.3.8.2.5.2.2.1.2 Property Type

XML Element

######### 2.3.8.2.5.2.2.1.3 Default Value

<ProjectReference Include=\"..\\..\\..\\..\\REPO-06-PLUGINS-SDK\\src\\ReportingSystem.Plugins.Sdk\\ReportingSystem.Plugins.Sdk.csproj\" />

######### 2.3.8.2.5.2.2.1.4 Required

✅ Yes

######### 2.3.8.2.5.2.2.1.5 Description

Specifies the project reference to the core SDK, providing the IConnector interface.

######## 2.3.8.2.5.2.2.2.0 Property Name

######### 2.3.8.2.5.2.2.2.1 Property Name

PackageReference to NHapi

######### 2.3.8.2.5.2.2.2.2 Property Type

XML Element

######### 2.3.8.2.5.2.2.2.3 Default Value

<PackageReference Include=\"NHapi.Model.V251\" Version=\"2.5.0.6\" />

######### 2.3.8.2.5.2.2.2.4 Required

✅ Yes

######### 2.3.8.2.5.2.2.2.5 Description

Specifies the dependency on the third-party NHapi library for HL7 v2 parsing.

##### 2.3.8.2.6.0.0.0.0 Validation Requirements

The project must build successfully using `dotnet build`.

### 2.3.9.0.0.0.0.0.0 Documentation Specifications

#### 2.3.9.1.0.0.0.0.0 Document Name

##### 2.3.9.1.1.0.0.0.0 Document Name

README.md (FHIR)

##### 2.3.9.1.2.0.0.0.0 File Path

src/ReportingSystem.Plugins.Examples.Fhir/README.md

##### 2.3.9.1.3.0.0.0.0 Purpose

Provides essential documentation for a System Integrator on how to understand, build, and use the FHIR connector example.

##### 2.3.9.1.4.0.0.0.0 Content Requirements

- A clear title: \"FHIR R4 Connector Example\".
- A brief description of the connector's purpose.
- A \"Prerequisites\" section listing required tools (.NET 8 SDK).
- A \"How to Build\" section with the `dotnet build` command.
- A \"Configuration\" section explaining the fields defined in `GetConfigurationSchema` (\"BaseUrl\", \"FhirQuery\", \"BearerToken\") with examples.
- A \"How to Deploy\" section explaining how to copy the resulting DLL to the main application's `plugins` folder.

#### 2.3.9.2.0.0.0.0.0 Document Name

##### 2.3.9.2.1.0.0.0.0 Document Name

README.md (HL7)

##### 2.3.9.2.2.0.0.0.0 File Path

src/ReportingSystem.Plugins.Examples.Hl7/README.md

##### 2.3.9.2.3.0.0.0.0 Purpose

Provides essential documentation for a System Integrator on how to understand, build, and use the HL7 connector example.

##### 2.3.9.2.4.0.0.0.0 Content Requirements

- A clear title: \"HL7 v2 Connector Example\".
- A brief description of the connector's purpose.
- A \"Prerequisites\" section listing required tools (.NET 8 SDK).
- A \"How to Build\" section with the `dotnet build` command.
- A \"Configuration\" section explaining the fields defined in `GetConfigurationSchema` (\"FilePath\", \"Encoding\").
- A \"How to Deploy\" section explaining how to copy the resulting DLL to the main application's `plugins` folder.

### 2.3.10.0.0.0.0.0.0 Dependency Injection Specifications

*No items available*

### 2.3.11.0.0.0.0.0.0 External Integration Specifications

#### 2.3.11.1.0.0.0.0.0 Integration Target

##### 2.3.11.1.1.0.0.0.0 Integration Target

FHIR R4 Server

##### 2.3.11.1.2.0.0.0.0 Integration Type

HTTP REST API

##### 2.3.11.1.3.0.0.0.0 Required Client Classes

- Hl7.Fhir.Client.FhirClient

##### 2.3.11.1.4.0.0.0.0 Configuration Requirements

Requires a Base URL, a FHIR resource query, and an optional Bearer Token for authentication.

##### 2.3.11.1.5.0.0.0.0 Error Handling Requirements

Specification requires handling `HttpRequestException` for network errors and `FhirOperationException` for server-side FHIR errors (e.g., 401, 403, 404) and wrapping them in an SDK-defined `ConnectorFetchDataException`.

##### 2.3.11.1.6.0.0.0.0 Authentication Requirements

Example must demonstrate how to add a Bearer token to the `FhirClient`'s `OnBeforeRequest` event handler.

##### 2.3.11.1.7.0.0.0.0 Framework Integration Patterns

Uses the `Hl7.Fhir.R4` SDK as the client library to abstract direct HTTP calls.

#### 2.3.11.2.0.0.0.0.0 Integration Target

##### 2.3.11.2.1.0.0.0.0 Integration Target

File System (for HL7)

##### 2.3.11.2.2.0.0.0.0 Integration Type

File I/O

##### 2.3.11.2.3.0.0.0.0 Required Client Classes

- System.IO.FileStream
- System.IO.StreamReader

##### 2.3.11.2.4.0.0.0.0 Configuration Requirements

Requires a local or UNC file path.

##### 2.3.11.2.5.0.0.0.0 Error Handling Requirements

Specification requires handling `FileNotFoundException`, `UnauthorizedAccessException`, and `IOException` during file access, wrapping them in an SDK-defined `ConnectorFetchDataException`.

##### 2.3.11.2.6.0.0.0.0 Authentication Requirements

Relies on the permissions of the user account running the main application's Windows Service.

##### 2.3.11.2.7.0.0.0.0 Framework Integration Patterns

Standard .NET asynchronous file I/O patterns (`async`/`await`).

## 2.4.0.0.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 2 |
| Total Interfaces | 0 |
| Total Enums | 0 |
| Total Dtos | 2 |
| Total Configurations | 2 |
| Total External Integrations | 2 |
| Grand Total Components | 8 |
| Phase 2 Claimed Count | 6 |
| Phase 2 Actual Count | 6 |
| Validation Added Count | 2 |
| Final Validated Count | 8 |

# 3.0.0.0.0.0.0.0.0 File Structure

## 3.1.0.0.0.0.0.0.0 Directory Organization

### 3.1.1.0.0.0.0.0.0 Directory Path

#### 3.1.1.1.0.0.0.0.0 Directory Path

/

#### 3.1.1.2.0.0.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.1.3.0.0.0.0.0 Contains Files

- ReportingSystem.Plugins.Examples.sln
- .editorconfig
- Directory.Build.props
- .gitignore

#### 3.1.1.4.0.0.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.1.5.0.0.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.2.0.0.0.0.0.0 Directory Path

#### 3.1.2.1.0.0.0.0.0 Directory Path

src/ReportingSystem.Plugins.Examples.Fhir

#### 3.1.2.2.0.0.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.2.3.0.0.0.0.0 Contains Files

- ReportingSystem.Plugins.Examples.Fhir.csproj
- README.md

#### 3.1.2.4.0.0.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.2.5.0.0.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.3.0.0.0.0.0.0 Directory Path

#### 3.1.3.1.0.0.0.0.0 Directory Path

src/ReportingSystem.Plugins.Examples.Hl7

#### 3.1.3.2.0.0.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.3.3.0.0.0.0.0 Contains Files

- ReportingSystem.Plugins.Examples.Hl7.csproj
- README.md

#### 3.1.3.4.0.0.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.3.5.0.0.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

