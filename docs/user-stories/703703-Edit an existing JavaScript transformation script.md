# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-044 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Edit an existing JavaScript transformation script |
| As A User Story | As an Administrator, I want to edit and save chang... |
| User Persona | Administrator: A technical user responsible for co... |
| Business Value | Enables the maintenance and evolution of data proc... |
| Functional Area | Data Transformation |
| Story Theme | Configuration Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Load an existing script into the editor

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an Administrator logged into the Control Panel and am on the 'Transformations' list page

### 3.1.5 When

I click the 'Edit' action for an existing script named 'Format Customer Data'

### 3.1.6 Then

I am navigated to the script editor page, which is pre-populated with the content of the current active version of the 'Format Customer Data' script.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Successfully edit and save a script with valid changes

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am on the script editor page for the 'Format Customer Data' script

### 3.2.5 When

I modify the JavaScript code and click the 'Save' button

### 3.2.6 Then

The system validates the syntax, saves the changes, and creates a new version of the script, making it the new active version.

### 3.2.7 And

The 'Last Modified' timestamp for the 'Format Customer Data' script is updated on the list page.

### 3.2.8 Validation Notes

Verify in the database that a new version record has been created and the parent record's active version pointer has been updated.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempt to save a script with invalid JavaScript syntax

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am on the script editor page for a script

### 3.3.5 When

I modify the code, introducing a syntax error (e.g., a missing curly brace), and click 'Save'

### 3.3.6 Then

The system prevents the save operation.

### 3.3.7 And

I remain on the editor page with my invalid code still present, allowing me to correct it.

### 3.3.8 Validation Notes

This relies on the functionality from US-045. The save button should trigger the validation logic.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Cancel editing a script with unsaved changes

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

I am on the script editor page and have made changes to the code

### 3.4.5 When

I click the 'Cancel' button

### 3.4.6 Then

A confirmation dialog appears asking 'Are you sure you want to discard your changes?'.

### 3.4.7 And

If I confirm, my changes are discarded, and I am returned to the 'Transformations' list page.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Cancel editing a script with no changes

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

I am on the script editor page but have not made any changes to the code

### 3.5.5 When

I click the 'Cancel' button

### 3.5.6 Then

I am returned to the 'Transformations' list page without a confirmation prompt.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Saving a script with no changes does not create a new version

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

I am on the script editor page for a script

### 3.6.5 When

I click the 'Save' button without making any modifications to the code

### 3.6.6 Then

The system shows a success message (or no message) and returns me to the list page.

### 3.6.7 And

A new version of the script is NOT created in the version history.

### 3.6.8 Validation Notes

Check the version count for the script in the database before and after the save action to ensure it has not increased.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- An 'Edit' button or icon next to each script in the 'Transformations' list.
- A code editor component (e.g., Monaco, CodeMirror) with JavaScript syntax highlighting.
- A 'Save' button to commit changes.
- A 'Cancel' button to discard changes and return to the list.
- A confirmation modal for discarding unsaved changes.
- A visible title on the editor page indicating which script is being edited.

## 4.2.0 User Interactions

- Clicking 'Edit' loads the script into the editor.
- Typing in the editor modifies the script content.
- Clicking 'Save' triggers validation and persists the changes.
- Clicking 'Cancel' initiates the exit flow.

## 4.3.0 Display Requirements

- The editor must be pre-populated with the content of the selected script's active version.
- Error messages for failed saves must be clear and actionable.

## 4.4.0 Accessibility Needs

- The code editor must be keyboard-accessible.
- All buttons ('Save', 'Cancel') must have accessible labels.
- Error messages must be associated with the editor field for screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Saving an edited configuration item must create a new version.

### 5.1.3 Enforcement Point

Backend API endpoint for updating a transformation script.

### 5.1.4 Violation Handling

The save operation should fail if a new version cannot be created. The entire operation must be transactional.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A transformation script cannot be saved if it contains invalid JavaScript syntax.

### 5.2.3 Enforcement Point

Backend API endpoint, prior to database commit.

### 5.2.4 Violation Handling

The API should return an HTTP 400 Bad Request error with a descriptive message about the syntax error.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-043

#### 6.1.1.2 Dependency Reason

The UI and backend for creating a script, including the code editor component, must exist before an edit function can be built upon it.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-045

#### 6.1.2.2 Dependency Reason

The save functionality requires the syntax validation logic to prevent saving invalid scripts.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-098

#### 6.1.3.2 Dependency Reason

While not a strict blocker, this story creates the version data that US-098 (View Version History) will display. They are part of the same feature set.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity for role-based access control.
- Jint library for server-side JavaScript syntax validation.
- Entity Framework Core for database transactions and versioning logic.
- React-based code editor component for the frontend.

## 6.3.0.0 Data Dependencies

- Requires existing transformation script records in the database to edit.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- Loading a script into the editor should take less than 1 second.
- Saving a script (including validation) should complete in under 500ms.

## 7.2.0.0 Security

- Only users with the 'Administrator' role can access the edit functionality.
- The API endpoint for updating scripts must be protected and require authentication and authorization.
- Input is not directly rendered as HTML, but the script content should be handled carefully to prevent any unforeseen injection vectors.

## 7.3.0.0 Usability

- Syntax error messages should be user-friendly and help the administrator locate the problem.
- The process of editing, saving, and returning to the list should be smooth and intuitive.

## 7.4.0.0 Accessibility

- The editor interface must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The editor UI must function correctly on the latest stable versions of Chrome, Firefox, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Backend logic for creating a new version while updating the parent entity within a single database transaction.
- Integration with the Jint library for server-side validation on save.
- Frontend state management to handle unsaved changes and the 'discard changes' confirmation flow.

## 8.3.0.0 Technical Risks

- Potential for race conditions if two administrators attempt to edit and save the same script simultaneously. A warning mechanism for concurrent edits should be considered if this is a high-risk scenario.
- Ensuring the database transaction for versioning is robust and handles rollbacks correctly on failure.

## 8.4.0.0 Integration Points

- API Endpoint: `PUT /api/v1/transformations/{id}`
- Database: `TransformationScripts` and a related versioning table (e.g., `TransformationScriptVersions`).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify an admin can successfully edit and save a script, creating a new version.
- Verify an admin is blocked from saving a script with syntax errors.
- Verify the 'cancel' and 'discard changes' confirmation flow works as expected.
- Verify that a user without the 'Administrator' role receives a 403 Forbidden error when attempting to access the edit page or call the update API.
- Verify that reports using the edited script pick up the new logic on their next run.

## 9.3.0.0 Test Data Needs

- A user account with the 'Administrator' role.
- At least one pre-existing transformation script in the database.
- Sample JavaScript code snippets, both valid and invalid, for testing.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend component tests.
- Playwright or Cypress for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for backend and frontend logic, achieving >80% coverage
- Integration testing for the API endpoint and database transaction completed successfully
- E2E test scenario for the edit-and-save flow is implemented and passing
- User interface reviewed for usability and adherence to design standards
- Security requirements (RBAC) validated
- Documentation for the versioning feature is updated
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story should be scheduled in a sprint after US-043 (Create Script) is completed.
- The backend developer needs to coordinate with the database schema design for versioning.
- The frontend developer can reuse the code editor component from US-043.

## 11.4.0.0 Release Impact

This is a core feature for configuration management and is essential for making the system maintainable for administrators.

