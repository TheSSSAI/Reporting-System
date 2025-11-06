# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-075 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Configure system-wide email notifications for job ... |
| As A User Story | As an Administrator, I want to configure a system-... |
| User Persona | Administrator |
| Business Value | Improves system monitoring and reliability by prov... |
| Functional Area | System Administration & Monitoring |
| Story Theme | System Health and Alerting |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Configure and receive a job failure notification

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am logged in as an Administrator and the system's SMTP server is correctly configured

### 3.1.5 When

I navigate to the System Settings page, enter 'admin1@example.com' and 'admin2@example.com' into the 'Job Failure Notification Emails' field, and save the configuration

### 3.1.6 And

the email body includes the report name, the time of failure, and a direct link to the job's execution log in the Control Panel.

### 3.1.7 Then

an email notification is successfully sent to both 'admin1@example.com' and 'admin2@example.com'

### 3.1.8 Validation Notes

Verify using a mock SMTP server (like Papercut or Smtp4dev) to intercept the outgoing email and inspect its headers and content.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Attempt to save an invalid email address

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

I am logged in as an Administrator on the notification settings page

### 3.2.5 When

I enter 'admin@example.com, not-a-valid-email' into the notification list and attempt to save

### 3.2.6 Then

the UI displays a clear validation error message indicating that 'not-a-valid-email' is not a valid format

### 3.2.7 And

the configuration is not saved.

### 3.2.8 Validation Notes

Check that the save button is disabled or the API call is blocked until the validation error is corrected. The API should also return a 400 Bad Request if invalid data is sent.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Save an empty notification list

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

I am logged in as an Administrator and the notification list contains at least one email address

### 3.3.5 When

I remove all email addresses from the list and save the configuration

### 3.3.6 And

no email notification is sent.

### 3.3.7 Then

the job's status is correctly marked as 'Failed' in the system

### 3.3.8 Validation Notes

Verify that the job failure listener correctly handles an empty recipient list without throwing an error.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

View settings page when SMTP is not configured

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

I am logged in as an Administrator

### 3.4.5 And

the message states that notifications cannot be sent until SMTP is configured and provides a link to the SMTP configuration page.

### 3.4.6 When

I navigate to the notification settings page

### 3.4.7 Then

a prominent, non-dismissible warning message is displayed on the page

### 3.4.8 Validation Notes

Verify the UI displays the warning banner. Manually trigger a job failure and confirm that an error is logged about the inability to send the notification email.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Job fails when SMTP is not configured

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am an Administrator and I have configured a valid email notification list

### 3.5.5 And

the system log records a high-severity error stating that the failure notification could not be sent due to an SMTP configuration issue.

### 3.5.6 When

a report job fails

### 3.5.7 Then

the job is correctly marked as 'Failed'

### 3.5.8 Validation Notes

Check the Serilog JSON output file for a specific, structured log entry detailing the notification sending failure.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated section within 'System Settings' for 'Notifications'.
- A multi-entry input field for email addresses, preferably using a tag-based UI for easy addition and removal.
- A 'Save' button to persist the settings.
- Inline validation messages for incorrectly formatted email addresses.
- A persistent warning banner that appears when system SMTP settings are not configured.

## 4.2.0 User Interactions

- Administrator can add emails by typing and pressing Enter or comma.
- Administrator can remove an email by clicking an 'x' icon on its tag.
- The 'Save' button is disabled until changes are made.
- Upon successful save, a temporary success notification (toast message) appears.

## 4.3.0 Display Requirements

- The current list of notification emails is loaded and displayed when the page is opened.
- Validation errors are displayed in close proximity to the input field.

## 4.4.0 Accessibility Needs

- All form fields must have associated labels.
- The multi-entry input must be keyboard navigable.
- Validation errors and success messages must be announced by screen readers (using ARIA live regions).

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'Email notifications for job failures are only sent if the notification list is not empty and the system SMTP server is configured correctly.', 'enforcement_point': 'Within the job failure event listener in the backend.', 'violation_handling': 'If the list is empty, the process is skipped. If SMTP is not configured, an error is logged to the system logs, but the job failure processing continues.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-071

#### 6.1.1.2 Dependency Reason

The system must have a concept of a job with a 'Failed' status to trigger the notification.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-SYS-001

#### 6.1.2.2 Dependency Reason

A separate, prerequisite story is needed to implement the system-wide SMTP server configuration UI and backend logic, which this feature depends on for sending emails. This is also a dependency for US-064.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity for Administrator role validation.
- Quartz.NET job listener for detecting job failures.
- A backend email sending library (e.g., MailKit).
- ASP.NET Core configuration system for storing settings.
- React component library (MUI) for the UI elements.

## 6.3.0.0 Data Dependencies

- Requires a new entry in the system configuration storage (SQLite database) to persist the list of email addresses.

## 6.4.0.0 External Dependencies

- A functional external SMTP server for sending the actual emails during integration testing and in production.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The email notification process must be asynchronous and not block or delay the job scheduler's main thread.
- The UI for configuring the list must load in under 500ms.

## 7.2.0.0 Security

- The API endpoints for getting and setting the notification list must be restricted to users with the 'Administrator' role.
- The list of email addresses must be stored securely within the encrypted SQLite database.

## 7.3.0.0 Usability

- The UI for managing the list of emails should be intuitive, making it easy to add and remove multiple addresses.
- Error messages must be clear and actionable.

## 7.4.0.0 Accessibility

- The settings page must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The UI must function correctly on the latest stable versions of Chrome, Firefox, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires touching multiple layers of the application: frontend UI, backend API, and the core job scheduling engine.
- Implementing a robust, non-blocking notification mechanism within the Quartz.NET job listener requires careful design.
- Dependency on a separate (and critical) SMTP configuration feature that must be completed first.

## 8.3.0.0 Technical Risks

- The job failure listener could introduce instability to the scheduler if not implemented with proper error handling.
- Incorrect SMTP configuration by users could lead to support requests; the UI must provide clear feedback and guidance.

## 8.4.0.0 Integration Points

- Quartz.NET Scheduler: A global `IJobListener` needs to be implemented and registered.
- System Configuration Service: A service to read/write the email list from the database.
- Email Service: A service that encapsulates the logic for sending emails via the configured SMTP server.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify email is sent on job failure with one recipient.
- Verify email is sent on job failure with multiple recipients.
- Verify no email is sent when the recipient list is empty.
- Verify UI validation prevents saving invalid email formats.
- Verify an error is logged if the email fails to send due to bad SMTP settings.
- Verify the UI warning appears when SMTP is not configured.

## 9.3.0.0 Test Data Needs

- A report configuration that can be reliably made to fail (e.g., by pointing to an invalid data source).
- Valid and invalid email address strings.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- A mock SMTP server (e.g., Smtp4dev) for integration tests to capture and verify emails without sending them externally.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for new logic with >= 80% coverage
- Integration testing with a mock SMTP server completed successfully
- User interface reviewed for usability and accessibility (WCAG 2.1 AA)
- Performance requirements (asynchronous sending) verified
- Security requirements (role-based access) validated
- Administrator User Guide is updated with instructions for this feature
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is blocked by the implementation of the system-wide SMTP configuration feature. It should be scheduled in a sprint after that dependency is resolved.
- Requires coordination between frontend and backend developers.

## 11.4.0.0 Release Impact

This is a key feature for system administration and monitoring. Its inclusion significantly improves the product's suitability for production environments.

