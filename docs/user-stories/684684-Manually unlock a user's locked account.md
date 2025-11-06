# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-025 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Manually unlock a user's locked account |
| As A User Story | As an Administrator, I want to manually unlock a u... |
| User Persona | Administrator |
| Business Value | Reduces user downtime and support friction by prov... |
| Functional Area | User Management |
| Story Theme | User & Access Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Administrator successfully unlocks a locked user account

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an Administrator logged into the Control Panel and am on the User Management page

### 3.1.5 When

I locate a user account that is marked as 'Locked' and click the 'Unlock' action for that user, and then confirm the action in the confirmation dialog

### 3.1.6 Then

The system must update the user's account status to 'Active' (unlocked) in the database, the UI must update in real-time to reflect the new status, a success notification ('User [username] has been successfully unlocked.') must be displayed, and the 'Unlock' action for that user must be disabled or hidden.

### 3.1.7 Validation Notes

Verify by checking the user's status in the UI and by attempting to log in as the unlocked user with their correct credentials. Also, check the database to confirm the `LockoutEnd` property for the user is set to null or a past date.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Administrator cancels the unlock action

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

I am an Administrator who has clicked the 'Unlock' action for a locked user

### 3.2.5 When

I click 'Cancel' or close the confirmation dialog without confirming

### 3.2.6 Then

The user's account must remain in the 'Locked' state and no changes should be made to the account.

### 3.2.7 Validation Notes

Verify that the user's status in the UI remains 'Locked' and they are still unable to log in.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Unlock action is not available for non-locked accounts

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am an Administrator viewing the list of users in the Control Panel

### 3.3.5 When

I view a user account that is not in a 'Locked' state

### 3.3.6 Then

The 'Unlock' action must be either hidden or disabled for that user.

### 3.3.7 Validation Notes

Inspect the UI for several active user accounts and confirm the unlock control is not available for interaction.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Audit log records the unlock event

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

An Administrator has successfully unlocked a user's account

### 3.4.5 When

The unlock operation is completed

### 3.4.6 Then

A new entry must be created in the system's audit log containing the timestamp, the administrator's username, the action performed ('User Account Unlocked'), the target username, and the source IP address.

### 3.4.7 Validation Notes

Check the Audit Log viewer in the Control Panel or query the audit log table directly to confirm the entry was created with the correct details.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Non-administrator user cannot perform unlock action

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

A user with the 'Viewer' role is logged into the system

### 3.5.5 When

They attempt to access the API endpoint for unlocking a user directly

### 3.5.6 Then

The API must return an HTTP 403 Forbidden (or similar authorization) error.

### 3.5.7 Validation Notes

Use an API client like Postman to make a request to the unlock endpoint while authenticated as a 'Viewer' and verify the response code.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A clear visual indicator (e.g., a 'Locked' badge or icon) next to locked users in the user list.
- An 'Unlock' button or menu item associated with each locked user.
- A confirmation modal dialog with 'Confirm' and 'Cancel' actions.
- A toast notification component to display success messages.

## 4.2.0 User Interactions

- Clicking the 'Unlock' button triggers the confirmation modal.
- The UI for the user list must update automatically after a successful unlock without requiring a manual page refresh.
- The 'Unlock' button must be disabled for users who are not locked to prevent invalid actions.

## 4.3.0 Display Requirements

- The confirmation dialog must display the username of the account being unlocked (e.g., 'Are you sure you want to unlock user: [username]?').

## 4.4.0 Accessibility Needs

- The 'Unlock' button and status indicators must have appropriate ARIA labels for screen readers.
- The confirmation modal must be keyboard-navigable and trap focus.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Only users with the 'Administrator' role can unlock user accounts.

### 5.1.3 Enforcement Point

API endpoint authorization middleware.

### 5.1.4 Violation Handling

The system returns an HTTP 403 Forbidden status code.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Unlocking an account must reset both the lockout flag and the failed login attempt counter for the user.

### 5.2.3 Enforcement Point

Backend service logic handling the unlock request.

### 5.2.4 Violation Handling

The operation is transactional; if either part fails, the entire unlock operation should be rolled back.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-031

#### 6.1.1.2 Dependency Reason

The system must be able to lock an account before the functionality to unlock it can be implemented or tested.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-019

#### 6.1.2.2 Dependency Reason

Requires the existence of an 'Administrator' role to enforce access control.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-101

#### 6.1.3.2 Dependency Reason

Requires the audit logging framework to be in place to record the security-sensitive unlock event.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity for user management and lockout state.
- React component library (MUI) for the user list, buttons, and modal dialogs.
- API authentication mechanism (JWT) to secure the endpoint.

## 6.3.0.0 Data Dependencies

- Requires access to the user table in the SQLite database, specifically the fields that manage lockout status (`LockoutEnd`, `AccessFailedCount`).

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API response time for the unlock action must be under 500ms.
- The user management UI should load and remain responsive with up to 1,000 user records.

## 7.2.0.0 Security

- The API endpoint for unlocking users must be protected against Cross-Site Request Forgery (CSRF).
- The action must be logged in a tamper-evident audit trail as specified in SRS 6.4.

## 7.3.0.0 Usability

- The process of identifying a locked user and unlocking them should be intuitive and require no more than three clicks.

## 7.4.0.0 Accessibility

- All UI elements related to this feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest stable versions of Chrome, Firefox, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Leverages standard functionality provided by the ASP.NET Core Identity framework (`UserManager`).
- Frontend work involves standard state management and API calls.
- No complex business logic is required.

## 8.3.0.0 Technical Risks

- Ensuring the UI state updates reactively and correctly after the API call without race conditions.

## 8.4.0.0 Integration Points

- Backend: ASP.NET Core Identity's `UserManager` service.
- Backend: The custom audit logging service.
- Frontend: The global state management solution (Zustand) may be used to manage the user list.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify a locked user can be successfully unlocked by an admin.
- Verify the unlock action is not available for an active user.
- Verify a user with the 'Viewer' role cannot unlock an account.
- Verify that after being unlocked, the user can successfully log in.
- Verify the audit log contains a correct entry for the unlock event.

## 9.3.0.0 Test Data Needs

- An active administrator account.
- A standard user account that is pre-configured to be in a 'locked' state.
- A standard user account that is in an 'active' state.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit/integration tests.
- Jest/React Testing Library for frontend unit tests.
- Cypress or Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit tests for backend and frontend implemented with >= 80% coverage for new code
- Integration testing for the API endpoint and database interaction completed successfully
- End-to-end test scenario for the full user flow is implemented and passing
- User interface reviewed for usability and adherence to design standards
- Security requirements (RBAC, audit logging) validated
- API documentation (Swagger/OpenAPI) is updated for the new endpoint
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

2

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a core administrative function required for basic user support. It should be prioritized soon after the account lockout mechanism (US-031) is complete.

## 11.4.0.0 Release Impact

- Enables basic system administration and user support, which is critical for any production release.

