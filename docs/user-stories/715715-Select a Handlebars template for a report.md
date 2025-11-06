# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-056 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Select a Handlebars template for a report |
| As A User Story | As an Administrator configuring a report, I want t... |
| User Persona | Administrator |
| Business Value | Enables the creation of customized, branded, and h... |
| Functional Area | Report Configuration |
| Story Theme | Report Generation & Delivery |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Template selector appears for HTML output format

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

An Administrator is on the 'Create/Edit Report' page and at least one Handlebars template has been uploaded to the system

### 3.1.5 When

The Administrator selects 'HTML' from the 'Output Format' dropdown

### 3.1.6 Then

A 'Template' selector UI element (e.g., a dropdown) becomes visible and enabled

### 3.1.7 And

The 'Template' selector is populated with the names of all available, uploaded Handlebars templates.

### 3.1.8 Validation Notes

Verify the UI element appears and the API call to fetch templates is successful.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Template selector appears for PDF output format

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

An Administrator is on the 'Create/Edit Report' page and at least one Handlebars template has been uploaded to the system

### 3.2.5 When

The Administrator selects 'PDF' from the 'Output Format' dropdown

### 3.2.6 Then

A 'Template' selector UI element becomes visible and enabled

### 3.2.7 And

The 'Template' selector is populated with the names of all available, uploaded Handlebars templates.

### 3.2.8 Validation Notes

Verify the UI element appears and the API call to fetch templates is successful.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Template selector is hidden for non-templated output formats

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

An Administrator is on the 'Create/Edit Report' page and has selected 'HTML' as the output format, making the 'Template' selector visible

### 3.3.5 When

The Administrator changes the 'Output Format' to 'CSV'

### 3.3.6 Then

The 'Template' selector becomes hidden or disabled.

### 3.3.7 Validation Notes

Test this for all non-templated formats: JSON, CSV, TXT. The previously selected template value should be cleared from the form's state.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Saving a report configuration with a selected template

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

An Administrator is configuring a report with the 'Output Format' set to 'PDF'

### 3.4.5 When

The Administrator selects a template from the 'Template' selector and saves the report configuration

### 3.4.6 Then

The system successfully saves the association between the report configuration and the selected template's ID in the database.

### 3.4.7 Validation Notes

Verify the `ReportConfiguration` record in the database has the correct foreign key for the selected template.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Attempting to save a templated report without selecting a template

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

An Administrator is configuring a report with the 'Output Format' set to 'HTML'

### 3.5.5 When

The Administrator attempts to save the report configuration without selecting a template from the 'Template' selector

### 3.5.6 Then

A validation error is displayed on the 'Template' selector field

### 3.5.7 And

The report configuration is not saved.

### 3.5.8 Validation Notes

Verify both frontend UI validation and backend API validation (HTTP 400 response) are in place.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

UI behavior when no templates are available

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

An Administrator is on the 'Create/Edit Report' page and no Handlebars templates have been uploaded to the system

### 3.6.5 When

The Administrator selects 'PDF' from the 'Output Format' dropdown

### 3.6.6 Then

The 'Template' selector UI element is visible but disabled

### 3.6.7 And

A message is displayed within or near the selector, such as 'No templates available. Please upload a template.'

### 3.6.8 Validation Notes

The user should be blocked from saving a configuration that requires a template if none are available.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dropdown or equivalent selection component labeled 'Template'.
- Helper text or a placeholder for the template selector (e.g., 'Select a template').
- A message area to display information when no templates are available.
- Standard validation error indicators (e.g., red border, error message text).

## 4.2.0 User Interactions

- The 'Template' selector's visibility and enabled state must be dynamically controlled by the value of the 'Output Format' selector.
- Selecting a template from the list updates the state of the report configuration form.
- Hovering over template names in the dropdown might show a tooltip with the template's description, if available.

## 4.3.0 Display Requirements

- The list of templates should be fetched from the backend and displayed alphabetically by name.
- When editing an existing report configuration, the currently saved template must be pre-selected in the dropdown.

## 4.4.0 Accessibility Needs

