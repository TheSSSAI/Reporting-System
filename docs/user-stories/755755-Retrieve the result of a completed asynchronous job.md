# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-096 |
| Elaboration Date | 2025-01-20 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Retrieve the result of a completed asynchronous jo... |
| As A User Story | As an API User, I want to call a dedicated result ... |
| User Persona | API User (e.g., an external system, a developer's ... |
| Business Value | Enables the completion of the asynchronous report ... |
| Functional Area | Secure External API |
| Story Theme | On-Demand Report Generation |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Retrieve result for a successfully completed job

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

An asynchronous report generation job with ID 'job-123' has a status of 'Succeeded' and its output file (e.g., a PDF) is available in storage, AND the API User is authenticated with a valid JWT and has permission to access the report.

### 3.1.5 When

The API User sends a GET request to the result endpoint '/api/v1/jobs/job-123/result'.

### 3.1.6 Then

The system responds with an HTTP 200 OK status code, AND the 'Content-Type' header correctly reflects the report format (e.g., 'application/pdf'), AND the 'Content-Disposition' header suggests a filename for download, AND the response body contains the raw binary or text data of the generated report.

### 3.1.7 Validation Notes

Verify the HTTP status, headers, and that the downloaded content matches the expected report file.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Error Condition: Job ID does not exist

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

The API User is authenticated.

### 3.2.5 When

The API User sends a GET request to '/api/v1/jobs/non-existent-job-id/result'.

### 3.2.6 Then

The system responds with an HTTP 404 Not Found status code, AND the response body is a JSON object with an error message, such as '{"error": "Job with ID 'non-existent-job-id' not found."}'.

### 3.2.7 Validation Notes

Test with a UUID or ID that is guaranteed not to be in the database.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Error Condition: Job is still running or queued

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

A job with ID 'job-456' has a status of 'Running' or 'Queued', AND the API User is authenticated.

### 3.3.5 When

The API User sends a GET request to '/api/v1/jobs/job-456/result'.

### 3.3.6 Then

The system responds with an HTTP 409 Conflict status code, AND the response body is a JSON object indicating the job is not yet complete, such as '{"status": "Running", "message": "Job is not yet complete. Please continue polling the status URL."}'.

### 3.3.7 Validation Notes

Create a job in a 'Running' state in a test environment and attempt to fetch its result.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Error Condition: Job has failed

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

A job with ID 'job-789' has a status of 'Failed', AND the API User is authenticated.

### 3.4.5 When

The API User sends a GET request to '/api/v1/jobs/job-789/result'.

### 3.4.6 Then

The system responds with an HTTP 410 Gone status code, AND the response body is a JSON object explaining the failure, such as '{"status": "Failed", "message": "Job failed to generate a result. Check job logs for details."}'.

### 3.4.7 Validation Notes

Create a job in a 'Failed' state in a test environment and attempt to fetch its result.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Edge Case: Result file is missing for a successful job

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

A job with ID 'job-000' has a status of 'Succeeded' in the database, but its associated report file has been deleted from storage, AND the API User is authenticated.

### 3.5.5 When

The API User sends a GET request to '/api/v1/jobs/job-000/result'.

### 3.5.6 Then

The system responds with an HTTP 500 Internal Server Error, AND a critical error is logged, AND the response body is a JSON object with an error message, such as '{"error": "Internal server error: Result file for completed job is missing."}'.

### 3.5.7 Validation Notes

Manually delete a report file from the storage location for a completed job and then call the API endpoint.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Security: Request without authentication

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

A job with ID 'job-123' has completed successfully.

### 3.6.5 When

A client sends a GET request to '/api/v1/jobs/job-123/result' without a valid 'Authorization: Bearer <token>' header.

### 3.6.6 Then

The system responds with an HTTP 401 Unauthorized status code.

### 3.6.7 Validation Notes

Use an API client like Postman or curl to make a request with no Authorization header.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Security: User lacks permission to access the report

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

A job with ID 'job-123' has completed successfully, AND the API User is authenticated with a valid JWT but their role does not grant them access to the report associated with the job.

### 3.7.5 When

The API User sends a GET request to '/api/v1/jobs/job-123/result'.

### 3.7.6 Then

The system responds with an HTTP 403 Forbidden status code.

### 3.7.7 Validation Notes

Configure RBAC such that a test user cannot access a specific report, then use that user's token to request the job result.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Not Applicable. This is a RESTful API endpoint with no user interface.

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

A job result can only be retrieved if the job's status is 'Succeeded'.

### 5.1.3 Enforcement Point

API Controller logic for the GET /api/v1/jobs/{jobId}/result endpoint.

### 5.1.4 Violation Handling

Return an appropriate HTTP error code (409 Conflict for in-progress, 410 Gone for failed) with a descriptive JSON body.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Access to a job's result is governed by the Role-Based Access Control (RBAC) permissions configured for the parent report.

### 5.2.3 Enforcement Point

API authorization filter/middleware before the controller logic is executed.

### 5.2.4 Violation Handling

Return an HTTP 403 Forbidden status code.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

If a report result has been purged due to retention policies, requests for it should be treated as if the job does not exist.

### 5.3.3 Enforcement Point

API Controller logic when checking for the physical file.

### 5.3.4 Violation Handling

Return an HTTP 404 Not Found status code.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-094

#### 6.1.1.2 Dependency Reason

This story creates the asynchronous job and provides the initial status URL. The job must exist before its result can be retrieved.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-095

#### 6.1.2.2 Dependency Reason

The status polling endpoint provides the client with the final result URL once the job is complete. This story depends on that URL being available.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-087

#### 6.1.3.2 Dependency Reason

The endpoint must be secured and requires a valid JWT for authentication, which is provided by this story.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-086

#### 6.1.4.2 Dependency Reason

The endpoint must enforce RBAC permissions defined for reports, which are configured via functionality from this story.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core 8 API framework
- Entity Framework Core 8 for database access to JobExecutionLog
- ASP.NET Core Identity for RBAC
- System's configured file storage solution (e.g., local disk, network share)

## 6.3.0.0 Data Dependencies

- Requires a 'JobExecutionLog' record in the database with a status and a path to the generated report artifact.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The endpoint must efficiently stream large report files (e.g., >100MB) without loading the entire file into memory to minimize RAM usage on the server.
- Time to first byte (TTFB) for the response stream should be under 500ms for a completed job on recommended hardware.

## 7.2.0.0 Security

- The endpoint must validate the JWT on every request.
- The endpoint must enforce RBAC to prevent unauthorized data access.
- The implementation must not be vulnerable to path traversal attacks when retrieving the file from storage.

## 7.3.0.0 Usability

- Not Applicable.

## 7.4.0.0 Accessibility

- Not Applicable.

## 7.5.0.0 Compatibility

- The endpoint must be accessible by any standard HTTP client (e.g., curl, Postman, Python requests, Java HttpClient).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires careful state management (checking job status).
- Involves secure and efficient file streaming.
- Requires mapping report output types to correct MIME types for the 'Content-Type' header.
- Robust error handling is needed for multiple failure scenarios (DB vs. file system state).
- Integration with the authentication and authorization system is critical.

## 8.3.0.0 Technical Risks

- Potential for race conditions if a client requests the result at the exact moment the job status is being updated.
- Risk of inconsistent state where the database indicates success but the file is not present on disk.
- Performance degradation if file streaming is not implemented using asynchronous I/O.

## 8.4.0.0 Integration Points

- SQLite Database: Reading the 'JobExecutionLog' table.
- File System: Reading the generated report file from the configured storage path.
- Authentication System: Validating the JWT and user identity.
- Authorization System: Checking user's role and permissions for the report.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- Security

## 9.2.0.0 Test Scenarios

- Verify successful download for all supported report types (PDF, CSV, JSON, etc.) and check for correct Content-Type headers.
- Test all error conditions defined in the acceptance criteria (404, 409, 410, 500).
- Test all security conditions (401 Unauthorized, 403 Forbidden).
- Test with a large file to ensure streaming is performant and does not cause excessive memory usage.

## 9.3.0.0 Test Data Needs

- Test jobs in the database with statuses: 'Succeeded', 'Running', 'Queued', 'Failed'.
- A 'Succeeded' job record where the corresponding physical file has been deleted.
- Generated report files of various types and sizes (including one large file > 100MB).
- User accounts with and without permission to access a specific test report.

## 9.4.0.0 Testing Tools

- xUnit and Moq for backend unit tests.
- An API testing tool like Postman or a scripted client using curl/PowerShell for integration and E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented with >80% coverage for the new logic and passing
- Integration testing completed successfully for all scenarios
- User interface reviewed and approved
- Performance requirements verified
- Security requirements validated
- Documentation updated appropriately
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is the final piece of the asynchronous API workflow and provides high value. It should be prioritized after its prerequisites (US-094, US-095) are complete.
- Requires coordination with any teams or developers who will be consuming this API to ensure the contract (endpoints, responses) meets their needs.

## 11.4.0.0 Release Impact

Completes the on-demand asynchronous report generation feature for API users, enabling a major integration use case.

