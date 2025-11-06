# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-076 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Receive an email notification on job failure |
| As A User Story | As an Administrator, I want to receive an email no... |
| User Persona | Administrator |
| Business Value | Enables proactive monitoring and faster incident r... |
| Functional Area | Job Monitoring & Alerting |
| Story Theme | System Reliability and Operations |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful email notification on job failure

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

the system has a valid SMTP server configuration and a list of one or more administrator emails is configured for failure notifications

### 3.1.5 When

a scheduled or on-demand report job executes and fails

### 3.1.6 Then

an email notification is sent to all configured administrator email addresses.

### 3.1.7 Validation Notes

Verify by triggering a job designed to fail (e.g., with an invalid data source) and checking the inbox of a test email account configured in the notification list.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Email notification content is correct and informative

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

a report job has failed and a notification email has been sent

### 3.2.5 When

the Administrator opens the email

### 3.2.6 Then

the email subject line is in the format: "[System Name] - Job Failed: [Report Name]".

### 3.2.7 And

the email body is HTML formatted and contains the Report Name, Job ID, Failure Timestamp (in UTC), a summary of the error message, and a clickable link that navigates directly to the detailed execution log page for that specific job in the Control Panel.

### 3.2.8 Validation Notes

Inspect the received email's source and content. Click the link to ensure it resolves to the correct URL (e.g., /controlpanel/jobs/{jobId}).

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Failure to send notification due to SMTP issues

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

the system has an invalid or unreachable SMTP server configuration

### 3.3.5 When

a report job fails

### 3.3.6 Then

the system attempts to send the notification email.

### 3.3.7 And

the job's status in the Control Panel remains 'Failed' and is not affected by the notification failure.

### 3.3.8 Validation Notes

Configure the system with incorrect SMTP credentials. Trigger a job failure and inspect the Serilog JSON output for a specific, high-severity log entry about the notification failure.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Job fails but no notification recipients are configured

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

the system has a valid SMTP server configuration but the administrator email notification list is empty

### 3.4.5 When

a report job fails

### 3.4.6 Then

the system does not attempt to send an email.

### 3.4.7 And

a warning is written to the system's main log file stating that the job failed but no notification was sent because no recipients were configured.

### 3.4.8 Validation Notes

Ensure the notification email list is empty in the system settings. Trigger a job failure and check the logs for the specific warning message. Verify no email was sent via a test SMTP server like smtp4dev.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Notification process does not block job scheduler

### 3.5.3 Scenario Type

Non_Functional

### 3.5.4 Given

multiple jobs are scheduled to run in close succession

### 3.5.5 When

the first job fails and the notification sending process is slow or fails

### 3.5.6 Then

the execution of the subsequent scheduled jobs is not delayed.

### 3.5.7 Validation Notes

This must be validated through code review, ensuring the email sending logic is executed asynchronously ('fire-and-forget' with logging) and does not block the main Quartz.NET scheduler thread.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- This story has no direct UI elements in the Control Panel. The 'UI' is the HTML email itself.

## 4.2.0 User Interactions

- User receives and reads the email.
- User clicks a link within the email to navigate to the Control Panel.

## 4.3.0 Display Requirements

- Email must be well-formatted and readable in modern email clients (e.g., Outlook, Gmail).
- Email must clearly present all required information: Report Name, Job ID, Timestamp, Error Summary, and a direct link.

## 4.4.0 Accessibility Needs

- The HTML email should use semantic tags (e.g., <h1>, <p>, <a>) to be accessible to screen readers.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "A notification email is triggered only on the terminal 'Failed' state of a job.", 'enforcement_point': 'Job Execution Engine, after all retries (as defined by Polly policies) have been exhausted.', 'violation_handling': 'N/A. This is a system logic rule.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-075

#### 6.1.1.2 Dependency Reason

This story requires the ability to configure SMTP settings and a list of recipient email addresses, which is provided by US-075.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-072

#### 6.1.2.2 Dependency Reason

The notification email must contain a direct link to the detailed job log view, which is implemented in US-072. The URL structure must be known.

## 6.2.0.0 Technical Dependencies

- Quartz.NET job scheduling framework for triggering jobs.
- A robust email sending library (e.g., MailKit).
- Serilog for structured logging of notification successes and failures.
- A mechanism within the job execution pipeline to catch exceptions and trigger a 'OnFailure' event.

## 6.3.0.0 Data Dependencies

- Access to system configuration for SMTP settings and recipient list.
- Access to job execution context to retrieve Report Name, Job ID, and error details upon failure.

## 6.4.0.0 External Dependencies

- A customer-provided SMTP server that is reachable from the host machine.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- Email notification sending must be an asynchronous operation that completes or times out within 10 seconds and does not block the core job processing thread pool.

## 7.2.0.0 Security

- SMTP credentials must be retrieved from the secure configuration store (e.g., Windows Certificate Store) and never logged in plaintext.
- The email content must not include any raw data from the report itself, only metadata about the job's execution failure.

## 7.3.0.0 Usability

- The email content must be clear and concise, providing enough information for an administrator to immediately understand the problem and where to find more details.

## 7.4.0.0 Accessibility

- The HTML email must adhere to basic accessibility standards (e.g., sufficient color contrast, semantic HTML).

## 7.5.0.0 Compatibility

- The HTML email should render correctly in the latest versions of major web and desktop email clients.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires integration with the job scheduler's error handling hooks.
- Asynchronous programming is required to avoid blocking.
- Requires a templating solution for the HTML email body to keep it maintainable.
- Error handling for SMTP connection/authentication failures adds complexity.

## 8.3.0.0 Technical Risks

- Variability in customer SMTP server configurations (ports, SSL/TLS requirements) could lead to support issues. The configuration UI from US-075 must be comprehensive.
- Emails being flagged as spam by recipient mail servers. The system should set appropriate headers if possible.

## 8.4.0.0 Integration Points

- The job execution module in the backend service.
- The system configuration service for retrieving SMTP and recipient settings.
- The logging framework (Serilog).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- A job fails due to a database connection error; verify email is sent and correct.
- A job fails due to a file not found error; verify email is sent and correct.
- A job succeeds; verify NO email is sent.
- SMTP server is offline; verify job fails and a critical error is logged about the notification failure.
- Recipient list is empty; verify job fails and a warning is logged.

## 9.3.0.0 Test Data Needs

- A configured report that is guaranteed to fail.
- A valid test email account and SMTP server credentials (e.g., Mailtrap, smtp4dev).

## 9.4.0.0 Testing Tools

- xUnit/Moq for unit tests.
- A local SMTP sink like smtp4dev for integration testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests for the notification service implemented and passing with >80% coverage
- Integration testing with a test SMTP server completed successfully
- User interface (the email template) reviewed and approved for clarity and correctness
- Performance requirements (asynchronous execution) verified via code review
- Security requirements (secure credential handling) validated
- Documentation for the alerting feature is updated in the Administrator Guide
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is blocked by US-075 and US-072. It cannot be started until they are complete.
- Requires access to a test SMTP server during development and QA.

## 11.4.0.0 Release Impact

This is a key feature for system monitoring and reliability. It is a high-value addition for administrators managing the system in production.

