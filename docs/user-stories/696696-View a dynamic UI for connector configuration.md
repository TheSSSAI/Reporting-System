# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-037 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | View a dynamic UI for connector configuration |
| As A User Story | As an Administrator, I want the user interface to ... |
| User Persona | Administrator |
| Business Value | Improves usability by simplifying a complex config... |
| Functional Area | System Configuration - Data Connectors |
| Story Theme | Data Ingestion Framework |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Dynamic form rendering for a new database connector

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

The Administrator is on the 'Create New Connector' page

### 3.1.5 When

The Administrator selects 'SQL Server' from the 'Connector Type' dropdown

### 3.1.6 Then

The form below the dropdown dynamically updates to display input fields for 'Name', 'Server', 'Database', 'Username', and 'Password', as defined by the SQL Server connector's schema.

### 3.1.7 Validation Notes

Verify that the rendered fields match the schema provided by the backend API for the 'SQL Server' connector type. Fields for other connector types (e.g., 'File Path') must not be visible.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Dynamic form rendering for a new file-based connector

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The Administrator is on the 'Create New Connector' page

### 3.2.5 When

The Administrator selects 'CSV File' from the 'Connector Type' dropdown

### 3.2.6 Then

The form below the dropdown dynamically updates to display input fields for 'Name', 'File Path', 'Delimiter', and a checkbox for 'Has Header', as defined by the CSV connector's schema.

### 3.2.7 Validation Notes

Verify that the rendered fields match the schema provided by the backend API for the 'CSV File' connector type. Fields for other connector types (e.g., 'Server') must not be visible.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Switching between connector types clears previous form state

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

The Administrator has selected 'SQL Server' and entered 'my-server' into the 'Server' field

### 3.3.5 When

The Administrator changes the 'Connector Type' selection to 'CSV File'

### 3.3.6 Then

The form fields for 'SQL Server' are removed, and the new form for 'CSV File' is displayed with all its fields empty.

### 3.3.7 Validation Notes

Test by entering data, switching types, and verifying the new form is in a pristine state. Consider adding a confirmation dialog if data has been entered.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Editing an existing connector pre-populates the dynamic form

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

An existing 'PostgreSQL' connector configuration has been saved with specific values

### 3.4.5 When

The Administrator navigates to the 'Edit' page for that connector

### 3.4.6 Then

The 'Connector Type' dropdown is pre-selected and disabled with 'PostgreSQL'.

### 3.4.7 And

The form is rendered with the correct fields for PostgreSQL, pre-populated with the saved configuration values.

### 3.4.8 Validation Notes

Verify that all saved values, including masked passwords, are correctly bound to the dynamically generated form fields.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Handling API failure when fetching connector schema

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

The backend API endpoint to fetch a connector's schema is unavailable or returns an error

### 3.5.5 When

The Administrator selects a connector type from the dropdown

### 3.5.6 Then

A user-friendly error message (e.g., 'Error: Could not load configuration details for this connector.') is displayed below the dropdown.

### 3.5.7 And

No configuration form fields are rendered.

### 3.5.8 Validation Notes

Use a tool like Postman or browser dev tools to mock an API failure (e.g., 500 Internal Server Error) and verify the UI's response.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

UI shows a loading state while fetching schema

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

The Administrator is on the 'Create New Connector' page

### 3.6.5 When

The Administrator selects a connector type, and the API call to fetch the schema is in progress

### 3.6.6 Then

A loading indicator (e.g., a spinner) is displayed in the form area.

### 3.6.7 And

The loading indicator disappears once the form is rendered or an error is shown.

### 3.6.8 Validation Notes

Simulate network latency using browser developer tools to ensure the loading state is visible and handled correctly.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Dynamic form supports custom connectors

### 3.7.3 Scenario Type

Happy_Path

### 3.7.4 Given

A custom connector (e.g., 'FHIR Connector') has been deployed and discovered by the system

### 3.7.5 When

The Administrator selects 'FHIR Connector' from the 'Connector Type' dropdown

### 3.7.6 Then

The form dynamically renders the specific fields required for the FHIR connector (e.g., 'Base URL', 'API Key'), based on the schema provided by the custom connector's DLL.

### 3.7.7 Validation Notes

This requires a test custom connector to be available to validate that the dynamic rendering works for non-built-in types.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dropdown menu labeled 'Connector Type' populated with all available built-in and custom connectors.
- A dynamic form container area that displays input fields.
- Standard form elements (text input, password input, checkbox, number input, text area) that can be rendered based on a schema.
- A loading indicator (e.g., spinner).
- An error message display area.

## 4.2.0 User Interactions

