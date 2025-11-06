# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-006 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Activate the system with a valid license key |
| As A User Story | As an Administrator, I want to enter a cloud-issue... |
| User Persona | Administrator |
| Business Value | Enables the conversion of a trial user to a paid c... |
| Functional Area | System Administration & Licensing |
| Story Theme | System Licensing & Activation |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful activation with a valid key

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

The system is operating in Trial Mode and the Administrator is logged into the Control Panel and has navigated to the Licensing page

### 3.1.5 When

The Administrator enters a valid, unexpired activation key and clicks the 'Activate' button

### 3.1.6 Then

The system must make a secure HTTPS call to the cloud licensing service, receive a success response, update its internal state to 'Active', display a success message like 'System successfully activated', and remove the 'Trial Mode' indicator from the UI.

### 3.1.7 Validation Notes

Verify that after activation, the user can create more than 3 connectors and schedules, and that newly generated reports do not have a watermark.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Activation attempt with an invalid key format

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

The Administrator is on the Licensing page

### 3.2.5 When

The Administrator enters a key that is syntactically incorrect or does not exist in the licensing service and clicks 'Activate'

### 3.2.6 Then

The system must display a specific error message, such as 'Invalid activation key. Please check the key and try again.', and the system must remain in Trial Mode.

### 3.2.7 Validation Notes

The call to the licensing service should return a specific error code for 'not found' or 'invalid'. The system state should not change.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Activation attempt with an expired or revoked key

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

The Administrator is on the Licensing page

### 3.3.5 When

The Administrator enters a key that is valid in format but has been marked as expired or revoked in the licensing service and clicks 'Activate'

### 3.3.6 Then

The system must display a specific error message, such as 'This activation key has expired or been revoked. Please contact support.', and the system must remain in Trial Mode.

### 3.3.7 Validation Notes

The licensing service should return a specific error code for 'expired' or 'revoked'. The system state must not change.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Activation attempt with no network connectivity

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

The Administrator is on the Licensing page and the host server has no outbound internet access

### 3.4.5 When

The Administrator enters a valid key and clicks 'Activate'

### 3.4.6 Then

The system must attempt to contact the licensing service, fail after a reasonable timeout (e.g., 15 seconds) with retries, display a network-related error message like 'Could not connect to the licensing service. Please check your internet connection and firewall settings.', and remain in Trial Mode.

### 3.4.7 Validation Notes

Verify that the Polly resiliency policy (retry with backoff) is triggered. The system state must not change.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Activation attempt with an empty key

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

The Administrator is on the Licensing page

### 3.5.5 When

The Administrator clicks the 'Activate' button without entering any text into the key input field

### 3.5.6 Then

The system must display a client-side validation message, such as 'Activation key cannot be empty.', and must not make any network call to the licensing service.

### 3.5.7 Validation Notes

The 'Activate' button should ideally be disabled until the input field is non-empty. Check the browser's network tab to ensure no API call is made.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

UI feedback during activation process

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

The Administrator is on the Licensing page

### 3.6.5 When

The Administrator enters a key and clicks 'Activate'

### 3.6.6 Then

The 'Activate' button must be disabled and show a loading indicator (e.g., a spinner) to prevent multiple submissions while the validation is in progress.

### 3.6.7 Validation Notes

Observe the UI state immediately after clicking the button and before the response is received.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated 'Licensing' section in the Control Panel, likely under 'System Settings'.
- A text input field designed for pasting license keys.
- An 'Activate' button.
- A display area for the current license status (e.g., 'Trial Mode', 'Active').
- UI components for feedback messages (e.g., toast notifications or alert boxes) for success and error states.

## 4.2.0 User Interactions

- User enters or pastes the key into the input field.
- The 'Activate' button is enabled only when the input field is not empty.
- Clicking 'Activate' triggers the validation process and provides visual feedback (loading state).
- The system displays a clear, dismissible message indicating the outcome.

## 4.3.0 Display Requirements

- The current license status must be clearly visible on the page.
- Error messages must be specific and guide the user toward a resolution where possible.

## 4.4.0 Accessibility Needs

