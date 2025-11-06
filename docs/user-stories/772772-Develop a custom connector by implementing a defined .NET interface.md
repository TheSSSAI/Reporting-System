# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-113 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Develop a custom connector by implementing a defin... |
| As A User Story | As a System Integrator, I want to develop a custom... |
| User Persona | System Integrator: A technical user (developer) re... |
| Business Value | Enables core product extensibility, allowing integ... |
| Functional Area | Data Ingestion Framework |
| Story Theme | Extensibility and Custom Connectors |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful implementation and loading of a valid custom connector

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A .NET class library project correctly implements the `IConnector` interface from the provided contract assembly

### 3.1.5 When

The compiled DLL is placed in the system's designated 'plugins' directory and the main application service is started

### 3.1.6 Then

The system's plug-in loader discovers and loads the custom connector assembly without errors, logging a success message.

### 3.1.7 Validation Notes

Verify via application logs that the connector was loaded. An internal API endpoint for listing loaded plugins should include the new connector.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

System correctly invokes the GetConfigurationSchema method

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

A valid custom connector is loaded by the system

### 3.2.5 When

The system needs to display the configuration UI for this connector and calls the `GetConfigurationSchema()` method

### 3.2.6 Then

The method must return a valid JSON string that defines the UI elements and validation rules for the connector's configuration.

### 3.2.7 Validation Notes

The returned string must be parsable as JSON and conform to the schema definition specified in the PDK. This output is the direct input for story US-037.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

System correctly invokes the TestConnection method

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

A custom connector is loaded and a valid configuration JSON is provided

### 3.3.5 When

The system calls the `TestConnection(configJson)` method with the provided configuration

### 3.3.6 Then

The method executes its internal logic and returns a result object indicating success or failure with a descriptive message.

### 3.3.7 Validation Notes

Test with both valid and invalid configurations to ensure the method returns the correct status and a meaningful error message upon failure.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

System correctly invokes the FetchData method

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

A report job is configured to use a loaded custom connector with a valid configuration

### 3.4.5 When

The report job's execution pipeline calls the `FetchData(configJson)` method

### 3.4.6 Then

The method must return data in the system's standardized internal JSON format, as specified in the PDK.

### 3.4.7 Validation Notes

The returned JSON must be well-formed and adhere to the documented structure for internal data processing.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

System handles a DLL in the plugins folder that does not implement the IConnector interface

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

A DLL file that does not contain a public class implementing `IConnector` is placed in the 'plugins' directory

### 3.5.5 When

The application service starts

### 3.5.6 Then

The system logs a warning indicating a DLL was found but could not be used as a connector, specifying the filename.

### 3.5.7 And

The application must start and operate normally without crashing.

### 3.5.8 Validation Notes

Check application logs for the specific warning message. Ensure other valid plugins are still loaded correctly.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

System gracefully handles exceptions thrown by a custom connector method

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

A custom connector's method (e.g., `FetchData`) is implemented to throw an unhandled exception

### 3.6.5 When

The system calls that method during a process like a report run

### 3.6.6 Then

The core application must catch the exception and not crash.

### 3.6.7 And

The full exception details from the connector are written to the relevant execution log for troubleshooting.

### 3.6.8 Validation Notes

Create a test connector designed to throw exceptions and verify that the main service remains stable and logs the error correctly.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

System isolates custom connector assemblies to prevent dependency conflicts

### 3.7.3 Scenario Type

Edge_Case

### 3.7.4 Given

Two different custom connector DLLs that depend on different versions of the same third-party library are in the 'plugins' directory

### 3.7.5 When

The application service starts and loads both connectors

### 3.7.6 Then

Both connectors must load and operate correctly without a `System.IO.FileLoadException` or other version conflict errors.

### 3.7.7 Validation Notes

This requires using an isolation mechanism like .NET's `AssemblyLoadContext` for each plugin.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- N/A

## 4.2.0 User Interactions

- N/A

## 4.3.0 Display Requirements

- This is a backend-focused story. There are no direct UI changes. It enables functionality for other UI-facing stories like US-037 and US-041.

## 4.4.0 Accessibility Needs

- N/A

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

All custom connectors must be implemented in a .NET DLL that contains at least one public, non-abstract class implementing the `IConnector` interface.

### 5.1.3 Enforcement Point

During application startup, by the plug-in discovery service.

