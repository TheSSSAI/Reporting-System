# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-05-INFRASTRUCTURE |
| Extraction Timestamp | 2024-07-28T10:30:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | Fully Ready |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

REQ-FUNC-DTR-001

#### 1.2.1.2 Requirement Text

The system's .NET backend shall provide a transformation engine to manipulate intermediate JSON data using JavaScript (ECMAScript 2015/ES6) executed via the Jint library.

#### 1.2.1.3 Validation Criteria

- The engine correctly executes a valid ES6 script that performs basic JSON object manipulation (add, remove, modify properties).
- The engine can be instantiated and invoked from the .NET backend code.
- The engine correctly passes a JSON object from the .NET environment to the JavaScript environment.
- The engine correctly returns a modified JSON object from the JavaScript environment to the .NET environment.

#### 1.2.1.4 Implementation Implications

- A wrapper class for the Jint.Engine must be created to implement the ITransformationEngine interface.
- The implementation must handle the conversion between .NET objects (System.Text.Json.JsonNode) and Jint's JavaScript objects.
- The implementation must expose a method like TransformAsync that takes the script and input JSON as parameters.

#### 1.2.1.5 Extraction Reasoning

This requirement mandates the use of the Jint library, a specific technology whose implementation details are the core responsibility of the Infrastructure layer. The repository's analysis explicitly identifies the 'Jint engine wrapper' as part of its scope.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

REQ-SEC-DTR-001

#### 1.2.2.2 Requirement Text

The Jint transformation engine must be executed within a secure sandbox environment with configurable constraints for execution timeout, memory allocation, statement count, and must have Common Language Runtime (CLR) access disabled.

#### 1.2.2.3 Validation Criteria

- A script attempting to execute longer than the configured timeout is terminated.
- A script attempting to allocate more memory than the configured limit is terminated.
- A script attempting to execute more statements than the configured limit is terminated.
- A script attempting to access .NET CLR objects throws a security exception or is blocked.

#### 1.2.2.4 Implementation Implications

- The Jint Engine must be configured with options to set MaxStatements, MemoryLimit, TimeoutInterval, and to DenyCLRAccess.
- The engine wrapper class must catch exceptions thrown by Jint when these constraints are violated (e.g., StatementsCountOverflowException, MemoryLimitExceededException).

#### 1.2.2.5 Extraction Reasoning

This is a direct, technology-specific implementation requirement for the Jint engine. Configuring the sandbox is a classic infrastructure concern, as stated in the repository's analysis which mentions the 'Jint engine configuration and execution sandbox'.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

REQ-SEC-DTR-003

#### 1.2.3.2 Requirement Text

All transformation scripts must be stored encrypted at rest in the system's database.

#### 1.2.3.3 Validation Criteria

- Direct inspection of the database table containing scripts shows that the script content is not in plaintext.
- The application can successfully decrypt and execute a script that is stored in the database.
- Encryption keys are managed securely according to platform best practices (e.g., .NET Data Protection APIs).

#### 1.2.3.4 Implementation Implications

- The Entity Framework Core repository implementation for scripts (EfCoreScriptRepository) must use a Value Converter or similar mechanism.
- The value converter will use .NET's Data Protection APIs (IDataProtector) to encrypt script content before saving and decrypt it after retrieval.
- The IDataProtectionProvider must be configured in the application's service host and injected into the repository or DbContext.

#### 1.2.3.5 Extraction Reasoning

Data persistence and at-rest encryption are core responsibilities of the Infrastructure layer. This requirement directly impacts how the data access layer, implemented in this repository, interacts with the database.

### 1.2.4.0 Requirement Id

#### 1.2.4.1 Requirement Id

REQ-PERF-DTR-002

#### 1.2.4.2 Requirement Text

The transformation engine must process a 10MB JSON dataset with a 200-statement benchmark script in under 10 seconds on standard server hardware (4 vCPU, 16GB RAM), and this benchmark must be included in the QA test suite.

#### 1.2.4.3 Validation Criteria

- A performance test exists in the QA test suite that executes the specified benchmark.
- The test fails if the execution time on the defined standard hardware exceeds 10 seconds.

#### 1.2.4.4 Implementation Implications

- The Jint engine wrapper must be implemented efficiently, avoiding unnecessary overhead in data conversion between .NET and JavaScript environments.
- The Jint engine instance should be configured for optimal performance where possible, though security constraints take precedence.

#### 1.2.4.5 Extraction Reasoning

This non-functional requirement dictates the performance characteristics of the Jint transformation engine, whose concrete implementation resides in this infrastructure repository.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

JintTransformationEngine

#### 1.3.1.2 Component Specification

Provides the concrete implementation of the ITransformationEngine interface using the Jint library. It is responsible for creating a sandboxed JavaScript execution environment, applying security constraints, executing user-provided scripts against JSON data, and returning the transformed result or a structured error.

#### 1.3.1.3 Implementation Requirements

- Must implement the ITransformationEngine interface from the Application layer.
- Must use the Jint library for JavaScript execution.
- Must apply configurable sandbox constraints for memory, CPU time, and statement count.
- Must disable CLR access.
- Must handle exceptions from Jint and wrap them in domain-specific exceptions.

