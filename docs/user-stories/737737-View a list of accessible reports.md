# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-078 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | View a list of accessible reports |
| As A User Story | As an End-User (Viewer), I want to see a list of a... |
| User Persona | End-User (Viewer). This also applies to Administra... |
| Business Value | Enables users to discover and access the reports r... |
| Functional Area | Report Viewer |
| Story Theme | Report Access and Consumption |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Viewer sees only permitted reports

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an End-User logged in with the 'Viewer' role, and this role has been granted access to 'Report A' but not 'Report B'

### 3.1.5 When

I navigate to the Report Viewer page

### 3.1.6 Then

I see all generated instances of 'Report A' in the list, and I do not see any instances of 'Report B'.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Administrator sees all reports

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am an Administrator, and 'Report A' and 'Report B' have both been generated

### 3.2.5 When

I navigate to the Report Viewer page

### 3.2.6 Then

I see all generated instances of both 'Report A' and 'Report B' in the list.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User with no permissions sees an empty state

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

I am an End-User logged in with a role that has no report access permissions granted

### 3.3.5 When

I navigate to the Report Viewer page

### 3.3.6 Then

I see a message indicating that no reports are available or I do not have permission to view any.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Report list displays required information

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

I am viewing the list of accessible reports

### 3.4.5 When

The list is displayed

### 3.4.6 Then

Each row must contain the Report Name, Generation Timestamp, Status ('Succeeded', 'Failed'), and Output Format ('PDF', 'CSV', etc.).

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Report list is sorted by most recent by default

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

I am viewing the list of accessible reports

### 3.5.5 When

The page initially loads

### 3.5.6 Then

The reports are sorted by the Generation Timestamp in descending order.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Report list supports pagination for large result sets

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

There are more than 50 generated reports that I am permitted to view

### 3.6.5 When

I navigate to the Report Viewer page

### 3.6.6 Then

The list displays the first 50 reports, and pagination controls are visible to navigate to subsequent pages.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

API failure is handled gracefully

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

I am a logged-in user

### 3.7.5 When

I navigate to the Report Viewer page and the backend API call to fetch reports fails

### 3.7.6 Then

The UI displays a user-friendly error message, such as 'Could not load reports. Please try again later.'

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A data table/grid to display the list of reports.
- Table columns for: Report Name, Generation Timestamp, Status, Format.
- Pagination controls (e.g., page numbers, next/previous buttons).
- A loading indicator (e.g., spinner) to show while data is being fetched.
- An empty state message for when no reports are available.
- An error state message for API failures.

## 4.2.0 User Interactions

- The page fetches and displays the list of reports upon loading.
- Users can click pagination controls to navigate through the list of reports.

## 4.3.0 Display Requirements

- The Generation Timestamp should be formatted for human readability (e.g., 'YYYY-MM-DD HH:mm:ss').
- The Status column should use clear indicators, such as colored icons or badges, to distinguish between 'Succeeded' and 'Failed'.

## 4.4.0 Accessibility Needs

- The report list table must use proper semantic HTML (`<table>`, `<thead>`, `<tbody>`, `<th>`, `<tr>`, `<td>`).
- Table headers (`<th>`) must have the correct `scope` attribute.
- Pagination controls must be keyboard-navigable and have appropriate ARIA labels.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A user can only view generated report instances for which their assigned role(s) have been granted explicit access.

### 5.1.3 Enforcement Point

Backend API endpoint (`GET /api/v1/generated-reports`)

### 5.1.4 Violation Handling

The report instance is excluded from the result set returned by the API. No error is thrown.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Users with the 'Administrator' role bypass all explicit report access permissions and can view all generated reports.

### 5.2.3 Enforcement Point

Backend API endpoint (`GET /api/v1/generated-reports`)

### 5.2.4 Violation Handling

N/A

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-027

#### 6.1.1.2 Dependency Reason

User must be authenticated to determine their identity and roles.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-019

