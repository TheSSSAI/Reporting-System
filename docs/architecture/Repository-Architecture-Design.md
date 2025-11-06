# Reporting System - Enterprise Architecture Documentation

## Executive Summary

This document outlines the comprehensive enterprise architecture for the Reporting System, an on-premise, self-hosted solution designed for automated data ingestion, transformation, and report generation. The system is architected as a **Hybrid, Modular Monolith**, deployed as a single Windows Service. This approach balances the operational simplicity of a monolithic deployment—critical for on-premise software—with the development scalability and maintainability of a modular design.

The core technology stack is centered around **.NET 8** for the backend and **React 18** for the web-based interfaces, with **PostgreSQL** serving as the robust configuration and metadata store and **Redis** for high-performance caching. The architecture is designed to ensure customer data privacy by performing all data processing entirely on the customer's local network, with a minimal, secure cloud connection used exclusively for license validation.

A key architectural tenet is **extensibility**, realized through a formal Plugin Development Kit (PDK) that allows System Integrators to develop custom data connectors. The strategic decomposition of the original single codebase into distinct, layered repositories enhances parallel development, enforces separation of concerns, and establishes clear ownership boundaries, delivering significant improvements in maintainability, testability, and development velocity.

## Solution Architecture Overview

The system's design follows the principles of **Clean Architecture** within a **Modular Monolith** pattern. This creates a clear separation between business logic, application logic, and infrastructure details.

- **Technology Stack**: The solution leverages a modern, high-performance stack validated for the system's concurrency and data volume requirements:
    - **Backend**: C# 12 on .NET 8, ASP.NET Core 8, Entity Framework Core 8
    - **Frontend**: React 18 with TypeScript, Vite, and MUI v5
    - **Database**: PostgreSQL 16 for primary storage, offering robustness and advanced monitoring capabilities.
    - **Caching**: Redis for distributed caching of user sessions, roles, and application configuration to ensure high performance.
    - **Job Scheduling**: Quartz.NET for managing concurrent, scheduled background report jobs.
    - **Data Transformation**: Jint, a secure, sandboxed JavaScript engine.

- **Architectural Patterns**:
    - **Modular Monolith**: The backend is composed of multiple, logically distinct libraries (Domain, Infrastructure, Shared) that are ultimately compiled and deployed as a single, self-contained Windows Service. This simplifies on-premise installation and management.
    - **Clean Architecture**: A strict dependency rule is enforced where inner layers (Domain) do not depend on outer layers (Infrastructure, UI). Dependencies flow inwards, ensuring the core business logic is independent of technology choices.
    - **Embedded API Gateway**: The ASP.NET Core host acts as a single entry point, handling authentication, authorization, routing, and other cross-cutting concerns for all API and web requests.
    - **Plug-in Architecture**: A formal SDK (`IConnector` interface) decouples the core application from custom data connectors, which are loaded dynamically at runtime.

- **Integration Approach**:
    - The React Single-Page Application (SPA) is fully decoupled from the backend, communicating exclusively via a versioned, secure RESTful API.
    - Internally, components are integrated via Dependency Injection, with the Service Host acting as the composition root.

## Repository Architecture Strategy

The initial monorepo was strategically decomposed into eight focused repositories to enhance the development lifecycle and enforce architectural boundaries. This transformation was driven by the need for clear separation of concerns, enabling parallel development, and establishing a formal contract for system extensibility.

- **Decomposition Rationale**: The primary goal was to align the code structure with the Clean Architecture principles. This involved isolating:
    1.  **Core Domain Logic**: Stable, technology-agnostic business rules and entities.
    2.  **Infrastructure Concerns**: Volatile, technology-specific implementations (database access, API clients).
    3.  **Presentation Layers**: The API and the UI, which have different lifecycles and technology stacks.
    4.  **Shared Contracts**: To create a stable boundary between the frontend and backend.
    5.  **Extensibility SDK**: To provide a safe and versionable contract for third-party developers.

- **Optimization Benefits**:
    - **Parallel Development**: Frontend, backend, and plugin development teams can now work independently in their respective repositories against stable contracts, significantly improving velocity.
    - **Improved Maintainability**: Clear ownership and focused responsibilities for each repository reduce cognitive load and simplify maintenance.
    - **Enhanced Testability**: The separation allows for focused unit testing of domain logic and integration testing of the infrastructure layer in isolation.
    - **Code Reusability**: Shared libraries for domain logic, contracts, and common utilities prevent code duplication.

- **Development Workflow**: The multi-repo structure supports a streamlined workflow where shared libraries are versioned and consumed as packages by the primary service application. The main `ReportingSystem.Service` repository acts as the integration point where all backend components are assembled into the final deployable artifact.

## System Architecture Diagrams

