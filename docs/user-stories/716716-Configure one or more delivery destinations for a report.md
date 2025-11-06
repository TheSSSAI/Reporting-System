# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-057 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Configure one or more delivery destinations for a ... |
| As A User Story | As an Administrator, I want to configure one or mo... |
| User Persona | Administrator |
| Business Value | Automates the final and most critical step of the ... |
| Functional Area | Report Configuration |
| Story Theme | Report Generation & Delivery |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Add a new delivery destination to a report

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

An Administrator is on the 'Delivery' step of the report configuration wizard

### 3.1.5 When

they select a destination type (e.g., 'Email (SMTP)') from a dropdown and fill in all required, valid configuration details

### 3.1.6 Then

the new destination is added to a list of configured destinations for the current report, and the form is cleared or hidden, ready for another destination to be added.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Dynamic form rendering for different destination types

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

An Administrator is on the 'Delivery' step

### 3.2.5 When

they select 'Amazon S3' as the destination type

### 3.2.6 Then

the UI displays a form with fields specific to S3: 'Bucket Name', 'Region', 'Access Key ID', 'Secret Access Key', and 'Object Key/Path'.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Dynamic form rendering for Email (SMTP) destination

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

An Administrator is on the 'Delivery' step

### 3.3.5 When

they select 'Email (SMTP)' as the destination type

### 3.3.6 Then

the UI displays a form with fields specific to SMTP: 'Server Host', 'Port', 'Use SSL/TLS', 'Username', 'Password', 'Sender Address', 'Recipients (To, CC, BCC)', 'Subject', and 'Body'.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Configure multiple destinations for a single report

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

An Administrator has already added an 'Email (SMTP)' destination

### 3.4.5 When

they then select 'Local/Network Storage', provide a valid UNC path, and add it

### 3.4.6 Then

the list of configured destinations now shows both the Email and the Network Storage destinations.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Test a delivery destination with valid configuration

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

An Administrator has filled out the form for a delivery destination with valid credentials and settings

### 3.5.5 When

they click the 'Test Delivery' button

### 3.5.6 Then

a success message is displayed, confirming that the connection and authentication were successful (e.g., 'Test email sent successfully' or 'Successfully connected to S3 bucket').

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Attempt to test a destination with invalid configuration

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

An Administrator has filled out the form for an SFTP destination with an incorrect password

### 3.6.5 When

they click the 'Test Delivery' button

### 3.6.6 Then

a user-friendly error message is displayed, indicating the failure reason (e.g., 'Authentication failed. Please check username and password.') and providing relevant diagnostic information.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Input validation for destination forms

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

An Administrator is configuring an Email destination

### 3.7.5 When

they enter an invalid email address in the 'Recipients' field and try to add the destination

### 3.7.6 Then

a validation error message is displayed next to the field, and the destination is not added to the list.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Edit an existing delivery destination

### 3.8.3 Scenario Type

Happy_Path

### 3.8.4 Given

A report has a configured destination for a network path '\\server\old_share'

### 3.8.5 When

the Administrator clicks the 'Edit' icon for that destination, changes the path to '\\server\new_share', and saves the changes

### 3.8.6 Then

the destination's configuration is updated in the list.

## 3.9.0 Criteria Id

### 3.9.1 Criteria Id

AC-009

### 3.9.2 Scenario

Remove a delivery destination

### 3.9.3 Scenario Type

Happy_Path

### 3.9.4 Given

A report has two configured destinations (Email and SFTP)

### 3.9.5 When

the Administrator clicks the 'Delete' icon next to the SFTP destination and confirms the action

### 3.9.6 Then

the SFTP destination is removed from the list, leaving only the Email destination.

## 3.10.0 Criteria Id

### 3.10.1 Criteria Id

AC-010

### 3.10.2 Scenario

Secure handling of credentials in UI

### 3.10.3 Scenario Type

Edge_Case

### 3.10.4 Given

An Administrator is editing a previously saved destination that has a password or secret key

### 3.10.5 When

the configuration form is displayed

### 3.10.6 Then

the password/secret key field is either empty or masked with placeholder characters (e.g., '********'), requiring re-entry to update, and is never displayed in plaintext.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated section or step within the report configuration wizard labeled 'Delivery Destinations'.
- A dropdown menu to select the destination type (e.g., 'Email', 'Amazon S3', 'Azure Blob', 'FTP/SFTP', 'Local/Network Storage', 'REST API').
- A dynamic form area that updates to show input fields relevant to the selected destination type.
- An 'Add Destination' button to add the currently configured destination to the report.
- A list view displaying all destinations configured for the report, with identifying information (e.g., type and email recipient/path).
- 'Edit' and 'Delete' icons/buttons for each item in the destination list.
- A 'Test Delivery' button within the dynamic form to validate the current settings before adding.

## 4.2.0 User Interactions

- Selecting a type from the dropdown immediately renders the corresponding form.
- Clicking 'Add Destination' adds the item to the list below and resets the form.
- Clicking 'Delete' on a list item prompts for confirmation before removal.
- Clicking 'Edit' populates the form with the selected destination's data for modification.

## 4.3.0 Display Requirements

- Clear validation messages for invalid inputs (e.g., 'Invalid email format', 'Path not found').
- Clear success or failure notifications for the 'Test Delivery' action.
- Tooltips or helper text for complex fields (e.g., explaining CRON syntax or object key formatting).

## 4.4.0 Accessibility Needs

