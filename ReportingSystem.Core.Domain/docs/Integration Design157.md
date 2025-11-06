# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-02-CORE-DOMAIN |
| Extraction Timestamp | 2024-07-28T10:15:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | Fully Ready |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

US-113

#### 1.2.1.2 Requirement Text

As a System Integrator, I want to develop a custom data connector as a .NET DLL by implementing a clearly defined IConnector interface, so that I can extend the system to ingest data from proprietary or unsupported data sources.

#### 1.2.1.3 Validation Criteria

- A .NET class library project correctly implements the IConnector interface from the provided contract assembly.
- The compiled DLL is placed in the system's designated 'plugins' directory and the main application service is started.
- The system's plug-in loader discovers and loads the custom connector assembly without errors, logging a success message.

#### 1.2.1.4 Implementation Implications

- The IConnector interface must be defined within this domain library to serve as the stable, core contract for all data connectors.
- This interface is the foundation of the system's plugin architecture for data ingestion.
- The domain layer itself must remain agnostic of any specific connector's implementation details.

#### 1.2.1.5 Extraction Reasoning

This user story directly mandates the creation of the IConnector interface. As the most abstract and stable contract for a core system capability (extensibility), its definition belongs in the innermost Domain layer, as confirmed by this repository's architectural analysis.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

US-051

#### 1.2.2.2 Requirement Text

As an Administrator, I want to use a guided, multi-step wizard to create and edit a new report configuration, so that I can ensure all required components (like data source, transformation, output, delivery, and schedule) are configured correctly and in a logical order, minimizing errors and setup time.

#### 1.2.2.3 Validation Criteria

- Successfully create a complete report configuration through the wizard.
- Navigate forward and backward between steps while preserving state.
- Step progression is prevented if required information is missing.

#### 1.2.2.4 Implementation Implications

- A ReportConfiguration domain entity must be defined in this repository to model the state and business rules of a report.
- This entity will be the central aggregate root for concepts like data source, transformation, delivery, and scheduling.
- The domain model must enforce invariants, such as a report requiring a data source and at least one delivery destination.

#### 1.2.2.5 Extraction Reasoning

This user story necessitates the existence of the ReportConfiguration domain entity, which is a core responsibility of this repository. The entity encapsulates the essential business rules and state for a report.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

US-018

#### 1.2.3.2 Requirement Text

As an Administrator, I want to create a new user account by providing their details and assigning a role, so that I can securely grant them the appropriate level of access to the system.

#### 1.2.3.3 Validation Criteria

- Successfully create a new user with a 'Viewer' role.
- Attempt to create a user with a username that already exists and receive an error.
- Attempt to create a user with an email address that already exists and receive an error.

#### 1.2.3.4 Implementation Implications

- The User and Role domain entities must be defined in this repository.
- Business rules, such as username and email uniqueness, should be encapsulated within the domain model or its associated services.
- An interface for user persistence, IUserRepository, must be defined here to abstract the data storage mechanism.

#### 1.2.3.5 Extraction Reasoning

This user story is fundamental to the system's multi-user and RBAC capabilities, requiring the User and Role domain entities and their persistence contract (IUserRepository) to be defined in this core repository.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

ReportConfiguration (Entity)

#### 1.3.1.2 Component Specification

Represents the central aggregate root for a report. It encapsulates all settings, including its name, description, the data connector to use, an optional transformation script, output format, delivery destinations, and schedule. It enforces business rules such as requiring a connector and at least one delivery destination.

#### 1.3.1.3 Implementation Requirements

- Must be implemented as a Plain Old C# Object (POCO).
- Must contain properties to reference other entities or their IDs (e.g., ConnectorConfigurationId, TransformationScriptId).
- Should contain methods or logic to enforce its own validity (invariants).

#### 1.3.1.4 Architectural Context

Domain Layer - Domain Entity

#### 1.3.1.5 Extraction Reasoning

This is a core domain entity identified in the repository's description and mandated by multiple requirements (e.g., US-051). It is a central part of the system's business logic.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

User (Entity)

#### 1.3.2.2 Component Specification

Represents a user of the system. It contains properties for identity (username, email) and status (e.g., IsActive, IsLocked). It is responsible for encapsulating business rules related to user identity.

