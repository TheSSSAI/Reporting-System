# 1 Components

## 1.1 Components

### 1.1.1 Controller

#### 1.1.1.1 Id

transformations-controller-001

#### 1.1.1.2 Name

TransformationsController

#### 1.1.1.3 Description

API Gateway Layer. Exposes RESTful endpoints for executing script previews and managing report jobs that use transformations. Handles HTTP request/response cycles, DTO validation, and delegates business logic to the Application Layer. Fulfills REQ-INTG-DTR-001.

#### 1.1.1.4 Type

🔹 Controller

#### 1.1.1.5 Dependencies

- transformation-service-001

#### 1.1.1.6 Properties

| Property | Value |
|----------|-------|
| Layer | API/Web Layer |
| Route Prefix | /api/v1/transformations |

#### 1.1.1.7 Interfaces

*No items available*

#### 1.1.1.8 Technology

ASP.NET Core 8

#### 1.1.1.9 Resources

##### 1.1.1.9.1 Cpu

0.25 cores

##### 1.1.1.9.2 Memory

256MB

#### 1.1.1.10.0 Configuration

*No data available*

#### 1.1.1.11.0 Health Check

*Not specified*

#### 1.1.1.12.0 Responsible Features

- REQ-INTG-DTR-001
- REQ-FUNC-DTR-002

#### 1.1.1.13.0 Security

##### 1.1.1.13.1 Requires Authentication

✅ Yes

##### 1.1.1.13.2 Requires Authorization

✅ Yes

##### 1.1.1.13.3 Allowed Roles

- Administrator
- ScriptManager

### 1.1.2.0.0 Controller

#### 1.1.2.1.0 Id

scripts-controller-002

#### 1.1.2.2.0 Name

ScriptsController

#### 1.1.2.3.0 Description

API Gateway Layer. Provides full CRUD (Create, Read, Update, Delete) RESTful endpoints for managing transformation scripts and their versions, including operations for viewing history and reverting. Fulfills REQ-FUNC-DTR-004, REQ-FUNC-DTR-005.

#### 1.1.2.4.0 Type

🔹 Controller

#### 1.1.2.5.0 Dependencies

- script-management-service-002

#### 1.1.2.6.0 Properties

| Property | Value |
|----------|-------|
| Layer | API/Web Layer |
| Route Prefix | /api/v1/scripts |

#### 1.1.2.7.0 Interfaces

*No items available*

#### 1.1.2.8.0 Technology

ASP.NET Core 8

#### 1.1.2.9.0 Resources

##### 1.1.2.9.1 Cpu

0.25 cores

##### 1.1.2.9.2 Memory

256MB

#### 1.1.2.10.0 Configuration

*No data available*

#### 1.1.2.11.0 Health Check

*Not specified*

#### 1.1.2.12.0 Responsible Features

- REQ-FUNC-DTR-004
- REQ-FUNC-DTR-005

#### 1.1.2.13.0 Security

##### 1.1.2.13.1 Requires Authentication

✅ Yes

##### 1.1.2.13.2 Requires Authorization

✅ Yes

##### 1.1.2.13.3 Allowed Roles

- Administrator
- ScriptManager

### 1.1.3.0.0 Service

#### 1.1.3.1.0 Id

transformation-service-001

#### 1.1.3.2.0 Name

TransformationService

#### 1.1.3.3.0 Description

Application Layer. Orchestrates the process of executing a transformation. It retrieves the script, validates the input JSON size against configured limits, invokes the transformation engine, and handles post-execution validation against a JSON schema if provided. Fulfills REQ-DATA-DTR-001, REQ-PERF-DTR-001, REQ-BR-DTR-001, REQ-FUNC-DTR-006.

#### 1.1.3.4.0 Type

🔹 Service

#### 1.1.3.5.0 Dependencies

- jint-transformation-engine-003
- ef-core-script-repository-004
- ef-core-audit-logger-005

#### 1.1.3.6.0 Properties

| Property | Value |
|----------|-------|
| Layer | Application Layer |

#### 1.1.3.7.0 Interfaces

- ITransformationService

#### 1.1.3.8.0 Technology

.NET 8

#### 1.1.3.9.0 Resources

##### 1.1.3.9.1 Cpu

