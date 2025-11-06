# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-041 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | View discovered custom connectors in the Control P... |
| As A User Story | As an Administrator, I want custom-developed conne... |
| User Persona | Administrator |
| Business Value | Enhances system extensibility by providing a seaml... |
| Functional Area | Data Ingestion |
| Story Theme | Extensible Connector Architecture |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: A valid custom connector is discovered and displayed

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a valid custom connector DLL that implements the `IConnector` interface has been placed in the designated plug-ins directory

### 3.1.5 When

the application service is started or restarted

### 3.1.6 Then

the system successfully loads the connector without errors, AND the connector's name appears in the list of available connector types on the 'Create Connector' page in the Control Panel.

### 3.1.7 Validation Notes

Test by deploying a sample valid connector DLL, restarting the service, and verifying its presence in the UI's connector selection list.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Error Condition: A non-connector DLL is ignored

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

a valid .NET DLL that does NOT implement the `IConnector` interface is in the plug-ins directory

### 3.2.5 When

the application service is started

### 3.2.6 Then

the system logs a warning or debug message that the DLL was ignored, AND the application starts successfully, AND the DLL does not appear in the connector type list.

### 3.2.7 Validation Notes

Test with a generic .NET class library DLL. Check logs for the expected message and the UI for its absence.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Error Condition: An invalid or corrupt file is handled gracefully

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

a file that is not a valid .NET assembly (e.g., a text file or a corrupted DLL) is in the plug-ins directory

### 3.3.5 When

the application service is started

### 3.3.6 Then

the system logs an error indicating the file could not be loaded, AND the application starts successfully without crashing, AND the invalid file does not appear in the connector type list.

### 3.3.7 Validation Notes

Test by placing a .txt file renamed to .dll in the directory. Verify the service starts and logs the error.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Edge Case: A previously used connector is removed

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

a custom connector DLL was previously loaded and used to create a connector configuration, AND the DLL is now removed from the plug-ins directory

### 3.4.5 When

the application service is restarted

### 3.4.6 Then

the removed connector type no longer appears as an option for creating *new* connectors, AND any existing configurations that used it are clearly marked as 'Invalid' or 'Orphaned' in the UI with an informative message.

### 3.4.7 Validation Notes

Create a configuration with a custom connector. Stop service, remove DLL, restart service. Verify the connector type is gone from the creation list and the existing configuration is flagged as invalid.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Logging: The discovery process is logged for diagnostics

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

the plug-in discovery process is initiated at startup

### 3.5.5 When

the system scans the plug-ins directory

### 3.5.6 Then

the system log contains an informational entry for each successfully loaded custom connector, AND a warning or error entry for any file that failed to load, including the reason for failure.

### 3.5.7 Validation Notes

Inspect the Serilog JSON output file after startup with a mix of valid and invalid plug-ins to confirm all events are logged correctly.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

API: The list of connector types is available via API

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

the system has started and discovered both built-in and custom connectors

### 3.6.5 When

an authenticated API client makes a GET request to the connector types endpoint

### 3.6.6 Then

the API returns a JSON array containing details for all available connectors, including those discovered from plug-ins.

### 3.6.7 Validation Notes

Use an API client like Postman or the integrated Swagger UI to call the endpoint and verify the response.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A list, dropdown, or card-based selection UI on the 'Create New Connector' page to display available connector types.

## 4.2.0 User Interactions

- The Administrator selects a connector type from the list to begin the configuration process.

## 4.3.0 Display Requirements

- The list must display the `Name` property provided by the connector's implementation.
- It is recommended to visually distinguish between built-in and custom connectors (e.g., using a 'Custom' tag or icon).

## 4.4.0 Accessibility Needs

