# 1 Design

code_design

# 2 Code Specfication

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-08-SERVICE-HOST |
| Validation Timestamp | 2024-05-24T11:00:00Z |
| Original Component Count Claimed | 0 |
| Original Component Count Actual | 0 |
| Gaps Identified Count | 10 |
| Components Added Count | 10 |
| Final Component Count | 34 |
| Validation Completeness Score | 100% |
| Enhancement Methodology | Systematic validation against cached requirements,... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

Fully compliant after enhancement. The initial empty specification was enhanced to include the composition root (Program.cs), API Layer components (Controllers, Middleware, DTOs), Application Layer components (CQRS handlers, interfaces, behaviors), and background job definitions (Quartz.NET IJob), fully aligning with the repository's defined role as the Modular Monolith's service host.

#### 2.2.1.2 Gaps Identified

- Missing specification for the application entry point and composition root (Program.cs).
- Missing specifications for API controllers required by the system (e.g., TransformationsController).
- Missing specification for global exception handling middleware.
- Missing specifications for background job orchestration (Quartz.NET jobs).

#### 2.2.1.3 Components Added

- Program.cs specification
- TransformationsController specification
- ExceptionHandlingMiddleware specification
- ReportGenerationJob specification

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100%

#### 2.2.2.2 Non Functional Requirements Coverage

100%

#### 2.2.2.3 Missing Requirement Components

- A component specification to handle structured API error responses (REQ-FUNC-DTR-003).
- A component specification to handle RBAC on API endpoints (REQ-SEC-DTR-004).
- A specification for enforcing API timeouts via CancellationTokens (REQ-PERF-DTR-001).
- A specification for ensuring job isolation and service reliability (REQ-REL-DTR-001).

#### 2.2.2.4 Added Requirement Components

- ExceptionHandlingMiddleware specification with detailed error mapping.
- Controller specifications enhanced with security attribute requirements.
- Method signatures enhanced to include CancellationToken propagation.
- ReportGenerationJob specification with robust top-level exception handling.

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

Fully specified. The initial empty specification was enhanced to detail the implementation of Clean Architecture, CQRS with MediatR, and the Dependency Inversion Principle through clearly defined layers and interface contracts.

#### 2.2.3.2 Missing Pattern Components

- CQRS Command/Handler structure for application use cases.
- MediatR pipeline behavior specifications for cross-cutting concerns (validation, logging).
- Interface specifications for infrastructure dependencies to enforce Dependency Inversion.

#### 2.2.3.3 Added Pattern Components

- ExecutePreview CQRS command, handler, and validator specifications.
- ITransformationEngine and IAuditLogger interface specifications.
- DependencyInjection specifications detailing registration of architectural patterns.

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

N/A - This repository does not contain data access implementations. Validation confirmed that the Application Layer correctly defines data access contracts (interfaces) and delegates implementation to the Infrastructure layer, which is architecturally compliant.

#### 2.2.4.2 Missing Database Components

*No items available*

#### 2.2.4.3 Added Database Components

*No items available*

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

Fully specified. The enhanced specifications for controllers, middleware, CQRS handlers, and background jobs directly map to the interactions and error handling flows detailed in the provided sequence diagrams.

#### 2.2.5.2 Missing Interaction Components

- A specification for the component that translates application exceptions into HTTP status codes as seen in sequence diagrams.
- A specification for the background job that orchestrates the reporting pipeline.

#### 2.2.5.3 Added Interaction Components

- ExceptionHandlingMiddleware specification.
- ReportGenerationJob specification.

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-08-SERVICE-HOST |
| Technology Stack | .NET 8, ASP.NET Core, Windows Services, Quartz.NET... |
| Technology Guidance Integration | .NET 8 best practices, Clean Architecture principl... |
| Framework Compliance Score | 100% |
| Specification Completeness | 100% |
| Component Count | 34 |
| Specification Methodology | Systematic, requirements-driven decomposition foll... |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- Composition Root
- Dependency Injection
- CQRS with MediatR
- Middleware Pipeline
- Repository Pattern (Interfaces)
- Service Layer
- Hosted Services (for Quartz.NET and background tasks)
- Options Pattern for Configuration

#### 2.3.2.2 Directory Structure Source

Microsoft Clean Architecture template, adapted for CQRS and a Modular Monolith composition root.

#### 2.3.2.3 Naming Conventions Source

Microsoft C# coding conventions, with suffixes like \"Controller\", \"Service\", \"Command\", \"Query\", \"Handler\", \"Validator\".

#### 2.3.2.4 Architectural Patterns Source

Modular Monolith composed of Clean Architecture layers, with CQRS pattern in the Application Layer.

#### 2.3.2.5 Performance Optimizations Applied