### Repository Dependency Architecture
This diagram illustrates the decomposed repository structure, the architectural layers, and the flow of dependencies between them. Dependencies strictly flow inwards, adhering to Clean Architecture principles.

mermaid
graph TD
    subgraph User Interaction
        UserBrowser["User's Browser (React SPA)"]
    end

    subgraph "Reporting System (On-Premise Host)"
        subgraph "Presentation Layer (UI)"
            UI[('REPO-09-WEB-UI<br><i>React, TypeScript, Vite</i>')]
        end

        subgraph "API & Application Layer (Composition Root)"
            ServiceHost[('REPO-08-SERVICE-HOST<br><i>ASP.NET Core, Quartz.NET</i>')]
        end

        subgraph "Domain Layer (Core Business Logic)"
            Domain[('REPO-02-CORE-DOMAIN<br><i>Entities, Interfaces</i>')]
        end

        subgraph "Infrastructure Layer (External Systems & I/O)"
            Infrastructure[('REPO-05-INFRASTRUCTURE<br><i>EF Core, Jint, Redis, Polly</i>')]
        end

        subgraph "Shared Libraries"
            Contracts[('REPO-03-SHARED-CONTRACTS<br><i>API DTOs</i>')]
            Common[('REPO-04-SHARED-COMMON<br><i>Utility Code</i>')]
        end

        subgraph "Extensibility Framework"
            SDK[('REPO-06-PLUGINS-SDK<br><i>IConnector Interface</i>')]
            Examples[('REPO-07-PLUGINS-EXAMPLES<br><i>FHIR & HL7 Connectors</i>')]
        end
    end

    subgraph External Dependencies
        PostgresDB[('PostgreSQL Database<br><i>Configuration, Logs, Users</i>')]
        RedisCache[('Redis Cache<br><i>Sessions, Roles, Config</i>')]
        ExternalSources["External Data Sources<br>(DBs, OPC UA, Files)"]
        DeliveryTargets["Delivery Targets<br>(S3, SMTP, FTP)"]
    end

    %% -- Primary Flows & Dependencies --
    UserBrowser --> |HTTPS/REST API Calls| ServiceHost
    ServiceHost --> |Serves Static Files| UI

    %% -- Backend Dependencies (Clean Architecture) --
    ServiceHost --> |References & Orchestrates| Domain
    ServiceHost --> |Injects & Uses| Infrastructure
    Infrastructure --> |Implements Interfaces From| Domain
    Infrastructure --> |Connects to| PostgresDB
    Infrastructure --> |Connects to| RedisCache
    Infrastructure --> |Communicates with| DeliveryTargets

    %% -- Shared Library Dependencies --
    ServiceHost --> |References| Contracts
    ServiceHost --> |References| Common
    Domain --> |References| Common
    Infrastructure --> |References| Common
    SDK --> |References| Contracts

    %% -- Extensibility Dependencies --
    ServiceHost --> |Dynamically Loads Plugins Implementing| SDK
    Examples -- Implements --> SDK

    %% -- Plugin Data Flow --
    ServiceHost --> |Via Loaded Plugins| ExternalSources

    %% -- Style Definitions --
    classDef ui fill:#e6f3ff,stroke:#333,stroke-width:2px;
    classDef service fill:#d5f5e3,stroke:#333,stroke-width:2px;
    classDef domain fill:#fff0b3,stroke:#333,stroke-width:4px;
    classDef infra fill:#f5e3d5,stroke:#333,stroke-width:2px;
    classDef shared fill:#e3d5f5,stroke:#333,stroke-width:2px;
    classDef sdk fill:#d5e8f5,stroke:#333,stroke-width:2px;
    classDef external fill:#f5d7d5,stroke:#333,stroke-width:2px;

    class UI,UserBrowser ui;
    class ServiceHost service;
    class Domain domain;
    class Infrastructure infra;
    class Contracts,Common shared;
    class SDK,Examples sdk;
    class PostgresDB,RedisCache,ExternalSources,DeliveryTargets external;


## Repository Catalog

### REPO-02-CORE-DOMAIN: ReportingSystem.Core.Domain
*   **Type**: Domain Library
*   **Description**: The foundational core of the application, containing all business entities (User, ReportConfiguration), domain logic, and interfaces for infrastructure dependencies (`IReportConfigurationRepository`). It is technology-agnostic and represents the innermost layer of the Clean Architecture, ensuring the business logic is stable and reusable.
*   **Technology**: .NET 8 Class Library
*   **Key Responsibilities**: Define business models, enforce domain invariants, and specify contracts for data persistence.

### REPO-03-SHARED-CONTRACTS: ReportingSystem.Shared.Contracts
*   **Type**: Model Library
*   **Description**: Defines the strict API contract between the frontend and backend. It contains only Data Transfer Objects (DTOs), enums, and constants used in REST API requests and responses. This decouples the UI from the backend's internal domain model.
*   **Technology**: .NET 8 Class Library
*   **Key Responsibilities**: Provide strongly-typed data structures for all API communication, enabling independent development and versioning.

