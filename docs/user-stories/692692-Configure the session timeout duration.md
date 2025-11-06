# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-033 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Configure the session timeout duration |
| As A User Story | As an Administrator, I want to configure the syste... |
| User Persona | Administrator |
| Business Value | Enables alignment with corporate security policies... |
| Functional Area | System Administration & Security |
| Story Theme | User & Access Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Administrator successfully configures a valid session timeout

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an Administrator logged into the Control Panel and navigating to the 'System Settings' > 'Security' page

### 3.1.5 When

I enter a valid integer, '30', into the 'Session Inactivity Timeout (minutes)' input field and click the 'Save' button

### 3.1.6 Then

A success notification 'Settings saved successfully' is displayed, the page reloads or the field retains the value '30', and all subsequent user sessions will be invalidated after 30 minutes of inactivity.

### 3.1.7 Validation Notes

Verify by checking the configuration value in the database. Also, verify through an end-to-end test where a user is logged out after the specified duration.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

System enforces the newly configured timeout duration

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

the session inactivity timeout has been configured to 10 minutes

### 3.2.5 When

a logged-in user (any role) remains inactive for more than 10 minutes and then attempts to perform an action

### 3.2.6 Then

their session is expired, and they are redirected to the login page with a message indicating their session has timed out.

### 3.2.7 Validation Notes

This requires an E2E test. Set timeout to a low value (e.g., 1 minute), wait, and then attempt to navigate or perform an action.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Administrator attempts to save a non-numeric value

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am an Administrator on the 'Security' settings page

### 3.3.5 When

I enter 'fifteen' into the 'Session Inactivity Timeout' input field and attempt to save

### 3.3.6 Then

a validation error message, such as 'Please enter a whole number', is displayed next to the input field, and the configuration is not saved.

### 3.3.7 Validation Notes

Test with various non-numeric strings.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Administrator attempts to save an out-of-range value

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

I am an Administrator on the 'Security' settings page, and the allowed range is 5-120 minutes

### 3.4.5 When

I enter '121' into the 'Session Inactivity Timeout' input field and attempt to save

### 3.4.6 Then

a validation error message, such as 'Value must be between 5 and 120', is displayed, and the configuration is not saved.

### 3.4.7 Validation Notes

Test with values below the minimum (e.g., 4, 0, -1) and above the maximum (e.g., 121).

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

A non-administrator user attempts to access the security settings page

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am a user with the 'Viewer' role and am logged in

### 3.5.5 When

I attempt to navigate directly to the URL for the 'Security' settings page

### 3.5.6 Then

I am shown a '403 Forbidden' or 'Access Denied' page and cannot view or change the settings.

### 3.5.7 Validation Notes

Verify that the navigation link to this page is not visible to non-admin users and that direct URL access is blocked.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Configuration change is recorded in the audit log

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

I am an Administrator on the 'Security' settings page

### 3.6.5 When

I successfully change the session timeout value from '15' to '30' and save it

### 3.6.6 Then

a new entry is created in the system's audit log recording the timestamp, my username, the setting that was changed ('Session Timeout'), the old value ('15'), and the new value ('30').

### 3.6.7 Validation Notes

After changing the setting, navigate to the Audit Log viewer and verify the new log entry exists and is accurate.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A numerical input field labeled 'Session Inactivity Timeout (minutes)' within the Control Panel's 'System Settings' > 'Security' section.
- Helper text below the input field stating: 'Enter the number of minutes of inactivity before a user is automatically logged out. Must be between 5 and 120.'
- A 'Save' button to persist the changes.
- A non-modal success notification (e.g., toast message) on successful save.
- Inline validation error messages for the input field.

## 4.2.0 User Interactions

- The 'Save' button should be disabled by default and only become enabled when a value on the page has been changed.
- Entering invalid data and attempting to save should prevent the API call and display an error.
- The system should use the default value of 15 minutes if no value has ever been configured.

## 4.3.0 Display Requirements

- The input field must be pre-populated with the currently configured value or the system default (15) if not yet set.

## 4.4.0 Accessibility Needs

- The input field must have a correctly associated `<label>`.
- Helper text and validation error messages must be associated with the input using `aria-describedby` to be accessible to screen readers.
- All UI elements must be keyboard navigable and operable.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

The session timeout duration must be an integer between 5 and 120, inclusive.

### 5.1.3 Enforcement Point

Both client-side (UI validation) and server-side (API endpoint validation).

### 5.1.4 Violation Handling

The request to save the configuration is rejected with a 400 Bad Request status and a descriptive error message.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Only users with the 'Administrator' role can view or modify the session timeout configuration.

### 5.2.3 Enforcement Point

Server-side API endpoint authorization.

### 5.2.4 Violation Handling

The request is rejected with a 403 Forbidden status.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-032

#### 6.1.1.2 Dependency Reason

This story makes the timeout value from US-032 configurable. The base mechanism for session timeout must exist first.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-019

#### 6.1.2.2 Dependency Reason

Requires the existence of an 'Administrator' role to properly secure the configuration endpoint.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-101

#### 6.1.3.2 Dependency Reason

Requires the audit logging framework to be in place to record changes to this security-sensitive setting as per AC-006.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity for session and cookie management.
- A centralized application configuration service/database table to store the setting.
- Frontend state management to handle form state and validation.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- Saving the configuration should complete in under 500ms.
- The session validation logic should add negligible overhead (<1ms) to each authenticated API request.

## 7.2.0.0 Security

- The API endpoint for updating this setting must be protected and require Administrator role.
- The change must be logged in a tamper-evident audit trail.
- The system must prevent Cross-Site Request Forgery (CSRF) on the settings form.

## 7.3.0.0 Usability

- The setting's purpose and constraints should be clearly explained in the UI to prevent user error.

## 7.4.0.0 Accessibility

- The settings form must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The primary complexity lies in the backend implementation. ASP.NET Core authentication options are typically configured at application startup. The system will need a mechanism (e.g., custom middleware, a custom `IConfigureOptions` implementation, or a custom `ITicketStore`) to dynamically read the timeout value from the database and apply it to each user's session cookie.
- Ensuring the change applies to currently active sessions without forcing a log-out for everyone immediately requires careful design.

## 8.3.0.0 Technical Risks

- Incorrectly implementing the dynamic configuration in the authentication pipeline could lead to security vulnerabilities or cause sessions to not expire correctly.

## 8.4.0.0 Integration Points

- Backend: ASP.NET Core's authentication middleware.
- Backend: The application's configuration storage (SQLite database).
- Backend: The audit logging service.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify an admin can set the value.
- Verify a viewer cannot access the settings page or API.
- Verify invalid inputs (string, float, negative, zero, out-of-range) are rejected by the UI and API.
- Crucially, an E2E test must confirm that a session actually expires after the configured time of inactivity.

## 9.3.0.0 Test Data Needs

- An active Administrator user account.
- An active Viewer user account.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- Cypress or Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for backend and frontend with >80% coverage
- Integration testing for API endpoint and database persistence completed successfully
- E2E test case for session timeout enforcement is implemented and passing
- User interface reviewed and approved for usability and accessibility (WCAG 2.1 AA)
- Security requirements validated, including endpoint protection and audit logging
- Administrator Guide documentation is updated to explain this new setting
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- The backend implementation is more complex than the frontend. Allocate sufficient time for researching and testing the integration with the ASP.NET Core authentication pipeline.
- Ensure prerequisite stories (US-032, US-101) are completed before starting development.

## 11.4.0.0 Release Impact

This is a significant security enhancement and a key feature for enterprise customers with strict compliance needs.

