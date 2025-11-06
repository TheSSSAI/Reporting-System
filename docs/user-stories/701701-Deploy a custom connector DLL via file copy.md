# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-042 |
| Elaboration Date | 2025-01-17 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Deploy a custom connector DLL via file copy |
| As A User Story | As an IT Support user, I want to deploy a new cust... |
| User Persona | IT Support: This user has OS-level access to the s... |
| Business Value | Enables a flexible and powerful extensibility mode... |
| Functional Area | System Architecture & Extensibility |
| Story Theme | Data Ingestion Framework |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Deploying a valid connector DLL to a running service

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

The Windows service is running and a 'plugins' directory exists in the application's installation folder

### 3.1.5 When

I copy a valid .NET DLL, which contains a public class implementing the 'IConnector' interface, into the 'plugins' directory

### 3.1.6 Then

The system automatically detects the new file, loads the assembly, registers the new connector, and makes it available for selection in the Control Panel without requiring a service restart.

### 3.1.7 Validation Notes

Verify by navigating to the 'Connectors' section in the Control Panel and confirming the new connector type appears in the 'Add New Connector' dropdown.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Happy Path: Connector is present before service startup

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The Windows service is stopped and a valid connector DLL is present in the 'plugins' directory

### 3.2.5 When

I start the Windows service

### 3.2.6 Then

The system scans the 'plugins' directory on startup, successfully loads and registers the connector, and a log entry confirms the successful loading of the connector.

### 3.2.7 Validation Notes

Check the application logs for a success message and verify the connector is available in the Control Panel UI.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Error Condition: Deploying a non-.NET DLL

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

The Windows service is running

### 3.3.5 When

I copy a file that is not a valid .NET assembly (e.g., a text file renamed to .dll) into the 'plugins' directory

### 3.3.6 Then

The system attempts to load the file, logs a clear error message indicating a 'BadImageFormatException' or similar, ignores the file, and continues to operate normally without crashing.

### 3.3.7 Validation Notes

Check the system's error logs for the specific error message. The service's health and other functionalities should remain unaffected.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Error Condition: Deploying a DLL without an IConnector implementation

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

The Windows service is running

### 3.4.5 When

I copy a valid .NET DLL that does not contain any public classes implementing the 'IConnector' interface into the 'plugins' directory

### 3.4.6 Then

The system inspects the assembly, finds no valid connector types, logs an informational message, and ignores the assembly without error.

### 3.4.7 Validation Notes

Check the application logs for an info/debug level message. The list of available connectors in the UI should not change.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Error Condition: Connector DLL with missing dependencies

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

The Windows service is running

### 3.5.5 When

I copy a connector DLL into the 'plugins' directory that depends on another assembly which is not present

### 3.5.6 Then

The system fails to load the connector, logs a detailed error message specifying the missing dependency (e.g., from a 'ReflectionTypeLoadException'), and the main application continues to run without crashing.

### 3.5.7 Validation Notes

The error log must clearly state which dependency could not be loaded. The specific connector will not be available in the UI.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Edge Case: Updating or removing a connector DLL

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

A custom connector DLL is loaded and in use by the system

### 3.6.5 When

I attempt to delete or overwrite the DLL file in the 'plugins' directory while the service is running

### 3.6.6 Then

The operating system will likely prevent the action due to a file lock. The documented procedure must state that the service must be stopped to update or remove a plugin, and the system must successfully unload the old version and load the new one upon restart.

### 3.6.7 Validation Notes

Test by stopping the service, replacing a connector DLL with a new version, starting the service, and verifying the new version's behavior is active.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- None for this story.

## 4.2.0 User Interactions

- This story involves direct file system interaction by an IT Support user, not UI interaction.

## 4.3.0 Display Requirements

- The result of this action is displayed in the Control Panel as described in US-041.

## 4.4.0 Accessibility Needs

- Not applicable.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Only assemblies containing a public, non-abstract class that implements the 'IConnector' interface will be treated as valid plugins.

### 5.1.3 Enforcement Point

During the assembly scanning and type-loading process.

### 5.1.4 Violation Handling

The assembly is ignored. An informational message is logged for debugging purposes.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The plugin loading mechanism must be fault-tolerant; a failure to load one plugin must not prevent other plugins or the core application from loading and running.

