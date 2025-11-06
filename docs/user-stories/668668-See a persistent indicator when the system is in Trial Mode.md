# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-009 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | See a persistent indicator when the system is in T... |
| As A User Story | As an Administrator, I want to see a persistent an... |
| User Persona | Administrator |
| Business Value | Improves user experience by preventing confusion o... |
| Functional Area | System Licensing & Activation |
| Story Theme | User Experience and Monetization |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Indicator is visible on login and all Control Panel pages in Trial Mode

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

The system's license state is 'Trial Mode'

### 3.1.5 When

An Administrator logs in and navigates to any page within the Control Panel (e.g., Dashboard, Reports, Connectors, System Settings)

### 3.1.6 Then

A persistent, non-dismissible indicator is displayed at the top of every page, clearly stating the system is in Trial Mode.

### 3.1.7 Validation Notes

Verify by logging in with a trial-state system. Navigate between at least 3 different Control Panel pages and confirm the banner remains.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Indicator contains correct text and an actionable link

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The Trial Mode indicator is visible

### 3.2.5 When

The Administrator views the indicator

### 3.2.6 Then

The indicator text reads: "System is in Trial Mode. Some features are limited. Activate now."

### 3.2.7 And

The 'Activate now' text is a hyperlink that directs the user to the system's licensing/activation page.

### 3.2.8 Validation Notes

Inspect the UI element for the exact text. Click the link and verify it navigates to the correct activation page.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Indicator is not visible for licensed systems

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

The system's license state is 'Active'

### 3.3.5 When

An Administrator logs in and navigates through the Control Panel

### 3.3.6 Then

The Trial Mode indicator is not displayed on any page.

### 3.3.7 Validation Notes

Log in with a fully licensed system and confirm the banner is absent.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Indicator disappears immediately after successful activation

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

The system is in Trial Mode and the Administrator is on the activation page

### 3.4.5 When

The Administrator successfully submits a valid license key

### 3.4.6 Then

The Trial Mode indicator immediately disappears from view without requiring a page reload or re-login.

### 3.4.7 Validation Notes

Perform the activation process and observe that the banner is removed from the DOM in real-time upon the success response from the activation API.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Indicator appears when a license expires and grace period ends

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

An Administrator is logged in, the system is 'Active', and the license grace period expires

### 3.5.5 When

The Administrator reloads the current page or navigates to a new page

### 3.5.6 Then

The Trial Mode indicator is now visible.

### 3.5.7 Validation Notes

This requires a test setup where the system state can be forced from 'Grace Period' to 'Trial'. Verify the banner appears on the next user navigation.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Indicator is accessible

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

The Trial Mode indicator is visible

### 3.6.5 When

The UI is inspected with accessibility tools

### 3.6.6 Then

The color contrast between the indicator's text and background meets WCAG 2.1 Level AA standards.

### 3.6.7 And

The 'Activate now' link has a clearly visible focus state when navigated to via the keyboard.

### 3.6.8 Validation Notes

Use browser developer tools (e.g., Lighthouse) and manual keyboard navigation (Tab key) to verify compliance.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A persistent banner at the top of the viewport, below the main application header.
- A hyperlink within the banner for activation.

## 4.2.0 User Interactions

- The banner is static and not dismissible.
- Clicking the 'Activate now' link navigates the user to the licensing page.

## 4.3.0 Display Requirements

- The banner should use an amber/yellow color scheme to indicate a warning/informational state, consistent with the MUI v5 theme.
- The banner must be responsive and not break the layout on screen widths of 1280px and above.

## 4.4.0 Accessibility Needs

- All text must be screen-reader accessible.
- Color contrast must meet WCAG 2.1 AA.
- Keyboard navigation to and activation of the link must be supported.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "The Trial Mode indicator must only be displayed when the system's license state is explicitly 'Trial'. It must not be shown during 'Active' or 'Grace Period' states.", 'enforcement_point': 'Frontend application layout component, based on system state provided by the backend.', 'violation_handling': "If the rule is violated, it's a bug. The UI would be showing an incorrect system status."}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-006

#### 6.1.1.2 Dependency Reason

The 'Activate now' link requires the activation page and functionality to exist.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-017

#### 6.1.2.2 Dependency Reason

The system must have a defined 'Trial Mode' state that this indicator reflects, particularly the transition from a grace period to trial.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint (e.g., as part of the user session data) that reliably provides the current license state ('TRIAL', 'ACTIVE', 'GRACE_PERIOD').
- A global state management solution in the frontend (e.g., Zustand) to hold and provide the license state to all components.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The check for license status must not introduce any perceptible delay (>50ms) to page load times.

## 7.2.0.0 Security

- The license status should be determined authoritatively by the backend and passed to the client; the client must not be able to spoof its license status.

## 7.3.0.0 Usability

- The indicator must be clear and unambiguous, but not so intrusive that it impedes the use of the application's primary functions.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The indicator must render correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires coordination between backend (exposing state) and frontend (displaying state).
- Real-time update upon activation requires careful state management on the client-side to avoid a full page reload.

## 8.3.0.0 Technical Risks

- Potential for a flash of the indicator on page load if the license state is not available immediately.
- State synchronization issues if the user has multiple tabs open when they activate the license.

## 8.4.0.0 Integration Points

- Integrates with the ASP.NET Core Identity/Session management to retrieve the license state.
- Integrates with the React frontend's root layout/component structure to ensure persistence.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Admin logs into a trial system.
- Admin logs into an active system.
- Admin activates a trial system and verifies the banner disappears.
- UI responsiveness check on different browser window sizes.
- Keyboard navigation and screen reader verification.

## 9.3.0.0 Test Data Needs

- Test user accounts with the 'Administrator' role.
- Ability to configure the backend system into 'Trial', 'Active', and 'Grace Period' states for testing purposes.

## 9.4.0.0 Testing Tools

- Jest and React Testing Library for frontend unit tests.
- xUnit for backend unit tests.
- Cypress or Playwright for E2E tests.
- Browser accessibility audit tools (e.g., Lighthouse, Axe).

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for both frontend component and backend state logic, achieving >80% coverage
- Integration testing completed successfully between frontend and backend
- User interface reviewed and approved for visual consistency and responsiveness
- Accessibility requirements validated (WCAG 2.1 AA)
- E2E tests for key scenarios are passing
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

2

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story should be prioritized early in the development cycle as it's a key part of the trial user experience.
- Requires the backend API for license status to be available before frontend work can be completed.

## 11.4.0.0 Release Impact

Essential for the initial release to properly manage and communicate the trial version's limitations to users.

