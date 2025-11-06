# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-086 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Configure report access permissions for user roles |
| As A User Story | As an Administrator, I want to assign specific use... |
| User Persona | Administrator |
| Business Value | Enforces the principle of least privilege for repo... |
| Functional Area | Access Control & Security |
| Story Theme | User & Access Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Administrator grants a role access to a report

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

An Administrator is logged in and is editing a report configuration named 'Quarterly Sales Performance'.

### 3.1.5 When

The Administrator navigates to the permissions section, selects the 'Viewer' role from a list of available roles, and saves the configuration.

### 3.1.6 Then

The system associates the 'Viewer' role with the 'Quarterly Sales Performance' report configuration. A user with the 'Viewer' role will now be able to see and access generated instances of this report in the Report Viewer.

### 3.1.7 Validation Notes

Verify in the database that a record exists linking the report configuration ID and the role ID. Verify through UI testing by logging in as a 'Viewer' user.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

A user without permission is denied access

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

A report 'Executive Payroll Summary' is configured to be accessible only by the 'Administrator' role.

### 3.2.5 When

A user with the 'Viewer' role logs in and navigates to the Report Viewer.

### 3.2.6 Then

The 'Executive Payroll Summary' report and its generated instances are not listed in the UI. Any direct API request to access the report's data by this user must return an HTTP 403 Forbidden status.

### 3.2.7 Validation Notes

Perform an E2E test by logging in as the 'Viewer' user and checking the UI. Use an API client to attempt a direct GET request to the report's result endpoint and assert a 403 response.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Default permissions for a newly created report

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

An Administrator creates a new report configuration.

### 3.3.5 When

The Administrator saves the report without explicitly assigning any access permissions to other roles.

### 3.3.6 Then

The report is accessible only to users with the 'Administrator' role by default.

### 3.3.7 Validation Notes

After creating the report, log in as a 'Viewer' user and confirm the report is not visible.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Administrator role has implicit, non-revocable access

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

An Administrator is editing the access permissions for any report.

### 3.4.5 When

The Administrator views the list of roles in the permissions UI.

### 3.4.6 Then

The 'Administrator' role is displayed as selected and is disabled (read-only), preventing the Administrator from accidentally revoking their own access.

### 3.4.7 Validation Notes

Verify in the UI that the checkbox or toggle for the 'Administrator' role is checked and grayed out.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Permissions are updated when a role is deleted

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

A report 'Marketing Leads' has been granted access to the 'Marketing Team' role.

### 3.5.5 When

An Administrator deletes the 'Marketing Team' role from the system.

### 3.5.6 Then

The permission grant for the 'Marketing Team' role is automatically removed from the 'Marketing Leads' report configuration.

### 3.5.7 Validation Notes

Check the database to ensure the association record is deleted. Verify that users who were previously in that role can no longer access the report.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

User with multiple roles gets access if one role is permitted

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

A user belongs to two roles: 'Viewer' and 'Sales Team'.

### 3.6.5 And

A report 'Sales Pipeline' has been granted access only to the 'Sales Team' role.

### 3.6.6 When

The user logs in and accesses the Report Viewer.

### 3.6.7 Then

The user can see and access the 'Sales Pipeline' report because their 'Sales Team' role grants them permission.

### 3.6.8 Validation Notes

Set up the user, roles, and permissions, then perform a UI test to confirm visibility.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated section or tab within the Report Configuration page labeled 'Access Permissions'.
- A multi-select component (e.g., list of checkboxes) to display all available user roles.
- A read-only, pre-selected indicator for the 'Administrator' role.
- A tooltip or help text explaining the default permission behavior (Admin-only).

## 4.2.0 User Interactions

- Administrator can check/uncheck roles to grant/revoke access.
- The state of the selections is persisted only when the main report configuration form is saved.

## 4.3.0 Display Requirements

- The list of roles must be fetched dynamically from the system's user management module.
- The UI must clearly indicate that 'Administrator' access is implicit and cannot be changed.

