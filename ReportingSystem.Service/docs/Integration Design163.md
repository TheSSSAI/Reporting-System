# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-08-SERVICE-HOST |
| Extraction Timestamp | 2024-07-28T10:30:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | Fully Ready |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

REQ-INTG-DTR-001

#### 1.2.1.2 Requirement Text

The system shall expose a RESTful endpoint POST /api/v1/transformations/preview that adheres to the specified request body structure and HTTP response code behavior.

#### 1.2.1.3 Validation Criteria

- A POST request to /api/v1/transformations/preview with a valid script and sample data returns a 200 OK status and the transformed JSON.
- A request with a JavaScript syntax error returns a 400 Bad Request status.
- A request made by an unauthenticated or unauthorized user returns a 403 Forbidden status.

#### 1.2.1.4 Implementation Implications

- An ASP.NET Core Controller (e.g., TransformationsController) must be created within this repository.
- The controller must have an action method decorated with [HttpPost("/api/v1/transformations/preview")].
- The action method must handle Data Transfer Objects (DTOs) from REPO-03-SHARED-CONTRACTS.

#### 1.2.1.5 Extraction Reasoning

This requirement directly defines a key API endpoint that this repository is responsible for hosting and implementing, making it a central responsibility.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

REQ-SEC-DTR-004

#### 1.2.2.2 Requirement Text

Access to script CRUD operations and report associations shall be restricted via Role-Based Access Control (RBAC), and all such operations must be logged in an immutable audit trail.

#### 1.2.2.3 Validation Criteria

- A user without the required role (e.g., 'Administrator') receives a 'Forbidden' error when attempting to access script management APIs.

#### 1.2.2.4 Implementation Implications

- Authentication (JWT Bearer) and Authorization (Role-based) middleware must be configured in Program.cs.
- API controllers responsible for script management (e.g., ScriptsController) must be decorated with [Authorize(Roles = "Administrator")] attributes.

#### 1.2.2.5 Extraction Reasoning

This repository hosts the API Layer, which is the primary enforcement point for security concerns like authentication and authorization. The requirement directly dictates the implementation of security attributes on API controllers.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

REQ-PERF-DTR-001

#### 1.2.3.2 Requirement Text

The transformation preview API endpoint shall enforce a configurable, server-side execution timeout (default 30 seconds) using a CancellationToken, and all timeout events must be logged.

#### 1.2.3.3 Validation Criteria

- An API call to the preview endpoint with a script that runs longer than the configured timeout is terminated and returns a timeout error.

#### 1.2.3.4 Implementation Implications

- Controllers must create a CancellationTokenSource with the configured timeout for relevant requests.
- The CancellationToken must be propagated through the application services to the infrastructure layer.

#### 1.2.3.5 Extraction Reasoning

This requirement defines a critical performance and reliability pattern (timeout) that is initiated and managed by the API/Application layers hosted within this repository.

### 1.2.4.0 Requirement Id

#### 1.2.4.1 Requirement Id

US-042

#### 1.2.4.2 Requirement Text

As an IT Support user, I want to deploy a new custom data connector by simply copying its compiled DLL file and any dependencies into a designated 'plugins' folder on the application server.

#### 1.2.4.3 Validation Criteria

- The system automatically detects the new file, loads the assembly, registers the new connector, and makes it available for selection in the Control Panel.

#### 1.2.4.4 Implementation Implications

- This repository must implement a plugin loading mechanism, likely using .NET's AssemblyLoadContext, that scans a 'plugins' directory.
- The loader must be robust against faulty plugins and prevent them from crashing the main service.

#### 1.2.4.5 Extraction Reasoning

The service host is explicitly responsible for the dynamic loading of plugins, which is a key integration point for system extensibility.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

API Controllers

#### 1.3.1.2 Component Specification

A collection of ASP.NET Core Controllers that define the system's RESTful API endpoints. They are responsible for handling HTTP requests, validating input DTOs, enforcing authorization, and orchestrating calls to the Application Layer services. Examples include TransformationsController, ScriptsController, and AuthController.

