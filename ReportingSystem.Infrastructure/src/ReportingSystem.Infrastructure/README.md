# ReportingSystem.Infrastructure

This project is the **Infrastructure Layer** in the Clean Architecture of the ReportingSystem application. It contains all the concrete implementations for interfaces defined in the `ReportingSystem.Application` and `ReportingSystem.Domain` layers. This layer is responsible for all interactions with external systems and frameworks.

## Purpose

The primary goal of the Infrastructure layer is to isolate the core application from the volatile details of external technologies. By depending on abstractions (interfaces) from the inner layers, we can swap out or update our infrastructure (e.g., change the database from PostgreSQL to SQL Server, or the caching provider from Redis to something else) with minimal impact on the core business logic.

## Key Responsibilities

This project handles the following concerns:

-   **Data Persistence**: Implements the Repository and Unit of Work patterns using **Entity Framework Core 8** to communicate with a **PostgreSQL** database. It contains the `AppDbContext`, repository implementations, and EF Core migrations.
-   **JavaScript Scripting**: Provides the concrete implementation of the `ITransformationEngine` interface using the **Jint** library. This includes a secure sandbox for executing user-defined scripts.
-   **Caching**: Implements distributed caching interfaces using **StackExchange.Redis**.
-   **PDF Generation**: Implements the `IPdfGenerator` interface using the **Puppeteer Sharp** library.
-   **Logging**: Configures **Serilog** for structured logging throughout the application.
-   **Resiliency**: Defines and registers **Polly** policies (e.g., Retry, Circuit Breaker) for resilient communication with external services.
-   **Dependency Injection**: Contains extension methods to register all of this layer's services with the .NET dependency injection container.

## Development Setup

To run this project locally, you will need the following external services:

1.  **PostgreSQL Database**
2.  **Redis Cache**

The recommended way to run these services for development is using Docker. A `docker-compose.yml` file may be provided at the solution root to simplify this setup.

Example `docker-compose.yml`:
```yaml
version: '3.8'
services:
  postgres:
    image: postgres:15
    environment:
      POSTGRES_DB: reporting_system_db
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: password
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data

  redis:
    image: redis:7
    ports:
      - "6379:6379"

volumes:
  postgres_data:
```

The connection strings in `appsettings.Development.json` are configured to connect to these default Docker instances.

## Database Migrations

This project uses Entity Framework Core Migrations to manage the database schema.

### Prerequisites

-   Ensure the `dotnet-ef` global tool is installed:
    `dotnet tool install --global dotnet-ef`

### Creating a New Migration

When you make changes to the entities in the `ReportingSystem.Domain` project or the configurations in this project, you will need to create a new migration.

Run the following command from the root of the repository (where the `.sln` file is):

```sh
dotnet ef migrations add <MigrationName> --project src/ReportingSystem.Infrastructure --startup-project src/ReportingSystem.ServiceHost
```

-   `<MigrationName>` should be a descriptive name of the changes (e.g., `AddUserRoles`).
-   The `--project` flag points to the project containing the `DbContext` (this project).
-   The `--startup-project` flag points to the executable project that configures all the services.

### Applying Migrations

Migrations are configured to be applied automatically on application startup in the `Program.cs` of the `ReportingSystem.ServiceHost` project.

To apply migrations manually, you can use the following command:

```sh
dotnet ef database update --project src/ReportingSystem.Infrastructure --startup-project src/ReportingSystem.ServiceHost
```