0.5 cores

##### 1.1.3.9.2 Memory

512MB

#### 1.1.3.10.0 Configuration

##### 1.1.3.10.1 Preview Timeout Seconds

30

##### 1.1.3.10.2 Max Dataset Size Mb

256

#### 1.1.3.11.0 Health Check

*Not specified*

#### 1.1.3.12.0 Responsible Features

- REQ-FUNC-DTR-002
- REQ-PERF-DTR-001
- REQ-BR-DTR-001
- REQ-FUNC-DTR-006
- REQ-DATA-DTR-001

#### 1.1.3.13.0 Security

##### 1.1.3.13.1 Requires Authentication

❌ No

##### 1.1.3.13.2 Requires Authorization

❌ No

### 1.1.4.0.0 Service

#### 1.1.4.1.0 Id

script-management-service-002

#### 1.1.4.2.0 Name

ScriptManagementService

#### 1.1.4.3.0 Description

Application Layer. Encapsulates all business logic for managing the lifecycle of transformation scripts, including creation, updating, versioning, and deletion. Interacts with the data layer via repository interfaces and logs all changes to the audit trail. Fulfills REQ-FUNC-DTR-004, REQ-FUNC-DTR-005.

#### 1.1.4.4.0 Type

🔹 Service

#### 1.1.4.5.0 Dependencies

- ef-core-script-repository-004
- ef-core-audit-logger-005

#### 1.1.4.6.0 Properties

| Property | Value |
|----------|-------|
| Layer | Application Layer |

#### 1.1.4.7.0 Interfaces

- IScriptManagementService

#### 1.1.4.8.0 Technology

.NET 8

#### 1.1.4.9.0 Resources

##### 1.1.4.9.1 Cpu

0.25 cores

##### 1.1.4.9.2 Memory

256MB

#### 1.1.4.10.0 Configuration

*No data available*

#### 1.1.4.11.0 Health Check

*Not specified*

#### 1.1.4.12.0 Responsible Features

- REQ-FUNC-DTR-004
- REQ-FUNC-DTR-005
- REQ-SEC-DTR-004

#### 1.1.4.13.0 Security

##### 1.1.4.13.1 Requires Authentication

❌ No

##### 1.1.4.13.2 Requires Authorization

❌ No

### 1.1.5.0.0 Engine

#### 1.1.5.1.0 Id

jint-transformation-engine-003

#### 1.1.5.2.0 Name

JintTransformationEngine

#### 1.1.5.3.0 Description

Infrastructure Layer. Implements the ITransformationEngine interface. This component is a wrapper around the Jint library, responsible for creating a secure, sandboxed JavaScript execution environment. It applies configurable constraints (timeout, memory, recursion, statement count), disables CLR access, executes scripts, and catches script exceptions. Fulfills REQ-FUNC-DTR-001, REQ-SEC-DTR-001, REQ-REL-DTR-001, REQ-PERF-DTR-002.

#### 1.1.5.4.0 Type

🔹 Engine

#### 1.1.5.5.0 Dependencies

- ef-core-audit-logger-005

#### 1.1.5.6.0 Properties

| Property | Value |
|----------|-------|
| Layer | Infrastructure Layer |

#### 1.1.5.7.0 Interfaces

- ITransformationEngine

#### 1.1.5.8.0 Technology

Jint

#### 1.1.5.9.0 Resources

##### 1.1.5.9.1 Cpu

2 cores (per execution)

##### 1.1.5.9.2 Memory

Configurable limit (e.g., 512MB per execution)

#### 1.1.5.10.0 Configuration

| Property | Value |
|----------|-------|
| Execution Timeout | Configurable (e.g., 5s) |
| Max Statements | Configurable (e.g., 10000) |
| Memory Limit Mb | Configurable (e.g., 256MB) |

#### 1.1.5.11.0 Health Check

*Not specified*

#### 1.1.5.12.0 Responsible Features

- REQ-FUNC-DTR-001
- REQ-SEC-DTR-001
- REQ-SEC-DTR-002
- REQ-FUNC-DTR-003
- REQ-PERF-DTR-002
- REQ-REL-DTR-001

#### 1.1.5.13.0 Security

##### 1.1.5.13.1 Requires Authentication

❌ No