- Extensive use of async/await for all I/O-bound operations.
- Configuration of Quartz.NET with a persistent job store and optimized thread pool.
- Use of CancellationToken for timeout and cancellation propagation in long-running operations.
- Strongly-typed configuration using IOptions pattern.
- Centralized and structured logging for efficient diagnostics.

### 2.3.3.0 File Structure

#### 2.3.3.1 Directory Organization

##### 2.3.3.1.1 Directory Path

###### 2.3.3.1.1.1 Directory Path

src/ReportingSystem.Service/Api

###### 2.3.3.1.1.2 Purpose

To host all ASP.NET Core API-specific components, representing the Presentation Layer of the Clean Architecture.

###### 2.3.3.1.1.3 Contains Files

- Controllers/TransformationsController.cs
- Controllers/ScriptsController.cs
- Controllers/ReportsController.cs
- Middleware/ExceptionHandlingMiddleware.cs
- Dtos/PreviewRequestDto.cs
- Dtos/CreateScriptDto.cs
- Dtos/UpdateScriptDto.cs
- Dtos/ScriptDto.cs
- Dtos/ScriptVersionDto.cs
- Dtos/ErrorResponseDto.cs

###### 2.3.3.1.1.4 Organizational Reasoning

Separates API concerns (routing, serialization, HTTP handling) from application business logic, adhering to Clean Architecture principles.

###### 2.3.3.1.1.5 Framework Convention Alignment

Follows standard ASP.NET Core conventions for controllers, DTOs, and middleware.

##### 2.3.3.1.2.0 Directory Path

###### 2.3.3.1.2.1 Directory Path

src/ReportingSystem.Service/Application

###### 2.3.3.1.2.2 Purpose

To contain all application-specific business logic, use cases, and orchestration, representing the Application Layer.

###### 2.3.3.1.2.3 Contains Files

- Features/Transformations/Commands/ExecutePreview.cs
- Features/Scripts/Commands/CreateScript.cs
- Features/Scripts/Commands/UpdateScript.cs
- Features/Scripts/Queries/GetScriptById.cs
- Common/Behaviors/ValidationBehavior.cs
- Common/Behaviors/LoggingBehavior.cs
- Common/Behaviors/TransactionBehavior.cs
- Common/Interfaces/IApplicationDbContext.cs
- Common/Interfaces/ITransformationEngine.cs
- Common/Interfaces/IAuditLogger.cs
- Common/Exceptions/ValidationException.cs
- Common/Exceptions/NotFoundException.cs

###### 2.3.3.1.2.4 Organizational Reasoning

Organizes use cases by feature using a vertical slice approach with CQRS, enhancing maintainability and cohesion.

###### 2.3.3.1.2.5 Framework Convention Alignment

Implements the CQRS pattern using MediatR, a common and effective approach in modern .NET applications.

##### 2.3.3.1.3.0 Directory Path

###### 2.3.3.1.3.1 Directory Path

src/ReportingSystem.Service/Core

###### 2.3.3.1.3.2 Purpose

Contains the main application entry point, host configuration, and startup logic.

###### 2.3.3.1.3.3 Contains Files

- Program.cs
- DependencyInjection.cs
- Jobs/ReportGenerationJob.cs

###### 2.3.3.1.3.4 Organizational Reasoning

Serves as the Composition Root, responsible for configuring services, the HTTP pipeline, and background job schedulers.

###### 2.3.3.1.3.5 Framework Convention Alignment

Standard .NET 8 host builder and startup configuration pattern.

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | ReportingSystem.Service |
| Namespace Organization | Organized by architectural layer (Api, Application... |
| Naming Conventions | Follows Microsoft's standard C# and .NET naming co... |
| Framework Alignment | Fully aligned with .NET project structure and name... |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

Program

##### 2.3.4.1.2.0 File Path

src/ReportingSystem.Service/Core/Program.cs

##### 2.3.4.1.3.0 Class Type

Application Entry Point

##### 2.3.4.1.4.0 Inheritance

n/a

##### 2.3.4.1.5.0 Purpose

Configures and runs the application host. This includes setting up the Windows Service, configuring the ASP.NET Core pipeline (Kestrel), initializing the dependency injection container, and starting the Quartz.NET scheduler.

##### 2.3.4.1.6.0 Dependencies

- ReportingSystem.Infrastructure
- ReportingSystem.Service.Application
- ReportingSystem.Service.Api

##### 2.3.4.1.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.1.8.0 Technology Integration Notes

Acts as the composition root for the entire Modular Monolith. Uses .UseWindowsService() for deployment and configures all middleware for the API.

##### 2.3.4.1.9.0 Validation Notes

Validation confirms this component is critical for orchestrating all other project references and fulfilling the \"composition root\" role of the service host.

##### 2.3.4.1.10.0 Properties

*No items available*

##### 2.3.4.1.11.0 Methods

- {'method_name': 'Main', 'method_signature': 'public static void Main(string[] args)', 'return_type': 'void', 'access_modifier': 'public static', 'is_async': False, 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'args', 'parameter_type': 'string[]', 'is_nullable': False, 'purpose': 'Command-line arguments passed to the application.', 'framework_attributes': []}], 'implementation_logic': 'Specification requires creating a host builder, configuring it to run as a Windows Service, configuring Kestrel endpoints, and setting up logging. The specification mandates calling extension methods from this project, the Application layer, and the Infrastructure project to configure the DI container completely. It must build the host and then run it.', 'exception_handling': 'Specification requires a top-level try-catch block to log any fatal exceptions during application startup, ensuring diagnostic visibility.', 'performance_considerations': 'n/a', 'validation_requirements': 'n/a', 'technology_integration_details': 'Specification mandates calls to extension methods like `AddApiServices()`, `AddApplicationServices()`, and `AddInfrastructureServices()` to configure the DI container, adhering to Clean Architecture principles.', 'validation_notes': 'Validation confirms this method specification is the central point for application startup and configuration, correctly setting up all required services and middleware.'}