#### 6.1.2.2 Dependency Reason

The permission model is based on assigning roles to users.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-086

#### 6.1.3.2 Dependency Reason

This story defines the mechanism for granting roles access to reports. US-078 consumes this permission data to filter the list.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-077

#### 6.1.4.2 Dependency Reason

Provides the login mechanism and entry point for the Report Viewer where this list will be displayed.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity for user authentication and role management.
- A database schema that links users, roles, report configurations, and report permissions.
- A defined API endpoint for fetching generated reports.

## 6.3.0.0 Data Dependencies

- Requires `JobExecutionLog` records to exist in the database to populate the list.
- Requires user, role, and permission configuration data to exist for filtering.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API endpoint for fetching the report list must have a P95 latency of under 200ms under normal load (100 concurrent users), as per SRS 6.1.
- The database query must be optimized with appropriate indexes to handle a large number of generated reports and permission checks efficiently.

## 7.2.0.0 Security

- The API endpoint must be secured and require a valid JWT.
- All permission checks MUST be enforced on the backend. The frontend should never receive data the user is not authorized to see.

## 7.3.0.0 Usability

- The report list should be easy to scan and understand at a glance.
- Loading and error states must provide clear feedback to the user.

## 7.4.0.0 Accessibility

- The Report Viewer interface must conform to WCAG 2.1 Level AA standards, as per SRS 8.4.

## 7.5.0.0 Compatibility

- The Report Viewer must function correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge, as per SRS 2.3.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The primary complexity lies in the backend database query, which requires joining multiple tables (Users, Roles, ReportPermissions, JobExecutionLogs) to enforce RBAC correctly and performantly.
- Implementing efficient server-side pagination for this complex query.
- Frontend state management for data, loading, error, and pagination states.

## 8.3.0.0 Technical Risks

- A poorly constructed database query could lead to significant performance degradation as the number of users, reports, and permissions grows.
- Incorrect implementation of the security logic on the backend could lead to data leakage.

## 8.4.0.0 Integration Points

- Backend: ASP.NET Core Identity for retrieving the current user's roles.
- Backend: Entity Framework Core for querying the SQLite database.
- Frontend: React application state management (Zustand) and data fetching library (e.g., Axios, fetch).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Performance
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify a 'Viewer' user sees only their permitted reports.
- Verify an 'Administrator' sees all reports.
- Verify a user with no permissions sees the correct empty state.
- Verify pagination works correctly when the number of reports exceeds the page size.
- Verify the API endpoint returns a 401 Unauthorized error for unauthenticated requests.
- Verify the performance of the API endpoint under simulated load.

## 9.3.0.0 Test Data Needs

- A set of test users with different roles ('Administrator', 'Viewer', 'ViewerWithNoPermissions').
- Multiple report configurations.
- Generated report instances (`JobExecutionLog` entries) for all reports.
- Report permission records linking roles to specific reports.

## 9.4.0.0 Testing Tools

- Backend: xUnit, Moq.
- Frontend: Jest, React Testing Library.
- E2E: Playwright or Cypress.
- API Testing: Postman or an integration test suite using `WebApplicationFactory`.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for backend logic and frontend components, achieving >= 80% coverage
- Integration testing for the API endpoint's RBAC logic completed successfully
- E2E tests for the user login and report viewing flow are passing
- User interface reviewed and approved for usability and adherence to design
- Performance requirements for the API endpoint are verified
- Security requirements (backend RBAC enforcement) are validated via testing
- Accessibility of the UI is validated against WCAG 2.1 AA standards
- API endpoint is documented in the OpenAPI specification
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a blocker for most other Report Viewer functionality (e.g., viewing/downloading a specific report).
- Requires prerequisite stories, especially US-086 (Configure Permissions), to be completed first.

## 11.4.0.0 Release Impact

This is a core feature of the Report Viewer and is essential for the initial release.