#### 1.3.1.3 Implementation Requirements

- Must inherit from ControllerBase and be decorated with [ApiController].
- Must use Data Transfer Objects (DTOs) from REPO-03-SHARED-CONTRACTS for all request/response bodies.
- Must delegate all business logic to Application Layer services (e.g., via MediatR or direct service injection).
- Must be secured using [Authorize] attributes to enforce RBAC.

#### 1.3.1.4 Architectural Context

Belongs to the API/Web Layer. Acts as the entry point for all client requests.

#### 1.3.1.5 Extraction Reasoning

This is the primary component type for implementing the API surface, a core responsibility of the service host.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

Application Services

#### 1.3.2.2 Component Specification

A collection of C# classes that implement the core use cases and business workflows of the application. They orchestrate domain entities and consume interfaces implemented by the Infrastructure Layer. Examples include TransformationService, ScriptManagementService, and ReportExecutionService.

#### 1.3.2.3 Implementation Requirements

- Must be registered in the Dependency Injection container.
- Must depend on interfaces (e.g., ITransformationEngine, IReportRepository), not concrete implementations.
- Must contain logic for transaction management and orchestration of domain entities.
- Must propagate CancellationTokens for long-running operations.

#### 1.3.2.4 Architectural Context

Belongs to the Application Layer. This is the heart of the system's business logic orchestration.

#### 1.3.2.5 Extraction Reasoning

This component type is explicitly mentioned in the repository's description as part of its scope and implements the core application logic.

### 1.3.3.0 Component Name

#### 1.3.3.1 Component Name

Composition Root (Program.cs)

#### 1.3.3.2 Component Specification

The main entry point of the application. It is responsible for building and configuring the application host, including setting up the dependency injection container, configuring the ASP.NET Core middleware pipeline (auth, logging, exception handling), and initializing background services like the Quartz.NET scheduler.

#### 1.3.3.3 Implementation Requirements

- Must correctly register all services from all referenced projects (Application, Infrastructure, etc.).
- Must configure middleware in the correct order for the HTTP request pipeline.
- Must configure the application to run as a Windows Service.
- Must apply database migrations on startup.

#### 1.3.3.4 Architectural Context

Acts as the composition root for the entire modular monolith, wiring all dependencies together.

#### 1.3.3.5 Extraction Reasoning

This is the central configuration and startup component for the entire backend, a critical responsibility of this repository.

## 1.4.0.0 Architectural Layers

### 1.4.1.0 Layer Name

#### 1.4.1.1 Layer Name

API/Web Layer

#### 1.4.1.2 Layer Responsibilities

- Defining and routing all RESTful API endpoints for the system.
- Handling HTTP-specific concerns like request/response serialization (JSON).
- Implementing authentication (JWT validation) and authorization (RBAC policies) middleware.
- Validating incoming Data Transfer Objects (DTOs).
- Serving the static files for the frontend React application (REPO-09-WEB-UI).

#### 1.4.1.3 Layer Constraints

- Must not contain business logic; should delegate to the Application Layer.
- Must not interact directly with the database; should only call Application Services.

#### 1.4.1.4 Implementation Patterns

- Controller Pattern
- Middleware Pattern
- Data Transfer Object (DTO)

#### 1.4.1.5 Extraction Reasoning

The repository description explicitly states it 'hosts the ASP.NET Core web server for the API' and contains 'API controllers'. This layer is a primary responsibility of this repository.

### 1.4.2.0 Layer Name

#### 1.4.2.1 Layer Name

Application Layer

#### 1.4.2.2 Layer Responsibilities

- Executing core application use cases (e.g., 'Create a Script', 'Run a Transformation').
- Orchestrating domain objects and infrastructure services to fulfill requests.
- Defining interfaces for all infrastructure dependencies (e.g., IRepository, ITransformationEngine).
- Handling transaction boundaries for business operations.

