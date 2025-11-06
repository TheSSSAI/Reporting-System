# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-073 |
| Elaboration Date | 2025-01-20 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Manually cancel a running or queued job |
| As A User Story | As an Administrator, I want to manually cancel a r... |
| User Persona | Administrator |
| Business Value | Provides essential operational control, allowing a... |
| Functional Area | Job Monitoring & Management |
| Story Theme | System Administration & Operations |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Cancel a job in 'Queued' state

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

An Administrator is viewing the Job Monitoring Dashboard and a job exists with the status 'Queued'

### 3.1.5 When

The Administrator clicks the 'Cancel' action for the queued job and confirms the action in the confirmation dialog

### 3.1.6 Then

The job's status in the UI is immediately updated to 'Cancelled'.

### 3.1.7 And

The job's execution log is updated with an entry stating it was manually cancelled by the administrator, including a timestamp.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Cancel a job in 'Running' state

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

An Administrator is viewing the Job Monitoring Dashboard and a job exists with the status 'Running'

### 3.2.5 When

The Administrator clicks the 'Cancel' action for the running job and confirms the action

### 3.2.6 Then

The job's status in the UI immediately updates to a transient state, such as 'Cancelling...'.

### 3.2.7 And

The job's execution log is updated with an entry stating it was manually cancelled by the administrator.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

UI element visibility for cancellable jobs

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

An Administrator is viewing the Job Monitoring Dashboard which lists jobs with various statuses

### 3.3.5 When

The Administrator inspects the list of jobs

### 3.3.6 Then

A 'Cancel' action/button is visible and enabled only for jobs with the status 'Queued' or 'Running'.

### 3.3.7 And

The 'Cancel' action is hidden or disabled for jobs with terminal statuses ('Succeeded', 'Failed', 'Cancelled').

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Cancellation confirmation dialog

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

An Administrator is viewing the Job Monitoring Dashboard

### 3.4.5 When

The Administrator clicks the 'Cancel' action for a cancellable job

### 3.4.6 Then

A confirmation modal dialog is displayed with a message like 'Are you sure you want to cancel this job? This action cannot be undone.'

### 3.4.7 And

The cancellation is only triggered after the Administrator confirms the action within the dialog.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Job completes before cancellation request is processed

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

A job is in the 'Running' state

### 3.5.5 When

The Administrator initiates a cancellation, but the job finishes (Succeeds or Fails) before the cancellation signal is processed

### 3.5.6 Then

The cancellation request is ignored.

### 3.5.7 And

The UI correctly reflects the job's final completion status.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Job transitions from Queued to Running during cancellation attempt

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

A job is in the 'Queued' state

### 3.6.5 When

The Administrator initiates a cancellation at the exact moment the scheduler begins executing the job

### 3.6.6 Then

The system handles the race condition gracefully and proceeds to cancel the now 'Running' job as per AC-002.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Non-responsive job fails to cancel

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

A job is in the 'Running' state and is unresponsive to the cancellation signal

### 3.7.5 When

An Administrator initiates a cancellation

### 3.7.6 Then

After a system-defined timeout (e.g., 30 seconds), the job's status is updated to 'Failed'.

### 3.7.7 And

The job's execution log contains an entry indicating that a manual cancellation attempt timed out.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A 'Cancel' icon button (e.g., a stop or 'X' icon) in each row of the job monitoring table.
- A confirmation modal dialog with 'Confirm' and 'Cancel' buttons.

## 4.2.0 User Interactions

- The 'Cancel' button is only clickable for jobs in 'Queued' or 'Running' states.
- Clicking the 'Cancel' button opens the confirmation modal.
- Confirming the cancellation triggers an API call and provides immediate visual feedback (e.g., status change to 'Cancelling...').

## 4.3.0 Display Requirements

- The job status must update in near real-time to reflect the cancellation process ('Cancelling...', 'Cancelled').
- Tooltips on the cancel button should indicate its function, e.g., 'Cancel Job'.

## 4.4.0 Accessibility Needs

