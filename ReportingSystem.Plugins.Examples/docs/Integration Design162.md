# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-07-PLUGINS-EXAMPLES |
| Extraction Timestamp | 2024-05-24T10:00:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | Fully Ready |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

US-112

#### 1.2.1.2 Requirement Text

As a System Integrator, I want to access a Plug-in Development Kit (PDK) that contains comprehensive documentation and complete, working example projects for FHIR and HL7, so that I can quickly learn the plug-in architecture and have a solid foundation for developing my own custom data connectors.

#### 1.2.1.3 Validation Criteria

- The FHIR example project is buildable out-of-the-box.
- The HL7 example project is buildable out-of-the-box.
- Example projects can be loaded and recognized by the system.

#### 1.2.1.4 Implementation Implications

- This repository must produce two distinct .NET Class Library projects: one for FHIR and one for HL7.
- Each project must be a complete, self-contained example that can be compiled to a DLL.
- The projects must demonstrate best practices for implementing the IConnector interface from the SDK.

#### 1.2.1.5 Extraction Reasoning

This is the primary requirement that this repository fulfills. It explicitly mandates the creation of the FHIR and HL7 example projects that form the core of this repository's scope.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

US-113

#### 1.2.2.2 Requirement Text

As a System Integrator, I want to develop a custom data connector as a .NET DLL by implementing a clearly defined IConnector interface, so that I can extend the system to ingest data from proprietary or unsupported data sources.

#### 1.2.2.3 Validation Criteria

- A .NET class library project correctly implements the IConnector interface from the provided contract assembly.

#### 1.2.2.4 Implementation Implications

- The projects in this repository serve as the reference implementation for this user story.
- The code must be well-documented to guide System Integrators in their own development efforts.

#### 1.2.2.5 Extraction Reasoning

This repository provides the official, working examples of how to fulfill US-113, making it directly relevant as a practical guide and template.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

US-042

#### 1.2.3.2 Requirement Text

As an IT Support user, I want to deploy a new custom data connector by simply copying its compiled DLL file and any dependencies into a designated 'plugins' folder on the application server.

#### 1.2.3.3 Validation Criteria

- The system automatically detects the new file, loads the assembly, registers the new connector, and makes it available for selection in the Control Panel.

#### 1.2.3.4 Implementation Implications

- The build output of the projects in this repository (the DLLs) are the artifacts that will be used to test the functionality of US-042.

#### 1.2.3.5 Extraction Reasoning

The compiled DLLs from this repository are the primary subject of the deployment process described in this requirement, making it a critical consumer of this repository's output.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

FHIR Connector Example

#### 1.3.1.2 Component Specification

A complete, working implementation of the IConnector interface that demonstrates how to connect to a FHIR R4 server, fetch data based on a configured resource query, and return it in the system's standard JSON format.

#### 1.3.1.3 Implementation Requirements

- Must implement the IConnector interface from REPO-06-PLUGINS-SDK.
- Must use the Hl7.Fhir.R4 third-party library for communication with a FHIR server.
- Must provide a JSON schema via GetConfigurationSchema() that defines its configuration fields (e.g., 'BaseUrl', 'AuthenticationMethod').
- Must implement TestConnectionAsync to validate connectivity and authentication to a FHIR server.
- Must implement FetchDataAsync to execute a query and return data as a System.Text.Json.JsonNode.

#### 1.3.1.4 Architectural Context

An example plug-in residing in the 'Examples Layer', loaded at runtime by the main application's plug-in discovery mechanism.

#### 1.3.1.5 Extraction Reasoning

This component is explicitly required by US-112 and serves as the primary reference implementation for creating FHIR-based custom connectors.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

HL7 Connector Example

#### 1.3.2.2 Component Specification

A complete, working implementation of the IConnector interface that demonstrates how to parse HL7 v2 messages (e.g., from a file source) and transform them into the system's standard JSON format.

#### 1.3.2.3 Implementation Requirements

- Must implement the IConnector interface from REPO-06-PLUGINS-SDK.
- Must use a suitable third-party library for parsing HL7 v2 messages (e.g., NHapi).
- Must provide a JSON schema via GetConfigurationSchema() that defines its configuration fields (e.g., 'FilePath', 'Encoding').
- Must implement TestConnectionAsync to validate the ability to read and parse a sample of the source file.
- Must implement FetchDataAsync to read the entire source file, parse the HL7 messages, and return the data as a System.Text.Json.JsonNode.

