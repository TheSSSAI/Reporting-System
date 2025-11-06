# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-108 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Configure system health monitoring thresholds for ... |
| As A User Story | As an Administrator, I want to configure warning t... |
| User Persona | Administrator: A technical user responsible for th... |
| Business Value | Increases system reliability and uptime by enablin... |
| Functional Area | System Administration & Monitoring |
| Story Theme | System Health and Reliability |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Administrator successfully configures and saves valid health thresholds

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an Administrator logged into the Control Panel and have navigated to the 'System Settings > Monitoring' page

### 3.1.5 When

I enter '90' for the CPU usage threshold (%), '5' for the breach duration (minutes), and click the 'Save' button

### 3.1.6 Then

a success notification is displayed, the settings are persisted to the configuration database, and when I refresh the page, the values '90' and '5' are correctly displayed in their respective fields.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

System prevents saving of invalid non-numeric input

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

I am an Administrator on the 'System Settings > Monitoring' page

### 3.2.5 When

I enter 'abc' into the 'Memory Usage Threshold' field and attempt to save

### 3.2.6 Then

a validation error message is displayed next to the field stating 'Input must be a valid number', and the settings are not saved.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

System prevents saving of out-of-range percentage values

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am an Administrator on the 'System Settings > Monitoring' page

### 3.3.5 When

I enter '101' into the 'CPU Usage Threshold' field and attempt to save

### 3.3.6 Then

a validation error message is displayed next to the field stating 'Value must be between 0 and 100', and the settings are not saved.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

System prevents saving of non-positive duration values

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am an Administrator on the 'System Settings > Monitoring' page

### 3.4.5 When

I enter '0' into the 'Breach Duration' field for any metric and attempt to save

### 3.4.6 Then

a validation error message is displayed next to the field stating 'Duration must be greater than 0', and the settings are not saved.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Non-Administrator roles are denied access to the monitoring configuration page

### 3.5.3 Scenario Type

Security

### 3.5.4 Given

I am a user with the 'Viewer' role and I am logged into the system

### 3.5.5 When

I attempt to navigate directly to the '/settings/monitoring' URL

### 3.5.6 Then

I am redirected to an access denied page or the main dashboard, and I cannot view or modify the settings.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Monitoring settings page loads with sensible default values on a fresh installation

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

the system has been newly installed and no monitoring settings have been saved before

### 3.6.5 When

an Administrator navigates to the 'System Settings > Monitoring' page for the first time

### 3.6.6 Then

the fields are pre-populated with default values: CPU Threshold=95%, Duration=5min; Memory Threshold=90%, Duration=5min; Disk Space Threshold=10%, Duration=1min.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Backend monitoring service correctly uses the configured values

### 3.7.3 Scenario Type

Integration

### 3.7.4 Given

the CPU threshold is configured to 90% for a duration of 5 minutes

### 3.7.5 When

the backend health monitoring service performs its periodic check

### 3.7.6 Then

the service compares the actual host CPU usage against the configured value of 90% from the database.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A new navigation item 'Monitoring' under 'System Settings' in the Control Panel sidebar.
- A dedicated page for 'System Health Monitoring'.
- Three distinct sections for 'CPU Usage', 'Memory Usage', and 'Available Disk Space'.
- For each section: a numeric input field for the threshold value (%), and a numeric input field for the breach duration (minutes).
- A single 'Save' button for the page, which should be disabled by default and enabled only when changes are made.
- Tooltips or helper text next to each field explaining its purpose (e.g., 'Alert if CPU usage is above this value for the specified duration').
- Toast notifications for success and error messages upon saving.

## 4.2.0 User Interactions

- User enters numeric values into the input fields.
- Inline validation messages appear immediately if the user enters invalid data (e.g., text, out-of-range numbers).
- User clicks 'Save' to persist all settings on the page.
- User receives clear feedback on the result of the save operation.

## 4.3.0 Display Requirements

- Labels for input fields must clearly state the unit (e.g., '%', 'minutes').
- The page must correctly display the currently saved values when loaded.

## 4.4.0 Accessibility Needs

- All form inputs must have corresponding `<label>` tags.
- Validation error messages must be programmatically linked to their respective inputs using `aria-describedby`.
- The page must be fully navigable and operable using only a keyboard.
- All UI elements must meet WCAG 2.1 AA contrast ratio standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Health metric thresholds must be a percentage between 0 and 100, inclusive.

### 5.1.3 Enforcement Point

Client-side UI validation and server-side API validation.

### 5.1.4 Violation Handling

Display a user-friendly error message and reject the save request with an HTTP 400 Bad Request response.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The breach duration for any threshold must be a positive integer greater than 0, representing minutes.

