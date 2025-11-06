# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-022 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Be prevented from deleting the primary administrat... |
| As A User Story | As an Administrator, I want to be prevented from d... |
| User Persona | Administrator |
| Business Value | Prevents irreversible system lockout, ensuring adm... |
| Functional Area | User Management & Security |
| Story Theme | System Integrity & Safety |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

UI prevents deletion of the primary administrator

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am logged in as an Administrator and am on the User Management page of the Control Panel

### 3.1.5 When

I view the list of users and locate the entry for the primary administrator account

### 3.1.6 Then

the 'Delete' button or action for that specific user is visually disabled and cannot be clicked.

### 3.1.7 Validation Notes

Verify the button's 'disabled' attribute is true in the DOM. Manual and automated E2E tests can confirm this.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

UI provides context for disabled delete action

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

the 'Delete' button for the primary administrator is disabled on the User Management page

### 3.2.5 When

I hover my mouse cursor over the disabled 'Delete' button

### 3.2.6 Then

a tooltip appears with the message: 'The primary administrator account cannot be deleted.'

### 3.2.7 Validation Notes

Manual testing or E2E test to verify the presence and content of the tooltip on hover.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

API rejects request to delete the primary administrator

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am an authenticated API user with Administrator privileges and I know the user ID of the primary administrator

### 3.3.5 When

I send a DELETE request to the API endpoint for deleting users (e.g., `/api/v1/users/{primary_admin_id}`)

### 3.3.6 Then

the server must respond with an HTTP 403 Forbidden status code.

### 3.3.7 And

the response body must contain a JSON object with an error message, such as `{"error": "The primary administrator account cannot be deleted."}`

### 3.3.8 Validation Notes

Integration test that directly calls the API endpoint and asserts the status code and response body.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

A non-primary administrator account can be deleted

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

I am logged in as an Administrator and there are at least two administrator accounts: the primary and a secondary

### 3.4.5 When

I attempt to delete the secondary (non-primary) administrator account through the UI or API

### 3.4.6 Then

the system proceeds with the standard deletion workflow (e.g., confirmation prompt followed by successful deletion).

### 3.4.7 Validation Notes

E2E test for the UI flow and an integration test for the API flow to confirm a non-primary admin can be deleted successfully.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- User list table/grid
- Disabled 'Delete' button
- Tooltip for the disabled button

## 4.2.0 User Interactions

- The 'Delete' button for the primary admin must be non-interactive.
- Hovering over the disabled button reveals an explanatory tooltip.

## 4.3.0 Display Requirements

- The user list must be rendered.
- The disabled state of the button must be visually distinct from an enabled state (e.g., greyed out).

## 4.4.0 Accessibility Needs

- The disabled button must have the `aria-disabled="true"` attribute to be correctly interpreted by screen readers.
- The tooltip must be accessible, either via focus or through an `aria-describedby` attribute.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'The primary administrator account, created during initial system installation, is non-deletable.', 'enforcement_point': 'This rule is enforced in the backend business logic layer before any database operation is attempted.', 'violation_handling': 'An attempt to violate this rule results in a hard rejection of the request with a clear error message, both in the API response and presented to the user in the UI.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-003

#### 6.1.1.2 Dependency Reason

This story defines the creation of the primary administrator account. The system needs a way to identify this specific account, which should be established in US-003 (e.g., a database flag).

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-021

#### 6.1.2.2 Dependency Reason

This story implements the general functionality for deleting user accounts. US-022 adds a specific constraint to that existing functionality.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-089

#### 6.1.3.2 Dependency Reason

This story defines the API for user management. The API-level protection in US-022 must be implemented within the endpoints established by US-089.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity for user management.
- Entity Framework Core data model for the 'User' entity, which must include a way to identify the primary admin (e.g., a boolean `IsPrimary` flag).

## 6.3.0.0 Data Dependencies

- Requires the existence of a user account that is flagged as the primary administrator in the configuration database.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The check to determine if a user is the primary admin must add negligible overhead (< 5ms) to the user deletion process.

## 7.2.0.0 Security

- The check MUST be performed on the server-side (backend). Relying only on a disabled UI button is insufficient and insecure.

## 7.3.0.0 Usability

- The reason for the inability to delete the account should be immediately clear to the user via the disabled state and tooltip, preventing user confusion and support requests.

## 7.4.0.0 Accessibility

- The implementation must adhere to WCAG 2.1 Level AA standards, specifically for disabled interactive elements.

## 7.5.0.0 Compatibility

- The UI behavior must be consistent across all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires a minor change to the User entity in the database.
- Involves a simple conditional check in the backend service.
- Requires a conditional property (`disabled`) on a frontend component.

## 8.3.0.0 Technical Risks

- If the mechanism for identifying the primary admin (from US-003) is not robust, this feature could fail. The flag must be non-nullable and correctly set during installation.

## 8.4.0.0 Integration Points

- User deletion logic in the backend service/repository.
- User deletion endpoint in the REST API controller.
- User list component in the React frontend.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Unit Test: User service throws a specific exception when attempting to delete a user model where `IsPrimary` is true.
- Unit Test: User service does NOT throw an exception for a user where `IsPrimary` is false.
- Integration Test: API `DELETE /api/v1/users/{id}` returns 403 when the ID belongs to the primary admin.
- Integration Test: API `DELETE /api/v1/users/{id}` returns 204 (or 200) when the ID is for a non-primary admin.
- E2E Test: Load the user management page and verify the delete button for the primary admin is disabled and has a tooltip.
- E2E Test: Verify the delete button for a secondary admin is enabled and functional.

## 9.3.0.0 Test Data Needs

- A test database seeded with at least two users with the 'Administrator' role: one flagged as primary, one not.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- ASP.NET Core Test Host for integration tests.
- Jest/React Testing Library for frontend E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for the backend service logic and passing with >80% coverage for the modified code
- Integration testing for the API endpoint completed successfully
- User interface reviewed and approved, including the disabled state and tooltip
- Security requirement of server-side enforcement is validated
- Accessibility requirements for the disabled button are met
- Documentation updated if any user-facing changes are significant
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

1

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a system integrity story and should be prioritized early. It has dependencies on the core user deletion functionality.

## 11.4.0.0 Release Impact

This is a critical safeguard required for a stable V1.0 release. It prevents a high-severity support issue.

