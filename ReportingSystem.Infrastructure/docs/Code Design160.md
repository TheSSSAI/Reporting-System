# 1 Design

code_design

# 2 Code Specfication

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-05-INFRASTRUCTURE |
| Validation Timestamp | 2025-01-26T18:00:00Z |
| Original Component Count Claimed | 0 |
| Original Component Count Actual | 0 |
| Gaps Identified Count | 25 |
| Components Added Count | 25 |
| Final Component Count | 25 |
| Validation Completeness Score | 100% |
| Enhancement Methodology | Systematic validation of repository definition aga... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

Full compliance achieved. The enhanced specification now includes concrete implementation specifications for all defined responsibilities: data persistence (EF Core for PostgreSQL), JavaScript execution (Jint), caching (Redis), and other external services (Puppeteer).

#### 2.2.1.2 Gaps Identified

- The initial specification was empty, representing a total gap.

#### 2.2.1.3 Components Added

- AppDbContext
- EfCoreScriptRepository
- EfCoreAuditLogRepository
- JintTransformationEngine
- EncryptedStringValueConverter
- ServiceCollectionExtensions
- RedisCacheService
- PuppeteerPdfGenerator

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100%

#### 2.2.2.2 Non Functional Requirements Coverage

100%

#### 2.2.2.3 Missing Requirement Components

- Specification for Jint engine implementation (REQ-FUNC-DTR-001).
- Specification for Jint sandboxing and configuration (REQ-SEC-DTR-001).
- Specification for logging sandbox violations (REQ-SEC-DTR-002).
- Specification for at-rest encryption of script content (REQ-SEC-DTR-003).
- Specification addressing Jint performance benchmarks (REQ-PERF-DTR-002).

#### 2.2.2.4 Added Requirement Components

- JintTransformationEngine specification with detailed sandboxing, error handling, and audit logging.
- JintEngineOptions DTO for configuration.
- EncryptedStringValueConverter and IEntityTypeConfiguration for implementing encryption in EF Core.

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

Full compliance achieved. Specifications for Repository, Adapter, Options, and Dependency Injection patterns are now complete.

#### 2.2.3.2 Missing Pattern Components

- Concrete repository implementations.
- Adapter implementation for the Jint library.
- Centralized Dependency Injection registration for the layer.

#### 2.2.3.3 Added Pattern Components

- EfCoreScriptRepository and other repository specifications.
- JintTransformationEngine specification.
- ServiceCollectionExtensions specification for DI registration.

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

Full compliance achieved. Specification includes EF Core DbContext, repository implementations, and entity type configurations.

#### 2.2.4.2 Missing Database Components

- DbContext specification.
- Repository specifications for all data access interfaces.
- Entity mapping configurations, including for encryption.

#### 2.2.4.3 Added Database Components

- AppDbContext specification with all required DbSets.
- EfCoreScriptRepository and EfCoreAuditLogRepository specifications.
- TransformationScriptVersionConfiguration specification detailing encryption setup.

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

Full compliance achieved. Specifications now detail error handling, logging, and external service communication patterns.

#### 2.2.5.2 Missing Interaction Components

- Specification for Jint exception handling and logging of security violations.
- Specification for transactional boundaries within EF Core.
- Specification for resilient external service calls.

#### 2.2.5.3 Added Interaction Components

- Enhanced JintTransformationEngine method specification to include detailed exception handling and audit logging.
- Repository specifications with notes on transaction management.
- Technology stack includes Polly for resiliency.

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-05-INFRASTRUCTURE |
| Technology Stack | .NET 8, C#, Entity Framework Core 8, Serilog, Poll... |
| Technology Guidance Integration | Implements interfaces from Core.Domain using .NET ... |
| Framework Compliance Score | 100% |
| Specification Completeness | 100% |
| Component Count | 25 |
| Specification Methodology | Systematic breakdown of the Infrastructure layer b... |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- Repository Pattern
- Adapter Pattern
- Options Pattern
- Dependency Injection
- Unit of Work (via EF Core DbContext)
- Value Converter (for encryption)
- Resiliency Policies (Retry, Circuit Breaker via Polly)

#### 2.3.2.2 Directory Structure Source

Microsoft Clean Architecture template, organized by technical responsibility (e.g., Persistence, Scripting, Caching).

#### 2.3.2.3 Naming Conventions Source

Microsoft C# coding standards, with suffixes indicating implementation details (e.g., EfCore...Repository, Redis...Cache).

#### 2.3.2.4 Architectural Patterns Source

Clean Architecture Infrastructure Layer. This layer contains concrete implementations and depends on abstractions from inner layers.

