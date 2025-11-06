# 1 Id

REPO-06-PLUGINS-SDK

# 2 Name

ReportingSystem.Plugins.Sdk

# 3 Description

This repository contains the Plugin Development Kit (PDK), which is the public-facing contract for third-party developers (System Integrators) to create custom data connectors. Its primary artifact is the `IConnector` interface, along with any required data models, helper classes, or documentation needed to build a valid plugin. This repository is completely decoupled from the main application's implementation details. It is intended to be versioned and distributed independently, typically as a NuGet package. This allows the plugin ecosystem to evolve separately from the core product, providing a stable and well-defined extension point.

# 4 Type

🔹 Cross-Cutting Library

# 5 Namespace

ReportingSystem.Plugins.Sdk

# 6 Output Path

src/sdk/ReportingSystem.Plugins.Sdk

# 7 Framework

.NET 8

# 8 Language

C#

# 9 Technology

.NET Class Library

# 10 Thirdparty Libraries

*No items available*

# 11 Layer Ids

- sdk-layer

# 12 Dependencies

- REPO-03-SHARED-CONTRACTS

# 13 Requirements

*No items available*

# 14 Generate Tests

❌ No

# 15 Generate Documentation

✅ Yes

# 16 Architecture Style

Plug-in Architecture

# 17 Architecture Map

- extension-points

# 18 Components Map

*No items available*

# 19 Requirements Map

- 1.1.1 Extensibility
- 4.3. Custom Connector Architecture

# 20 Decomposition Rationale

## 20.1 Operation Type

NEW_DECOMPOSED

## 20.2 Source Repository

REPO-01-MONO

## 20.3 Decomposition Reasoning

Extracted to create a formal, distributable Software Development Kit (SDK) for plugin developers. This isolates the public API for extensions from the internal implementation of the application, providing a stable, versionable contract that enables an ecosystem of third-party connectors without exposing the core source code.

## 20.4 Extracted Responsibilities

- The `IConnector` interface definition.
- Shared data models required for plugin development.
- Documentation and usage guidelines for creating connectors.

## 20.5 Reusability Scope

- This SDK is the single dependency for any custom connector project.
- It is distributed via a package manager (NuGet) for easy consumption.

## 20.6 Development Benefits

- Allows third parties to extend the system's capabilities safely and independently.
- Enables independent versioning and release cycles for the SDK and the main application.

# 21.0 Dependency Contracts

*No data available*

# 22.0 Exposed Contracts

## 22.1 Public Interfaces

- {'interface': 'IConnector', 'methods': ['string GetName()', 'Task<JsonNode> FetchDataAsync(ConnectorConfiguration config, CancellationToken cancellationToken)'], 'events': [], 'properties': [], 'consumers': ['REPO-07-PLUGINS-EXAMPLES', 'REPO-08-SERVICE-HOST']}

# 23.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | N/A - Plugins are loaded dynamically, not via DI. |
| Event Communication | N/A |
| Data Flow | Defines the contract for how data is fetched by a ... |
| Error Handling | Plugins are expected to handle their own internal ... |
| Async Patterns | All data fetching operations are defined as asynch... |

# 24.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Must maintain compatibility with the target .NET v... |
| Performance Considerations | SDK design should encourage performant data fetchi... |
| Security Considerations | The contract should not require plugins to handle ... |
| Testing Approach | N/A |

# 25.0 Scope Boundaries

## 25.1 Must Implement

- All public interfaces, base classes, and models for plugin development.

## 25.2 Must Not Implement

- Any concrete implementation of a connector.
- Any logic specific to the main application's runtime.

## 25.3 Extension Points

- The SDK can be versioned to add new capabilities for plugins.

## 25.4 Validation Rules

*No items available*