#### 1.3.2.4 Architectural Context

An example plug-in residing in the 'Examples Layer', loaded at runtime by the main application's plug-in discovery mechanism.

#### 1.3.2.5 Extraction Reasoning

This component is explicitly required by US-112 and serves as the primary reference implementation for creating HL7-based custom connectors.

## 1.4.0.0 Architectural Layers

- {'layer_name': 'Examples Layer', 'layer_responsibilities': ['Provides concrete, runnable, and well-documented examples of plug-ins.', 'Serves as a reference implementation for third-party developers (System Integrators).', "This layer is not part of the core product but is a key part of the product's developer ecosystem."], 'layer_constraints': ['Must only contain example code.', 'Must not contain any core application logic.', 'Must only depend on the official SDK (REPO-06-PLUGINS-SDK).'], 'implementation_patterns': ['Plug-in Architecture'], 'extraction_reasoning': "This repository is explicitly defined as a provider of example plug-ins. It doesn't fit into the core application's Clean Architecture layers but serves as an external component that integrates with the system, justifying its own conceptual layer."}

## 1.5.0.0 Dependency Interfaces

- {'interface_name': 'IConnector', 'source_repository': 'REPO-06-PLUGINS-SDK', 'method_contracts': [{'method_name': 'GetName', 'method_signature': 'string GetName()', 'method_purpose': 'Returns the display name of the connector for use in the Control Panel UI.', 'integration_context': "Called by the plugin loader in REPO-08-SERVICE-HOST during startup to register the connector's name for UI selection."}, {'method_name': 'GetConfigurationSchema', 'method_signature': 'string GetConfigurationSchema()', 'method_purpose': 'Returns a JSON schema string that defines the configuration fields required by the connector, which the UI will use to dynamically render a configuration form.', 'integration_context': 'Called by the core application (REPO-08-SERVICE-HOST) when an Administrator selects this connector type in the Control Panel UI (REPO-09-WEB-UI), as per US-037.'}, {'method_name': 'TestConnectionAsync', 'method_signature': 'Task<ConnectionTestResult> TestConnectionAsync(System.Text.Json.Nodes.JsonNode configuration, CancellationToken cancellationToken)', 'method_purpose': 'Performs a live test of the connection using the provided configuration to validate settings like hostnames, credentials, and permissions before the connector is saved.', 'integration_context': "Called by the core application (REPO-08-SERVICE-HOST) when an Administrator clicks the 'Test Connection' button in the connector configuration UI, as per US-038."}, {'method_name': 'FetchDataAsync', 'method_signature': 'Task<System.Text.Json.Nodes.JsonNode> FetchDataAsync(System.Text.Json.Nodes.JsonNode configuration, CancellationToken cancellationToken)', 'method_purpose': 'The primary data ingestion method. Connects to the data source using the provided configuration, retrieves the data, and returns it as a standardized JsonNode object.', 'integration_context': 'Called by the core reporting engine in REPO-08-SERVICE-HOST at the beginning of a report job execution, as per REQ-DATA-DTR-001 and US-113.'}], 'integration_pattern': 'Interface Implementation (Plug-in)', 'communication_protocol': 'In-Process Method Call', 'extraction_reasoning': "This repository's entire purpose is to provide concrete implementations of the IConnector interface defined in its sole dependency, the SDK (REPO-06-PLUGINS-SDK). This contract is the central architectural element for this repository."}

## 1.6.0.0 Exposed Interfaces

*No items available*

## 1.7.0.0 Technology Context

### 1.7.1.0 Framework Requirements

The repository must contain .NET 8 Class Library projects.

### 1.7.2.0 Integration Technologies

- Hl7.Fhir.R4 (for the FHIR connector)
- NHapi (for the HL7 connector)

### 1.7.3.0 Performance Constraints

Example code should demonstrate efficient data fetching and parsing techniques appropriate for a plug-in, such as asynchronous I/O and stream processing where applicable.

### 1.7.4.0 Security Requirements

The examples must demonstrate best practices for handling credentials passed in the configuration JSON, such as not logging them in plaintext and retrieving them from the provided configuration object rather than hardcoding.

## 1.8.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | The repository is fully mapped to its primary requ... |
| Cross Reference Validation | The repository's role as an implementer of the ICo... |
| Implementation Readiness Assessment | The repository is fully ready for implementation. ... |
| Quality Assurance Confirmation | The analysis systematically verified the repositor... |