### 5.1.4 Violation Handling

The DLL is ignored, and a warning is logged. The application continues to run.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Data returned from a connector's `FetchData` method must conform to the standardized internal JSON structure.

### 5.2.3 Enforcement Point

Immediately after the `FetchData` method returns, before the data transformation step.

### 5.2.4 Violation Handling

The data is considered invalid, the report job is marked as 'Failed', and an error is logged detailing the validation failure.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

*No items available*

## 6.2.0 Technical Dependencies

- A finalized `IConnector` interface definition.
- A separate, distributable 'Contract' assembly (.dll) containing the `IConnector` interface and related models for System Integrators to reference.
- A robust plug-in loading mechanism in the core application, likely using .NET's `AssemblyLoadContext` for isolation and dynamic loading/unloading.
- A clear definition of the 'standardized JSON format' for data interchange.

## 6.3.0 Data Dependencies

*No items available*

## 6.4.0 External Dependencies

*No items available*

# 7.0.0 Non Functional Requirements

## 7.1.0 Performance

- The plug-in discovery process on startup should add no more than 500ms to the application's start time for up to 20 plugins.

## 7.2.0 Security

- The plug-in execution environment must be isolated from the main application domain to prevent malicious or poorly written code from crashing the host process.
- The plug-in loading mechanism must not execute arbitrary code from non-DLL files in the plugins directory.

## 7.3.0 Usability

- The `IConnector` interface must be well-documented with XML comments so that IntelliSense provides clear guidance to the System Integrator in their IDE.

## 7.4.0 Accessibility

- N/A

## 7.5.0 Compatibility

- The contract assembly must target a .NET version compatible with the main application (.NET 8).

# 8.0.0 Implementation Considerations

## 8.1.0 Complexity Assessment

High

## 8.2.0 Complexity Factors

- Designing a stable, version-tolerant public API (the `IConnector` interface) is critical and difficult to change later.
- Implementing a secure and robust plug-in loading architecture requires deep knowledge of .NET assembly loading and isolation (`AssemblyLoadContext`).
- Requires creation of a separate 'Contract' assembly to be distributed in the PDK.
- Thorough error handling is required to ensure the main application remains stable regardless of the quality of third-party plugins.

## 8.3.0 Technical Risks

- Security vulnerabilities from executing third-party code.
- Memory leaks if plug-in assemblies and their dependencies are not correctly unloaded (if dynamic unloading is supported).
- Dependency hell if plug-in isolation is not implemented correctly.

## 8.4.0 Integration Points

- Core Application Service: For loading plugins on startup.
- Configuration Module: For calling `GetConfigurationSchema` and `TestConnection`.
- Report Execution Engine: For calling `FetchData`.

# 9.0.0 Testing Requirements

## 9.1.0 Testing Types

- Unit
- Integration

## 9.2.0 Test Scenarios

- Create a sample 'Good Connector' project that correctly implements the interface and verify it loads and all its methods can be called successfully.
- Create a 'Bad Connector' project that throws exceptions in its methods and verify the host application handles them gracefully.
- Create a test with a DLL that does not implement the interface and verify it is ignored.
- Create two test connectors with conflicting dependencies to validate the assembly loading isolation.

## 9.3.0 Test Data Needs

- Sample configuration JSON for test connectors.
- Sample data payloads to be returned by the test connectors.

## 9.4.0 Testing Tools

- xUnit
- Moq

# 10.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code for the plug-in loader and interface interaction is peer-reviewed and merged
- Unit test coverage for the plug-in loading service is at or above 80%
- Integration tests using a sample connector project are implemented and passing
- The `IConnector` interface and its contract assembly are finalized and documented with XML comments
- Security review of the plug-in loading mechanism has been completed
- The contract assembly is ready for packaging into the PDK (for story US-111)
- Story deployed and verified in the staging environment

# 11.0.0 Planning Information

## 11.1.0 Story Points

13

## 11.2.0 Priority

🔴 High

## 11.3.0 Sprint Considerations

- This is a foundational, architectural story. It blocks other stories in the 'Extensibility' theme (e.g., US-037, US-041).
- A technical design spike may be required to finalize the implementation details of the `AssemblyLoadContext` strategy before starting development.

## 11.4.0 Release Impact

This story is critical for the extensibility feature set. The entire feature cannot be released without it.