### 5.2.3 Enforcement Point

Within the try-catch block of the plugin loading loop.

### 5.2.4 Violation Handling

The specific error is caught and logged in detail, and the system proceeds to the next file.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

- {'story_id': 'US-113', 'dependency_reason': "The 'IConnector' interface and the overall plug-in specification must be defined and implemented before a loading mechanism can be built to consume it."}

## 6.2.0 Technical Dependencies

- The core application must be built using a technology that supports dynamic assembly loading, such as .NET's AssemblyLoadContext.
- A designated 'plugins' directory must be created by the MSI installer (US-001).

## 6.3.0 Data Dependencies

- None.

## 6.4.0 External Dependencies

- Depends on a System Integrator (user class) providing a custom connector DLL for testing purposes.

# 7.0.0 Non Functional Requirements

## 7.1.0 Performance

- The startup scan of the 'plugins' directory should add no more than 2 seconds to the service's total startup time, assuming up to 20 plugins.
- Runtime scanning (if implemented via FileSystemWatcher) should not introduce noticeable CPU overhead.

## 7.2.0 Security

- The system executes third-party code. Documentation must strongly warn users to only deploy DLLs from trusted sources.
- File system permissions on the 'plugins' directory should be restricted by the installer to only allow write access for Administrators/IT Support roles to prevent unauthorized code execution.

## 7.3.0 Usability

- The deployment process must be as simple as 'file copy'. No configuration file edits or database entries should be required for the system to discover the plugin.

## 7.4.0 Accessibility

- Not applicable.

## 7.5.0 Compatibility

- The plugin loading mechanism must be compatible with any .NET 8 compliant DLL.

# 8.0.0 Implementation Considerations

## 8.1.0 Complexity Assessment

Medium

## 8.2.0 Complexity Factors

- Requires correct implementation of .NET's AssemblyLoadContext to ensure plugins and their dependencies are loaded in an isolated context, preventing conflicts.
- Robust error handling is critical to ensure a single faulty plugin does not crash the entire application.
- Managing file locks and the lifecycle of loaded assemblies (especially for updates/unloads) is non-trivial.

## 8.3.0 Technical Risks

- Risk of 'dependency hell' if multiple plugins require different versions of the same shared library. Using AssemblyLoadContext helps mitigate this but adds complexity.
- A poorly written plugin could cause memory leaks or performance issues in the main application.

## 8.4.0 Integration Points

- The application's core service startup sequence.
- The application's logging system (Serilog).
- The data service that provides the list of available connector types to the Control Panel API.

# 9.0.0 Testing Requirements

## 9.1.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0 Test Scenarios

- Deploy a valid connector and verify it appears in the UI.
- Deploy a DLL with missing dependencies and verify the logged error.
- Deploy a non-.NET file and verify the system remains stable.
- Deploy multiple valid connectors at once.
- Verify the documented procedure for updating a connector (stop service, replace file, start service) works as expected.

## 9.3.0 Test Data Needs

- A sample 'valid' connector project that can be compiled to a DLL.
- A sample 'invalid' connector project (e.g., with a dependency on a non-existent DLL).
- A sample non-assembly file (e.g., image.dll).

## 9.4.0 Testing Tools

- xUnit for unit tests.
- Moq for mocking file system interactions.

# 10.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for the plugin loader logic, covering success and failure cases, with >80% coverage
- Integration testing completed with sample valid and invalid plugins
- The procedure for deploying, updating, and removing custom connectors is clearly documented in the Installation and Administration Guide
- Security considerations regarding trusted code are documented
- Story deployed and verified in staging environment by following the documented procedure

# 11.0.0 Planning Information

## 11.1.0 Story Points

5

## 11.2.0 Priority

🔴 High

## 11.3.0 Sprint Considerations

- This is a foundational story for the system's extensibility. It should be prioritized early in the development cycle.
- Must be scheduled after US-113 is complete. Should be scheduled in the same sprint as or immediately before US-041 to enable end-to-end testing of the custom connector feature.

## 11.4.0 Release Impact

- Enables a key selling point of the product: its extensibility. Critical for the initial release.