##### 1.1.5.13.2 Requires Authorization

❌ No

### 1.1.6.0.0 Repository

#### 1.1.6.1.0 Id

ef-core-script-repository-004

#### 1.1.6.2.0 Name

EfCoreScriptRepository

#### 1.1.6.3.0 Description

Infrastructure Layer. Implements the IScriptRepository interface using Entity Framework Core and PostgreSQL. Manages all data access operations for the TransformationScript and TransformationScriptVersion entities, including soft deletes and version history queries. Relies on database-level encryption to fulfill REQ-SEC-DTR-003.

#### 1.1.6.4.0 Type

🔹 Repository

#### 1.1.6.5.0 Dependencies

*No items available*

#### 1.1.6.6.0 Properties

| Property | Value |
|----------|-------|
| Layer | Infrastructure Layer |
| Db Context | PostgresDbContext |

#### 1.1.6.7.0 Interfaces

- IScriptRepository

#### 1.1.6.8.0 Technology

Entity Framework Core 8

#### 1.1.6.9.0 Resources

##### 1.1.6.9.1 Cpu

Shared with application

##### 1.1.6.9.2 Memory

Shared with application

#### 1.1.6.10.0 Configuration

*No data available*

#### 1.1.6.11.0 Health Check

*Not specified*

#### 1.1.6.12.0 Responsible Features

- REQ-FUNC-DTR-004
- REQ-FUNC-DTR-005
- REQ-SEC-DTR-003

#### 1.1.6.13.0 Security

##### 1.1.6.13.1 Requires Authentication

❌ No

##### 1.1.6.13.2 Requires Authorization

❌ No

### 1.1.7.0.0 Repository

#### 1.1.7.1.0 Id

ef-core-audit-logger-005

#### 1.1.7.2.0 Name

EfCoreAuditLogger

#### 1.1.7.3.0 Description

Infrastructure Layer. Implements the IAuditLogger interface. Provides a concrete implementation for writing security-relevant events (script changes, sandbox violations) to the immutable AuditLog table in the database. Fulfills REQ-SEC-DTR-002, REQ-SEC-DTR-004, REQ-COMP-DTR-001.

#### 1.1.7.4.0 Type

🔹 Repository

#### 1.1.7.5.0 Dependencies

*No items available*

#### 1.1.7.6.0 Properties

| Property | Value |
|----------|-------|
| Layer | Infrastructure Layer |
| Db Context | PostgresDbContext |

#### 1.1.7.7.0 Interfaces

- IAuditLogger

#### 1.1.7.8.0 Technology

Entity Framework Core 8

#### 1.1.7.9.0 Resources

##### 1.1.7.9.1 Cpu

Shared with application

##### 1.1.7.9.2 Memory

Shared with application

#### 1.1.7.10.0 Configuration

##### 1.1.7.10.1 Log Retention Days

365

#### 1.1.7.11.0 Health Check

*Not specified*

#### 1.1.7.12.0 Responsible Features

- REQ-SEC-DTR-002
- REQ-SEC-DTR-004
- REQ-COMP-DTR-001
- REQ-SEC-DTR-002

#### 1.1.7.13.0 Security

##### 1.1.7.13.1 Requires Authentication

❌ No

##### 1.1.7.13.2 Requires Authorization

❌ No

### 1.1.8.0.0 Middleware

#### 1.1.8.1.0 Id

global-exception-middleware-006

#### 1.1.8.2.0 Name

GlobalExceptionHandlingMiddleware

#### 1.1.8.3.0 Description

API/Web Layer. A cross-cutting concern component in the ASP.NET Core pipeline that intercepts all unhandled exceptions. It logs the full error details and transforms the exception into a structured, internationalized JSON error response for the client. Fulfills REQ-FUNC-DTR-003 and supports REQ-REL-DTR-001.

#### 1.1.8.4.0 Type

🔹 Middleware

#### 1.1.8.5.0 Dependencies

*No items available*

#### 1.1.8.6.0 Properties

| Property | Value |
|----------|-------|
| Layer | API/Web Layer |

#### 1.1.8.7.0 Interfaces

*No items available*

#### 1.1.8.8.0 Technology

ASP.NET Core 8

#### 1.1.8.9.0 Resources