#### 1.4.2.3 Layer Constraints

- Must be independent of UI and Infrastructure details (e.g., database type, logging framework).
- Must not have any dependencies on the API/Web or Infrastructure layers' concrete implementations.

#### 1.4.2.4 Implementation Patterns

- Service Layer
- Use Case/Interactor
- Dependency Inversion

#### 1.4.2.5 Extraction Reasoning

The repository description states it 'contains the application layer logic, including services that orchestrate the business workflows'. This layer contains the core logic that the API layer exposes.

## 1.5.0.0 Dependency Interfaces

### 1.5.1.0 Interface Name

#### 1.5.1.1 Interface Name

Infrastructure Layer Implementations

#### 1.5.1.2 Source Repository

REPO-05-INFRASTRUCTURE

#### 1.5.1.3 Method Contracts

- {'method_name': 'N/A', 'method_signature': 'N/A', 'method_purpose': "Provides concrete implementations for interfaces defined in this repository's Application Layer.", 'integration_context': 'At application startup, the Composition Root (Program.cs) in this repository registers implementations from REPO-05 (e.g., JintTransformationEngine, EfCoreScriptRepository) against interfaces defined here (e.g., ITransformationEngine, IScriptRepository).'}

#### 1.5.1.4 Integration Pattern

Dependency Injection / Inversion of Control

#### 1.5.1.5 Communication Protocol

In-Process Method Call

#### 1.5.1.6 Extraction Reasoning

This is the primary integration pattern of the Clean Architecture. This repository depends on the concrete classes from the Infrastructure layer at the composition root to fulfill its application layer's contracts.

### 1.5.2.0 Interface Name

#### 1.5.2.1 Interface Name

Domain Model & Core Contracts

#### 1.5.2.2 Source Repository

REPO-02-CORE-DOMAIN

#### 1.5.2.3 Method Contracts

- {'method_name': 'N/A', 'method_signature': 'N/A', 'method_purpose': 'Provides the core business entities, aggregates, and value objects that the Application Layer services orchestrate.', 'integration_context': 'Application services in this repository create, modify, and use domain entities (e.g., ReportConfiguration, User) to execute business logic.'}

#### 1.5.2.4 Integration Pattern

Project Reference

#### 1.5.2.5 Communication Protocol

In-Process Method Call

#### 1.5.2.6 Extraction Reasoning

As per Clean Architecture, the Application Layer (in this repository) directly depends on the Domain Layer to access the core business model.

### 1.5.3.0 Interface Name

#### 1.5.3.1 Interface Name

API Data Contracts (DTOs)

#### 1.5.3.2 Source Repository

REPO-03-SHARED-CONTRACTS

#### 1.5.3.3 Method Contracts

- {'method_name': 'N/A', 'method_signature': 'N/A', 'method_purpose': 'Provides the Data Transfer Objects (DTOs) that define the public contract of all RESTful API endpoints.', 'integration_context': 'All API Controllers in this repository use DTOs from REPO-03 as parameters for request bodies and as return types for response bodies.'}

#### 1.5.3.4 Integration Pattern

Project Reference

#### 1.5.3.5 Communication Protocol

In-Process Method Call

#### 1.5.3.6 Extraction Reasoning

This dependency is essential for decoupling the API's public contract from the internal domain model, a core tenet of the specified architecture.

### 1.5.4.0 Interface Name

#### 1.5.4.1 Interface Name

Plugin SDK Contracts

#### 1.5.4.2 Source Repository

REPO-06-PLUGINS-SDK

#### 1.5.4.3 Method Contracts

- {'method_name': 'IConnector', 'method_signature': 'Interface', 'method_purpose': 'Provides the IConnector interface contract, which the plugin loading mechanism in this repository uses to identify and interact with custom connectors.', 'integration_context': 'The plugin loader service, hosted in this repository, will use reflection to find types that implement the IConnector interface from dynamically loaded assemblies.'}

#### 1.5.4.4 Integration Pattern

