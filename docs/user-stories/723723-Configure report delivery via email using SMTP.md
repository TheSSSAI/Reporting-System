# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-064 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Configure report delivery via email using SMTP |
| As A User Story | As an Administrator, I want to configure a report ... |
| User Persona | Administrator |
| Business Value | Automates the distribution of reports to stakehold... |
| Functional Area | Report Delivery Configuration |
| Story Theme | Report Generation & Delivery |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

UI for adding an email delivery destination

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

An Administrator is editing a report configuration in the Control Panel and navigates to the 'Delivery Destinations' step.

### 3.1.5 When

The Administrator adds a new destination and selects 'Email (SMTP)' from the list of available types.

### 3.1.6 Then

A configuration form is displayed with sections for 'SMTP Server Settings' and 'Email Details'.

### 3.1.7 Validation Notes

Verify the UI component for email configuration appears when selected.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Successfully test and save a valid SMTP configuration

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The email delivery configuration form is displayed.

### 3.2.5 When

The Administrator enters valid SMTP server details, recipient information, clicks the 'Test Email' button, receives a success notification, and then saves the report configuration.

### 3.2.6 Then

The system sends a test email successfully, and the email delivery destination is saved as part of the report configuration.

### 3.2.7 Validation Notes

Requires a test SMTP server. Verify a test email is received and the configuration is persisted in the database.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Successful automated email delivery after report generation

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

A report job is configured with a valid and enabled email delivery destination.

### 3.3.5 When

The scheduled or manually triggered report job runs and generates the report file successfully.

### 3.3.6 Then

An email is sent to all configured 'To', 'CC', and 'BCC' recipients, the email has the correct 'From' address, subject, and body, the generated report is attached, and the job execution log records a 'Succeeded' status for this delivery step.

### 3.3.7 Validation Notes

Verify the email is received by all recipients with the correct content and attachment. Check the job log for the success entry.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Handle invalid form input

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

The email delivery configuration form is displayed.

### 3.4.5 When

The Administrator enters an invalid email address in the 'To' field or a non-numeric value in the 'Port' field and attempts to save.

### 3.4.6 Then

The UI displays clear, field-level validation errors, and the configuration is not saved.

### 3.4.7 Validation Notes

Test various invalid inputs like malformed emails, empty required fields, and incorrect data types.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Handle failed test email due to connection error

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

The email delivery configuration form is displayed.

### 3.5.5 When

The Administrator enters an incorrect server host or port and clicks 'Test Email'.

### 3.5.6 Then

The system attempts to connect, fails, and displays a specific error message to the user, such as 'Connection failed. Please check the server host, port, and firewall settings.'

### 3.5.7 Validation Notes

Simulate a connection failure by using a wrong hostname or a port that is not open.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Handle failed test email due to authentication error

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

The email delivery configuration form is displayed.

### 3.6.5 When

The Administrator enters a correct server host and port but an incorrect username or password and clicks 'Test Email'.

### 3.6.6 Then

The system attempts to authenticate, fails, and displays a specific error message, such as 'Authentication failed. Please check your username and password.'

### 3.6.7 Validation Notes

Use valid server details with invalid credentials to trigger an authentication failure.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Handle failed delivery during a report job run

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

A report job is configured with an email destination whose credentials have become invalid since it was last tested.

### 3.7.5 When

The report job runs and generates the report file successfully.

### 3.7.6 Then

The system attempts to send the email and fails, the job execution log records a 'Failed' status for the email delivery step with the specific SMTP error message, and the overall job status is marked as 'Failed'.

### 3.7.7 Validation Notes

Configure a valid destination, then change the password on the mail server. Run the job and verify the job log and overall status.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Securely handle SMTP password

### 3.8.3 Scenario Type

Happy_Path

### 3.8.4 Given

The email delivery configuration form is displayed.

### 3.8.5 When

The Administrator enters a password in the SMTP password field.

### 3.8.6 Then

The password is masked in the UI, and upon saving, it is stored encrypted at rest in the configuration database.

### 3.8.7 Validation Notes

Inspect the UI to ensure the password is a password-type input. Inspect the database to confirm the value is encrypted and not in plaintext.

## 3.9.0 Criteria Id

### 3.9.1 Criteria Id

AC-009

### 3.9.2 Scenario

Support for multiple recipients

### 3.9.3 Scenario Type

Happy_Path

### 3.9.4 Given

The email delivery configuration form is displayed.

### 3.9.5 When

The Administrator enters multiple, comma-separated email addresses in the 'To', 'CC', and 'BCC' fields and the job runs.

### 3.9.6 Then

The email is successfully delivered to all specified recipients in the correct fields.

### 3.9.7 Validation Notes

Test with multiple addresses in each recipient field and verify receipt and email headers.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A new 'Email (SMTP)' option in the delivery destination type selector.
- A form with input fields for: SMTP Host, Port, Encryption (Dropdown: None, SSL/TLS, STARTTLS), Use Authentication (Checkbox).
- Conditional input fields for Username and Password that appear when 'Use Authentication' is checked.
- Input fields for: From Address, To Address(es), CC Address(es), BCC Address(es), Subject, and Body (Text Area).
- A 'Test Email' button.
- Toast notifications or inline messages for test success/failure.

