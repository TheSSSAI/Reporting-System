# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-080 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Filter reports by date range and status |
| As A User Story | As an End-User (Viewer), I want to filter the list... |
| User Persona | End-User (Viewer). This functionality also benefit... |
| Business Value | Improves user efficiency and satisfaction by makin... |
| Functional Area | Report Viewing & Access |
| Story Theme | Report Viewer Usability Enhancements |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Filter by a single status

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an authenticated user on the Report Viewer page, and there are multiple reports with 'Succeeded' and 'Failed' statuses

### 3.1.5 When

I select 'Succeeded' from the status filter and apply the filters

### 3.1.6 Then

The report list updates to display only the reports with the 'Succeeded' status.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Filter by a date range

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am on the Report Viewer page, and there are reports generated yesterday and today

### 3.2.5 When

I select yesterday's date as both the start and end date in the date range filter and apply the filters

### 3.2.6 Then

The report list updates to display only the reports generated yesterday.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Filter by both date range and status

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I am on the Report Viewer page with a mix of reports from the last two weeks

### 3.3.5 When

I select 'Failed' from the status filter, select a date range covering last week, and apply the filters

### 3.3.6 Then

The report list updates to display only the reports with the 'Failed' status that were generated within last week's date range.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Clear all active filters

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

I have applied filters for 'Succeeded' status and a specific date range, and the report list is showing a filtered view

### 3.4.5 When

I click the 'Clear Filters' button

### 3.4.6 Then

The date range and status filter controls are reset to their default state, and the report list refreshes to show the initial, unfiltered list of all accessible reports.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

No reports match the filter criteria

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

I am on the Report Viewer page

### 3.5.5 When

I apply a filter combination (e.g., a date range with no generated reports) that matches no existing reports

### 3.5.6 Then

The report list area is empty and displays a user-friendly message, such as 'No reports match the selected filters.'

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Attempt to apply an invalid date range

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

I am on the Report Viewer page and interacting with the date range filter

### 3.6.5 When

I select a start date that is after the end date and click 'Apply Filters'

### 3.6.6 Then

A validation error message is displayed near the date filter controls, and the API call to filter the list is not made.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Filter state is maintained during pagination

### 3.7.3 Scenario Type

Happy_Path

### 3.7.4 Given

I have applied a filter that results in more than one page of reports

### 3.7.5 When

I navigate to the next page of the report list

### 3.7.6 Then

The filter criteria remain active, and the second page displays the correct subsequent set of filtered reports.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Filters work in conjunction with text search

### 3.8.3 Scenario Type

Alternative_Flow

### 3.8.4 Given

I am on the Report Viewer page and have searched for reports with 'Sales' in the name

### 3.8.5 When

I then apply a status filter for 'Succeeded'

### 3.8.6 Then

The list updates to show only reports with 'Sales' in the name AND a status of 'Succeeded'.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A date range picker component with 'Start Date' and 'End Date' inputs.
- A multi-select dropdown or checkbox group for 'Status' (options: Succeeded, Failed, etc.).
- An 'Apply Filters' button to trigger the search.
- A 'Clear Filters' button to reset all filter inputs to their default state.

## 4.2.0 User Interactions

- Selecting a date from the date picker populates the corresponding input field.
- Clicking 'Apply Filters' sends a request to the backend API with the selected filter parameters and updates the report list.
- Clicking 'Clear Filters' resets the state of the filter components and re-fetches the default report list.
- The filter controls should be disabled while data is being fetched after applying filters.

## 4.3.0 Display Requirements

- When no reports match the filters, a clear message must be displayed in the list area.
- A visual indicator (e.g., active state on the filter button, a summary of active filters) should be present when filters are applied.

## 4.4.0 Accessibility Needs

- All filter controls (date pickers, dropdowns, buttons) must be fully keyboard accessible (navigable via Tab, operable via Enter/Space).
- All controls must have appropriate ARIA labels for screen reader compatibility.
- Complies with WCAG 2.1 Level AA standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Filtering logic must be executed on the server-side to ensure performance and scalability, especially with large numbers of reports.