- The input field must have a proper label.
- All buttons and interactive elements must be keyboard-accessible.
- Feedback messages (success/error) must be announced by screen readers using ARIA live regions.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A valid license key is required to move the system from Trial Mode to an Active state.

### 5.1.3 Enforcement Point

During the activation attempt via the Control Panel.

### 5.1.4 Violation Handling

The system remains in Trial Mode and displays an appropriate error message.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The activation key must be validated against the central, cloud-based licensing service.

### 5.2.3 Enforcement Point

When the 'Activate' button is clicked.

### 5.2.4 Violation Handling

If the service is unreachable or rejects the key, the activation fails.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-003

#### 6.1.1.2 Dependency Reason

An Administrator account must exist to log in and access the Control Panel.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-009

#### 6.1.2.2 Dependency Reason

This story defines the initial 'Trial Mode' state and UI indicator that will be changed upon successful activation.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-027

#### 6.1.3.2 Dependency Reason

The Administrator must be able to log in to perform this action.

## 6.2.0.0 Technical Dependencies

- A defined and stable API contract for the external cloud-based licensing service.
- A mechanism within the application to store and manage the current license state (e.g., in the encrypted SQLite database).
- The Polly library for implementing resilient HTTP requests (retry, timeout).

## 6.3.0.0 Data Dependencies

- A set of valid, invalid, and expired test keys must be available from the team managing the licensing service for testing purposes.

## 6.4.0.0 External Dependencies

- The cloud-based licensing service must be operational and accessible from the development and testing environments.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The activation request to the cloud service, including retries, should not exceed a total of 15 seconds.
- The UI should remain responsive during the activation call.

## 7.2.0.0 Security

- All communication with the cloud licensing service must be encrypted using HTTPS with TLS 1.2 or higher.
- The activation key, if stored locally after validation, must be encrypted at rest in the SQLite database using DPAPI.

## 7.3.0.0 Usability

- The activation process should be self-explanatory, requiring no documentation for the user to complete.
- Error messages should be clear, concise, and user-friendly.

## 7.4.0.0 Accessibility

- The licensing UI must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Dependency on an external service, requiring robust error handling for network failures and various API responses.
- Requires a reliable internal state management system to ensure that once activated, all feature flags and limitations are correctly updated across the application.
- Security considerations for handling and storing the license key.

## 8.3.0.0 Technical Risks

- The external licensing service API is unavailable or its contract changes, blocking development or testing.
- Firewall or proxy issues at customer sites preventing the on-premise service from reaching the cloud licensing endpoint.

## 8.4.0.0 Integration Points

- Backend: A new API endpoint in the ASP.NET Core application to handle the activation request from the frontend.
- Backend: A service class responsible for communicating with the external cloud licensing API.
- Backend: The application's central configuration or state service that holds the current license status.
- Frontend: A new React component for the Licensing page.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify successful activation with a valid key.
- Verify failure with an invalid key.
- Verify failure with an expired key.
- Verify failure when the licensing service is unreachable (mocked).
- Verify UI behavior (loading states, feedback messages) for all outcomes.
- Verify that after activation, trial limitations are removed.

## 9.3.0.0 Test Data Needs

- A valid, active test license key.
- An invalid/non-existent test license key.
- An expired/revoked test license key.

## 9.4.0.0 Testing Tools

- xUnit and Moq for backend unit tests.
- Jest and React Testing Library for frontend unit tests.
- A tool like Postman or an integration test suite to test against a staging version of the licensing API.
- An E2E testing framework like Playwright or Cypress.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >80% coverage for new code
- Integration testing against a staging licensing service completed successfully
- User interface reviewed and approved by UX/Product Owner
- Performance requirements verified
- Security requirements validated (HTTPS communication, data at rest encryption)
- Documentation for the licensing page updated in the Administrator Guide
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- Access to a stable, staging environment of the cloud licensing service is a hard dependency and must be available before the sprint starts.
- The API contract (request/response schemas, endpoints) for the licensing service must be finalized and documented.

## 11.4.0.0 Release Impact

This is a critical feature for the initial release, as it is the primary mechanism for monetization and enabling full product functionality for customers.

