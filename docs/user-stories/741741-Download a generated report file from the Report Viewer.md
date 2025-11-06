# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-082 |
| Elaboration Date | 2025-01-18 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Download a generated report file from the Report V... |
| As A User Story | As an End-User (Viewer) browsing the Report Viewer... |
| User Persona | End-User (Viewer). This functionality also applies... |
| Business Value | Enables data portability, allowing users to utiliz... |
| Functional Area | Report Access |
| Story Theme | Report Viewing & Access |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful download of a PDF report

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an authenticated user on the Report Viewer page and have permission to access a generated report named 'Monthly Sales' in PDF format

### 3.1.5 When

I click the 'Download' action for the 'Monthly Sales' report

### 3.1.6 Then

my browser initiates a standard file download, the downloaded file is named in the format 'Monthly_Sales_[Timestamp].pdf', and the file is a valid, non-corrupt PDF document.

### 3.1.7 Validation Notes

Verify the HTTP response headers include 'Content-Type: application/pdf' and 'Content-Disposition' is set to 'attachment'.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Successful download of a CSV report

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am an authenticated user on the Report Viewer page and have permission to access a generated report named 'User Data' in CSV format

### 3.2.5 When

I click the 'Download' action for the 'User Data' report

### 3.2.6 Then

my browser initiates a file download for a valid CSV file named 'User_Data_[Timestamp].csv'.

### 3.2.7 Validation Notes

Verify the HTTP response headers include 'Content-Type: text/csv'.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Successful download of other supported report formats (JSON, TXT, HTML)

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I am an authenticated user on the Report Viewer page with access to generated reports in JSON, TXT, and HTML formats

### 3.3.5 When

I click the 'Download' action for each of these reports

### 3.3.6 Then

a file download is initiated for each with the correct file extension (.json, .txt, .html) and appropriate MIME type.

### 3.3.7 Validation Notes

Check for 'application/json', 'text/plain', and 'text/html' MIME types respectively.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Attempt to download a report where the underlying file is missing

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am viewing a report entry in the Report Viewer, but its corresponding file has been deleted from the server's storage

### 3.4.5 When

I click the 'Download' action for that report

### 3.4.6 Then

a user-friendly, non-blocking error message (e.g., a toast notification) is displayed in the UI stating 'Error: Report file could not be found.' and no file download is initiated.

### 3.4.7 Validation Notes

The backend API should return an appropriate error code, such as HTTP 404 Not Found, which the frontend handles gracefully.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Attempt to download a report without sufficient permissions

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am an authenticated user and I attempt to access a direct download URL for a report I do not have permission to view

### 3.5.5 When

the HTTP request is sent to the server

### 3.5.6 Then

the server must reject the request with an HTTP 403 Forbidden status code and the download must be blocked.

### 3.5.7 Validation Notes

This should be tested by crafting a direct API call with a valid token for a user who lacks permissions for the target report.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A distinct 'Download' icon button (e.g., an arrow pointing down into a tray) must be present for each report entry in the Report Viewer list.

## 4.2.0 User Interactions

- Clicking the 'Download' icon initiates the file download.
- Hovering over the 'Download' icon displays a tooltip with the full filename, e.g., 'Download Sales_Report_2025-01-18T10-30-00.pdf'.

## 4.3.0 Display Requirements

- The download action must be available for all generated report types (HTML, PDF, JSON, CSV, TXT).
- Error messages for failed downloads must be displayed clearly to the user without disrupting the page.

## 4.4.0 Accessibility Needs

- The download icon button must be keyboard-focusable and operable (e.g., using Enter key).
- The button must have an appropriate ARIA label, such as 'Download report [Report Name] generated on [Date]'.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'A user can only download reports they have been granted access to view, as defined by role-based access controls.', 'enforcement_point': 'Backend API endpoint responsible for serving the file.', 'violation_handling': 'The server returns an HTTP 403 Forbidden response, and the download is blocked.'}

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

The Report Viewer UI with a list of reports must exist to add a download button to it.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-086

#### 6.1.3.2 Dependency Reason

The permission system must be in place for the backend to validate user access rights before serving the file.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint for securely serving files.
- Access to the storage location where generated reports are saved.
- The JobExecutionLog entity in the database to look up report metadata and file paths.

## 6.3.0.0 Data Dependencies

- Requires generated report files to exist in the system's configured storage.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The backend must stream large files instead of loading them entirely into memory to maintain low memory usage and support large reports.
- Download initiation should be near-instantaneous (<500ms) after the user clicks the button.

## 7.2.0.0 Security

- The file download endpoint must be protected and require a valid JWT.
- The endpoint must perform authorization checks to ensure the user has rights to the requested report.
- Direct, unauthenticated access to report files via URL must be prevented.

## 7.3.0.0 Usability

- The download mechanism should use the browser's native download functionality for a familiar user experience.
- Downloaded filenames should be human-readable and include the report name and a timestamp to avoid naming conflicts.

## 7.4.0.0 Accessibility

- The UI must comply with WCAG 2.1 Level AA standards as per the SRS.

## 7.5.0.0 Compatibility

- The download functionality must work correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires a secure, streaming-capable backend endpoint.
- Integration of permission checks into the download logic.
- Graceful error handling on both frontend and backend for cases like missing files.

## 8.3.0.0 Technical Risks

- Improper handling of large files could lead to high server memory consumption.
- Insecure implementation could expose sensitive report data to unauthorized users (e.g., path traversal vulnerabilities or insecure direct object references).

## 8.4.0.0 Integration Points

- Frontend: React component for the Report Viewer list.
- Backend: ASP.NET Core Identity for authentication/authorization.
- Backend: Entity Framework Core for accessing the JobExecutionLog.
- Backend: System's file storage for retrieving the report artifact.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify download of each supported file type (PDF, CSV, JSON, TXT, HTML).
- Test download attempt with an unauthorized user.
- Test download attempt for a file that has been manually deleted from storage.
- Test download of a large file (>100MB) to ensure streaming is effective.
- Verify keyboard navigation to and activation of the download button.

## 9.3.0.0 Test Data Needs

- User accounts with 'Viewer' and 'Administrator' roles.
- Generated reports of each supported file type, including one very large file.
- A report entry in the database whose corresponding physical file is deleted.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- An E2E testing framework like Cypress or Playwright.
- Browser developer tools for inspecting network requests and headers.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for backend and frontend, achieving >= 80% coverage
- Integration testing completed successfully for the API endpoint
- E2E test simulating a user downloading a file is implemented and passing
- User interface reviewed and approved for usability and accessibility
- Performance requirements for large file streaming verified
- Security requirements validated, including permission checks and prevention of direct access
- Functionality verified on all supported browsers
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a core feature for the Report Viewer and a high-priority user expectation. It should be scheduled in an early sprint focused on the Report Viewer functionality.
- Depends on the completion of the basic report list view (US-078).

## 11.4.0.0 Release Impact

The Report Viewer feature is incomplete and of limited value without the ability for users to download reports. This story is critical for the feature's release.

