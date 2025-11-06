# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-023 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Configure a system-wide password policy |
| As A User Story | As an Administrator, I want to configure and enfor... |
| User Persona | Administrator |
| Business Value | Enhances system security by enforcing strong passw... |
| Functional Area | System Administration & Security |
| Story Theme | User & Access Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Administrator views the password policy settings page

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am logged in as an Administrator

### 3.1.5 When

I navigate to the 'System Settings' -> 'Security' -> 'Password Policy' section in the Control Panel

### 3.1.6 Then

I can see the current system-wide password policy settings displayed in an editable form, with values populated from the system's configuration.

### 3.1.7 Validation Notes

Verify that the UI correctly fetches and displays the current policy. The default values from SRS 4.2 should be present on a fresh install.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Administrator successfully updates the password policy

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am on the Password Policy configuration page

### 3.2.5 When

I modify the policy settings (e.g., set minimum length to 14, require special characters, set expiration to 60 days, and history to 10) and click 'Save'

### 3.2.6 Then

A success notification is displayed, the new settings are persisted to the database, and the form now shows the updated values.

### 3.2.7 Validation Notes

Check the database to confirm the new policy values are saved. Verify the change is recorded in the audit log.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

New password policy is enforced during user password change

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

The password policy has been updated and saved

### 3.3.5 When

Any user attempts to change their password with a new password that complies with the new policy

### 3.3.6 Then

The password change is successful.

### 3.3.7 Validation Notes

This must be tested for all password change scenarios: user self-service change, admin-initiated reset, and new user creation.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

User password change is rejected for violating the length policy

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

The password policy requires a minimum length of 12 characters

### 3.4.5 When

A user tries to set a new password that is 10 characters long

### 3.4.6 Then

The system rejects the password and displays a clear error message: 'Password must be at least 12 characters long.'

### 3.4.7 Validation Notes

Verify the error message is specific to the rule that was violated.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

User password change is rejected for violating the complexity policy

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

The password policy requires at least one uppercase letter and one number

### 3.5.5 When

A user tries to set a new password 'newpassword!'

### 3.5.6 Then

The system rejects the password and displays clear error messages: 'Password must contain at least one uppercase letter.' and 'Password must contain at least one number.'

### 3.5.7 Validation Notes

Verify that all violated rules are listed in the feedback to the user.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

User password change is rejected for violating the history policy

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

The password history policy is set to prevent reuse of the last 5 passwords

### 3.6.5 When

A user tries to change their password to one they have used within their last 5 changes

### 3.6.6 Then

The system rejects the password and displays a clear error message: 'Cannot reuse a recent password.'

### 3.6.7 Validation Notes

The system must store a hash of previous passwords to check against, not plaintext.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Administrator enters invalid data into policy fields

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

I am on the Password Policy configuration page

### 3.7.5 When

I enter a non-numeric value for 'Minimum Length' or a negative number for 'Password Expiration'

### 3.7.6 Then

The UI displays an inline validation error next to the invalid field, and the 'Save' button is disabled until the error is corrected.

### 3.7.7 Validation Notes

Test with various invalid inputs like text, negative numbers, and decimals where integers are expected.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Policy changes do not force immediate change for existing users

### 3.8.3 Scenario Type

Edge_Case

### 3.8.4 Given

An Administrator makes the password policy more restrictive

### 3.8.5 When

An existing user whose current password does not meet the new policy logs in

### 3.8.6 Then

The user is able to log in successfully without being forced to change their password.

### 3.8.7 Validation Notes

The new policy should only be enforced upon the user's next password change or expiration.

## 3.9.0 Criteria Id

### 3.9.1 Criteria Id

AC-009

### 3.9.2 Scenario

Setting password expiration to 0 disables expiration

### 3.9.3 Scenario Type

Edge_Case

### 3.9.4 Given

I am on the Password Policy configuration page

### 3.9.5 When

I set the 'Password Expiration (days)' to 0 and save the policy

### 3.9.6 Then

User passwords will no longer expire.

### 3.9.7 Validation Notes

Verify that users are not prompted to change their password after the previous expiration period has passed.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated 'Password Policy' section within the Control Panel's 'System Settings'.
- Numeric input for 'Minimum Password Length' (e.g., default 12).
- Checkbox for 'Require Uppercase Letter'.
- Checkbox for 'Require Lowercase Letter'.
- Checkbox for 'Require Number'.
- Checkbox for 'Require Special Character'.
- Numeric input for 'Password Expiration (days)' (e.g., default 90).
- Numeric input for 'Enforce Password History (prevent reuse of last N passwords)' (e.g., default 5).
- 'Save Changes' button.
- 'Reset to Defaults' button.

## 4.2.0 User Interactions