### 5.1.3 Enforcement Point

Backend API endpoint for fetching generated reports.

### 5.1.4 Violation Handling

N/A (Architectural constraint)

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A start date in the date range filter cannot be later than the end date.

### 5.2.3 Enforcement Point

Frontend UI validation before submitting the filter request.

### 5.2.4 Violation Handling

Display a validation error message to the user and prevent the API call.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-078

#### 6.1.1.2 Dependency Reason

The basic list view for generated reports must exist before filtering functionality can be added to it.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-077

#### 6.1.2.2 Dependency Reason

Users must be able to log in and access the Report Viewer page.

## 6.2.0.0 Technical Dependencies

- The backend API must have an endpoint to fetch generated reports (from the JobExecutionLog entity). This story requires modifying that endpoint to accept query parameters for filtering.
- The frontend must use a UI component library (MUI v5) that provides accessible date picker and select components.
- The JobExecutionLog entity in the database must have indexed columns for generation date and status to ensure query performance.

## 6.3.0.0 Data Dependencies

- Requires test data in the `JobExecutionLog` table with a variety of statuses and generation timestamps to properly validate the filtering functionality.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API response time for a filtered query should have a P95 latency of under 500ms, assuming proper database indexing.
- The UI should not freeze or lag when the user interacts with the filter controls.

## 7.2.0.0 Security

- All filter parameters sent to the backend must be properly sanitized to prevent injection attacks (e.g., SQL injection).
- The API endpoint must enforce user permissions, ensuring users can only see filtered results from reports they are authorized to view.

## 7.3.0.0 Usability

- The filter controls should be intuitive and follow common web design patterns.
- It should be visually clear when filters are active.

## 7.4.0.0 Accessibility

- Must adhere to WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge, as per SRS 2.3.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires coordinated changes to both frontend and backend.
- Frontend state management to handle filter state alongside existing search and pagination state.
- Backend API modification to dynamically build a database query based on optional filter parameters.
- Requires adding or verifying database indexes on `JobExecutionLog` table for performance.

## 8.3.0.0 Technical Risks

- Poorly constructed dynamic queries on the backend could lead to performance degradation or security vulnerabilities if not handled carefully with a framework like EF Core.
- Complex interactions between filtering, searching, and pagination state on the frontend could lead to bugs if not managed properly.

## 8.4.0.0 Integration Points

- Frontend: Report Viewer component.
- Backend: API endpoint for listing `JobExecutionLog` entries.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify filtering by each status individually.
- Verify filtering by a date range where start and end dates are the same.
- Verify filtering by a date range spanning multiple days.
- Verify filtering with all criteria applied (date range, status, and text search).
- Verify the 'Clear Filters' functionality resets the UI and the data list correctly.
- Verify the 'No results' message appears when appropriate.
- Verify UI validation for invalid date ranges.

## 9.3.0.0 Test Data Needs

- A set of at least 50 `JobExecutionLog` records with various statuses ('Succeeded', 'Failed') and timestamps spanning at least a month.

## 9.4.0.0 Testing Tools

- Frontend: Jest, React Testing Library.
- Backend: xUnit, Moq.
- E2E: Playwright or Cypress.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests for frontend components and backend logic implemented and passing with >80% coverage for new code
- API integration testing completed successfully for the modified endpoint
- End-to-end tests for key filtering scenarios are implemented and passing
- User interface reviewed for usability and adherence to design specifications
- Accessibility requirements (WCAG 2.1 AA) validated
- API documentation (OpenAPI/Swagger) is updated to reflect new query parameters
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational feature for the Report Viewer's usability and should be prioritized early in the development of that module.
- Requires both a frontend and backend developer, or a full-stack developer.

## 11.4.0.0 Release Impact

- Significantly improves the user experience of the Report Viewer, making it a key feature for the initial release.