#### 1.3.2.3 Implementation Requirements

- Must be implemented as a POCO.
- Will be linked to a Role entity to establish permissions.
- Should not contain password information directly; this is an infrastructure concern.

#### 1.3.2.4 Architectural Context

Domain Layer - Domain Entity

#### 1.3.2.5 Extraction Reasoning

The User entity is fundamental for the system's authentication, authorization, and auditing capabilities, as required by numerous user stories (e.g., US-018, US-019).

### 1.3.3.0 Component Name

#### 1.3.3.1 Component Name

TransformationScript (Entity)

#### 1.3.3.2 Component Specification

Represents the aggregate root for a transformation script. It manages its own metadata (e.g., name) and a collection of its versions. It is responsible for enforcing rules related to script versioning, such as tracking the active version.

#### 1.3.3.3 Implementation Requirements

- Must be implemented as a POCO.
- Must manage a collection of TransformationScriptVersion child entities.
- Must contain logic for creating new versions upon update.

#### 1.3.3.4 Architectural Context

Domain Layer - Domain Entity

#### 1.3.3.5 Extraction Reasoning

This entity is explicitly required by REQ-FUNC-DTR-004 and REQ-FUNC-DTR-005 to manage the lifecycle and versioning of transformation scripts.

## 1.4.0.0 Architectural Layers

- {'layer_name': 'Domain Layer', 'layer_responsibilities': 'Contains the core business logic, entities, and abstract interfaces of the application. It is the heart of the Clean Architecture model, defining the business rules and data structures without any dependencies on external frameworks like databases or web APIs. It defines the contracts that the Infrastructure and Application layers must fulfill.', 'layer_constraints': ['Must not have any dependencies on the API/Web or Infrastructure layers.', 'Must not contain dependencies on specific frameworks like Entity Framework Core or ASP.NET Core.', 'Entities within this layer must enforce their own validity (invariants) where possible.'], 'implementation_patterns': ['Domain Entities (POCOs)', 'Aggregate Roots', 'Repository Pattern (Interfaces)', 'Dependency Inversion Principle'], 'extraction_reasoning': "This repository, ReportingSystem.Core.Domain, is explicitly mapped to the 'domain_layer' in the architecture document, and its description, dependencies, and scope boundaries perfectly align with the responsibilities of the Domain Layer in a Clean Architecture."}

## 1.5.0.0 Dependency Interfaces

*No items available*

## 1.6.0.0 Exposed Interfaces

### 1.6.1.0 Interface Name

#### 1.6.1.1 Interface Name

IReportConfigurationRepository

#### 1.6.1.2 Consumer Repositories

- REPO-05-INFRASTRUCTURE
- REPO-08-SERVICE-HOST

#### 1.6.1.3 Method Contracts

##### 1.6.1.3.1 Method Name

###### 1.6.1.3.1.1 Method Name

GetByIdAsync

###### 1.6.1.3.1.2 Method Signature

Task<ReportConfiguration?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)

###### 1.6.1.3.1.3 Method Purpose

Retrieves a single ReportConfiguration entity by its unique identifier.

###### 1.6.1.3.1.4 Implementation Requirements

The implementation must use the underlying data store (e.g., EF Core) to query and return the entity.

##### 1.6.1.3.2.0 Method Name

###### 1.6.1.3.2.1 Method Name

AddAsync

###### 1.6.1.3.2.2 Method Signature

Task AddAsync(ReportConfiguration config, CancellationToken cancellationToken = default)

###### 1.6.1.3.2.3 Method Purpose

Persists a new ReportConfiguration entity.

###### 1.6.1.3.2.4 Implementation Requirements

The implementation must use the underlying data store to insert a new record.

##### 1.6.1.3.3.0 Method Name

###### 1.6.1.3.3.1 Method Name

UpdateAsync

###### 1.6.1.3.3.2 Method Signature

Task UpdateAsync(ReportConfiguration config, CancellationToken cancellationToken = default)

###### 1.6.1.3.3.3 Method Purpose

Updates an existing ReportConfiguration entity.

###### 1.6.1.3.3.4 Implementation Requirements

The implementation must use the underlying data store to update an existing record.

#### 1.6.1.4.0.0 Service Level Requirements

*No items available*

