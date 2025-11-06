# 1 Id

REPO-07-PLUGINS-EXAMPLES

# 2 Name

ReportingSystem.Plugins.Examples

# 3 Description

This repository provides fully functional, well-documented example implementations of custom data connectors using the SDK from the `ReportingSystem.Plugins.Sdk` repository. It includes the sample FHIR and HL7 connectors mentioned in the requirements. This serves as a practical guide and starting point for System Integrators. By keeping the examples in a separate repository, they can be updated, and new examples can be added, without affecting the core product or the SDK itself. It is a crucial part of the developer experience for the plugin ecosystem.

# 4 Type

🔹 Application Services

# 5 Namespace

ReportingSystem.Plugins.Examples

# 6 Output Path

src/examples/ReportingSystem.Plugins.Examples

# 7 Framework

.NET 8

# 8 Language

C#

# 9 Technology

.NET Class Library

# 10 Thirdparty Libraries

- Hl7.Fhir.R4

# 11 Layer Ids

- examples-layer

# 12 Dependencies

- REPO-06-PLUGINS-SDK

# 13 Requirements

*No items available*

# 14 Generate Tests

✅ Yes

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

# 20 Decomposition Rationale

## 20.1 Operation Type

NEW_DECOMPOSED

## 20.2 Source Repository

REPO-01-MONO

## 20.3 Decomposition Reasoning

Separated to provide a clear, isolated reference implementation for plugin developers. This prevents example code from cluttering the main application repository and allows the examples to be maintained and versioned independently.

## 20.4 Extracted Responsibilities

- FHIR connector implementation.
- HL7 connector implementation.

## 20.5 Reusability Scope

- Serves as a template or starting point for new custom connector projects.

## 20.6 Development Benefits

- Provides a better developer experience for System Integrators.
- Decouples the lifecycle of example code from the core product release cycle.

# 21.0 Dependency Contracts

## 21.1 Repo-06-Plugins-Sdk

### 21.1.1 Required Interfaces

- {'interface': 'IConnector', 'methods': ['Provides concrete implementations for the FHIR and HL7 connectors.'], 'events': [], 'properties': []}

### 21.1.2 Integration Pattern

Implements interfaces from the SDK.

### 21.1.3 Communication Protocol

N/A

# 22.0.0 Exposed Contracts

## 22.1.0 Public Interfaces

*No items available*

# 23.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | N/A |
| Event Communication | N/A |
| Data Flow | N/A |
| Error Handling | Demonstrates best practices for error handling wit... |
| Async Patterns | Implements the asynchronous methods defined in the... |

# 24.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Demonstrates how to integrate third-party librarie... |
| Performance Considerations | Example code should follow performance best practi... |
| Security Considerations | Shows how to handle credentials and sensitive data... |
| Testing Approach | Includes unit and integration tests for the exampl... |

# 25.0.0 Scope Boundaries

## 25.1.0 Must Implement

- At least one fully working example for each major plugin type.

## 25.2.0 Must Not Implement

- Anything other than example plugins.

## 25.3.0 Extension Points

- New examples can be added to this repository.

## 25.4.0 Validation Rules

*No items available*

