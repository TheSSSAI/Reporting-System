# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-107 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Configure the log file retention period |
| As A User Story | As an Administrator, I want to configure the reten... |
| User Persona | Administrator |
| Business Value | Prevents service outages caused by insufficient di... |
| Functional Area | System Administration & Operations |
| Story Theme | System Logging and Monitoring |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Administrator successfully views and updates the log retention period

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am logged in as an Administrator and have navigated to the 'System Settings' page in the Control Panel

### 3.1.5 When

I enter the value '90' into the 'Log File Retention Period (days)' input field and click the 'Save' button

### 3.1.6 Then

A success notification is displayed, the new value of '90' is persisted, and if I refresh the page, the input field shows '90'.

### 3.1.7 Validation Notes

Verify via UI inspection and by checking the configuration value in the SQLite database.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

System uses the default retention period when none is configured

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The system is a fresh installation and the log retention period has never been explicitly set

### 3.2.5 When

I navigate to the 'System Settings' page

### 3.2.6 Then

The 'Log File Retention Period (days)' input field is pre-populated with the system default value of '30'.

### 3.2.7 Validation Notes

Verify the default value is displayed in the UI on a clean installation.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

System rejects non-numeric input for retention period

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am logged in as an Administrator on the 'System Settings' page

### 3.3.5 When

I enter 'thirty' into the 'Log File Retention Period (days)' input field and attempt to save

### 3.3.6 Then

A validation error message is displayed next to the field, and the configuration is not saved.

### 3.3.7 Validation Notes

The save button might be disabled, or a validation message like 'Please enter a valid number' should appear upon clicking save.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

System rejects out-of-range values for retention period

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am logged in as an Administrator on the 'System Settings' page

### 3.4.5 When

I enter '0' or '-10' or '5000' into the 'Log File Retention Period (days)' input field and attempt to save

### 3.4.6 Then

A validation error message is displayed, such as 'Value must be between 1 and 3650', and the configuration is not saved.

### 3.4.7 Validation Notes

Test with values below the minimum (1) and above the maximum (3650).

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Non-administrator users cannot access the log retention setting

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am logged in as a user with the 'Viewer' role

### 3.5.5 When

I attempt to navigate directly to the URL for the 'System Settings' page

### 3.5.6 Then

I am shown an 'Access Denied' (403 Forbidden) page or redirected to the dashboard.

### 3.5.7 Validation Notes

Verify that both the UI navigation link is hidden and direct URL access is blocked for non-admin roles.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

System correctly purges old log files based on the configured retention period

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

The log retention period is configured to '7' days and the service is running

### 3.6.5 And

There are log files on the server's file system that are 8 or more days old

### 3.6.6 When

The system's daily log maintenance cycle occurs

### 3.6.7 Then

All log files older than 7 days are deleted from the file system, while files 7 days old or newer are retained.

### 3.6.8 Validation Notes

This requires manual or automated E2E testing. Create mock log files with past dates, set the configuration, restart the service, wait for the cleanup cycle (typically daily), and verify the file system state.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated 'System Settings' or 'Logging' section in the Control Panel.
- A numeric input field labeled 'Log File Retention Period (days)'.
- A 'Save' button to persist the setting.
- Helper text below the input field, e.g., 'Number of days to keep log files before automatic deletion. Recommended: 30-90. Range: 1-3650.'
- A toast or inline notification for success/error messages upon saving.

## 4.2.0 User Interactions

- The input field should only accept integer values.
- The 'Save' button should be disabled until a change is made to the value.
- Inline validation messages should appear immediately if the user enters an invalid value.

## 4.3.0 Display Requirements

- The current saved value (or system default) must be displayed when the page loads.

## 4.4.0 Accessibility Needs

- The input field must have a correctly associated `<label>`.
- Validation error messages must be programmatically associated with the input field for screen readers.
- All UI elements must be keyboard navigable.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-LOG-001

### 5.1.2 Rule Description

The log retention period must be a positive integer between 1 and 3650 (approximately 10 years), inclusive.

### 5.1.3 Enforcement Point

Both client-side (UI) and server-side (API) validation.

### 5.1.4 Violation Handling

The request to save the configuration is rejected with a user-friendly error message.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-LOG-002

### 5.2.2 Rule Description

If no retention period is explicitly configured by an administrator, a default value of 30 days must be used.

### 5.2.3 Enforcement Point

Application startup and logging service initialization.

### 5.2.4 Violation Handling

N/A - This is a default behavior.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-019

#### 6.1.1.2 Dependency Reason

Requires the existence of the 'Administrator' role for role-based access control (RBAC).

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-070

#### 6.1.2.2 Dependency Reason

Assumes a Control Panel with a navigation structure where a 'System Settings' page can be added.

## 6.2.0.0 Technical Dependencies

- Serilog: The implementation will directly configure the Serilog file sink.
- ASP.NET Core Identity: Required for securing the API endpoint to ensure only Administrators can change the setting.
- Entity Framework Core & SQLite: The configuration value must be stored in and retrieved from the application's SQLite database.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The log file cleanup process must run as a low-priority background task that does not noticeably impact the performance of core application functions like report generation or API responsiveness.

## 7.2.0.0 Security

- The API endpoint for updating the log retention setting must be protected and only accessible by authenticated users with the 'Administrator' role.
- The setting should be treated as configuration data and protected by the at-rest encryption of the SQLite database.

## 7.3.0.0 Usability

- The setting should be easy to find for an Administrator, and the purpose of the setting should be clearly explained in the UI.

## 7.4.0.0 Accessibility

- The settings page and its form elements must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The UI must function correctly on all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The primary complexity lies in applying the database-stored configuration value to the Serilog logger, which is initialized very early in the application's startup pipeline. A robust mechanism is needed to read from the database at startup or to reconfigure the logger dynamically if the setting is changed while the service is running.
- Requires changes across the full stack: Frontend (React component), Backend (API endpoint), and Core Application Logic (Serilog configuration).

## 8.3.0.0 Technical Risks

- Potential race conditions or complexity if attempting to reconfigure the logger on-the-fly without a service restart. The simplest approach is to apply the setting on service start, which should be documented.

## 8.4.0.0 Integration Points

- The setting value from the database must be integrated into the `Program.cs` or a dedicated logging configuration service.
- The Serilog.Sinks.File `retainedFileCountLimit` or a similar property will be the direct consumer of this setting.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify UI validation for various invalid inputs (text, negative, zero, large numbers).
- Verify API endpoint rejects requests from non-admin users.
- Verify the configuration value is correctly saved to and retrieved from the database.
- Manually verify file system behavior: create old log files, set a short retention period, restart the service, and confirm the old files are deleted after the cleanup interval.

## 9.3.0.0 Test Data Needs

- User accounts with 'Administrator' and 'Viewer' roles.
- Mock log files with timestamps spanning more than the default retention period.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- A browser automation tool (e.g., Playwright) for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for both frontend and backend, achieving >80% coverage for new code
- Integration testing completed successfully for the API endpoint and database persistence
- E2E test scenario for file deletion has been manually or automatically verified
- User interface reviewed and approved for usability and accessibility
- Security requirements validated (endpoint is protected)
- Documentation for this setting is added to the Administration Guide
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational operational feature crucial for long-term system stability. It should be prioritized before the system is deployed to a production environment where it will run for extended periods.
- The implementation team needs to agree on the strategy for applying the configuration to Serilog (e.g., on startup vs. dynamic reconfiguration).

## 11.4.0.0 Release Impact

- Improves the operational readiness and maintainability of the product.