#### 1.6.1.5.0.0 Implementation Constraints

- Implementations of this interface must reside in the Infrastructure layer.

#### 1.6.1.6.0.0 Extraction Reasoning

This is a key exposed contract that defines the persistence requirements for ReportConfiguration entities, as required by US-051. It is implemented by REPO-05-INFRASTRUCTURE and consumed by REPO-08-SERVICE-HOST, perfectly demonstrating the Dependency Inversion principle.

### 1.6.2.0.0.0 Interface Name

#### 1.6.2.1.0.0 Interface Name

IConnector

#### 1.6.2.2.0.0 Consumer Repositories

- REPO-06-PLUGINS-SDK
- REPO-08-SERVICE-HOST
- REPO-07-PLUGINS-EXAMPLES

#### 1.6.2.3.0.0 Method Contracts

##### 1.6.2.3.1.0 Method Name

###### 1.6.2.3.1.1 Method Name

GetName

###### 1.6.2.3.1.2 Method Signature

string GetName()

###### 1.6.2.3.1.3 Method Purpose

Returns the display name of the connector for use in the Control Panel UI.

###### 1.6.2.3.1.4 Implementation Requirements

The name should be human-readable and unique among connectors.

##### 1.6.2.3.2.0 Method Name

###### 1.6.2.3.2.1 Method Name

GetConfigurationSchema

###### 1.6.2.3.2.2 Method Signature

string GetConfigurationSchema()

###### 1.6.2.3.2.3 Method Purpose

Returns a JSON schema that defines the configuration UI for this connector, as required by US-037.

###### 1.6.2.3.2.4 Implementation Requirements

Implementation must return a valid JSON schema string.

##### 1.6.2.3.3.0 Method Name

###### 1.6.2.3.3.1 Method Name

TestConnectionAsync

###### 1.6.2.3.3.2 Method Signature

Task TestConnectionAsync(JsonNode configuration, CancellationToken cancellationToken)

###### 1.6.2.3.3.3 Method Purpose

Performs a live test of the connection using the provided configuration, as required by US-038.

###### 1.6.2.3.3.4 Implementation Requirements

Must attempt a connection and throw a specific exception on failure.

##### 1.6.2.3.4.0 Method Name

###### 1.6.2.3.4.1 Method Name

FetchDataAsync

###### 1.6.2.3.4.2 Method Signature

Task<JsonNode> FetchDataAsync(JsonNode configuration, CancellationToken cancellationToken)

###### 1.6.2.3.4.3 Method Purpose

Executes the data ingestion logic for a specific connector configuration.

###### 1.6.2.3.4.4 Implementation Requirements

Implementations must handle connection, data fetching, and transformation into a System.Text.Json.JsonNode object.

#### 1.6.2.4.0.0 Service Level Requirements

*No items available*

#### 1.6.2.5.0.0 Implementation Constraints

- Implementations of this interface are intended to be loaded as plugins.

#### 1.6.2.6.0.0 Extraction Reasoning

This contract is the cornerstone of the system's plugin architecture for data sources (US-113). It is defined in the core domain to ensure maximum stability. It is exposed to the SDK (REPO-06) for third-party developers, the Examples (REPO-07) for reference, and consumed by the Service Host (REPO-08) to execute reports.

### 1.6.3.0.0.0 Interface Name

#### 1.6.3.1.0.0 Interface Name

IUserRepository

#### 1.6.3.2.0.0 Consumer Repositories

- REPO-05-INFRASTRUCTURE
- REPO-08-SERVICE-HOST

#### 1.6.3.3.0.0 Method Contracts

##### 1.6.3.3.1.0 Method Name

###### 1.6.3.3.1.1 Method Name

GetByIdAsync

###### 1.6.3.3.1.2 Method Signature

Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)

###### 1.6.3.3.1.3 Method Purpose

Retrieves a single User entity by its unique identifier.

###### 1.6.3.3.1.4 Implementation Requirements

Implementation must query the underlying data store for the user.

##### 1.6.3.3.2.0 Method Name

###### 1.6.3.3.2.1 Method Name

GetByUsernameAsync

###### 1.6.3.3.2.2 Method Signature

Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)

###### 1.6.3.3.2.3 Method Purpose

Retrieves a single User entity by its unique username.

