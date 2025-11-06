# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-062 |
| Elaboration Date | 2025-01-17 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Upload and manage Handlebars templates |
| As A User Story | As an Administrator, I want a dedicated interface ... |
| User Persona | Administrator |
| Business Value | Enables the creation of customized, branded, and v... |
| Functional Area | Configuration Management |
| Story Theme | Report Generation & Templating |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

View the list of existing templates

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am logged in as an Administrator and have navigated to the 'Templates' section of the Control Panel

### 3.1.5 When

the page loads

### 3.1.6 Then

I see a table listing all previously uploaded templates, with columns for 'Template Name' and 'Last Modified Date'.

### 3.1.7 Validation Notes

Verify the API endpoint GET /api/v1/templates returns the correct list and the frontend renders it.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

View the empty state when no templates exist

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am logged in as an Administrator and have navigated to the 'Templates' section

### 3.2.5 When

no templates have been uploaded yet

### 3.2.6 Then

I see a message indicating 'No templates found.' and a prominent 'Upload Template' button.

### 3.2.7 Validation Notes

Verify the UI displays the correct empty state message when the API returns an empty array.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Successfully upload a new, valid template file

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I am on the 'Templates' page

### 3.3.5 When

I click 'Upload Template', select a valid file with a '.hbs' extension, and confirm the upload

### 3.3.6 Then

a success notification is displayed, and the new template appears in the list without a page refresh.

### 3.3.7 Validation Notes

Test with a valid .hbs file. Verify the file is stored on the server's file system and a corresponding record is in the database.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

View and edit the content of a template

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

I am on the 'Templates' page

### 3.4.5 When

I click the 'Edit' action for a template

### 3.4.6 Then

I am taken to a new view containing a code editor that displays the full content of the template with Handlebars syntax highlighting.

### 3.4.7 Validation Notes

Verify the editor component (e.g., Monaco) loads and correctly displays the template content fetched from the backend.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Save changes to a template's content

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

I am editing a template's content in the code editor

### 3.5.5 When

I modify the content and click 'Save'

### 3.5.6 Then

a success notification is displayed, the changes are persisted, and the 'Last Modified Date' for that template is updated in the list view.

### 3.5.7 Validation Notes

Verify the PUT /api/v1/templates/{id} endpoint is called and the file content on the server is updated.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Successfully delete an unused template

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

I am on the 'Templates' page and a template is not being used by any Report Configuration

### 3.6.5 When

I click the 'Delete' action for that template and confirm the action in the confirmation dialog

### 3.6.6 Then

a success notification is displayed, and the template is removed from the list.

### 3.6.7 Validation Notes

Verify the DELETE /api/v1/templates/{id} call succeeds and the template file is removed from the server.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Attempt to upload a file with an invalid extension

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

I am on the 'Templates' page

### 3.7.5 When

I attempt to upload a file with an extension other than '.hbs' (e.g., '.txt', '.exe')

### 3.7.6 Then

the upload is rejected, and an error message 'Invalid file type. Please upload a .hbs file.' is displayed.

### 3.7.7 Validation Notes

Test with various invalid file types. The check should be performed on both the client and server side.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Attempt to upload a template with a name that already exists

### 3.8.3 Scenario Type

Error_Condition

### 3.8.4 Given

a template named 'my-template.hbs' already exists

### 3.8.5 When

I attempt to upload another file named 'my-template.hbs'

### 3.8.6 Then

the upload is rejected, and an error message 'A template with this name already exists.' is displayed.

### 3.8.7 Validation Notes

The backend API should enforce name uniqueness.

## 3.9.0 Criteria Id

### 3.9.1 Criteria Id

AC-009

### 3.9.2 Scenario

Attempt to delete a template that is in use

### 3.9.3 Scenario Type

Edge_Case

### 3.9.4 Given

a template is currently assigned to one or more Report Configurations

### 3.9.5 When

I attempt to delete that template

### 3.9.6 Then

the deletion is blocked, and an error message is displayed, stating 'This template cannot be deleted because it is in use by the following reports: [Report Name 1], [Report Name 2].'

### 3.9.7 Validation Notes

The backend must check for dependencies in the ReportConfiguration table before processing the deletion.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A data table to list templates with columns for 'Template Name', 'Last Modified Date', and 'Actions'.
- An 'Upload Template' button.
- A standard file selection dialog for uploads.
- Action icons/buttons for each template: 'Edit' and 'Delete'.
- A confirmation modal for the delete action.
- A dedicated page/view for the template editor.
- A code editor component (e.g., Monaco, CodeMirror) with Handlebars syntax highlighting.
- A 'Save' button on the editor page.
- Toast notifications for success and error messages.

## 4.2.0 User Interactions

