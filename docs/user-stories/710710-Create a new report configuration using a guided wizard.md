# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-051 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Create a new report configuration using a guided w... |
| As A User Story | As an Administrator, I want to use a guided, multi... |
| User Persona | Administrator |
| Business Value | Improves the reliability of report configurations ... |
| Functional Area | Report Configuration Management |
| Story Theme | Core Reporting Workflow |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Successfully create a complete report configuration

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an Administrator logged into the Control Panel and have initiated the 'Create New Report' action

### 3.1.5 When

I proceed through each step of the wizard, providing a valid name, selecting an existing connector, an optional transformation, an output format (e.g., PDF) with a required template, a valid delivery destination, and a valid schedule

### 3.1.6 And

A success notification 'Report configuration created successfully' is displayed.

### 3.1.7 Then

The system saves the new report configuration via an API call

### 3.1.8 Validation Notes

Verify the new report exists in the database and is displayed in the UI. Check the network tab for a successful POST request to the reports API endpoint.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Wizard Navigation: Navigate forward and backward between steps

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am on Step 3 ('Transformation') of the report creation wizard and have already filled out data on Step 1 and 2

### 3.2.5 When

I then click the 'Next' button

### 3.2.6 Then

I am returned to Step 3 and any previously selected transformation is still selected.

### 3.2.7 Validation Notes

Test navigation between all steps to ensure form state is preserved for the duration of the wizard session.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Step Validation: Prevent progression with missing required information

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am on Step 1 ('Metadata') of the wizard

### 3.3.5 When

I leave the 'Report Name' field blank and click the 'Next' button

### 3.3.6 Then

An inline validation error message 'Report Name is required' appears next to the field

### 3.3.7 And

I remain on Step 1 and cannot proceed to the next step.

### 3.3.8 Validation Notes

Verify that each step enforces its own validation rules before allowing the user to proceed.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Conditional UI: Template selection is only available for specific output formats

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

I am on the 'Output Configuration' step of the wizard

### 3.4.5 When

I change the output format to 'PDF'

### 3.4.6 Then

The 'Select Template' control becomes visible and is marked as a required field.

### 3.4.7 Validation Notes

Test with all output formats (HTML, PDF, JSON, CSV, TXT) to ensure the template selector's visibility and requirement status are correct.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Edge Case: Attempting to create a report when no data connectors are configured

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

No data connectors have been configured in the system

### 3.5.5 When

I navigate to the 'Select Connector' step of the wizard

### 3.5.6 Then

The UI displays a message like 'No data connectors found. Please create a connector first.'

### 3.5.7 And

The 'Next' button is disabled.

### 3.5.8 Validation Notes

Also test this for the 'Select Template' step when no templates are available for HTML/PDF formats.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Unsaved Changes: User is warned when navigating away from the wizard

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

I am in the report creation wizard and have entered data into at least one field

### 3.6.5 When

I attempt to click a navigation link to another part of the application (e.g., 'Dashboard')

### 3.6.6 Then

The browser displays a native confirmation prompt warning me that unsaved changes will be lost.

### 3.6.7 Validation Notes

Verify this behavior for browser refresh, closing the tab, and internal application navigation.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Final Review: Review all configured settings before saving

### 3.7.3 Scenario Type

Happy_Path

### 3.7.4 Given

I have completed all previous steps of the wizard

### 3.7.5 When

I arrive at the final 'Review & Save' step

### 3.7.6 Then

The UI displays a read-only summary of all my selections: Report Name, Connector, Transformation, Output Format, Template, Delivery Destinations, and Schedule

### 3.7.7 And

Each section in the summary has an 'Edit' link that navigates me back to the corresponding step to make changes.

### 3.7.8 Validation Notes

Verify that clicking an 'Edit' link takes the user to the correct step and that returning to the review step reflects the changes.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Multi-step wizard component with a progress indicator (e.g., Stepper)
- Navigation buttons ('Next', 'Back', 'Cancel', 'Save')
- Form inputs for report metadata (name, description)
- Dropdown/selector for existing Connectors
- Dropdown/selector for existing Transformations (with a 'None' option)
- Controls for Output Configuration (format selection, template selection)
- Component for adding/managing a list of Delivery Destinations
- CRON expression builder UI for scheduling
- Read-only summary view for the final review step

## 4.2.0 User Interactions

- User progresses linearly through steps but can go back to edit previous steps.
- State of the entire report configuration object is maintained throughout the wizard session.
- Validation errors are displayed inline and prevent progression.
- UI elements in later steps may be conditional on selections in earlier steps.