### 5.2.3 Enforcement Point

Client-side UI validation and server-side API validation.

### 5.2.4 Violation Handling

Display a user-friendly error message and reject the save request with an HTTP 400 Bad Request response.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

Only users with the 'Administrator' role can access or modify system health monitoring settings.

### 5.3.3 Enforcement Point

Backend API endpoint authorization middleware.

### 5.3.4 Violation Handling

Return an HTTP 403 Forbidden response for API calls. Redirect to an access denied page for UI navigation attempts.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

- {'story_id': 'US-019', 'dependency_reason': "The Role-Based Access Control (RBAC) system with a defined 'Administrator' role must exist to secure the configuration endpoint."}

## 6.2.0 Technical Dependencies

- ASP.NET Core Identity for user roles and authentication.
- Entity Framework Core and SQLite for persisting configuration settings.
- A .NET library/API for querying host system metrics on Windows (e.g., System.Diagnostics.PerformanceCounter).
- React and MUI v5 for the frontend UI components.

## 6.3.0 Data Dependencies

- A new database table is required to store the monitoring configuration settings. An EF Core migration must be created.

## 6.4.0 External Dependencies

*No items available*

# 7.0.0 Non Functional Requirements

## 7.1.0 Performance

- The backend health monitoring service must have a low performance overhead, consuming negligible CPU and memory during its checks.
- The check interval for the monitoring service should be configurable, defaulting to 60 seconds.
- The API response time for saving settings must be under 200ms.

## 7.2.0 Security

- The API endpoint for updating settings must be protected and require Administrator role.
- All user input must be sanitized on the server-side to prevent any potential vulnerabilities, even if numeric.

## 7.3.0 Usability

- The purpose of each setting should be immediately clear through labels and helper text.
- The system should provide clear and immediate feedback for user actions (e.g., saving, errors).

## 7.4.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0 Compatibility

- The UI must render correctly on the latest stable versions of Chrome, Firefox, and Edge.

# 8.0.0 Implementation Considerations

## 8.1.0 Complexity Assessment

Medium

## 8.2.0 Complexity Factors

- Requires creation of a new, robust background service (IHostedService) in the .NET backend to perform periodic checks.
- The background service needs to manage state to track the duration of threshold breaches.
- Requires platform-specific code to query Windows performance counters for CPU, memory, and disk.
- Involves frontend (new React page/component), backend (new API controller and service), and database (new EF Core model and migration) work.

## 8.3.0 Technical Risks

- Querying performance counters can sometimes have permission issues depending on the user account the Windows service is running as. This needs to be tested during installation.
- The logic for tracking breach duration must be resilient to service restarts.

## 8.4.0 Integration Points

- The new background service will read from the SQLite configuration database.
- The React frontend will call a new ASP.NET Core API endpoint to GET and POST settings.

# 9.0.0 Testing Requirements

## 9.1.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0 Test Scenarios

- Verify that valid settings can be saved and retrieved.
- Verify that various types of invalid input are rejected by both UI and API.
- Verify that a non-admin user cannot access the page or the API endpoint.
- Verify that the background service starts on application startup and logs its periodic checks.
- Mock system metric providers to test the breach detection logic in unit tests (e.g., metric crosses threshold, stays for duration, drops below).

## 9.3.0 Test Data Needs

- User accounts with 'Administrator' and 'Viewer' roles.

## 9.4.0 Testing Tools

- xUnit and Moq for backend unit tests.
- Jest and React Testing Library for frontend unit tests.
- A UI automation tool (e.g., Playwright) for E2E tests.
- A CPU/memory stress testing tool for manual verification of the monitoring service.

# 10.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for new backend and frontend code, achieving >= 80% coverage
- Integration testing for the API endpoint and database persistence completed successfully
- User interface reviewed for usability and adherence to design standards
- Security requirements (RBAC) validated
- Accessibility requirements (WCAG 2.1 AA) validated
- The Administrator Guide documentation is updated with a section explaining how to configure system health monitoring
- Story deployed and verified in the staging environment

# 11.0.0 Planning Information

## 11.1.0 Story Points

5

## 11.2.0 Priority

🔴 High

## 11.3.0 Sprint Considerations

- This story is a foundational requirement for the alerting feature (US-109) and must be completed first.
- Requires a developer with skills across the full stack: .NET background services, ASP.NET Core API, EF Core, and React.
- The team should allocate time to research and test the reliability of reading Windows performance counters under the service's security context.

## 11.4.0 Release Impact

Enables a key reliability and monitoring feature set for the application. Essential for production-ready deployments.