###### 1.6.3.3.2.4 Implementation Requirements

The lookup should be case-insensitive to align with login business rules.

##### 1.6.3.3.3.0 Method Name

###### 1.6.3.3.3.1 Method Name

AddAsync

###### 1.6.3.3.3.2 Method Signature

Task AddAsync(User user, CancellationToken cancellationToken = default)

###### 1.6.3.3.3.3 Method Purpose

Persists a new User entity.

###### 1.6.3.3.3.4 Implementation Requirements

Implementation must insert a new user record into the data store.

#### 1.6.3.4.0.0 Service Level Requirements

*No items available*

#### 1.6.3.5.0.0 Implementation Constraints

- Implementations of this interface must reside in the Infrastructure layer.

#### 1.6.3.6.0.0 Extraction Reasoning

This repository contract is necessary to support all user management requirements (US-018, US-019, US-020, etc.) while abstracting the persistence details from the application services.

### 1.6.4.0.0.0 Interface Name

#### 1.6.4.1.0.0 Interface Name

ITransformationScriptRepository

#### 1.6.4.2.0.0 Consumer Repositories

- REPO-05-INFRASTRUCTURE
- REPO-08-SERVICE-HOST

#### 1.6.4.3.0.0 Method Contracts

##### 1.6.4.3.1.0 Method Name

###### 1.6.4.3.1.1 Method Name

GetByIdAsync

###### 1.6.4.3.1.2 Method Signature

Task<TransformationScript?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)

###### 1.6.4.3.1.3 Method Purpose

Retrieves a single TransformationScript aggregate root, including its versions.

###### 1.6.4.3.1.4 Implementation Requirements

The implementation must correctly load the aggregate and its child entities.

##### 1.6.4.3.2.0 Method Name

###### 1.6.4.3.2.1 Method Name

AddAsync

###### 1.6.4.3.2.2 Method Signature

Task AddAsync(TransformationScript script, CancellationToken cancellationToken = default)

###### 1.6.4.3.2.3 Method Purpose

Persists a new TransformationScript aggregate.

###### 1.6.4.3.2.4 Implementation Requirements

Implementation must handle the transactional save of the parent and its initial version.

##### 1.6.4.3.3.0 Method Name

###### 1.6.4.3.3.1 Method Name

UpdateAsync

###### 1.6.4.3.3.2 Method Signature

Task UpdateAsync(TransformationScript script, CancellationToken cancellationToken = default)

###### 1.6.4.3.3.3 Method Purpose

Updates an existing TransformationScript aggregate, typically by adding a new version.

###### 1.6.4.3.3.4 Implementation Requirements

Implementation must handle the transactional save of a new version and update of the parent.

#### 1.6.4.4.0.0 Service Level Requirements

*No items available*

#### 1.6.4.5.0.0 Implementation Constraints

- Implementations of this interface must reside in the Infrastructure layer.

#### 1.6.4.6.0.0 Extraction Reasoning

This repository contract is essential for implementing the CRUD and versioning requirements for transformation scripts (REQ-FUNC-DTR-004, REQ-FUNC-DTR-005) in a persistence-ignorant manner.

## 1.7.0.0.0.0 Technology Context

### 1.7.1.0.0.0 Framework Requirements

The repository must be built as a .NET 8 Class Library. It must remain pure and framework-agnostic, with no dependencies on ASP.NET Core, Entity Framework Core, or any UI frameworks.

### 1.7.2.0.0.0 Integration Technologies

*No items available*

### 1.7.3.0.0.0 Performance Constraints

Performance is not a primary concern for this layer, as it contains business logic rather than I/O operations. However, interfaces it defines (e.g., for repositories) must be designed to support asynchronous operations to enable performance in other layers.

### 1.7.4.0.0.0 Security Requirements

Domain entities should enforce their own business rules and invariants through constructors and methods to ensure they cannot exist in an invalid state. This is the first line of defense for data integrity.

## 1.8.0.0.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | All repository mappings were analyzed and found to... |
| Cross Reference Validation | Cross-repository analysis confirmed that this repo... |
| Implementation Readiness Assessment | The repository is fully ready for implementation. ... |
| Quality Assurance Confirmation | Systematic review confirmed the high quality of th... |

