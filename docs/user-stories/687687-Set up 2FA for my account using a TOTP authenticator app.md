# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-028 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Set up 2FA for my account using a TOTP authenticat... |
| As A User Story | As an Authenticated User, I want to set up Time-ba... |
| User Persona | Any authenticated user of the system (Administrato... |
| Business Value | Enhances the overall security posture of the appli... |
| Functional Area | User Management & Security |
| Story Theme | Account Security Enhancements |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Successfully enable 2FA for the first time

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in user, my account does not have 2FA enabled, and the system-wide 2FA setting is enabled by an administrator

### 3.1.5 When

I navigate to my 'Account Settings' page and initiate the 2FA setup process

### 3.1.6 Then

The system presents a setup screen containing a unique QR code, a manual setup key, and clear instructions.

### 3.1.7 Validation Notes

Verify the QR code and manual key are displayed. The key should be generated on the backend for this specific user and session.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Successfully verify the authenticator app and complete setup

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am on the 2FA setup screen and have scanned the QR code with my TOTP authenticator app

### 3.2.5 When

I enter the valid 6-digit code from my app into the verification field and submit the form

### 3.2.6 Then

The system confirms the code is correct, displays a success message, and enables 2FA for my account.

### 3.2.7 Validation Notes

Check the user's record in the database to confirm the 2FA flag is set to true and a secret key is stored (encrypted).

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Receive and acknowledge recovery codes after setup

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I have just successfully verified my authenticator app

### 3.3.5 When

The system confirms 2FA is enabled

### 3.3.6 Then

I am immediately shown a list of single-use recovery codes with strong advice to store them securely.

### 3.3.7 Validation Notes

Verify a list of 8-10 recovery codes is displayed. Verify there are options to copy the codes to the clipboard and/or download them as a .txt file.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Account settings reflect that 2FA is enabled

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

I have completed the 2FA setup process

### 3.4.5 When

I navigate back to my 'Account Settings' page

### 3.4.6 Then

The page clearly indicates that 'Two-Factor Authentication is Enabled' and provides options to 'Disable 2FA' and 'View/Regenerate Recovery Codes'.

### 3.4.7 Validation Notes

Check the UI state on the account settings page.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Attempt to enable 2FA with an incorrect verification code

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am on the 2FA setup screen and have scanned the QR code

### 3.5.5 When

I enter an invalid or expired 6-digit code and submit the form

### 3.5.6 Then

The system displays a clear, non-blocking error message like 'Invalid verification code. Please try again.'

### 3.5.7 Validation Notes

Verify the error message is shown and the user remains on the setup screen. Check the database to ensure the 2FA flag for the user remains false.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Abandon the 2FA setup process before completion

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

I have initiated the 2FA setup process and am viewing the QR code

### 3.6.5 When

I navigate away from the page or close my browser without entering a verification code

### 3.6.6 Then

2FA is not enabled for my account.

### 3.6.7 Validation Notes

Log back in and navigate to account settings; verify 2FA is still disabled. Initiating setup again should generate a new QR code and secret key.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A button/link in User Profile/Settings to 'Enable Two-Factor Authentication'.
- A modal or dedicated page for the setup flow.
- A display area for the QR code image.
- A read-only text field displaying the manual setup key with a 'Copy' button.
- A text input field for the 6-digit verification code.
- A 'Verify & Enable' button.
- A dedicated screen to display recovery codes with 'Copy Codes' and 'Download Codes' buttons.
- An indicator on the main settings page showing 2FA status ('Enabled'/'Disabled').

## 4.2.0 User Interactions

- The setup flow should be a guided, step-by-step process.
- The verification code input should only accept digits and have a max length of 6.
- Feedback (success/error messages) must be immediate and clear.

## 4.3.0 Display Requirements

- Instructions must be clear and concise (e.g., 'Step 1: Scan QR Code', 'Step 2: Enter Verification Code').
- A strong warning must be displayed with the recovery codes, emphasizing the need to store them in a safe place and that they will not be shown again.

## 4.4.0 Accessibility Needs

- The UI must be WCAG 2.1 Level AA compliant.
- The manual setup key is critical for users with visual impairments using screen readers and must be programmatically accessible.
- All buttons and form fields must have proper labels.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A new, unique TOTP secret key must be generated for each setup attempt.

### 5.1.3 Enforcement Point

Backend, when the user initiates the 2FA setup flow.

### 5.1.4 Violation Handling

If a key cannot be generated, an error is shown to the user and the process is halted.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Recovery codes must be generated and shown to the user only once upon successful 2FA activation.

### 5.2.3 Enforcement Point

Backend, immediately after successful verification of the initial TOTP code.

### 5.2.4 Violation Handling

If recovery codes cannot be generated, the 2FA activation should be rolled back and an error shown.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-027

#### 6.1.1.2 Dependency Reason

User must be able to log in to access their account settings to set up 2FA.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-026

#### 6.1.2.2 Dependency Reason

The system-wide feature toggle for 2FA must be enabled by an Administrator before any user can set it up for their own account.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity framework for TOTP logic and user management.
- A .NET library for QR code generation (e.g., QRCoder).
- React frontend framework for building the UI components.
- Entity Framework Core for database schema migrations to the User entity.

## 6.3.0.0 Data Dependencies

- The User table in the SQLite database must be modified to include fields for the encrypted 2FA secret key, a boolean flag for 2FA status, and stored recovery codes (hashed).

## 6.4.0.0 External Dependencies

- User must have a TOTP-compliant authenticator application (e.g., Google Authenticator, Microsoft Authenticator, Authy) on a separate device.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- QR code generation and initial page load should complete in under 500ms.
- Verification of the TOTP code should respond in under 200ms.

## 7.2.0.0 Security

- The TOTP secret key must be generated using a cryptographically secure pseudo-random number generator (CSPRNG).
- The secret key must be stored encrypted at rest in the database using the system's DPAPI mechanism.
- The verification endpoint must be protected against brute-force attacks (rate limiting).
- Recovery codes must be stored hashed, not in plaintext.

## 7.3.0.0 Usability

- The setup process must be intuitive and require minimal technical knowledge from the user.
- Error messages must be user-friendly and actionable.

## 7.4.0.0 Accessibility

- Must adhere to WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The web interface must function correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires coordinated changes across the frontend (React UI flow), backend (API endpoints, business logic), and database (schema migration).
- Security considerations for key generation, storage, and verification are critical and non-trivial.
- State management on the frontend to guide the user through the multi-step setup process.

## 8.3.0.0 Technical Risks

- Improper handling or storage of the secret key could create a major security vulnerability.
- Poor user experience in the setup flow could lead to user frustration and support calls.

## 8.4.0.0 Integration Points

- Integrates with the existing user authentication system (ASP.NET Core Identity).
- The frontend account settings page must be updated to include this new functionality.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Successful end-to-end setup of 2FA.
- Attempted setup with an incorrect verification code.
- Abandoning and restarting the setup process.
- Verifying recovery codes are generated and downloadable.
- Confirming the UI correctly reflects the 2FA status post-setup.

## 9.3.0.0 Test Data Needs

- Test user accounts with 2FA disabled.
- A method to programmatically retrieve the TOTP code for a given secret key for automated E2E testing.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- A framework like Playwright or Cypress for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing (>=80% coverage)
- Integration testing completed successfully
- E2E tests for happy path and key error conditions are implemented and passing
- User interface reviewed for usability and approved
- Security requirements validated via code review and/or vulnerability scan
- Accessibility audit passed (WCAG 2.1 AA)
- Documentation in the User Guide for this feature is created/updated
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a blocker for US-029 (Log in using a 2FA code). Both should be planned in close succession, ideally in the same or consecutive sprints.
- Requires both frontend and backend development effort.

## 11.4.0.0 Release Impact

This is a significant security feature. Its inclusion in a release should be highlighted in release notes.

