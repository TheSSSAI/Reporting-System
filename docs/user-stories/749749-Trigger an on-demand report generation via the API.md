# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-090 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Trigger an on-demand report generation via the API |
| As A User Story | As an API User (System/Integrator), I want to trig... |
| User Persona | API User (System/Integrator). This represents a de... |
| Business Value | Enables real-time, event-driven reporting, allowin... |
| Functional Area | API and System Integration |
| Story Theme | On-Demand Report Generation |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successfully trigger a report configured for asynchronous generation

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an authenticated API user with the 'Administrator' role and a valid JWT

### 3.1.5 When

I send a POST request to `/api/v1/reports/{id}/generate` for a report that is configured for asynchronous 'API Response' delivery

### 3.1.6 Then

The system accepts the request, queues a new report generation job in the scheduler, and returns an HTTP 202 Accepted response.

### 3.1.7 Validation Notes

Verify the HTTP status code is 202. Check the job scheduler (e.g., Quartz.NET tables or logs) to confirm a new job for the specified report ID has been queued.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Successfully trigger a report configured for synchronous generation

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am an authenticated API user with the 'Administrator' role and a valid JWT

### 3.2.5 When

I send a POST request to `/api/v1/reports/{id}/generate` for a report that is configured for synchronous 'API Response' delivery

### 3.2.6 Then

The system accepts the request and begins executing the report generation job immediately in a blocking fashion.

### 3.2.7 Validation Notes

The system should not return a response until the job is complete or times out. The successful response content is handled in US-091. This AC verifies the job is initiated.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempt to trigger a report that does not exist

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am an authenticated API user with the 'Administrator' role and a valid JWT

### 3.3.5 When

I send a POST request to `/api/v1/reports/{id}/generate` where the {id} does not correspond to any existing report

### 3.3.6 Then

The system returns an HTTP 404 Not Found response.

### 3.3.7 Validation Notes

Verify the HTTP status code is 404 and the response body, if any, indicates the resource was not found.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Attempt to trigger a report without authentication

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am an unauthenticated user (no JWT provided)

### 3.4.5 When

I send a POST request to `/api/v1/reports/{id}/generate`

### 3.4.6 Then

The system returns an HTTP 401 Unauthorized response.

### 3.4.7 Validation Notes

Verify the HTTP status code is 401. The response should not contain any sensitive information.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Attempt to trigger a report without sufficient permissions

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am an authenticated user with the 'Viewer' role (who lacks generation permissions)

### 3.5.5 When

I send a POST request to `/api/v1/reports/{id}/generate`

### 3.5.6 Then

The system returns an HTTP 403 Forbidden response.

### 3.5.7 Validation Notes

Verify the HTTP status code is 403. This confirms role-based access control is enforced on the endpoint.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Attempt to trigger a report not configured for API delivery

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

I am an authenticated API user with the 'Administrator' role and a valid JWT

### 3.6.5 When

I send a POST request to `/api/v1/reports/{id}/generate` for a report that does NOT have 'API Response' as a configured delivery destination

### 3.6.6 Then

The system returns an HTTP 409 Conflict response with a JSON body containing a descriptive error message like `{"error": "Report is not configured for API delivery."}`.

### 3.6.7 Validation Notes

Verify the HTTP status code is 409 and the response body contains the expected error message.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

A successful trigger is recorded in the audit log

### 3.7.3 Scenario Type

Happy_Path

### 3.7.4 Given

The audit logging system is operational

### 3.7.5 When

An authorized user successfully triggers a report generation via the API

### 3.7.6 Then

A new entry is created in the audit log containing the timestamp, responsible user ID, source IP address, action ('On-Demand Report Generation Triggered'), and the target report ID.

### 3.7.7 Validation Notes

Query the audit log table or file to confirm the presence and correctness of the new log entry.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Attempt to trigger a report using an incorrect HTTP method

### 3.8.3 Scenario Type

Error_Condition

### 3.8.4 Given

I am an authenticated API user with the 'Administrator' role and a valid JWT

### 3.8.5 When

I send a GET, PUT, or DELETE request to `/api/v1/reports/{id}/generate`

### 3.8.6 Then

The system returns an HTTP 405 Method Not Allowed response.

### 3.8.7 Validation Notes

Verify the HTTP status code is 405 and the `Allow` header in the response is set to 'POST'.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Not applicable. This is an API-only feature.

## 4.2.0 User Interactions

- The user interacts by making an HTTP POST request to a defined endpoint.

## 4.3.0 Display Requirements

