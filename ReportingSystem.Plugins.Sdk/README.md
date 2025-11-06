# Reporting System - Plugin SDK

This repository contains the `ReportingSystem.Plugins.Sdk`, the official Software Development Kit (SDK) for building custom data connectors for the Reporting System application.

## Overview

The purpose of this SDK is to provide a stable, public contract for third-party developers and system integrators to create their own data connectors. By implementing the `IConnector` interface defined in this library, you can extend the Reporting System to ingest data from proprietary or unsupported data sources.

This allows for a powerful "plug-and-play" architecture, where new data sources can be added to the system simply by deploying a compiled `.dll` file.

## Key Components

The core of the SDK is the `IConnector` interface. An implementation of this interface is responsible for:

-   **Discovery**: Providing a user-friendly name to be displayed in the Control Panel.
-   **Configuration**: Defining a JSON schema that the Control Panel uses to dynamically render a configuration UI.
-   **Connection Testing**: A method to validate the user-provided configuration (e.g., test database credentials) before saving.
-   **Data Fetching**: The primary method for connecting to the data source and returning data as a standardized `System.Text.Json.Nodes.JsonNode` object.

All long-running operations support cooperative cancellation via `CancellationToken` to ensure system stability.

## Getting Started

1.  **Reference the SDK**: Add a reference to the `ReportingSystem.Plugins.Sdk` NuGet package in your .NET 8 Class Library project.
2.  **Implement `IConnector`**: Create a public class in your library that implements the `IConnector` interface.
3.  **Build your Connector**: Implement the required methods with the logic specific to your data source.
4.  **Deploy**: Compile your project and copy the resulting `.dll` (along with any dependencies) into the `plugins` directory of the Reporting System application server. The application will automatically discover and load your connector.

For detailed examples, please refer to the `ReportingSystem.Plugins.Examples` repository.

## Versioning

The SDK follows Semantic Versioning 2.0.0. We will make every effort to maintain backward compatibility for the `IConnector` interface. Any breaking changes will be accompanied by a major version increment.

## Building the SDK

To build this project locally:

1.  Ensure you have the .NET 8 SDK installed.
2.  Clone this repository.
3.  Run `dotnet build` from the root directory.
4.  Run `dotnet test` to execute the unit tests.

This will produce the `ReportingSystem.Plugins.Sdk.nupkg` file in the `src/bin/Release` directory.