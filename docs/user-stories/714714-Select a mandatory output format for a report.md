# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-055 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Select a mandatory output format for a report |
| As A User Story | As an Administrator, I want to select a mandatory ... |
| User Persona | Administrator |
| Business Value | Enables the system to produce reports in various f... |
| Functional Area | Report Configuration |
| Story Theme | Report Configuration & Scheduling |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Display of Output Format options in new report configuration

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an Administrator logged into the Control Panel and I am creating a new report configuration

### 3.1.5 When

I navigate to the 'Output' step of the report configuration wizard

### 3.1.6 Then

I am presented with a UI control (e.g., dropdown menu) labeled 'Output Format' and it contains the exact options: 'HTML', 'PDF', 'JSON', 'CSV', and 'TXT'.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Mandatory selection enforcement

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

I am an Administrator on the 'Output' step of the report configuration wizard

### 3.2.5 When

I attempt to save or proceed to the next step without selecting an 'Output Format'

### 3.2.6 Then

The system prevents the action and displays a clear validation error message, such as 'Output Format is required'.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Conditional UI for templated formats (HTML/PDF)

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

I am an Administrator configuring a report

### 3.3.5 When

I select 'HTML' or 'PDF' as the 'Output Format'

### 3.3.6 Then

The UI element for selecting a Handlebars template (related to US-056) becomes visible and is marked as a required field.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Conditional UI for direct data formats (JSON/CSV/TXT)

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

I am an Administrator configuring a report

### 3.4.5 When

I select 'JSON', 'CSV', or 'TXT' as the 'Output Format'

### 3.4.6 Then

The UI element for selecting a Handlebars template is either hidden or disabled, as it is not applicable.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Saving the selected format

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

I am an Administrator creating a report and have selected 'PDF' as the 'Output Format' and filled all other mandatory fields

### 3.5.5 When

I save the report configuration

### 3.5.6 Then

The configuration is saved successfully, and the selected 'PDF' format is persisted in the system's database for that report.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Editing an existing report configuration

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

An existing report configuration has its 'Output Format' saved as 'CSV'

### 3.6.5 When

I, as an Administrator, open that report configuration for editing

### 3.6.6 Then

The 'Output Format' UI control is pre-populated with the value 'CSV'.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

API validation for output format

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

An API client is attempting to create or update a report configuration

### 3.7.5 When

The client sends a request to the API endpoint with a null, empty, or invalid value for the 'outputFormat' field

### 3.7.6 Then

The API rejects the request with an HTTP 400 Bad Request status and a descriptive error message in the response body.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dropdown menu or radio button group labeled 'Output Format'.
- A validation message container for the 'Output Format' field.

## 4.2.0 User Interactions

- User must be able to select one of the five format options.
- The UI must dynamically update to show/hide the template selection field based on the chosen format.
- The system must prevent form submission if no format is selected.

## 4.3.0 Display Requirements

- The selected format must be clearly visible.
- Validation errors must be displayed near the 'Output Format' control and be easily noticeable.

## 4.4.0 Accessibility Needs

- The 'Output Format' control must have a proper label for screen readers.
- The control must be fully operable via keyboard.
- Validation error messages must be programmatically associated with the control and announced by screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A report configuration must have exactly one output format selected from the approved list (HTML, PDF, JSON, CSV, TXT).

### 5.1.3 Enforcement Point

During the save/update operation of a ReportConfiguration in both the UI and the backend API.

### 5.1.4 Violation Handling

The operation is blocked, and a validation error is returned to the user/client.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A Handlebars template is required if and only if the output format is HTML or PDF.

### 5.2.3 Enforcement Point

During the validation of a ReportConfiguration.

### 5.2.4 Violation Handling

The operation is blocked, and a validation error is returned.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

- {'story_id': 'US-051', 'dependency_reason': 'This story provides the report configuration wizard UI where the output format selection will be implemented.'}

## 6.2.0 Technical Dependencies

- The `ReportConfiguration` data model in the backend must be updated to include an `outputFormat` field.
- The backend API (`POST /api/v1/reports`, `PUT /api/v1/reports/{id}`) must be updated to accept and validate the `outputFormat` field.
- The frontend state management (Zustand) must be able to handle the state of the selected output format and its effect on other UI components.

## 6.3.0 Data Dependencies

*No items available*

## 6.4.0 External Dependencies

*No items available*

# 7.0.0 Non Functional Requirements

## 7.1.0 Performance

- The UI response to selecting a format (e.g., showing/hiding the template selector) must be perceived as instantaneous (<100ms).

## 7.2.0 Security

- The API endpoint for saving the report configuration must be protected and only accessible to users with the 'Administrator' role.
- Input validation must be performed on the backend to prevent invalid data from being persisted.

## 7.3.0 Usability

- The purpose and options of the 'Output Format' control should be self-explanatory to an Administrator.
- Error messages for validation failures must be clear and actionable.

## 7.4.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0 Compatibility

- The UI must function correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge.

# 8.0.0 Implementation Considerations

## 8.1.0 Complexity Assessment

Low

## 8.2.0 Complexity Factors

- Requires changes to the backend data model, API controller, and frontend component.
- Involves conditional rendering logic in the frontend, which adds minor complexity.
- Database migration will be required for the schema change.

## 8.3.0 Technical Risks

- Potential for regression in the report configuration wizard if state management is not handled carefully.
- Ensuring the conditional logic for the template selector is robust and doesn't create UI bugs.

## 8.4.0 Integration Points

- Backend: `ReportConfiguration` entity and its corresponding EF Core model.
- Backend: `ReportsController` for handling CRUD operations.
- Frontend: The React component responsible for the report configuration wizard.

# 9.0.0 Testing Requirements

## 9.1.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0 Test Scenarios

- Verify all 5 output formats can be selected and saved.
- Test the validation logic for not selecting a format.
- Test the conditional UI logic for each of the 5 format options to ensure the template selector appears/disappears correctly.
- Test the API endpoint with valid, invalid, and null values for `outputFormat`.
- Verify that editing and re-saving a report configuration correctly persists the output format.

## 9.3.0 Test Data Needs

- An existing report configuration to test the 'edit' scenario.
- API request bodies with and without the `outputFormat` field.

## 9.4.0 Testing Tools

- Backend: xUnit, Moq
- Frontend: Jest, React Testing Library
- E2E: A framework like Playwright or Cypress could be used.

# 10.0.0 Definition Of Done

- All acceptance criteria validated and passing.
- Code reviewed and approved by team.
- Unit tests for backend validation and frontend component logic implemented and passing with >=80% coverage.
- Integration testing for the create/update API endpoints completed successfully.
- User interface reviewed and approved for usability and adherence to design.
- Accessibility requirements (WCAG 2.1 AA) validated via automated tools and manual testing.
- API documentation (OpenAPI/Swagger) is updated to reflect the new mandatory `outputFormat` field.
- Story deployed and verified in the staging environment.

# 11.0.0 Planning Information

## 11.1.0 Story Points

2

## 11.2.0 Priority

🔴 High

## 11.3.0 Sprint Considerations

- This is a foundational story for report configuration and a blocker for US-056 (Template Selection). It should be prioritized early in the development cycle.
- The backend and frontend work can be done in parallel but requires coordination on the API contract for the `outputFormat` field.

## 11.4.0 Release Impact

This feature is critical for the initial release as it defines a core aspect of a report. The system is not functional without it.

