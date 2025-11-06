# 1 Style

ModularMonolith

# 2 Patterns

## 2.1 CleanArchitecture

### 2.1.1 Name

CleanArchitecture

### 2.1.2 Description

The backend is structured using Clean Architecture principles to enforce separation of concerns. Dependencies point inwards: API/Web -> Application -> Domain. The Infrastructure layer implements interfaces defined in the Application layer, inverting the dependency flow for external concerns like the database and the Jint engine.

### 2.1.3 Benefits

- High cohesion and low coupling between components.
- Enhanced testability, as business logic is independent of UI and database.
- Improved maintainability and flexibility to change external components (e.g., database, logging framework).
- Directly supports maintainability and reliability quality attributes.

### 2.1.4 Tradeoffs

- Slightly higher initial setup complexity compared to a simple layered architecture.
- Requires discipline to maintain dependency rules.

### 2.1.5 Applicability

#### 2.1.5.1 Scenarios

- Enterprise applications with moderate to high complexity.
- Systems where long-term maintainability and testability are high priorities.
- Projects that need to be adaptable to changing technologies in the infrastructure layer.

#### 2.1.5.2 Constraints

- May be overkill for very simple CRUD-only applications or short-lived projects.

## 2.2.0.0 Repository

### 2.2.1.0 Name

Repository

### 2.2.2.0 Description

Data access is abstracted using the Repository pattern. Interfaces for data operations (e.g., `ITransformationScriptRepository`) are defined in the Application or Domain layer, with implementations using Entity Framework Core in the Infrastructure layer.

### 2.2.3.0 Benefits

- Decouples business logic from data access technology.
- Centralizes data access logic, making it easier to manage and optimize.
- Simplifies unit testing of application services by allowing repositories to be mocked.

### 2.2.4.0 Tradeoffs

- Can add an extra layer of abstraction that might not be necessary for simple queries.

### 2.2.5.0 Applicability

#### 2.2.5.1 Scenarios

- Applications requiring a clear separation between domain logic and data persistence.
- Systems that may need to support different database backends in the future.

#### 2.2.5.2 Constraints