- All form fields must have associated labels.
- UI controls must be keyboard navigable.
- Feedback messages (success/error) must be announced by screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A report configuration must have at least one delivery destination to be scheduled for automatic execution.

### 5.1.3 Enforcement Point

During the final save/validation of the report configuration.

### 5.1.4 Violation Handling

The system prevents saving a scheduled report with zero destinations and displays a validation error: 'A scheduled report must have at least one delivery destination.'

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

All sensitive credentials (passwords, API keys, secret keys) must be stored encrypted at rest.

### 5.2.3 Enforcement Point

Backend data access layer when saving a delivery configuration.

### 5.2.4 Violation Handling

The system must use the .NET Data Protection APIs (DPAPI) or equivalent to encrypt data before persisting to the SQLite database. Plaintext storage is a critical failure.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

- {'story_id': 'US-051', 'dependency_reason': 'This story implements the UI for configuring delivery destinations, which is a step within the report creation wizard provided by US-051.'}

## 6.2.0 Technical Dependencies

- Backend API endpoints for CRUD operations on delivery configurations.
- Secure credential storage mechanism (e.g., .NET Secret Manager / Windows Certificate Store integration).
- Frontend state management library (Zustand) to handle the dynamic form and list of destinations.
- Third-party .NET libraries for each delivery type: MailKit (SMTP), AWS SDK, Azure SDK, SSH.NET (SFTP), etc.

## 6.3.0 Data Dependencies

- The `ReportConfiguration` data entity must exist in the database to associate delivery destinations with.

## 6.4.0 External Dependencies

*No items available*

# 7.0.0 Non Functional Requirements

## 7.1.0 Performance

- The 'Test Delivery' action should time out and return an error after 30 seconds to prevent locking up the UI.

## 7.2.0 Security

- All credentials entered in the UI must be transmitted to the backend over HTTPS.
- Credentials must be encrypted at rest in the configuration database.
- The UI must never display stored secrets in plaintext when editing a configuration.

## 7.3.0 Usability

- Error messages from failed delivery tests must be clear and actionable for a non-technical administrator.
- The process of adding, editing, and removing destinations should be intuitive and require minimal training.

## 7.4.0 Accessibility

- The UI must comply with WCAG 2.1 Level AA standards.

## 7.5.0 Compatibility

- The UI must function correctly on the latest stable versions of Chrome, Firefox, and Edge.

# 8.0.0 Implementation Considerations

## 8.1.0 Complexity Assessment

Medium

## 8.2.0 Complexity Factors

- Implementing distinct backend logic for 6 different delivery types (SMTP, S3, Azure, FTP/SFTP, Local, API).
- Creating a flexible and robust dynamic form on the frontend to handle the different configuration schemas.
- Ensuring secure handling and storage of various credential types (passwords, API keys, SSH keys).
- Setting up a reliable integration testing environment for all delivery types.

## 8.3.0 Technical Risks

- Firewall or network configuration issues at customer sites may complicate connectivity for delivery destinations, leading to support challenges. Error messages must be very clear.
- Insecure implementation of credential storage could lead to a major security vulnerability.

## 8.4.0 Integration Points

- External Systems: SMTP servers, AWS S3, Azure Blob Storage, FTP/SFTP servers, customer-defined REST APIs.
- Internal Systems: The Report Generation engine, which will invoke the configured delivery services after a report is created.

# 9.0.0 Testing Requirements

## 9.1.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0 Test Scenarios

- Verify each delivery type can be configured, tested, and saved successfully.
- Verify a report with multiple destinations delivers the file to all configured locations.
- Test failure conditions for each delivery type (e.g., wrong password, invalid host, insufficient permissions) and verify correct error handling.
- End-to-end test: Create a report, configure an email destination, run the report, and verify the email is received with the correct attachment.
- Security test: Verify that stored credentials cannot be retrieved in plaintext via API or direct database access.

## 9.3.0 Test Data Needs

- Valid and invalid credentials for test instances of each delivery service (e.g., a test SMTP server, an S3 bucket, an SFTP server).
- Sample report files of various formats (PDF, CSV, JSON) to use as test attachments/payloads.

## 9.4.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Docker for spinning up containerized test dependencies (e.g., `smtp4dev`, `vsftpd`, `minio`).
- Jest/React Testing Library for frontend component tests.
- Playwright or Cypress for E2E tests.

# 10.0.0 Definition Of Done

- All acceptance criteria validated and passing.
- Code reviewed and approved by team.
- Unit tests implemented for all backend delivery services and frontend components, achieving >80% coverage.
- Integration tests for all supported delivery types are implemented and passing against a test environment.
- User interface reviewed by a UX designer and approved.
- Security review of credential handling has been completed and any findings addressed.
- Documentation for configuring each delivery type has been written for the User Guide.
- Story deployed and verified in staging environment.

# 11.0.0 Planning Information

## 11.1.0 Story Points

13

## 11.2.0 Priority

🔴 High

## 11.3.0 Sprint Considerations

- This is a foundational feature for making the product useful. It is a high priority.
- Consider splitting the implementation by delivery type if the full scope is too large for a single sprint. For example, a first story could deliver the UI framework plus Email and Local File delivery, with subsequent stories for Cloud, FTP, and API.

## 11.4.0 Release Impact

This feature is critical for the Minimum Viable Product (MVP). The product cannot be considered feature-complete without the ability to deliver reports.

