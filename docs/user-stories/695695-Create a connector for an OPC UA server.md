# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-036 |
| Elaboration Date | 2025-01-17 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Create a connector for an OPC UA server |
| As A User Story | As an Administrator, I want to configure and test ... |
| User Persona | Administrator |
| Business Value | Enables the system to connect to a critical data s... |
| Functional Area | Data Ingestion |
| Story Theme | Data Ingestion Framework |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

UI for OPC UA connector configuration is displayed correctly

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

An Administrator is on the 'Create New Connector' page in the Control Panel

### 3.1.5 When

The Administrator selects 'OPC UA' from the connector type dropdown

### 3.1.6 Then

The UI dynamically displays specific configuration fields for OPC UA: 'Endpoint URL', 'Security Policy', 'Message Security Mode', 'Authentication Method', and a text area for 'Node Definitions'.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Successfully test and save an OPC UA connector with Anonymous authentication

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The OPC UA configuration form is displayed and a test server is available

### 3.2.5 When

The Administrator enters a valid 'Endpoint URL', selects 'None' for security, 'Anonymous' for authentication, provides at least one valid node definition (e.g., 'ns=2;s=Server.ServerStatus.CurrentTime, ServerTime'), and clicks 'Test Connection'

### 3.2.6 Then

A 'Connection successful' message is displayed.

### 3.2.7 And

The Administrator clicks 'Save' and the connector is successfully created and listed.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Successfully test and save an OPC UA connector with Username/Password authentication

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

The OPC UA configuration form is displayed

### 3.3.5 When

The Administrator provides all valid connection details including correct credentials and clicks 'Test Connection'

### 3.3.6 Then

A 'Connection successful' message is displayed and the connector can be saved.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Connection test fails due to an invalid endpoint

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

The OPC UA configuration form is displayed

### 3.4.5 When

The Administrator enters an unreachable or malformed 'Endpoint URL' and clicks 'Test Connection'

### 3.4.6 Then

A user-friendly error message is displayed, such as 'Connection failed: The endpoint is unreachable or invalid'.

### 3.4.7 And

The 'Save' button remains disabled.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Connection test fails due to authentication error

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

The OPC UA configuration is set to use Username/Password authentication

### 3.5.5 When

The Administrator enters incorrect credentials and clicks 'Test Connection'

### 3.5.6 Then

A user-friendly error message is displayed, such as 'Connection failed: Authentication error (BadUserOrPassword)'.

### 3.5.7 And

The 'Save' button remains disabled.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Connection test fails due to a non-existent node

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

The OPC UA configuration form has valid connection and authentication details

### 3.6.5 When

The Administrator enters a Node ID that does not exist on the target server and clicks 'Test Connection'

### 3.6.6 Then

A user-friendly error message is displayed, such as 'Connection successful, but node validation failed: Node 'ns=X;s=InvalidNode' not found'.

### 3.6.7 And

The 'Save' button remains disabled.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Report job successfully ingests data from an OPC UA connector

### 3.7.3 Scenario Type

Happy_Path

### 3.7.4 Given

A report job is configured to use a valid OPC UA connector with two node definitions: 'ns=2;s=Temp, Temperature' and 'ns=2;s=Pressure, Pressure'

### 3.7.5 When

The report job is executed

### 3.7.6 Then

The data ingestion step successfully reads the current values for both nodes from the OPC UA server.

### 3.7.7 And

The data passed to the transformation engine is a single JSON object containing key-value pairs derived from the node aliases and their values, e.g., {"Temperature": 25.5, "Pressure": 101.3}.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Report job fails gracefully if the OPC UA server is unavailable

### 3.8.3 Scenario Type

Error_Condition

### 3.8.4 Given

A scheduled report job uses an OPC UA connector

### 3.8.5 When

The job runs, but the target OPC UA server is offline

### 3.8.6 Then

The system retries the connection according to the configured resiliency policy.

### 3.8.7 And

The job execution log contains a detailed error message indicating the connection failure.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Dropdown for 'Security Policy' (e.g., None, Basic256Sha256)
- Dropdown for 'Message Security Mode' (e.g., None, Sign, SignAndEncrypt)
- Dropdown for 'Authentication Method' (e.g., Anonymous, Username/Password)
- Conditional text input for 'Username'
- Conditional password input for 'Password'
- Text area for 'Node Definitions', with placeholder text showing the format 'NodeId, Alias' (e.g., 'ns=2;s=MyDevice.Temperature, Temp_C')
- 'Test Connection' button
- Status display area for connection test feedback (success or error messages)

## 4.2.0 User Interactions