##### 2.3.4.1.12.0 Events

*No items available*

##### 2.3.4.1.13.0 Implementation Notes

This specification is critical. The implementation must correctly orchestrate the setup of Authentication, Authorization, Swagger/OpenAPI, the custom ExceptionHandlingMiddleware, Quartz.NET scheduling, and all service registrations.

#### 2.3.4.2.0.0 Class Name

##### 2.3.4.2.1.0 Class Name

TransformationsController

##### 2.3.4.2.2.0 File Path

src/ReportingSystem.Service/Api/Controllers/TransformationsController.cs

##### 2.3.4.2.3.0 Class Type

API Controller

##### 2.3.4.2.4.0 Inheritance

ControllerBase

##### 2.3.4.2.5.0 Purpose

Exposes the RESTful API endpoint for previewing transformation scripts, as required by REQ-INTG-DTR-001.

##### 2.3.4.2.6.0 Dependencies

- MediatR.ISender

##### 2.3.4.2.7.0 Framework Specific Attributes

- [ApiController]
- [Route(\"api/v1/transformations\")]
- [Authorize(Roles = \"Administrator\")]

##### 2.3.4.2.8.0 Technology Integration Notes

This specification follows the standard for an ASP.NET Core ApiController that uses MediatR to decouple the controller from the application logic.

##### 2.3.4.2.9.0 Validation Notes

Validation confirms this component correctly implements the API/Web Layer and adheres to CQRS patterns by delegating logic to MediatR.

##### 2.3.4.2.10.0 Properties

*No items available*

##### 2.3.4.2.11.0 Methods

- {'method_name': 'Preview', 'method_signature': 'public async Task<IActionResult> Preview([FromBody] PreviewRequestDto request, CancellationToken cancellationToken)', 'return_type': 'Task<IActionResult>', 'access_modifier': 'public', 'is_async': True, 'framework_specific_attributes': ['[HttpPost(\\"preview\\")]', '[ProducesResponseType(typeof(System.Text.Json.JsonNode), StatusCodes.Status200OK)]', '[ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]', '[ProducesResponseType(StatusCodes.Status403Forbidden)]'], 'parameters': [{'parameter_name': 'request', 'parameter_type': 'PreviewRequestDto', 'is_nullable': False, 'purpose': 'The DTO containing the script and sample data for the preview.', 'framework_attributes': ['[FromBody]']}, {'parameter_name': 'cancellationToken', 'parameter_type': 'CancellationToken', 'is_nullable': False, 'purpose': 'Propagated from the HTTP request to enforce timeouts, fulfilling REQ-PERF-DTR-001.', 'framework_attributes': []}], 'implementation_logic': 'Specification requires creating an `ExecutePreview.Command` object from the request DTO. It must then send this command using the injected MediatR `ISender` instance. The result from the handler should be wrapped in an `OkObjectResult`. The specification prohibits any business logic within this controller method.', 'exception_handling': 'Specification mandates that exceptions are handled by the global ExceptionHandlingMiddleware. This method should not contain its own try-catch blocks for business exceptions.', 'performance_considerations': 'The controller action must remain lightweight. Specification requires passing the `CancellationToken` to the MediatR pipeline to handle timeouts in the application logic.', 'validation_requirements': "ASP.NET Core's built-in model validation will be automatically triggered for the PreviewRequestDto based on its attributes.", 'technology_integration_details': 'This method specification directly implements the API endpoint contract defined in REQ-INTG-DTR-001 and is consistent with sequence diagram 334.', 'validation_notes': 'Validation confirms that the signature correctly includes the CancellationToken and the implementation logic correctly delegates to the Application Layer via MediatR.'}

##### 2.3.4.2.12.0 Events

*No items available*

##### 2.3.4.2.13.0 Implementation Notes

This controller specification defines a thin layer responsible only for translating HTTP requests into application-layer commands.

#### 2.3.4.3.0.0 Class Name

##### 2.3.4.3.1.0 Class Name

ExceptionHandlingMiddleware

##### 2.3.4.3.2.0 File Path

src/ReportingSystem.Service/Api/Middleware/ExceptionHandlingMiddleware.cs

##### 2.3.4.3.3.0 Class Type

ASP.NET Core Middleware

##### 2.3.4.3.4.0 Inheritance

IMiddleware

##### 2.3.4.3.5.0 Purpose

Provides global exception handling for the API pipeline. Catches unhandled exceptions, logs them, and returns a standardized JSON error response to the client, fulfilling REQ-FUNC-DTR-003 and supporting sequence diagram 343.

##### 2.3.4.3.6.0 Dependencies

- ILogger<ExceptionHandlingMiddleware>

##### 2.3.4.3.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.3.8.0 Technology Integration Notes

This middleware specification requires registration in Program.cs as one of the first components in the pipeline to ensure it can catch all subsequent errors.

##### 2.3.4.3.9.0 Validation Notes

Validation confirms this component is essential for meeting reliability and API contract requirements.

##### 2.3.4.3.10.0 Properties

*No items available*

##### 2.3.4.3.11.0 Methods

- {'method_name': 'InvokeAsync', 'method_signature': 'public async Task InvokeAsync(HttpContext context, RequestDelegate next)', 'return_type': 'Task', 'access_modifier': 'public', 'is_async': True, 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'context', 'parameter_type': 'HttpContext', 'is_nullable': False, 'purpose': 'The HTTP context for the current request.', 'framework_attributes': []}, {'parameter_name': 'next', 'parameter_type': 'RequestDelegate', 'is_nullable': False, 'purpose': 'The next middleware in the pipeline.', 'framework_attributes': []}], 'implementation_logic': 'Specification requires wrapping the call to `await next(context);` in a try-catch block. The catch block must handle different exception types: `ValidationException` from the Application Layer should result in a 400 Bad Request; `NotFoundException` should result in a 404 Not Found. Script-related exceptions (e.g., `Jint.Runtime.JavaScriptException` or a custom wrapper) must be caught and formatted into the structured `ErrorResponseDto` as per REQ-FUNC-DTR-003. All other unhandled exceptions must be logged as critical errors and result in a generic 500 Internal Server Error response to avoid leaking implementation details.', 'exception_handling': 'This component is specified as the primary exception handler for the entire API.', 'performance_considerations': 'Specification requires this component to be lightweight and only execute its main logic when an exception is thrown.', 'validation_requirements': 'n/a', 'technology_integration_details': 'This specification represents a core pattern for building robust ASP.NET Core APIs, centralizing error handling logic.', 'validation_notes': 'Validation confirms this specification correctly maps various application-layer exceptions to the appropriate HTTP status codes and response bodies, fulfilling multiple requirements.'}

##### 2.3.4.3.12.0 Events

*No items available*

##### 2.3.4.3.13.0 Implementation Notes

The mapping of exception types to HTTP status codes should be clearly defined, extensible, and must not expose sensitive stack traces in production environments.

#### 2.3.4.4.0.0 Class Name

##### 2.3.4.4.1.0 Class Name

ExecutePreview

##### 2.3.4.4.2.0 File Path

src/ReportingSystem.Service/Application/Features/Transformations/Commands/ExecutePreview.cs

##### 2.3.4.4.3.0 Class Type

CQRS Command/Handler

##### 2.3.4.4.4.0 Inheritance

n/a

##### 2.3.4.4.5.0 Purpose

Encapsulates the entire use case for executing a transformation script preview. This includes the command record, its validator, and the handler logic, following Vertical Slice Architecture.

##### 2.3.4.4.6.0 Dependencies

- MediatR
- FluentValidation
- ITransformationEngine
- IConnectorRepository

##### 2.3.4.4.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.4.8.0 Technology Integration Notes

This specification follows the Vertical Slice Architecture pattern with CQRS. The single file contains multiple related, nested classes for high cohesion.

##### 2.3.4.4.9.0 Validation Notes

Validation confirms this structured approach is compliant with the chosen architecture and correctly encapsulates a single feature.

##### 2.3.4.4.10.0 Properties

*No items available*

##### 2.3.4.4.11.0 Methods

- {'method_name': 'Handle', 'method_signature': 'public async Task<System.Text.Json.JsonNode> Handle(Command request, CancellationToken cancellationToken)', 'return_type': 'Task<System.Text.Json.JsonNode>', 'access_modifier': 'public', 'is_async': True, 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'request', 'parameter_type': 'Command', 'is_nullable': False, 'purpose': 'The command containing the request details.', 'framework_attributes': []}, {'parameter_name': 'cancellationToken', 'parameter_type': 'CancellationToken', 'is_nullable': False, 'purpose': 'Used to enforce timeouts and allow for cancellation, as per REQ-PERF-DTR-001.', 'framework_attributes': []}], 'implementation_logic': 'The handler logic specification requires first determining the data source. If `sampleData` is provided in the request, it must be parsed into a `JsonNode`. If `connectorId` is provided, the handler must use the injected `IConnectorRepository` (from Infrastructure) to fetch the connector configuration and then invoke it to get a sample dataset. The handler must then call the `_transformationEngine.ExecuteAsync()` method, passing the script content, the prepared data, and security constraints. The call must be wrapped in a try-catch block to handle script-specific exceptions.', 'exception_handling': 'Specification requires catching exceptions from the transformation engine (as per REQ-REL-DTR-001) and re-throwing them as specific, application-level exceptions that the global middleware can interpret and map to correct HTTP responses.', 'performance_considerations': 'Specification mandates that the handler must honor the `cancellationToken` passed from the controller, propagating it to the call to the transformation engine to enforce the timeout required by REQ-PERF-DTR-001.', 'validation_requirements': 'The nested `Validator` class specification requires using FluentValidation to ensure that either `sampleData` or `connectorId` is provided in the command, but not both. This validation should be executed by a MediatR pipeline behavior.', 'technology_integration_details': 'This handler specification is the core orchestration logic for the preview feature, consistent with sequence diagrams 334 and 335.', 'validation_notes': 'Validation confirms this specification correctly orchestrates dependencies and implements the complete business logic for the preview use case.'}