### REPO-04-SHARED-COMMON: ReportingSystem.Shared.Common
*   **Type**: Utility Library
*   **Description**: A centralized library for reusable, non-domain-specific helper code, such as string extension methods and common patterns. It reduces code duplication and ensures consistency across the backend.
*   **Technology**: .NET 8 Class Library
*   **Key Responsibilities**: Adhere to the DRY (Don't Repeat Yourself) principle by providing a single location for generic utilities.

### REPO-05-INFRASTRUCTURE: ReportingSystem.Infrastructure
*   **Type**: Infrastructure Library
*   **Description**: Contains all concrete implementations for interfaces defined in `Core.Domain`. This layer handles all communication with external systems: PostgreSQL database (via EF Core), Redis cache, file systems, and third-party delivery services. It isolates volatile technology dependencies from the core application.
*   **Technology**: .NET 8, Entity Framework Core, Jint, Puppeteer Sharp, Polly, Serilog, Redis Client
*   **Key Responsibilities**: Data persistence, caching, secure script execution, PDF generation, and resilient communication with external APIs.

### REPO-06-PLUGINS-SDK: ReportingSystem.Plugins.Sdk
*   **Type**: SDK Library
*   **Description**: The public-facing Plugin Development Kit (PDK) for creating custom data connectors. It defines the `IConnector` interface and is distributed as a versioned package, providing a stable contract for third-party developers.
*   **Technology**: .NET 8 Class Library
*   **Key Responsibilities**: Define a secure, stable, and well-documented API for system extensibility.

### REPO-07-PLUGINS-EXAMPLES: ReportingSystem.Plugins.Examples
*   **Type**: Example Library
*   **Description**: Provides reference implementations of custom connectors (FHIR, HL7) that use the `Plugins.Sdk`. It serves as a practical guide and starting point for System Integrators.
*   **Technology**: .NET 8 Class Library
*   **Key Responsibilities**: Demonstrate best practices for building, testing, and securing custom plugins.

### REPO-08-SERVICE-HOST: ReportingSystem.Service
*   **Type**: Service Host Application
*   **Description**: The main executable and composition root of the backend. It hosts the ASP.NET Core web server, configures dependency injection, manages the Quartz.NET scheduler, and dynamically loads plugins. This repository assembles all other backend libraries into the final deployable Modular Monolith.
*   **Technology**: ASP.NET Core 8, Windows Services, Quartz.NET
*   **Key Responsibilities**: Host the application, expose the REST API, orchestrate application-level workflows, and manage background jobs.

### REPO-09-WEB-UI: ReportingSystem.Web.UI
*   **Type**: Frontend Application
*   **Description**: A self-contained Single-Page Application (SPA) built with React and TypeScript. It provides the Control Panel and Report Viewer interfaces and communicates exclusively with the backend via the REST API. Its static build artifacts are served by the `Service Host`.
*   **Technology**: React 18, TypeScript, Vite, MUI
*   **Key Responsibilities**: Deliver a responsive, accessible, and intuitive user experience for managing and viewing reports.

## Integration Architecture

- **Client-Server Communication**: The primary integration pattern is **Request-Reply** over HTTPS. The React SPA (`Web.UI`) acts as a client to the RESTful API exposed by the `Service Host`. All communication uses DTOs defined in the `Shared.Contracts` repository to ensure a stable, decoupled interface.

- **Internal Backend Composition**: Within the backend, the pattern is **Dependency Injection**. The `Service Host` acts as the composition root, registering concrete implementations from the `Infrastructure` repository against interfaces defined in the `Core.Domain` repository. This allows for loose coupling and high testability.

- **Extensibility**: The system uses a **Dynamic Plugin Loading** pattern. The `Service Host` scans a designated directory for DLLs that contain classes implementing the `IConnector` interface from the `Plugins.Sdk`. These are loaded at runtime and made available to the application, allowing for extensibility without recompiling the core product.

- **Asynchronous Processing**: For long-running report jobs, the system uses a **Scheduled Job** pattern with Quartz.NET. The API accepts a request and schedules a job for immediate or future execution, allowing the API to respond quickly while the work is performed in the background.

## Technology Implementation Framework

- **Domain Layer (`Core.Domain`)**: Must remain completely framework-agnostic. All logic should be pure C# with no dependencies on EF Core, ASP.NET Core, or other external libraries. Entities must protect their invariants through validation in constructors and methods.

- **Infrastructure Layer (`Infrastructure`)**: This is the only layer permitted to contain direct dependencies on external technologies (e.g., `Npgsql.EntityFrameworkCore.PostgreSQL`, `StackExchange.Redis`). All I/O operations must be asynchronous (`async`/`await`). Resiliency policies (e.g., Polly for retries) must be implemented for all external network calls.

- **Application & API Layer (`Service Host`)**: This layer orchestrates use cases. It contains all ASP.NET Core specifics, including Controllers, Middleware, and DI configuration. It should not contain business rules (Domain) or direct data access (Infrastructure). All incoming request DTOs must be validated.

- **UI Layer (`Web.UI`)**: Must adhere to modern React best practices (functional components, hooks). All state should be managed explicitly (e.g., using Zustand). Secure handling of JWTs (storage and attachment to requests) is paramount. All data rendered in the UI must be treated as untrusted and properly handled to prevent XSS attacks.

## Performance & Scalability Architecture

- **Scalability Model**: The primary model is **vertical scaling**, where the performance of the single-instance Windows Service is enhanced by increasing the CPU and RAM of the host machine. This aligns with the on-premise, single-tenant deployment model.

- **Performance Enablers**:
    - **Asynchronous Processing**: Long-running data ingestion and report generation tasks are offloaded to a background job scheduler (Quartz.NET), keeping the API responsive.
    - **Caching**: A distributed Redis cache is used to store frequently accessed, non-transactional data, such as user roles and system configuration. This significantly reduces database load and improves API latency.
    - **Resource Sandboxing**: The Jint engine and Puppeteer (headless Chromium) are configured with strict resource limits (memory, execution time) to prevent poorly written scripts or large reports from destabilizing the entire service.
    - **Optimized Data Access**: The Infrastructure layer is responsible for writing performant database queries, utilizing indexing, projections, and avoiding common pitfalls like N+1 queries.

## Development & Deployment Strategy

- **Development Workflow**: The multi-repository structure enables independent, parallel development streams:
    - **Frontend Team**: Works within `Web.UI`, using NPM/Vite, against a stable API contract.
    - **Backend Team**: Works across the `Service Host`, `Infrastructure`, and `Domain` repositories.
    - **System Integrators**: Consume the `Plugins.Sdk` as a package to develop custom connectors.

- **Continuous Integration/Continuous Deployment (CI/CD)**:
A GitHub Actions pipeline automates the build, test, and packaging process.
    1.  **Build & Test**: Each repository is built and its unit/integration tests are executed upon a push or pull request.
    2.  **Package**: Shared libraries (`Core.Domain`, `Shared.Contracts`, etc.) are packaged as artifacts.
    3.  **Compose**: The `Service Host` build consumes the library artifacts, composing the final backend application.
    4.  **Package Installer**: The WiX Toolset is used to package the composed service, the static UI files from the `Web.UI` build, and all dependencies into a single **MSI installer** for deployment.

- **Deployment Model**: The final MSI package is delivered to customers for on-premise installation. The installer handles the setup of the Windows Service, initial configuration, and places all necessary files on the host machine.

## Architecture Decision Records

### ADR-001: Adopt a Modular Monolith with Clean Architecture
*   **Decision**: To structure the application as a Modular Monolith, internally organized using Clean Architecture principles, rather than a distributed microservices architecture or a traditional layered monolith.
*   **Rationale**: This hybrid approach provides the operational simplicity required for an on-premise, customer-managed product (single deployable unit) while gaining the benefits of a modular design, such as improved maintainability, testability, and clear separation of concerns.

### ADR-002: Decompose the Monorepo into Layered Repositories
*   **Decision**: To break down the single source code repository into multiple, focused repositories aligned with architectural layers (Domain, Infrastructure, UI, SDK).
*   **Rationale**: This enforces the Clean Architecture dependency rule at a structural level, enables independent development workflows for different teams (frontend vs. backend), and creates a formal, distributable SDK for third-party plugin development.

### ADR-003: Utilize PostgreSQL for Primary Storage
*   **Decision**: To use PostgreSQL as the primary database for storing all system configuration, user data, and job metadata, superseding the initial consideration of SQLite.
*   **Rationale**: The system's requirements for up to 100 concurrent users and 1,000 daily jobs, along with the need for robust monitoring and backup/recovery, necessitate a more powerful and scalable client-server RDBMS. PostgreSQL offers superior concurrency control, performance, and operational maturity compared to an embedded database like SQLite.

### ADR-004: Implement a Formal Plugin SDK for Extensibility
*   **Decision**: To create a dedicated, versionable SDK library (`Plugins.Sdk`) that defines a public interface (`IConnector`) for custom data connectors.
*   **Rationale**: This provides a stable, secure, and decoupled contract for third-party System Integrators. It allows the plugin ecosystem to evolve independently of the core application, prevents third-party code from needing access to internal application logic, and formalizes the extensibility point of the system.