- The cancel button must have an appropriate `aria-label`.
- The confirmation modal must trap focus and be keyboard-navigable.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Only users with the 'Administrator' role can cancel jobs.

### 5.1.3 Enforcement Point

Backend API endpoint and Frontend UI rendering.

### 5.1.4 Violation Handling

API returns a 403 Forbidden error. UI does not render the cancel button for non-administrators.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A job can only be cancelled if its current status is 'Queued' or 'Running'.

### 5.2.3 Enforcement Point

Backend API endpoint before processing the cancellation request.

### 5.2.4 Violation Handling

API returns a 409 Conflict error with a message like 'Job is not in a cancellable state.'

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-070

#### 6.1.1.2 Dependency Reason

The Job Monitoring Dashboard is required as the primary interface for this functionality.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-071

#### 6.1.2.2 Dependency Reason

The ability to view and update a job's status is fundamental to the cancellation process.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-072

#### 6.1.3.2 Dependency Reason

The job execution logging mechanism is required to record the cancellation event.

## 6.2.0.0 Technical Dependencies

- Quartz.NET job scheduling library for interacting with the job queue and interrupting jobs.
- ASP.NET Core backend for the API endpoint.
- React frontend for the UI components.
- Cooperative cancellation pattern (e.g., .NET CancellationToken) must be implemented throughout the job execution pipeline (ingestion, transformation, generation, delivery).

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The cancellation API call should respond in under 200ms.
- The UI status update for a 'Queued' job cancellation should appear instantaneous (< 500ms).
- A 'Running' job should respond to a cancellation signal within 1 second of receiving it.

## 7.2.0.0 Security

- The API endpoint for cancelling jobs must be protected and only accessible to authenticated users with the 'Administrator' role.

## 7.3.0.0 Usability

- The cancellation process must be intuitive and provide clear feedback to the user at each step (request sent, processing, final state).

## 7.4.0.0 Accessibility

- All UI elements related to this feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- Functionality must be consistent across all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementing graceful, cooperative cancellation throughout the entire multi-stage job pipeline is the primary complexity driver.
- Handling race conditions between the scheduler and the cancellation request requires careful, thread-safe code.
- The frontend work is low complexity, but the backend logic for interrupting a running job is significant.

## 8.3.0.0 Technical Risks

- Custom connector plug-ins might not correctly implement or honor the cancellation token, making some jobs un-cancellable. The PDK documentation must be very clear on this requirement.
- Improper resource cleanup in the cancellation logic could lead to memory or file handle leaks.

## 8.4.0.0 Integration Points

- Quartz.NET Scheduler: To remove queued jobs and to signal running jobs to stop.
- Job Execution Engine: Each step of the engine must be modified to check for and respond to a cancellation request.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Cancel a queued job and verify it never runs.
- Cancel a running job during its data ingestion phase.
- Cancel a running job during its PDF generation phase.
- Attempt to cancel a job that has already completed.
- Verify non-admin users cannot see or use the cancel functionality.
- Test the timeout scenario for a non-responsive job.

## 9.3.0.0 Test Data Needs

- A report configuration that runs very quickly (to test race conditions).
- A report configuration that is artificially slow (e.g., processes a large file or includes a delay) to provide a window to cancel it while 'Running'.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- React Testing Library/Jest for frontend unit tests.
- An E2E testing framework like Playwright or Cypress.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for backend and frontend logic, achieving >80% coverage for new code
- Integration tests for the cancellation pipeline are implemented and passing
- E2E tests simulating the user flow are implemented and passing
- User interface reviewed and approved by UX/Product Owner
- Performance requirements for API response and job termination are verified
- Security requirements for the API endpoint are validated
- Documentation for the Plug-in Development Kit (PDK) is updated to include requirements for handling cancellation tokens
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story has a significant backend component. Ensure developers with experience in the job execution pipeline are available.
- Should be scheduled after US-070, US-071, and US-072 are completed and merged.

## 11.4.0.0 Release Impact

This is a key feature for system administrators and is considered essential for providing adequate operational control over the application.

