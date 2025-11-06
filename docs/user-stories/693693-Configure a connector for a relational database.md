# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-034 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Configure a connector for a relational database |
| As A User Story | As an Administrator, I want to configure and save ... |
| User Persona | Administrator |
| Business Value | Enables the system to ingest data from the most co... |
| Functional Area | Data Ingestion |
| Story Theme | Data Input Connectors |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Displaying the Relational Database Connector Form

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an authenticated Administrator on the 'Create Connector' page

### 3.1.5 When

I select 'Relational Database' as the connector type

### 3.1.6 Then

A form is displayed with fields for 'Name', 'Description', 'Database Type' (dropdown with SQL Server, MySQL, PostgreSQL), 'Server', 'Port', 'Database Name', 'Username', 'Password', and a multi-line 'SQL Query' text area.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Successfully Creating a SQL Server Connector

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am on the relational database connector form and have selected 'SQL Server'

### 3.2.5 When

I fill in all required fields with valid SQL Server details, successfully test the connection, and click 'Save'

### 3.2.6 Then

The connector configuration is saved, my password is encrypted at rest, and I am redirected to the connector list where the new 'SQL Server' connector is visible.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Successfully Creating a MySQL Connector

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I am on the relational database connector form and have selected 'MySQL'

### 3.3.5 When

I fill in all required fields with valid MySQL details, successfully test the connection, and click 'Save'

### 3.3.6 Then

The connector configuration is saved, my password is encrypted at rest, and I am redirected to the connector list where the new 'MySQL' connector is visible.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Successfully Creating a PostgreSQL Connector

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

I am on the relational database connector form and have selected 'PostgreSQL'

### 3.4.5 When

I fill in all required fields with valid PostgreSQL details, successfully test the connection, and click 'Save'

### 3.4.6 Then

The connector configuration is saved, my password is encrypted at rest, and I am redirected to the connector list where the new 'PostgreSQL' connector is visible.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Attempting to Save with Missing Required Fields

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am on the relational database connector form

### 3.5.5 When

I attempt to save the configuration without filling in a required field, such as 'Server'

### 3.5.6 Then

The form submission is prevented, and a clear, inline validation message appears next to the empty required field.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Attempting to Save with an Untested Connection

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

I have filled out the connector form but have not yet clicked the 'Test Connection' button or the last test failed

### 3.6.5 When

I click the 'Save' button

### 3.6.6 Then

The save action is blocked, and a message is displayed informing me that a successful connection test is required before saving.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Connection Test Fails Due to Invalid Credentials

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

I have filled out the form with an incorrect username or password

### 3.7.5 When

I click 'Test Connection'

### 3.7.6 Then

A user-friendly error message is displayed, such as 'Authentication failed. Please check the username and password.', and the connection status is marked as 'Failed'.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Connection Test Fails Due to Network/Server Issues

### 3.8.3 Scenario Type

Error_Condition

### 3.8.4 Given

I have filled out the form with an incorrect server address, port, or a firewall is blocking the connection

### 3.8.5 When

I click 'Test Connection'

### 3.8.6 Then

A user-friendly error message is displayed, such as 'Cannot connect to the database server. Please verify the server address, port, and network accessibility.', and the connection status is marked as 'Failed'.

## 3.9.0 Criteria Id

### 3.9.1 Criteria Id

AC-009

### 3.9.2 Scenario

Connection Test Fails Due to Insufficient SQL Permissions

### 3.9.3 Scenario Type

Error_Condition

### 3.9.4 Given

The connection details are correct, but the database user lacks permissions for the tables in the SQL query

### 3.9.5 When

I click 'Test Connection'

### 3.9.6 Then

A user-friendly error message is displayed indicating a permissions issue, such as 'Query execution failed due to insufficient permissions.', and the connection status is marked as 'Failed'.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dropdown menu to select 'Database Type' (SQL Server, MySQL, PostgreSQL).
- Standard text input fields for Name, Description, Server, Port, Database Name, Username.
- A password input field that masks characters for the 'Password'.
- A large, multi-line text area for the 'SQL Query', preferably using a monospaced font.
- A 'Test Connection' button with a visible loading state (e.g., spinner).
- A 'Save' button.
- A dedicated area to display the status of the connection test (e.g., a colored alert box showing success or failure).

## 4.2.0 User Interactions

- Selecting a database type from the dropdown populates the form.
- Inline validation messages appear immediately if a required field is left blank and the user tries to save.
- Clicking 'Test Connection' disables the button and shows a loading indicator until a result is returned.
- The 'Save' button is disabled until a successful connection test has been completed in the current session.

## 4.3.0 Display Requirements

- Error messages from the connection test must be clear, user-friendly, and provide actionable advice.
- Upon saving, a success notification should be displayed.
- When editing an existing connector, the password field should be blank and only allow for updating, not viewing the existing password.

## 4.4.0 Accessibility Needs