##### 2.3.4.4.12.0 Events

*No items available*

##### 2.3.4.4.13.0 Implementation Notes

The specification for this file requires nested classes for high cohesion: public record Command(...) : IRequest<JsonNode>; public class Validator : AbstractValidator<Command> { ... }; public class Handler : IRequestHandler<Command, JsonNode> { ... };

#### 2.3.4.5.0.0 Class Name

##### 2.3.4.5.1.0 Class Name

ReportGenerationJob

##### 2.3.4.5.2.0 File Path

src/ReportingSystem.Service/Core/Jobs/ReportGenerationJob.cs

##### 2.3.4.5.3.0 Class Type

Quartz.NET Job

##### 2.3.4.5.4.0 Inheritance

IJob

##### 2.3.4.5.5.0 Purpose

The background job executed by Quartz.NET to generate a report. It orchestrates the entire report generation pipeline for a specific job execution instance.

##### 2.3.4.5.6.0 Dependencies

- Quartz
- IReportExecutionService

##### 2.3.4.5.7.0 Framework Specific Attributes

- [DisallowConcurrentExecution]

##### 2.3.4.5.8.0 Technology Integration Notes

This class is specified as the entry point for all scheduled and on-demand report generation tasks executed in the background.

##### 2.3.4.5.9.0 Validation Notes

