# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-030 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Be forced to change my password on first login aft... |
| As A User Story | As a User whose password has been reset by an admi... |
| User Persona | Any system user (Administrator or Viewer) whose ac... |
| Business Value | Enhances account security by eliminating the risk ... |
| Functional Area | User Management & Security |
| Story Theme | Authentication & Access Control |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful forced password change on first login

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

An administrator has reset my password and my account is flagged as 'password change required'

### 3.1.5 When

I log in for the first time with the temporary password

### 3.1.6 Then

I am immediately redirected to a dedicated 'Change Password' page and cannot access any other part of the application.

### 3.1.7 And

My password is successfully updated, the 'password change required' flag is removed from my account, and I am redirected to my default landing page (Dashboard or Report Viewer).

### 3.1.8 Validation Notes

Verify by logging out and logging back in with the new password. The system should grant access directly to the dashboard without forcing another password change.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Attempt to change password with non-matching new passwords

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

I am on the 'Change Password' page after a forced redirect

### 3.2.5 When

I enter a new password and a confirmation password that do not match

### 3.2.6 Then

The system displays an inline error message stating 'Passwords do not match' and does not change my password.

### 3.2.7 And

I remain on the 'Change Password' page.

### 3.2.8 Validation Notes

The form submission should fail, and the error message should be clearly visible and associated with the confirmation password field.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempt to change password to one that violates the password policy

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am on the 'Change Password' page after a forced redirect

### 3.3.5 When

I enter a new password that does not meet the configured password policy (e.g., too short, lacks complexity)

### 3.3.6 Then

The system displays a specific error message detailing which policy rule was violated (e.g., 'Password must be at least 12 characters long').

### 3.3.7 And

My password is not changed, and I remain on the 'Change Password' page.

### 3.3.8 Validation Notes

Test against each rule defined in the password policy (US-023) to ensure correct error messages are displayed.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Attempt to bypass the forced password change page

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

I have been redirected to the 'Change Password' page after logging in with a temporary password

### 3.4.5 When

I attempt to manually navigate to another application URL (e.g., '/dashboard', '/reports')

### 3.4.6 Then

The system intercepts the request and redirects me back to the 'Change Password' page.

### 3.4.7 Validation Notes

This must be enforced server-side (e.g., via middleware) to prevent client-side manipulation.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

API access is restricted with a temporary password

### 3.5.3 Scenario Type

Security

### 3.5.4 Given

I have authenticated via the API with a temporary password and received a JWT

### 3.5.5 When

I attempt to call any API endpoint other than the one for changing the password

### 3.5.6 Then

The API returns an HTTP 403 Forbidden status with an error code indicating a password change is required.

### 3.5.7 Validation Notes

Test various API endpoints (e.g., GET /api/v1/reports) to confirm they are blocked, while the password change endpoint remains accessible.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Attempt to change password with incorrect current (temporary) password

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

I am on the 'Change Password' page after a forced redirect

### 3.6.5 When

I enter an incorrect password in the 'Current Password' field

### 3.6.6 Then

The system displays an error message 'Incorrect current password' and does not change my password.

### 3.6.7 Validation Notes

The server must validate the temporary password before allowing a change.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated, full-page 'Change Password' form.
- Input field for 'Current Password' (the temporary one).
- Input field for 'New Password' with a show/hide toggle.
- Input field for 'Confirm New Password'.
- A 'Submit' or 'Update Password' button.
- A visual password strength indicator.
- A clear text block displaying the current password policy requirements.

## 4.2.0 User Interactions

- The page should not display the standard application navigation (sidebar, header) to prevent navigation away.
- Inline validation errors should appear next to the relevant fields upon failed submission.
- The 'Submit' button should be disabled until all fields are filled.

## 4.3.0 Display Requirements

- A clear title such as 'Please update your password'.
- A brief explanation that a password change is required for security.
- A success notification (e.g., a toast message) upon successful password change before redirecting.

## 4.4.0 Accessibility Needs

- All form fields must have associated `<label>` tags.
- Error messages must be programmatically linked to their corresponding input fields using `aria-describedby`.
- The page must be fully navigable and operable using only a keyboard.
- Color contrast must meet WCAG 2.1 AA standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A user account flagged for a mandatory password change cannot access any application functionality until the password has been successfully changed.

### 5.1.3 Enforcement Point

Server-side, on every authenticated request (both UI and API).

### 5.1.4 Violation Handling

HTTP requests to the web UI are redirected to the 'Change Password' page. API requests return an HTTP 403 Forbidden error.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The new password must comply with the system's active password policy.

### 5.2.3 Enforcement Point

Server-side, during the submission of the password change form.

### 5.2.4 Violation Handling

The request is rejected with a specific error message indicating the policy failure.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-023

#### 6.1.1.2 Dependency Reason

The password policy defined in this story is required to validate the user's new password.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-024

#### 6.1.2.2 Dependency Reason

The administrator's password reset action in this story is the trigger that sets the 'password change required' state for the user.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-027

#### 6.1.3.2 Dependency Reason

The core login mechanism must be modified to check for the 'password change required' flag after successful authentication.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity for user management and password hashing.
- A server-side mechanism (e.g., middleware in ASP.NET Core) to intercept requests and enforce the redirect.

## 6.3.0.0 Data Dependencies

- Requires a new field/claim on the user data model (e.g., a boolean `MustChangePassword`) to track the user's state.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The password change and validation process should complete within 500ms.

## 7.2.0.0 Security

- All communication must be over HTTPS.
- The new password must be securely hashed and salted before storage, using the existing system mechanism (ASP.NET Core Identity's default).
- The 'password change required' state must be managed securely on the server and not be modifiable from the client.

## 7.3.0.0 Usability

- The reason for the forced password change should be clearly communicated to the user on the page.

## 7.4.0.0 Accessibility

- The 'Change Password' page must be compliant with WCAG 2.1 Level AA.

## 7.5.0.0 Compatibility

- The feature must work correctly on all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Modifying the core authentication workflow.
- Implementing server-side middleware to prevent bypass is non-trivial.
- Requires changes to the user entity in the database.
- Coordinating state between the backend (redirect) and frontend (handling the redirect).

## 8.3.0.0 Technical Risks

- Incorrectly implemented middleware could create a security loophole or lock users out.
- Database migration for adding the new user flag must be handled carefully to avoid data loss.

## 8.4.0.0 Integration Points

- The login controller/service.
- The user management service (for the admin reset part).
- A new API endpoint for handling the password change.
- A new middleware component in the ASP.NET Core request pipeline.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Full end-to-end flow: Admin resets password, user logs in, is forced to change, changes successfully, logs in again successfully.
- All error conditions listed in acceptance criteria (mismatched passwords, policy violations, incorrect current password).
- Bypass attempt via direct URL navigation.
- Bypass attempt via API calls.
- Session timeout on the forced change password page.

## 9.3.0.0 Test Data Needs

- A user account that can be repeatedly reset by an admin.
- A set of invalid passwords to test each rule of the password policy.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- An E2E testing framework like Playwright or Cypress.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for all new logic, achieving >= 80% coverage
- Integration testing for the authentication and redirect flow completed successfully
- End-to-end automated test for the happy path scenario is created and passing
- User interface reviewed and approved for usability and accessibility (WCAG 2.1 AA)
- Security requirements validated, including checks for bypass vulnerabilities
- Documentation for the user password reset flow is updated
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is blocked by US-023 and US-024.
- Requires careful coordination between backend and frontend developers due to the modified authentication flow.

## 11.4.0.0 Release Impact

This is a critical security feature required for a secure initial release.

