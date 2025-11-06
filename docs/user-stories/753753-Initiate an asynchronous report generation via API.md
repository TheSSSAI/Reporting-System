# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-094 |
| Elaboration Date | 2025-01-17 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Initiate an asynchronous report generation via API |
| As A User Story | As an API User, I want to trigger a long-running r... |
| User Persona | System Integrator or any programmatic client inter... |
| Business Value | Enables robust, scalable, and non-blocking integra... |
| Functional Area | API and Report Generation |
| Story Theme | Secure External API |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful initiation of an asynchronous report job

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

An authenticated API user has a valid JWT and permissions to generate reports, AND a report with ID 'report-123' is configured with an 'API Response' delivery target set to asynchronous mode

### 3.1.5 When

The user sends a POST request to the endpoint '/api/v1/reports/report-123/generate'

### 3.1.6 Then

The system MUST respond with an HTTP 202 Accepted status code, AND the response body MUST be a JSON object containing a non-null string 'jobId' (formatted as a UUID) and a 'statusUrl' which is a valid, fully-qualified URL pointing to the job status endpoint (e.g., 'https://<host>/api/v1/jobs/{jobId}'), AND a new record MUST be created in the 'JobExecutionLog' table with the generated 'jobId' and a status of 'Queued'.

### 3.1.7 Validation Notes

Verify the HTTP status code, the structure and content of the JSON response body, and the creation of the corresponding record in the database.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Attempting to trigger a non-existent report

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

An authenticated API user has a valid JWT

### 3.2.5 When

The user sends a POST request to '/api/v1/reports/non-existent-report/generate'

### 3.2.6 Then

The system MUST respond with an HTTP 404 Not Found status code.

### 3.2.7 Validation Notes

Test with an ID that does not correspond to any report configuration in the database.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempting to trigger a report not configured for asynchronous API delivery

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

An authenticated API user has a valid JWT, AND a report with ID 'report-456' exists but is configured for synchronous API delivery or another delivery method entirely (e.g., email)

### 3.3.5 When

The user sends a POST request to '/api/v1/reports/report-456/generate'

### 3.3.6 Then

The system MUST respond with an HTTP 409 Conflict status code, AND the response body MUST contain a JSON object with an error message like '{"error": "Report is not configured for asynchronous generation."}'.

### 3.3.7 Validation Notes

This ensures the endpoint correctly distinguishes between report configurations and rejects inappropriate requests.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Attempting to trigger a report without authentication

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

A client attempts to make a request without a valid JWT in the Authorization header

### 3.4.5 When

The client sends a POST request to '/api/v1/reports/report-123/generate'

### 3.4.6 Then

The system MUST respond with an HTTP 401 Unauthorized status code.

### 3.4.7 Validation Notes

Verify by sending the request with no 'Authorization' header, or an invalid/expired token.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Attempting to trigger a report without sufficient permissions

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

An authenticated API user has a valid JWT but their role does not grant them permission to generate the specified report

### 3.5.5 When

The user sends a POST request to '/api/v1/reports/report-123/generate'

### 3.5.6 Then

The system MUST respond with an HTTP 403 Forbidden status code.

### 3.5.7 Validation Notes

Requires setting up a user with a role that lacks report generation permissions for testing.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Failure to enqueue the job in the scheduler

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

An authenticated API user sends a valid request to trigger an asynchronous report

### 3.6.5 When

The system fails to communicate with the job scheduler (e.g., Quartz.NET) or the database during job creation

### 3.6.6 Then

The system MUST respond with an HTTP 500 Internal Server Error or 503 Service Unavailable, AND it MUST NOT leave a partial or orphaned job record in the database.

### 3.6.7 Validation Notes

This can be tested by mocking the scheduler service to throw an exception and verifying the HTTP response and the database state.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Not Applicable. This is a backend API-only story.

## 4.2.0 User Interactions

- Not Applicable.

## 4.3.0 Display Requirements

- Not Applicable.

## 4.4.0 Accessibility Needs

- Not Applicable.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A report generation request can only be processed asynchronously if the target report configuration explicitly has an 'API Response' delivery destination with the mode set to 'asynchronous'.

### 5.1.3 Enforcement Point

Within the '/api/v1/reports/{id}/generate' controller logic, before job creation.

### 5.1.4 Violation Handling

