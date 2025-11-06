# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-071 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | View the status of each job |
| As A User Story | As an Administrator, I want to see a clear, distin... |
| User Persona | Administrator |
| Business Value | Provides immediate operational visibility into the... |
| Functional Area | Job Monitoring |
| Story Theme | System Administration & Monitoring |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Display 'Succeeded' status for a successfully completed job

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

an Administrator is logged in and viewing the Job Monitoring Dashboard

### 3.1.5 When

a report job has finished executing without any errors

### 3.1.6 Then

the job's entry in the list displays the status 'Succeeded' with a corresponding green visual indicator (e.g., a checkmark icon).

### 3.1.7 Validation Notes

Verify via E2E test that a job known to be successful in the database is rendered with the correct text and CSS class/icon on the frontend.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Display 'Failed' status for a job that encountered an error

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

an Administrator is logged in and viewing the Job Monitoring Dashboard

### 3.2.5 When

a report job has failed during any stage of its execution (ingestion, transformation, generation, or delivery)

### 3.2.6 Then

the job's entry in the list displays the status 'Failed' with a corresponding red visual indicator (e.g., an 'X' or warning icon).

### 3.2.7 Validation Notes

Trigger a job configured to fail (e.g., bad connection string) and verify the UI reflects the 'Failed' status.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Display 'Running' status for a job that is currently executing

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

an Administrator is logged in and viewing the Job Monitoring Dashboard

### 3.3.5 When

a report job is actively being processed by the system

### 3.3.6 Then

the job's entry in the list displays the status 'Running' with a corresponding blue visual indicator (e.g., a spinning icon).

### 3.3.7 Validation Notes

Trigger a long-running job and refresh the dashboard while it's active to confirm the 'Running' status is shown.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Display 'Queued' status for a job waiting for execution

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

an Administrator is logged in and viewing the Job Monitoring Dashboard, and all scheduler threads are busy

### 3.4.5 When

a new report job is triggered and is waiting for an available thread

### 3.4.6 Then

the job's entry in the list displays the status 'Queued' with a corresponding gray visual indicator (e.g., a clock icon).

### 3.4.7 Validation Notes

Saturate the job scheduler and queue a new job. Verify the UI shows the 'Queued' status.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Display 'Cancelled' status for a manually cancelled job

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

an Administrator is logged in and viewing the Job Monitoring Dashboard

### 3.5.5 When

the Administrator manually cancels a job that was in the 'Queued' or 'Running' state

### 3.5.6 Then

the job's entry in the list updates to display the status 'Cancelled' with a corresponding orange/yellow visual indicator (e.g., a stop icon).

### 3.5.7 Validation Notes

Cancel a running job via the UI or API and verify the status updates correctly on the dashboard.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Status indicators must be accessible

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

an Administrator is using assistive technology like a screen reader

### 3.6.5 When

they navigate to the status column of the jobs list

### 3.6.6 Then

the screen reader clearly announces the full text of the status (e.g., 'Status: Succeeded') for each job.

### 3.6.7 Validation Notes

Perform manual testing with NVDA or JAWS screen readers. Run automated accessibility checks (e.g., Axe) to ensure color contrast and ARIA attributes are correct.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Dashboard displays an empty state message when no jobs exist

### 3.7.3 Scenario Type

Edge_Case

### 3.7.4 Given

an Administrator is logged in and viewing the Job Monitoring Dashboard on a fresh installation

### 3.7.5 When

no report jobs have been executed yet

### 3.7.6 Then

the dashboard displays a clear, user-friendly message indicating that no jobs have run yet, instead of an empty table.

### 3.7.7 Validation Notes

Test on a clean database and verify the empty state message is displayed.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A 'Status' column in the job list table on the monitoring dashboard.
- Iconography and color-coded text/badges to represent each job status (Succeeded, Failed, Running, Queued, Cancelled).

## 4.2.0 User Interactions

- The status should be immediately scannable, allowing the user to quickly identify jobs needing attention.
- Hovering over a status icon could optionally display a tooltip with the timestamp of the last status change.