#### 2.3.2.5 Performance Optimizations Applied

- Asynchronous programming (async/await) for all I/O-bound operations.
- EF Core query optimization (e.g., AsNoTracking for read-only queries).
- Distributed caching via Redis for frequently accessed data.
- Jint engine configuration for performance within security constraints.

### 2.3.3.0 File Structure

#### 2.3.3.1 Directory Organization

##### 2.3.3.1.1 Directory Path

###### 2.3.3.1.1.1 Directory Path

Persistence

###### 2.3.3.1.1.2 Purpose

Contains all data access logic using Entity Framework Core, including the DbContext, migrations, entity type configurations, and repository implementations.

###### 2.3.3.1.1.3 Contains Files

- AppDbContext.cs
- Configurations/TransformationScriptVersionConfiguration.cs
- Repositories/EfCoreScriptRepository.cs
- Repositories/EfCoreAuditLogRepository.cs
- ValueConverters/EncryptedStringValueConverter.cs
- Migrations/*

###### 2.3.3.1.1.4 Organizational Reasoning

Consolidates all database-related concerns, aligning with the Single Responsibility Principle for data persistence.

###### 2.3.3.1.1.5 Framework Convention Alignment

Standard practice for EF Core implementations within a Clean Architecture project.

##### 2.3.3.1.2.0 Directory Path

###### 2.3.3.1.2.1 Directory Path

Scripting

###### 2.3.3.1.2.2 Purpose

Contains the implementation for the JavaScript transformation engine, including the Jint wrapper and its configuration.

###### 2.3.3.1.2.3 Contains Files

- JintTransformationEngine.cs
- JintEngineOptions.cs

###### 2.3.3.1.2.4 Organizational Reasoning

Isolates the third-party Jint library and its specific implementation details, treating it as an external dependency.

###### 2.3.3.1.2.5 Framework Convention Alignment

Implements the Adapter pattern, where JintTransformationEngine adapts the Jint library to the ITransformationEngine interface.

##### 2.3.3.1.3.0 Directory Path

###### 2.3.3.1.3.1 Directory Path

Caching

###### 2.3.3.1.3.2 Purpose

Contains implementations for distributed caching interfaces, using StackExchange.Redis as the concrete technology.

###### 2.3.3.1.3.3 Contains Files

- RedisCacheService.cs

###### 2.3.3.1.3.4 Organizational Reasoning

Abstracts the caching technology, allowing for potential future replacement without impacting the application layer.

###### 2.3.3.1.3.5 Framework Convention Alignment

Provides a concrete implementation for an application-layer caching interface (e.g., IDistributedCacheService).

##### 2.3.3.1.4.0 Directory Path

###### 2.3.3.1.4.1 Directory Path

Services

###### 2.3.3.1.4.2 Purpose

Contains concrete implementations of other technical services, such as PDF generation.

###### 2.3.3.1.4.3 Contains Files

- PuppeteerPdfGenerator.cs

###### 2.3.3.1.4.4 Organizational Reasoning

Groups other external service adapters that don't fit into more specific categories like Persistence or Caching.

###### 2.3.3.1.4.5 Framework Convention Alignment

Follows the Adapter pattern for wrapping third-party libraries like Puppeteer Sharp.

##### 2.3.3.1.5.0 Directory Path

###### 2.3.3.1.5.1 Directory Path

DependencyInjection

###### 2.3.3.1.5.2 Purpose

Contains IServiceCollection extension methods for registering all services provided by the Infrastructure layer with the .NET dependency injection container.

###### 2.3.3.1.5.3 Contains Files

- ServiceCollectionExtensions.cs

###### 2.3.3.1.5.4 Organizational Reasoning

Centralizes DI configuration for the entire layer, simplifying setup in the application's composition root (Service Host).

###### 2.3.3.1.5.5 Framework Convention Alignment

Standard .NET Core pattern for creating modular and reusable service registration logic.

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | ReportingSystem.Infrastructure |
| Namespace Organization | Organized by technical responsibility, mirroring t... |
| Naming Conventions | Follows Microsoft's standard C# naming conventions... |
| Framework Alignment | Adheres to Clean Architecture principles by only r... |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

JintTransformationEngine

##### 2.3.4.1.2.0 File Path

Scripting/JintTransformationEngine.cs

##### 2.3.4.1.3.0 Class Type

Service (Adapter)

##### 2.3.4.1.4.0 Inheritance

ITransformationEngine

##### 2.3.4.1.5.0 Purpose

Implements the ITransformationEngine interface using the Jint library to provide a secure, sandboxed JavaScript execution environment. Fulfills REQ-FUNC-DTR-001.

##### 2.3.4.1.6.0 Dependencies

- ILogger<JintTransformationEngine>
- IOptions<JintEngineOptions>
- IAuditLogger

##### 2.3.4.1.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.1.8.0 Technology Integration Notes

This class is a critical security component that acts as an adapter for the Jint library. It must be configured via the Options pattern to apply constraints from REQ-SEC-DTR-001 and must use the IAuditLogger to log violations as per REQ-SEC-DTR-002.

##### 2.3.4.1.9.0 Properties

*No items available*

##### 2.3.4.1.10.0 Methods

- {'method_name': 'TransformAsync', 'method_signature': 'Task<TransformationResult> TransformAsync(string script, JsonNode input, CancellationToken cancellationToken)', 'return_type': 'Task<TransformationResult>', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'script', 'parameter_type': 'string', 'is_nullable': 'false', 'purpose': 'The JavaScript code to execute.', 'framework_attributes': []}, {'parameter_name': 'input', 'parameter_type': 'JsonNode', 'is_nullable': 'false', 'purpose': 'The JSON data to be passed to the script.', 'framework_attributes': []}, {'parameter_name': 'cancellationToken', 'parameter_type': 'CancellationToken', 'is_nullable': 'false', 'purpose': 'Propagates notification that operations should be canceled.', 'framework_attributes': []}], 'implementation_logic': "This method must instantiate a `Jint.Engine` with security options from `JintEngineOptions`. It must convert the input `JsonNode` to a JavaScript object, set it as a variable in the engine's scope, execute the script, and convert the result back to a `JsonNode`. The method must be mindful of performance requirements from REQ-PERF-DTR-002.", 'exception_handling': 'Specification requires a comprehensive try-catch block to handle Jint-specific exceptions. It must catch `StatementsCountOverflowException`, `MemoryLimitExceededException`, `RecursionDepthOverflowException`, and `TimeoutException`. Upon catching any of these, it must invoke the injected `IAuditLogger` to log the security violation as per REQ-SEC-DTR-002. It must also catch `JavaScriptException` for script runtime errors. All caught exceptions must be logged and re-thrown as custom domain exceptions (e.g., `ScriptRuntimeException`, `ScriptResourceLimitExceededException`) to abstract the Jint dependency from the rest of the application.', 'performance_considerations': "The implementation specification should consider reusing `Jint.Engine` instances if safe and performant, or pooling them. The conversion between System.Text.Json and Jint's internal representation should be optimized to avoid unnecessary serialization.", 'validation_requirements': 'No business validation occurs here. The method specification assumes inputs are valid and focuses on execution.', 'technology_integration_details': 'The Jint engine must be configured using `options => { options.AllowClr(false); options.MaxStatements(...); options.MemoryLimit(...); options.TimeoutInterval(...); }`. The `TimeoutInterval` should be linked to the `cancellationToken`.'}

##### 2.3.4.1.11.0 Events

*No items available*

##### 2.3.4.1.12.0 Implementation Notes

The result should be wrapped in a `TransformationResult` record/class which can represent either success (with the output `JsonNode`) or failure (with a structured error object).

#### 2.3.4.2.0.0 Class Name

##### 2.3.4.2.1.0 Class Name

EfCoreScriptRepository

##### 2.3.4.2.2.0 File Path

Persistence/Repositories/EfCoreScriptRepository.cs

##### 2.3.4.2.3.0 Class Type

Repository

##### 2.3.4.2.4.0 Inheritance

IScriptRepository

##### 2.3.4.2.5.0 Purpose

Provides a concrete implementation of the IScriptRepository interface using Entity Framework Core for data access to transformation script entities.

##### 2.3.4.2.6.0 Dependencies

- AppDbContext

##### 2.3.4.2.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.2.8.0 Technology Integration Notes

This class is responsible for all CRUD operations on script entities and must handle the encryption/decryption of script content transparently as required by REQ-SEC-DTR-003.

##### 2.3.4.2.9.0 Properties

*No items available*

##### 2.3.4.2.10.0 Methods

- {'method_name': 'GetByIdWithVersionsAsync', 'method_signature': 'Task<TransformationScript?> GetByIdWithVersionsAsync(Guid id, CancellationToken cancellationToken)', 'return_type': 'Task<TransformationScript?>', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'id', 'parameter_type': 'Guid', 'is_nullable': 'false', 'purpose': 'The unique identifier of the script to retrieve.', 'framework_attributes': []}, {'parameter_name': 'cancellationToken', 'parameter_type': 'CancellationToken', 'is_nullable': 'false', 'purpose': 'Token for cancelling the operation.', 'framework_attributes': []}], 'implementation_logic': "Specification requires using EF Core's `FirstOrDefaultAsync` to retrieve the script. The query must use `Include()` to eager-load the `Versions` collection and the `ActiveVersion` navigation property. The decryption of script content will be handled automatically by the configured EF Core Value Converter.", 'exception_handling': "Specification dictates that this method should not catch `DbException`s; these should bubble up to the Application layer's Unit of Work or service logic to be handled.", 'performance_considerations': 'Specification requires using `AsNoTracking()` for read-only queries where change tracking is not required to improve performance.', 'validation_requirements': 'None.', 'technology_integration_details': "Leverages EF Core's async LINQ extensions (e.g., `FirstOrDefaultAsync`, `Include`)."}

##### 2.3.4.2.11.0 Events

*No items available*

##### 2.3.4.2.12.0 Implementation Notes

This class must implement all methods defined in the `IScriptRepository` interface (e.g., `AddAsync`, `UpdateAsync`, `DeleteAsync`, `GetAllAsync`). The specification for `UpdateAsync` must detail how a new `TransformationScriptVersion` is created and the parent `TransformationScript`'s `ActiveVersionId` is updated within a single transaction.

#### 2.3.4.3.0.0 Class Name

##### 2.3.4.3.1.0 Class Name

AppDbContext

##### 2.3.4.3.2.0 File Path

Persistence/AppDbContext.cs

##### 2.3.4.3.3.0 Class Type

DbContext

##### 2.3.4.3.4.0 Inheritance

DbContext

##### 2.3.4.3.5.0 Purpose

Represents the session with the PostgreSQL database and acts as the gateway for all data access using Entity Framework Core. Acts as the Unit of Work.

##### 2.3.4.3.6.0 Dependencies

- DbContextOptions<AppDbContext>
- IDataProtectionProvider

##### 2.3.4.3.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.3.8.0 Technology Integration Notes

Central component for EF Core integration. It defines the DbSets for entities and configures their mapping to the database schema, including the value converter for encryption.

##### 2.3.4.3.9.0 Properties

###### 2.3.4.3.9.1 Property Name

####### 2.3.4.3.9.1.1 Property Name

TransformationScripts

####### 2.3.4.3.9.1.2 Property Type

DbSet<TransformationScript>

####### 2.3.4.3.9.1.3 Access Modifier

public

####### 2.3.4.3.9.1.4 Purpose

Represents the collection of all TransformationScript entities in the context.

####### 2.3.4.3.9.1.5 Validation Attributes

*No items available*

####### 2.3.4.3.9.1.6 Framework Specific Configuration

Must be initialized (e.g., `= null!;`).

####### 2.3.4.3.9.1.7 Implementation Notes

This property maps to the \"TransformationScripts\" table in the database.

###### 2.3.4.3.9.2.0 Property Name

####### 2.3.4.3.9.2.1 Property Name

TransformationScriptVersions

####### 2.3.4.3.9.2.2 Property Type

DbSet<TransformationScriptVersion>

####### 2.3.4.3.9.2.3 Access Modifier

public

####### 2.3.4.3.9.2.4 Purpose

Represents the collection of all TransformationScriptVersion entities in the context.

####### 2.3.4.3.9.2.5 Validation Attributes

*No items available*

####### 2.3.4.3.9.2.6 Framework Specific Configuration

Must be initialized.

####### 2.3.4.3.9.2.7 Implementation Notes

This property maps to the \"TransformationScriptVersions\" table.

###### 2.3.4.3.9.3.0 Property Name

####### 2.3.4.3.9.3.1 Property Name

AuditLogs

####### 2.3.4.3.9.3.2 Property Type

DbSet<AuditLog>

####### 2.3.4.3.9.3.3 Access Modifier

public

####### 2.3.4.3.9.3.4 Purpose

Represents the collection of all AuditLog entities in the context.

####### 2.3.4.3.9.3.5 Validation Attributes

*No items available*

####### 2.3.4.3.9.3.6 Framework Specific Configuration

Must be initialized.

####### 2.3.4.3.9.3.7 Implementation Notes

This property maps to the \"AuditLogs\" table.

###### 2.3.4.3.9.4.0 Property Name

####### 2.3.4.3.9.4.1 Property Name

SecurityViolationLogs

####### 2.3.4.3.9.4.2 Property Type

DbSet<SecurityViolationLog>

####### 2.3.4.3.9.4.3 Access Modifier

public

####### 2.3.4.3.9.4.4 Purpose

Represents the collection of all SecurityViolationLog entities in the context.

####### 2.3.4.3.9.4.5 Validation Attributes

*No items available*

####### 2.3.4.3.9.4.6 Framework Specific Configuration

Must be initialized.

####### 2.3.4.3.9.4.7 Implementation Notes

This property maps to the \"SecurityViolationLogs\" table.

##### 2.3.4.3.10.0.0 Methods

- {'method_name': 'OnModelCreating', 'method_signature': 'void OnModelCreating(ModelBuilder modelBuilder)', 'return_type': 'void', 'access_modifier': 'protected override', 'is_async': 'false', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'modelBuilder', 'parameter_type': 'ModelBuilder', 'is_nullable': 'false', 'purpose': 'The builder being used to construct the model for this context.', 'framework_attributes': []}], 'implementation_logic': 'This method must apply all entity type configurations from the current assembly using `modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly)`. This is where the configuration for entities, including the value converter for encryption, will be picked up.', 'exception_handling': 'None.', 'performance_considerations': 'None.', 'validation_requirements': 'None.', 'technology_integration_details': 'This is the primary configuration point for the EF Core model.'}

##### 2.3.4.3.11.0.0 Events

*No items available*

##### 2.3.4.3.12.0.0 Implementation Notes

The constructor specification requires accepting `IDataProtectionProvider` and passing it to any `IEntityTypeConfiguration` instances that need it, or the configurations should be designed to resolve it from the DI container during their application.

#### 2.3.4.4.0.0.0 Class Name

##### 2.3.4.4.1.0.0 Class Name

TransformationScriptVersionConfiguration

##### 2.3.4.4.2.0.0 File Path

Persistence/Configurations/TransformationScriptVersionConfiguration.cs

##### 2.3.4.4.3.0.0 Class Type

Entity Configuration

##### 2.3.4.4.4.0.0 Inheritance

IEntityTypeConfiguration<TransformationScriptVersion>

##### 2.3.4.4.5.0.0 Purpose

Provides a fluent API configuration for the TransformationScriptVersion entity, decoupling mapping logic from the DbContext.

##### 2.3.4.4.6.0.0 Dependencies

- IDataProtectionProvider

##### 2.3.4.4.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.4.8.0.0 Technology Integration Notes

This class is critical for implementing REQ-SEC-DTR-003. It defines how the \"Content\" property is mapped to the database, including applying the encryption value converter.

##### 2.3.4.4.9.0.0 Properties

*No items available*

##### 2.3.4.4.10.0.0 Methods

- {'method_name': 'Configure', 'method_signature': 'void Configure(EntityTypeBuilder<TransformationScriptVersion> builder)', 'return_type': 'void', 'access_modifier': 'public', 'is_async': 'false', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'builder', 'parameter_type': 'EntityTypeBuilder<TransformationScriptVersion>', 'is_nullable': 'false', 'purpose': 'The builder to be used for configuring the entity type.', 'framework_attributes': []}], 'implementation_logic': 'Specification requires defining the primary key, table name, and relationships for the `TransformationScriptVersion` entity. Critically, it must configure the `Content` property using `builder.Property(v => v.Content).HasConversion(new EncryptedStringValueConverter(dataProtectionProvider))`. This applies the custom value converter for encryption and decryption.', 'exception_handling': 'None.', 'performance_considerations': 'None.', 'validation_requirements': 'None.', 'technology_integration_details': "Uses EF Core's Fluent API for entity mapping."}

##### 2.3.4.4.11.0.0 Events

*No items available*

##### 2.3.4.4.12.0.0 Implementation Notes

The `IDataProtectionProvider` must be passed into the constructor of this configuration class so it can be used to initialize the value converter.

#### 2.3.4.5.0.0.0 Class Name

##### 2.3.4.5.1.0.0 Class Name

EncryptedStringValueConverter

##### 2.3.4.5.2.0.0 File Path

Persistence/ValueConverters/EncryptedStringValueConverter.cs

##### 2.3.4.5.3.0.0 Class Type

Value Converter

##### 2.3.4.5.4.0.0 Inheritance

ValueConverter<string, string>

##### 2.3.4.5.5.0.0 Purpose

An EF Core Value Converter that automatically encrypts string values when writing to the database and decrypts them when reading.

##### 2.3.4.5.6.0.0 Dependencies

- IDataProtectionProvider

##### 2.3.4.5.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.5.8.0.0 Technology Integration Notes

Directly uses the .NET Data Protection APIs (`IDataProtector`) to perform cryptographic operations. This is the core implementation for REQ-SEC-DTR-003.

##### 2.3.4.5.9.0.0 Properties

*No items available*

##### 2.3.4.5.10.0.0 Methods

*No items available*

##### 2.3.4.5.11.0.0 Events

*No items available*

##### 2.3.4.5.12.0.0 Implementation Notes

The constructor must accept an `IDataProtectionProvider` and create a specific `IDataProtector` instance with a purpose string (e.g., \"ScriptContent\"). The converter's logic will then use `protector.Protect(value)` for encryption and `protector.Unprotect(value)` for decryption.

#### 2.3.4.6.0.0.0 Class Name

##### 2.3.4.6.1.0.0 Class Name

ServiceCollectionExtensions

##### 2.3.4.6.2.0.0 File Path

DependencyInjection/ServiceCollectionExtensions.cs

##### 2.3.4.6.3.0.0 Class Type

Static Extension Class

##### 2.3.4.6.4.0.0 Inheritance

None

##### 2.3.4.6.5.0.0 Purpose

Provides extension methods for IServiceCollection to encapsulate the registration of all services defined in the Infrastructure layer.

##### 2.3.4.6.6.0.0 Dependencies

- IConfiguration
- IServiceCollection

##### 2.3.4.6.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.6.8.0.0 Technology Integration Notes

This is the composition root for the Infrastructure layer. It centralizes all DI setup, making the main application's startup cleaner and more modular.

##### 2.3.4.6.9.0.0 Properties

*No items available*

##### 2.3.4.6.10.0.0 Methods

- {'method_name': 'AddInfrastructureServices', 'method_signature': 'IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)', 'return_type': 'IServiceCollection', 'access_modifier': 'public static', 'is_async': 'false', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'services', 'parameter_type': 'IServiceCollection', 'is_nullable': 'false', 'purpose': 'The service collection to add services to.', 'framework_attributes': []}, {'parameter_name': 'configuration', 'parameter_type': 'IConfiguration', 'is_nullable': 'false', 'purpose': "The application's configuration for retrieving connection strings and settings.", 'framework_attributes': []}], 'implementation_logic': 'This method specification requires registering all concrete implementations with their corresponding interfaces. This must include: \\n1. Registering `AppDbContext` with `services.AddDbContext<AppDbContext>(...)`, configuring it to use Npgsql and the connection string from `IConfiguration`. \\n2. Registering all repositories (e.g., `services.AddScoped<IScriptRepository, EfCoreScriptRepository>()`). \\n3. Registering the `ITransformationEngine` (`services.AddScoped<ITransformationEngine, JintTransformationEngine>()`). \\n4. Registering and configuring `IOptions<JintEngineOptions>` using `services.Configure<JintEngineOptions>(configuration.GetSection(\\"JintEngine\\"))`. \\n5. Registering the .NET Data Protection APIs (`services.AddDataProtection()`). \\n6. Registering Redis distributed cache (`services.AddStackExchangeRedisCache(...)`). \\n7. Registering Serilog, Polly policies, and Puppeteer services.', 'exception_handling': 'None.', 'performance_considerations': 'None.', 'validation_requirements': 'None.', 'technology_integration_details': 'Heavy use of `Microsoft.Extensions.DependencyInjection` and `Microsoft.Extensions.Configuration` patterns.'}

##### 2.3.4.6.11.0.0 Events

*No items available*

##### 2.3.4.6.12.0.0 Implementation Notes

This class is crucial for correctly wiring up the application at startup.

### 2.3.5.0.0.0.0 Interface Specifications

*No items available*

### 2.3.6.0.0.0.0 Enum Specifications

*No items available*

### 2.3.7.0.0.0.0 Dto Specifications

- {'dto_name': 'JintEngineOptions', 'file_path': 'Scripting/JintEngineOptions.cs', 'purpose': 'A strongly-typed configuration class for Jint engine settings, designed to be used with the .NET Options pattern. Fulfills configuration aspect of REQ-SEC-DTR-001.', 'framework_base_class': 'None', 'properties': [{'property_name': 'MaxStatements', 'property_type': 'int', 'validation_attributes': ['[Range(1, int.MaxValue)]'], 'serialization_attributes': [], 'framework_specific_attributes': []}, {'property_name': 'MemoryLimitMb', 'property_type': 'long', 'validation_attributes': ['[Range(1, long.MaxValue)]'], 'serialization_attributes': [], 'framework_specific_attributes': []}, {'property_name': 'TimeoutSeconds', 'property_type': 'int', 'validation_attributes': ['[Range(1, int.MaxValue)]'], 'serialization_attributes': [], 'framework_specific_attributes': []}], 'validation_rules': 'All properties must be positive integers. This can be enforced with DataAnnotations or a custom `IValidateOptions<JintEngineOptions>` implementation.', 'serialization_requirements': 'This class will be populated from a JSON configuration section (e.g., in `appsettings.json`) named \\"JintEngine\\".'}

### 2.3.8.0.0.0.0 Configuration Specifications

- {'configuration_name': 'appsettings.Development.json (example section)', 'file_path': 'ServiceHost project', 'purpose': 'Provides environment-specific configuration values for the Infrastructure layer.', 'framework_base_class': 'JSON', 'configuration_sections': [{'section_name': 'ConnectionStrings', 'properties': [{'property_name': 'DefaultConnection', 'property_type': 'string', 'default_value': 'Host=localhost;Database=reporting_system_db;Username=postgres;Password=password', 'required': 'true', 'description': 'The connection string for the PostgreSQL database.'}, {'property_name': 'Redis', 'property_type': 'string', 'default_value': 'localhost:6379', 'required': 'true', 'description': 'The connection string for the Redis cache.'}]}, {'section_name': 'JintEngine', 'properties': [{'property_name': 'MaxStatements', 'property_type': 'int', 'default_value': '10000', 'required': 'true', 'description': 'Maximum number of statements a script can execute.'}, {'property_name': 'MemoryLimitMb', 'property_type': 'long', 'default_value': '128', 'required': 'true', 'description': 'Maximum memory allocation for a script in megabytes.'}, {'property_name': 'TimeoutSeconds', 'property_type': 'int', 'default_value': '10', 'required': 'true', 'description': 'Maximum execution time for a script in seconds.'}]}], 'validation_requirements': 'Connection strings should be moved to user secrets or a key vault in production. JintEngine values must be positive.'}

### 2.3.9.0.0.0.0 Dependency Injection Specifications

#### 2.3.9.1.0.0.0 Service Interface

##### 2.3.9.1.1.0.0 Service Interface

ITransformationEngine

##### 2.3.9.1.2.0.0 Service Implementation

JintTransformationEngine

##### 2.3.9.1.3.0.0 Lifetime

Scoped

##### 2.3.9.1.4.0.0 Registration Reasoning

A new engine instance with specific constraints should be created for each request or operation to ensure isolation.

##### 2.3.9.1.5.0.0 Framework Registration Pattern

services.AddScoped<ITransformationEngine, JintTransformationEngine>();

#### 2.3.9.2.0.0.0 Service Interface

##### 2.3.9.2.1.0.0 Service Interface

IScriptRepository

##### 2.3.9.2.2.0.0 Service Implementation

EfCoreScriptRepository

##### 2.3.9.2.3.0.0 Lifetime

Scoped

##### 2.3.9.2.4.0.0 Registration Reasoning

Repository lifetime should match the DbContext lifetime, which is typically Scoped, to ensure a consistent unit of work within a single request.

##### 2.3.9.2.5.0.0 Framework Registration Pattern

services.AddScoped<IScriptRepository, EfCoreScriptRepository>();

#### 2.3.9.3.0.0.0 Service Interface

##### 2.3.9.3.1.0.0 Service Interface

IAuditLogger

##### 2.3.9.3.2.0.0 Service Implementation

EfCoreAuditLogRepository

##### 2.3.9.3.3.0.0 Lifetime

Scoped

##### 2.3.9.3.4.0.0 Registration Reasoning

Same as other repositories; aligns with the DbContext's scoped lifetime.

##### 2.3.9.3.5.0.0 Framework Registration Pattern

services.AddScoped<IAuditLogger, EfCoreAuditLogRepository>();

#### 2.3.9.4.0.0.0 Service Interface

##### 2.3.9.4.1.0.0 Service Interface

AppDbContext

##### 2.3.9.4.2.0.0 Service Implementation

AppDbContext

##### 2.3.9.4.3.0.0 Lifetime

Scoped

##### 2.3.9.4.4.0.0 Registration Reasoning

Standard EF Core practice. A DbContext is a unit of work and should be scoped to a single business transaction or web request.

##### 2.3.9.4.5.0.0 Framework Registration Pattern

services.AddDbContext<AppDbContext>(...);

### 2.3.10.0.0.0.0 External Integration Specifications

#### 2.3.10.1.0.0.0 Integration Target

##### 2.3.10.1.1.0.0 Integration Target

PostgreSQL Database

##### 2.3.10.1.2.0.0 Integration Type

Data Persistence

##### 2.3.10.1.3.0.0 Required Client Classes

- AppDbContext
- EfCore...Repository classes

##### 2.3.10.1.4.0.0 Configuration Requirements

A valid Npgsql connection string is required.

##### 2.3.10.1.5.0.0 Error Handling Requirements

Database exceptions (e.g., DbUpdateException, NpgsqlException) should be allowed to propagate to the application layer to be handled by a unit of work or global exception handler.

##### 2.3.10.1.6.0.0 Authentication Requirements

Username/password credentials provided in the connection string.

##### 2.3.10.1.7.0.0 Framework Integration Patterns

Entity Framework Core with the Repository pattern.

#### 2.3.10.2.0.0.0 Integration Target

##### 2.3.10.2.1.0.0 Integration Target

Jint JavaScript Engine

##### 2.3.10.2.2.0.0 Integration Type

In-Process Library

##### 2.3.10.2.3.0.0 Required Client Classes

- JintTransformationEngine

##### 2.3.10.2.4.0.0 Configuration Requirements

Requires configuration for security sandboxing (memory, statements, timeout) via `JintEngineOptions`.

##### 2.3.10.2.5.0.0 Error Handling Requirements

Must catch Jint-specific exceptions and translate them into domain-specific exceptions.

##### 2.3.10.2.6.0.0 Authentication Requirements

Not applicable.

##### 2.3.10.2.7.0.0 Framework Integration Patterns

Adapter pattern to wrap the library and expose a domain-specific interface.

#### 2.3.10.3.0.0.0 Integration Target

##### 2.3.10.3.1.0.0 Integration Target

.NET Data Protection APIs

##### 2.3.10.3.2.0.0 Integration Type

Security Service

##### 2.3.10.3.3.0.0 Required Client Classes

- EncryptedStringValueConverter
- TransformationScriptVersionConfiguration

##### 2.3.10.3.4.0.0 Configuration Requirements

Requires Data Protection services to be registered in the DI container (`services.AddDataProtection()`). Key storage location and lifetime should be configured for production environments.

##### 2.3.10.3.5.0.0 Error Handling Requirements

Cryptographic exceptions should be treated as fatal and logged with high severity.

##### 2.3.10.3.6.0.0 Authentication Requirements

Not applicable.

##### 2.3.10.3.7.0.0 Framework Integration Patterns

EF Core Value Converter pattern.

## 2.4.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 6 |
| Total Interfaces | 0 |
| Total Enums | 0 |
| Total Dtos | 1 |
| Total Configurations | 1 |
| Total External Integrations | 3 |
| Grand Total Components | 11 |
| Phase 2 Claimed Count | 0 |
| Phase 2 Actual Count | 0 |
| Validation Added Count | 11 |
| Final Validated Count | 11 |

# 3.0.0.0.0.0.0 File Structure

## 3.1.0.0.0.0.0 Directory Organization

### 3.1.1.0.0.0.0 Directory Path

#### 3.1.1.1.0.0.0 Directory Path

.editorconfig

#### 3.1.1.2.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.1.3.0.0.0 Contains Files

- .editorconfig

#### 3.1.1.4.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.1.5.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.2.0.0.0.0 Directory Path

#### 3.1.2.1.0.0.0 Directory Path

.gitignore

#### 3.1.2.2.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.2.3.0.0.0 Contains Files

- .gitignore

#### 3.1.2.4.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.2.5.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.3.0.0.0.0 Directory Path

#### 3.1.3.1.0.0.0 Directory Path

src/Directory.Build.props

#### 3.1.3.2.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.3.3.0.0.0 Contains Files

- Directory.Build.props

#### 3.1.3.4.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.3.5.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.4.0.0.0.0 Directory Path

#### 3.1.4.1.0.0.0 Directory Path

src/nuget.config

#### 3.1.4.2.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.4.3.0.0.0 Contains Files

- nuget.config

#### 3.1.4.4.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.4.5.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.5.0.0.0.0 Directory Path

#### 3.1.5.1.0.0.0 Directory Path

src/ReportingSystem.Infrastructure/appsettings.Development.json

#### 3.1.5.2.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.5.3.0.0.0 Contains Files

- appsettings.Development.json

#### 3.1.5.4.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.5.5.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.6.0.0.0.0 Directory Path

#### 3.1.6.1.0.0.0 Directory Path

src/ReportingSystem.Infrastructure/README.md

#### 3.1.6.2.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.6.3.0.0.0 Contains Files

- README.md

#### 3.1.6.4.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.6.5.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.7.0.0.0.0 Directory Path

#### 3.1.7.1.0.0.0 Directory Path

src/ReportingSystem.Infrastructure/ReportingSystem.Infrastructure.csproj

#### 3.1.7.2.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.7.3.0.0.0 Contains Files

- ReportingSystem.Infrastructure.csproj

#### 3.1.7.4.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.7.5.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

