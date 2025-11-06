# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-021 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Delete user accounts |
| As A User Story | As an Administrator, I want to permanently delete ... |
| User Persona | Administrator |
| Business Value | Enhances system security by enabling timely de-pro... |
| Functional Area | User Management |
| Story Theme | System Administration & Security |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successfully delete a standard user account

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an Administrator logged into the Control Panel and am on the User Management page

### 3.1.5 When

I click the 'Delete' action for a standard user, and I confirm the deletion in the subsequent confirmation modal

### 3.1.6 Then

A success notification is displayed confirming the user was deleted, the user is removed from the user list in the UI, and an audit log entry is created for this action.

### 3.1.7 Validation Notes

Verify the user can no longer log in. Check the database to confirm the user record is removed. Check the audit log for the corresponding entry.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Cancel the deletion process

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

I am an Administrator on the User Management page and have clicked the 'Delete' action for a user, opening the confirmation modal

### 3.2.5 When

I click the 'Cancel' button in the confirmation modal

### 3.2.6 Then

The confirmation modal closes, no action is taken, and the user account remains active and visible in the user list.

### 3.2.7 Validation Notes

Verify the user account is not deleted from the database and the user can still log in.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempt to delete the primary administrator account

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

I am an Administrator on the User Management page

### 3.3.5 When

I view the entry for the primary administrator account in the user list

### 3.3.6 Then

The 'Delete' action is disabled or not visible for this account.

### 3.3.7 Validation Notes

The UI must visually indicate that this action is not permitted. A tooltip should explain why.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Attempt to delete one's own administrator account

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

I am an Administrator on the User Management page

### 3.4.5 When

I view the entry for my own account in the user list

### 3.4.6 Then

The 'Delete' action is disabled or not visible for my account.

### 3.4.7 Validation Notes

The UI must prevent self-deletion to avoid accidental lockout. A tooltip should explain why.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

API request to delete the primary administrator is rejected

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am an authenticated Administrator with a valid JWT

### 3.5.5 When

I send a direct API request to the endpoint responsible for deleting the primary administrator's account

### 3.5.6 Then

The API responds with an appropriate error code (e.g., 403 Forbidden or 409 Conflict) and a message stating that this action is not permitted.

### 3.5.7 Validation Notes

Use an API client like Postman or curl to test this endpoint directly. The database state should remain unchanged.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

System feedback on failed deletion

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

I am an Administrator attempting to delete a user

### 3.6.5 When

The deletion process fails due to a backend or database error

### 3.6.6 Then

An error notification is displayed to me, the user account is not deleted, and the error is logged by the system.

### 3.6.7 Validation Notes

Simulate a database connection failure during the delete operation to test this scenario.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A 'Delete' button or icon for each user in the user list.
- A confirmation modal dialog with a clear warning message.
- A 'Confirm' button within the modal.
- A 'Cancel' button within the modal.
- A toast/notification component for success and error messages.

## 4.2.0 User Interactions

- Clicking 'Delete' opens the confirmation modal.
- The 'Delete' button/icon must be disabled for the primary administrator and the currently logged-in user.
- Hovering over a disabled 'Delete' button should show a tooltip explaining why it is disabled.
- The confirmation modal must trap focus for accessibility.
- After successful deletion, the user list should update automatically without a page reload.

## 4.3.0 Display Requirements

- The confirmation modal must display the username of the account being deleted to prevent mistakes.
- The modal must contain a warning that the action is permanent and irreversible.

## 4.4.0 Accessibility Needs

- All interactive elements (buttons, modal) must be keyboard accessible and have proper focus indicators.
- The confirmation modal must use ARIA attributes to be accessible to screen readers.
- The UI must adhere to WCAG 2.1 Level AA standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

The primary administrator account, created during system installation, cannot be deleted.

### 5.1.3 Enforcement Point

Both UI (disabled action) and API (request rejection).

### 5.1.4 Violation Handling

The UI prevents the action. The API returns a 4xx error with an explanatory message.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

An administrator cannot delete their own user account.

### 5.2.3 Enforcement Point

Both UI (disabled action) and API (request rejection).

### 5.2.4 Violation Handling

The UI prevents the action. The API returns a 4xx error with an explanatory message.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

Every user deletion action must be recorded in the system's audit log.

### 5.3.3 Enforcement Point

Backend service layer, within the same transaction as the user deletion.

### 5.3.4 Violation Handling

If the audit log entry cannot be created, the entire deletion transaction should be rolled back to ensure data integrity.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-018

#### 6.1.1.2 Dependency Reason

A user must be creatable before they can be deleted.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-019

#### 6.1.2.2 Dependency Reason

The 'Administrator' role must exist to define permissions for this action.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-101

#### 6.1.3.2 Dependency Reason

The audit logging system must be in place to record the deletion event.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity for user management.
- A defined RESTful API endpoint for user deletion (e.g., DELETE /api/v1/users/{id}).
- A frontend user management component capable of displaying a list of users.

## 6.3.0.0 Data Dependencies

- Requires existing user data in the database to perform the deletion.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API response for the delete operation should be under 500ms.
- The UI update after deletion should be visually instantaneous.

## 7.2.0.0 Security

- The API endpoint must be protected and only accessible by users with the 'Administrator' role.
- The audit log entry must include the timestamp, the acting administrator's username, the deleted user's username, and the source IP address.

## 7.3.0.0 Usability

- The process must include a confirmation step to prevent accidental deletion of users.
- Feedback (success or error) must be immediate and clear.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires both frontend (UI component, modal, state management) and backend (API endpoint, service logic) changes.
- Business logic is required to check for primary admin and self-deletion.
- Integration with the audit logging system is necessary.

## 8.3.0.0 Technical Risks

- Decisions on handling foreign key constraints for data created by the user (e.g., report configurations, logs) need to be made. A hard delete might fail if constraints are not handled. The default assumption is that related data should be handled via cascade delete or setting FKs to null where appropriate.

## 8.4.0.0 Integration Points

- ASP.NET Core Identity framework.
- System audit logging service.
- Entity Framework Core for database operations.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify successful deletion of a non-admin user.
- Verify cancellation of the delete action.
- Verify the delete action is disabled in the UI for the primary admin and self.
- Verify API calls to delete the primary admin and self are rejected.
- Verify an audit log is created upon successful deletion.
- Verify UI feedback for both success and failure cases.

## 9.3.0.0 Test Data Needs

- An administrator account for testing.
- A standard (Viewer) user account to be deleted.
- The primary administrator account to test prevention rules.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- An E2E testing framework like Playwright or Cypress.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for both backend and frontend, achieving >80% coverage for new code
- Integration testing for the API endpoint completed successfully
- End-to-end test scenario for the full user flow is implemented and passing
- User interface reviewed and approved for usability and accessibility (WCAG 2.1 AA)
- Security requirements, including role-based access and audit logging, are validated
- Documentation for the API endpoint is updated in the OpenAPI specification
- Story deployed and verified in a staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story should be prioritized after the core user creation and listing functionalities are complete.
- Requires coordination between frontend and backend development.

## 11.4.0.0 Release Impact

This is a core security and administrative feature required for a production-ready release.

