# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-085 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Trigger a re-delivery for selected reports |
| As A User Story | As an End-User (or Administrator) viewing generate... |
| User Persona | End-User (Viewer), Administrator |
| Business Value | Improves user self-sufficiency by allowing them to... |
| Functional Area | Report Viewer |
| Story Theme | Report Access and Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Re-deliver a single selected report

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in user in the Report Viewer and see a list of generated reports

### 3.1.5 When

I select a single report, click the 'Re-deliver' button, and confirm the action in the subsequent dialog

### 3.1.6 Then

the system initiates an asynchronous delivery job using the original report artifact, I see a notification that the re-delivery has started, and a new entry is created in the job execution log for the delivery attempt.

### 3.1.7 Validation Notes

Verify the configured delivery destinations (e.g., email inbox, S3 bucket) receive the original report file. Check the audit log for the action and the job log for the delivery outcome.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Happy Path: Re-deliver multiple selected reports

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am a logged-in user in the Report Viewer

### 3.2.5 When

I select three reports using their checkboxes, click 'Re-deliver', and confirm

### 3.2.6 Then

the system queues three separate asynchronous delivery jobs, I see a notification like 'Re-delivery initiated for 3 reports', and the destinations for each report receive the correct artifacts.

### 3.2.7 Validation Notes

Confirm all destinations for all three reports receive the files. Check that three distinct delivery attempts are logged.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

UI Interaction: 'Re-deliver' button is enabled only upon selection

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

I am viewing the list of reports in the Report Viewer

### 3.3.5 When

no reports are selected

### 3.3.6 Then

the 'Re-deliver' button is visible but disabled.

### 3.3.7 Validation Notes

Check the button's 'disabled' attribute. Then, select a report and verify the 'disabled' attribute is removed.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Error Condition: Re-delivery fails due to an invalid destination

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I select a report whose configured email destination is now an invalid address

### 3.4.5 When

I trigger the re-delivery for that report

### 3.4.6 Then

the system attempts delivery, fails, and I receive a notification that the delivery failed. The job execution log for this attempt is marked as 'Failed' and contains a detailed error message about the invalid address.

### 3.4.7 Validation Notes

Check the UI for the failure notification. Inspect the job execution log for the specific error message from the SMTP service.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Error Condition: Original report artifact is missing

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I select a report whose generated file has been deleted from the system's storage

### 3.5.5 When

I trigger the re-delivery for that report

### 3.5.6 Then

the action fails immediately, and I see an error notification stating 'Report artifact not found. Cannot re-deliver.'

### 3.5.7 Validation Notes

Manually delete a report file from the storage location and then attempt to re-deliver it via the UI to verify the error handling.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Edge Case: Report has no configured delivery destinations

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

a report was generated with only 'Local Storage' as its destination and no other delivery targets

### 3.6.5 When

I select this report in the Report Viewer

### 3.6.6 Then

the 'Re-deliver' button remains disabled, or if enabled, clicking it shows a message 'This report has no configured delivery destinations.'

### 3.6.7 Validation Notes

Configure a report without any remote delivery targets, generate it, and then test the re-delivery action for that specific instance.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Security: User lacks permission to access a report

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

I am logged in as an End-User and have selected a report I am authorized to see and another report I am not (e.g., via a crafted API call)

### 3.7.5 When

I trigger a re-delivery for both reports via the API

### 3.7.6 Then

the backend rejects the request with an HTTP 403 Forbidden error, and no delivery jobs are queued.

### 3.7.7 Validation Notes

This must be tested at the API level to ensure backend authorization checks are enforced.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A 'Re-deliver' button in the Report Viewer's action bar.
- A confirmation modal dialog with 'Confirm' and 'Cancel' buttons, displaying text like 'Are you sure you want to re-deliver the X selected report(s)?'
- Non-blocking toast notifications for success, failure, and in-progress states.

## 4.2.0 User Interactions

- The 'Re-deliver' button is disabled by default and becomes enabled when one or more reports in the list are selected via checkboxes.
- Clicking the 'Re-deliver' button opens the confirmation modal.
- Confirming the action closes the modal and triggers the backend process, displaying an initial toast notification.
- A final toast notification appears upon completion of the asynchronous process.

## 4.3.0 Display Requirements

- The number of selected reports should be reflected in the confirmation dialog.
- Notifications should clearly state the outcome (e.g., 'Re-delivery for 3 reports succeeded', 'Re-delivery for 1 report failed. See logs for details.').

## 4.4.0 Accessibility Needs