- Selecting an item from the 'Connector Type' dropdown triggers an API call and re-renders the form.
- When editing, the 'Connector Type' dropdown should be disabled to prevent changing the type of an existing configuration.
- Form fields should support standard interactions like typing, checking boxes, etc.

## 4.3.0 Display Requirements

- Field labels, placeholder text, and help text should be driven by the fetched schema.
- Fields marked as required in the schema must be visually indicated (e.g., with an asterisk *).
- Fields marked as sensitive/secret in the schema must be rendered as password inputs (masked text).

## 4.4.0 Accessibility Needs

- All dynamically generated form fields must have an associated `<label>` tag with a `for` attribute linking to the input's `id`.
- The entire form must be navigable using only a keyboard.
- The UI must comply with WCAG 2.1 Level AA standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

The schema for a connector's configuration is defined by the connector plug-in itself via the `GetConfigurationSchema()` method.

### 5.1.3 Enforcement Point

Backend API

### 5.1.4 Violation Handling

If a plug-in provides a malformed schema, the backend should log a critical error, and the API should return a server error, preventing that connector from being configured.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The type of a connector configuration cannot be changed after it has been created.

### 5.2.3 Enforcement Point

User Interface

### 5.2.4 Violation Handling

The 'Connector Type' dropdown is disabled on the 'Edit Connector' page.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-041

#### 6.1.1.2 Dependency Reason

The system must be able to discover available connectors to populate the 'Connector Type' dropdown.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-113

#### 6.1.2.2 Dependency Reason

The backend `IConnector` interface, specifically the `GetConfigurationSchema()` method, must be defined and implemented for this UI to consume.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint to list all available connector types.
- A backend API endpoint to fetch the JSON configuration schema for a given connector type (e.g., `GET /api/v1/connectors/types/{typeName}/schema`).
- A frontend component capable of rendering a form from a JSON schema definition.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The dynamic form should render within 500ms after the schema API call completes.
- The P95 latency for the schema-fetching API endpoint should be under 100ms.

## 7.2.0.0 Security

- The schema definition must not contain any sensitive data.
- The UI must correctly identify fields marked as 'secret' in the schema and render them as `<input type="password">` to prevent shoulder surfing.

## 7.3.0.0 Usability

- The process of selecting a connector type and seeing the form update should feel instantaneous and intuitive to the user.

## 7.4.0.0 Accessibility

- The dynamically rendered form must adhere to WCAG 2.1 Level AA guidelines.

## 7.5.0.0 Compatibility

- The dynamic form rendering must function correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires a well-defined, standardized JSON schema format for all connectors.
- Requires building or integrating a generic, schema-driven form rendering component in React.
- Involves tight coupling between the frontend component and the backend API contract for the schema.
- Backend implementation is required for `GetConfigurationSchema()` for all built-in connectors before the feature is fully testable.

## 8.3.0.0 Technical Risks

- The chosen JSON schema format might not be flexible enough for future complex connectors, requiring rework.
- A custom-built form renderer could be complex to maintain; an off-the-shelf library might have limitations or styling issues.

## 8.4.0.0 Integration Points

- Frontend Control Panel -> Backend API (`/api/v1/connectors/...`)
- Backend API -> Connector Plug-in Assembly (`IConnector.GetConfigurationSchema()`)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify form rendering for each built-in connector type (SQL Server, MySQL, PostgreSQL, File System, OPC UA).
- Verify form rendering for a sample custom connector.
- Test the UI's behavior during API loading and error states.
- Test editing an existing connector and verify data is pre-populated correctly.
- Test keyboard navigation and screen reader compatibility for the dynamic form.

## 9.3.0.0 Test Data Needs

- Mock JSON schemas for various connector types, including one with all possible field types (text, password, checkbox, etc.).
- Saved connector configurations in the test database to validate the 'edit' functionality.

## 9.4.0.0 Testing Tools

- Jest and React Testing Library for frontend unit tests.
- xUnit and Moq for backend unit tests.
- A browser automation tool (e.g., Playwright, Cypress) for E2E tests.
- Axe for accessibility scanning.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >80% coverage for new code
- Integration testing between frontend and backend API completed successfully
- User interface reviewed and approved by UX/Product Owner
- Performance requirements verified
- Security requirements validated
- Accessibility scan passed with no critical violations
- Documentation for the schema format is created for plug-in developers
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational story for all connector configuration work. It should be prioritized early.
- Backend and frontend tasks can be parallelized if a mock API contract for the schema is agreed upon early.
- Requires coordination to define the standard JSON schema format that all developers will adhere to.

## 11.4.0.0 Release Impact

- This feature is critical for the initial release as it is the primary mechanism for configuring data sources.

