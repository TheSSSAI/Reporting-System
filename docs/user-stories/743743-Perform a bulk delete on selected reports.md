# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-084 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Perform a bulk delete on selected reports |
| As A User Story | As an End-User, I want to select and delete multip... |
| User Persona | End-User (Viewer) or Administrator interacting wit... |
| Business Value | Improves user efficiency by allowing for quick cle... |
| Functional Area | Report Viewer |
| Story Theme | Report Management & Accessibility |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: User successfully deletes multiple selected reports

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am logged in as a user with permission to delete reports and I am on the Report Viewer page where at least three reports are listed

### 3.1.5 When

I select the checkboxes for three reports, click the 'Delete Selected' button, and click 'Confirm' in the confirmation dialog

### 3.1.6 Then

A success notification appears stating '3 reports successfully deleted', the three selected reports are removed from the list, and their corresponding files are deleted from storage.

### 3.1.7 Validation Notes

Verify the UI list refreshes and the reports are gone. Check the backend storage (local disk, S3, etc.) to confirm the physical files have been removed. Check the database to confirm the metadata records are deleted.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

UI State: 'Delete Selected' button is enabled only when reports are selected

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am on the Report Viewer page

### 3.2.5 When

I select one or more reports using their checkboxes

### 3.2.6 Then

The 'Delete Selected' button becomes enabled.

### 3.2.7 Validation Notes

Observe the button's 'disabled' attribute or class in the browser's developer tools based on selection state.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Alternative Flow: User cancels the delete operation

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

I am on the Report Viewer page and have selected two reports

### 3.3.5 When

I click the 'Delete Selected' button and then click 'Cancel' in the confirmation dialog

### 3.3.6 Then

The confirmation dialog closes, no reports are deleted, and the two reports remain selected in the list.

### 3.3.7 Validation Notes

Verify that no API call for deletion was made and that the UI state remains unchanged, with the reports still selected.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Error Condition: User attempts to delete reports without sufficient permissions

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am logged in as a user and have selected two reports, one of which I do not have permission to delete

### 3.4.5 When

I click 'Delete Selected' and confirm the action

### 3.4.6 Then

An error notification appears stating 'You do not have permission to delete one or more of the selected reports.' and no reports are deleted.

### 3.4.7 Validation Notes

The backend API should return a 403 Forbidden status. The entire operation must be atomic; if one permission check fails, nothing should be deleted.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Edge Case: Deletion fails for some reports (e.g., file missing)

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

I am an Administrator and have selected two reports to delete, but one report's physical file is missing from storage

### 3.5.5 When

I confirm the deletion of both reports

### 3.5.6 Then

A warning notification appears with a message like '1 report deleted successfully. 1 report record removed, but its file was not found.' and both reports are removed from the UI list.

### 3.5.7 Validation Notes

Verify that both metadata records are deleted from the database, and a warning is logged on the backend for the missing file.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

UI Feedback: System provides clear feedback during and after deletion

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

I have selected several reports and confirmed the deletion

### 3.6.5 When

The operation completes

### 3.6.6 Then

The loading indicator disappears and a final success or error notification is shown.

### 3.6.7 Validation Notes

Visually confirm the loading state and the appearance of a toast/snackbar notification.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A 'Delete Selected' button, likely positioned near the top of the report list.
- A confirmation modal/dialog with a clear warning message, a 'Confirm' button, and a 'Cancel' button.
- Toast/Snackbar notifications for success, warning, and error feedback.
- A loading indicator (e.g., spinner) to show when the operation is in progress.

## 4.2.0 User Interactions

- The 'Delete Selected' button is disabled by default and becomes enabled when one or more reports are selected via checkboxes.
- Clicking 'Delete Selected' opens the confirmation modal.
- Clicking 'Confirm' in the modal initiates the delete API call.
- Clicking 'Cancel' closes the modal with no action.
- The report list should automatically refresh upon successful deletion.

## 4.3.0 Display Requirements

- The confirmation dialog must display the number of reports selected for deletion (e.g., 'Are you sure you want to permanently delete the 5 selected reports?').
- Feedback messages must be clear and concise.

## 4.4.0 Accessibility Needs

- The 'Delete Selected' button must have an accessible name and its disabled/enabled state must be conveyed to screen readers.
- The confirmation modal must trap focus and be dismissible with the Escape key.
- All interactive elements (buttons, checkboxes) must be keyboard navigable and operable.
- Notifications must be announced by screen readers using ARIA live regions.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A user can only delete reports for which they have explicit delete permissions.