## 4.2.0 User Interactions

- The password field must be masked.
- The 'Test Email' button should be disabled until all required server fields are filled.
- Clicking 'Test Email' should show a loading indicator and provide clear feedback on success or failure.
- Form fields should have client-side validation for required format (e.g., email, number).

## 4.3.0 Display Requirements

- Tooltips explaining each configuration field, e.g., explaining the difference between SSL/TLS and STARTTLS.
- Error messages from the test connection must be user-friendly and actionable.

## 4.4.0 Accessibility Needs

- All form inputs must have corresponding `<label>` tags.
- Validation errors must be programmatically associated with their respective input fields.
- The UI must be navigable and operable using only a keyboard.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

If any single delivery destination fails for a report job, the entire job shall be marked as 'Failed'.

### 5.1.3 Enforcement Point

Job Execution Engine, after all delivery attempts are complete.

### 5.1.4 Violation Handling

The job's final status is set to 'Failed' and a failure notification is sent if configured (US-075).

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

SMTP passwords must be stored encrypted at rest using the system's secret management facility.

### 5.2.3 Enforcement Point

Data Access Layer, when saving the connector configuration.

### 5.2.4 Violation Handling

N/A - System design must enforce this rule.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-051

#### 6.1.1.2 Dependency Reason

Provides the core report configuration wizard where delivery destinations are added.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-057

#### 6.1.2.2 Dependency Reason

Provides the framework for managing multiple types of delivery destinations, into which this email type will be plugged.

## 6.2.0.0 Technical Dependencies

- A .NET SMTP client library (e.g., MailKit) for handling email sending.
- The system's defined secret management service for storing passwords securely (as per SRS 3.3).
- The Quartz.NET job execution engine to invoke the delivery process.

## 6.3.0.0 Data Dependencies

- Requires a generated report file to exist before the delivery step can be executed.

## 6.4.0.0 External Dependencies

- Requires network connectivity from the host server to the customer's configured SMTP server on the specified port.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The connection to the SMTP server must time out after 30 seconds to prevent jobs from hanging.
- Email delivery should be performed asynchronously to avoid blocking the main job processing thread.

## 7.2.0.0 Security

- SMTP passwords must be encrypted at rest (DPAPI) and in transit (HTTPS for configuration).
- The system must support modern, secure email protocols like TLS 1.2+ for communication with the SMTP server.

## 7.3.0.0 Usability

- Error messages related to SMTP configuration and delivery must be clear, specific, and guide the user toward a solution.

## 7.4.0.0 Accessibility

- The configuration form must adhere to WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The implementation should be compatible with standard SMTP servers, including those requiring modern authentication methods.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Integrating a third-party SMTP library (e.g., MailKit).
- Handling various SMTP authentication and encryption schemes.
- Implementing a secure storage and retrieval mechanism for SMTP passwords.
- Creating a robust 'Test Email' API endpoint and corresponding frontend logic.
- Ensuring proper error handling and logging for a wide range of potential network and server issues.

## 8.3.0.0 Technical Risks

- Customer firewall rules may block outbound SMTP traffic, leading to support issues.
- Variability in SMTP server implementations may require handling of specific edge cases.

## 8.4.0.0 Integration Points

- Report Configuration Service (for saving settings).
- Job Execution Pipeline (for triggering delivery).
- Secret Management Service (for credentials).
- Logging Service (for recording outcomes).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Test with an open (no auth) SMTP server.
- Test with an SMTP server requiring SSL/TLS and authentication.
- Test with an SMTP server requiring STARTTLS and authentication.
- Test delivery failure scenarios (wrong host, wrong port, wrong credentials).
- Test with multiple recipients in all fields (To, CC, BCC).
- Test security of password storage by inspecting the database.

## 9.3.0.0 Test Data Needs

- Access to one or more test SMTP servers (e.g., Mailtrap.io, smtp4dev, or a corporate test server).
- Valid and invalid sets of credentials for testing.

## 9.4.0.0 Testing Tools

- A mock SMTP server tool like `smtp4dev` for automated integration tests.
- A real SMTP service like Mailtrap or a dedicated test email account for E2E testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests for SMTP service logic and UI components implemented and passing with >80% coverage
- Integration testing with a mock SMTP server completed successfully
- E2E testing with a real SMTP server confirms successful email delivery
- User interface reviewed and approved for usability and accessibility
- Security review of password handling and storage is complete and approved
- User Guide documentation for configuring email delivery is written and published
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- Requires a stable framework for report configuration and delivery destinations (US-051, US-057) to be completed first.
- A test SMTP server/service must be available to the development and QA team at the start of the sprint.

## 11.4.0.0 Release Impact

This is a core feature for automated report distribution and is critical for meeting the primary product requirements.