## 4.3.0 Display Requirements

- The current step must be clearly indicated.
- All required fields must be clearly marked.
- The final review screen must accurately summarize all user selections.

## 4.4.0 Accessibility Needs

- The wizard must be navigable using a keyboard.
- All form controls must have associated labels.
- Focus management should be handled correctly when moving between steps.
- Must comply with WCAG 2.1 Level AA.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A report configuration must have at least one data connector and one delivery destination.

### 5.1.3 Enforcement Point

During validation on the respective wizard steps and on final save.

### 5.1.4 Violation Handling

Prevent user from proceeding to the next step or saving the configuration. Display a clear error message.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A Handlebars template is mandatory if the output format is 'HTML' or 'PDF'.

### 5.2.3 Enforcement Point

During validation on the 'Output Configuration' step.

### 5.2.4 Violation Handling

Prevent user from proceeding. Display an error message if no template is selected for these formats.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-034

#### 6.1.1.2 Dependency Reason

The wizard requires the ability to select from existing database connectors.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-035

#### 6.1.2.2 Dependency Reason

The wizard requires the ability to select from existing file system connectors.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-043

#### 6.1.3.2 Dependency Reason

The wizard requires the ability to select from existing transformation scripts.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-062

#### 6.1.4.2 Dependency Reason

The wizard requires the ability to select from uploaded Handlebars templates for HTML/PDF reports.

### 6.1.5.0 Story Id

#### 6.1.5.1 Story Id

US-059

#### 6.1.5.2 Dependency Reason

The wizard requires the CRON builder UI component for defining a schedule.

## 6.2.0.0 Technical Dependencies

- React 18 with TypeScript
- MUI v5 for UI components (specifically the Stepper component)
- Zustand for frontend state management to hold the report configuration object across steps
- Backend API endpoint for creating report configurations (`POST /api/v1/reports`)

## 6.3.0.0 Data Dependencies

- Requires access to lists of pre-configured Connectors, Transformations, and Templates from the backend.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- Each step of the wizard should load in under 1 second.
- API calls to fetch lists (connectors, templates) should respond within 500ms.

## 7.2.0.0 Security

- Only users with the 'Administrator' role can access this feature.
- All user input must be properly sanitized to prevent XSS attacks.

## 7.3.0.0 Usability

- The wizard flow must be intuitive and logical.
- Error messages must be clear and actionable.

## 7.4.0.0 Accessibility

- The UI must adhere to WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The wizard must function correctly on the latest stable versions of Chrome, Firefox, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Managing form state across multiple, interdependent steps.
- Implementing robust, step-by-step validation logic.
- Handling conditional UI rendering based on user selections.
- Integrating multiple distinct components (connector selector, CRON builder, etc.) into a single cohesive flow.

## 8.3.0.0 Technical Risks

- State management could become complex if not designed carefully from the start.
- Ensuring a seamless user experience when navigating back and forth between steps with dependent data.

## 8.4.0.0 Integration Points

- GET API endpoints to fetch lists of available connectors, transformations, and templates.
- POST `/api/v1/reports` endpoint to save the final configuration object.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Create a simple report with no transformation and CSV output.
- Create a complex report with a transformation and PDF output.
- Attempt to create a report with invalid data in each step.
- Test navigation back and forth, changing a value in a previous step, and verifying it persists.
- Test the 'no connectors available' and 'no templates available' edge cases.

## 9.3.0.0 Test Data Needs

- At least two pre-configured connectors of different types.
- At least one pre-configured transformation script.
- At least one uploaded Handlebars template.
- Pre-configured delivery destination settings.

## 9.4.0.0 Testing Tools

- Jest and React Testing Library for unit/integration tests.
- A browser-based E2E testing framework like Cypress or Playwright.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for each wizard step and state management logic, achieving >80% coverage
- E2E tests covering the full happy path and at least one error path are implemented and passing
- User interface reviewed for UX consistency and responsiveness
- Accessibility audit passed against WCAG 2.1 AA standards
- Documentation for the report creation process is updated in the User Guide
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a key feature and depends on several other stories being completed first. It should be scheduled in a sprint after its prerequisites are met.
- This story integrates many smaller pieces, so sufficient time for integration testing should be allocated.

## 11.4.0.0 Release Impact

This is a critical path feature for the initial release. The application is not viable without the ability to configure reports.

