# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-069 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Configure an API delivery target for asynchronous ... |
| As A User Story | As an Administrator, I want to configure an 'API R... |
| User Persona | Administrator |
| Business Value | Enables the system to reliably handle long-running... |
| Functional Area | Report Configuration & Delivery |
| Story Theme | Secure External API |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

UI option for asynchronous mode becomes available

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

An Administrator is creating or editing a report configuration in the Control Panel

### 3.1.5 When

the Administrator adds a delivery destination and selects the type 'API Response'

### 3.1.6 Then

a new UI element, labeled 'Execution Mode', appears with options 'Synchronous' and 'Asynchronous'.

### 3.1.7 Validation Notes

Verify that the 'Execution Mode' radio button group or dropdown is rendered only when 'API Response' is the selected delivery type.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Asynchronous mode configuration is saved correctly

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

the Administrator has selected 'API Response' as the delivery type and 'Asynchronous' as the execution mode

### 3.2.5 When

the Administrator saves the report configuration

### 3.2.6 Then

the system persists the delivery configuration with the execution mode set to 'Asynchronous'.

### 3.2.7 Validation Notes

Inspect the database record for the report's delivery configuration to confirm the 'Asynchronous' mode is stored.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

UI option for execution mode is hidden for other delivery types

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

the 'Execution Mode' option is visible for an 'API Response' delivery target

### 3.3.5 When

the Administrator changes the delivery type to 'Email', 'FTP', or any other non-API type

### 3.3.6 Then

the 'Execution Mode' UI element is hidden.

### 3.3.7 Validation Notes

Cycle through all available delivery types to ensure the 'Execution Mode' option only appears for 'API Response'.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

API trigger for an asynchronously configured report returns HTTP 202 Accepted

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

a report is configured with an 'API Response' delivery target in 'Asynchronous' mode

### 3.4.5 When

an authenticated API user sends a POST request to the '/api/v1/reports/{id}/generate' endpoint for that report

### 3.4.6 Then

the API immediately responds with an HTTP status code of 202 Accepted.

### 3.4.7 Validation Notes

Use an API client to trigger the report and verify the HTTP status code in the response.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

API response for asynchronous trigger contains job details

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

an asynchronous report generation has been successfully triggered via the API

### 3.5.5 When

the client receives the HTTP 202 Accepted response

### 3.5.6 Then

the response body must be a JSON object containing a non-null 'jobId' (string) and a 'statusUrl' (string) which is a valid URL.

### 3.5.7 Validation Notes

Verify the structure and content of the JSON response body. The 'jobId' should be a unique identifier (e.g., UUID) and the 'statusUrl' should correctly format to '/api/v1/jobs/{jobId}'.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A radio button group or dropdown menu labeled 'Execution Mode' within the delivery destination configuration panel.
- Options within the control: 'Synchronous' and 'Asynchronous'.
- A tooltip or help icon next to the 'Execution Mode' label explaining the use case for each mode (e.g., 'Asynchronous is recommended for reports that take longer than 30 seconds to generate').

## 4.2.0 User Interactions

- The 'Execution Mode' control is conditionally rendered, appearing only when the delivery type is 'API Response'.
- Selecting an execution mode updates the form's state.
- The selection is persisted upon saving the report configuration.

## 4.3.0 Display Requirements

- The default selection for 'Execution Mode' should be 'Synchronous'.
- The selected mode must be clearly visible when editing an existing 'API Response' delivery target.

## 4.4.0 Accessibility Needs

- The 'Execution Mode' control must have a proper 'for' attribute linking the label to the input.
- The control must be fully navigable and operable using a keyboard.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "The 'Execution Mode' setting is only applicable to the 'API Response' delivery type.", 'enforcement_point': 'Report Configuration UI and Backend Validation', 'violation_handling': 'The UI will not show the option for other types. Any backend attempt to save this setting for another type should be ignored or rejected.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-057

#### 6.1.1.2 Dependency Reason