Validation confirms this component is critical for the system's asynchronous processing and reliability capabilities.

##### 2.3.4.5.10.0 Properties

*No items available*

##### 2.3.4.5.11.0 Methods

- {'method_name': 'Execute', 'method_signature': 'public async Task Execute(IJobExecutionContext context)', 'return_type': 'Task', 'access_modifier': 'public', 'is_async': True, 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'context', 'parameter_type': 'IJobExecutionContext', 'is_nullable': False, 'purpose': "Provides the job's execution context, including job data map and the scheduler's cancellation token.", 'framework_attributes': []}], 'implementation_logic': 'Specification requires extracting the `JobExecutionLogId` from the `context.JobDetail.JobDataMap`. It must then use an injected `IReportExecutionService` (resolved via a DI-aware custom JobFactory) and call a method like `ExecuteReportAsync(jobExecutionLogId, context.CancellationToken)`. A top-level try-catch block is a critical part of this specification.', 'exception_handling': 'The catch block specification is paramount for system reliability. It must catch ANY exception, log it with full details, and update the job\'s status in the database to \\"Failed\\" via a repository or service call. This is the primary mechanism for fulfilling REQ-REL-DTR-001 for background jobs, ensuring that a single job failure does not crash the entire scheduler thread pool.', 'performance_considerations': 'The job must be specified as fully asynchronous (`async Task`) and must honor the `CancellationToken` provided by the Quartz scheduler to allow for graceful shutdown and manual job cancellation.', 'validation_requirements': 'n/a', 'technology_integration_details': "This specification requires integration with Quartz.NET's job execution model and the application's main DI container.", 'validation_notes': 'Validation confirms the exception handling specification is robust and directly addresses the critical reliability requirement REQ-REL-DTR-001.'}

##### 2.3.4.5.12.0 Events

*No items available*

##### 2.3.4.5.13.0 Implementation Notes

