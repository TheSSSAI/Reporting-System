# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-070 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | View a real-time job monitoring dashboard |
| As A User Story | As an Administrator, I want to view a real-time jo... |
| User Persona | Administrator |
| Business Value | Provides critical operational visibility into the ... |
| Functional Area | System Administration & Monitoring |
| Story Theme | Job Monitoring & Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Dashboard displays recent jobs on load

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an Administrator logged into the Control Panel and multiple report jobs have been executed

### 3.1.5 When

I navigate to the 'Job Monitoring' dashboard page

### 3.1.6 Then

I see a paginated table listing the most recent job executions, sorted by start time in descending order.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Dashboard displays essential job information

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am viewing the job monitoring dashboard with at least one job listed

### 3.2.5 When

I inspect a row for a single job execution

### 3.2.6 Then

The table must display columns for 'Report Name', 'Status', 'Start Time', 'End Time', and 'Duration'.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Dashboard displays clear visual status indicators

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I am viewing the job monitoring dashboard

### 3.3.5 When

I observe the 'Status' column for jobs in different states

### 3.3.6 Then

The status is displayed with a distinct color and/or icon: Queued (grey), Running (blue), Succeeded (green), Failed (red), and Cancelled (orange).

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Dashboard data updates automatically

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

I am viewing the job monitoring dashboard

### 3.4.5 And

a new report job is triggered in the background

### 3.4.6 When

I wait for the automatic refresh interval (15 seconds) to pass

### 3.4.7 Then

The new job appears at the top of the list without requiring a manual page reload.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Dashboard displays an empty state message

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

I am an Administrator on a new system where no jobs have been executed

### 3.5.5 When

I navigate to the 'Job Monitoring' dashboard

### 3.5.6 Then

I see a user-friendly message like 'No report jobs have been executed yet' instead of an empty table.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Dashboard handles API connection errors gracefully

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

I am an Administrator viewing the job monitoring dashboard

### 3.6.5 And

the frontend loses connection to the backend API

### 3.6.6 When

the dashboard attempts to refresh its data

### 3.6.7 Then

a clear, non-disruptive error message is displayed indicating that the job list could not be updated.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

User can navigate to detailed job logs

### 3.7.3 Scenario Type

Happy_Path

### 3.7.4 Given

I am viewing the job monitoring dashboard

### 3.7.5 When

I click on any job row in the table

### 3.7.6 Then

I am navigated to the detailed execution log view for that specific job instance.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Dashboard includes pagination for large numbers of jobs

### 3.8.3 Scenario Type

Happy_Path

### 3.8.4 Given

There are more than 25 job executions recorded in the system

### 3.8.5 When

I view the job monitoring dashboard

### 3.8.6 Then

I see pagination controls at the bottom of the table, allowing me to navigate through pages of job history.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A data table/grid to display the list of jobs.
- Pagination controls (e.g., 'Previous', 'Next', page numbers).
- Color-coded status badges/icons.
- A manual 'Refresh' button.
- A loading indicator shown during data fetches.
- An error message area for API failures.

## 4.2.0 User Interactions

- The job list automatically updates every 15 seconds.
- Clicking a job row navigates to its detail page.
- Hovering over a truncated report name displays the full name in a tooltip.
- Users can click pagination controls to view older job records.

## 4.3.0 Display Requirements

- Timestamps ('Start Time', 'End Time') must be displayed in the user's local time zone or a clearly indicated system time zone.
- Duration should be displayed in a human-readable format (e.g., '1m 15s').
- The dashboard should be accessible via a main navigation item in the Control Panel called 'Monitoring' or 'Jobs'.

## 4.4.0 Accessibility Needs

- The data table must use proper `<table>`, `<thead>`, `<tbody>`, and `<th>` tags for screen reader compatibility.
- Status indicators must have accessible text alternatives (e.g., `<span class='status-green'>Succeeded</span>`).
- All interactive elements (buttons, links, pagination) must be keyboard-focusable and operable.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "Access to the job monitoring dashboard is restricted to users with the 'Administrator' role.", 'enforcement_point': 'Server-side API endpoint and client-side routing.', 'violation_handling': "User is redirected to a '403 Forbidden' or 'Not Authorized' page."}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-019

#### 6.1.1.2 Dependency Reason

Requires the existence of an 'Administrator' role for access control.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-051

#### 6.1.2.2 Dependency Reason

Requires the ability to create report configurations that can be executed as jobs.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-072

#### 6.1.3.2 Dependency Reason

This dashboard must link to the detailed job log view, which is defined in US-072.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint (e.g., GET /api/v1/jobs) must exist to provide paginated job execution data from the `JobExecutionLog` entity.
- The core job scheduling and execution engine (Quartz.NET) must be implemented to generate job data.
- The `JobExecutionLog` table in the SQLite database must be defined and populated by the execution engine.

## 6.3.0.0 Data Dependencies

- Requires job execution records to be present in the `JobExecutionLog` database table for display.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The initial API call to load the first page of jobs must complete in under 500ms.
- Subsequent polling API calls must be lightweight and complete in under 200ms.
- The UI must remain responsive and not exhibit any lag or freezing during the automatic data refresh.

## 7.2.0.0 Security

- The API endpoint for fetching job data must be protected and only accessible to authenticated users with the 'Administrator' role.

## 7.3.0.0 Usability

- The dashboard must be intuitive, providing an at-a-glance understanding of system status.
- Error messages must be clear and user-friendly.

## 7.4.0.0 Accessibility

- The dashboard must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The dashboard must render correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementing a reliable and performant client-side polling mechanism for real-time updates.
- Creating an efficient, paginated backend query to fetch job data.
- Ensuring the UI state is managed correctly during automatic refreshes to provide a smooth user experience.

## 8.3.0.0 Technical Risks

- A naive polling implementation could cause performance issues or memory leaks on the client.
- The database query for jobs could become slow over time if not properly indexed on the 'Start Time' column.

## 8.4.0.0 Integration Points

- Frontend React component for the dashboard.
- Backend ASP.NET Core API controller for serving job data.
- Entity Framework Core query against the `JobExecutionLog` table in the SQLite database.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify dashboard loads and displays jobs correctly.
- Verify the empty state is shown when no jobs exist.
- Verify API error handling.
- Verify auto-refresh adds new jobs to the list.
- Verify pagination works correctly.
- Verify clicking a job navigates to the correct detail page.
- Verify access is denied for non-Administrator users.

## 9.3.0.0 Test Data Needs

- A set of seeded `JobExecutionLog` records with various statuses (Succeeded, Failed, Running, etc.).
- A test setup with more than one page of job records (e.g., >25) to test pagination.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend component tests.
- Playwright or Cypress for E2E tests.
- Axe for accessibility scanning.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >= 80% coverage
- Integration testing completed successfully
- E2E test for the primary happy path is implemented and passing
- User interface reviewed and approved for UX and accessibility compliance
- Performance requirements verified
- Security requirements validated
- Documentation for the feature is created or updated
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a key feature for system administration and should be prioritized soon after the core job execution logic is complete.
- Requires both backend (API endpoint) and frontend (UI component) development, which can be done in parallel.

## 11.4.0.0 Release Impact

- Provides essential monitoring capabilities required for a production-ready release. Without this, administrators have no visibility into system operations.

