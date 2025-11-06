# 1 Id

REPO-08-SERVICE-HOST

# 2 Name

ReportingSystem.Service

# 3 Description

This is the main executable repository that acts as the composition root for the entire backend application. It is responsible for hosting the Windows Service, running the ASP.NET Core web server for the API and web UI, and configuring the dependency injection container to wire together all the components from the other libraries (Core.Domain, Infrastructure, etc.). It contains the application layer logic, including services that orchestrate the business workflows (e.g., `ReportExecutionService`), API controllers that handle HTTP requests, the Quartz.NET scheduler setup, and the dynamic loading mechanism for plugins. This repository represents the deployable 'Modular Monolith' artifact, referencing all other backend libraries to build the final product.

# 4 Type

🔹 Business Logic

# 5 Namespace

ReportingSystem.Service

# 6 Output Path

src/app/ReportingSystem.Service

# 7 Framework

.NET 8

# 8 Language

C#

# 9 Technology

ASP.NET Core, Windows Services, Quartz.NET

# 10 Thirdparty Libraries

- Microsoft.AspNetCore
- Quartz

# 11 Layer Ids

- application-layer
- presentation-layer-api

# 12 Dependencies

- REPO-02-CORE-DOMAIN
- REPO-03-SHARED-CONTRACTS
- REPO-04-SHARED-COMMON
- REPO-05-INFRASTRUCTURE
- REPO-06-PLUGINS-SDK

# 13 Requirements

*No items available*

# 14 Generate Tests

✅ Yes

# 15 Generate Documentation

✅ Yes

# 16 Architecture Style

Modular Monolith

# 17 Architecture Map

- api-gateway
- job-scheduler

# 18 Components Map

- transformations-controller-001
- scripts-controller-002
- transformation-service-001
- script-management-service-002

# 19 Requirements Map

- REQ-INTG-DTR-001
- 4. Overall Description

# 20 Decomposition Rationale

## 20.1 Operation Type

UNCHANGED_CORE

## 20.2 Source Repository

REPO-01-MONO

## 20.3 Decomposition Reasoning

This repository remains as the central orchestrator and deployable unit, fulfilling the 'Monolith' part of the Modular Monolith architecture. While its internal components have been extracted into focused libraries, this host is essential for composing them into a functional application. It is responsible for all runtime concerns like hosting, configuration, and scheduling.

## 20.4 Extracted Responsibilities

*No items available*

## 20.5 Reusability Scope

- This component is the final product and is not designed to be reused as a library.

## 20.6 Development Benefits

- Provides a single entry point for running and debugging the entire backend.
- Simplifies the deployment process as it produces a single, self-contained artifact.

# 21.0 Dependency Contracts

## 21.1 Repo-05-Infrastructure

### 21.1.1 Required Interfaces

- {'interface': 'IReportConfigurationRepository', 'methods': ['Consumes the concrete implementation of the repository.'], 'events': [], 'properties': []}

### 21.1.2 Integration Pattern

Dependency Injection.

### 21.1.3 Communication Protocol

In-process method calls.

# 22.0.0 Exposed Contracts

## 22.1.0 Public Interfaces

- {'interface': 'ReportsController (API Endpoint)', 'methods': ['GET /api/v1/reports/{id}', 'POST /api/v1/reports'], 'events': [], 'properties': [], 'consumers': ['REPO-09-WEB-UI']}

# 23.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | Acts as the composition root, configuring the DI c... |
| Event Communication | Publishes domain events to an in-process mediator ... |
| Data Flow | Orchestrates the flow of data from API requests th... |
| Error Handling | Contains global exception handling middleware for ... |
| Async Patterns | Manages asynchronous background jobs via the Quart... |

# 24.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Contains all ASP.NET Core-specific code like contr... |
| Performance Considerations | API endpoint performance is critical. Caching stra... |
| Security Considerations | Implements authentication and authorization middle... |
| Testing Approach | Requires end-to-end integration tests that spin up... |

# 25.0.0 Scope Boundaries

## 25.1.0 Must Implement

- API controllers.
- Application services that orchestrate business workflows.
- Program entry point and host configuration (Windows Service, Kestrel).
- DI container setup.

## 25.2.0 Must Not Implement

- Direct database access (should use repositories from Infrastructure).
- Core domain logic (should be in Core.Domain).

## 25.3.0 Extension Points

- New API endpoints and application services can be added.

## 25.4.0 Validation Rules

- Handles validation of incoming API request DTOs.

