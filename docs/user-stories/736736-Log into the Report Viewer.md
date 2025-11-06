# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-077 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Log into the Report Viewer |
| As A User Story | As an End-User (Viewer), I want to securely log in... |
| User Persona | End-User (Viewer). This functionality also applies... |
| Business Value | Provides secure, authenticated access to the Repor... |
| Functional Area | User Authentication & Access Control |
| Story Theme | User & Access Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful login for a user without Two-Factor Authentication (2FA)

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A user with a valid, active account (without 2FA enabled) is on the Report Viewer login page

### 3.1.5 When

The user enters their correct username and password and clicks the 'Log In' button

### 3.1.6 Then

The system authenticates the user, a session is created, and the user is redirected to the main Report Viewer dashboard.

### 3.1.7 Validation Notes

Verify redirection to the correct page and that a valid JWT is issued by the authentication API.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Successful login for a user with Two-Factor Authentication (2FA) enabled

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

A user with a valid, active account (with 2FA enabled) has entered their correct username and password

### 3.2.5 When

The user is presented with the 2FA screen and enters the correct code from their authenticator app

### 3.2.6 Then

The system validates the 2FA code, authenticates the user, and redirects them to the main Report Viewer dashboard.

### 3.2.7 Validation Notes

This is a two-step process. First, verify the redirect to the 2FA page. Second, verify successful login after correct code entry.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Login attempt with invalid credentials

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

A user is on the Report Viewer login page

### 3.3.5 When

The user enters an incorrect username or password and clicks 'Log In'

### 3.3.6 Then

The system rejects the authentication, a single, non-specific error message ('Invalid username or password') is displayed, and the user remains on the login page.

### 3.3.7 Validation Notes

Verify the error message is generic to prevent username enumeration. The failed login attempt counter for the user must be incremented.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Login attempt with an invalid 2FA code

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

A user has successfully entered their credentials and is on the 2FA verification page

### 3.4.5 When

The user enters an incorrect 2FA code and clicks 'Verify'

### 3.4.6 Then

The system rejects the code, an error message ('Invalid verification code') is displayed, and the user remains on the 2FA verification page.

### 3.4.7 Validation Notes

Verify the user is not logged out and can re-attempt entering the 2FA code.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Login attempt with empty credentials

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

A user is on the Report Viewer login page

### 3.5.5 When

The user clicks the 'Log In' button without entering a username and/or password

### 3.5.6 Then

Client-side validation prevents form submission, and inline error messages appear next to the required empty fields.

### 3.5.7 Validation Notes

Verify no API call is made and that validation messages are clear and accessible.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Login attempt for a locked account

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

A user's account has been locked after 5 consecutive failed login attempts

### 3.6.5 When

The user attempts to log in with their correct credentials

### 3.6.6 Then

The system rejects the authentication, and a specific error message ('Your account is locked. Please contact an administrator.') is displayed.

### 3.6.7 Validation Notes

Verify that even correct credentials fail for a locked account.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Input field for 'Username'
- Input field for 'Password'
- A 'Log In' button to submit the form
- A dedicated screen/modal for 2FA code entry with a 'Verify' button
- An area to display login error messages

## 4.2.0 User Interactions

- The password field must mask characters as they are typed.
- Pressing 'Enter' in the password field should trigger the login action.
- The login button should show a disabled/loading state while the authentication request is in progress.

## 4.3.0 Display Requirements

- The login page should be clean and focused on the login task.
- Error messages must be clearly visible and easy to understand.

## 4.4.0 Accessibility Needs

- All form fields must have associated labels.
- The UI must be navigable using a keyboard.
- Error messages must be associated with their respective fields for screen readers (e.g., using `aria-describedby`).
- Color contrast must meet WCAG 2.1 AA standards.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'A user account shall be locked after 5 consecutive failed login attempts.', 'enforcement_point': 'Backend authentication service (ASP.NET Core Identity).', 'violation_handling': "The user's account is flagged as 'locked' in the database, preventing any further successful logins until an Administrator unlocks it."}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-018

#### 6.1.1.2 Dependency Reason

A user account must exist before a user can log in.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-029

#### 6.1.2.2 Dependency Reason

The 2FA login flow depends on the core 2FA logic being implemented.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-031

#### 6.1.3.2 Dependency Reason

The account lockout logic must be in place to be enforced during login attempts.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-087

#### 6.1.4.2 Dependency Reason

The frontend login UI requires the backend API endpoint for authentication and JWT issuance.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity for user management and authentication.
- JWT generation and validation middleware.
- Frontend state management (Zustand) for handling authentication state.
- HTTPS must be configured for the web server.

## 6.3.0.0 Data Dependencies

- Access to the 'Users' table in the SQLite configuration database.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The P95 latency for the login API call should be under 500ms.

## 7.2.0.0 Security

- All communication between the client and server must be encrypted using HTTPS/TLS 1.2+.
- Passwords must never be stored in plaintext; they must be hashed using a strong, salted algorithm (handled by ASP.NET Core Identity).
- The system must prevent username enumeration by providing a generic error message for failed logins.
- The issued JWT must have a configurable, reasonably short expiration time (e.g., 60 minutes) as per US-088.

## 7.3.0.0 Usability

- The login process should be simple and intuitive for non-technical users.

## 7.4.0.0 Accessibility

- The login interface must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The login page must render and function correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementing the two-step authentication flow for 2FA.
- Integrating the frontend React components with the backend ASP.NET Core Identity API.
- Securely handling and storing the JWT on the client side.
- Implementing and testing the failed attempt counter and account lockout logic.

## 8.3.0.0 Technical Risks

- Improper handling of JWTs on the client could lead to security vulnerabilities (e.g., XSS).
- Race conditions in the failed login attempt counter if not handled atomically.

## 8.4.0.0 Integration Points

- Frontend (React) -> Backend Login API (/api/v1/auth/login)
- Backend API -> ASP.NET Core Identity Service
- ASP.NET Core Identity Service -> SQLite Database

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify successful login with and without 2FA.
- Verify login failure with incorrect password.
- Verify login failure with incorrect username.
- Verify login failure with incorrect 2FA code.
- Verify account lockout after 5 failed attempts.
- Verify that a locked account cannot log in even with correct credentials.
- Verify client-side validation for empty fields.

## 9.3.0.0 Test Data Needs

- A test user account without 2FA enabled.
- A test user account with 2FA enabled and a known TOTP secret.
- A test user account that can be programmatically locked.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- Cypress or Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >80% coverage
- Integration testing completed successfully for the full login flow
- User interface reviewed and approved for usability and accessibility
- Performance requirements verified
- Security requirements validated, including checks against OWASP Top 10 for authentication
- Documentation updated for the login process
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational story and a blocker for most other UI-based stories. It should be prioritized in an early sprint.
- Requires both frontend and backend development effort.

## 11.4.0.0 Release Impact

- Core functionality required for the initial release. The product cannot be used without it.

