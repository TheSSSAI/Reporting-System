# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-081 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | View an HTML report in the browser |
| As A User Story | As an End-User, I want to view an HTML report's co... |
| User Persona | End-User (Viewer). Secondary persona: Administrato... |
| Business Value | Improves user efficiency and satisfaction by provi... |
| Functional Area | Report Access |
| Story Theme | Report Viewer Experience |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successfully view an HTML report

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an authenticated user on the Report Viewer page and have permission to view a report named 'Daily Sales Summary' of type HTML

### 3.1.5 When

I click on the 'Daily Sales Summary' report entry in the list

### 3.1.6 Then

A view (e.g., a modal or a dedicated panel) opens within the application, displaying the fully rendered content of the HTML report.

### 3.1.7 Validation Notes

Verify the report's layout, text, and styles are preserved. The view should not be a new browser tab pointing to a raw .html file.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Close the report view

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am currently viewing an HTML report within the Report Viewer

### 3.2.5 When

I click the 'Close' or 'Back' button, or press the 'Escape' key

### 3.2.6 Then

The report view closes, and I am returned to the Report Viewer list page.

### 3.2.7 Validation Notes

Test all three closing mechanisms: button click, icon click, and Escape key press.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempt to view a report with a missing source file

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am on the Report Viewer page, and a report entry exists, but its underlying HTML file has been deleted from the server's storage

### 3.3.5 When

I click to view that report

### 3.3.6 Then

The report view opens but displays a user-friendly error message, such as 'Error: Report content could not be loaded. The file may be missing or corrupt.'

### 3.3.7 Validation Notes

The application should not crash or show a technical error (e.g., a raw 404 page). The error should be displayed within the application's UI.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Loading indicator is displayed for large reports

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

I am on the Report Viewer page

### 3.4.5 When

I click to view an HTML report and the content takes time to load

### 3.4.6 Then

A loading indicator (e.g., a spinner or skeleton screen) is displayed immediately, and it is replaced by the report content once loading is complete.

### 3.4.7 Validation Notes

Simulate network latency using browser developer tools to verify the loading state is visible.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

HTML content is rendered in a secure, sandboxed environment

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

A generated HTML report contains a malicious script, such as '<script>alert("XSS")</script>'

### 3.5.5 When

I view this report in the Report Viewer

### 3.5.6 Then

The report's HTML content is displayed, but the script does not execute, and no alert dialog appears.

### 3.5.7 Validation Notes

Verify the HTML is rendered inside an iframe with a restrictive 'sandbox' attribute that prevents script execution and access to the parent origin.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Attempt to view a report without sufficient permissions

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

I am an authenticated user on the Report Viewer page

### 3.6.5 When

I attempt to fetch the content of a report for which I do not have view permissions (e.g., via a direct API call)

### 3.6.6 Then

The API returns a 403 Forbidden status, and the UI displays an 'Access Denied' message.

### 3.6.7 Validation Notes

This primarily tests the backend API security, ensuring the permission model is enforced.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A clickable element (e.g., 'View' button or the entire row) for HTML reports in the report list.
- A modal dialog or dedicated view panel to display the rendered HTML.
- A prominent 'Close' button (e.g., 'X' icon) within the report view.
- A loading state indicator (e.g., spinner).
- An error message display area within the view panel.

## 4.2.0 User Interactions

- Clicking a report entry opens the view.
- Clicking the close button or pressing 'Escape' closes the view.
- The content within the report view should be scrollable if it exceeds the viewport.

## 4.3.0 Display Requirements

- The report's title should be displayed as the title of the view modal/panel.
- The rendered HTML must maintain its intended styling and structure.

## 4.4.0 Accessibility Needs

- The report view modal must be WCAG 2.1 AA compliant.
- Focus must be trapped within the modal when it is open.
- The modal can be closed using the Escape key.
- All interactive elements (e.g., close button) must have accessible names.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Users can only view reports for which their assigned role has been granted access.