- Using a generic repository can sometimes hide the complexity of the underlying data access framework (e.g., EF Core's change tracking).

# 3.0.0.0 Layers

## 3.1.0.0 Presentation

### 3.1.1.0 Id

presentation_layer_react

### 3.1.2.0 Name

Presentation Layer (Control Panel UI)

### 3.1.3.0 Description

A Single Page Application (SPA) that provides the user interface for managing and testing transformation scripts. It communicates with the backend via a RESTful API. Fulfills REQ-UI-DTR-001, REQ-UI-DTR-002, REQ-UI-DTR-003.

### 3.1.4.0 Technologystack

React 18, TypeScript 5.4, Monaco Editor, Axios

### 3.1.5.0 Language

TypeScript

### 3.1.6.0 Type

🔹 Presentation

### 3.1.7.0 Responsibilities

- Rendering the user interface for script CRUD operations, version history, and report associations.
- Embedding the Monaco Editor for script editing with syntax highlighting and validation.
- Providing the three-pane layout for script editing, sample data input, and result/error output.
- Calling the backend API to fetch data, save scripts, and run transformation previews.
- Displaying performance warnings and internationalized error messages.

### 3.1.8.0 Components

- ScriptEditorComponent (using Monaco)
- ScriptListComponent
- VersionHistoryComponent (with diff viewer)
- PreviewPanelComponent
- ReportAssociationManagerComponent

### 3.1.9.0 Interfaces

*No items available*

### 3.1.10.0 Dependencies

- {'layerId': 'api_web_layer', 'type': 'Required'}

### 3.1.11.0 Constraints

- {'type': 'Framework', 'description': 'Must use the React framework and Monaco Editor component as per REQ-UI-DTR-002.'}

## 3.2.0.0 APIGateway

### 3.2.1.0 Id

api_web_layer

### 3.2.2.0 Name

API/Web Layer

### 3.2.3.0 Description

The entry point for all client requests. It exposes RESTful endpoints, handles HTTP-specific concerns, and translates requests into calls to the Application Layer. Implements REQ-INTG-DTR-001.

### 3.2.4.0 Technologystack

ASP.NET Core 8

### 3.2.5.0 Language

C#

### 3.2.6.0 Type

🔹 APIGateway

### 3.2.7.0 Responsibilities

- Defining and routing RESTful API endpoints (e.g., `/api/v1/transformations/preview`).
- Handling request/response serialization/deserialization (JSON).
- Implementing authentication (JWT validation) and authorization (RBAC policies).
- Validating incoming Data Transfer Objects (DTOs).
- Orchestrating calls to the Application layer use cases.

### 3.2.8.0 Components

- TransformationsController
- ScriptsController
- AuthenticationMiddleware
- GlobalExceptionHandlingMiddleware
- RequestValidationFilter (using FluentValidation)

### 3.2.9.0 Interfaces

*No items available*

### 3.2.10.0 Dependencies

- {'layerId': 'application_layer', 'type': 'Required'}

### 3.2.11.0 Constraints

*No items available*

## 3.3.0.0 ApplicationServices

### 3.3.1.0 Id

application_layer

### 3.3.2.0 Name

Application Layer

### 3.3.3.0 Description

Contains the application-specific business logic and use cases. It orchestrates the domain objects and directs the infrastructure layer to perform tasks. This layer is independent of UI and Infrastructure.

### 3.3.4.0 Technologystack

.NET 8

### 3.3.5.0 Language

C#

### 3.3.6.0 Type

🔹 ApplicationServices

### 3.3.7.0 Responsibilities

- Executing core use cases like creating a script, running a transformation preview, or associating a script with a report.
- Defining interfaces for infrastructure dependencies (e.g., `ITransformationEngine`, `IScriptRepository`, `IAuditLogger`).
- Handling transaction management across multiple domain entities.
- Coordinating domain logic and infrastructure services to fulfill a request.

### 3.3.8.0 Components

- TransformationService
- ScriptManagementService
- UserManagementService
- ReportConfigurationService

### 3.3.9.0 Interfaces

#### 3.3.9.1 Interface

##### 3.3.9.1.1 Name

ITransformationEngine

##### 3.3.9.1.2 Type

🔹 Interface

##### 3.3.9.1.3 Operations

- ExecuteAsync(script, jsonData, constraints)

##### 3.3.9.1.4 Visibility

Public

#### 3.3.9.2.0 Interface

##### 3.3.9.2.1 Name

IScriptRepository

##### 3.3.9.2.2 Type

🔹 Interface

##### 3.3.9.2.3 Operations

- GetByIdAsync(id)
- AddAsync(script)
- UpdateAsync(script)

##### 3.3.9.2.4 Visibility

Public

#### 3.3.9.3.0 Interface

##### 3.3.9.3.1 Name

IAuditLogger

##### 3.3.9.3.2 Type

🔹 Interface

##### 3.3.9.3.3 Operations

- LogSecurityEventAsync(eventDetails)

##### 3.3.9.3.4 Visibility

Public

### 3.3.10.0.0 Dependencies

- {'layerId': 'domain_layer', 'type': 'Required'}

### 3.3.11.0.0 Constraints

- {'type': 'Dependency', 'description': 'Must not contain any dependencies on the API/Web or Infrastructure layers.'}

## 3.4.0.0.0 Domain

### 3.4.1.0.0 Id

domain_layer

### 3.4.2.0.0 Name

Domain Layer

### 3.4.3.0.0 Description

The core of the application, containing business entities, value objects, and domain-specific logic. It has no dependencies on any other layer.

### 3.4.4.0.0 Technologystack

.NET 8

### 3.4.5.0.0 Language

C#

### 3.4.6.0.0 Type

🔹 Domain

### 3.4.7.0.0 Responsibilities

- Defining the state and behavior of core business entities (TransformationScript, ReportJob, User).
- Encapsulating business rules that are independent of application use cases.
- Containing domain events that can be raised when entity state changes.

### 3.4.8.0.0 Components

- TransformationScript Entity
- TransformationScriptVersion Entity
- ReportConfiguration Entity
- ReportJob Entity
- AuditLog Entity
- User Entity
- Role Entity

### 3.4.9.0.0 Interfaces

*No items available*

### 3.4.10.0.0 Dependencies

*No items available*

### 3.4.11.0.0 Constraints

- {'type': 'Dependency', 'description': 'Must not have any dependencies on external frameworks or libraries (e.g., EF Core, ASP.NET Core).'}

## 3.5.0.0.0 Infrastructure

### 3.5.1.0.0 Id

infrastructure_layer

### 3.5.2.0.0 Name

Infrastructure Layer

### 3.5.3.0.0 Description

Implements interfaces defined in the Application layer and handles all interactions with external systems and frameworks. This includes database access, the Jint engine, logging, and monitoring.

### 3.5.4.0.0 Technologystack

.NET 8, Entity Framework Core 8, Jint, Serilog, prometheus-net, Npgsql, Redis

### 3.5.5.0.0 Language

C#

### 3.5.6.0.0 Type

🔹 Infrastructure

### 3.5.7.0.0 Responsibilities

- Implementing repository interfaces for data access using Entity Framework Core.
- Implementing the `ITransformationEngine` interface by wrapping the Jint library and applying security sandbox constraints (REQ-SEC-DTR-001).
- Handling database connections, migrations, and encryption at rest.
- Implementing the `IAuditLogger` to write to the AuditLog table.
- Exposing Prometheus metrics for monitoring (REQ-OPER-DTR-001).
- Managing distributed caching with Redis for configuration and roles.

### 3.5.8.0.0 Components

- JintTransformationEngine
- EfCoreScriptRepository
- EfCoreAuditLogRepository
- PostgresDbContext (EF Core)
- SerilogLoggingProvider
- PrometheusMetricsExporter
- RedisCacheService

### 3.5.9.0.0 Interfaces

*No items available*

### 3.5.10.0.0 Dependencies

- {'layerId': 'application_layer', 'type': 'Required'}

### 3.5.11.0.0 Constraints

- {'type': 'Implementation', 'description': 'Must use Jint for JavaScript execution (REQ-FUNC-DTR-001) and System.Text.Json for JSON handling (REQ-DATA-DTR-001).'}

# 4.0.0.0.0 Quality Attributes

## 4.1.0.0.0 Performance

### 4.1.1.0.0 Tactics

- Asynchronous execution of all I/O bound operations (`async`/`await`).
- Distributed caching (Redis) for frequently accessed, rarely changed data like application configuration and user roles.
- Database indexing as defined in the provided database design.
- Use of a Materialized View (`ReportingDashboardMV`) to pre-aggregate data for reporting dashboards.
- Connection pooling managed by Entity Framework Core.
- Enforcing server-side timeouts on long-running operations like script previews (REQ-PERF-DTR-001).

### 4.1.2.0.0 Metrics

- Process a 10MB JSON dataset with a 200-statement script in under 10 seconds (REQ-PERF-DTR-002).
- Preview API endpoint timeout of 30 seconds (REQ-PERF-DTR-001).

## 4.2.0.0.0 Scalability

### 4.2.1.0.0 Tactics

- Stateless API design to allow for horizontal scaling via container orchestration (e.g., Kubernetes).
- Centralized distributed cache (Redis) to handle shared state across multiple instances.

### 4.2.2.0.0 Approach

Horizontal

## 4.3.0.0.0 Security

| Property | Value |
|----------|-------|
| Authentication | Stateless JWT Bearer token authentication for all ... |
| Authorization | Role-Based Access Control (RBAC) implemented using... |
| Data Protection | Encryption at rest for transformation scripts in t... |

## 4.4.0.0.0 Reliability

### 4.4.1.0.0 Tactics

- Isolation of Jint script execution within `try-catch` blocks to ensure script failures do not crash the main application service (REQ-REL-DTR-001).
- Graceful handling of timeouts using `CancellationToken`.
- Structured logging to capture detailed error information for diagnostics.
- Inclusion of all script data in standard database backup procedures (REQ-OPER-DTR-002).

## 4.5.0.0.0 Maintainability

### 4.5.1.0.0 Tactics

- Strict adherence to Clean Architecture principles to ensure separation of concerns.
- Use of Dependency Injection throughout the application to promote loose coupling.
- Comprehensive unit, integration, and end-to-end test suites.
- Adherence to OpenAPI 3.0 specification for API documentation (REQ-QUAL-DTR-001).

## 4.6.0.0.0 Extensibility

### 4.6.1.0.0 Tactics

- Use of interfaces for all infrastructure dependencies, allowing for alternative implementations to be easily substituted.

# 5.0.0.0.0 Technology Stack

## 5.1.0.0.0 Primary Language

C# 12

## 5.2.0.0.0 Frameworks

- .NET 8
- ASP.NET Core 8
- Entity Framework Core 8
- React 18

## 5.3.0.0.0 Database

| Property | Value |
|----------|-------|
| Type | PostgreSQL |
| Version | 16 |
| Orm | Entity Framework Core 8 |

## 5.4.0.0.0 Domain Specific Libraries

### 5.4.1.0.0 Jint

#### 5.4.1.1.0 Name

Jint

#### 5.4.1.2.0 Version

3.1.2+

#### 5.4.1.3.0 Purpose

Provides the core JavaScript execution engine as required by REQ-FUNC-DTR-001.

#### 5.4.1.4.0 Domain

Data Transformation

### 5.4.2.0.0 JsonSchema.Net

#### 5.4.2.1.0 Name

JsonSchema.Net

#### 5.4.2.2.0 Version

5.5.1+

#### 5.4.2.3.0 Purpose

Provides JSON Schema validation capabilities to fulfill REQ-FUNC-DTR-006.

#### 5.4.2.4.0 Domain

Data Validation

### 5.4.3.0.0 Monaco Editor

#### 5.4.3.1.0 Name

Monaco Editor

#### 5.4.3.2.0 Version

0.45.0+

#### 5.4.3.3.0 Purpose

Provides the rich code editing experience in the frontend as required by REQ-UI-DTR-002.

#### 5.4.3.4.0 Domain

User Interface

## 5.5.0.0.0 Infrastructure

| Property | Value |
|----------|-------|
| Logging | Serilog |
| Caching | Redis |
| Testing | xUnit, Moq, Playwright (for E2E tests) |

# 6.0.0.0.0 Backend Services

## 6.1.0.0.0 Data transformation

### 6.1.1.0.0 Name

JintTransformationEngine

### 6.1.2.0.0 Purpose

Encapsulates all logic for executing user-provided JavaScript via the Jint library. It is responsible for creating the sandboxed engine instance, applying all security constraints (timeout, memory, statement count, no CLR access), injecting the JSON data, executing the script, and returning the result or a structured error.

### 6.1.3.0.0 Type

🔹 Data transformation

### 6.1.4.0.0 Communication

Invoked by Application Layer services via the ITransformationEngine interface using Dependency Injection.

## 6.2.0.0.0 Auditing

### 6.2.1.0.0 Name

SecurityAuditService

### 6.2.2.0.0 Purpose

Provides a centralized service for logging all security-relevant events, such as script CRUD operations, sandbox violations, and permission changes, to an immutable audit trail as required by REQ-SEC-DTR-002, REQ-SEC-DTR-004, and REQ-COMP-DTR-001.

### 6.2.3.0.0 Type

🔹 Auditing

### 6.2.4.0.0 Communication

Invoked by Application Layer services via the IAuditLogger interface.

# 7.0.0.0.0 Cross Cutting Concerns

| Property | Value |
|----------|-------|
| Logging | Implemented using Serilog, configured via Dependen... |
| Exception Handling | A global exception handling middleware in the ASP.... |
| Configuration | Managed via ASP.NET Core's IConfiguration, with a ... |
| Validation | Handled using the FluentValidation library. Valida... |
| Security | Authentication is handled by JWT bearer middleware... |

