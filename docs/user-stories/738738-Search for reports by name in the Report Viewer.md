# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-079 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Search for reports by name in the Report Viewer |
| As A User Story | As an End-User (Viewer), I want to enter text into... |
| User Persona | End-User (Viewer). This functionality also benefit... |
| Business Value | Improves user efficiency and satisfaction by enabl... |
| Functional Area | Report Access (Report Viewer) |
| Story Theme | Report Viewing & Access |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Basic search with partial name match

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am on the Report Viewer page and a list of generated reports is displayed, including one named 'Daily Sales Summary'

### 3.1.5 When

I type 'Sales' into the report name search box

### 3.1.6 Then

The list of reports updates to show only reports whose names contain 'Sales', including 'Daily Sales Summary'.

### 3.1.7 Validation Notes

Verify that the filtering is applied after a short delay (debounce) and the resulting list is accurate.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Search is case-insensitive

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am on the Report Viewer page with a report named 'Monthly Revenue Report'

### 3.2.5 When

I type 'monthly revenue' into the search box

### 3.2.6 Then

The 'Monthly Revenue Report' is displayed in the filtered list.

### 3.2.7 Validation Notes

Test with various casings like 'MONTHLY', 'monthly', and 'MoNtHlY' to ensure consistent results.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Clearing the search term

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I have searched for 'Sales' and the report list is filtered

### 3.3.5 When

I delete all text from the search box or click the 'clear' icon within the input

### 3.3.6 Then

The search filter is removed, and the report list reverts to its previous state (e.g., showing all reports or respecting other active filters like date range).

### 3.3.7 Validation Notes

Verify the list correctly repopulates to its pre-search state.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Search yields no results

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am on the Report Viewer page

### 3.4.5 When

I type a search term like 'nonexistent_report_xyz' that does not match any report names

### 3.4.6 Then

The report list becomes empty, and a user-friendly message such as 'No reports found matching your criteria.' is displayed.

### 3.4.7 Validation Notes

Ensure the message is clear and the list area is properly handled visually when empty.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Search works in conjunction with other filters

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

I have filtered the report list to show only reports with 'Succeeded' status from the last 7 days

### 3.5.5 When

I then type 'Sales' into the search box

### 3.5.6 Then

The list is further filtered to show only reports from the last 7 days with a 'Succeeded' status AND whose names contain 'Sales'.

### 3.5.7 Validation Notes

Test the additive nature of filters. Clearing the name search should leave the other filters (status, date) active.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Search input with leading/trailing spaces

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

I am on the Report Viewer page with a report named 'Quarterly Review'

### 3.6.5 When

I type '  Quarterly Review  ' (with leading and trailing spaces) into the search box

### 3.6.6 Then

The input is trimmed, and the search is performed for 'Quarterly Review', successfully finding the report.

### 3.6.7 Validation Notes

Verify that whitespace is handled gracefully and does not affect search results.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A text input field, clearly labeled 'Search by name' or similar, placed prominently above the report list.
- A magnifying glass icon associated with the search input.
- A small 'X' icon inside the search input that appears when text is entered, allowing the user to clear the search with one click.
- A message area to display 'No reports found' when applicable.

## 4.2.0 User Interactions

- As the user types, the report list should update automatically after a short delay (e.g., 300ms debounce) to avoid excessive API calls.
- The search input should be focusable via keyboard navigation (e.g., Tab key).
- Pressing 'Enter' in the search box can trigger the search immediately, but is not required due to the debounce mechanism.

## 4.3.0 Display Requirements

- The search term entered by the user must remain visible in the search box while the filter is active.
- The total count of matching reports should be updated to reflect the filtered results.

## 4.4.0 Accessibility Needs

- The search input must have a corresponding `<label>` element for screen readers.
- The search input must have a high contrast ratio and be easily visible.
- Any status messages (like 'No reports found') should be announced by screen readers using ARIA live regions.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'The search operation must only return reports that the currently logged-in user is permitted to view, as defined by their role and configured permissions (ref US-086).', 'enforcement_point': 'Backend API query', 'violation_handling': 'The API must never return data for reports the user is not authorized to see. The query must include an authorization check (e.g., WHERE user_id = X OR role IN (...)).'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