- Selecting an 'Authentication Method' dynamically shows/hides the relevant credential fields.
- The 'Save' button is disabled until a successful 'Test Connection' has been completed in the current session.
- Clicking 'Test Connection' shows a loading indicator and then displays a clear success or detailed error message.

## 4.3.0 Display Requirements

- Clear labels for all configuration fields.
- Help text or tooltips explaining the 'Node Definitions' format.

## 4.4.0 Accessibility Needs

- All form fields must have associated labels for screen readers.
- Connection test feedback must be conveyed in a way accessible to users of assistive technologies (e.g., using ARIA live regions).

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-OPC-001

### 5.1.2 Rule Description

The connector must support the format 'NodeId, Alias' in the Node Definitions field, where the Alias is used as the key in the resulting JSON object. If no alias is provided, the full NodeId string should be used as the key.

### 5.1.3 Enforcement Point

Data Ingestion Logic

### 5.1.4 Violation Handling

N/A - Parsing logic should handle both cases.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-OPC-002

### 5.2.2 Rule Description

During a report run, all specified nodes are read in a single batch request to the server for efficiency. The result is collated into a single JSON object for downstream processing.

### 5.2.3 Enforcement Point

Data Ingestion Logic

### 5.2.4 Violation Handling

If any single node read fails, the entire ingestion step fails for that job execution to ensure data integrity.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-037

#### 6.1.1.2 Dependency Reason

Requires the core framework for dynamically rendering connector-specific UI elements.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-038

#### 6.1.2.2 Dependency Reason

Requires the backend infrastructure and UI pattern for the 'Test Connection' functionality.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-039

#### 6.1.3.2 Dependency Reason

Requires the business rule enforcement that prevents saving a connector before a successful test.

## 6.2.0.0 Technical Dependencies

- A .NET OPC UA client library, such as the official OPCFoundation.NetStandard.Opc.Ua NuGet package.
- The system's internal IConnector interface and plug-in loading mechanism must be defined and implemented.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

- Requires access to a configurable OPC UA test server during development and for automated integration testing. This is a critical dependency.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- A connection test should time out after 15 seconds.
- Data ingestion for up to 100 nodes should complete within 5 seconds under normal network conditions.

## 7.2.0.0 Security

- Passwords and other secrets must be stored encrypted at rest, following the system's secret management strategy (US-SEC-MGMT).
- The implementation must correctly handle OPC UA security policies and encryption to protect data in transit.

## 7.3.0.0 Usability

- Error messages for connection failures must be clear and actionable for an Administrator.
- The configuration process should be intuitive for a user familiar with OPC UA concepts.

## 7.4.0.0 Accessibility

- The configuration UI must adhere to WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The connector must be compatible with OPC UA servers that adhere to the UA 1.04 specification.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

High

## 8.2.0.0 Complexity Factors

- OPC UA is a complex protocol with a steep learning curve compared to standard database or file connectors.
- Correctly implementing the various security and authentication modes is non-trivial and security-critical.
- Requires setting up and maintaining a dedicated OPC UA test server, which is a significant effort.
- Error handling needs to be robust to cover a wide range of network, security, and server-side issues.

## 8.3.0.0 Technical Risks

- Underestimation of the effort required to learn and correctly use the OPC UA client library.
- Inability to set up a test server that covers all required security and data scenarios, leading to gaps in testing.
- Potential for performance issues if session management with the OPC UA server is not handled efficiently.

## 8.4.0.0 Integration Points

- Integrates with the system's core connector framework.
- Integrates with the system's configuration UI.
- Integrates with the Quartz.NET job execution engine.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Test connection and data retrieval for each supported security policy and authentication method.
- Test with a list of multiple valid nodes.
- Test with a mix of valid and invalid nodes.
- Test behavior when the target server is offline or unreachable.
- Test end-to-end report generation using the OPC UA connector.

## 9.3.0.0 Test Data Needs

- An OPC UA test server with a known set of nodes, data types, and access permissions.
- Valid user credentials for the test server.
- Server configuration that allows testing of different security policies (None, Basic256Sha256, etc.).

## 9.4.0.0 Testing Tools

- An OPC UA simulation server (e.g., Prosys Simulation Server, UA .NET Standard Sample Server).

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >80% coverage for new code
- Integration tests against a live OPC UA test server are implemented and passing for all ACs
- User interface reviewed and approved by UX/Product Owner
- Security review completed for handling of credentials and secure communication
- Documentation for configuring the OPC UA connector is written and added to the user guide
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

13

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- A research spike may be required prior to this story to evaluate OPC UA libraries and test server options.
- The availability of the OPC UA test server is a hard dependency and must be confirmed before starting the sprint.

## 11.4.0.0 Release Impact

- This is a major feature required for penetrating the industrial/manufacturing market segment.