The request is rejected with an HTTP 409 Conflict response.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The generated 'jobId' must be a globally unique and non-sequential identifier to ensure security and prevent enumeration.

### 5.2.3 Enforcement Point

During the creation of the 'JobExecutionLog' record.

### 5.2.4 Violation Handling

System design must use a UUID/GUID generation library.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-069

#### 6.1.1.2 Dependency Reason

This story depends on the ability to configure a report for asynchronous 'API Response' delivery. The logic cannot be implemented without the configuration being available.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-087

#### 6.1.2.2 Dependency Reason

The API endpoint must be secured, requiring the JWT authentication mechanism to be in place.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-095

#### 6.1.3.2 Dependency Reason

This story generates a 'statusUrl'. For the workflow to be testable and functional, the endpoint for polling job status (US-095) must exist.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core 8 API framework
- Quartz.NET 3.x for job scheduling
- Entity Framework Core 8 for database interaction with the 'JobExecutionLog' table
- Swashbuckle.AspNetCore for OpenAPI documentation

## 6.3.0.0 Data Dependencies

- Requires access to the 'ReportConfiguration' and 'JobExecutionLog' tables in the SQLite database.

## 6.4.0.0 External Dependencies

- None

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The P95 latency for the API response to this endpoint must be under 100ms, as it only involves creating a database record and scheduling a job, not the actual report generation.

## 7.2.0.0 Security

- The endpoint must be protected by JWT authentication.
- The generated 'jobId' must be a non-sequential, unpredictable string (e.g., UUID) to prevent enumeration attacks.
- The system must validate that the authenticated user has the necessary permissions to trigger the specified report.

## 7.3.0.0 Usability

- The API response must be well-formed JSON and adhere to the documented schema in the OpenAPI specification.

## 7.4.0.0 Accessibility

- Not Applicable.

## 7.5.0.0 Compatibility

- The API endpoint must be accessible by any standard HTTP client.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires integration with the Quartz.NET scheduling service, which adds a layer of complexity beyond a simple database write.
- Logic must correctly differentiate between reports configured for synchronous vs. asynchronous API delivery.
- Requires robust, transactional error handling to prevent inconsistent states if the database write succeeds but the job scheduling fails (or vice-versa).

## 8.3.0.0 Technical Risks

- Potential for race conditions if not handled carefully, although unlikely in this specific flow.
- Misconfiguration of the Quartz.NET scheduler could lead to jobs not being picked up from the queue.

## 8.4.0.0 Integration Points

- The primary integration is between the API controller layer, the data access layer (EF Core), and the job scheduling service (Quartz.NET).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify successful 202 response for a correctly configured async report.
- Verify 409 response for a report configured for sync delivery.
- Verify 404 for a non-existent report ID.
- Verify 401/403 for invalid authentication/authorization.
- Verify 5xx response and transactional rollback if the job scheduler fails to enqueue the job.
- Verify the structure and content of the JSON response body in the happy path.

## 9.3.0.0 Test Data Needs

- A report configuration saved with 'API Response' delivery set to 'asynchronous'.
- A report configuration saved with 'API Response' delivery set to 'synchronous'.
- User accounts with and without permissions to generate reports.

## 9.4.0.0 Testing Tools

- xUnit and Moq for backend unit tests.
- ASP.NET Core TestServer for in-memory integration tests.
- Postman or a similar API client for manual E2E validation.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing in automated tests.
- Code has been peer-reviewed and merged into the main branch.
- Unit and integration tests are implemented for the new logic, achieving at least 80% coverage.
- The OpenAPI/Swagger documentation for the '/api/v1/reports/{id}/generate' endpoint is updated to reflect the 202 response model.
- Security requirements (authentication, authorization, non-sequential ID) have been implemented and verified.
- Performance requirement (P95 < 100ms) has been benchmarked and met.
- The feature is deployed and successfully verified in the staging environment.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a prerequisite for US-095 (Poll a status URL) and US-096 (Retrieve the result). It should be planned in a sprint before or concurrently with them to deliver a complete end-to-end workflow.
- Ensure that the team has a clear understanding of the interaction pattern with the Quartz.NET scheduler before starting development.

## 11.4.0.0 Release Impact

This is a key feature for enabling robust automation and integration, making it a high-value item for any release targeting API users.