- The list of connectors must be navigable using a keyboard.
- Each connector type must have an accessible name for screen readers.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'Only assemblies that successfully load and contain a public, parameterless constructor class implementing the `IConnector` interface are considered valid connectors.', 'enforcement_point': 'During the plug-in discovery scan at application startup.', 'violation_handling': 'The assembly or type is ignored, a diagnostic message is logged, and the application continues to load.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-113

#### 6.1.1.2 Dependency Reason

The `IConnector` interface must be defined before a discovery mechanism can be built to find implementations of it.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-042

#### 6.1.2.2 Dependency Reason

A designated plug-ins directory and deployment process must be established for the discovery mechanism to have a location to scan.

## 6.2.0.0 Technical Dependencies

- .NET 8 Assembly Loading APIs (e.g., `Assembly.LoadFrom`, `AssemblyLoadContext`).
- A shared class library containing the `IConnector` interface definition.
- An ASP.NET Core API endpoint to expose the list of discovered connectors to the frontend.
- Serilog for structured logging of the discovery process.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The plug-in discovery process should add no more than 5 seconds to the application's startup time, assuming a directory with up to 20 plug-ins.

## 7.2.0.0 Security

- The application must gracefully handle exceptions during assembly loading to prevent a malicious or poorly-coded plug-in from crashing the service.
- The plug-in loading mechanism should not be vulnerable to assembly injection attacks beyond placing files in the designated folder, which is controlled by IT Support.

## 7.3.0.0 Usability

- The process should be entirely automatic after DLL deployment; no UI-based registration should be required.

## 7.4.0.0 Accessibility

- Meets WCAG 2.1 Level AA standards for all related UI elements.

## 7.5.0.0 Compatibility

- The discovery mechanism must be compatible with any .NET 8 DLL.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires robust error handling to ensure a single faulty plug-in does not destabilize the entire application.
- Involves .NET reflection and assembly loading, which has inherent complexities.
- Requires careful design of the API contract between the backend discovery service and the frontend UI.

## 8.3.0.0 Technical Risks

- Potential for dependency conflicts if plug-ins bring in different versions of shared libraries. Using `AssemblyLoadContext` could mitigate this but adds complexity.
- Risk of memory leaks if assemblies are loaded but not properly managed (less of a concern with a simple startup-scan approach).

## 8.4.0.0 Integration Points

- Backend: The file system's plug-ins directory.
- Backend: The central connector configuration service that manages all connector types.
- Frontend: The 'Create New Connector' UI component in the React application.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Deploying a valid connector DLL and verifying its appearance in the UI.
- Deploying various invalid files (corrupt DLL, non-connector DLL, non-DLL file) and verifying they are ignored and logged correctly.
- Removing a connector DLL and verifying it disappears from the new connector list and flags existing configurations.
- Starting the service with an empty plug-ins directory.

## 9.3.0.0 Test Data Needs

- A sample, fully functional custom connector project/DLL for testing the happy path.
- A sample .NET DLL project that does not implement the `IConnector` interface.
- A set of invalid/corrupt files to test error handling.

## 9.4.0.0 Testing Tools

- xUnit for backend unit/integration tests.
- Moq for mocking file system interactions.
- Jest/React Testing Library for frontend component tests.
- Playwright or Cypress for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests for the discovery service achieve >80% coverage
- Integration testing for loading valid and invalid plug-ins completed successfully
- API endpoint for listing connector types is implemented, documented in OpenAPI spec, and tested
- User interface correctly displays discovered connectors and is approved
- Performance impact on startup time is measured and within acceptable limits
- Security considerations for assembly loading have been reviewed and addressed
- Documentation for IT Support on the plug-in directory is updated
- Story deployed and verified in staging environment with a sample plug-in

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational story for the entire custom connector feature. It should be prioritized early in the development cycle for that theme.
- Requires coordination between backend (discovery logic, API) and frontend (UI display) development efforts.

## 11.4.0.0 Release Impact

- Enables a major feature (Extensibility) outlined in the project scope. Its completion is critical for delivering the custom connector capability.

