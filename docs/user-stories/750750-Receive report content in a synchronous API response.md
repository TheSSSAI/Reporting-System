# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-091 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Receive report content in a synchronous API respon... |
| As A User Story | As an API User (System Integrator), I want to trig... |
| User Persona | API User (System Integrator or developer of an ext... |
| Business Value | Enables simple, real-time integration patterns by ... |
| Functional Area | API Interfaces |
| Story Theme | On-Demand Report Generation API |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-091-01

### 3.1.2 Scenario

Successful generation of a JSON report

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A report with ID '123' is configured with the 'JSON' output format and an 'API_RESPONSE' delivery target set to synchronous mode

### 3.1.5 When

An authenticated API User sends a POST request to '/api/v1/reports/123/generate'

### 3.1.6 Then



```
The system responds with an HTTP 200 OK status code within the 30-second timeout period.
AND The 'Content-Type' header of the response is 'application/json'.
AND The response body contains the complete, well-formed JSON data of the generated report.
```

### 3.1.7 Validation Notes

Verify using an API client like Postman or an integration test. Check the status code, 'Content-Type' header, and validate the JSON structure of the response body.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-091-02

### 3.2.2 Scenario

Successful generation of a PDF report

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

A report with ID '456' is configured with the 'PDF' output format and an 'API_RESPONSE' delivery target set to synchronous mode

### 3.2.5 When

An authenticated API User sends a POST request to '/api/v1/reports/456/generate'

### 3.2.6 Then



```
The system responds with an HTTP 200 OK status code.
AND The 'Content-Type' header of the response is 'application/pdf'.
AND The 'Content-Disposition' header is set to 'attachment; filename="<report_name>.pdf"'.
AND The response body contains the binary data of the generated PDF file.
```

### 3.2.7 Validation Notes

Verify using an API client. Check headers and save the response body as a .pdf file. The file should be a valid, viewable PDF document.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-091-03

### 3.3.2 Scenario

Successful generation of a CSV report

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

A report with ID '789' is configured with the 'CSV' output format and an 'API_RESPONSE' delivery target set to synchronous mode

### 3.3.5 When

An authenticated API User sends a POST request to '/api/v1/reports/789/generate'

### 3.3.6 Then



```
The system responds with an HTTP 200 OK status code.
AND The 'Content-Type' header of the response is 'text/csv'.
AND The 'Content-Disposition' header is set to 'attachment; filename="<report_name>.csv"'.
AND The response body contains the RFC 4180 compliant CSV data.
```

### 3.3.7 Validation Notes

Verify using an API client. Check headers and save the response body as a .csv file. The file should open correctly in a spreadsheet application.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-091-04

### 3.4.2 Scenario

Report generation fails due to an internal error

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

A report is configured for synchronous generation, but its underlying data source is unavailable

### 3.4.5 When

An authenticated API User sends a POST request to generate the report

### 3.4.6 Then



```
The system responds with an HTTP 500 Internal Server Error status code.
AND The response body contains a structured JSON object with an error message, e.g., {"error": "Failed to generate report due to data source connection failure."}
```

### 3.4.7 Validation Notes

Simulate a data source failure (e.g., incorrect connection string, stopped database). Trigger the API call and verify the 500 status and error payload.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-091-05

### 3.5.2 Scenario

Attempting to generate a non-existent report

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

An authenticated API User has a valid JWT

### 3.5.5 When

The user sends a POST request to '/api/v1/reports/invalid-id/generate'

### 3.5.6 Then

The system responds with an HTTP 404 Not Found status code.

### 3.5.7 Validation Notes

Use any UUID or ID that does not correspond to a report and verify the 404 response.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-091-06

### 3.6.2 Scenario

Report with zero records is generated successfully

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

A report configured for synchronous CSV generation points to a data source that returns no records

### 3.6.5 When

An authenticated API User sends a POST request to generate the report

### 3.6.6 Then



```
The system responds with an HTTP 200 OK status code.
AND The response body contains only the CSV header row.
```

### 3.6.7 Validation Notes

Set up a data source (e.g., an empty table or file) and run the report. Verify the 200 OK response and that the body contains the expected headers but no data rows.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Not Applicable (API Endpoint)

## 4.2.0 User Interactions

- Not Applicable (API Endpoint)

## 4.3.0 Display Requirements

- Not Applicable (API Endpoint)

## 4.4.0 Accessibility Needs

- Not Applicable (API Endpoint)

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-API-01

### 5.1.2 Rule Description

The synchronous generation endpoint must only process reports explicitly configured for synchronous 'API_RESPONSE' delivery.

### 5.1.3 Enforcement Point

Controller logic for POST /api/v1/reports/{id}/generate

### 5.1.4 Violation Handling

If the report is configured for any other delivery method (including asynchronous API), the request must be rejected with an HTTP 409 Conflict status. (Handled by US-093)

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-API-02

### 5.2.2 Rule Description

A synchronous report generation request must not exceed a hard timeout of 30 seconds.

### 5.2.3 Enforcement Point

The report generation service or the hosting controller.

### 5.2.4 Violation Handling

If the generation process exceeds 30 seconds, the connection must be terminated and an HTTP 408 Request Timeout response must be returned. (Handled by US-092)

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-068

#### 6.1.1.2 Dependency Reason

The ability to configure a report for synchronous API delivery must exist before this endpoint can be implemented.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-087

#### 6.1.2.2 Dependency Reason

The API authentication mechanism (JWT) must be in place to secure this endpoint.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-090

#### 6.1.3.2 Dependency Reason

This story is a specific implementation path for the general on-demand generation trigger defined in US-090.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core 8 Web API framework
- The core Report Generation Engine (including data ingestion, transformation, and serialization components)
- JWT authentication middleware

## 6.3.0.0 Data Dependencies

- Access to the SQLite configuration database to retrieve the ReportConfiguration entity.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The entire request-to-response cycle for a synchronous report must complete in under 30 seconds.
- The API endpoint should handle at least 20 concurrent synchronous requests on recommended hardware without significant degradation.

## 7.2.0.0 Security

- The endpoint must be protected and require a valid JWT bearer token for access.
- The user associated with the token must have permissions to execute the requested report.
- The system must properly sanitize all inputs (e.g., report ID) to prevent injection attacks.

## 7.3.0.0 Usability

- The API response headers ('Content-Type', 'Content-Disposition') must be correctly and consistently set to provide a good developer experience for API consumers.

## 7.4.0.0 Accessibility

- Not Applicable

## 7.5.0.0 Compatibility

- The endpoint must be consumable by any standard HTTP/1.1 or HTTP/2 client.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementing a reliable timeout mechanism for the entire generation pipeline.
- Correctly handling various MIME types and binary vs. text data in the HTTP response.
- Robust error handling to translate internal exceptions into meaningful HTTP status codes and error payloads.
- Coordinating logic with the asynchronous path (US-094) within the same controller action.

## 8.3.0.0 Technical Risks

- The underlying report generation process might not be easily cancellable, making the 30-second timeout difficult to enforce cleanly.
- Memory leaks or high resource consumption if the generated report content is not handled efficiently as a stream before being written to the response.

## 8.4.0.0 Integration Points

- ASP.NET Core Controller Layer
- Authentication/Authorization Middleware
- Core ReportGenerationService
- Configuration Data Access Layer

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Test successful generation for each supported output format (JSON, CSV, PDF, HTML, TXT).
- Test the 404 Not Found case for an invalid report ID.
- Test the 401/403 Unauthorized/Forbidden cases for invalid/insufficiently-permissioned JWTs.
- Test the 500 Internal Server Error case by mocking a failure in the generation service.
- Test the timeout scenario (US-092) to ensure a 408 response is returned.
- Test the conflict scenario (US-093) to ensure a 409 response is returned for non-synchronous reports.

## 9.3.0.0 Test Data Needs

- A set of pre-configured reports in the test database for each output format, all set to synchronous API delivery.
- Sample data sources (e.g., CSV files, a test database) for these reports to pull from.

## 9.4.0.0 Testing Tools

- xUnit and Moq for backend unit tests.
- ASP.NET Core TestServer for integration tests.
- Postman or a similar API client for manual E2E testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for the controller logic, achieving >80% coverage
- Integration testing completed successfully for happy paths and key error conditions (200, 404, 500)
- Security requirements (JWT authentication) validated
- The endpoint is fully documented in the OpenAPI/Swagger specification (as part of US-097)
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is part of a feature set including US-092, US-093, and US-094. They should be developed and tested together if possible to ensure cohesive behavior of the generation endpoint.
- Requires prerequisite stories (US-068, US-087) to be completed in a prior sprint.

## 11.4.0.0 Release Impact

This is a key feature for enabling real-time system integrations and is a major selling point of the API.