Provides the base functionality for configuring report delivery destinations.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-068

#### 6.1.2.2 Dependency Reason

Implements the 'API Response' delivery target and the synchronous mode, which this story builds upon by adding the asynchronous option.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-090

#### 6.1.3.2 Dependency Reason

Establishes the API endpoint '/api/v1/reports/{id}/generate' that this story modifies.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-094

#### 6.1.4.2 Dependency Reason

Implements the backend logic for the API to return an HTTP 202 response, which is triggered by the configuration set in this story.

### 6.1.5.0 Story Id

#### 6.1.5.1 Story Id

US-095

#### 6.1.5.2 Dependency Reason

The 'statusUrl' returned by an async trigger requires this story's endpoint to be functional for the workflow to be complete.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Web API framework for the controller logic.
- Entity Framework Core for data model updates and migrations.
- React component library for the delivery configuration UI.
- Quartz.NET for enqueuing the report generation job in the background.

## 6.3.0.0 Data Dependencies

- The 'ReportConfiguration' and associated delivery settings tables in the SQLite database must be extensible to include the execution mode.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API response for an asynchronous trigger (HTTP 202) must have a P99 latency of less than 200ms, as it should not perform any long-running work.

## 7.2.0.0 Security

- The generated 'jobId' must be a cryptographically secure, non-sequential unique identifier (e.g., UUID/GUID) to prevent enumeration.
- The 'statusUrl' endpoint must be protected by the same JWT authentication as all other secure API endpoints.

## 7.3.0.0 Usability

- The purpose of synchronous vs. asynchronous modes should be clearly explained in the UI to guide the administrator to the correct choice.

## 7.4.0.0 Accessibility

- All new UI elements must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The UI must function correctly on all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires database schema changes and a data migration.
- Involves modifying the core API controller logic to support a new execution branch.
- Requires integration with the Quartz.NET job scheduler to enqueue the report generation task.
- Backend state management for the job (Queued, Running, etc.) must be handled robustly.

## 8.3.0.0 Technical Risks

- Potential for race conditions if the job status is not updated atomically.
- Incorrectly configured background job processing could lead to dropped tasks.

## 8.4.0.0 Integration Points

- Frontend: Report configuration form.
- Backend: Report generation API controller.
- Backend: Job scheduling service (Quartz.NET).
- Database: Report configuration tables.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- API

## 9.2.0.0 Test Scenarios

- Verify UI correctly shows/hides the 'Execution Mode' option.
- Verify saving a report with 'Asynchronous' mode persists the setting.
- End-to-end test: Configure a report via UI, trigger it via API, and validate the HTTP 202 response and its JSON payload.
- API test: Directly call the endpoint for a report configured for synchronous mode to ensure it does NOT return 202.
- Verify that a new entry is created in the 'JobExecutionLog' table with a 'Queued' status upon a successful async trigger.

## 9.3.0.0 Test Data Needs

- A pre-configured report definition that can be modified during tests.
- Valid user credentials for an Administrator (for UI tests) and an API user.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- Postman or a similar HTTP client for manual API testing.
- Automated API testing framework (e.g., using C# HttpClient in an integration test suite).

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >80% coverage for new code
- Integration testing completed successfully, verifying the API behavior and database state change
- User interface reviewed and approved by a UX designer or product owner
- Performance requirements for the API response time are verified
- Security requirements for jobId generation and endpoint protection are validated
- API documentation (OpenAPI/Swagger) is updated to reflect the new possible HTTP 202 response for the generation endpoint
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🟡 Medium

## 11.3.0.0 Sprint Considerations

- This story is part of a larger feature set for asynchronous API operations. It should be planned alongside US-094, US-095, and US-096 to deliver the full end-to-end workflow.
- Requires a database migration, which should be coordinated with other DB changes in the sprint.

## 11.4.0.0 Release Impact

- Enables a critical feature for enterprise integration and automation. The full value is realized when the entire async workflow (trigger, poll, retrieve) is complete.