## 4.4.0 Accessibility Needs

- The multi-select component for roles must be fully keyboard accessible (navigable with Tab, selectable with Spacebar).
- All labels and instructions must be properly associated with their form controls for screen reader compatibility, meeting WCAG 2.1 Level AA.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

The 'Administrator' role shall have implicit read access to all reports, regardless of explicit permission settings.

### 5.1.3 Enforcement Point

Backend API Authorization Layer

### 5.1.4 Violation Handling

This rule is enforced by design; it cannot be violated through the UI.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

If no roles are explicitly granted access to a report, access is restricted to the 'Administrator' role only.

### 5.2.3 Enforcement Point

Report Configuration Save Logic & API Authorization

### 5.2.4 Violation Handling

N/A - This is the default secure behavior.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

Access is granted if a user belongs to at least one of the roles assigned to the report.

### 5.3.3 Enforcement Point

Backend API Authorization Layer

### 5.3.4 Violation Handling

N/A - This defines the permission evaluation logic.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-019

#### 6.1.1.2 Dependency Reason

The system must support the concept of user roles and assignment of users to roles before permissions can be configured for those roles.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-051

#### 6.1.2.2 Dependency Reason

The UI for configuring permissions must be integrated into the report creation and editing workflow.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-078

#### 6.1.3.2 Dependency Reason

The Report Viewer's backend API must be updated to enforce the permissions configured by this story.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity for role management.
- Entity Framework Core for database schema migrations (requires a new join table between Reports and Roles).

## 6.3.0.0 Data Dependencies

- A list of existing user roles must be available to populate the UI.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to list reports for a user must remain performant (<200ms P95 latency), even with many reports and complex permission schemes. This requires efficient database queries.

## 7.2.0.0 Security

- All permission checks MUST be enforced on the backend API. Client-side hiding of reports is insufficient.
- Direct access to report artifacts via URL must be protected by the same role-based authorization check.

## 7.3.0.0 Usability

- The interface for managing permissions should be intuitive, making it clear which roles have access and how to change it.

## 7.4.0.0 Accessibility

- The UI must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must work correctly on all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires database schema changes and data migration.
- Requires coordinated changes across the backend (API authorization, service layer) and frontend (new UI component).
- Authorization logic must be carefully implemented to avoid security loopholes.

## 8.3.0.0 Technical Risks

- Risk of inefficient database queries causing performance degradation in the Report Viewer.
- Risk of incorrectly implemented authorization logic leading to data leaks.

## 8.4.0.0 Integration Points

- Backend: ASP.NET Core Authorization policies.
- Database: New `ReportRolePermission` join table.
- Frontend: Report configuration form in the React application.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify an Admin can grant and revoke access for a role.
- Verify a user with a permitted role can see the report.
- Verify a user without a permitted role cannot see the report or access it via a direct link (403 error).
- Verify default permissions on a new report are Admin-only.
- Verify deleting a role cleans up its permissions.
- Verify implicit Admin access cannot be removed.

## 9.3.0.0 Test Data Needs

- Multiple user accounts with different role assignments (e.g., Admin, Viewer, Viewer+Sales, Sales-only).
- Multiple report configurations with varying permission settings.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend component tests.
- An E2E testing framework (e.g., Playwright or Cypress) for workflow validation.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for backend and frontend logic, achieving >80% coverage
- Integration testing for the API authorization logic is completed and passing
- E2E tests for the complete user workflow are implemented and passing
- User interface for permission management is reviewed and approved by UX/Product Owner
- Performance of the Report Viewer list API is verified under load
- Security review confirms backend enforcement of permissions
- User Guide documentation for Administrators is updated to explain the feature
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a prerequisite for making the Report Viewer generally available to non-administrator users.
- Ensure prerequisite stories (US-019, US-051) are completed in a prior sprint or early in the same sprint.

## 11.4.0.0 Release Impact

- This is a critical feature for any release targeting multi-user environments or deployments handling sensitive data.

