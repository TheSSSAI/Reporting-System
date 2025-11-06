# FHIR R4 Connector Example

This project is a complete, working example of a custom data connector for the Reporting System. It demonstrates how to implement the `IConnector` interface from the `ReportingSystem.Plugins.Sdk` to ingest data from a FHIR R4 compliant server.

This example is provided as part of the Plug-in Development Kit (PDK) to serve as a reference for System Integrators.

## Prerequisites

- .NET 8 SDK
- Access to a FHIR R4 test server. A public one like `http://hapi.fhir.org/baseR4` can be used for testing.

## How to Build

Navigate to this project's directory in a terminal and run the following command:

```bash
dotnet build --configuration Release
```

This will produce the `ReportingSystem.Plugins.Examples.Fhir.dll` file in the `bin/Release/net8.0` directory.

## Configuration

When you select this connector in the Reporting System's Control Panel, you will be prompted to provide the following configuration, as defined by the `GetConfigurationSchema()` method:

-   **Base URL**: The base endpoint of the FHIR R4 server.
    -   _Example_: `http://hapi.fhir.org/baseR4`
-   **FHIR Query**: The resource query to execute against the server. This is the part of the URL that comes after the base URL.
    -   _Example_: `Patient?family=Smith&_count=50`
-   **Bearer Token (Optional)**: If the FHIR server requires OAuth 2.0 authentication, provide the Bearer token here. The connector will add it to the `Authorization` header of the request. For security, this field is treated as a secret and will be masked in the UI.

## How to Deploy

To deploy this connector, simply copy the compiled DLL file to the main application's `plugins` directory.

1.  Stop the Reporting System Windows Service.
2.  Copy `ReportingSystem.Plugins.Examples.Fhir.dll` from the build output directory to the `plugins` folder located in the main application's installation path (e.g., `C:\Program Files\ReportingSystem\plugins`).
3.  Start the Reporting System Windows Service.

The connector will be automatically discovered on startup and will become available for selection in the Control Panel.