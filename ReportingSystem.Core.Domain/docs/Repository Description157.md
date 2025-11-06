# 1 Id

REPO-02-CORE-DOMAIN

# 2 Name

ReportingSystem.Core.Domain

# 3 Description

This foundational repository contains the heart of the business logic, completely decoupled from any infrastructure or presentation concerns. It houses the core domain entities such as User, Role, ReportConfiguration, and JobExecutionLog, defining their properties and intrinsic business rules. It also specifies the essential contracts (interfaces) for data persistence (e.g., IReportRepository, IUserRepository) and external services (e.g., IConnector for the plugin model). By centralizing the pure domain model and its abstract dependencies, this component ensures architectural integrity and enforces the Dependency Inversion Principle. It was extracted from the original monorepo to create a stable, reusable core that changes less frequently than application or infrastructure code, forming the innermost layer of the Clean Architecture.

# 4 Type

🔹 Domain Library

# 5 Namespace

ReportingSystem.Core.Domain

# 6 Output Path

src/libs/ReportingSystem.Core.Domain

# 7 Framework

.NET 8

# 8 Language

C#

# 9 Technology

.NET Class Library

# 10 Thirdparty Libraries

*No items available*

# 11 Layer Ids

- domain-layer

# 12 Dependencies

- REPO-04-SHARED-COMMON

# 13 Requirements

*No items available*

# 14 Generate Tests

✅ Yes

# 15 Generate Documentation

✅ Yes

# 16 Architecture Style

Clean Architecture (Domain Layer)

# 17 Architecture Map

- domain-entities
- business-rules

# 18 Components Map

*No items available*

# 19 Requirements Map

- 3.2.1 Core Data Entities

# 20 Decomposition Rationale

## 20.1 Operation Type

NEW_DECOMPOSED

## 20.2 Source Repository

REPO-01-MONO

## 20.3 Decomposition Reasoning

Extracted to establish a pure, stable, and reusable business domain core. This separation protects the business logic from changes in technology (UI, database, etc.) and provides a clear foundation for all other backend components to build upon, strictly enforcing the Clean Architecture model.

## 20.4 Extracted Responsibilities

- Core domain entity definitions (User, ReportConfiguration, etc.)
- Business logic and validation rules intrinsic to the domain models
- Abstract repository and service interfaces (e.g., IUserRepository, IConnector)

## 20.5 Reusability Scope

- This library is the fundamental building block for all backend services within the Reporting System.
- It could potentially be reused in other future systems that interact with the same business domain.

## 20.6 Development Benefits

- Provides a stable foundation that changes infrequently, reducing cognitive load for feature developers.
- Enforces a clear separation between business rules and technology implementations.

# 21.0 Dependency Contracts

*No data available*

# 22.0 Exposed Contracts

## 22.1 Public Interfaces

### 22.1.1 Interface

#### 22.1.1.1 Interface

IReportConfigurationRepository

#### 22.1.1.2 Methods

- Task<ReportConfiguration> GetByIdAsync(Guid id)
- Task AddAsync(ReportConfiguration config)
- Task UpdateAsync(ReportConfiguration config)

#### 22.1.1.3 Events

*No items available*

#### 22.1.1.4 Properties

*No items available*

#### 22.1.1.5 Consumers

- REPO-05-INFRASTRUCTURE
- REPO-08-SERVICE-HOST

### 22.1.2.0 Interface

#### 22.1.2.1 Interface

IConnector

#### 22.1.2.2 Methods

- Task<JsonNode> FetchDataAsync(ConnectorConfiguration config)

#### 22.1.2.3 Events

*No items available*

#### 22.1.2.4 Properties

*No items available*

#### 22.1.2.5 Consumers

- REPO-06-PLUGINS-SDK
- REPO-08-SERVICE-HOST

# 23.0.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | Interfaces are defined here; implementations are i... |
| Event Communication | Defines domain event classes, but does not publish... |
| Data Flow | Defines the shape of data (entities) flowing throu... |
| Error Handling | Defines custom business-logic-specific exceptions. |
| Async Patterns | All data access interfaces are designed to be asyn... |

# 24.0.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | This library must remain framework-agnostic. Avoid... |
| Performance Considerations | N/A - This layer contains logic, not performance-c... |
| Security Considerations | Domain entities should encapsulate validation logi... |
| Testing Approach | Focus on pure unit tests with no external dependen... |

# 25.0.0.0 Scope Boundaries

## 25.1.0.0 Must Implement

- All core business entities and aggregates.
- Interfaces for all infrastructure dependencies (persistence, etc.).
- Domain-level validation logic.

## 25.2.0.0 Must Not Implement

- Any data access code (e.g., EF Core DbContext).
- Any web or API related code (e.g., Controllers, DTOs).
- Any direct interaction with external systems (e.g., logging, caching).

## 25.3.0.0 Extension Points

- New domain entities and services can be added.

## 25.4.0.0 Validation Rules

- Entity constructors and methods should enforce invariants to prevent invalid states.

