# 1 Id

REPO-03-SHARED-CONTRACTS

# 2 Name

ReportingSystem.Shared.Contracts

# 3 Description

This repository serves as the definitive, shared contract between the frontend (UI) and the backend (API). It contains only plain C# objects (DTOs - Data Transfer Objects), enums, and constants that are used in API requests and responses. By isolating these contracts, it completely decouples the presentation layer from the backend's internal domain model, allowing them to evolve independently. For example, a `ReportConfigurationDto` might be a flattened version of the `ReportConfiguration` domain entity. This repository is essential for enabling independent development streams and can be packaged (e.g., as a NuGet or NPM package via code generation) to provide a strongly-typed API client for consumers.

# 4 Type

🔹 Model Library

# 5 Namespace

ReportingSystem.Shared.Contracts

# 6 Output Path

src/libs/ReportingSystem.Shared.Contracts

# 7 Framework

.NET 8

# 8 Language

C#

# 9 Technology

.NET Class Library

# 10 Thirdparty Libraries

- System.Text.Json

# 11 Layer Ids

- contracts-layer

# 12 Dependencies

*No items available*

# 13 Requirements

*No items available*

# 14 Generate Tests

❌ No

# 15 Generate Documentation

✅ Yes

# 16 Architecture Style

API Contract-First (Conceptual)

# 17 Architecture Map

- api-contracts

# 18 Components Map

*No items available*

# 19 Requirements Map

- 5.2 API Interfaces

# 20 Decomposition Rationale

## 20.1 Operation Type

NEW_DECOMPOSED

## 20.2 Source Repository

REPO-01-MONO

## 20.3 Decomposition Reasoning

Extracted to create a strict, technology-agnostic boundary between the frontend and backend. It prevents the backend's domain model from leaking into the UI and allows the API surface to be designed and versioned independently of the underlying business logic, which is critical for maintainability.

## 20.4 Extracted Responsibilities

- API Data Transfer Objects (DTOs) for all REST endpoints.
- Shared enumerations used in API requests/responses.
- Request and response models for all CRUD operations.

## 20.5 Reusability Scope

- Consumed directly by the backend API layer.
- Can be used to generate TypeScript models for the React frontend.
- Can be published as a client library for any third-party system integrating with the API.

## 20.6 Development Benefits

- Enables frontend and backend teams to work in parallel against a stable contract.
- Simplifies API versioning and evolution.

# 21.0 Dependency Contracts

*No data available*

# 22.0 Exposed Contracts

## 22.1 Public Interfaces

### 22.1.1 Interface

#### 22.1.1.1 Interface

CreateReportRequest (Class)

#### 22.1.1.2 Methods

*No items available*

#### 22.1.1.3 Events

*No items available*

#### 22.1.1.4 Properties

- string Name
- string Description
- Guid ConnectorId

#### 22.1.1.5 Consumers

- REPO-08-SERVICE-HOST
- REPO-09-WEB-UI

### 22.1.2.0 Interface

#### 22.1.2.1 Interface

ReportResponse (Class)

#### 22.1.2.2 Methods

*No items available*

#### 22.1.2.3 Events

*No items available*

#### 22.1.2.4 Properties

- Guid Id
- string Name
- DateTime CreatedAt

#### 22.1.2.5 Consumers

- REPO-08-SERVICE-HOST
- REPO-09-WEB-UI

# 23.0.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | N/A - These are passive data structures. |
| Event Communication | N/A |
| Data Flow | Defines the structure of data crossing the network... |
| Error Handling | Defines the structure of standardized API error re... |
| Async Patterns | N/A |

# 24.0.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Objects in this library must be simple POCOs. Avoi... |
| Performance Considerations | Keep models lightweight and optimized for serializ... |
| Security Considerations | Do not include sensitive data (like password hashe... |
| Testing Approach | Minimal testing required, primarily focused on ser... |

# 25.0.0.0 Scope Boundaries

## 25.1.0.0 Must Implement

- Data structures for all public API endpoints.

## 25.2.0.0 Must Not Implement

- Any business logic or validation (this belongs in the domain/application layer).
- Any database-specific attributes or dependencies.

## 25.3.0.0 Extension Points

- New DTOs can be added for new API versions or endpoints.

## 25.4.0.0 Validation Rules

- Basic data annotations (e.g., [Required], [MaxLength]) can be used for automatic model validation in ASP.NET Core.

