# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-026 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Configure system-wide enforcement of Two-Factor Au... |
| As A User Story | As an Administrator, I want to enable or disable a... |
| User Persona | Administrator |
| Business Value | Increases system security by enforcing multi-facto... |
| Functional Area | System Administration & Security |
| Story Theme | User & Access Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Administrator enables mandatory 2FA

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an Administrator logged into the Control Panel and the 'Enforce 2FA' setting is currently disabled

### 3.1.5 When

I navigate to the 'System Settings -> Security' page, toggle the 'Enforce 2FA' option to 'Enabled', and confirm the action in a confirmation dialog

### 3.1.6 Then

The system saves the setting, a success notification is displayed, and a security event is recorded in the audit log indicating that 2FA has been enabled system-wide by my user account.

### 3.1.7 Validation Notes

Verify the setting persists after a page refresh. Check the audit log for the corresponding entry.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

User without 2FA is forced to set it up after enforcement is enabled

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The system-wide 'Enforce 2FA' setting is enabled

### 3.2.5 When

A user who has not yet configured 2FA successfully authenticates with their username and password

### 3.2.6 Then

The user is immediately redirected to the mandatory 2FA setup page, which displays a QR code and instructions, and they are blocked from accessing any other part of the application until setup is successfully completed.

### 3.2.7 Validation Notes

Attempt to navigate to other application URLs directly; the user should always be redirected back to the 2FA setup page.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User with existing 2FA setup logs in while enforcement is enabled

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

The system-wide 'Enforce 2FA' setting is enabled

### 3.3.5 When

A user who has already configured 2FA successfully authenticates with their username and password

### 3.3.6 Then

The user is prompted to enter a valid TOTP code from their authenticator app to complete the login process.

### 3.3.7 Validation Notes

Verify that a correct code grants access and an incorrect code shows an error and prevents login.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Administrator disables mandatory 2FA

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

I am an Administrator logged into the Control Panel and the 'Enforce 2FA' setting is currently enabled

### 3.4.5 When

I navigate to the 'System Settings -> Security' page, toggle the 'Enforce 2FA' option to 'Disabled', and confirm the action

### 3.4.6 Then

The system saves the setting, a success notification is displayed, and a security event is recorded in the audit log indicating that 2FA has been disabled system-wide.

### 3.4.7 Validation Notes

Verify the setting persists. Check the audit log for the corresponding entry.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

User logs in when 2FA enforcement is disabled

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

The system-wide 'Enforce 2FA' setting is disabled

### 3.5.5 When

Any user (with or without a prior 2FA setup) successfully authenticates with their username and password

### 3.5.6 Then

The user is immediately granted access to the application without being prompted for a 2FA code.

### 3.5.7 Validation Notes

Test with a user who has 2FA configured and one who does not. Both should log in directly after password validation.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Administrator is shown a confirmation dialog before changing the 2FA setting

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

I am an Administrator on the 'System Settings -> Security' page

### 3.6.5 When

I click the toggle to change the 'Enforce 2FA' setting

### 3.6.6 Then

A confirmation modal appears, clearly stating the impact of the change (e.g., 'Enabling this will require all users to set up and use 2FA on their next login. Are you sure?') and the change is only saved after I confirm.

### 3.6.7 Validation Notes

Verify that canceling the dialog reverts the toggle to its original state and does not save the change.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A toggle switch or checkbox labeled 'Enforce Two-Factor Authentication (2FA) for all users' within the 'System Settings -> Security' section of the Control Panel.
- Helper text below the toggle explaining its function and impact.
- A confirmation modal dialog with 'Confirm' and 'Cancel' actions.
- Success and error notification toasts for the save operation.

## 4.2.0 User Interactions

- Administrator clicks the toggle to change the state.
- Administrator confirms or cancels the change in the modal dialog.

## 4.3.0 Display Requirements

- The current state (Enabled/Disabled) of 2FA enforcement must be clearly visible on the settings page.

## 4.4.0 Accessibility Needs