#### 1.3.1.4 Architectural Context

Belongs to the Infrastructure Layer. Acts as an adapter between the application's abstract requirement for transformation and the specific Jint technology.

#### 1.3.1.5 Extraction Reasoning

This component is explicitly identified in the repository's analysis and is the primary implementation for requirements REQ-FUNC-DTR-001 and REQ-SEC-DTR-001.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

EfCoreScriptRepository

#### 1.3.2.2 Component Specification

Provides the concrete implementation of the IScriptRepository interface using Entity Framework Core and a PostgreSQL database. It handles all CRUD operations for TransformationScript and TransformationScriptVersion entities, including the mandatory encryption and decryption of script content.

#### 1.3.2.3 Implementation Requirements

- Must implement the IScriptRepository interface from the Application layer.
- Must use Entity Framework Core for data access.
- Must integrate with the .NET Data Protection API to handle encryption/decryption of the script content field transparently.
- Must be registered in the Dependency Injection container.

#### 1.3.2.4 Architectural Context

Belongs to the Infrastructure Layer. Implements the Repository pattern for data access, decoupling the application from EF Core and PostgreSQL.

#### 1.3.2.5 Extraction Reasoning

This component is explicitly identified in the repository's analysis and represents a core data access responsibility of the Infrastructure layer for implementing REQ-FUNC-DTR-004 and REQ-SEC-DTR-003.

## 1.4.0.0 Architectural Layers

- {'layer_name': 'Infrastructure Layer', 'layer_responsibilities': 'Implements interfaces defined in the Application/Domain layers and handles all interactions with external systems and frameworks. This includes database access (PostgreSQL via EF Core), JavaScript execution (Jint), PDF generation (Puppeteer Sharp), caching (Redis), logging (Serilog), and resiliency (Polly).', 'layer_constraints': ['Must not contain any business logic or application workflow orchestration.', 'Must depend only on inner layers (Application, Domain, Shared) for interfaces and models.', 'Must be the only layer that has direct dependencies on third-party libraries for external concerns (e.g., database drivers, API clients).'], 'implementation_patterns': ['Repository Pattern: For abstracting data access.', 'Adapter Pattern: For wrapping external services and libraries (e.g., Jint engine, S3 client).', 'Dependency Injection: Concrete implementations are registered to be injected where their interfaces are required.'], 'extraction_reasoning': "This repository is explicitly defined as the 'Infrastructure Layer' in the architecture document. Its entire purpose is to provide the concrete implementation for this architectural layer."}

## 1.5.0.0 Dependency Interfaces

### 1.5.1.0 Interface Name

#### 1.5.1.1 Interface Name

ITransformationEngine

#### 1.5.1.2 Source Repository

REPO-08-SERVICE-HOST

#### 1.5.1.3 Method Contracts

- {'method_name': 'ExecuteAsync', 'method_signature': 'Task<TransformationResult> ExecuteAsync(string script, JsonNode jsonData, ScriptConstraints constraints, CancellationToken cancellationToken)', 'method_purpose': 'Executes a given JavaScript against a JSON data structure within a secure sandbox defined by the constraints.', 'integration_context': 'Called by the TransformationService when a script preview is requested or when a transformation step is executed in a report job.'}

#### 1.5.1.4 Integration Pattern

Dependency Inversion (Adapter Pattern)

#### 1.5.1.5 Communication Protocol

In-Process Method Call

#### 1.5.1.6 Extraction Reasoning

As per Clean Architecture, the Application Layer (in REPO-08) defines this interface, and this Infrastructure repository provides the concrete Jint-based implementation. This is a primary example of an interface dependency.

### 1.5.2.0 Interface Name

#### 1.5.2.1 Interface Name

IReportConfigurationRepository

#### 1.5.2.2 Source Repository

REPO-02-CORE-DOMAIN

#### 1.5.2.3 Method Contracts

##### 1.5.2.3.1 Method Name

###### 1.5.2.3.1.1 Method Name

GetByIdAsync

###### 1.5.2.3.1.2 Method Signature

Task<ReportConfiguration> GetByIdAsync(Guid id)

###### 1.5.2.3.1.3 Method Purpose

Retrieves a single ReportConfiguration entity by its unique identifier.

###### 1.5.2.3.1.4 Integration Context

Called by application services when needing to load a specific report's configuration for execution or editing.

##### 1.5.2.3.2.0 Method Name

###### 1.5.2.3.2.1 Method Name

AddAsync

###### 1.5.2.3.2.2 Method Signature

Task AddAsync(ReportConfiguration config)

###### 1.5.2.3.2.3 Method Purpose

Persists a new ReportConfiguration entity.

###### 1.5.2.3.2.4 Integration Context

Called by application services when a new report is created by an administrator.

#### 1.5.2.4.0.0 Integration Pattern

Dependency Inversion (Repository Pattern)

#### 1.5.2.5.0.0 Communication Protocol

In-Process Method Call

#### 1.5.2.6.0.0 Extraction Reasoning

