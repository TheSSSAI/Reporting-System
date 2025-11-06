# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-052 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Define metadata for a report configuration |
| As A User Story | As an Administrator, I want to provide a unique, d... |
| User Persona | Administrator: This user is responsible for creati... |
| Business Value | Improves system usability and manageability by all... |
| Functional Area | Report Configuration Management |
| Story Theme | Report Configuration & Scheduling |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successfully create a new report with a unique name and description

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an Administrator on the 'Create New Report' page

### 3.1.5 When

I enter a unique value in the 'Report Name' field, an optional value in the 'Description' field, and successfully complete the rest of the configuration

### 3.1.6 Then

the report configuration is saved, and the new report appears in the list of reports with its specified name.

### 3.1.7 Validation Notes

Verify the new record exists in the 'ReportConfiguration' table in the database with the correct name and description values.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Attempt to save a report configuration without a name

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

I am an Administrator on the 'Create New Report' page

### 3.2.5 When

I leave the 'Report Name' field blank and attempt to save the configuration

### 3.2.6 Then

the save operation must fail, and a clear validation message such as 'Report Name is required' is displayed next to the field.

### 3.2.7 Validation Notes

Check that the form submission is blocked client-side and that the API returns a 400 Bad Request error if the validation is bypassed.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempt to save a report configuration with a duplicate name

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

a report named 'Monthly Financial Summary' already exists and I am an Administrator on the 'Create New Report' page

### 3.3.5 When

I enter 'Monthly Financial Summary' in the 'Report Name' field and attempt to save the configuration

### 3.3.6 Then

the save operation must fail, and a clear validation message such as 'A report with this name already exists' is displayed.

### 3.3.7 Validation Notes

The uniqueness check should be case-insensitive. The API must return a 409 Conflict error.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Successfully edit the name and description of an existing report

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

I am an Administrator editing an existing report configuration

### 3.4.5 When

I modify the values in the 'Report Name' and 'Description' fields and save the changes

### 3.4.6 Then

the configuration is updated successfully, and the report list reflects the new name.

### 3.4.7 Validation Notes

Verify the corresponding record in the 'ReportConfiguration' table is updated in the database.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Input fields are pre-populated when editing a report

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

I am an Administrator

### 3.5.5 When

I open an existing report configuration for editing

### 3.5.6 Then

the 'Report Name' and 'Description' fields are pre-populated with their currently saved values.

### 3.5.7 Validation Notes

Confirm the data is fetched and correctly bound to the form fields upon component load.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Input fields enforce character limits

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

I am an Administrator creating or editing a report configuration

### 3.6.5 When

I attempt to enter more than 100 characters in the 'Report Name' field or more than 500 characters in the 'Description' field

### 3.6.6 Then

the UI prevents me from typing additional characters, and if submitted via API, a validation error is returned.

### 3.6.7 Validation Notes

Test both UI behavior (e.g., `maxlength` attribute) and backend API validation.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A text input field labeled 'Report Name'.
- A multi-line textarea field labeled 'Description'.
- Validation message display areas associated with each field.

## 4.2.0 User Interactions

- The 'Report Name' field must be marked as a required field in the UI.
- The 'Description' field should be marked as optional.
- Real-time validation feedback (e.g., on blur) for required and duplicate name checks is desirable.
- In report list views, the full description should be available, for example, via a tooltip on hover if truncated.

## 4.3.0 Display Requirements

- The report name must be prominently displayed in any list or table of report configurations.
- The description should be accessible from the report list view to provide context.

## 4.4.0 Accessibility Needs

- Both 'Report Name' and 'Description' input fields must have correctly associated `<label>` elements.
- The 'Report Name' field must have the `aria-required="true"` attribute.
- Validation errors must be programmatically associated with their respective input fields using `aria-describedby`.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A report configuration must have a name.

### 5.1.3 Enforcement Point

Client-side form validation and server-side API validation upon create/update.

### 5.1.4 Violation Handling

Prevent saving the configuration and display a user-friendly error message.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Report configuration names must be unique within the system (case-insensitive).

### 5.2.3 Enforcement Point

Server-side API validation upon create/update.

