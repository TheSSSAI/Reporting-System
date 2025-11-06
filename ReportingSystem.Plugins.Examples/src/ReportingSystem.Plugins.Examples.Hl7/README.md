# HL7 v2 Connector Example

This project provides a complete, working example of a custom data connector for the Reporting System. It demonstrates how to implement the `IConnector` interface from the `ReportingSystem.Plugins.Sdk` to parse an HL7 v2 message file and transform its contents into the system's standard JSON format.

This example is provided as part of the Plug-in Development Kit (PDK) to serve as a reference for System Integrators building file-based connectors.

## Prerequisites

-   .NET 8 SDK
-   A sample HL7 v2 file for testing.

## How to Build

Navigate to this project's directory in a terminal and run the following command:

```bash
dotnet build --configuration Release
```

This will produce the `ReportingSystem.Plugins.Examples.Hl7.dll` file in the `bin/Release/net8.0` directory. This build will also restore the required `NHapi` NuGet package dependency.

## Configuration

When you select this connector in the Reporting System's Control Panel, you will be prompted to provide the following configuration, as defined by the `GetConfigurationSchema()` method:

-   **File Path**: The full local or UNC path to the source HL7 v2 file. The user account running the Reporting System Windows Service must have read access to this path.
    -   _Example (Local)_: `C:\data\hl7\adt_messages.hl7`
    -   _Example (UNC)_: `\\fileserver\shares\hl7_inbound\messages.txt`
-   **Encoding**: The character encoding of the source file.
    -   _Default_: `UTF-8`
    -   _Example_: `ISO-8859-1`

## How to Deploy

To deploy this connector, simply copy the compiled DLL file to the main application's `plugins` directory.

1.  Stop the Reporting System Windows Service.
2.  Copy `ReportingSystem.Plugins.Examples.Hl7.dll` from the build output directory to the `plugins` folder located in the main application's installation path (e.g., `C:\Program Files\ReportingSystem\plugins`).
3.  Start the Reporting System Windows Service.

The connector will be automatically discovered on startup and will become available for selection in the Control Panel.