The Domain Layer (REPO-02) defines the data access contract for its aggregates. This Infrastructure repository provides the concrete Entity Framework Core implementation, fulfilling the contract.

### 1.5.3.0.0.0 Interface Name

#### 1.5.3.1.0.0 Interface Name

IAuditLogger

#### 1.5.3.2.0.0 Source Repository

REPO-08-SERVICE-HOST

#### 1.5.3.3.0.0 Method Contracts

- {'method_name': 'LogAuditEventAsync', 'method_signature': 'Task LogAuditEventAsync(AuditEvent auditEvent)', 'method_purpose': 'Asynchronously logs a structured audit event to a persistent, immutable store.', 'integration_context': 'Called by application services whenever a security-sensitive action occurs, such as updating a configuration or a user role.'}

#### 1.5.3.4.0.0 Integration Pattern

Dependency Inversion (Service Interface)

#### 1.5.3.5.0.0 Communication Protocol

In-Process Method Call

#### 1.5.3.6.0.0 Extraction Reasoning

The Application Layer (in REPO-08) defines an abstract contract for auditing. This Infrastructure repository implements the persistence of those audit logs to the database, as required by REQ-SEC-DTR-004.

## 1.6.0.0.0.0 Exposed Interfaces

### 1.6.1.0.0.0 Interface Name

#### 1.6.1.1.0.0 Interface Name

JintTransformationEngine

#### 1.6.1.2.0.0 Consumer Repositories

- REPO-08-SERVICE-HOST

#### 1.6.1.3.0.0 Method Contracts

- {'method_name': 'ExecuteAsync', 'method_signature': 'Task<TransformationResult> ExecuteAsync(string script, JsonNode jsonData, ScriptConstraints constraints, CancellationToken cancellationToken)', 'method_purpose': 'Implements the ITransformationEngine interface using the Jint library, providing a sandboxed JavaScript execution environment.', 'implementation_requirements': 'The class must be public and have a public constructor to be registered in the Dependency Injection container.'}

#### 1.6.1.4.0.0 Service Level Requirements

- Must adhere to performance requirement REQ-PERF-DTR-002.
- Must enforce security constraints from REQ-SEC-DTR-001.

#### 1.6.1.5.0.0 Implementation Constraints

- Must use the Jint library.
- Must handle exceptions gracefully as per REQ-REL-DTR-001.

#### 1.6.1.6.0.0 Extraction Reasoning

This is a concrete class in this repository that implements a core interface. It is 'exposed' to the application's composition root (in REPO-08-SERVICE-HOST) which needs to know about this specific type to register it for dependency injection.

### 1.6.2.0.0.0 Interface Name

#### 1.6.2.1.0.0 Interface Name

EfCoreReportConfigurationRepository

#### 1.6.2.2.0.0 Consumer Repositories

- REPO-08-SERVICE-HOST

#### 1.6.2.3.0.0 Method Contracts

- {'method_name': 'GetByIdAsync', 'method_signature': 'Task<ReportConfiguration> GetByIdAsync(Guid id)', 'method_purpose': 'Implements the IReportConfigurationRepository interface using Entity Framework Core.', 'implementation_requirements': "The class must be public, inherit from the repository interface, and take the application's DbContext as a constructor dependency."}

#### 1.6.2.4.0.0 Service Level Requirements

- Database queries must be optimized for performance.

#### 1.6.2.5.0.0 Implementation Constraints

- Must use Entity Framework Core and the Npgsql provider.

#### 1.6.2.6.0.0 Extraction Reasoning

This is a concrete repository implementation class that is exposed to the service host (REPO-08) for DI registration. It represents the data access implementation for a core domain entity defined in REPO-02-CORE-DOMAIN.

## 1.7.0.0.0.0 Technology Context

### 1.7.1.0.0.0 Framework Requirements

.NET 8, C# 12. Must implement interfaces from the .NET 8 Domain and Application libraries.

### 1.7.2.0.0.0 Integration Technologies

- Entity Framework Core 8 with Npgsql provider for PostgreSQL communication.
- Jint for sandboxed JavaScript execution.
- Puppeteer Sharp for generating PDFs from HTML.
- StackExchange.Redis for distributed caching.
- Polly for implementing resiliency patterns (retry, circuit breaker) on external API calls.
- Serilog for structured logging.

### 1.7.3.0.0.0 Performance Constraints

Database queries must be optimized to prevent performance bottlenecks. Caching strategies for frequently accessed data should be implemented using Redis. Jint engine must meet the benchmark in REQ-PERF-DTR-002.

### 1.7.4.0.0.0 Security Requirements

Manages database connection strings and other secrets securely via .NET's configuration system. The Jint engine must be strictly sandboxed to prevent malicious code execution. Sensitive data (like script content) must be encrypted at rest using .NET Data Protection APIs.

## 1.8.0.0.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | The repository's mappings to requirements, compone... |
| Cross Reference Validation | All cross-references are consistent. The repositor... |
| Implementation Readiness Assessment | The context is highly implementation-ready. It spe... |
| Quality Assurance Confirmation | The extracted context demonstrates high quality th... |

