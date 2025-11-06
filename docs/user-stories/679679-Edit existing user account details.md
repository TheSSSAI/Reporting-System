# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-020 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Edit existing user account details |
| As A User Story | As an Administrator, I want to edit the details of... |
| User Persona | Administrator: A user with full system privileges ... |
| Business Value | Ensures data accuracy for user management, maintai... |
| Functional Area | User Management |
| Story Theme | System Administration |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Navigate to and load the user edit form

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an Administrator logged into the Control Panel and am viewing the user list

### 3.1.5 When

I click the 'Edit' action for a specific user

### 3.1.6 Then

I am navigated to the user edit page, and a form is displayed pre-populated with the user's current Full Name, Email, Role, and Active Status. The username is also displayed but is in a read-only field.

### 3.1.7 Validation Notes

Verify the API call to fetch user details is successful and the form fields are correctly populated. The username field must be disabled.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Successfully update user details

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am on the user edit form for a specific user

### 3.2.5 When

I modify the Full Name, Email, Role, or Active Status and click 'Save'

### 3.2.6 Then

the system validates the input, saves the changes to the database, redirects me back to the user list, and displays a success notification: 'User details updated successfully.'

### 3.2.7 Validation Notes

Check the database to confirm the user record is updated. Verify the success notification appears and the user is redirected.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Verify updated details are reflected in the system

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I have successfully updated a user's details

### 3.3.5 When

I view the user list or navigate back to the edit page for that user

### 3.3.6 Then

the newly updated information is correctly displayed.

### 3.3.7 Validation Notes

This confirms the data persistence and retrieval logic is working correctly.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Attempt to save with an invalid email format

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am on the user edit form

### 3.4.5 When

I enter an email address with an invalid format (e.g., 'user@domain') and click 'Save'

### 3.4.6 Then

the save operation is prevented, and a validation error message 'Please enter a valid email address.' is displayed next to the email field.

### 3.4.7 Validation Notes

Test with multiple invalid formats. The form should retain the invalid data entered by the user for correction.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Attempt to save with a duplicate email address

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

User 'A' has the email 'user.a@example.com' and User 'B' has the email 'user.b@example.com'

### 3.5.5 When

I edit User 'B' and change their email to 'user.a@example.com' and click 'Save'

### 3.5.6 Then

the save operation is prevented, and a clear error message 'This email address is already in use by another account.' is displayed.

### 3.5.7 Validation Notes

The uniqueness check must exclude the user's own current email address.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Attempt to save with empty required fields

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

I am on the user edit form

### 3.6.5 When

I clear the content of a required field (e.g., Full Name or Email) and click 'Save'

### 3.6.6 Then

the save operation is prevented, and a validation error message 'This field is required.' is displayed next to the empty field.

### 3.6.7 Validation Notes

Verify this for all mandatory fields on the form.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Attempt to change the role of the primary administrator

### 3.7.3 Scenario Type

Edge_Case

### 3.7.4 Given

I am editing the primary administrator account

### 3.7.5 When

I attempt to change the role from 'Administrator' to 'Viewer' and click 'Save'

### 3.7.6 Then

the save operation is prevented, and an error message 'The primary administrator's role cannot be changed.' is displayed.

### 3.7.7 Validation Notes

This rule is critical to prevent system lockout. The primary admin is defined as the one created during installation.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Attempt to disable one's own administrator account

### 3.8.3 Scenario Type

Edge_Case

### 3.8.4 Given

I am an Administrator editing my own user account

### 3.8.5 When

I change my 'Active Status' to 'Inactive' and click 'Save'

### 3.8.6 Then

the save operation is prevented, and an error message 'You cannot disable your own account.' is displayed.

### 3.8.7 Validation Notes

This prevents an administrator from accidentally locking themselves out of their active session.

## 3.9.0 Criteria Id

### 3.9.1 Criteria Id

AC-009

### 3.9.2 Scenario

Cancel the edit operation

### 3.9.3 Scenario Type

Alternative_Flow

### 3.9.4 Given

I am on the user edit form and have made unsaved changes

### 3.9.5 When

I click the 'Cancel' button

### 3.9.6 Then

I am redirected back to the user list, no changes are saved, and no notification is shown.

### 3.9.7 Validation Notes

Verify that the user's data in the database remains unchanged.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A user list with an 'Edit' button/icon per user row.
- An edit form with input fields for 'Full Name' (text), 'Email' (email), and 'Username' (read-only text).
- A dropdown/select component for 'Role', populated with system roles (Administrator, Viewer).
- A toggle switch or radio buttons for 'Active Status' (Active/Inactive).
- A 'Save' button, which should be disabled until a change is made to the form.
- A 'Cancel' button to discard changes and return to the user list.
- Inline validation message areas next to each field.
- A global notification component (toast/snackbar) for success or failure messages.

## 4.2.0 User Interactions

- Clicking 'Edit' on the user list navigates to the edit form.
- Modifying any form field enables the 'Save' button.
- Clicking 'Save' triggers validation and an API call.
- Clicking 'Cancel' discards changes and navigates back.

## 4.3.0 Display Requirements

- The form must be clearly titled, e.g., 'Edit User: [Username]'.
- Current user data must be accurately pre-populated.
- Validation errors must be displayed in a contrasting color (e.g., red) and be easy to understand.