- All form fields must have corresponding `<label>` tags.
- Validation errors must be programmatically associated with their respective input fields using `aria-describedby`.
- All interactive elements (buttons, inputs) must be keyboard-navigable and operable.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A relational database connector configuration cannot be saved without a successful connection test.

### 5.1.3 Enforcement Point

Frontend UI (disabling save button) and Backend API (validation check on save request).

### 5.1.4 Violation Handling

The UI prevents the save action and displays an informative message. The API rejects the request with a 400 Bad Request error.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

All sensitive credentials (e.g., passwords) must be encrypted before being persisted.

### 5.2.3 Enforcement Point

Backend service layer before saving to the SQLite database.

### 5.2.4 Violation Handling

The operation fails and an error is logged if encryption fails. Credentials are never stored in plaintext.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-038

#### 6.1.1.2 Dependency Reason

This story implements the 'Test Connection' button functionality, which is a mandatory step in the workflow of creating a connector.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-039

#### 6.1.2.2 Dependency Reason

This story defines the business rule that a successful test is required before saving, which directly governs the behavior of the save action in this story.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-098

#### 6.1.3.2 Dependency Reason

The save action for a connector configuration must trigger the versioning system defined in this story.

## 6.2.0.0 Technical Dependencies

- Backend API endpoints for creating, validating, and storing connector configurations.
- System's secret management service for encrypting and decrypting database passwords (as per SRS 3.3).
- .NET data provider libraries for SQL Server, MySQL, and PostgreSQL (e.g., Microsoft.Data.SqlClient, MySql.Data, Npgsql).
- Frontend form components from the MUI v5 library.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

- Availability of test database instances (SQL Server, MySQL, PostgreSQL) for integration and E2E testing.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The 'Test Connection' operation must time out after 30 seconds to prevent the UI from appearing frozen.

## 7.2.0.0 Security

- Database credentials entered by the user must be transmitted over HTTPS.
- Stored credentials must be encrypted at rest using the .NET Data Protection APIs (DPAPI).
- The SQL query input must be parameterized on the backend to prevent SQL injection, even though the user provides the query themselves (defense-in-depth).
- Error messages returned to the UI should not expose sensitive system details (e.g., full stack traces or internal server paths).

## 7.3.0.0 Usability

- Error messages should be clear and guide the user toward a solution.
- The form layout should be logical and follow a natural flow.

## 7.4.0.0 Accessibility

- The user interface must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The Control Panel interface must function correctly on the latest stable versions of Chrome, Firefox, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementing a backend strategy or factory pattern to handle three different database providers and their unique connection string formats and exception types.
- Creating robust and secure handling of database credentials, including integration with the encryption service.
- Translating a wide variety of potential database and network exceptions into a small set of user-friendly error messages.
- Managing the frontend state to enforce the 'test-before-save' workflow.

## 8.3.0.0 Technical Risks

- Differences in error handling and exception types across the three database providers may require custom logic for each.
- Ensuring the host environment has the necessary database drivers (or .NET providers) installed and accessible.

## 8.4.0.0 Integration Points

- The system's central configuration database (SQLite) for storing the connector definition.
- The system's secret management/encryption service.
- External relational databases (SQL Server, MySQL, PostgreSQL) over the network.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify form validation for all fields.
- Test the successful creation and saving of a connector for each of the three database types.
- Test failure scenarios: invalid host, invalid port, incorrect credentials, insufficient permissions, invalid SQL syntax.
- Verify that the password is encrypted in the database after saving.
- Test the UI behavior for the 'test-before-save' rule.

## 9.3.0.0 Test Data Needs

- Valid and invalid connection credentials for test instances of SQL Server, MySQL, and PostgreSQL.
- Sample SQL queries, both valid and syntactically incorrect.
- A database user with limited permissions to test permission-denied errors.

## 9.4.0.0 Testing Tools

- xUnit and Moq for backend unit tests.
- Jest and React Testing Library for frontend unit tests.
- Docker to spin up database instances for automated integration tests.
- A framework like Playwright or Cypress for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing for all three database types (SQL Server, MySQL, PostgreSQL).
- Code reviewed and approved by team.
- Unit tests implemented with >80% coverage for new backend and frontend code.
- Integration tests that connect to live instances of all three database types are implemented and passing.
- User interface reviewed and approved by a UX designer or product owner.
- Connection timeout performance requirement is verified.
- Security requirements, especially credential encryption and secure error handling, are validated.
- Documentation for configuring relational database connectors is updated.
- Story deployed and verified in staging environment.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational feature for data ingestion and blocks many other reporting-related stories. It should be prioritized early.
- Requires coordination with infrastructure to ensure test database instances are available for the development and CI/CD environments.

## 11.4.0.0 Release Impact

- This story is critical for the minimum viable product (MVP) release. The product has limited value without the ability to connect to standard databases.