##### 1.1.8.9.1 Cpu

Minimal

##### 1.1.8.9.2 Memory

Minimal

#### 1.1.8.10.0 Configuration

*No data available*

#### 1.1.8.11.0 Health Check

*Not specified*

#### 1.1.8.12.0 Responsible Features

- REQ-FUNC-DTR-003
- REQ-REL-DTR-001
- REQ-QUAL-DTR-002

#### 1.1.8.13.0 Security

##### 1.1.8.13.1 Requires Authentication

❌ No

##### 1.1.8.13.2 Requires Authorization

❌ No

### 1.1.9.0.0 Monitoring

#### 1.1.9.1.0 Id

prometheus-metrics-exporter-007

#### 1.1.9.2.0 Name

PrometheusMetricsExporter

#### 1.1.9.3.0 Description

Infrastructure Layer. A component that instruments the application to collect key metrics (e.g., transformation execution duration, failure rates, API endpoint latency) and exposes them via a Prometheus-compatible `/metrics` endpoint for monitoring. Fulfills REQ-OPER-DTR-001.

#### 1.1.9.4.0 Type

🔹 Monitoring

#### 1.1.9.5.0 Dependencies

*No items available*

#### 1.1.9.6.0 Properties

| Property | Value |
|----------|-------|
| Layer | Infrastructure Layer |

#### 1.1.9.7.0 Interfaces

*No items available*

#### 1.1.9.8.0 Technology

prometheus-net

#### 1.1.9.9.0 Resources

##### 1.1.9.9.1 Cpu

Minimal

##### 1.1.9.9.2 Memory

Minimal

#### 1.1.9.10.0 Configuration

*No data available*

#### 1.1.9.11.0 Health Check

*Not specified*

#### 1.1.9.12.0 Responsible Features

- REQ-OPER-DTR-001

#### 1.1.9.13.0 Security

##### 1.1.9.13.1 Requires Authentication

❌ No

##### 1.1.9.13.2 Requires Authorization

❌ No

### 1.1.10.0.0 UI Component

#### 1.1.10.1.0 Id

script-editor-view-008

#### 1.1.10.2.0 Name

ScriptEditorView

#### 1.1.10.3.0 Description

Presentation Layer (UI). The main React container component for the script management page. It orchestrates the three-pane layout for the script editor, sample JSON input, and transformed JSON output/error view. It also displays the persistent performance warning. Fulfills REQ-UI-DTR-003, REQ-UI-DTR-001.

#### 1.1.10.4.0 Type

🔹 UI Component

#### 1.1.10.5.0 Dependencies

- monaco-editor-component-009
- api-client-010

#### 1.1.10.6.0 Properties

| Property | Value |
|----------|-------|
| Layer | Presentation Layer |

#### 1.1.10.7.0 Interfaces

*No items available*

#### 1.1.10.8.0 Technology

React

#### 1.1.10.9.0 Resources

*No data available*

#### 1.1.10.10.0 Configuration

*No data available*

#### 1.1.10.11.0 Health Check

*Not specified*

#### 1.1.10.12.0 Responsible Features

- REQ-UI-DTR-001
- REQ-UI-DTR-003
- REQ-FUNC-DTR-002

#### 1.1.10.13.0 Security

*No data available*

### 1.1.11.0.0 UI Component

#### 1.1.11.1.0 Id

monaco-editor-component-009

#### 1.1.11.2.0 Name

MonacoEditorComponent

#### 1.1.11.3.0 Description

Presentation Layer (UI). A specialized React component that wraps and configures the Monaco Editor. It provides the core editing experience with JavaScript syntax highlighting, code completion, and real-time validation. Fulfills REQ-UI-DTR-002.

#### 1.1.11.4.0 Type

🔹 UI Component

#### 1.1.11.5.0 Dependencies

*No items available*

#### 1.1.11.6.0 Properties

| Property | Value |
|----------|-------|
| Layer | Presentation Layer |

#### 1.1.11.7.0 Interfaces

*No items available*

#### 1.1.11.8.0 Technology

Monaco Editor, React

#### 1.1.11.9.0 Resources

*No data available*

#### 1.1.11.10.0 Configuration

*No data available*

#### 1.1.11.11.0 Health Check