## 4.4.0 Accessibility Needs

- The form must be fully navigable using a keyboard.
- All form inputs must have corresponding `<label>` tags.
- Validation error messages must be programmatically linked to their respective inputs using `aria-describedby` to be accessible to screen readers.
- The UI must adhere to WCAG 2.1 Level AA standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A user's username cannot be changed after creation.

### 5.1.3 Enforcement Point

User Interface and API Backend

### 5.1.4 Violation Handling

The username field is rendered as read-only in the UI. The backend API should ignore any username value in the request payload.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A user's email address must be unique across all accounts.

### 5.2.3 Enforcement Point

API Backend (on save)

### 5.2.4 Violation Handling

If a submitted email already exists for another user, the API returns an HTTP 409 Conflict error with a descriptive message.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

The primary administrator account's role cannot be changed from 'Administrator'.

### 5.3.3 Enforcement Point

API Backend (on save)

### 5.3.4 Violation Handling

The API returns an HTTP 400 Bad Request error if an attempt is made to change the primary admin's role.

## 5.4.0 Rule Id

### 5.4.1 Rule Id

BR-004

### 5.4.2 Rule Description

An administrator cannot disable their own account.

### 5.4.3 Enforcement Point

API Backend (on save)

### 5.4.4 Violation Handling

The API returns an HTTP 400 Bad Request error if an admin attempts to set their own account status to inactive.

## 5.5.0 Rule Id

### 5.5.1 Rule Id

BR-005

### 5.5.2 Rule Description

All changes to a user account must be recorded in the audit log.

### 5.5.3 Enforcement Point

API Backend (after successful save)

### 5.5.4 Violation Handling

The save operation should be transactional. If audit logging fails, the user data update should be rolled back.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-018

#### 6.1.1.2 Dependency Reason

Defines the user entity and the creation process. This story modifies the data created by US-018.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-019

#### 6.1.2.2 Dependency Reason

Establishes the concept of roles. The edit form needs the list of available roles to populate the role selection dropdown.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-101

#### 6.1.3.2 Dependency Reason

Defines the audit logging mechanism. This story must generate an audit log entry upon successful update.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity for user management.
- Entity Framework Core for database access.
- A UI component for listing users must exist to provide the entry point for editing.
- The global notification/toast system for displaying feedback.

## 6.3.0.0 Data Dependencies

- Requires access to the `Users` and `Roles` tables in the SQLite database.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The user edit form should load within 1 second.
- The save operation (API call and database update) should complete within 500ms under normal load.

## 7.2.0.0 Security

- The API endpoint for editing users (`PUT /api/v1/users/{id}`) must be protected and require 'Administrator' role.
- All user data changes must be recorded in the audit log, including the administrator who made the change, timestamp, and a before/after snapshot of the changed fields.
- Input validation must be performed on both the client-side and server-side to prevent injection attacks and invalid data.

## 7.3.0.0 Usability

- The form should be intuitive and provide clear feedback on actions (e.g., success, failure, validation errors).
- The 'Save' button should be disabled by default and only enabled when data has been changed to prevent accidental empty submissions.

## 7.4.0.0 Accessibility

- The user interface must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The user interface must function correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires careful implementation of business logic for validation (email uniqueness, primary admin rules, self-disable rule).
- Requires integration with the audit logging system.
- Involves both frontend (React form with state management) and backend (secure API endpoint with business logic) development.

## 8.3.0.0 Technical Risks

- The logic to identify the 'primary administrator' must be robust and unambiguous.
- The transactionality of the save operation (data update + audit log) must be handled correctly to prevent data inconsistency.

## 8.4.0.0 Integration Points

- Backend: ASP.NET Core Identity, EF Core, Serilog (for audit logging).
- Frontend: React Router for navigation, Zustand for state management, API client (e.g., Axios) for communication.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify successful update of each editable field individually.
- Verify successful update of all editable fields at once.
- Test all validation error scenarios (invalid email, duplicate email, required fields).
- Test all edge cases (editing primary admin, disabling own account).
- Verify the 'Cancel' button functionality.
- Verify that an unauthorized user (e.g., 'Viewer') receives a 403 Forbidden error when attempting to access the edit API endpoint.

## 9.3.0.0 Test Data Needs

- At least three user accounts: a primary administrator, a secondary administrator, and a viewer.
- Test data should be configured to test the duplicate email scenario.

## 9.4.0.0 Testing Tools

- Backend: xUnit, Moq.
- Frontend: Jest, React Testing Library.
- E2E: Playwright or Cypress.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing.
- Code reviewed and approved by at least one other developer.
- Unit tests implemented for both frontend and backend logic, achieving >= 80% coverage.
- Integration tests for the API endpoint are implemented and passing.
- End-to-end tests covering the happy path and key error conditions are passing.
- User interface has been reviewed and approved by the product owner/designer.
- Security requirements (RBAC, audit logging) have been implemented and verified.
- Accessibility checks (automated and manual) have been completed.
- API documentation (Swagger/OpenAPI) for the endpoint is updated.
- Story has been deployed and verified in the staging environment.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- Ensure prerequisite stories (US-018, US-019, US-101) are completed before starting this story.
- Requires a developer with both frontend and backend skills or a pair of developers.

## 11.4.0.0 Release Impact

This is a core feature for user administration and is essential for the initial release.

