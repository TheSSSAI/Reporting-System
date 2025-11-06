# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-014 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | See a prominent notification during the license va... |
| As A User Story | As an Administrator, I want to see a prominent, pe... |
| User Persona | Administrator |
| Business Value | Proactively informs administrators of a pending li... |
| Functional Area | System Licensing & Administration |
| Story Theme | System Licensing & Activation |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Banner appears when system enters Grace Period

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

an Administrator is logged into the Control Panel and the system's license state is 'Active'

### 3.1.5 When

the system fails a periodic license validation and its state transitions to 'Grace Period' with 7 days remaining

### 3.1.6 Then

a prominent notification banner is immediately displayed at the top of the current page

### 3.1.7 Validation Notes

Test by manually setting the system state to 'Grace Period' via a test endpoint or database flag. Verify the banner appears on page load/refresh.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Banner displays correct and informative content

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

the system is in a 'Grace Period' with 5 days remaining

### 3.2.5 When

the Administrator views any page in the Control Panel

### 3.2.6 Then

the banner contains text that clearly states the system is in a grace period, indicates that it will revert to Trial Mode, and shows the number of days remaining (e.g., 'in 5 days')

### 3.2.7 And

the banner includes a clickable link or button to navigate to the licensing configuration page.

### 3.2.8 Validation Notes

Verify the text content and the functionality of the link. The countdown logic must be accurate.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Banner is persistent across all Control Panel pages

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

the system is in a 'Grace Period' and the notification banner is visible on the Dashboard

### 3.3.5 When

the Administrator navigates to the 'Reports' page, then to the 'Users' page

### 3.3.6 Then

the notification banner remains visible at the top of every page viewed.

### 3.3.7 Validation Notes

Click through all major navigation items in the Control Panel and confirm the banner's consistent presence.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Banner is not dismissible

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

the system is in a 'Grace Period' and the notification banner is visible

### 3.4.5 When

the Administrator interacts with the UI

### 3.4.6 Then

there is no 'close' or 'dismiss' button on the banner.

### 3.4.7 Validation Notes

Inspect the banner's HTML to ensure no close functionality exists. The banner should only disappear when the underlying system state changes.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Banner disappears when license issue is resolved

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

the system is in a 'Grace Period' and the notification banner is visible

### 3.5.5 When

the Administrator successfully re-validates the license, transitioning the system state back to 'Active'

### 3.5.6 Then

the grace period notification banner is immediately removed from view without requiring a full page reload.

### 3.5.7 Validation Notes

This requires the frontend to re-fetch or be notified of the license status change and update the UI accordingly.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Banner is replaced by Trial Mode indicator when Grace Period expires

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

the system is in a 'Grace Period' on its final day

### 3.6.5 When

the grace period expires and the system state transitions to 'Trial Mode'

### 3.6.6 Then

the grace period notification banner is removed and the persistent 'Trial Mode' indicator becomes visible.

### 3.6.7 Validation Notes

Test by setting the grace period expiry to a time in the near past and refreshing the page. Verify the correct UI state transition.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A full-width banner component to be displayed at the top of the application layout.
- A warning icon (e.g., triangle with exclamation mark).
- A text area for the notification message.
- A hyperlink or button element for the call-to-action.

## 4.2.0 User Interactions

- The banner is static and does not accept user input, other than clicking the call-to-action link/button.
- The call-to-action link/button navigates the user to the license management section of the Control Panel.

## 4.3.0 Display Requirements

- The banner must be positioned below the main header/navigation but above all page-specific content.
- The banner must use a distinct warning color scheme (e.g., yellow or orange background) to differentiate it from normal UI.
- The banner must not overlay page content; it should push the content down.

## 4.4.0 Accessibility Needs

- The banner must have a `role` attribute of 'alert' or be contained within a landmark region so it is announced by screen readers.
- Text content must have a color contrast ratio of at least 4.5:1 against the banner background to meet WCAG 2.1 AA.
- The call-to-action link/button must be focusable via keyboard and have a clear, descriptive accessible name.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

The grace period notification is only displayed when the system's license status is exactly 'GracePeriod'.

### 5.1.3 Enforcement Point

Frontend application layout rendering.

### 5.1.4 Violation Handling

If the state is not 'GracePeriod', the banner must not be rendered in the DOM.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The countdown for days remaining must be calculated based on the grace period expiry timestamp provided by the backend.

### 5.2.3 Enforcement Point

Frontend component logic.

### 5.2.4 Violation Handling

If the calculation results in zero or a negative number, the banner should be hidden as the grace period has expired.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-016

#### 6.1.1.2 Dependency Reason

The backend must implement the logic for entering a 'Grace Period' state before the UI can display a notification for it.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-006

#### 6.1.2.2 Dependency Reason

The notification needs a destination page for the user to take action, which is the activation/licensing page from this story.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint (e.g., GET /api/v1/system/license-status) that provides the current license state and, if applicable, the grace period expiry timestamp.
- A global state management solution in the frontend (Zustand) to hold and provide the license status to the UI components.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The license status check should not add more than 50ms of latency to the initial loading of the Control Panel's main application shell.

## 7.2.0.0 Security

- The license status information retrieved from the backend should not contain any sensitive license key details, only the status and expiry.

## 7.3.0.0 Usability

- The notification message must be clear, concise, and easily understood by a non-technical administrator.
- The call-to-action must be unambiguous and lead the user directly to the place where they can resolve the issue.

## 7.4.0.0 Accessibility

- Must conform to Web Content Accessibility Guidelines (WCAG) 2.1 at Level AA.

## 7.5.0.0 Compatibility

- The banner must render correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires coordination between a new backend API endpoint and a new frontend component.
- Frontend logic for calculating and displaying the countdown needs to be robust.
- Component needs to be integrated into the global application layout to ensure persistence.

## 8.3.0.0 Technical Risks

- Potential for inaccurate countdown display if time zones are not handled correctly between the server and client.
- Risk of layout issues on some pages if the banner is not implemented carefully to push content down.

## 8.4.0.0 Integration Points

- Backend: Licensing module.
- Frontend: Main application layout/shell, global state (Zustand), and API client service.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify banner appears when state changes to 'GracePeriod'.
- Verify banner disappears when state changes to 'Active'.
- Verify banner is replaced by 'Trial' indicator when state changes to 'Trial'.
- Verify countdown text is accurate for various remaining times (7 days, 1 day, <24 hours).
- Verify navigation link works correctly.
- Verify UI layout is not broken on any Control Panel page.
- Verify accessibility with screen reader and keyboard navigation.

## 9.3.0.0 Test Data Needs

- Ability to set the system's license state to 'Active', 'GracePeriod', and 'Trial' on demand in a test environment.
- Ability to specify the expiry timestamp for the 'GracePeriod' state.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend component tests.
- Browser developer tools for accessibility checks (e.g., Lighthouse, Axe).

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >80% coverage for new code
- Integration testing between frontend and backend completed successfully
- User interface reviewed and approved for visual correctness and responsiveness
- Performance requirements verified
- Security requirements validated
- Accessibility audit passed for the new component
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is dependent on the completion of US-016 (backend logic for grace period). It should be scheduled in the same sprint as or a subsequent sprint to US-016.
- This is a high-visibility feature for user experience and should be prioritized before a major release.

## 11.4.0.0 Release Impact

- Improves the customer experience around license management and reduces potential friction or support calls related to license expiration.

