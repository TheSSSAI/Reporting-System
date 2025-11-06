# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-103 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Trigger an on-demand backup of the configuration d... |
| As A User Story | As an Administrator, I want to trigger an on-deman... |
| User Persona | Administrator: A privileged user responsible for s... |
| Business Value | Provides a critical data protection mechanism, mit... |
| Functional Area | System Administration & Maintenance |
| Story Theme | System Reliability and Disaster Recovery |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful on-demand backup by an Administrator

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an Administrator logged into the Control Panel and have navigated to the 'System Settings' page

### 3.1.5 When

I click the 'Backup Configuration' button, and in the browser's file save dialog, I select a valid location and confirm the save

### 3.1.6 Then

The system initiates a file download of the configuration database, a success notification 'Configuration backup completed successfully.' is displayed in the UI, and the downloaded file is a valid, byte-for-byte copy of the server's SQLite database.

### 3.1.7 Validation Notes

Verify by checking the file size of the downloaded backup against the source file on the server. The file name should default to a format like 'system_backup_YYYY-MM-DD_HH-MM-SS.db'.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Unauthorized access attempt by a non-Administrator

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

I am a user with the 'Viewer' role and am logged into the system

### 3.2.5 When

I navigate to the 'System Settings' page or any other part of the Control Panel

### 3.2.6 Then

The 'Backup Configuration' button is not visible or accessible to me.

### 3.2.7 Validation Notes

Additionally, test by directly calling the backup API endpoint (e.g., GET /api/v1/system/backup) as the 'Viewer' user and verify that the response is an HTTP 403 Forbidden status.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User cancels the backup operation

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

I am an Administrator on the 'System Settings' page

### 3.3.5 When

I click the 'Backup Configuration' button and then click 'Cancel' in the browser's file save dialog

### 3.3.6 Then

The backup process is aborted gracefully, no file is downloaded, and no success or error message is displayed.

### 3.3.7 Validation Notes

Verify that no network request is made to the server if the dialog is cancelled before a file path is confirmed, or that the download is simply aborted by the browser without UI feedback from the application.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Server fails to access the database file for backup

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am an Administrator on the 'System Settings' page

### 3.4.5 And

the server's SQLite database file is inaccessible due to a file lock or permissions issue

### 3.4.6 When

I click the 'Backup Configuration' button

### 3.4.7 Then

the file download does not start, and a user-friendly error notification is displayed, such as 'Backup failed: Unable to access the configuration database. Please try again or contact support.'

### 3.4.8 Validation Notes

Simulate a file lock on the SQLite database on the server. The API should return an HTTP 500 Internal Server Error with a structured error message that the frontend can display.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A 'Backup Configuration' button located within a 'Maintenance' or 'Backup & Restore' section of the 'System Settings' page.
- A success notification (toast) component to display the success message.
- An error notification (toast) component to display failure messages.

## 4.2.0 User Interactions

- Clicking the button triggers the browser's native file save dialog.
- The system should not navigate away from the current page after the backup is initiated.

## 4.3.0 Display Requirements

- The default filename in the save dialog must include a timestamp to prevent accidental overwrites and provide version context.
- Feedback messages (success/error) must be clear and concise.

## 4.4.0 Accessibility Needs

- The 'Backup Configuration' button must be keyboard accessible (focusable and activatable via Enter/Space).
- The button must have a descriptive `aria-label`.
- Notification messages must be announced by screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Only users with the 'Administrator' role can perform a system configuration backup.

### 5.1.3 Enforcement Point

Both Frontend (UI visibility) and Backend (API endpoint authorization).

### 5.1.4 Violation Handling

UI element is hidden. API requests from unauthorized users are rejected with an HTTP 403 Forbidden status.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The backup file must be a complete and exact copy of the encrypted SQLite database file.

### 5.2.3 Enforcement Point

Backend file I/O process.

### 5.2.4 Violation Handling

If the file cannot be read completely or is corrupted during the read, the operation must fail and return a server error.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-019

#### 6.1.1.2 Dependency Reason

Requires the existence of a distinct 'Administrator' role for access control.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-027

#### 6.1.2.2 Dependency Reason

Requires a functioning user authentication system to identify the user's role.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity for role-based authorization.
- SQLite database file location must be known to the backend service.
- Frontend notification/toast component for user feedback.

## 6.3.0.0 Data Dependencies

- Requires the existence of the SQLite configuration database file on the server's file system.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API response to initiate the file download should be within 500ms, as it involves reading a relatively small file from local disk.

## 7.2.0.0 Security

- The API endpoint for backups must be protected against Cross-Site Request Forgery (CSRF).
- The endpoint must enforce role-based access control, restricting access to Administrators only.
- The generated backup file will be encrypted at rest, inheriting the encryption of the source database (DPAPI as per SRS 3.3). Documentation must advise users to store the backup file in a secure location.

## 7.3.0.0 Usability

- The feature should be easily discoverable by an Administrator within the system's administrative settings.
- The process should require minimal steps: navigate, click, save.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The file download mechanism must be compatible with all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Backend implementation is a straightforward file-serving endpoint.
- Frontend implementation involves adding a button and handling a file download link.
- No complex state management is required.

## 8.3.0.0 Technical Risks

- Potential for file locking issues on the SQLite database if a backup is attempted during a write-intensive operation. The read operation should be resilient to this.
- Ensuring consistent and user-friendly error feedback for file downloads can be tricky across different browsers.

## 8.4.0.0 Integration Points

- Integrates with the existing ASP.NET Core authentication and authorization middleware.
- Interacts directly with the server's file system to read the database file.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify successful backup as Administrator.
- Verify UI element is hidden for 'Viewer' role.
- Verify API endpoint returns 403 for 'Viewer' role.
- Verify server-side file read error is handled gracefully and reported to the user.
- Verify the integrity of the downloaded backup file by comparing it with the source or using it in a restore test (related to US-104).

## 9.3.0.0 Test Data Needs

- User accounts with 'Administrator' and 'Viewer' roles.
- A populated configuration database on the server to ensure the backup is not an empty file.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- Postman or similar tool for direct API security testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit tests for the new API endpoint and frontend component implemented, achieving >80% code coverage
- An integration test for the API endpoint is created and passing
- Manual end-to-end testing of the backup workflow is completed and signed off by QA
- Security testing confirms that non-Admins cannot access the functionality
- The Installation and Administration Guide is updated with instructions for this feature
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

2

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a prerequisite for US-104 (Restore Configuration). It is highly recommended to plan both stories in the same or consecutive sprints to deliver the complete backup/restore feature set.
- The work is self-contained and has no external team dependencies.

## 11.4.0.0 Release Impact

Adds a critical system reliability and maintenance feature. This should be included in the next available release.