Project Reference (for contract) & Dynamic Assembly Loading (for implementations)

#### 1.5.4.5 Communication Protocol

In-Process Method Call / Reflection

#### 1.5.4.6 Extraction Reasoning

To implement the plugin loading functionality (US-042), this repository must have a compile-time dependency on the SDK to understand the contract it is searching for.

## 1.6.0.0 Exposed Interfaces

- {'interface_name': 'Reporting System RESTful API', 'consumer_repositories': ['REPO-09-WEB-UI', 'External API Clients'], 'method_contracts': [{'method_name': 'POST /api/v1/auth/token', 'method_signature': 'Authenticates a user and returns a JWT.', 'method_purpose': 'To provide secure, token-based access to the API.', 'implementation_requirements': 'Implements US-087.'}, {'method_name': 'POST /api/v1/transformations/preview', 'method_signature': 'Receives a script and sample data, returns a result.', 'method_purpose': 'To provide real-time feedback on transformation script development.', 'implementation_requirements': 'Must enforce server-side timeout (REQ-PERF-DTR-001) and return structured errors (REQ-FUNC-DTR-003).'}, {'method_name': 'POST /api/v1/reports/{id}/generate', 'method_signature': 'Triggers on-demand report generation.', 'method_purpose': 'To enable programmatic and event-driven reporting.', 'implementation_requirements': 'Must support both synchronous (200 OK) and asynchronous (202 Accepted) workflows as per US-091 and US-094.'}, {'method_name': 'GET /api/v1/jobs/{id}/status', 'method_signature': 'Returns the status of an asynchronous job.', 'method_purpose': 'To allow clients to poll for the result of a long-running report.', 'implementation_requirements': 'Implements US-095.'}, {'method_name': 'CRUD Endpoints', 'method_signature': 'GET, POST, PUT, DELETE on /api/v1/{resource}', 'method_purpose': 'To provide full programmatic management of system entities like users, reports, connectors, and transformations.', 'implementation_requirements': 'Implements US-089 and must be secured via RBAC.'}], 'service_level_requirements': ['Must adhere to the 99.9% availability target as per REQ-OPER-DTR-003.', 'All endpoints must respond over HTTPS with a valid TLS certificate.'], 'implementation_constraints': ['Must be secured via JWT bearer authentication.', 'Must be documented via an OpenAPI 3.0 specification, generated automatically.', 'All error responses must be in a standardized JSON format.'], 'extraction_reasoning': 'This is the primary exposed interface of the entire backend system. This repository is solely responsible for hosting and defining this API, which is consumed by the frontend and external clients.'}

## 1.7.0.0 Technology Context

### 1.7.1.0 Framework Requirements

This repository is the host and composition root for a .NET 8 application. It uses ASP.NET Core 8 to host the API and web server, and is deployed as a Windows Service. It configures and runs the Quartz.NET scheduler for background jobs.

### 1.7.2.0 Integration Technologies

- ASP.NET Core Middleware for cross-cutting concerns (authentication, exception handling, logging).
- Quartz.NET for scheduling and running background jobs (report generation).
- Microsoft.Extensions.DependencyInjection for wiring up all application components.
- .NET's AssemblyLoadContext for dynamic loading of connector plugins.

### 1.7.3.0 Performance Constraints

API endpoints must be highly responsive. Application services must enforce timeouts on long-running operations like script previews. Background job processing must be isolated to not impact API responsiveness.

### 1.7.4.0 Security Requirements

Implements the primary security boundary for the system. Must configure and enforce JWT authentication and role-based authorization for all API endpoints. Must use a global exception handler to prevent leaking sensitive information in error responses.

## 1.8.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | The repository's integration points are fully mapp... |
| Cross Reference Validation | All extracted elements show strong consistency. Th... |
| Implementation Readiness Assessment | Readiness is high. The context provides clear, act... |
| Quality Assurance Confirmation | The systematic analysis confirmed that this reposi... |