- The 'Re-deliver' button must have an accessible name (aria-label).
- The confirmation dialog must trap focus and be keyboard navigable.
- Toast notifications must be announced by screen readers using ARIA live regions.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Re-delivery must use the original, existing report artifact. It must not re-generate the report data.

### 5.1.3 Enforcement Point

Backend job execution logic.

### 5.1.4 Violation Handling

The job logic must be designed to locate the existing file and fail if it's not found, rather than triggering the generation pipeline.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Re-delivery must use the delivery destinations as defined in the report's configuration at the time the re-delivery is triggered.

### 5.2.3 Enforcement Point

Backend job queuing service.

### 5.2.4 Violation Handling

The system will fetch the current delivery configuration for the report and apply it to the re-delivery job.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

Each re-delivery attempt must be logged as a distinct event in the system's audit and job logs.

### 5.3.3 Enforcement Point

API Controller and Job Execution Logic.

### 5.3.4 Violation Handling

The request fails if logging fails. The job fails if its outcome cannot be logged.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-078

#### 6.1.1.2 Dependency Reason

Requires the Report Viewer list to display generated reports.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-083

#### 6.1.2.2 Dependency Reason

Requires the checkbox mechanism to select multiple reports for bulk operations.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-086

#### 6.1.3.2 Dependency Reason

The re-delivery action must respect the access permissions configured for each report and user role.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-072

#### 6.1.4.2 Dependency Reason

Requires the job execution log view to exist for troubleshooting failed re-delivery attempts.

## 6.2.0.0 Technical Dependencies

- Backend API endpoint for triggering the action.
- Job scheduling system (Quartz.NET) to handle asynchronous delivery.
- Existing report delivery modules (SMTP, S3, FTP, etc.).
- Frontend state management (Zustand) for handling report selection.

## 6.3.0.0 Data Dependencies

- Requires access to the `JobExecutionLog` table to identify reports.
- Requires access to the storage location of generated report artifacts.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The re-delivery action must be asynchronous to prevent blocking the UI, especially for bulk requests.
- The API response to the trigger request should be under 500ms.

## 7.2.0.0 Security

- The backend must perform an authorization check to ensure the requesting user has permission to access and re-deliver EVERY report ID included in the request.
- The action must be logged in the security audit log, including the user, timestamp, source IP, and IDs of the reports.

## 7.3.0.0 Usability

- The user must receive clear and immediate feedback that their action has been initiated.
- The final outcome (success or failure) must be clearly communicated.

## 7.4.0.0 Accessibility

- The feature must be fully keyboard-operable, including selecting reports and triggering the action.
- All UI elements must meet WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Potential refactoring of the core job execution logic may be required to create a 'delivery-only' pipeline that can be triggered independently of the full 'generate-and-deliver' pipeline.
- Backend logic to handle bulk requests, including partial failures, adds complexity.
- Ensuring robust error handling and logging for various delivery failure scenarios (e.g., invalid credentials, network issues, missing artifacts).

## 8.3.0.0 Technical Risks

- The existing job execution logic might be tightly coupled, making it difficult to separate the delivery step without significant refactoring.
- Race conditions if a report's configuration is changed while a re-delivery is being initiated.

## 8.4.0.0 Integration Points

- Frontend Report Viewer component.
- Backend API for job management.
- Quartz.NET job scheduler.
- All existing delivery modules (SMTP, S3, FTP, etc.).
- System logging and auditing services.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Successful re-delivery of a single report to all destination types (Email, S3, FTP).
- Successful bulk re-delivery of multiple reports.
- Failure scenario where a delivery destination is unreachable.
- Failure scenario where the report artifact file is missing.
- API security test to ensure a user cannot re-deliver a report they are not authorized to view.

## 9.3.0.0 Test Data Needs

- Generated reports with various delivery configurations.
- User accounts with different roles and permissions.
- A report instance where the physical file has been removed to test error handling.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- Playwright or Cypress for E2E testing.
- Mock SMTP/S3 servers for integration testing delivery.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >80% coverage for new code
- Integration testing completed successfully for the API endpoint and job logic
- E2E test scenario for the happy path is automated and passing
- User interface reviewed and approved for usability and accessibility
- Security requirements validated, especially API authorization
- API endpoint is documented in the OpenAPI specification
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a high-value feature for user self-service. It should be prioritized after the core report generation and viewing functionalities are stable.
- Requires careful coordination between frontend and backend development due to the new API endpoint and asynchronous nature of the task.

## 11.4.0.0 Release Impact

- Significantly enhances the usability of the Report Viewer and reduces support load. This should be highlighted in release notes as a key new feature.