- User clicks to upload, triggering a file picker.
- User clicks 'Edit' to navigate to the editor view.
- User clicks 'Delete', which opens a confirmation dialog before proceeding.
- User types in the code editor to modify template content.

## 4.3.0 Display Requirements

- Template names should be displayed clearly.
- Dates should be formatted in a user-friendly way (e.g., 'YYYY-MM-DD HH:mm').
- Error messages must be clear and actionable.

## 4.4.0 Accessibility Needs

- All buttons and interactive elements must have accessible labels (aria-label).
- The data table must be properly structured with `<thead>`, `<tbody>`, and `<th>` scopes.
- The code editor component must be keyboard-navigable.
- Confirmation dialogs must trap focus.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Only files with a '.hbs' extension are considered valid templates.

### 5.1.3 Enforcement Point

File upload (client-side validation and server-side enforcement).

### 5.1.4 Violation Handling

The file upload is rejected, and an error message is displayed to the user.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Template names (filenames) must be unique within the system.

### 5.2.3 Enforcement Point

During file upload on the backend.

### 5.2.4 Violation Handling

The upload is rejected, and a 'duplicate name' error is returned.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

A template cannot be deleted if it is currently associated with any Report Configuration.

### 5.3.3 Enforcement Point

During the delete operation on the backend.

### 5.3.4 Violation Handling

The deletion is blocked, and an error message listing the dependent reports is returned.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-019

#### 6.1.1.2 Dependency Reason

Requires a role-based access control system to ensure only Administrators can manage templates.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-027

#### 6.1.2.2 Dependency Reason

Requires a user authentication system to log in as an Administrator.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-056

#### 6.1.3.2 Dependency Reason

This story provides the templates that US-056 consumes. The data model for how a ReportConfiguration references a template must be defined and agreed upon between both stories.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core backend for API endpoints and file handling.
- React frontend framework for the UI.
- A frontend code editor library (e.g., @monaco-editor/react).
- Server file system for storing template files.
- SQLite database with EF Core for storing template metadata.

## 6.3.0.0 Data Dependencies

- Requires access to the `ReportConfiguration` entity/table to check for usage dependencies before deletion.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- Template list page should load in under 2 seconds with up to 200 templates.
- File uploads up to 5MB should complete within 5 seconds on a standard network connection.

## 7.2.0.0 Security

- Access to the template management feature must be restricted to users with the 'Administrator' role.
- The file upload mechanism must validate file types and sanitize filenames to prevent path traversal attacks.
- Uploaded files must be stored in a secure, non-executable directory outside of the web root.
- Template content rendered in the editor must be treated as text to prevent any potential XSS vulnerabilities.

## 7.3.0.0 Usability

- The interface for managing templates should be intuitive and require minimal training.
- Error messages should be clear and help the user resolve the issue.

## 7.4.0.0 Accessibility

- The UI must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest stable versions of Chrome, Firefox, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementing secure file upload, storage, and retrieval.
- Integrating and configuring a third-party code editor component in the React frontend.
- Implementing the business logic for the dependency check before deletion.
- Requires a new database table (`Templates`) and an EF Core migration.

## 8.3.0.0 Technical Risks

- The chosen code editor library might have a steep learning curve or performance implications.
- Improper handling of file uploads could introduce security vulnerabilities.

## 8.4.0.0 Integration Points

- Backend API with endpoints for CRUD operations on templates.
- Frontend React application to consume the API.
- Database schema (new `Templates` table and a foreign key relationship from `ReportConfigurations`).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Full CRUD lifecycle of a template: Upload, view list, edit content, save, delete.
- Attempt to upload invalid file types.
- Attempt to upload a duplicate template name.
- Attempt to delete a template that is in use by one report.
- Attempt to delete a template that is in use by multiple reports.
- Verify the UI's empty state when no templates exist.

## 9.3.0.0 Test Data Needs

- Sample valid `.hbs` template files of varying sizes.
- Sample invalid files (e.g., .txt, .jpg, .exe).
- Pre-existing `ReportConfiguration` records in the database that link to a test template.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- Playwright or Cypress for E2E tests.
- Security scanning tools for vulnerability analysis of the file upload endpoint.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for backend services and frontend components, achieving >80% coverage
- Integration testing for all new API endpoints completed successfully
- E2E test for the 'delete in-use template' scenario is automated and passing
- User interface reviewed for usability and adherence to design standards
- Security review of the file handling mechanism completed and any findings addressed
- Documentation for managing templates is added to the Administrator User Guide
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational story for creating custom HTML/PDF reports. It should be prioritized before or in the same sprint as the story for selecting a template in a report configuration (US-056).

## 11.4.0.0 Release Impact

- This feature is critical for the initial release as it enables a core product capability.

