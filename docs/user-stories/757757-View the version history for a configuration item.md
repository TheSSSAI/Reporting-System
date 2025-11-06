# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-098 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | View the version history for a configuration item |
| As A User Story | As an Administrator, I want to view a chronologica... |
| User Persona | Administrator |
| Business Value | Provides auditability and traceability for configu... |
| Functional Area | Configuration Management |
| Story Theme | Configuration Versioning & Rollback |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Viewing history for a configuration with multiple versions

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

an Administrator is logged in and is viewing the edit page for a Connector configuration that has been saved at least twice

### 3.1.5 When

the Administrator clicks the 'View History' button

### 3.1.6 Then

a modal or view appears displaying a list of all saved versions for that connector.

### 3.1.7 And

each item in the list clearly displays the Version Number, the Timestamp of the save (in a human-readable format), and the Username of the administrator who saved it.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Viewing history for a newly created configuration

### 3.2.3 Scenario Type

Edge_Case

### 3.2.4 Given

an Administrator has just created and saved a new Report configuration for the first time

### 3.2.5 When

the Administrator clicks the 'View History' button on the edit page

### 3.2.6 Then

the version history view appears and displays a single entry for 'Version 1'.

### 3.2.7 And

the entry shows the correct timestamp and the current administrator's username.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Feature is available for all versioned configuration types

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

an Administrator is logged in

### 3.3.5 When

the Administrator navigates to the edit page for a Connector, a Transformation Script, and a Report Configuration

### 3.3.6 Then

each of these pages contains a clearly visible 'View History' button or equivalent UI element.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

History displays information for a deleted user

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

an Administrator is viewing the version history for a configuration item

### 3.4.5 And

the username field for that entry displays the stored username, possibly with an indicator like '(deleted)'.

### 3.4.6 When

the version history list is displayed

### 3.4.7 Then

the entry for the change made by the deleted user is still present in the list.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Closing the version history view

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

the version history modal or view is open

### 3.5.5 When

the Administrator clicks the 'Close' button or presses the 'Escape' key

### 3.5.6 Then

the version history view is dismissed, and the user is returned to the configuration edit page.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A 'View History' button or link, prominently placed on the edit pages for Connectors, Transformations, and Reports.
- A modal dialog or dedicated view to display the version history.
- A list or table within the modal to display version entries.
- A 'Close' button or 'X' icon to dismiss the history view.

## 4.2.0 User Interactions

- Clicking the 'View History' button opens the history modal.
- The list of versions should be scrollable if it exceeds the view's height.
- Clicking 'Close' or pressing 'Escape' closes the modal.

## 4.3.0 Display Requirements

- The history view must display a list of versions.
- Each version entry must show: Version Number, Timestamp, and Username.
- The list must be sorted with the most recent version at the top.
- If the history is empty (e.g., for an unsaved new configuration), a message like 'No version history available.' should be displayed.

## 4.4.0 Accessibility Needs

- The history modal must be keyboard navigable (focusable elements, close with Escape key).
- The version list must be structured semantically (e.g., using `<ul>` and `<li>` or a `<table>` with headers) for screen reader compatibility.
- All UI controls must meet WCAG 2.1 AA contrast requirements.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A new version of a configuration item (Connector, Transformation, Report) is created automatically every time it is saved.

### 5.1.3 Enforcement Point

Backend save operation for the configuration entities.

### 5.1.4 Violation Handling

If a version cannot be created, the save operation must fail and return an error to the user.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The username of the modifying user must be captured and stored with the version record at the time of the save.

### 5.2.3 Enforcement Point

Backend save operation.

### 5.2.4 Violation Handling

The save operation should not proceed if the user context cannot be determined.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-034

#### 6.1.1.2 Dependency Reason

Requires the ability to create and save Connector configurations to have a history to view.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-043

#### 6.1.2.2 Dependency Reason

Requires the ability to create and save Transformation scripts to have a history to view.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-051

#### 6.1.3.2 Dependency Reason

Requires the ability to create and save Report configurations to have a history to view.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity for retrieving the current user's information.
- Entity Framework Core data model that supports versioning of configuration entities as per SRS 3.2.1.
- A reusable frontend component (React/MUI) for displaying the history list.

## 6.3.0.0 Data Dependencies

- Requires the existence of versioned configuration data in the SQLite database.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API endpoint for fetching version history must respond with a P95 latency of under 200ms for configurations with up to 100 versions.
- The UI should render the history modal and list within 500ms of the user's click.

## 7.2.0.0 Security

- Access to the version history feature must be restricted to users with the 'Administrator' role.
- The API endpoint must be protected and require a valid JWT from an authenticated Administrator.

## 7.3.0.0 Usability

- The feature should be easily discoverable from the main configuration editing screens.
- The information presented in the history view must be clear, concise, and easy to understand.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires a new API endpoint for each of the three configuration types (`/api/v1/{configType}/{id}/history`).
- Requires a robust data model to store version snapshots, timestamps, and user information.
- The username should be denormalized and stored on the version record to handle cases where the original user account is deleted.
- Development of a reusable frontend component to be used across three different parts of the application.

## 8.3.0.0 Technical Risks

- Potential for performance degradation if a configuration has an extremely large number of versions (e.g., thousands). The API should be designed with potential pagination in mind for future-proofing.
- Ensuring the configuration snapshot is captured correctly and is a true representation of the saved state.

## 8.4.0.0 Integration Points

- Frontend: Integrates into the edit pages for Connectors, Transformations, and Reports.
- Backend: Integrates with the data access layer to query the version history tables from the SQLite database.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify history for a new, unsaved configuration (empty state).
- Verify history for a configuration with one version.
- Verify history for a configuration with many versions.
- Verify sorting order is correct (descending by date).
- Verify all required data points (version, user, timestamp) are displayed correctly.
- Verify the feature works for all three configuration types (Connector, Transformation, Report).
- Verify behavior when the modifying user has been deleted.
- Verify UI responsiveness and keyboard navigation.

## 9.3.0.0 Test Data Needs

- A configuration item with no saved versions.
- A configuration item with a single saved version.
- A configuration item with 10+ versions saved by at least two different users.
- A version record where the associated user ID points to a deleted user.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend component tests.
- Playwright or Cypress for E2E tests.
- Axe for accessibility scanning.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for backend and frontend with >= 80% coverage
- Integration testing completed successfully for the API-UI interaction
- E2E test scenario for viewing history is implemented and passing
- User interface reviewed and approved for usability and consistency
- Performance requirements verified under test conditions
- Security requirements validated (role-based access)
- Accessibility audit passed (WCAG 2.1 AA)
- API endpoint is documented in the OpenAPI specification
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🟡 Medium

## 11.3.0.0 Sprint Considerations

- This story is a foundational piece for the versioning feature set. It should be prioritized before US-099 (Diff View) and US-100 (Restore Version), as those stories will build upon the UI and data access established here.

## 11.4.0.0 Release Impact

This is a significant enhancement to the system's manageability and audit capabilities. It should be highlighted in release notes as a key feature for administrators.

