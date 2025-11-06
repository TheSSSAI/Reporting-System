# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-106 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Configure the system logging level at runtime |
| As A User Story | As an Administrator, I want to change the system's... |
| User Persona | Administrator |
| Business Value | Enables rapid, on-demand system diagnostics withou... |
| Functional Area | System Administration & Maintenance |
| Story Theme | System Operations & Monitoring |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Administrator successfully changes the logging level

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an Administrator logged into the Control Panel and I have navigated to the 'System Settings' page

### 3.1.5 When

I select a new logging level, for example 'Debug', from the logging level selector and click the 'Save' button

### 3.1.6 Then

A success notification is displayed, the UI updates to show 'Debug' as the current logging level, and the system immediately begins writing log entries at the 'Debug' verbosity level without a service restart.

### 3.1.7 Validation Notes

Verify by checking the system's rolling log file. After the change, trigger an action that logs a debug message and confirm it appears in the log. Before the change, it should not.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

The change in logging level is recorded in the audit log

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

An Administrator has successfully changed the logging level from 'Information' to 'Debug'

### 3.2.5 When

Another Administrator views the system's Audit Log

### 3.2.6 Then

A new audit log entry is present that records the user who made the change, the timestamp, the setting that was changed ('Logging Level'), the previous value ('Information'), and the new value ('Debug').

### 3.2.7 Validation Notes

Requires US-101 (View the system audit log) to be implemented. Check the audit log table or UI for the specific entry.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

A non-administrator user cannot access the logging level setting

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am a user with the 'Viewer' role and I am logged into the system

### 3.3.5 When

I attempt to navigate to the 'System Settings' page or access the logging configuration endpoint directly

### 3.3.6 Then

I am shown an 'Access Denied' error or redirected, and I am unable to view or change the logging level.

### 3.3.7 Validation Notes

Test by logging in as a 'Viewer' and attempting to access the URL for the settings page. Also, test the API endpoint with a 'Viewer' JWT.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

System provides feedback on a failed attempt to save the logging level

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am an Administrator on the 'System Settings' page, and the system's configuration file is temporarily locked or read-only

### 3.4.5 When

I attempt to change the logging level and click 'Save'

### 3.4.6 Then

A user-friendly error message is displayed indicating the change could not be saved, and the logging level displayed in the UI remains unchanged.

### 3.4.7 Validation Notes

This can be simulated by changing file permissions on the configuration store during the test.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

The logging level selector displays all standard levels

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

I am an Administrator on the 'System Settings' page

### 3.5.5 When

I view the logging level configuration control

### 3.5.6 Then

The selector contains the options: 'Verbose', 'Debug', 'Information', 'Warning', 'Error', and 'Fatal', and it correctly indicates the current active level.

### 3.5.7 Validation Notes

Inspect the UI to confirm all options are present and the current setting is pre-selected.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated 'Logging' section within the 'System Settings' page of the Control Panel.
- A dropdown menu or radio button group labeled 'Logging Level' to select the desired verbosity.
- A 'Save' button to apply the changes.
- A non-editable text display showing the current active logging level.
- Toast notifications for success and error messages.

## 4.2.0 User Interactions

- User selects a new level from the control.
- User clicks 'Save' to persist the change.
- The UI should provide immediate feedback (e.g., loading spinner on the save button) during the save operation to prevent multiple clicks.

## 4.3.0 Display Requirements

- The currently configured logging level must always be visible and accurate on the settings page.

## 4.4.0 Accessibility Needs

- All UI controls (label, dropdown, button) must be WCAG 2.1 AA compliant, including proper ARIA attributes and keyboard navigability.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Only users with the 'Administrator' role can modify the system's logging level.

### 5.1.3 Enforcement Point

Backend API endpoint for updating the configuration.

### 5.1.4 Violation Handling

The API will return an HTTP 403 Forbidden status code.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Changes to the logging level must take effect immediately without requiring a service restart.

### 5.2.3 Enforcement Point

The backend service logic that handles the configuration update.

### 5.2.4 Violation Handling

This is a core functional requirement; failure to meet this constitutes a bug.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-019

#### 6.1.1.2 Dependency Reason

Requires the existence of the 'Administrator' role for access control.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-101

#### 6.1.2.2 Dependency Reason

Requires the audit logging framework to be in place to record the configuration change event.

## 6.2.0.0 Technical Dependencies

- The backend must use the Serilog logging library, as specified in the SRS, and be configured with a `LoggingLevelSwitch` to allow for runtime changes.
- The ASP.NET Core backend must have a secure API endpoint for updating the setting.
- The React frontend must have a 'System Settings' area in the Control Panel where this UI can be placed.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to update the logging level should complete in under 500ms.
- The change must be reflected in the logging system instantly, with no delay.

## 7.2.0.0 Security

- Access to the feature (both UI and API) must be strictly limited to users in the 'Administrator' role.
- The action of changing the log level must be recorded in a tamper-evident audit log, including the user, timestamp, and the old/new values.

## 7.3.0.0 Usability

- The setting should be easy to find for an administrator within a clearly marked 'System Settings' or 'Maintenance' section.
- The system must provide clear and immediate feedback on the success or failure of the action.

## 7.4.0.0 Accessibility

- The UI must adhere to WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly in all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- The chosen logging framework (Serilog) has built-in support for this functionality via `LoggingLevelSwitch`, simplifying the backend implementation.
- Frontend work is standard: creating a settings component, fetching state, and posting an update.
- Integration with the existing audit log is required.

## 8.3.0.0 Technical Risks

- Low risk. The primary risk would be incorrect initial configuration of the Serilog `LoggingLevelSwitch`, which could prevent the runtime update from propagating correctly.

## 8.4.0.0 Integration Points

- ASP.NET Core Identity for role-based authorization.
- Serilog configuration and `LoggingLevelSwitch`.
- System Audit Log service.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify an Administrator can change the level from 'Information' to 'Debug' and see debug logs appear.
- Verify an Administrator can change the level from 'Debug' to 'Warning' and see debug logs disappear.
- Verify a 'Viewer' user receives a 403 error when attempting to call the configuration update API.
- Verify that an audit log is created with the correct details after a successful change.
- Verify the UI correctly reflects the current state before and after a change.

## 9.3.0.0 Test Data Needs

- User accounts with 'Administrator' and 'Viewer' roles.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- A test harness capable of making API calls and inspecting log file output.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for backend and frontend, achieving >80% coverage on new code, and passing
- Integration testing completed successfully, confirming log output changes as expected
- User interface reviewed for usability and consistency with the design system
- Security requirements (RBAC and auditing) validated
- Documentation for system administration updated to include this feature
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🟡 Medium

## 11.3.0.0 Sprint Considerations

- This story is valuable for improving maintainability and should be prioritized after core features are stable. It depends on the audit log framework (US-101) being completed.

## 11.4.0.0 Release Impact

Improves the supportability of the product post-release. No direct impact on end-user report generation features.

