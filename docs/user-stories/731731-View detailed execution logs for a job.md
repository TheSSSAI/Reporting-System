# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-072 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | View detailed execution logs for a job |
| As A User Story | As an Administrator, I want to view a detailed, ti... |
| User Persona | Administrator |
| Business Value | Reduces Mean Time To Resolution (MTTR) for failed ... |
| Functional Area | Job Monitoring & Management |
| Story Theme | System Observability |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Accessing logs for a successfully completed job

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an authenticated Administrator on the Job Monitoring Dashboard

### 3.1.5 When

I click on a job record that has a 'Succeeded' status

### 3.1.6 Then

I am navigated to a dedicated log viewer page for that specific job execution.

### 3.1.7 Validation Notes

Verify the URL reflects the specific job execution ID. The page should load without errors.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Verifying log content and structure for a successful job

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am on the log viewer page for a successfully completed job

### 3.2.5 When

the page finishes loading

### 3.2.6 Then

the page displays a header with the Report Name, final status ('Succeeded'), Start Time, and End Time.

### 3.2.7 And

each log entry contains a timestamp, a log level (e.g., INFO), the job stage (e.g., INGESTION, TRANSFORMATION, GENERATION, DELIVERY), and a descriptive message.

### 3.2.8 Validation Notes

Check that timestamps are in a consistent, readable format. Verify that logs for all major stages of the job pipeline are present.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Accessing and viewing logs for a failed job

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am on the Job Monitoring Dashboard and a job has a 'Failed' status

### 3.3.5 When

I click on the failed job record

### 3.3.6 Then

the log viewer page is displayed with the final status clearly marked as 'Failed'.

### 3.3.7 And

the error log entry includes a detailed error message and a full stack trace, which may be collapsed by default for readability.

### 3.3.8 Validation Notes

Verify the highlighting makes the error easy to spot. Test that the stack trace can be expanded and collapsed.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Viewing logs for a currently running job

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

I am on the Job Monitoring Dashboard and a job has a 'Running' status

### 3.4.5 When

I click on the running job record

### 3.4.6 Then

the log viewer page is displayed with the status clearly marked as 'Running'.

### 3.4.7 And

the log entries are displayed and the view updates periodically (e.g., every 5 seconds or via a manual refresh button) to show new log entries as they are generated.

### 3.4.8 Validation Notes

Trigger a long-running job and open the log view. Confirm that new log entries appear without requiring a full page reload.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Filtering logs by level

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

I am on the log viewer page with multiple log entries of different levels (INFO, WARN, ERROR)

### 3.5.5 When

I use the filter control to select only the 'ERROR' level

### 3.5.6 Then

the log view updates to show only the log entries with the 'ERROR' level.

### 3.5.7 Validation Notes

Test filtering for each available log level and combinations of levels.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Searching within logs

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

I am on the log viewer page

### 3.6.5 When

I type a search term (e.g., a specific filename or error code) into the search bar and press Enter

### 3.6.6 Then

the log view updates to show only the log entries that contain the search term, with the term highlighted.

### 3.6.7 Validation Notes

Test with case-sensitive and case-insensitive searches. Verify that clearing the search bar restores the full, unfiltered log view.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Handling jobs with no execution logs

### 3.7.3 Scenario Type

Edge_Case

### 3.7.4 Given

A job was created but never executed (e.g., system restart before it could run)

### 3.7.5 When

I attempt to view its logs from the dashboard

### 3.7.6 Then

the log viewer page displays a clear message indicating that 'No execution logs are available for this job run'.

### 3.7.7 Validation Notes

Manually create a job record in the database without associated logs to simulate this state.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Copying logs to clipboard

### 3.8.3 Scenario Type

Happy_Path

### 3.8.4 Given

I am on the log viewer page

### 3.8.5 When

I click the 'Copy to Clipboard' button

### 3.8.6 Then

the entire, unfiltered content of the log is copied to my system clipboard as plain text.

### 3.8.7 And

a visual confirmation (e.g., a toast notification) is displayed.

### 3.8.8 Validation Notes

Paste the content into a text editor and verify it matches the logs shown on the page.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Header displaying Report Name, Job Execution ID, Status, Start Time, End Time.
- Log display area (virtualized list for performance).
- Log level filter (e.g., checkboxes for INFO, WARN, ERROR).
- Text input search bar.
- A 'Copy to Clipboard' button.
- A 'Refresh' button for running jobs.
- Breadcrumb or 'Back' link to return to the Job Monitoring Dashboard.

