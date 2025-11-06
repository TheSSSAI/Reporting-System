# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-032 |
| Elaboration Date | 2025-01-17 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Be automatically logged out after a period of inac... |
| As A User Story | As a User (Administrator or Viewer), I want to be ... |
| User Persona | Any authenticated user of the system, including 'A... |
| Business Value | Enhances system security by mitigating the risk of... |
| Functional Area | User Management and Security |
| Story Theme | System Security & Access Control |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

User is automatically logged out after the default inactivity period

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a user is logged into the application and the inactivity timeout is set to the default of 15 minutes

### 3.1.5 When

the user performs no actions (no clicks, key presses, or mouse movements) for 15 minutes

### 3.1.6 Then

the user's session is terminated on the server, and their browser is automatically redirected to the login page.

### 3.1.7 Validation Notes

Test by logging in, waiting for the timeout period (use a shortened value in the test environment), and verifying the redirection. Check browser local storage/session storage for cleared auth tokens.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

User activity resets the inactivity timer

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

a user is logged into the application and the inactivity timeout is 15 minutes

### 3.2.5 When

the user performs an action (e.g., clicks a navigation link) after 10 minutes of inactivity

### 3.2.6 Then

the inactivity timer is reset, and the user remains logged in.

### 3.2.7 Validation Notes

Test by logging in, waiting for a period less than the timeout, performing an action, and verifying that the session does not expire at the original 15-minute mark.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User receives a warning modal before the session expires

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

a user is logged into the application and the inactivity timeout is 15 minutes

### 3.3.5 When

the user has been inactive for 14 minutes

### 3.3.6 Then

a warning modal is displayed, clearly stating that the session will expire in 60 seconds and showing a countdown.

### 3.3.7 Validation Notes

Verify the modal appears at the correct time (e.g., T-minus 60 seconds) and the countdown timer is functioning.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

User extends their session via the warning modal

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

the session expiration warning modal is displayed

### 3.4.5 When

the user clicks the 'Stay Logged In' button before the countdown ends

### 3.4.6 Then

the modal closes, the user's session is extended, and the inactivity timer is reset to 15 minutes.

### 3.4.7 Validation Notes

Verify that clicking the button makes a successful API call to refresh the token, the modal closes, and the user is not logged out.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Session timeout is consistent across multiple browser tabs

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

a user is logged into the application in two separate browser tabs

### 3.5.5 When

the user is active in Tab A, which resets the inactivity timer

### 3.5.6 Then

the user is not logged out in Tab B, as the session as a whole is still active.

### 3.5.7 Validation Notes

Test by opening two tabs, interacting with one, and confirming the other does not time out. Then, let both time out and confirm both are redirected to the login page.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Server rejects API calls with an expired session token

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

a user's session has timed out and they have been logged out

### 3.6.5 When

a malicious or outdated request attempts to use the old JWT to make an API call

### 3.6.6 Then

the server rejects the request with an HTTP 401 Unauthorized status code.

### 3.6.7 Validation Notes

Use an API client like Postman or curl. Capture a valid JWT, wait for the session to expire, and then attempt to use the captured JWT to access a protected endpoint. Verify the 401 response.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A modal dialog for the session timeout warning.
- A countdown timer display within the modal.
- A 'Stay Logged In' button within the modal.
- A 'Log Out Now' button (optional) within the modal.

## 4.2.0 User Interactions

- The application must detect user activity globally (mouse movement, clicks, key presses).
- Clicking 'Stay Logged In' should dismiss the modal and reset the timer.
- If no action is taken, the modal should close automatically upon timeout, followed by a redirect to the login page.

## 4.3.0 Display Requirements

- The warning modal must overlay the current screen content.
- The modal text must clearly communicate that the session is about to expire and show the remaining time.

## 4.4.0 Accessibility Needs

- The warning modal must be compliant with WCAG 2.1 Level AA.
- The modal must trap focus, be navigable via keyboard, and be properly announced by screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

The default session inactivity timeout duration is 15 minutes.

### 5.1.3 Enforcement Point

Client-side timer and server-side JWT validation.

### 5.1.4 Violation Handling

The user session is terminated.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A session timeout warning must be displayed 60 seconds before the session expires.

### 5.2.3 Enforcement Point

Client-side application logic.

### 5.2.4 Violation Handling

N/A. If the warning fails, the primary timeout rule (BR-001) still applies.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-027

#### 6.1.1.2 Dependency Reason

A user must be able to log in and establish a session before that session can be timed out.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-087

#### 6.1.2.2 Dependency Reason

The session management relies on JWTs. The timeout mechanism is tied to the JWT lifecycle and validation.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-088

#### 6.1.3.2 Dependency Reason

The 'Stay Logged In' functionality requires an API endpoint to refresh the JWT, which is defined in this story.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity for user management.
- JWT bearer token authentication middleware in the backend.
- A global state management solution (Zustand) in the React frontend to manage session state.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- Client-side activity tracking must be lightweight and have no noticeable impact on UI performance or responsiveness.

## 7.2.0.0 Security

- Session termination must occur on the server-side by enforcing JWT expiration. A client-side redirect alone is insufficient.
- The communication to refresh the session token must be over HTTPS.

## 7.3.0.0 Usability

- The timeout warning provides a clear, non-intrusive way for users to maintain their session without interruption if they are still present.

## 7.4.0.0 Accessibility

- The warning modal and its controls must be fully accessible as per WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The timeout mechanism must function correctly on all supported browsers (latest stable Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires coordinated logic between the frontend (React) and backend (ASP.NET Core).
- Implementing a robust, global activity listener on the client side.
- Managing state across multiple browser tabs can be complex.
- Requires careful handling of JWT refresh logic to ensure security.

## 8.3.0.0 Technical Risks

- An improperly implemented client-side timer could cause performance issues or fail to fire correctly.
- If session state is not managed correctly across tabs, a user could be logged out unexpectedly.

## 8.4.0.0 Integration Points

- Frontend global application layout (to host the timer and modal logic).
- Backend authentication middleware (to validate tokens).
- Backend token refresh endpoint.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify logout after timeout with no user interaction.
- Verify timer reset after user interaction.
- Verify warning modal appears at the correct time.
- Verify session extension by clicking 'Stay Logged In'.
- Verify multi-tab consistency for activity and logout.
- Verify server rejects API calls with an expired token.

## 9.3.0.0 Test Data Needs

- A valid test user account (any role).
- Ability to configure a short timeout duration in the test environment to make E2E tests feasible.

## 9.4.0.0 Testing Tools

- Jest and React Testing Library for frontend unit tests.
- xUnit for backend unit tests.
- A framework like Cypress or Playwright for E2E testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for both frontend and backend logic, achieving >80% coverage
- E2E tests for the full timeout and session extension flow are implemented and passing
- User interface for the warning modal reviewed and approved for UX and accessibility (WCAG 2.1 AA)
- Security requirement of server-side token invalidation is verified
- Documentation updated to reflect the session timeout feature
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational security feature that should be implemented early.
- This story is a prerequisite for US-033 (Configure the session timeout duration).
- Requires both frontend and backend development effort within the same sprint.

## 11.4.0.0 Release Impact

Improves the security posture of the application for its initial release.