## 4.3.0 Display Requirements

- The status must be displayed for every job listed on the dashboard.
- The text for each status must be one of: 'Succeeded', 'Failed', 'Running', 'Queued', 'Cancelled'.

## 4.4.0 Accessibility Needs

- Color must not be the only means of conveying the status information, per WCAG 2.1 AA. Icons with appropriate ARIA labels must be used alongside color.
- Text and background colors must meet a minimum contrast ratio of 4.5:1.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "A job's final status is determined by the outcome of its last critical step. Any failure in ingestion, transformation, generation, or delivery results in a 'Failed' status.", 'enforcement_point': 'Backend job processing service.', 'violation_handling': 'N/A - This is a state-setting rule.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-070

#### 6.1.1.2 Dependency Reason

This story adds a status column to the job monitoring dashboard, which must be created first by US-070.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-073

#### 6.1.2.2 Dependency Reason

The 'Cancelled' status cannot be fully tested until the functionality to cancel a job is implemented in US-073.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint (e.g., GET /api/v1/jobs) must exist to provide a list of recent jobs and their current status from the `JobExecutionLog` entity.
- The core job execution engine must correctly persist the status of each job in the SQLite database (`JobExecutionLog` table).

## 6.3.0.0 Data Dependencies

- Requires access to the `JobExecutionLog` table in the application's SQLite database.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to fetch job statuses for the dashboard must have a P95 latency of under 200ms, as per SRS 6.1.
- The dashboard UI should feel responsive, potentially using polling (e.g., every 5-10 seconds) to refresh job statuses without requiring a full page reload.

## 7.2.0.0 Security

- Access to the job monitoring dashboard, and therefore the job statuses, must be restricted to users with the 'Administrator' role.

## 7.3.0.0 Usability

- The visual indicators for status should be intuitive and follow common design patterns (e.g., green for success, red for failure).

## 7.4.0.0 Accessibility

- The feature must be compliant with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The dashboard must render correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge, as per SRS 2.3.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Frontend work is primarily presentational, leveraging the existing MUI component library.
- Backend work involves a straightforward database query and exposing it via an existing or new API endpoint.
- The logic for setting the status is handled by the job execution engine, which is a dependency.

## 8.3.0.0 Technical Risks

- Ensuring the real-time update mechanism (e.g., polling) is efficient and does not place undue load on the server or client.

## 8.4.0.0 Integration Points

- Frontend (React) integrates with the Backend (ASP.NET Core) via a RESTful API call to fetch job data.
- Backend API reads from the SQLite database's `JobExecutionLog` table.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify each of the five statuses (Queued, Running, Succeeded, Failed, Cancelled) is displayed correctly based on the job's state in the database.
- Test the empty state when no jobs have been run.
- Test UI responsiveness on different screen sizes (down to 1280px width).
- Perform screen reader testing to validate accessibility of status indicators.

## 9.3.0.0 Test Data Needs

- A set of jobs in the database representing each possible status.
- A clean database to test the 'no jobs' empty state scenario.

## 9.4.0.0 Testing Tools

- Frontend: Jest, React Testing Library
- Backend: xUnit, Moq
- E2E: A framework like Playwright or Cypress
- Accessibility: Axe browser extension, NVDA/JAWS screen readers

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >= 80% coverage
- Integration testing completed successfully for the API endpoint
- E2E tests created for displaying each status and are passing
- User interface reviewed and approved for visual correctness and usability
- Accessibility requirements (WCAG 2.1 AA) validated
- Documentation for the Job Monitoring dashboard is updated to include descriptions of each status
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a core part of the monitoring feature and should be prioritized high. It depends on US-070 (the dashboard shell) being completed in the same or a prior sprint.
- The backend API endpoint can be developed in parallel with the frontend UI component.

## 11.4.0.0 Release Impact

This is a key feature for the Administrator persona and is essential for the initial release of the monitoring capabilities.