### 5.1.3 Enforcement Point

Backend API endpoint that serves the report content.

### 5.1.4 Violation Handling

The API must return an HTTP 403 Forbidden error.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

All user-generated HTML content must be treated as untrusted and rendered in a sandboxed environment to prevent Cross-Site Scripting (XSS).

### 5.2.3 Enforcement Point

Frontend rendering component.

### 5.2.4 Violation Handling

The browser's security model, enforced by the iframe 'sandbox' attribute, will prevent malicious script execution.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-077

#### 6.1.1.2 Dependency Reason

User must be able to log in to access the Report Viewer.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-078

#### 6.1.2.2 Dependency Reason

The Report Viewer list UI must exist to provide a list of reports to click on.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-086

#### 6.1.3.2 Dependency Reason

The backend must enforce role-based access permissions before serving report content.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint to securely fetch the raw HTML content of a specific generated report, e.g., GET /api/v1/jobs/{jobId}/result/content.
- Frontend component library (MUI) for creating the modal/view panel.
- Frontend state management (Zustand) to manage the view's state (open, loading, error).

## 6.3.0.0 Data Dependencies

- Requires at least one report to have been generated in HTML format and stored by the system.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The report view should open and begin rendering content within 2 seconds for reports up to 1MB on a standard network connection.
- The API endpoint serving the HTML content should have a P95 latency of under 300ms (excluding file transfer time).

## 7.2.0.0 Security

- The system MUST prevent XSS attacks by rendering the HTML content within a sandboxed iframe. The sandbox attribute should be as restrictive as possible, disallowing scripts and same-origin access by default.
- The API endpoint for fetching report content must be protected and require a valid JWT.

## 7.3.0.0 Usability

- The process of viewing a report should be intuitive, requiring only a single click from the report list.
- The report view should be clean and uncluttered, focusing on the report content.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementing the secure sandboxed rendering of untrusted HTML is the primary complexity driver.
- Requires a new, secure backend API endpoint.
- Handling loading and error states gracefully in the UI.
- Ensuring the modal/view component is fully accessible.

## 8.3.0.0 Technical Risks

- An improperly configured sandbox could either break legitimate report functionality (if too restrictive) or create a security vulnerability (if too permissive).
- Large HTML files could impact frontend performance and memory usage.

## 8.4.0.0 Integration Points

- Frontend Report Viewer list component.
- Backend API for fetching report data.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify a standard HTML report renders correctly.
- Verify a report with embedded CSS and images renders correctly.
- Test with a report containing various XSS payloads to confirm the sandbox is effective.
- Test the UI response when the API returns a 403, 404, or 500 error.
- Perform manual keyboard navigation testing to validate accessibility.
- Test with a very large HTML file to observe performance and loading behavior.

## 9.3.0.0 Test Data Needs

- A simple, valid HTML report.
- An HTML report containing malicious script tags.
- A large (5MB+) HTML report.
- A report entry in the database whose corresponding file is missing from storage.

## 9.4.0.0 Testing Tools

- Jest and React Testing Library for frontend unit tests.
- xUnit for backend unit tests.
- Cypress or Playwright for E2E tests.
- Browser developer tools for simulating network conditions and inspecting the DOM.
- Axe for automated accessibility checks.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >80% coverage for new code
- Integration testing between frontend and backend completed successfully
- E2E test scenario for viewing a report is automated and passing
- Security review of the sandboxing implementation is complete and approved
- Accessibility audit (automated and manual) is passed
- Documentation for the new API endpoint is created/updated
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- The backend API endpoint is a prerequisite for complete frontend implementation. It should be developed first or in parallel.
- Requires collaboration between frontend and backend developers.
- Allocation of time for thorough security and accessibility testing is crucial.

## 11.4.0.0 Release Impact

This is a key feature for the Report Viewer, significantly improving its usability and value to end-users.

