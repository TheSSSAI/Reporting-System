# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-102 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Export the audit log with filtering and format sel... |
| As A User Story | As an Administrator, I want to export the audit lo... |
| User Persona | Administrator |
| Business Value | Enables compliance with regulatory requirements (e... |
| Functional Area | System Administration & Security |
| Story Theme | System Auditing and Compliance |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Exporting a filtered audit log as a CSV file

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an Administrator logged into the Control Panel and am on the Audit Log page, and there are audit log entries within a specific date range

### 3.1.5 When

I select the start and end dates for the range, choose 'CSV' as the export format, and initiate the export

### 3.1.6 Then

The system generates a CSV file and my browser initiates a download of that file.

### 3.1.7 Validation Notes

Verify the downloaded file is named descriptively (e.g., 'audit-log-export_YYYY-MM-DD_to_YYYY-MM-DD.csv'). The file must contain a header row ('Timestamp', 'User', 'SourceIP', 'Action', 'Outcome') and all rows must correspond to the log entries within the selected date range.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Exporting a filtered audit log as a JSON file

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am an Administrator logged into the Control Panel and am on the Audit Log page, and there are audit log entries within a specific date range

### 3.2.5 When

I select the start and end dates for the range, choose 'JSON' as the export format, and initiate the export

### 3.2.6 Then

The system generates a JSON file and my browser initiates a download of that file.

### 3.2.7 Validation Notes

Verify the downloaded file is named descriptively (e.g., 'audit-log-export_YYYY-MM-DD_to_YYYY-MM-DD.json'). The file must contain a single JSON array where each object represents a log entry with keys ('timestamp', 'user', 'sourceIP', 'action', 'outcome') and all objects correspond to the log entries within the selected date range.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Exporting an unfiltered (full) audit log

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

I am an Administrator on the Audit Log page

### 3.3.5 When

I do not select a date range, choose a format (CSV or JSON), and initiate the export

### 3.3.6 Then

The system generates a file containing all audit log entries in the database.

### 3.3.7 Validation Notes

The filename should reflect that it's a full export (e.g., 'audit-log-export_full.csv'). The content should match the entire content of the audit log table.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Exporting when the selected date range has no log entries

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

I am an Administrator on the Audit Log page

### 3.4.5 When

I select a date range that contains no audit log entries and initiate an export

### 3.4.6 Then

For CSV format, a file is downloaded containing only the header row.

### 3.4.7 Validation Notes

For JSON format, a file is downloaded containing an empty JSON array '[]'.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Unauthorized user attempts to export the audit log

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am logged in as a user with the 'Viewer' role

### 3.5.5 When

I attempt to access the audit log export functionality via the UI or a direct API call

### 3.5.6 Then

The system must prevent the action and I should receive a 'Forbidden' (403) error response.

### 3.5.7 Validation Notes

The UI should not display the export button or options to non-Administrator roles. Direct API calls to the export endpoint must be rejected.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- An 'Export' button on the Audit Log view.
- A modal or panel that appears upon clicking 'Export'.
- Date picker controls for 'Start Date' and 'End Date'.
- Radio buttons or a dropdown to select the format ('CSV', 'JSON').
- A 'Download' or 'Generate Export' button within the modal/panel to trigger the process.
- A loading indicator to provide feedback while the file is being generated.

## 4.2.0 User Interactions

- The user clicks 'Export' to open the options panel.
- The user can optionally select a start and end date. Dates should be validated (end date cannot be before start date).
- The user must select a format.
- The user clicks 'Download' to start the file generation and download.

## 4.3.0 Display Requirements

- The export options should be clearly labeled.
- Any validation errors (e.g., invalid date range) should be displayed to the user.

## 4.4.0 Accessibility Needs

- All UI elements (buttons, date pickers, radio buttons) must be keyboard accessible and have appropriate ARIA labels.
- The modal must properly trap focus.
- Compliant with WCAG 2.1 Level AA standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Only users with the 'Administrator' role can export the audit log.

### 5.1.3 Enforcement Point

API endpoint and UI rendering.

### 5.1.4 Violation Handling

The UI element will be hidden. The API will return a 403 Forbidden status code.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The exported audit log data must be a direct, unaltered representation of the data stored in the database.

### 5.2.3 Enforcement Point

Backend data retrieval and serialization logic.

### 5.2.4 Violation Handling

This is a core data integrity requirement; failure would be a high-severity bug.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

- {'story_id': 'US-101', 'dependency_reason': 'This story adds the export functionality to the audit log viewing interface, which must exist first.'}

## 6.2.0 Technical Dependencies

- A functioning audit logging mechanism that populates the 'AuditLog' table as defined in SRS section 6.4.
- ASP.NET Core Identity for role-based access control.
- A backend library for efficient CSV serialization (e.g., CsvHelper).
- Frontend state management (Zustand) to handle the state of the export modal.

## 6.3.0 Data Dependencies

- Requires access to the 'AuditLog' table in the SQLite database.

## 6.4.0 External Dependencies

*No items available*

# 7.0.0 Non Functional Requirements

## 7.1.0 Performance

- The export of 100,000 log entries must complete and the download must begin within 30 seconds on recommended hardware.
- The implementation must use data streaming to avoid loading the entire result set into memory, keeping memory consumption low regardless of export size.

## 7.2.0 Security

- Access to the export endpoint must be restricted to authenticated users with the 'Administrator' role.
- The file download must be served over HTTPS.
- The database query must be parameterized to prevent SQL injection.

## 7.3.0 Usability

- The process of selecting a date range and format should be intuitive.
- The downloaded filename should be predictable and include the date range for easy identification.

## 7.4.0 Accessibility

- The feature must adhere to WCAG 2.1 Level AA standards.

## 7.5.0 Compatibility

- The feature must work correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge.

# 8.0.0 Implementation Considerations

## 8.1.0 Complexity Assessment

Medium

## 8.2.0 Complexity Factors

- Implementing a performant, streaming-based file generation on the backend to handle potentially very large audit logs without high memory usage or timeouts.
- Ensuring the database query on the 'AuditLog' table is efficient, which may require adding an index to the timestamp column.
- Frontend implementation of a clean, accessible modal with date pickers and state management.

## 8.3.0 Technical Risks

- Poor performance or high memory consumption if a non-streaming approach is used for large exports.
- Potential for slow database queries if the timestamp column in the 'AuditLog' table is not indexed.

## 8.4.0 Integration Points

- Frontend: Integrates with the Audit Log viewing page component.
- Backend: A new API endpoint (e.g., GET /api/v1/auditlog/export) that queries the SQLite database via Entity Framework Core.

# 9.0.0 Testing Requirements

## 9.1.0 Testing Types

- Unit
- Integration
- E2E
- Performance
- Security

## 9.2.0 Test Scenarios

- Verify CSV and JSON export for a specific date range.
- Verify export for the entire log.
- Verify export for an empty date range.
- Verify role-based access control prevents non-admins from exporting.
- Test performance with a large dataset (e.g., >100,000 rows) to validate streaming implementation.

## 9.3.0 Test Data Needs

- A seeded database with a significant number of audit log entries spanning several days/months.
- User accounts with 'Administrator' and 'Viewer' roles.

## 9.4.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- An E2E testing framework like Playwright or Cypress.
- A scripting tool to populate the database for performance testing.

# 10.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit tests implemented for backend and frontend logic, achieving >80% coverage for new code
- Integration tests for the API endpoint are implemented and passing
- E2E tests simulating the user flow are implemented and passing
- Performance testing confirms the system meets the specified performance NFRs
- Security review confirms RBAC is correctly enforced
- User documentation (User Guide) is updated to explain the export feature
- Story deployed and verified in the staging environment

# 11.0.0 Planning Information

## 11.1.0 Story Points

5

## 11.2.0 Priority

🔴 High

## 11.3.0 Sprint Considerations

- This story is dependent on US-101 and should be scheduled in a sprint after its completion.
- The performance requirement necessitates a specific implementation approach (streaming), which should be accounted for during task breakdown.

## 11.4.0 Release Impact

This is a critical feature for enterprise customers and those in regulated industries. Its inclusion is important for security and compliance-focused releases.