- The 'Template' selector must have a proper `<label>` element associated with it.
- The component must be fully keyboard accessible (navigable with Tab, openable with Space/Enter, options selectable with arrow keys).
- The component must adhere to WCAG 2.1 Level AA standards for color contrast and ARIA attributes.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A Handlebars template is mandatory if the report output format is 'HTML' or 'PDF'.

### 5.1.3 Enforcement Point

During the save/update action of a Report Configuration, both on the client-side (UI) and server-side (API).

### 5.1.4 Violation Handling

The save operation is rejected, and a clear validation error message is returned to the user.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A Handlebars template must not be associated with a report if the output format is 'JSON', 'CSV', or 'TXT'.

### 5.2.3 Enforcement Point

During the save/update action of a Report Configuration on the server-side (API).

### 5.2.4 Violation Handling

The system should ignore/nullify any provided template ID if the output format is non-templated. The UI should prevent this state from being submitted.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-051

#### 6.1.1.2 Dependency Reason

Provides the report configuration wizard/page where this selection component will be implemented.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-055

#### 6.1.2.2 Dependency Reason

Provides the 'Output Format' selector, which is the trigger for this story's functionality.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-062

#### 6.1.3.2 Dependency Reason

Provides the functionality to upload and manage the Handlebars templates that will be listed in the selector. This is a hard dependency.

## 6.2.0.0 Technical Dependencies

- Backend API endpoint to list available templates (e.g., GET /api/v1/templates).
- Modification of the ReportConfiguration entity and corresponding database table to include a reference to the selected template.
- Modification of the API endpoints for creating/updating reports (e.g., POST/PUT /api/v1/reports/{id}) to accept and validate the template ID.

## 6.3.0.0 Data Dependencies

- Requires at least one Handlebars template to be present in the system for full end-to-end testing.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to fetch the list of templates should respond in under 200ms, assuming a list of up to 500 templates.

## 7.2.0.0 Security

- The backend must validate that the submitted template ID is a valid, existing ID from the templates table to prevent data integrity issues.

## 7.3.0.0 Usability

- The conditional appearance and disappearance of the template selector should feel instantaneous and not disrupt the user's flow.
- Error messages must be clear and actionable.

## 7.4.0.0 Accessibility

- Must conform to WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The UI component must function correctly on all supported browsers (latest stable Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires coordinated changes across frontend, backend, and database.
- Frontend state management for conditional UI rendering.
- Backend validation logic.

## 8.3.0.0 Technical Risks

- Potential for race conditions in the UI if the user rapidly changes the output format. State updates must be handled robustly.
- Ensuring the database migration to add the new column to the `ReportConfiguration` table is non-destructive.

## 8.4.0.0 Integration Points

- Frontend: Report Configuration component.
- Backend: Report Configuration API controller (POST/PUT methods).
- Backend: New API controller/endpoint for listing templates.
- Database: `ReportConfiguration` table schema.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Create a new PDF report, select a template, save, and verify it's stored.
- Edit an existing CSV report, change its format to HTML, select a template, and save.
- Attempt to save an HTML report without selecting a template and verify the error.
- Create a PDF report when no templates exist in the system and verify the UI state.
- Navigate the report configuration form using only a keyboard to ensure the template selector is accessible.

## 9.3.0.0 Test Data Needs

- A system state with zero uploaded templates.
- A system state with multiple (3+) uploaded templates to test the selector list.

## 9.4.0.0 Testing Tools

- Frontend: Jest, React Testing Library.
- Backend: xUnit, Moq.
- E2E: Playwright or similar.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for both frontend component logic and backend validation, achieving >80% coverage
- Integration testing completed successfully for the API endpoints
- End-to-end tests covering the user flow are passing
- User interface reviewed and approved for usability and accessibility (WCAG 2.1 AA)
- Performance requirements verified
- Security requirements validated
- API documentation (OpenAPI/Swagger) updated for any changes
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is blocked by US-062 (Template Management). It should be scheduled in a sprint after US-062 is completed.
- The API for listing templates (from US-062) should be available for the frontend development to proceed, even if the full management UI is not finished.

## 11.4.0.0 Release Impact

This is a core feature for generating human-readable reports. Its completion is critical for the report generation feature set.

