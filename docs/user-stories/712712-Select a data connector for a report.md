# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-053 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Select a data connector for a report |
| As A User Story | As an Administrator, I want to select a previously... |
| User Persona | Administrator: A user with full CRUD access to sys... |
| Business Value | Enables the core function of a report by linking i... |
| Functional Area | Report Configuration |
| Story Theme | Report Configuration & Scheduling |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Display list of available connectors

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an Administrator logged into the Control Panel, and at least one data connector has been successfully configured and saved

### 3.1.5 When

I navigate to the 'Create Report' page or the 'Edit Report' page for an existing report

### 3.1.6 Then

I should see a UI element (e.g., a searchable dropdown) labeled 'Data Connector' which is populated with the names of all available connector configurations.

### 3.1.7 Validation Notes

Verify via UI inspection. The API endpoint GET /api/v1/connectors should be called and should return a list of connector names and IDs.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Select a connector and save the report

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am on the report configuration page with the list of data connectors displayed

### 3.2.5 When

I select a specific connector from the list and save the report configuration

### 3.2.6 Then

The system must associate the selected connector's ID with the report configuration in the database.

### 3.2.7 Validation Notes

After saving, inspect the database or call the GET /api/v1/reports/{id} endpoint for the report and verify the 'connectorConfigurationId' field is correctly populated.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempt to create a report with no connectors configured

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

I am an Administrator logged into the Control Panel, and no data connectors have been configured in the system

### 3.3.5 When

I navigate to the 'Create Report' page

### 3.3.6 Then

The data connector selection area should be disabled or display a message like 'No data connectors found. Please create a connector first.'

### 3.3.7 And

The 'Save' or 'Next' button in the report configuration wizard should be disabled, preventing me from proceeding.

### 3.3.8 Validation Notes

Set up the system with zero connectors in the database. Navigate to the UI and verify the message and the disabled state of the save/next action.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Attempt to save a report without selecting a connector

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am on the report configuration page, and multiple connectors are available

### 3.4.5 When

I fill out other required fields but do not select a data connector and attempt to save the configuration

### 3.4.6 Then

A validation error message must be displayed next to the connector selection field, indicating it is required.

### 3.4.7 And

The report configuration must not be saved.

### 3.4.8 Validation Notes

Perform the action in the UI and verify the validation message appears and no POST/PUT request is sent to the server, or if it is, the server returns a 400 Bad Request with a validation error.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Change an existing connector selection

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

I am editing an existing report configuration that already has a data connector selected

### 3.5.5 When

I select a different data connector from the list and save the report

### 3.5.6 Then

The report configuration in the database must be updated to reference the newly selected connector's ID.

### 3.5.7 Validation Notes

Edit a report, change the connector, save, and then re-load the edit page or check the API to confirm the new connector is associated.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A clearly labeled, searchable dropdown or autocomplete component (e.g., MUI Autocomplete) for connector selection.
- A text message area to display information when no connectors are available.
- Validation message display for the required field.

## 4.2.0 User Interactions

- User can click to open the list of connectors.
- User can type in the component to filter the list of connectors if the list is long.
- User can select a connector using a mouse click or keyboard navigation (Enter key).
- The selection must be a mandatory step in the report configuration wizard.

## 4.3.0 Display Requirements

- The list must show the user-defined name of each connector configuration.
- The component must be marked with a visual indicator (e.g., an asterisk) to show it is a required field.

## 4.4.0 Accessibility Needs

- The component must have a proper `<label>` associated with it.
- The component must be fully keyboard navigable (up/down arrows, Enter to select, Esc to close).
- The component must be compatible with screen readers, announcing the selected option and the available options.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'A report configuration must be associated with exactly one valid, existing data connector configuration.', 'enforcement_point': 'During the save/update operation of a report configuration.', 'violation_handling': 'The system will reject the save/update operation and return a validation error to the user.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-034

#### 6.1.1.2 Dependency Reason

The system must allow creation of database connectors before one can be selected.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-035

#### 6.1.2.2 Dependency Reason

The system must allow creation of file system connectors before one can be selected.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-051

#### 6.1.3.2 Dependency Reason

This story implements a step within the report creation wizard defined in US-051.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint (e.g., GET /api/v1/connectors) to provide a list of available connector configurations (ID and Name).
- The `ReportConfiguration` data model must include a foreign key to the `ConnectorConfiguration` entity.
- Frontend state management (Zustand) to fetch and cache the list of connectors for the UI.

## 6.3.0.0 Data Dependencies

- Requires at least one `ConnectorConfiguration` record to exist in the database for the happy path to be testable.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to fetch the list of connectors must respond with a P95 latency of under 200ms for up to 500 connector configurations.

## 7.2.0.0 Security

- The API endpoint for listing connectors must be protected and only accessible by authenticated users with the 'Administrator' role.

## 7.3.0.0 Usability

- For lists with more than 20 connectors, the selection component must provide a text-based search/filter capability to allow users to find a connector easily.

## 7.4.0.0 Accessibility

- The UI component must adhere to WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The UI component must render and function correctly on the latest stable versions of Chrome, Firefox, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires a simple backend read endpoint.
- Requires a standard UI component (e.g., MUI Autocomplete).
- Integration into the existing report configuration form/wizard is the main task.

## 8.3.0.0 Technical Risks

- Potential for performance issues if the connector list API is not paginated or optimized and the number of connectors grows very large (1000+), though this is unlikely in most deployments.

## 8.4.0.0 Integration Points

- Frontend: Report Configuration component/page.
- Backend: `ReportConfiguration` service and controller (for saving the association), and a new `ConnectorConfiguration` controller (for listing).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify a user can select a connector from a list of 5 items.
- Verify a user can select a connector from a list of 50 items using the search feature.
- Verify the UI behavior when zero connectors are configured.
- Verify form validation prevents saving without a selection.
- Verify that changing a selection on an existing report and saving works correctly.

## 9.3.0.0 Test Data Needs

- A system state with zero configured connectors.
- A system state with a small number (1-5) of configured connectors.
- A system state with a large number (50+) of configured connectors with varied names to test search functionality.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend component tests.
- Playwright or Cypress for E2E tests.
- Axe for accessibility scanning.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for backend and frontend, achieving >80% coverage
- Integration testing for the API endpoint and data persistence completed successfully
- E2E test scenario for selecting a connector in the report wizard is implemented and passing
- User interface reviewed and approved for usability and responsiveness
- Accessibility requirements (WCAG 2.1 AA) validated
- Documentation for the report configuration process is updated to include this step
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

2

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a blocker for any further steps in the report configuration wizard that depend on data, such as data transformation or template preview.
- Must be scheduled after the core connector creation stories (US-034, US-035) are complete.

## 11.4.0.0 Release Impact

This is a critical path feature for the core report creation functionality. The product cannot ship without it.

