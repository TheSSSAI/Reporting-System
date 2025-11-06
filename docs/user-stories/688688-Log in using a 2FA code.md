# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-029 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Log in using a 2FA code |
| As A User Story | As a User with 2FA enabled, I want to enter a time... |
| User Persona | Any system user (Administrator, End-User) who has ... |
| Business Value | Enhances system security by adding a second layer ... |
| Functional Area | User Authentication and Security |
| Story Theme | User & Access Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful login with a valid 2FA code

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A user with 2FA enabled has successfully authenticated with their correct username and password

### 3.1.5 When

the user is presented with the 2FA code entry screen, enters the correct 6-digit code from their authenticator app, and clicks 'Verify'

### 3.1.6 Then

the system validates the code, completes the login, issues a JWT, and redirects the user to their appropriate default page (Control Panel or Report Viewer).

### 3.1.7 Validation Notes

Verify that a valid JWT is received by the client and the user is successfully redirected to the correct application view.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Login attempt with an invalid 2FA code

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

A user with 2FA enabled is on the 2FA code entry screen after a successful password validation

### 3.2.5 When

the user enters an incorrect 6-digit code and clicks 'Verify'

### 3.2.6 Then

the system displays a clear error message such as 'Invalid authentication code. Please try again.', the user remains on the 2FA screen, and the input field is cleared.

### 3.2.7 Validation Notes

Verify the error message is displayed and the user is not logged in. The failed login attempt counter for the user should be incremented.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Login attempt with an expired 2FA code

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

A user with 2FA enabled is on the 2FA code entry screen

### 3.3.5 When

