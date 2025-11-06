# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-038 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Test a data connector's connection |
| As A User Story | As an Administrator, I want to click a 'Test Conne... |
| User Persona | Administrator |
| Business Value | Reduces configuration errors, prevents future repo... |
| Functional Area | Data Ingestion |
| Story Theme | Connector Configuration and Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful connection test for a relational database

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

an Administrator is on the 'Create/Edit Connector' page for a supported database (e.g., SQL Server)

### 3.1.5 When

they enter valid connection parameters (server, database, username, password) and click the 'Test Connection' button

### 3.1.6 Then

a success message, such as 'Connection successful!', is displayed to the user within 30 seconds.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Successful connection test for a file system path

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

an Administrator is on the 'Create/Edit Connector' page for a file system source

### 3.2.5 When

they enter a valid and accessible local or network (UNC) path and click the 'Test Connection' button

### 3.2.6 Then

a success message, such as 'Path is accessible.', is displayed to the user.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Failed connection test due to invalid credentials

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

an Administrator is on the 'Create/Edit Connector' page for a database

### 3.3.5 When

they enter a valid server address but an incorrect username or password and click the 'Test Connection' button

### 3.3.6 Then

a user-friendly error message is displayed, such as 'Authentication failed. Please check your username and password.'

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Failed connection test due to network issue

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

an Administrator is on the 'Create/Edit Connector' page for any network-based source (Database, OPC UA)

### 3.4.5 When

they enter an invalid hostname or a valid hostname that is blocked by a firewall and click the 'Test Connection' button

### 3.4.6 Then

a user-friendly error message is displayed, such as 'Connection failed. The host could not be reached. Please check the server address and network/firewall settings.'

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Failed connection test due to insufficient permissions

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

an Administrator is on the 'Create/Edit Connector' page for a database

### 3.5.5 When

they enter credentials for a user that can connect but lacks permissions to access the specified database and click the 'Test Connection' button

### 3.5.6 Then

a user-friendly error message is displayed, such as 'Connection successful, but access to the specified database was denied.'

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

UI state changes during connection test

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

an Administrator is on any 'Create/Edit Connector' page

### 3.6.5 When

they click the 'Test Connection' button

### 3.6.6 Then

the 'Test Connection' button becomes disabled and a loading indicator is displayed.

### 3.6.7 And

once the test completes (success or failure), the button is re-enabled and the loading indicator is hidden.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Connection test timeout

### 3.7.3 Scenario Type

Edge_Case

### 3.7.4 Given

an Administrator is testing a connection to a server that is online but unresponsive

### 3.7.5 When

they click the 'Test Connection' button and the connection attempt does not complete within 30 seconds

### 3.7.6 Then

the connection attempt is aborted and a timeout error message is displayed, such as 'Connection timed out. The server is not responding.'

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Error messages do not expose sensitive information

### 3.8.3 Scenario Type

Error_Condition

### 3.8.4 Given

a connection test fails for any reason

### 3.8.5 When

the error message is displayed to the user

### 3.8.6 Then

the message must not contain any part of the password, secret key, or full connection string.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A primary action button labeled 'Test Connection' on all connector configuration forms.
- A non-intrusive loading indicator (e.g., spinner) that appears next to the button during the test.
- A dedicated area or a toast notification component to display success or error messages.

## 4.2.0 User Interactions

- Clicking the 'Test Connection' button triggers the validation process.
- The button must be disabled during the test to prevent multiple concurrent requests.
- The success/error message should be clear, concise, and remain visible until dismissed by the user or a timeout.

## 4.3.0 Display Requirements

- Success messages should be visually distinct from error messages (e.g., using green for success, red for error).
- Error messages must provide actionable feedback to help the user diagnose the problem.

## 4.4.0 Accessibility Needs

- The 'Test Connection' button must have a clear, accessible label.
- The loading state must be communicated to screen readers (e.g., using `aria-busy`).
- The result message area must use `aria-live` attributes so that screen readers announce the outcome of the test automatically.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'A connection test must be performed using the current, potentially unsaved, values in the configuration form.', 'enforcement_point': 'Backend API endpoint for testing connections.', 'violation_handling': 'N/A - System design must adhere to this rule.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-034

#### 6.1.1.2 Dependency Reason

Provides the UI form for database connectors where the 'Test Connection' button will be placed.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-035

#### 6.1.2.2 Dependency Reason

Provides the UI form for file system connectors where the 'Test Connection' button will be placed.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-036

#### 6.1.3.2 Dependency Reason

Provides the UI form for OPC UA connectors where the 'Test Connection' button will be placed.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-113

#### 6.1.4.2 Dependency Reason

Defines the `IConnector` interface which must include a `TestConnection(configJson)` method for the backend to call.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core for the backend API endpoint.
- React for the frontend UI component.
- The plug-in architecture for dynamically loading and invoking the correct connector logic.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

- Availability of target data sources (databases, file shares, OPC UA servers) for integration testing.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The P99 latency for a connection test to a responsive, local-network data source should be under 5 seconds.
- The connection test must have a hard timeout of 30 seconds to prevent the UI from appearing frozen.

## 7.2.0.0 Security

- All connection test requests from the client to the server must be sent over HTTPS.
- Sensitive credentials sent in the request body must be handled securely in memory on the server and never logged.
- Error messages returned to the client must be sanitized to prevent leaking internal system details or secrets.

## 7.3.0.0 Usability

- Feedback from the test must be immediate and unambiguous.
- Error messages should be helpful and guide the user toward a solution.

## 7.4.0.0 Accessibility

- All UI elements and interactions must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires a new, generic API endpoint that can handle various connector types.
- Requires implementing specific connection logic for each built-in connector type.
- Requires robust error handling to catch and translate a wide range of network, authentication, and permission exceptions into user-friendly messages.
- Frontend state management is needed to handle the loading and feedback states.

## 8.3.0.0 Technical Risks

- Difficulty in creating generic, yet helpful, error messages across different database providers and protocols.
- Potential for long-running connection attempts to remote or slow networks impacting user experience.

## 8.4.0.0 Integration Points

- Backend API: A new endpoint like `POST /api/v1/connectors/test`.
- Connector Plug-in Interface: The `IConnector.TestConnection(configJson)` method.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Test successful connection for each built-in connector type.
- Test connection failure due to invalid credentials for each connector type.
- Test connection failure due to network/host resolution errors.
- Test connection failure due to insufficient permissions.
- Test connection timeout scenario.
- Verify UI state transitions (loading, success, error) are correct.

## 9.3.0.0 Test Data Needs

- Access to live (or containerized) instances of SQL Server, MySQL, and PostgreSQL.
- Valid and invalid credentials for each test database.
- A test network share (UNC path) with specific read permissions.
- A test OPC UA server instance.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- Docker for spinning up test database instances for integration tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing for all built-in connector types.
- Code reviewed and approved by team.
- Unit tests implemented for frontend and backend logic, achieving >80% coverage.
- Integration tests implemented for each connector type, covering success and common failure cases.
- User interface reviewed and approved by a UX designer or product owner.
- Performance requirements (timeout) verified.
- Security requirements (no leaked secrets) validated via code review and testing.
- Documentation for the `IConnector.TestConnection` method is added to the PDK.
- Story deployed and verified in staging environment.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story provides significant value and should be prioritized early in the development of the connector configuration feature.
- Work can be parallelized: one developer on the frontend/API, while others implement the `TestConnection` logic for each connector.

## 11.4.0.0 Release Impact

This is a core usability feature for system configuration. Its absence would be a significant negative for the user experience.

