# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-067 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Configure report delivery via a RESTful API call |
| As A User Story | As an Administrator, I want to configure a report ... |
| User Persona | Administrator: A technical user responsible for sy... |
| Business Value | Enables seamless, automated, and near real-time in... |
| Functional Area | Report Delivery Configuration |
| Story Theme | System Integration and Extensibility |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Administrator configures a new API delivery destination with API Key authentication

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

An Administrator is creating or editing a report's delivery configuration

### 3.1.5 When

The Administrator selects 'RESTful API' as the delivery type, enters a valid HTTPS URL, selects the 'POST' method, chooses 'API Key' for authentication, provides a header name (e.g., 'X-API-Key') and a key value, and adds a 'Content-Type: application/json' header

### 3.1.6 Then

The system saves the configuration, storing the API key value securely encrypted.

### 3.1.7 Validation Notes

Verify in the database that the API key is not stored in plaintext. The UI should mask the key value after it has been entered and saved.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Administrator successfully tests the API delivery configuration

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

A valid API delivery destination is configured

### 3.2.5 When

The Administrator clicks the 'Test Delivery' button

### 3.2.6 Then

The system sends a predefined sample JSON payload to the configured endpoint with the correct method, headers, and authentication, and the external API returns an HTTP 2xx status code. The UI displays a clear success message to the Administrator.

### 3.2.7 Validation Notes

Use a mock API endpoint to verify the request's structure (method, URL, headers, body) is correct. The UI message should be user-friendly, e.g., 'Connection successful (Status: 200 OK)'.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

A report job successfully delivers its payload via the configured API endpoint

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

A report is configured to deliver its output to a valid and available API endpoint

### 3.3.5 When

The scheduled or on-demand report job runs and successfully generates its data

### 3.3.6 Then

The system sends the generated report data as the request body to the configured API endpoint, the delivery is successful (receives a 2xx response), and the job execution log records the successful delivery, including the HTTP status code.

### 3.3.7 Validation Notes

Check the job execution log for a success entry for the API delivery step. Verify the external system received the data correctly.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Administrator attempts to save an incomplete API delivery configuration

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

An Administrator is configuring an API delivery destination

### 3.4.5 When

The Administrator tries to save the configuration without providing a required field, such as the Endpoint URL or authentication credentials

### 3.4.6 Then

The UI prevents saving the configuration and displays clear validation errors highlighting the missing fields.

### 3.4.7 Validation Notes

Test for missing URL, missing credentials for selected auth type, and invalid URL format.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

The API delivery test fails due to an authentication error

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

An API delivery destination is configured with an incorrect API key or password

### 3.5.5 When

The Administrator clicks the 'Test Delivery' button

### 3.5.6 Then

The external API returns an HTTP 401 Unauthorized or 403 Forbidden status code, and the UI displays a specific error message, e.g., 'Test failed: Authentication error (Status: 401 Unauthorized)'.

### 3.5.7 Validation Notes

Use a mock API to simulate a 401 response. The error message should be clear and actionable for the user.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

A report job's API delivery fails due to a server error

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

A report is configured to deliver to an API endpoint that is down or malfunctioning

### 3.6.5 When

The report job runs and attempts to deliver the data

### 3.6.6 Then

The system attempts the API call, retries according to the configured resiliency policy (e.g., 3 times), and after the final failure (e.g., receiving a 503 Service Unavailable), it marks the delivery step as 'Failed', marks the entire report job as 'Failed', and logs the detailed error, including the final status code and any response body, in the job execution log.

### 3.6.7 Validation Notes

Verify the retry mechanism is triggered. Check the job log for detailed error information. Confirm the overall job status is 'Failed'.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Administrator configures API delivery with Basic Authentication

### 3.7.3 Scenario Type

Alternative_Flow

### 3.7.4 Given

An Administrator is configuring an API delivery destination

### 3.7.5 When

The Administrator selects 'Basic Authentication' as the authentication type

### 3.7.6 Then

The UI dynamically displays input fields for 'Username' and 'Password', and the system correctly constructs the 'Authorization: Basic <base64_encoded_string>' header for test and runtime calls.

### 3.7.7 Validation Notes

Verify the Base64 encoding is correct and the header is properly formed in the outgoing request.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A 'RESTful API' option in the 'Delivery Type' dropdown menu.
- A dynamic configuration form for the API destination.
- Text input for 'Endpoint URL' with format validation.
- Dropdown for 'HTTP Method' (options: POST, PUT, PATCH).
- Dropdown for 'Authentication' (options: None, Basic Authentication, API Key, Bearer Token).
- Conditional input fields for authentication details (e.g., Username/Password, Header Name/Key).
- A key-value pair editor for adding 'Custom Headers'.
- A 'Test Delivery' button.
- A non-editable area to display feedback from the test (success or error messages).

## 4.2.0 User Interactions