the user enters a code that has just expired (outside the server's allowed time-drift window)

### 3.3.6 Then

the system treats it as an invalid code and displays the corresponding error message.

### 3.3.7 Validation Notes

The TOTP validation logic should allow for a small time skew (e.g., one 30-second interval in the past or future). This test should verify that codes outside this window are rejected.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Account is locked after multiple failed 2FA attempts

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

A user with 2FA enabled is on the 2FA code entry screen and has 4 previous failed login attempts in the current session

### 3.4.5 When

the user enters an incorrect 2FA code for the 5th consecutive time

### 3.4.6 Then

the system locks the user's account, logs the security event in the audit log, and displays a message like 'Your account has been locked due to too many failed login attempts. Please contact an administrator.'

### 3.4.7 Validation Notes

Verify the account's 'LockoutEnd' property is set in the database and any subsequent login attempts are blocked until the account is unlocked.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Login flow for a user without 2FA enabled

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

A user who does NOT have 2FA enabled on their account

### 3.5.5 When

the user successfully authenticates with their correct username and password

### 3.5.6 Then

the system bypasses the 2FA code entry screen entirely, logs the user in directly, and redirects them to their default page.

### 3.5.7 Validation Notes

This is a negative test case to ensure the 2FA screen is only shown when required.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Client-side validation for 2FA code format

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

A user is on the 2FA code entry screen

### 3.6.5 When

the user attempts to enter non-numeric characters or a code that is not 6 digits long

### 3.6.6 Then

the UI prevents the invalid input, and the 'Verify' button remains disabled until a valid 6-digit numeric code is entered.

### 3.6.7 Validation Notes

Test by trying to type letters and symbols into the input field and by entering fewer or more than 6 digits.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated page or modal for 2FA code entry.
- A text input field for the 6-digit code.
- A primary action button labeled 'Verify' or 'Submit'.
- A designated area to display validation error messages.

## 4.2.0 User Interactions

- The input field should be auto-focused when the page loads.
- The input field should accept only numeric characters.
- The 'Verify' button should be disabled until 6 digits are entered.
- Pressing 'Enter' in the input field should trigger the 'Verify' action.

## 4.3.0 Display Requirements

- Clear instructional text, e.g., 'Enter the 6-digit code from your authenticator app.'
- Error messages must be clear, concise, and displayed close to the input field.

## 4.4.0 Accessibility Needs

- The input field must have a proper `<label>`.
- The page must be navigable using a keyboard.
- Error messages must be associated with the input field using `aria-describedby` for screen readers.
- The UI must meet WCAG 2.1 Level AA standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A user's account shall be locked after 5 consecutive failed login attempts, including failures at the 2FA stage.

### 5.1.3 Enforcement Point

Server-side, after validating the submitted 2FA code.

### 5.1.4 Violation Handling

The user account's `LockoutEnabled` flag is set, a `LockoutEnd` timestamp is recorded, and an audit log entry is created.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The 2FA verification step is mandatory for any user who has enabled it.

### 5.2.3 Enforcement Point

Server-side, during the login API workflow.

### 5.2.4 Violation Handling

The system will not issue a final authentication token (JWT) until the 2FA challenge is successfully completed.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-027

#### 6.1.1.2 Dependency Reason

The basic username/password login flow must exist before the second factor can be added to it.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-028

#### 6.1.2.2 Dependency Reason

Users must be able to set up and enable 2FA on their accounts before they can use it to log in.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-031

#### 6.1.3.2 Dependency Reason

The core account lockout mechanism must be implemented to be extended to cover 2FA failures.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity framework for user management and TOTP validation.
- React frontend framework for building the UI component.
- JWT generation and validation middleware for API authentication.

## 6.3.0.0 Data Dependencies

- The user's record in the database must have a flag indicating if 2FA is enabled and a stored secret key for TOTP generation.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The server-side validation of the 2FA code must complete with a P95 latency of under 500ms.

## 7.2.0.0 Security

- All communication between the client and server during the 2FA process must be over HTTPS/TLS 1.2+.
- The TOTP validation logic must be robust against timing attacks.
- The state between the password step and the 2FA step must be managed securely (e.g., using a short-lived, single-use token) to prevent session fixation.
- The login attempt counter must be handled securely on the server side.

## 7.3.0.0 Usability

- The process should be intuitive, requiring minimal user instruction.
- The `autocomplete="one-time-code"` attribute should be used on the input field to improve user experience on supported devices.

## 7.4.0.0 Accessibility

- The 2FA login page must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Modifying the existing single-step login API to a multi-step workflow.
- Securely managing the intermediate state between password validation and 2FA code submission.
- Implementing the corresponding multi-step logic and state management on the React frontend.
- Integrating the ASP.NET Core Identity 2FA provider with the custom JWT authentication scheme.

## 8.3.0.0 Technical Risks

- Improperly managing the intermediate login state could introduce security vulnerabilities.
- Ensuring the failed attempt counter correctly spans both the password and 2FA stages of login.

## 8.4.0.0 Integration Points

- The existing `/api/v1/login` endpoint.
- A new endpoint, e.g., `/api/v1/login/verify-2fa`, will be required.
- The ASP.NET Core Identity user store and sign-in manager.
- The system's audit logging service.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Full E2E login flow for a 2FA-enabled user.
- Full E2E login flow for a non-2FA user.
- Submitting an invalid 2FA code.
- Triggering the account lockout mechanism through repeated 2FA failures.
- Attempting to bypass the 2FA step after successful password validation.

## 9.3.0.0 Test Data Needs

- At least two test user accounts: one with 2FA enabled, one without.
- A mobile device with an authenticator app for E2E testing.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- A tool like Postman or Insomnia for API integration testing.
- An E2E testing framework like Playwright or Cypress.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for both backend and frontend, achieving >= 80% coverage for new code
- Integration testing of the multi-step login API completed successfully
- E2E tests for the complete login flow are passing
- User interface reviewed and approved for usability and accessibility (WCAG 2.1 AA)
- Security requirements validated, including checks for vulnerabilities in the multi-step flow
- User Guide documentation updated to explain the 2FA login process
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is blocked by US-028 (2FA Setup). It should be scheduled in a sprint immediately following the completion of US-028.
- Requires coordinated effort between backend and frontend developers due to the multi-step API flow.

## 11.4.0.0 Release Impact

Completes a critical security feature. The full 2FA functionality (setup and login) can be highlighted in release notes.

