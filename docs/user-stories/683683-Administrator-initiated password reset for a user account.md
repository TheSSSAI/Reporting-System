# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-024 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Administrator-initiated password reset for a user ... |
| As A User Story | As an Administrator, I want to initiate a password... |
| User Persona | Administrator |
| Business Value | Enables efficient user support by allowing adminis... |
| Functional Area | User Management |
| Story Theme | System Administration & Security |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful password reset by an Administrator

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an Administrator logged into the Control Panel and am on the User Management page

### 3.1.5 When

I select a user and trigger the 'Reset Password' action, and then confirm the action in a confirmation dialog

### 3.1.6 Then

a secure, temporary password that complies with the system's password policy is generated, the system displays this temporary password in a dialog, the user's account is flagged to require a password change on their next login, a success notification is shown, and an audit log event is created for this action.

### 3.1.7 Validation Notes

Verify the UI flow, the displayed temporary password, the user's status flag in the database, and the creation of a corresponding audit log entry.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Administrator cancels the password reset action

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

I am an Administrator and have opened the password reset confirmation dialog for a user

### 3.2.5 When

I click the 'Cancel' button or close the dialog without confirming

### 3.2.6 Then

the dialog closes, and no changes are made to the user's password or account status.

### 3.2.7 Validation Notes

Confirm that the user's password hash and account flags in the database remain unchanged.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User logs in with the temporary password

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

an Administrator has reset a user's password and provided them with the temporary password

### 3.3.5 When

the user attempts to log in using their username and the temporary password

### 3.3.6 Then

they are successfully authenticated but are immediately forced to a screen where they must set a new password before they can access any other part of the application.

### 3.3.7 Validation Notes

This validates the dependency on US-030. The test requires logging in as the affected user and verifying the forced password change workflow.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

System error during password reset process

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am an Administrator attempting to reset a user's password

### 3.4.5 When

a backend or database error occurs during the process

### 3.4.6 Then

a user-friendly error message is displayed (e.g., 'Failed to reset password. Please try again.'), and the user's account remains unchanged.

### 3.4.7 Validation Notes

Simulate a database connection failure or API error and verify that the UI handles the error gracefully and no data is corrupted.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Audit log records the password reset event

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

an Administrator named 'AdminUser' is logged in

### 3.5.5 When

they successfully reset the password for a user named 'TargetUser'

### 3.5.6 Then

a new entry is created in the audit log that includes the timestamp, the responsible user ('AdminUser'), the source IP address, the action ('User Password Reset'), the target ('TargetUser'), and the outcome ('Success').

### 3.5.7 Validation Notes

Query the audit log table or use the audit log viewer (US-101) to confirm the event was recorded with the correct details.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A 'Reset Password' option in an action menu or as a button for each user in the user list.
- A confirmation modal with the text 'Are you sure you want to reset the password for [Username]?' and 'Confirm'/'Cancel' buttons.
- A result modal to display the generated temporary password, with a 'Copy to Clipboard' button and a clear warning that the password will not be shown again.
- Success and error notification toasts.

## 4.2.0 User Interactions

- Administrator clicks the reset password action.
- Administrator confirms or cancels the action in the modal.
- Administrator copies the temporary password from the result modal and closes it.

## 4.3.0 Display Requirements

- The username of the targeted user must be displayed in the confirmation modal to prevent errors.
- The temporary password must be clearly visible in the result modal.

## 4.4.0 Accessibility Needs

- All modals, buttons, and notifications must be keyboard accessible and compliant with WCAG 2.1 Level AA standards.
- The temporary password should be readable by screen readers when the result modal is focused.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A user account that has had its password reset by an administrator must be forced to change their password upon their next successful login.

### 5.1.3 Enforcement Point

During the login process.

### 5.1.4 Violation Handling

Access to the application is denied until a new password is set.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The generated temporary password must conform to the currently configured system password policy.

### 5.2.3 Enforcement Point

During password generation.

### 5.2.4 Violation Handling

The generation process fails, and an error is returned to the administrator.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-018

#### 6.1.1.2 Dependency Reason

A user account must exist to have its password reset. The user management UI is built as part of user CRUD.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-023

#### 6.1.2.2 Dependency Reason

The system's password policy must be configurable, as the generated temporary password must adhere to it.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-030

#### 6.1.3.2 Dependency Reason

This story is critically dependent on the functionality to force a user to change their password after a reset. The reset action sets the flag that US-030 acts upon.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-101

#### 6.1.4.2 Dependency Reason

The password reset action is a security-sensitive event that must be logged in the system's audit trail.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity for user management and password generation.
- React Control Panel UI for the user management interface.
- Audit logging service/module.

## 6.3.0.0 Data Dependencies

- Requires access to the User entity in the SQLite database to update the password hash and the 'MustChangePassword' flag.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The password reset operation (API call and database update) should complete in under 500ms.

## 7.2.0.0 Security

- The generated temporary password must be created using a cryptographically secure pseudo-random number generator (CSPRNG).
- The temporary password must not be stored in plaintext anywhere in the system after being displayed to the administrator.
- The API endpoint for resetting passwords must be protected and only accessible to users with the 'Administrator' role.
- The action must be logged in a tamper-evident audit log as per SRS 6.4.

## 7.3.0.0 Usability

- The process should be intuitive, with clear confirmation steps to prevent accidental resets.
- Providing a 'Copy to Clipboard' button for the temporary password is required for ease of use.

## 7.4.0.0 Accessibility

- The entire workflow must be compliant with WCAG 2.1 Level AA.

## 7.5.0.0 Compatibility

- The feature must function correctly on all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires coordinated changes in both the frontend (React UI) and backend (ASP.NET Core API).
- Tight integration with the login workflow (US-030) and password policy (US-023).
- Security considerations for generating and handling the temporary password.
- Requires adding a new field (e.g., 'MustChangePassword' boolean) to the User entity and managing the corresponding database migration.

## 8.3.0.0 Technical Risks

- Ensuring the temporary password is truly single-use and invalidated correctly after the user sets a new one.
- Potential for race conditions if the user attempts to log in while the admin is resetting their password (low risk, but should be considered).

## 8.4.0.0 Integration Points

- Backend API: A new endpoint like `POST /api/v1/users/{userId}/reset-password`.
- Database: The `Users` table will need a schema update.
- Frontend UI: The user management component in the React Control Panel.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify successful password reset and display of temporary password.
- Verify cancellation of the reset action leaves the account unchanged.
- Full E2E test: Admin resets password -> User logs in with temp password -> User is forced to change password -> User logs in with new password successfully.
- Verify an audit log is created with correct details.
- Test API endpoint security to ensure non-admin users receive a 403 Forbidden error.

## 9.3.0.0 Test Data Needs

- At least two user accounts: one with an 'Administrator' role and one with a 'Viewer' role (the target).
- A configured password policy to test compliance of the generated password.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- A dedicated E2E testing framework (e.g., Playwright or Cypress) to automate the full user journey.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit tests for both frontend and backend implemented with >= 80% coverage
- Integration testing for the API endpoint and database interaction completed successfully
- E2E test scenario for the full workflow is implemented and passing
- User interface changes reviewed and approved for usability and accessibility (WCAG 2.1 AA)
- Security requirements, including audit logging and role-based access, are validated
- Documentation for the user management section is updated to include this feature
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story has a hard dependency on US-030. They should be developed in close succession, ideally within the same sprint, to allow for E2E testing.
- Requires a database migration to add the 'MustChangePassword' flag to the user model.

## 11.4.0.0 Release Impact

This is a core administrative feature required for the initial release (MVP) to support basic user management and security operations.