*Not specified*

#### 1.1.11.12.0 Responsible Features

- REQ-UI-DTR-002

#### 1.1.11.13.0 Security

*No data available*

### 1.1.12.0.0 Service

#### 1.1.12.1.0 Id

api-client-010

#### 1.1.12.2.0 Name

ApiClient

#### 1.1.12.3.0 Description

Presentation Layer (UI). A TypeScript module that centralizes all communication with the backend REST API. It uses a library like Axios to handle HTTP requests, attach JWT authentication headers, and manage API-level error handling.

#### 1.1.12.4.0 Type

🔹 Service

#### 1.1.12.5.0 Dependencies

*No items available*

#### 1.1.12.6.0 Properties

| Property | Value |
|----------|-------|
| Layer | Presentation Layer |

#### 1.1.12.7.0 Interfaces

*No items available*

#### 1.1.12.8.0 Technology

Axios, TypeScript

#### 1.1.12.9.0 Resources

*No data available*

#### 1.1.12.10.0 Configuration

##### 1.1.12.10.1 Api Base Url

/api/v1

#### 1.1.12.11.0 Health Check

*Not specified*

#### 1.1.12.12.0 Responsible Features

- REQ-INTG-DTR-001
- REQ-FUNC-DTR-004
- REQ-FUNC-DTR-005

#### 1.1.12.13.0 Security

*No data available*

## 1.2.0.0.0 Configuration

### 1.2.1.0.0 Environment

production

### 1.2.2.0.0 Logging Level

Information

### 1.2.3.0.0 Feature Flags

#### 1.2.3.1.0 Data Transformation Engine

true (controlled by REQ-OPER-IMP-001)

### 1.2.4.0.0 Cache Ttl

600s

### 1.2.5.0.0 Max Threads

100

# 2.0.0.0.0 Component Relations

## 2.1.0.0.0 Architecture

### 2.1.1.0.0 Components

#### 2.1.1.1.0 Infrastructure Service

##### 2.1.1.1.1 Id

transformation-engine-001

##### 2.1.1.1.2 Name

JintTransformationEngine

##### 2.1.1.1.3 Description

Implements the ITransformationEngine interface. Encapsulates the Jint library to execute JavaScript transformation scripts within a secure, configurable sandbox. It is responsible for applying constraints like execution timeout, memory limits, and statement counts, and for disabling CLR access.

##### 2.1.1.1.4 Type

🔹 Infrastructure Service

##### 2.1.1.1.5 Dependencies

- audit-logger-001
- configuration-service-001

##### 2.1.1.1.6 Properties

| Property | Value |
|----------|-------|
| Jint Version | 3.1.2+ |
| Es Version | ES6 |

##### 2.1.1.1.7 Interfaces

- ITransformationEngine

##### 2.1.1.1.8 Technology

Jint

##### 2.1.1.1.9 Resources

###### 2.1.1.1.9.1 Cpu

Variable (per job)

###### 2.1.1.1.9.2 Memory

Variable (per job, constrained)

##### 2.1.1.1.10.0 Configuration

| Property | Value |
|----------|-------|
| Default Timeout Ms | 5000 |
| Default Max Statements | 10000 |
| Default Memory Limit Mb | 128 |

##### 2.1.1.1.11.0 Health Check

*Not specified*

##### 2.1.1.1.12.0 Responsible Features

- Data Transformation Engine
- Security

##### 2.1.1.1.13.0 Security

| Property | Value |
|----------|-------|
| Requires Authentication | ❌ |
| Requires Authorization | ❌ |
| Notes | Executes in a highly restricted sandbox. All secur... |

#### 2.1.1.2.0.0 API Controller

##### 2.1.1.2.1.0 Id

transformations-controller-001

##### 2.1.1.2.2.0 Name

TransformationsController

##### 2.1.1.2.3.0 Description

API Layer component that exposes the RESTful endpoint for script previewing (`POST /api/v1/transformations/preview`). It handles request validation, deserialization, and orchestrates the call to the TransformationService.

##### 2.1.1.2.4.0 Type

🔹 API Controller

##### 2.1.1.2.5.0 Dependencies

- transformation-service-001

##### 2.1.1.2.6.0 Properties