- The API endpoint must be documented in the OpenAPI 3.0 (Swagger) specification, including the path, method, parameters, expected responses (202, 401, 403, 404, 409), and security schema.

## 4.4.0 Accessibility Needs

- Not applicable.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Only reports explicitly configured with an 'API Response' delivery destination can be triggered via this endpoint.

### 5.1.3 Enforcement Point

Within the API controller logic before a job is queued or executed.

### 5.1.4 Violation Handling

The request is rejected with an HTTP 409 Conflict status and a descriptive error message.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Only users with 'Administrator' privileges can trigger on-demand report generation via the API.

### 5.2.3 Enforcement Point

At the API endpoint's authorization middleware.

### 5.2.4 Violation Handling

The request is rejected with an HTTP 403 Forbidden status.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-087

#### 6.1.1.2 Dependency Reason

API authentication using JWT must be implemented to secure this endpoint.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-057

#### 6.1.2.2 Dependency Reason

The ability to configure delivery destinations for a report is required.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-068

#### 6.1.3.2 Dependency Reason

The system needs to understand the configuration for synchronous API delivery to correctly route the job.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-069

#### 6.1.4.2 Dependency Reason

The system needs to understand the configuration for asynchronous API delivery to correctly queue the job.

### 6.1.5.0 Story Id

#### 6.1.5.1 Story Id

US-101

#### 6.1.5.2 Dependency Reason

The audit logging framework must be in place to record the trigger event.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core 8 Web API framework
- ASP.NET Core Identity for authentication and authorization
- Quartz.NET 3.x for job scheduling and execution
- Entity Framework Core 8 for accessing report configuration data

## 6.3.0.0 Data Dependencies

- Requires existing report configurations in the SQLite database to target for generation.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API endpoint must acknowledge the request and (for async jobs) return a response with a P95 latency of under 200ms, as per SRS 6.1.

## 7.2.0.0 Security

- The endpoint must be protected by JWT authentication as defined in SRS 3.3.
- The endpoint must enforce role-based access control (RBAC), restricting access to Administrators.
- The report ID parameter should be validated to prevent injection-style attacks.

## 7.3.0.0 Usability

- The API endpoint must be clearly and accurately documented in the OpenAPI specification (Swagger UI) for ease of use by developers.

## 7.4.0.0 Accessibility

- Not applicable.

## 7.5.0.0 Compatibility

- The API should be consumable by any standard HTTP client.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires interaction with the Quartz.NET scheduling service, which is a separate system component.
- Involves conditional logic to differentiate between synchronous and asynchronous report configurations.
- Requires careful and precise error handling to return the correct HTTP status codes for various failure scenarios.
- Integration with the audit logging service adds another point of interaction.

## 8.3.0.0 Technical Risks

- Improper configuration of the interaction with Quartz.NET could lead to jobs not being scheduled correctly or resource leaks.
- Failure to handle exceptions from the job scheduler gracefully could result in unhandled 500 errors.

## 8.4.0.0 Integration Points

- ASP.NET Core Authentication/Authorization Middleware
- Entity Framework Core DbContext for data access
- Quartz.NET IScheduler service
- Serilog-based Audit Logging service

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify successful triggering of both sync and async reports.
- Verify all specified error responses (401, 403, 404, 405, 409) under the correct conditions.
- Verify that an audit log is created upon successful trigger.
- Verify that a user with a 'Viewer' role cannot trigger a report.
- Verify that a report without an 'API Response' delivery target cannot be triggered.

## 9.3.0.0 Test Data Needs

- A user account with 'Administrator' role.
- A user account with 'Viewer' role.
- A report configuration set for synchronous API delivery.
- A report configuration set for asynchronous API delivery.
- A report configuration with no API delivery target.

## 9.4.0.0 Testing Tools

- xUnit and Moq for backend unit tests.
- An HTTP client library (e.g., in an integration test project) or Postman for E2E and manual testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for the controller logic, achieving >80% coverage
- Integration tests implemented to validate the request pipeline, including auth, db access, and scheduler interaction
- Security requirements (authentication, authorization) validated
- The new endpoint is fully documented in the OpenAPI/Swagger specification
- Audit logging for the trigger event is verified
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a foundational blocker for subsequent API generation stories (US-091 to US-096). It must be completed before them.
- The development team should have a clear contract for how the API controller will interact with the Quartz.NET scheduler service.

## 11.4.0.0 Release Impact

Enables a major feature set for system integration. This is a key selling point for customers who need to automate reporting.