### 5.1.3 Enforcement Point

Backend API endpoint before processing the delete request.

### 5.1.4 Violation Handling

The API rejects the entire request with a 403 Forbidden status code if any selected report fails the permission check. The operation must be atomic.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Deleting a report is a permanent action that removes both the metadata record and the physical report file.

### 5.2.3 Enforcement Point

Backend deletion service.

### 5.2.4 Violation Handling

A confirmation dialog must be shown to the user before the action is executed to prevent accidental data loss.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

The deletion of a report by an Administrator must be recorded in the system's audit log.

### 5.3.3 Enforcement Point

Backend deletion service, after successful deletion.

### 5.3.4 Violation Handling

N/A - This is a logging requirement.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-078

#### 6.1.1.2 Dependency Reason

Requires the Report Viewer list to be implemented to display reports.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-083

#### 6.1.2.2 Dependency Reason

Requires the checkbox selection mechanism for selecting multiple reports.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-086

#### 6.1.3.2 Dependency Reason

Requires the role-based permission system to be in place to check if a user can delete a specific report.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint (e.g., `DELETE /api/v1/generated-reports`) that can accept an array of report IDs.
- An abstraction layer for file storage operations to handle deletion from different providers (local disk, S3, Azure Blob).
- ASP.NET Core Identity for user authentication and role checking.
- The `AuditLog` entity and service for recording the action.

## 6.3.0.0 Data Dependencies

- Requires access to the `JobExecutionLog` (or equivalent) table that stores metadata about generated reports, including their storage path.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API response for deleting up to 100 reports should be within 5 seconds under normal load.
- The UI must remain responsive during the API call, displaying a loading indicator without freezing.

## 7.2.0.0 Security

- The API endpoint must be protected and require authentication.
- The backend must perform a permission check for every single report ID in the request payload against the authenticated user's permissions.
- The operation must be atomic: if any permission check fails, the entire batch deletion must be aborted.

## 7.3.0.0 Usability

- The process must include a confirmation step to prevent accidental deletion.
- The system must provide clear, immediate feedback on the outcome of the operation.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires both frontend and backend development.
- Backend logic must be transactional to ensure data integrity (all-or-nothing based on permissions).
- Interaction with the file system or cloud storage adds complexity compared to a simple database operation.
- Requires robust error handling for partial failures (e.g., file not found, network issues to storage).

## 8.3.0.0 Technical Risks

- Potential for orphaned data if a file is deleted but the database record fails to be removed (or vice-versa). This must be handled with careful ordering of operations and logging.
- Performance degradation if deleting a very large number of files (e.g., thousands) in a single request. May need to consider batching on the backend if this is a likely scenario.

## 8.4.0.0 Integration Points

- Frontend (React) calls to the Backend (ASP.NET Core) API.
- Backend service integrates with the database (SQLite via EF Core) and the file storage system.
- Backend service integrates with the Audit Logging service.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify successful deletion of single and multiple reports.
- Verify the cancel deletion flow.
- Verify the UI button states (enabled/disabled).
- Test the permission denied scenario by attempting to delete a report as a user without sufficient rights.
- Test the edge case where a report file is missing from storage.
- Verify that an audit log is created when an Administrator deletes a report.

## 9.3.0.0 Test Data Needs

- User accounts with different roles (Administrator, Viewer).
- Generated reports with permissions assigned to different roles.
- A test case where a report's database record exists but its physical file does not.

## 9.4.0.0 Testing Tools

- Frontend: Jest, React Testing Library.
- Backend: xUnit, Moq.
- E2E: A framework like Playwright or Cypress.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing.
- Code reviewed and approved by team.
- Unit tests implemented for both frontend and backend logic, achieving >80% coverage.
- Integration testing for the API endpoint and service layer completed successfully.
- E2E test scenario for bulk deletion is created and passing.
- User interface reviewed and approved for usability and accessibility (WCAG 2.1 AA).
- Performance requirements for deleting 100 reports are verified.
- Security requirements, especially permission checks, are validated via testing.
- Audit logging for the delete action is confirmed.
- Story deployed and verified in the staging environment.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🟡 Medium

## 11.3.0.0 Sprint Considerations

- Requires a clear API contract between frontend and backend to be defined early in the sprint.
- Dependent stories (US-083, US-086) must be completed before this story can be finalized and tested.

## 11.4.0.0 Release Impact

Provides a significant usability improvement for the Report Viewer. It is a key feature for long-term management of the system.