| Property | Value |
|----------|-------|
| Route Prefix | /api/v1/transformations |

##### 2.1.1.2.7.0 Interfaces

*No items available*

##### 2.1.1.2.8.0 Technology

ASP.NET Core 8

##### 2.1.1.2.9.0 Resources

| Property | Value |
|----------|-------|
| Cpu | 0.25 cores |
| Memory | 128MB |
| Network | 100Mbps |

##### 2.1.1.2.10.0 Configuration

*Not specified*

##### 2.1.1.2.11.0 Health Check

| Property | Value |
|----------|-------|
| Path | /healthz |
| Interval | 30 |
| Timeout | 5 |

##### 2.1.1.2.12.0 Responsible Features

- Data Transformation Engine
- API

##### 2.1.1.2.13.0 Security

###### 2.1.1.2.13.1 Requires Authentication

✅ Yes

###### 2.1.1.2.13.2 Requires Authorization

✅ Yes

###### 2.1.1.2.13.3 Allowed Roles

- Administrator
- ScriptManager

#### 2.1.1.3.0.0 Application Service

##### 2.1.1.3.1.0 Id

transformation-service-001

##### 2.1.1.3.2.0 Name

TransformationService

##### 2.1.1.3.3.0 Description

Application Layer service responsible for the 'preview transformation' use case. It retrieves the script, orchestrates the JintTransformationEngine, enforces server-side timeouts via CancellationToken, and formats the response or error object.

##### 2.1.1.3.4.0 Type

🔹 Application Service

##### 2.1.1.3.5.0 Dependencies

- transformation-engine-001
- script-repository-001

##### 2.1.1.3.6.0 Properties

*No data available*

##### 2.1.1.3.7.0 Interfaces

*No items available*

##### 2.1.1.3.8.0 Technology

.NET 8

##### 2.1.1.3.9.0 Resources

###### 2.1.1.3.9.1 Cpu

0.1 cores

###### 2.1.1.3.9.2 Memory

64MB

##### 2.1.1.3.10.0 Configuration

###### 2.1.1.3.10.1 Default Preview Timeout Seconds

30

##### 2.1.1.3.11.0 Health Check

*Not specified*

##### 2.1.1.3.12.0 Responsible Features

- Data Transformation Engine
- Performance
- Error Handling

##### 2.1.1.3.13.0 Security

| Property | Value |
|----------|-------|
| Requires Authentication | ❌ |
| Requires Authorization | ❌ |
| Notes | Authorization is handled at the API Controller lay... |

#### 2.1.1.4.0.0 Repository

##### 2.1.1.4.1.0 Id

script-repository-001

##### 2.1.1.4.2.0 Name

ScriptRepository

##### 2.1.1.4.3.0 Description

Infrastructure component that implements the IScriptRepository interface. It handles all data access for TransformationScript and TransformationScriptVersion entities using Entity Framework Core. It is also responsible for transparently encrypting and decrypting script content before writing to and after reading from the database.

##### 2.1.1.4.4.0 Type

🔹 Repository

##### 2.1.1.4.5.0 Dependencies

- database-context-001

##### 2.1.1.4.6.0 Properties

| Property | Value |
|----------|-------|
| Orm | Entity Framework Core 8 |

##### 2.1.1.4.7.0 Interfaces

- IScriptRepository

##### 2.1.1.4.8.0 Technology

Entity Framework Core

##### 2.1.1.4.9.0 Resources

###### 2.1.1.4.9.1 Cpu

Shared

###### 2.1.1.4.9.2 Memory

Shared with DbContext

##### 2.1.1.4.10.0 Configuration

*Not specified*

##### 2.1.1.4.11.0 Health Check

*Not specified*

##### 2.1.1.4.12.0 Responsible Features

- Script Management
- Security

##### 2.1.1.4.13.0 Security