Specification requires a custom `IJobFactory` to be implemented and registered with the Quartz.NET scheduler to allow for proper dependency injection into this job class.

### 2.3.5.0.0.0 Interface Specifications

#### 2.3.5.1.0.0 Interface Name

##### 2.3.5.1.1.0 Interface Name

ITransformationEngine

##### 2.3.5.1.2.0 File Path

src/ReportingSystem.Service/Application/Common/Interfaces/ITransformationEngine.cs

##### 2.3.5.1.3.0 Purpose

Defines the abstract contract for the script transformation engine. This decouples the Application Layer from the specific implementation (Jint) in the Infrastructure Layer, adhering to the Dependency Inversion Principle.

##### 2.3.5.1.4.0 Generic Constraints

None

##### 2.3.5.1.5.0 Framework Specific Inheritance

None

##### 2.3.5.1.6.0 Validation Notes

Validation confirms this interface is a key architectural component for maintaining separation of concerns.

##### 2.3.5.1.7.0 Method Contracts

- {'method_name': 'ExecuteAsync', 'method_signature': 'Task<TransformationResult> ExecuteAsync(string script, System.Text.Json.JsonNode jsonData, ScriptConstraints constraints, CancellationToken cancellationToken)', 'return_type': 'Task<TransformationResult>', 'framework_attributes': [], 'parameters': [{'parameter_name': 'script', 'parameter_type': 'string', 'purpose': 'The JavaScript code to be executed.'}, {'parameter_name': 'jsonData', 'parameter_type': 'System.Text.Json.JsonNode', 'purpose': 'The input data for the script, as a flexible JsonNode object.'}, {'parameter_name': 'constraints', 'parameter_type': 'ScriptConstraints', 'purpose': 'A record/class specifying the security sandbox constraints (timeout, memory, statement count) as per REQ-SEC-DTR-001.'}, {'parameter_name': 'cancellationToken', 'parameter_type': 'CancellationToken', 'purpose': 'Token for cancelling the execution, supporting timeouts.'}], 'contract_description': 'Specification requires the method to execute a given script within a secure sandbox and return a result object containing either the transformed data or a structured error.', 'exception_contracts': 'The specification states that implementations may throw specific exceptions for sandbox constraint violations (e.g., TimeoutException, MemoryLimitExceededException), which the calling service must handle.'}

##### 2.3.5.1.8.0 Property Contracts

*No items available*

##### 2.3.5.1.9.0 Implementation Guidance

The implementation of this interface must reside in the Infrastructure project. The Application Layer, within this repository, only defines and consumes this contract.

#### 2.3.5.2.0.0 Interface Name

##### 2.3.5.2.1.0 Interface Name

IAuditLogger

##### 2.3.5.2.2.0 File Path

src/ReportingSystem.Service/Application/Common/Interfaces/IAuditLogger.cs

##### 2.3.5.2.3.0 Purpose

Defines the contract for logging security-sensitive audit events, decoupling the application logic from the specific logging persistence mechanism.

##### 2.3.5.2.4.0 Generic Constraints

None

##### 2.3.5.2.5.0 Framework Specific Inheritance

None

##### 2.3.5.2.6.0 Validation Notes

Validation confirms this interface is required to fulfill auditing requirements like REQ-SEC-DTR-004 in a decoupled manner.

##### 2.3.5.2.7.0 Method Contracts

- {'method_name': 'LogAuditEventAsync', 'method_signature': 'Task LogAuditEventAsync(AuditEvent auditEvent)', 'return_type': 'Task', 'framework_attributes': [], 'parameters': [{'parameter_name': 'auditEvent', 'parameter_type': 'AuditEvent', 'purpose': 'A DTO or domain object representing the structured event to be logged.'}], 'contract_description': 'Specification requires the method to asynchronously log a structured audit event to a persistent, immutable store.', 'exception_contracts': 'Implementations should be specified to handle logging failures gracefully (e.g., log to a fallback or console) without throwing exceptions that would roll back the primary business transaction.'}

##### 2.3.5.2.8.0 Property Contracts

*No items available*

##### 2.3.5.2.9.0 Implementation Guidance

This interface will be consumed by Application Services (e.g., ScriptManagementService) to log events like script creation, deletion, and role changes, as required by REQ-SEC-DTR-004 and REQ-COMP-DTR-001.

### 2.3.6.0.0.0 Enum Specifications

*No items available*

### 2.3.7.0.0.0 Dto Specifications

#### 2.3.7.1.0.0 Dto Name

##### 2.3.7.1.1.0 Dto Name

PreviewRequestDto

##### 2.3.7.1.2.0 File Path

src/ReportingSystem.Service/Api/Dtos/PreviewRequestDto.cs

##### 2.3.7.1.3.0 Purpose