- The toggle switch must be keyboard accessible and have appropriate ARIA attributes (e.g., `role='switch'`, `aria-checked`).
- The confirmation modal must trap focus and be dismissible via the Escape key.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

When system-wide 2FA is enforced, a user cannot complete a login session or access any application resources after password validation until a valid 2FA token is provided or 2FA is configured.

### 5.1.3 Enforcement Point

Post-authentication middleware, before a full session is established.

### 5.1.4 Violation Handling

User is redirected to the 2FA setup/entry page. API requests from a partially authenticated session are rejected with an HTTP 403 Forbidden status.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Any change to the system-wide 2FA enforcement setting must be recorded in the system audit log.

### 5.2.3 Enforcement Point

Backend service layer when the setting is updated.

### 5.2.4 Violation Handling

The operation should fail if the audit log entry cannot be written.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-027

#### 6.1.1.2 Dependency Reason

A standard username/password login flow must exist to hook into.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-028

#### 6.1.2.2 Dependency Reason

The UI and backend logic for an individual user to set up their own 2FA (QR code generation, secret storage) must be implemented first.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-029

#### 6.1.3.2 Dependency Reason

The mechanism for prompting for and validating a TOTP code during login must be implemented first.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-101

#### 6.1.4.2 Dependency Reason

The audit logging service must be available to record this critical security configuration change.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity for user management and authentication flows.
- A system configuration store (SQLite database) to persist the global setting.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The check for the 2FA enforcement setting should add negligible latency (<5ms) to the authentication process.

## 7.2.0.0 Security

- The API endpoint for changing this setting must be restricted to Administrator roles only.
- The change must be logged in the tamper-evident audit log, including the administrator's username, timestamp, and the change made.
- The system must prevent session fixation for users in the middle of the 2FA setup flow.

## 7.3.0.0 Usability

- The setting's purpose and impact must be clearly communicated to the Administrator in the UI.
- The forced setup process for end-users must be simple and intuitive.

## 7.4.0.0 Accessibility

- All UI elements related to this feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Modifying the core authentication pipeline in ASP.NET Core Identity is a sensitive operation.
- Requires careful handling of user states (e.g., authenticated-but-needs-2fa-setup).
- Potential risk of user lockout if implemented incorrectly.
- Requires both backend (logic, database) and frontend (UI component, state management) work.

## 8.3.0.0 Technical Risks

- A bug could lock all users, including administrators, out of the system. A recovery mechanism or script should be considered.
- Improper session management could allow a user to bypass the mandatory 2FA setup.

## 8.4.0.0 Integration Points

- ASP.NET Core Identity authentication pipeline.
- System configuration service.
- Audit logging service.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Admin enables 2FA; verify a non-2FA user is forced to set it up.
- Admin enables 2FA; verify a 2FA-enabled user is prompted for a code.
- Admin disables 2FA; verify all users can log in with only a password.
- Create a new user while 2FA is enabled; verify they are forced to set it up on first login.
- Verify the audit log is correctly updated on every change to the setting.
- Attempt to bypass 2FA setup by navigating to other URLs.
- Security review of the authentication flow to check for vulnerabilities.

## 9.3.0.0 Test Data Needs

- An Administrator account.
- A user account with 2FA already configured.
- A user account with no 2FA configured.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- An E2E testing framework like Playwright or Cypress.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit test coverage for new logic is at or above 80%
- Integration tests for the authentication flows are implemented and passing
- E2E tests covering the primary user scenarios are passing
- UI/UX has been reviewed and approved by the product owner
- Security review of the authentication changes has been completed
- All related UI components meet WCAG 2.1 AA standards
- Documentation for administrators on how to manage this setting is updated
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- All prerequisite stories (US-027, US-028, US-029, US-101) must be completed and merged before starting work on this story.
- Allocate extra time for thorough QA and security testing due to the critical nature of the authentication flow.

## 11.4.0.0 Release Impact

This is a key security feature for enterprise customers and should be included in any major release targeting such clients.