| Property | Value |
|----------|-------|
| Requires Authentication | ❌ |
| Requires Authorization | ❌ |
| Notes | Handles encryption at rest for script content (REQ... |

#### 2.1.1.5.0.0 Application Service

##### 2.1.1.5.1.0 Id

script-management-service-001

##### 2.1.1.5.2.0 Name

ScriptManagementService

##### 2.1.1.5.3.0 Description

Application Layer service that contains the business logic for all CRUD operations on transformation scripts, including versioning, viewing history, and reverting to previous versions. It uses the ScriptRepository for persistence and the AuditLogger for recording changes.

##### 2.1.1.5.4.0 Type

🔹 Application Service

##### 2.1.1.5.5.0 Dependencies

- script-repository-001
- audit-logger-001

##### 2.1.1.5.6.0 Properties

*No data available*

##### 2.1.1.5.7.0 Interfaces

*No items available*

##### 2.1.1.5.8.0 Technology

.NET 8

##### 2.1.1.5.9.0 Resources

###### 2.1.1.5.9.1 Cpu

0.1 cores

###### 2.1.1.5.9.2 Memory

64MB

##### 2.1.1.5.10.0 Configuration

*Not specified*

##### 2.1.1.5.11.0 Health Check

*Not specified*

##### 2.1.1.5.12.0 Responsible Features

- Script Management

##### 2.1.1.5.13.0 Security

###### 2.1.1.5.13.1 Requires Authentication

❌ No

###### 2.1.1.5.13.2 Requires Authorization

❌ No

#### 2.1.1.6.0.0 Infrastructure Service

##### 2.1.1.6.1.0 Id

audit-logger-001

##### 2.1.1.6.2.0 Name

AuditLogger

##### 2.1.1.6.3.0 Description

Infrastructure component that implements the IAuditLogger interface. It writes security-relevant events, such as script modifications or sandbox constraint violations, to an immutable audit trail in the database. Designed for compliance with SOC 2 / ISO 27001.

##### 2.1.1.6.4.0 Type

🔹 Infrastructure Service

##### 2.1.1.6.5.0 Dependencies

- database-context-001

##### 2.1.1.6.6.0 Properties

| Property | Value |
|----------|-------|
| Retention Period Days | 365 |

##### 2.1.1.6.7.0 Interfaces

- IAuditLogger

##### 2.1.1.6.8.0 Technology

Entity Framework Core, Serilog

##### 2.1.1.6.9.0 Resources

###### 2.1.1.6.9.1 Cpu

Shared

###### 2.1.1.6.9.2 Memory

Shared

##### 2.1.1.6.10.0 Configuration

*Not specified*

##### 2.1.1.6.11.0 Health Check

*Not specified*

##### 2.1.1.6.12.0 Responsible Features

- Security
- Compliance

##### 2.1.1.6.13.0 Security

| Property | Value |
|----------|-------|
| Requires Authentication | ❌ |
| Requires Authorization | ❌ |
| Notes | Core security component responsible for logging. |

#### 2.1.1.7.0.0 UI Component

##### 2.1.1.7.1.0 Id

script-editor-ui-001

##### 2.1.1.7.2.0 Name

ScriptEditorComponent

##### 2.1.1.7.3.0 Description

A frontend component built with React that embeds the Monaco Editor. It provides the main user interface for creating and editing JavaScript transformation scripts, offering syntax highlighting, code completion, and validation.

##### 2.1.1.7.4.0 Type

🔹 UI Component

##### 2.1.1.7.5.0 Dependencies

- api-client-001

##### 2.1.1.7.6.0 Properties

| Property | Value |
|----------|-------|
| Editor | Monaco Editor |

##### 2.1.1.7.7.0 Interfaces

*No items available*

##### 2.1.1.7.8.0 Technology

React, TypeScript, Monaco Editor

##### 2.1.1.7.9.0 Resources

###### 2.1.1.7.9.1 Cpu

Client-side

###### 2.1.1.7.9.2 Memory

Client-side

##### 2.1.1.7.10.0 Configuration

*Not specified*

##### 2.1.1.7.11.0 Health Check

*Not specified*

##### 2.1.1.7.12.0 Responsible Features

- Control Panel UI

##### 2.1.1.7.13.0 Security

###### 2.1.1.7.13.1 Requires Authentication

✅ Yes

###### 2.1.1.7.13.2 Requires Authorization

✅ Yes

#### 2.1.1.8.0.0 UI Component

##### 2.1.1.8.1.0 Id

preview-panel-ui-001

##### 2.1.1.8.2.0 Name

PreviewPanelComponent

##### 2.1.1.8.3.0 Description

A composite React UI component that provides the three-pane layout for the script editor, a sample JSON input text area, and a transformed JSON output/error view. It orchestrates the interaction between these panes and calls the preview API.

##### 2.1.1.8.4.0 Type

🔹 UI Component

##### 2.1.1.8.5.0 Dependencies

- script-editor-ui-001
- api-client-001

##### 2.1.1.8.6.0 Properties

*No data available*

##### 2.1.1.8.7.0 Interfaces

*No items available*

##### 2.1.1.8.8.0 Technology

React, TypeScript

##### 2.1.1.8.9.0 Resources

###### 2.1.1.8.9.1 Cpu

Client-side

###### 2.1.1.8.9.2 Memory

Client-side

##### 2.1.1.8.10.0 Configuration

*Not specified*

##### 2.1.1.8.11.0 Health Check

*Not specified*

##### 2.1.1.8.12.0 Responsible Features

- Control Panel UI
- Data Transformation Engine

##### 2.1.1.8.13.0 Security

###### 2.1.1.8.13.1 Requires Authentication

✅ Yes

###### 2.1.1.8.13.2 Requires Authorization

✅ Yes

#### 2.1.1.9.0.0 Infrastructure Service

##### 2.1.1.9.1.0 Id

configuration-service-001

##### 2.1.1.9.2.0 Name

ConfigurationService

##### 2.1.1.9.3.0 Description

Infrastructure component that provides a strongly-typed, cached interface to settings stored in the ApplicationConfiguration table. It is used to retrieve configurable values like sandbox limits, feature flags, and dataset size limits.

##### 2.1.1.9.4.0 Type

🔹 Infrastructure Service

##### 2.1.1.9.5.0 Dependencies

- database-context-001

##### 2.1.1.9.6.0 Properties

| Property | Value |
|----------|-------|
| Cache | In-memory with Redis backplane |

##### 2.1.1.9.7.0 Interfaces

- IConfigurationService

##### 2.1.1.9.8.0 Technology

.NET 8, Redis

##### 2.1.1.9.9.0 Resources

###### 2.1.1.9.9.1 Cpu

0.1 cores

###### 2.1.1.9.9.2 Memory

128MB

##### 2.1.1.9.10.0 Configuration

###### 2.1.1.9.10.1 Cache Ttl

300s

##### 2.1.1.9.11.0 Health Check

*Not specified*

##### 2.1.1.9.12.0 Responsible Features

- Security
- Deployment
- Business Rules

##### 2.1.1.9.13.0 Security

###### 2.1.1.9.13.1 Requires Authentication

❌ No

###### 2.1.1.9.13.2 Requires Authorization

❌ No

#### 2.1.1.10.0.0 Monitoring Agent

##### 2.1.1.10.1.0 Id

metrics-exporter-001

##### 2.1.1.10.2.0 Name

PrometheusMetricsExporter

##### 2.1.1.10.3.0 Description

A middleware component in the ASP.NET Core pipeline that collects and exposes key performance and health metrics (e.g., transformation execution time, error rates, job counts) via a Prometheus-compatible `/metrics` endpoint.

##### 2.1.1.10.4.0 Type

🔹 Monitoring Agent

##### 2.1.1.10.5.0 Dependencies

*No items available*

##### 2.1.1.10.6.0 Properties

*No data available*

##### 2.1.1.10.7.0 Interfaces

*No items available*

##### 2.1.1.10.8.0 Technology

prometheus-net

##### 2.1.1.10.9.0 Resources

###### 2.1.1.10.9.1 Cpu

0.05 cores

###### 2.1.1.10.9.2 Memory

32MB

##### 2.1.1.10.10.0 Configuration

*Not specified*

##### 2.1.1.10.11.0 Health Check

*Not specified*

##### 2.1.1.10.12.0 Responsible Features

- Monitoring

##### 2.1.1.10.13.0 Security

| Property | Value |
|----------|-------|
| Requires Authentication | ❌ |
| Requires Authorization | ❌ |
| Notes | Endpoint should be firewalled to be accessible onl... |

### 2.1.2.0.0.0 Configuration

| Property | Value |
|----------|-------|
| Environment | production |
| Logging Level | INFO |
| Notes | Component design adheres to the Modular Monolith a... |