- Administrator can modify values in the input fields and toggle checkboxes.
- The 'Save Changes' button becomes enabled only when there are unsaved changes and no validation errors.
- Hovering over a setting's label displays a tooltip explaining the setting.
- Saving displays a success toast/notification; failure displays an error message.

## 4.3.0 Display Requirements

- The form must clearly display the current, active password policy.
- Validation errors must be displayed inline, next to the corresponding input field, and be user-friendly.

## 4.4.0 Accessibility Needs

- All form fields must have associated `<label>` tags.
- The UI must be navigable and operable using a keyboard.
- Validation errors must be programmatically associated with their inputs for screen reader users.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

The password policy is a global setting and applies to all non-system users.

### 5.1.3 Enforcement Point

During password creation, password change, and password reset operations.

### 5.1.4 Violation Handling

The operation is rejected, and a user-facing error message detailing the violations is returned.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Changes to the password policy must be recorded in the system audit log.

### 5.2.3 Enforcement Point

Upon successful saving of a modified password policy.

### 5.2.4 Violation Handling

If auditing fails, the save operation should be rolled back and an error should be logged and displayed to the administrator.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

The system must ship with secure default password policy settings as defined in SRS 4.2.

### 5.3.3 Enforcement Point

During initial application installation/setup.

### 5.3.4 Violation Handling

N/A - This is a setup requirement.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-018

#### 6.1.1.2 Dependency Reason

The policy must be enforced when creating new users.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-024

#### 6.1.2.2 Dependency Reason

The policy must be enforced when an administrator resets a user's password.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-101

#### 6.1.3.2 Dependency Reason

Changes to the password policy are a security-sensitive event and must be audited.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity for user management and password validation.
- SQLite database for persisting the policy settings and password history.
- React component library (MUI) for the settings form UI.

## 6.3.0.0 Data Dependencies

- A new database table is required to store the password policy settings.
- A new database table is required to store a history of password hashes for each user to enforce the reuse policy.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- Saving the password policy should complete in under 500ms.
- Password validation checks during login or password change should add negligible latency (<50ms) to the operation.

## 7.2.0.0 Security

- The API endpoint for updating the password policy must be protected and accessible only by users with the 'Administrator' role.
- Password history must be stored as salted hashes, not plaintext.
- The system must not leak information about which part of a password is correct or incorrect beyond the defined rules.

## 7.3.0.0 Usability

- Error messages for password policy violations must be clear, specific, and consolidated to guide the user effectively.

## 7.4.0.0 Accessibility

- The password policy configuration page must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The configuration page must function correctly on all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementing the password history check requires a custom `IPasswordValidator<TUser>` in ASP.NET Core Identity.
- Requires database schema changes for both the policy settings and the password history table, which must be managed via EF Core migrations.
- Ensuring the policy is consistently applied across all relevant workflows (new user, reset password, change password) requires careful integration.

## 8.3.0.0 Technical Risks

- Incorrect implementation of the custom password history validator could lead to security vulnerabilities or performance issues.
- Forgetting to apply the validation logic at one of the password-setting endpoints would create a security loophole.

## 8.4.0.0 Integration Points

- ASP.NET Core Identity pipeline (specifically `UserManager` and `PasswordValidator`).
- User creation API endpoint.
- Password reset API endpoint.
- Password change API endpoint.
- Audit logging service.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Admin can view, update, and reset the password policy.
- A user attempts to set a new password that violates each policy rule individually.
- A user attempts to set a new password that violates multiple policy rules simultaneously.
- A user attempts to reuse a password that is in their history.
- A user with a non-compliant password can still log in after the policy is made stricter.
- A non-administrator user attempts to access the password policy API endpoint and is denied.

## 9.3.0.0 Test Data Needs

- User accounts with 'Administrator' and 'Viewer' roles.
- A set of test passwords that specifically pass or fail different combinations of policy rules.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- An E2E testing framework like Playwright or Cypress.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing.
- Code reviewed and approved by at least one other developer.
- Unit tests implemented for backend logic and frontend components, achieving >80% coverage.
- Integration tests completed for API endpoints and password validation logic.
- E2E test scenario for updating the policy and then testing its enforcement is implemented and passing.
- UI/UX for the configuration page has been reviewed and approved.
- Security review confirms that the API is secured and password history is stored correctly.
- Documentation for the password policy feature is added to the Administrator User Guide.
- Story deployed and verified in the staging environment.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- The backend work on the custom password validator can be complex and should be tackled early. The frontend form is more straightforward.
- Requires a database migration, which needs to be coordinated with deployment.

## 11.4.0.0 Release Impact

This is a core security feature expected in an enterprise product. Its absence would be a significant gap.