- {'story_id': 'US-078', 'dependency_reason': 'This story enhances the report list view. The list view must exist before a search function can be added to it.'}

## 6.2.0 Technical Dependencies

- A backend API endpoint that can serve a list of generated reports and accept a query parameter for name-based filtering.
- Frontend state management (e.g., Zustand) to handle the search term and filtered results.

## 6.3.0 Data Dependencies

- Requires access to the `JobExecutionLog` entity or a similar data source that stores the names and metadata of generated reports.

## 6.4.0 External Dependencies

*No items available*

# 7.0.0 Non Functional Requirements

## 7.1.0 Performance

- The API response for a search query must be returned in under 500ms for up to 10,000 report records.
- The UI filtering action must feel instantaneous to the user, with no noticeable lag while typing in the search box.

## 7.2.0 Security

- All user-provided search input must be sanitized on the backend to prevent SQL injection or other injection-based attacks.
- The API endpoint must enforce the authorization rules defined in BR-001.

## 7.3.0 Usability

- The search functionality should be intuitive and require no user training.
- The search box should be in a consistent and predictable location on the page.

## 7.4.0 Accessibility

- The search feature must be compliant with WCAG 2.1 Level AA standards.

## 7.5.0 Compatibility

- The feature must function correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge.

# 8.0.0 Implementation Considerations

## 8.1.0 Complexity Assessment

Low

## 8.2.0 Complexity Factors

- Requires coordinated changes between the frontend (React UI component) and the backend (API endpoint modification).
- The decision to use server-side filtering is critical for scalability. The backend query must be optimized (e.g., using a database index on the report name column).
- Implementing a debounce mechanism on the frontend is necessary to ensure good performance and prevent API abuse.

## 8.3.0 Technical Risks

- Poor database query performance for the search could lead to a slow user experience. The report name column in the database should be indexed.
- If client-side filtering is chosen for simplicity, it may lead to performance issues with a large number of reports, requiring a future refactor.

## 8.4.0 Integration Points

- The frontend search component will integrate with the API service layer to fetch filtered data.
- The backend API controller for fetching reports will integrate with the data access layer to modify the database query.

# 9.0.0 Testing Requirements

## 9.1.0 Testing Types

- Unit
- Integration
- E2E
- Performance
- Security

## 9.2.0 Test Scenarios

- Verify search with partial, full, and case-insensitive terms.
- Verify behavior when no results are found.
- Verify clearing the search restores the list.
- Verify search works correctly when combined with other filters (date, status).
- Verify search input is sanitized against common injection strings.
- Verify performance with a large number of report records in the database.

## 9.3.0 Test Data Needs

- A set of generated reports with varied names, including names with spaces, numbers, and special characters.
- A test user account with access to a specific subset of reports to verify permissions are enforced.

## 9.4.0 Testing Tools

- Jest and React Testing Library for frontend unit/integration tests.
- xUnit for backend unit tests.
- A browser automation tool like Playwright or Cypress for E2E tests.

# 10.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit tests implemented for both frontend and backend components, achieving >80% coverage
- Integration tests validating the full flow from UI to database are passing
- E2E tests for key user scenarios are implemented and passing
- Backend API endpoint is secured and validated against injection attacks
- UI is responsive and meets WCAG 2.1 AA accessibility standards
- Any relevant user documentation for the Report Viewer is updated
- Story deployed and verified in the staging environment

# 11.0.0 Planning Information

## 11.1.0 Story Points

3

## 11.2.0 Priority

🔴 High

## 11.3.0 Sprint Considerations

- This story should be worked on after US-078 is complete.
- Coordination is needed with the developer working on US-080 (Filter by date/status) to ensure a consistent API design for combining filters.

## 11.4.0 Release Impact

This is a significant usability improvement for the Report Viewer and a core feature for end-users. It should be included in the next minor or major release.