## 4.2.0 User Interactions

- Clicking a job on the dashboard navigates to this view.
- Log level filters update the displayed list in real-time.
- Search is triggered on input change or 'Enter' key press.
- Error messages with stack traces are expandable/collapsible.

## 4.3.0 Display Requirements

- Log entries must be color-coded or have icons corresponding to their log level (e.g., red for ERROR, yellow for WARN).
- Timestamps must be displayed in a consistent, human-readable format (e.g., YYYY-MM-DD HH:MM:SS).
- The UI must remain responsive and not freeze when displaying thousands of log entries.

## 4.4.0 Accessibility Needs

- All UI controls (filters, buttons, search) must be keyboard accessible and have appropriate ARIA labels.
- Color-coding for log levels must have a secondary indicator (icon or text) to meet WCAG 2.1 AA contrast and color requirements.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "Access to job execution logs is restricted to users with the 'Administrator' role.", 'enforcement_point': 'API endpoint and UI routing.', 'violation_handling': "User is shown a '403 Forbidden' error page or redirected to the login screen."}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-070

#### 6.1.1.2 Dependency Reason

The Job Monitoring Dashboard is the entry point for accessing the detailed log view for a specific job.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-071

#### 6.1.2.2 Dependency Reason

The job status displayed on the dashboard must be available to link to the detailed log view.

## 6.2.0.0 Technical Dependencies

- A backend logging mechanism (Serilog) capable of capturing structured logs per job execution.
- The `JobExecutionLog` entity in the SQLite database must be designed to store a collection of detailed log entries.
- A new RESTful API endpoint (e.g., `GET /api/v1/jobs/{jobId}/logs`) to serve the log data to the frontend.

## 6.3.0.0 Data Dependencies

- Requires job execution records to exist in the database, generated by the report scheduling and execution engine (Quartz.NET).

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The log viewer page for a job with 10,000 log entries must achieve Largest Contentful Paint (LCP) in under 3 seconds.
- Filtering and searching on the client-side for 10,000 log entries must complete in under 500ms.

## 7.2.0.0 Security

- All log data retrieved via the API must be subject to role-based access control (Administrator only).
- Any data displayed in the logs must be properly encoded/sanitized to prevent Cross-Site Scripting (XSS) attacks.

## 7.3.0.0 Usability

- The log view must be intuitive and allow an administrator to quickly identify the cause of a failure.
- The chronological flow of events should be easy to follow.

## 7.4.0.0 Accessibility

- The entire interface must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The log viewer must function correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Backend: Ensuring structured logs are correctly associated with a unique job execution ID in a performant way.
- Frontend: Implementing a performant UI for potentially very large log files, likely requiring a virtualized list component.
- Real-time updates for running jobs add complexity (polling vs. WebSockets).

## 8.3.0.0 Technical Risks

- Performance degradation when loading or filtering extremely large log files (100,000+ entries).
- Storing large text blobs for logs in the SQLite database could impact overall database performance if not managed carefully.

## 8.4.0.0 Integration Points

- The Quartz.NET job execution pipeline must be instrumented to emit structured logs with the correct job context.
- The frontend Control Panel's routing must be updated to include the new log viewer page.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Performance
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify logs for a successful job with all pipeline stages.
- Verify logs for a job that fails during ingestion.
- Verify logs for a job that fails during delivery.
- Test UI performance with a job that has 20,000 log entries.
- Test real-time log updates for a job that runs for 60 seconds.
- Verify that a non-administrator user receives a 403 error when trying to access the log API endpoint directly.

## 9.3.0.0 Test Data Needs

- Pre-generated job execution records in the database for Succeeded, Failed, and Running states.
- A sample job with a very large number of log entries.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend component tests.
- A dedicated E2E testing framework like Cypress or Playwright.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >80% coverage
- Integration testing between the job runner, database, and API is completed successfully
- User interface reviewed and approved by a UX designer or product owner
- Performance requirements for large log files are verified
- Security requirements (RBAC, XSS prevention) are validated
- Documentation for the feature is updated in the User Guide
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is dependent on the completion of US-070. It should be scheduled in a subsequent sprint.
- The team needs to agree on the technical approach for the frontend (virtualization library) and real-time updates (polling) during sprint planning.

## 11.4.0.0 Release Impact

This is a core feature for system administration and troubleshooting. Its inclusion is critical for the product's supportability and operational value.

