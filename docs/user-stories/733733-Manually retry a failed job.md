# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-074 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Manually retry a failed job |
| As A User Story | As an Administrator, I want to manually trigger a ... |
| User Persona | Administrator |
| Business Value | Improves operational efficiency and system reliabi... |
| Functional Area | Job Monitoring & Management |
| Story Theme | System Operability and Maintenance |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful retry of a failed job

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

an Administrator is logged in and is viewing the Job Monitoring dashboard

### 3.1.5 When

the Administrator clicks the 'Retry' action for a job with the status 'Failed'

### 3.1.6 Then

a new job execution record is created in the database with the exact same configuration as the failed job, a new unique JobExecutionLog ID, and a status of 'Queued'.

### 3.1.7 Validation Notes

Verify in the database that a new JobExecutionLog entry exists. The new entry should appear at the top of the Job Monitoring dashboard.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Original failed job record is preserved

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

an Administrator has successfully initiated a retry for a failed job

### 3.2.5 When

the Job Monitoring dashboard is refreshed

### 3.2.6 Then

the original job record still exists with its status as 'Failed' and its original timestamps.

### 3.2.7 Validation Notes

Check the dashboard and the database to confirm the original failed job's record is untouched.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

UI feedback on successful retry

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

an Administrator is on the Job Monitoring dashboard

### 3.3.5 When

they click the 'Retry' action for a failed job and the request is successful

### 3.3.6 Then

a non-blocking success notification (e.g., a toast message) is displayed, confirming that the job has been queued for retry.

### 3.3.7 Validation Notes

Manually test the UI to ensure the success message appears and is user-friendly.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Retry action is not available for non-failed jobs

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

an Administrator is viewing the Job Monitoring dashboard

### 3.4.5 When

they view the actions for a job with a status of 'Succeeded', 'Running', or 'Queued'

### 3.4.6 Then

the 'Retry' action is not visible or is disabled.

### 3.4.7 Validation Notes

Inspect the UI for jobs in various states to confirm the retry option is conditionally rendered.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Attempt to retry a job whose configuration has been deleted

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

a job has failed, and its parent ReportConfiguration has since been deleted

### 3.5.5 When

an Administrator clicks the 'Retry' action for that failed job

### 3.5.6 Then

the system does not queue a new job, and an error notification is displayed to the user, stating that the original configuration could not be found.

### 3.5.7 Validation Notes

Requires setting up a test case where a ReportConfiguration is deleted after a job fails. Verify the API returns an appropriate error code (e.g., 409 Conflict) and the UI displays the error.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Retry action is audited

### 3.6.3 Scenario Type

Security

### 3.6.4 Given

an Administrator is logged in

### 3.6.5 When

they successfully initiate a retry for a failed job

### 3.6.6 Then

a new entry is created in the system's Audit Log.

### 3.6.7 Validation Notes

Check the AuditLog table or UI to confirm an entry exists with the administrator's username, the action ('Job Retry'), the original Job ID, the new Job ID, and a timestamp.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A 'Retry' icon button (e.g., a circular arrow) in the actions column for each job row on the Job Monitoring dashboard.

## 4.2.0 User Interactions

- The 'Retry' button is only enabled and visible for jobs with a 'Failed' status.
- Clicking the 'Retry' button triggers an API call to initiate the retry process.
- The button should show a loading/spinner state while the API call is in progress to prevent double-clicks.
- On success, a toast notification appears. On failure, an error toast notification appears.

## 4.3.0 Display Requirements

- The new, retried job should appear in the job list, typically at the top if sorted by creation time, with a 'Queued' status.

## 4.4.0 Accessibility Needs

- The 'Retry' button must have an accessible name, such as `aria-label="Retry job [Report Name]"`.
- The button must be keyboard focusable and operable via Enter/Space keys.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A job can only be retried if its current status is 'Failed'.

### 5.1.3 Enforcement Point

Backend API and Frontend UI

### 5.1.4 Violation Handling

The API will return an error response (e.g., 409 Conflict). The UI will prevent the action by disabling or hiding the button.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A retried job is a new, distinct execution and must not overwrite the original failed job's log.

### 5.2.3 Enforcement Point

Backend Service Logic

### 5.2.4 Violation Handling

The system must create a new `JobExecutionLog` record. This is a core architectural constraint.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-070

#### 6.1.1.2 Dependency Reason

The Job Monitoring dashboard is the required user interface for placing the 'Retry' action.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-071

#### 6.1.2.2 Dependency Reason

The system must be able to identify jobs by their 'Failed' status to conditionally show the retry option.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-101

#### 6.1.3.2 Dependency Reason

The retry action is a security-sensitive event that must be recorded in the audit log.

## 6.2.0.0 Technical Dependencies

- Quartz.NET job scheduling service must be available to queue the new job.
- ASP.NET Core Identity for role-based access control to restrict the action to Administrators.
- Entity Framework Core for creating the new JobExecutionLog record.

## 6.3.0.0 Data Dependencies

- Requires existing `JobExecutionLog` records in the database to test against.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to initiate a retry should complete in under 500ms (P95) as it is a lightweight operation.

## 7.2.0.0 Security

- The API endpoint for retrying a job must be protected and accessible only by users with the 'Administrator' role.
- The action must be logged in the audit trail as specified in AC-006.

## 7.3.0.0 Usability

- The process to retry a job should be intuitive and require only a single click from the jobs list.

## 7.4.0.0 Accessibility

- The UI controls for this feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires both frontend and backend changes.
- Interaction with the core job scheduling component (Quartz.NET).
- State management in the frontend to update the job list and show notifications.

## 8.3.0.0 Technical Risks

- Potential for race conditions if the underlying configuration is modified or deleted at the exact moment a retry is initiated. The backend logic must handle this gracefully.
- Ensuring the new job is queued with the exact state of the original configuration at the time of its execution.

## 8.4.0.0 Integration Points

- Backend API: A new endpoint, e.g., `POST /api/v1/jobs/{jobId}/retry`.
- Database: Creation of a new row in the `JobExecutionLog` table.
- Scheduler: A call to the Quartz.NET API to schedule the new job.
- Audit Log: A call to the auditing service to log the event.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify a failed job can be successfully retried.
- Verify the retry button is absent for a successful job.
- Verify the retry button is absent for a running job.
- Verify an error is handled correctly if the report configuration was deleted.
- Verify a non-administrator user cannot see the retry button or call the API.
- Verify the audit log is correctly written after a retry.

## 9.3.0.0 Test Data Needs

- A test database with jobs in 'Failed', 'Succeeded', and 'Running' states.
- A test case where a `ReportConfiguration` is deleted after its associated job has failed.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- Cypress or Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for both frontend and backend, achieving >= 80% coverage for new code
- Integration testing for the API endpoint and scheduler interaction completed successfully
- E2E test scenario for the retry workflow is automated and passing
- User interface reviewed and approved for usability and accessibility
- Security requirements (RBAC and auditing) validated
- Documentation for the Job Monitoring feature is updated to include the retry functionality
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a high-value feature for system administrators and should be prioritized soon after the initial job monitoring dashboard is complete.
- Ensure prerequisite stories are completed before starting this work.

## 11.4.0.0 Release Impact

- Enhances the core operational management capabilities of the application, making it more robust and user-friendly for administrators.

