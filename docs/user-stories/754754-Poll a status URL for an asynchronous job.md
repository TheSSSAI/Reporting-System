# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-095 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Poll a status URL for an asynchronous job |
| As A User Story | As an API User, I want to make a GET request to th... |
| User Persona | API User (e.g., a developer, an automated script, ... |
| Business Value | Enables reliable tracking of long-running, resourc... |
| Functional Area | API Interfaces |
| Story Theme | Asynchronous Report Generation API |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Polling a job that is currently running

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

An asynchronous report generation job with ID 'job-123' has been initiated and is in the 'Running' state

### 3.1.5 When

The API User sends an authenticated GET request to '/api/v1/jobs/job-123'

### 3.1.6 Then

The system returns an HTTP 200 OK status code

### 3.1.7 And

The 'resultUrl' and 'errorDetails' fields in the JSON response are null.

### 3.1.8 Validation Notes

Verify the response code is 200 and the JSON payload matches the specified structure for a running job.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Polling a job that has successfully completed

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

An asynchronous report generation job with ID 'job-456' has completed with a 'Succeeded' state

### 3.2.5 When

The API User sends an authenticated GET request to '/api/v1/jobs/job-456'

### 3.2.6 Then

The system returns an HTTP 200 OK status code

### 3.2.7 And

The 'errorDetails' field is null.

### 3.2.8 Validation Notes

Verify the response code is 200 and the JSON payload includes the 'Succeeded' status and the correct result URL.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Polling a job that has failed

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

An asynchronous report generation job with ID 'job-789' has failed during execution

### 3.3.5 When

The API User sends an authenticated GET request to '/api/v1/jobs/job-789'

### 3.3.6 Then

The system returns an HTTP 200 OK status code (as the request to get the status was successful)

### 3.3.7 And

The 'resultUrl' field is null.

### 3.3.8 Validation Notes

Verify the response code is 200 and the JSON payload includes the 'Failed' status and structured error information.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Polling for a non-existent job ID

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

No job exists with the ID 'non-existent-job'

### 3.4.5 When

The API User sends an authenticated GET request to '/api/v1/jobs/non-existent-job'

### 3.4.6 Then

The system returns an HTTP 404 Not Found status code.

### 3.4.7 Validation Notes

Verify the response code is exactly 404.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Polling without authentication

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

An asynchronous job with ID 'job-123' exists

### 3.5.5 When

A client sends a GET request to '/api/v1/jobs/job-123' without a valid JWT in the Authorization header

### 3.5.6 Then

The system returns an HTTP 401 Unauthorized status code.

### 3.5.7 Validation Notes

Verify the endpoint is protected and returns 401 for unauthenticated requests.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Not Applicable. This is an API-only feature.

## 4.2.0 User Interactions

- Not Applicable.

## 4.3.0 Display Requirements

- The API response must be a well-formed JSON object.
- The JSON response schema must be documented in the OpenAPI 3.0 specification.
- The response payload must include at least: 'jobId' (string), 'status' (string, enum: Queued, Running, Succeeded, Failed), 'startTime' (ISO 8601 string), 'endTime' (ISO 8601 string, nullable), 'resultUrl' (string, nullable), 'errorDetails' (object, nullable).

## 4.4.0 Accessibility Needs

- Not Applicable.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "The job status must only be one of the predefined states: 'Queued', 'Running', 'Succeeded', 'Failed'.", 'enforcement_point': 'Data model and API response serialization.', 'violation_handling': 'A violation would indicate a system bug. The system should log an error.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-094

#### 6.1.1.2 Dependency Reason

This story implements the status URL that is provided to the user by US-094. Without US-094, there is no asynchronous job to poll.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-087

#### 6.1.2.2 Dependency Reason

This endpoint must be secured using the JWT authentication mechanism established in US-087.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core for API endpoint implementation.
- Entity Framework Core for accessing the 'JobExecutionLog' table.
- Swashbuckle.AspNetCore for OpenAPI documentation.

## 6.3.0.0 Data Dependencies

- Requires access to the 'JobExecutionLog' table in the SQLite database to retrieve job status and metadata.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The P95 latency for the status endpoint must be under 50ms under normal load, as it is designed to be polled frequently.
- The endpoint logic should be lightweight, primarily reading from an indexed column in the database without performing complex calculations.

## 7.2.0.0 Security

- The endpoint must be protected and require a valid JWT for access.
- The 'jobId' used in the URL must be a non-sequential, unpredictable value (e.g., GUID) to prevent enumeration attacks.

## 7.3.0.0 Usability

- The API response structure must be consistent, predictable, and clearly documented in the OpenAPI specification.

## 7.4.0.0 Accessibility

- Not Applicable.

## 7.5.0.0 Compatibility

- The endpoint must be accessible by any standard HTTP client.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- The logic is a straightforward database lookup and data mapping.
- Requires creating a new controller action and a DTO for the response.
- Error handling for non-existent jobs and authentication is standard.

## 8.3.0.0 Technical Risks

- Potential for inefficient database queries if the 'jobId' column is not indexed, leading to performance degradation under load.

## 8.4.0.0 Integration Points

- Integrates with the authentication middleware.
- Reads data from the 'JobExecutionLog' table, which is written to by the report generation engine.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify correct response for a job in 'Running' state.
- Verify correct response and 'resultUrl' for a 'Succeeded' job.
- Verify correct response and 'errorDetails' for a 'Failed' job.
- Verify 404 response for a non-existent 'jobId'.
- Verify 401 response for a request without a valid token.
- An E2E test should chain US-094, US-095 (polling until completion), and US-096.

## 9.3.0.0 Test Data Needs

- Database records in the 'JobExecutionLog' table representing each possible job state ('Queued', 'Running', 'Succeeded', 'Failed').

## 9.4.0.0 Testing Tools

- xUnit for unit tests.
- Moq for mocking dependencies.
- ASP.NET Core TestServer or WebApplicationFactory for integration tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for controller and service layers, achieving >80% coverage for new code
- Integration testing completed successfully for all scenarios, including auth and error cases
- User interface reviewed and approved
- Performance requirements verified via targeted integration tests
- Security requirements validated via automated tests and manual review
- API endpoint is fully documented in the OpenAPI (Swagger) specification
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

2

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- Must be scheduled in a sprint after or concurrent with US-094.
- This story is a blocker for US-096, which consumes the 'resultUrl' provided by this endpoint.

## 11.4.0.0 Release Impact

This is a core component of the asynchronous API workflow. The feature is incomplete without it.