- Selecting an authentication type dynamically shows/hides the relevant credential fields.
- Password and secret key fields should mask the input.
- Clicking 'Test Delivery' triggers an API call and displays feedback without saving the configuration.
- Validation errors are displayed inline next to the problematic fields upon attempting to save.

## 4.3.0 Display Requirements

- Clear success or failure messages must be displayed after a delivery test.
- Saved secret values (passwords, keys) must always be masked in the UI (e.g., '**********').

## 4.4.0 Accessibility Needs

- All new form fields must have `for`/`id` linked labels.
- The dynamic UI changes must be manageable by screen readers.
- All UI elements must meet WCAG 2.1 Level AA contrast and keyboard navigation standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Sensitive credentials for API delivery (passwords, tokens, keys) must be stored encrypted at rest.

### 5.1.3 Enforcement Point

Backend, upon saving the connector configuration.

### 5.1.4 Violation Handling

N/A - System design must enforce this. A failure to encrypt is a critical bug.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

An API delivery attempt is only considered successful if the external server responds with an HTTP 2xx status code.

### 5.2.3 Enforcement Point

Report delivery engine, after receiving the API response.

### 5.2.4 Violation Handling

Any non-2xx response will cause the delivery step to be marked as 'Failed' after all retries are exhausted.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

Failed API delivery attempts must be retried according to the system's resiliency policy.

### 5.3.3 Enforcement Point

Report delivery engine, upon detecting a transient failure (e.g., network error, 5xx status).

### 5.3.4 Violation Handling

The system will retry 3 times with exponential backoff before marking the delivery as permanently failed.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-051

#### 6.1.1.2 Dependency Reason

This story provides the report configuration wizard where delivery destinations are added.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-057

#### 6.1.2.2 Dependency Reason

This story establishes the generic framework for configuring and managing different types of delivery destinations.

## 6.2.0.0 Technical Dependencies

- .NET Secret Management / DPAPI (SRS 3.3, 6.4) for secure credential storage.
- Polly library (SRS 3.2) for implementing retry and timeout logic.
- Serilog library (SRS 3.2) for structured logging of delivery outcomes.
- Entity Framework Core (SRS 3.2.1) for saving the new configuration entity to the SQLite database.

## 6.3.0.0 Data Dependencies

- Requires access to the report's generated data (JSON, CSV, etc.) to form the request body.

## 6.4.0.0 External Dependencies

- Requires a network path from the host server to the target external API endpoint.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call must have a configurable timeout (default 30 seconds) to prevent blocking the job scheduler.
- The delivery handler should not significantly increase the overall report generation time for reasonably fast external APIs.

## 7.2.0.0 Security

- All API calls must be made over HTTPS/TLS 1.2+.
- All secrets (passwords, API keys, bearer tokens) must be encrypted at rest using DPAPI.
- Secrets must never be written to logs or exposed in the UI after initial entry.

## 7.3.0.0 Usability

- The configuration UI should be intuitive for a technical administrator.
- Error messages from test or runtime failures must be clear, specific, and actionable.

## 7.4.0.0 Accessibility

- The configuration form must be fully compliant with WCAG 2.1 Level AA.

## 7.5.0.0 Compatibility

- The feature must function correctly in all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementing secure storage and handling of various authentication credentials.
- Creating a dynamic and user-friendly frontend form for the configuration.
- Integrating the Polly library for robust retry and timeout policies.
- Requires a mock API server for reliable automated testing.

## 8.3.0.0 Technical Risks

- Improper handling of secrets could lead to a major security vulnerability.
- Failure to correctly implement retry logic could lead to either data loss (no retries) or system overload (infinite retries).

## 8.4.0.0 Integration Points

- The Report Configuration data model in the SQLite database.
- The Quartz.NET job execution pipeline.
- The system's central logging service (Serilog).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify each authentication type (None, Basic, API Key, Bearer) is correctly implemented.
- Test against a mock API that returns various status codes (200, 201, 400, 401, 403, 404, 500, 503).
- Test the retry mechanism by having the mock API fail the first two times and succeed on the third.
- Test the timeout mechanism by having the mock API delay its response.
- Perform a security review of the credential storage and handling implementation.

## 9.3.0.0 Test Data Needs

- Sample report data in JSON format to be used as the request body for tests.
- Dummy credentials for each authentication type.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- A mock API server tool like WireMock.Net or a simple Kestrel-based mock for integration tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team, with a specific focus on security aspects
- Unit tests implemented for backend and frontend components, achieving >80% coverage
- Integration testing against a mock API endpoint completed successfully for all scenarios
- User interface reviewed and approved by a UX designer or product owner
- Security requirements validated, including confirmation of encrypted storage
- Documentation for the 'RESTful API' delivery destination is created in the User Guide
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story enables a key integration pattern for many customers.
- The security implications require careful implementation and review, which should be factored into the timeline.
- A mock API for testing should be set up at the beginning of the sprint.

## 11.4.0.0 Release Impact

- This is a significant new feature that should be highlighted in release notes as a major enhancement to the system's integration capabilities.