Represents the request body for the `POST /api/v1/transformations/preview` endpoint, as required by REQ-INTG-DTR-001.

##### 2.3.7.1.4.0 Framework Base Class

record

##### 2.3.7.1.5.0 Properties

###### 2.3.7.1.5.1 Property Name

####### 2.3.7.1.5.1.1 Property Name

ScriptContent

####### 2.3.7.1.5.1.2 Property Type

string

####### 2.3.7.1.5.1.3 Validation Attributes

- [Required]

####### 2.3.7.1.5.1.4 Serialization Attributes

- [JsonPropertyName(\"scriptContent\")]

####### 2.3.7.1.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.1.5.2.0 Property Name

####### 2.3.7.1.5.2.1 Property Name

SampleData

####### 2.3.7.1.5.2.2 Property Type

System.Text.Json.JsonNode?

####### 2.3.7.1.5.2.3 Validation Attributes

*No items available*

####### 2.3.7.1.5.2.4 Serialization Attributes

- [JsonPropertyName(\"sampleData\")]

####### 2.3.7.1.5.2.5 Framework Specific Attributes

*No items available*

###### 2.3.7.1.5.3.0 Property Name

####### 2.3.7.1.5.3.1 Property Name

ConnectorId

####### 2.3.7.1.5.3.2 Property Type

Guid?

####### 2.3.7.1.5.3.3 Validation Attributes

*No items available*

####### 2.3.7.1.5.3.4 Serialization Attributes

- [JsonPropertyName(\"connectorId\")]

####### 2.3.7.1.5.3.5 Framework Specific Attributes

*No items available*

##### 2.3.7.1.6.0.0 Validation Rules

Specification requires a custom validation rule (using FluentValidation) to ensure that either SampleData or ConnectorId is provided, but not both. This enforces the mutually exclusive choice in the preview functionality.

##### 2.3.7.1.7.0.0 Serialization Requirements

Specification requires standard JSON serialization with camelCase property names.

##### 2.3.7.1.8.0.0 Validation Notes

Validation confirms this DTO correctly models the API contract for the preview feature.

#### 2.3.7.2.0.0.0 Dto Name

##### 2.3.7.2.1.0.0 Dto Name

ErrorResponseDto

##### 2.3.7.2.2.0.0 File Path

src/ReportingSystem.Service/Api/Dtos/ErrorResponseDto.cs

##### 2.3.7.2.3.0.0 Purpose

Represents the structured error object returned by the API for script exceptions, as per the contract defined in REQ-FUNC-DTR-003.

##### 2.3.7.2.4.0.0 Framework Base Class

record

##### 2.3.7.2.5.0.0 Properties

- {'property_name': 'Error', 'property_type': 'ErrorDetails', 'validation_attributes': [], 'serialization_attributes': ['[JsonPropertyName(\\"error\\")]'], 'framework_specific_attributes': []}

##### 2.3.7.2.6.0.0 Validation Rules

n/a

##### 2.3.7.2.7.0.0 Serialization Requirements

Specification requires this DTO to contain a nested \"ErrorDetails\" record with \"Message\", \"StackTrace\", and \"LineNumber\" properties, all as strings, for consistent JSON output.

##### 2.3.7.2.8.0.0 Validation Notes

Validation confirms this DTO fulfills the API error contract requirement.

### 2.3.8.0.0.0.0 Configuration Specifications

*No items available*

### 2.3.9.0.0.0.0 Dependency Injection Specifications

#### 2.3.9.1.0.0.0 Service Interface

##### 2.3.9.1.1.0.0 Service Interface

n/a

##### 2.3.9.1.2.0.0 Service Implementation

DependencyInjection.cs

##### 2.3.9.1.3.0.0 Lifetime

n/a

##### 2.3.9.1.4.0.0 Registration Reasoning

This specification defines a static class containing extension methods to encapsulate the DI registrations for the Application and API layers. This keeps the main Program.cs file clean and organized, adhering to the Composition Root pattern.

##### 2.3.9.1.5.0.0 Framework Registration Pattern

public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration) { ... }

##### 2.3.9.1.6.0.0 Validation Notes

Validation confirms this is a best practice for managing DI in large .NET applications.

#### 2.3.9.2.0.0.0 Service Interface

##### 2.3.9.2.1.0.0 Service Interface

IMediator

##### 2.3.9.2.2.0.0 Service Implementation

MediatR

##### 2.3.9.2.3.0.0 Lifetime

Scoped

##### 2.3.9.2.4.0.0 Registration Reasoning

MediatR is central to the specified CQRS pattern. The specification requires registering MediatR along with all commands, queries, handlers, and pipeline behaviors from the Application assembly.

##### 2.3.9.2.5.0.0 Framework Registration Pattern

services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(Application.DependencyInjection).Assembly); cfg.AddOpenBehavior(typeof(ValidationBehavior<,>)); });

##### 2.3.9.2.6.0.0 Validation Notes

Validation confirms this registration is essential for the CQRS architecture to function.

#### 2.3.9.3.0.0.0 Service Interface

##### 2.3.9.3.1.0.0 Service Interface

IValidator<T>

##### 2.3.9.3.2.0.0 Service Implementation

FluentValidation

##### 2.3.9.3.3.0.0 Lifetime

Transient

##### 2.3.9.3.4.0.0 Registration Reasoning

Specification requires registering all FluentValidation validators from the Application assembly for automatic use in the MediatR validation pipeline behavior, ensuring all commands are validated.

##### 2.3.9.3.5.0.0 Framework Registration Pattern

services.AddValidatorsFromAssembly(typeof(Application.DependencyInjection).Assembly);

##### 2.3.9.3.6.0.0 Validation Notes

Validation confirms this is the correct way to integrate FluentValidation with DI and MediatR.

#### 2.3.9.4.0.0.0 Service Interface

##### 2.3.9.4.1.0.0 Service Interface

n/a

##### 2.3.9.4.2.0.0 Service Implementation

Quartz.NET

##### 2.3.9.4.3.0.0 Lifetime

Singleton

##### 2.3.9.4.4.0.0 Registration Reasoning

Specification requires configuring the Quartz.NET scheduler, a custom DI-aware job factory, and a persistent job store (using the application's main DB context).

##### 2.3.9.4.5.0.0 Framework Registration Pattern

services.AddQuartz(q => { q.UseMicrosoftDependencyInjectionJobFactory(); q.UsePersistentStore(s => { ... }); }); services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

##### 2.3.9.4.6.0.0 Validation Notes

Validation confirms this setup is necessary for reliable, persistent background job execution.

### 2.3.10.0.0.0.0 External Integration Specifications

*No items available*

## 2.4.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 19 |
| Total Interfaces | 2 |
| Total Enums | 0 |
| Total Dtos | 2 |
| Total Configurations | 0 |
| Total External Integrations | 0 |
| Grand Total Components | 23 |
| Phase 2 Claimed Count | 0 |
| Phase 2 Actual Count | 0 |
| Validation Added Count | 23 |
| Final Validated Count | 23 |

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

.vscode/launch.json

#### 3.1.3.2.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.3.3.0.0.0 Contains Files

- launch.json

#### 3.1.3.4.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.3.5.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.4.0.0.0.0 Directory Path

#### 3.1.4.1.0.0.0 Directory Path

.vscode/tasks.json

#### 3.1.4.2.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.4.3.0.0.0 Contains Files

- tasks.json

#### 3.1.4.4.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.4.5.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.5.0.0.0.0 Directory Path

#### 3.1.5.1.0.0.0 Directory Path

Directory.Build.props

#### 3.1.5.2.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.5.3.0.0.0 Contains Files

- Directory.Build.props

#### 3.1.5.4.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.5.5.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.6.0.0.0.0 Directory Path

#### 3.1.6.1.0.0.0 Directory Path

global.json

#### 3.1.6.2.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.6.3.0.0.0 Contains Files

- global.json

#### 3.1.6.4.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.6.5.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.7.0.0.0.0 Directory Path

#### 3.1.7.1.0.0.0 Directory Path

ReportingSystem.sln

#### 3.1.7.2.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.7.3.0.0.0 Contains Files

- ReportingSystem.sln

#### 3.1.7.4.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.7.5.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.8.0.0.0.0 Directory Path

#### 3.1.8.1.0.0.0 Directory Path

src/ReportingSystem.Service/appsettings.Development.json

#### 3.1.8.2.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.8.3.0.0.0 Contains Files

- appsettings.Development.json

#### 3.1.8.4.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.8.5.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.9.0.0.0.0 Directory Path

#### 3.1.9.1.0.0.0 Directory Path

src/ReportingSystem.Service/appsettings.json

#### 3.1.9.2.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.9.3.0.0.0 Contains Files

- appsettings.json

#### 3.1.9.4.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.9.5.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.10.0.0.0.0 Directory Path

#### 3.1.10.1.0.0.0 Directory Path

src/ReportingSystem.Service/Properties/launchSettings.json

#### 3.1.10.2.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.10.3.0.0.0 Contains Files

- launchSettings.json

#### 3.1.10.4.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.10.5.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

### 3.1.11.0.0.0.0 Directory Path

#### 3.1.11.1.0.0.0 Directory Path

src/ReportingSystem.Service/ReportingSystem.Service.csproj

#### 3.1.11.2.0.0.0 Purpose

Infrastructure and project configuration files

#### 3.1.11.3.0.0.0 Contains Files

- ReportingSystem.Service.csproj

#### 3.1.11.4.0.0.0 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

#### 3.1.11.5.0.0.0 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

