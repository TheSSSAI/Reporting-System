# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-008 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Manually re-validate the current license key |
| As A User Story | As an Administrator, I want to manually trigger a ... |
| User Persona | Administrator |
| Business Value | Empowers administrators to self-diagnose and resol... |
| Functional Area | System Administration & Licensing |
| Story Theme | System Licensing & Activation |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful re-validation of an active license

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

An Administrator is logged into the Control Panel and is viewing the licensing page, and the system has a valid, active license key configured

### 3.1.5 When

the Administrator clicks the 'Re-validate License' button

### 3.1.6 Then

the UI must disable the button and display a 'Validating...' status, the system must make a secure call to the cloud licensing service, and upon receiving a successful response, the UI must display a success message (e.g., 'License successfully validated. Status: Active.') and re-enable the button.

### 3.1.7 Validation Notes

Verify via UI feedback and by checking system logs for the successful API call to the licensing service.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Successful re-validation resolves a Grace Period status

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

An Administrator is logged into the Control Panel, the system is in a 'Grace Period' due to a previous validation failure, and the underlying issue (e.g., network connectivity) has been resolved

### 3.2.5 When

the Administrator clicks the 'Re-validate License' button

### 3.2.6 Then

the system successfully validates the key with the licensing service, the system's status must change from 'Grace Period' to 'Active', and any 'Grace Period' notifications in the UI must be removed.

### 3.2.7 Validation Notes

Verify the UI notification for the Grace Period disappears and the system status updates to 'Active'.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Re-validation fails due to an invalid or expired key

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

An Administrator is logged into the Control Panel and the system has a license key that is now invalid or expired

### 3.3.5 When

the Administrator clicks the 'Re-validate License' button

### 3.3.6 Then

the system receives a failure response from the licensing service, the UI must display a specific error message (e.g., 'License validation failed: The provided key is invalid or has expired.'), and the system must revert to 'Trial Mode'.

### 3.3.7 Validation Notes

Verify the specific error message is shown and the system's behavior reflects 'Trial Mode' limitations (e.g., trial watermark appears).

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Re-validation fails due to a network error

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

An Administrator is logged into the Control Panel and the system cannot connect to the cloud licensing service (e.g., no internet, firewall block)

### 3.4.5 When

the Administrator clicks the 'Re-validate License' button

### 3.4.6 Then

the system's API call to the licensing service must time out or fail, the UI must display a specific network-related error message (e.g., 'Could not connect to the licensing service. Please check your network connection and firewall settings.'), and the system's current license state must remain unchanged.

### 3.4.7 Validation Notes

Simulate a network failure by blocking the licensing service URL. Verify the correct error message is displayed and the system status does not change.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Button is disabled during validation process

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

An Administrator is on the licensing page

### 3.5.5 When

the Administrator clicks the 'Re-validate License' button

### 3.5.6 Then

the button must be immediately disabled and must remain disabled until the validation attempt is complete (either success or failure), at which point it must be re-enabled.

### 3.5.7 Validation Notes

Attempt to click the button multiple times in quick succession. Verify only one API call is made and the button is visually disabled.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A 'Re-validate License' button on the system's licensing configuration page.
- A text area to display the current license status (e.g., 'Active', 'Grace Period', 'Trial Mode').
- A notification area (e.g., toast or inline message) to display success or error feedback from the validation attempt.

## 4.2.0 User Interactions

- Clicking the 'Re-validate License' button triggers the validation process.
- The button should provide visual feedback when clicked and while the process is running (e.g., show a spinner, change text to 'Validating...').
- The button must be disabled during the validation request to prevent multiple submissions.

## 4.3.0 Display Requirements

- The current license status must be clearly visible.
- Feedback messages must be user-friendly and distinguish between different failure types (invalid key vs. network error).

## 4.4.0 Accessibility Needs

- The button must have a clear, accessible name (e.g., `aria-label='Re-validate license key'`).
- Loading states and feedback messages must be announced by screen readers using ARIA live regions.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'A manual re-validation attempt must not change the timing of the next scheduled automatic periodic validation.', 'enforcement_point': 'Backend license validation service', 'violation_handling': 'The manual check is an ad-hoc action and should not reset or alter the 30-day automatic validation timer.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-006

#### 6.1.1.2 Dependency Reason

This story establishes the core functionality for activating a key and communicating with the licensing service, which is reused here.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-016

#### 6.1.2.2 Dependency Reason

This story defines the 'Grace Period' state, which this story's functionality can resolve.

## 6.2.0.0 Technical Dependencies

- Backend API endpoint to handle the re-validation request.
- Frontend state management (Zustand) to handle UI state during the async operation.
- Secure HTTP client for communication with the external licensing service.

## 6.3.0.0 Data Dependencies

- The currently stored license key within the system's configuration database (SQLite).

## 6.4.0.0 External Dependencies

- The cloud-based licensing service must be available and its API contract must be stable.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to the external licensing service must have a client-side timeout of 15 seconds to prevent a poor user experience.

## 7.2.0.0 Security

- All communication with the cloud licensing service must be encrypted using HTTPS/TLS 1.2+.
- The action must be restricted to users with the 'Administrator' role.

## 7.3.0.0 Usability

- Feedback messages must be clear, concise, and provide actionable advice where possible (e.g., 'check network connection').

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Leverages existing license validation logic from the activation feature (US-006).
- Requires a new backend endpoint and simple frontend component.
- Primary complexity lies in robustly handling different error states from the external API call.

## 8.3.0.0 Technical Risks

- The external licensing service may be unavailable, requiring graceful error handling.
- Customer-side network configurations (firewalls, proxies) could interfere with the connection, making clear error messages critical for troubleshooting.

## 8.4.0.0 Integration Points

- Frontend (React) calls the Backend (ASP.NET Core) API.
- Backend API calls the external cloud-based licensing service API.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Test successful validation and UI update.
- Test validation failure due to an invalid key.
- Test validation failure due to a network error (mocked).
- Test that the button is disabled during the API call.
- Test that a successful validation clears a 'Grace Period' state.

## 9.3.0.0 Test Data Needs

- A known valid/active license key for testing.
- A known invalid/expired license key for testing.
- A mechanism to simulate network failure to the licensing service endpoint.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- A staging environment for the licensing service to allow for E2E testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing in a testing environment.
- Backend and frontend code reviewed and approved by at least one other developer.
- Unit tests implemented for both backend and frontend, achieving >= 80% coverage for new code.
- Integration testing between frontend, backend, and a mocked licensing service is completed successfully.
- UI/UX for the button, loading state, and feedback messages reviewed and approved.
- Manual QA has been performed for all scenarios, including network failure simulation.
- All security and accessibility requirements have been met and verified.
- The feature is deployed and verified in the staging environment.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

2

## 11.2.0.0 Priority

🟡 Medium

## 11.3.0.0 Sprint Considerations

- This is a quality-of-life improvement for administrators. It should be prioritized after the core licensing activation (US-006) is complete.
- Requires coordination if the external licensing service API is not yet stable or available for testing.

## 11.4.0.0 Release Impact

- Low impact on the overall release, but significantly improves the administrator's ability to manage the system's license.

