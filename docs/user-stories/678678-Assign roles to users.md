# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-019 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Assign roles to users |
| As A User Story | As an Administrator, I want to assign or change th... |
| User Persona | Administrator: A user with full CRUD access to all... |
| Business Value | Enforces the principle of least privilege, enhanci... |
| Functional Area | User & Access Management |
| Story Theme | System Administration |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Promote a 'Viewer' to an 'Administrator'

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an Administrator logged into the Control Panel and viewing the details of a user who currently has the 'Viewer' role.

### 3.1.5 When

I change the user's role to 'Administrator' using the role selection control and click the 'Save' button.

### 3.1.6 Then

A success notification is displayed, the user's role is updated to 'Administrator' in the system, and an audit log entry is created for the change.

### 3.1.7 Validation Notes

Verify via the user list UI that the role is updated. Check the AuditLog table in the database for a new entry detailing the change.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Demote an 'Administrator' to a 'Viewer'

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am an Administrator logged into the Control Panel and viewing the details of another user who currently has the 'Administrator' role.

### 3.2.5 When

I change the user's role to 'Viewer' using the role selection control and click the 'Save' button.

### 3.2.6 Then

A success notification is displayed, the user's role is updated to 'Viewer' in the system, and an audit log entry is created for the change.

### 3.2.7 Validation Notes

Verify by logging in as the affected user that they no longer have access to administrative functions. Check the AuditLog table for the change record.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempt to demote the last remaining Administrator

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

I am logged in as the only user with the 'Administrator' role and I am on my own user edit page.

### 3.3.5 When

I attempt to change my own role to 'Viewer' and click the 'Save' button.

### 3.3.6 Then

The system must display an error message stating 'The last administrator account cannot be demoted', the change is not saved, and my role remains 'Administrator'.

### 3.3.7 Validation Notes

Set up test data with only one administrator. Attempt the action and verify the error message and that the user's role in the database is unchanged.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Cancel a role change before saving

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

I am an Administrator on a user's edit page and the user's current role is 'Viewer'.

### 3.4.5 When

I change the role selection to 'Administrator' but then click the 'Cancel' button or navigate away from the page without saving.

### 3.4.6 Then

The system does not save the change, and the user's role remains 'Viewer'.

### 3.4.7 Validation Notes

Perform the action, then refresh the page or navigate back to the user list to confirm the role has not changed.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Role change is recorded in the audit log

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

An Administrator has successfully changed a user's role from 'Viewer' to 'Administrator'.

### 3.5.5 When

Another Administrator views the system's audit log.

### 3.5.6 Then

A new audit log entry is present that includes the timestamp, the acting administrator's username, the target user's username, the action performed (e.g., 'User Role Updated'), the old value ('Viewer'), and the new value ('Administrator').

### 3.5.7 Validation Notes

Check the audit log UI and the underlying AuditLog table in the database to confirm the entry is created with all required details.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dropdown menu or radio button group on the user edit page to select a role.
- A 'Save' button to commit the role change.
- A 'Cancel' button to discard changes.
- Toast notifications for success and error messages.

## 4.2.0 User Interactions

- Administrator selects a user from the user list to navigate to their edit page.
- Administrator selects a new role from the available options.
- Administrator clicks 'Save' to apply the change.
- The UI should provide immediate feedback (success/error notification) upon save attempt.

## 4.3.0 Display Requirements

- The user's current role must be clearly displayed and pre-selected in the role selection control.
- The list of available roles ('Administrator', 'Viewer') must be populated in the selection control.

## 4.4.0 Accessibility Needs

- The role selection control must have a proper label (`<label for=... >`).
- All interactive elements (buttons, dropdown) must be keyboard accessible and have clear focus indicators, adhering to WCAG 2.1 AA.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'The system must always have at least one active Administrator account.', 'enforcement_point': 'On the server-side, before committing a user role change to the database.', 'violation_handling': 'The operation is rejected, and an error message is returned to the user explaining why the action is not permitted.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-018

#### 6.1.1.2 Dependency Reason

A user account must exist before a role can be assigned to it.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-020

#### 6.1.2.2 Dependency Reason

This functionality will be part of the user editing interface, which is defined in this story.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-101

#### 6.1.3.2 Dependency Reason

The audit logging framework must be in place to record the role change event.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity for user and role management.
- Entity Framework Core for database interaction.
- A defined API endpoint (e.g., PUT /api/v1/users/{id}) for updating user details.
- React UI component for user editing.

## 6.3.0.0 Data Dependencies

- Requires the existence of `User`, `Role`, and `UserRole` tables in the SQLite database schema.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to update a user's role should complete in under 500ms (P95).

## 7.2.0.0 Security

- Only users with the 'Administrator' role can access the functionality to change another user's role.
- The API endpoint for this action must be protected and require Administrator-level authorization.
- All role change events must be logged in the tamper-evident audit log as per SRS 6.4.

## 7.3.0.0 Usability

- The process of changing a role should be intuitive and require no more than three clicks from the user list page.

## 7.4.0.0 Accessibility

- The user management interface must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- Functionality must be verified on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Leverages standard features of ASP.NET Core Identity, which simplifies backend logic.
- The primary complexity is the server-side validation to prevent the demotion of the last administrator.
- Frontend work is a straightforward form modification.

## 8.3.0.0 Technical Risks

- The 'last administrator' check must be implemented within a database transaction to prevent potential race conditions.

## 8.4.0.0 Integration Points

- Backend: ASP.NET Core Identity's UserManager.
- Database: SQLite via Entity Framework Core.
- Frontend: React user management component.
- Logging: Serilog for audit logging.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify a Viewer can be promoted to Administrator.
- Verify an Administrator can be demoted to Viewer.
- Verify the last Administrator cannot be demoted.
- Verify that a logged-in user's permissions are updated upon their next session/token refresh after a role change.
- Verify that only Administrators can access this functionality.
- Verify the audit log is correctly written for every successful role change.

## 9.3.0.0 Test Data Needs

- A user account with the 'Administrator' role for testing.
- A second user account with the 'Administrator' role.
- A user account with the 'Viewer' role.

## 9.4.0.0 Testing Tools

- xUnit and Moq for backend unit tests.
- Jest and React Testing Library for frontend unit tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >80% coverage
- Integration testing completed successfully
- User interface reviewed and approved by the Product Owner
- Security requirements (authorization, auditing) validated
- Documentation for the user management feature is updated
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational security feature and should be prioritized early in the development of administration capabilities.
- Dependent on the completion of basic user creation and editing stories (US-018, US-020).

## 11.4.0.0 Release Impact

Critical for the initial release as it enables basic security and user segmentation.