### 5.2.4 Violation Handling

Prevent saving the configuration and display a user-friendly error message.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

Report Name is limited to a maximum of 100 characters.

### 5.3.3 Enforcement Point

Client-side input constraints and server-side API validation.

### 5.3.4 Violation Handling

Prevent input of excess characters and reject API requests that exceed the limit.

## 5.4.0 Rule Id

### 5.4.1 Rule Id

BR-004

### 5.4.2 Rule Description

Report Description is limited to a maximum of 500 characters.

### 5.4.3 Enforcement Point

Client-side input constraints and server-side API validation.

### 5.4.4 Violation Handling

Prevent input of excess characters and reject API requests that exceed the limit.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

- {'story_id': 'US-051', 'dependency_reason': 'This story adds fields to the report creation/editing interface that is established by US-051. The UI context is required.'}

## 6.2.0 Technical Dependencies

- The backend data model `ReportConfiguration` must be extended to include 'Name' and 'Description' properties.
- An Entity Framework Core migration is required to update the SQLite database schema.

## 6.3.0 Data Dependencies

*No items available*

## 6.4.0 External Dependencies

*No items available*

# 7.0.0 Non Functional Requirements

## 7.1.0 Performance

- The duplicate name check on the server should complete in under 200ms.

## 7.2.0 Security

- All user-provided input for 'Name' and 'Description' must be properly sanitized on the server-side before being stored or rendered to prevent Cross-Site Scripting (XSS) vulnerabilities.

## 7.3.0 Usability

- Error messages for validation failures must be clear, concise, and displayed in close proximity to the input field causing the error.

## 7.4.0 Accessibility

- The UI must adhere to WCAG 2.1 Level AA standards, particularly concerning form labels and error feedback.

## 7.5.0 Compatibility

- Functionality must be consistent across the latest stable versions of Chrome, Firefox, and Edge.

# 8.0.0 Implementation Considerations

## 8.1.0 Complexity Assessment

Low

## 8.2.0 Complexity Factors

- Requires coordinated changes across frontend, backend, and database.
- Frontend: Add two form fields with validation to an existing React component.
- Backend: Add two properties to a C# model/DTO, add validation attributes, and update the service logic.
- Database: Create and apply a simple EF Core migration.

## 8.3.0 Technical Risks

- Risk of migration failure if not handled correctly during deployment.
- Potential for race conditions if two users attempt to create a report with the same name simultaneously, though this is a very low risk.

## 8.4.0 Integration Points

- Frontend report configuration form.
- Backend API endpoint for creating/updating report configurations (`POST /api/v1/reports`, `PUT /api/v1/reports/{id}`).
- SQLite database `ReportConfiguration` table.

# 9.0.0 Testing Requirements

## 9.1.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0 Test Scenarios

- Verify successful creation and editing of report metadata.
- Test all validation rules: required name, unique name, and character limits.
- Confirm that input sanitization prevents a basic XSS payload from being executed.
- Verify accessibility features for the form fields using automated tools and manual checks.

## 9.3.0 Test Data Needs

- A set of existing report configurations to test the duplicate name validation.
- Test strings that are at, below, and above the character limits.
- A sample XSS payload string (e.g., `<script>alert('xss')</script>`) for security testing.

## 9.4.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- Playwright or Cypress for E2E tests.

# 10.0.0 Definition Of Done

- All acceptance criteria validated and passing.
- Code reviewed and approved by team.
- Unit tests for backend validation and frontend component behavior implemented and passing with >80% coverage for new code.
- Integration testing for the API endpoint completed successfully.
- User interface changes reviewed and approved for usability and accessibility.
- Input sanitization for security requirements validated.
- Database migration script is created and tested.
- Story deployed and verified in staging environment.

# 11.0.0 Planning Information

## 11.1.0 Story Points

2

## 11.2.0 Priority

🔴 High

## 11.3.0 Sprint Considerations

- This is a foundational story for report management and should be completed early in the development cycle.
- Must be scheduled in a sprint after or alongside its prerequisite, US-051.

## 11.4.0 Release Impact

This feature is critical for the initial release as it is fundamental to managing reports.

