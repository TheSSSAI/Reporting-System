# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-068 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Configure an API delivery target for synchronous m... |
| As A User Story | As an Administrator, I want to configure a report'... |
| User Persona | Administrator |
| Business Value | Enables simple, real-time integration with externa... |
| Functional Area | Report Configuration & Delivery |
| Story Theme | Report Delivery and API Integration |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

UI displays synchronous mode options when 'API Response' is selected

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

an Administrator is creating or editing a report in the Control Panel

### 3.1.5 When

they navigate to the 'Delivery Destinations' configuration step and select 'API Response' as the delivery type

### 3.1.6 And

they select the 'Synchronous' mode option

### 3.1.7 Then

a numeric input field labeled 'Timeout (seconds)' is displayed, pre-filled with a default value of 30.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Successfully save a report with a synchronous API delivery target

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

an Administrator has configured an 'API Response' delivery target with 'Synchronous' mode and a valid timeout value

### 3.2.5 When

they save the report configuration

### 3.2.6 Then

the configuration is saved successfully without errors.

### 3.2.7 Validation Notes

Verify by reloading the report configuration and confirming the 'API Response' destination with 'Synchronous' mode and the specified timeout is correctly displayed.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

UI prevents saving with a non-numeric timeout value

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

an Administrator is configuring a synchronous API delivery target

### 3.3.5 When

they enter a non-numeric value (e.g., 'thirty') into the 'Timeout (seconds)' field and attempt to save the report configuration

### 3.3.6 Then

a validation error message is displayed next to the input field, and the configuration is not saved.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

UI prevents saving with an out-of-range timeout value

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

the system enforces a timeout range of 1 to 60 seconds for synchronous API delivery

### 3.4.5 When

the Administrator enters a value of '0' or '61' into the 'Timeout (seconds)' field and attempts to save

### 3.4.6 Then

a validation error message indicating the valid range (1-60) is displayed, and the configuration is not saved.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

UI displays a performance warning for potentially long-running reports

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

an Administrator is configuring a report that includes a data transformation script

### 3.5.5 When

they add an 'API Response' delivery target and select 'Synchronous' mode

### 3.5.6 Then

a non-blocking warning message is displayed in the UI, advising that synchronous mode may time out for reports with complex transformations or large datasets.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Backend API accepts and stores the new delivery configuration

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

a valid API request is constructed to create/update a report

### 3.6.5 When

the request payload includes a delivery destination of type 'API_RESPONSE' with parameters for synchronous mode and a timeout

### 3.6.6 Then

the backend API successfully validates and persists this configuration in the database.

### 3.6.7 Validation Notes

This will be verified via an integration test that checks the database state after the API call.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- An 'API Response' option in the delivery destination type selector.
- A radio button group for 'Mode' with 'Synchronous' and 'Asynchronous' options, displayed when 'API Response' is selected.
- A numeric input field for 'Timeout (seconds)' with a default value, displayed when 'Synchronous' mode is selected.
- Client-side validation messages for the timeout field.
- A non-blocking warning/info box for potential performance issues.

## 4.2.0 User Interactions

- Selecting 'API Response' dynamically reveals the 'Mode' options.
- Selecting 'Synchronous' mode dynamically reveals the 'Timeout' input field.
- Attempting to save with invalid timeout data prevents form submission and highlights the error.

## 4.3.0 Display Requirements

- The default timeout must be clearly displayed as 30 seconds.
- Helper text should explain the purpose of the timeout and its valid range (1-60 seconds).

## 4.4.0 Accessibility Needs

- All new form controls (radio buttons, inputs) must have associated labels.
- Validation errors must be programmatically associated with their respective inputs using `aria-describedby`.
- The dynamically appearing sections must be manageable by screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

The synchronous API delivery timeout must be an integer between 1 and 60, inclusive.

### 5.1.3 Enforcement Point

Both client-side (UI) and server-side (API) validation.

### 5.1.4 Violation Handling

The system shall reject the configuration save request and return an informative error message to the user.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The default timeout for synchronous API delivery is 30 seconds.

### 5.2.3 Enforcement Point

The Control Panel UI when a new synchronous delivery target is configured.

### 5.2.4 Violation Handling

N/A - this is a default setting.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-051

#### 6.1.1.2 Dependency Reason

This story adds a new option to the report configuration wizard created in US-051.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-057

#### 6.1.2.2 Dependency Reason

This story enhances the functionality for configuring delivery destinations established in US-057.

## 6.2.0.0 Technical Dependencies

- The backend `ReportConfiguration` data model and database schema must be updated to store delivery target details.
- The frontend state management must support the dynamic UI elements.
- The report configuration API endpoints (`POST/PUT /api/v1/reports`) must be updated to handle the new data structure.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The UI for configuring this option must load without any noticeable delay.

## 7.2.0.0 Security

- All configuration data, including delivery settings, must be transmitted over HTTPS.
- Server-side validation must be implemented to prevent malicious or invalid data from being saved.

## 7.3.0.0 Usability

- The purpose of synchronous mode and the timeout setting should be clear to an Administrator through labels and helper text.

## 7.4.0.0 Accessibility

- The UI components must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The UI must function correctly on all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires coordinated changes across the stack: database schema (EF Core migration), backend API/model, and frontend UI component.
- The data model for delivery destinations should be designed to be extensible for future types.

## 8.3.0.0 Technical Risks

- A poorly designed data model for storing delivery settings could make adding future delivery types difficult. A flexible JSON-based settings field is recommended.

## 8.4.0.0 Integration Points

- Backend: `ReportConfiguration` service and repository layers.
- Database: `ReportConfigurations` table and a new related table for `DeliveryDestinations`.
- Frontend: Report configuration wizard component.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify that a report can be created and saved with a synchronous API delivery target.
- Verify that attempting to save with an invalid timeout (non-numeric, out of range) fails with a clear error.
- Verify that the performance warning appears when a transformation script is part of the report config.
- Verify that editing and saving an existing report with this delivery target preserves the settings.

## 9.3.0.0 Test Data Needs

- A test user with the 'Administrator' role.
- An existing report configuration to edit.
- A report configuration that includes a transformation script to test the warning message.

## 9.4.0.0 Testing Tools

- Jest and React Testing Library for frontend unit tests.
- xUnit for backend unit/integration tests.
- A browser automation framework (e.g., Playwright, Cypress) for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for both frontend and backend logic, achieving >80% coverage for new code
- Integration testing for the API endpoint and database persistence completed successfully
- E2E test scenario for configuring the synchronous target is implemented and passing
- User interface reviewed for usability and accessibility (WCAG 2.1 AA)
- Database migration script is created and tested
- API documentation (OpenAPI/Swagger) is updated to reflect the new request body structure
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a prerequisite for the actual synchronous API execution (US-091). It should be scheduled in a sprint before or in the same sprint as US-091.
- Consider implementing this alongside its sibling story US-069 ('Configure asynchronous mode') as they share the same UI components.

## 11.4.0.0 Release Impact

- This feature is a key enabler for the on-demand API generation capability.

