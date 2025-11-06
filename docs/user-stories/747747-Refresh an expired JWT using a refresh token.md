# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-088 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Refresh an expired JWT using a refresh token |
| As A User Story | As an API Client, I want to exchange a valid refre... |
| User Persona | API Client (a developer or automated system integr... |
| Business Value | Enhances API security by allowing short-lived acce... |
| Functional Area | Security and Authentication |
| Story Theme | Secure External API |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful token refresh with a valid refresh token

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

An API Client has authenticated and possesses a valid, unexpired refresh token and an expired access token

### 3.1.5 When

The client sends a POST request to the `/api/v1/auth/refresh` endpoint with the valid refresh token in the request body

### 3.1.6 Then

The system responds with an HTTP 200 OK status

### 3.1.7 And

The previously used refresh token is invalidated in the system.

### 3.1.8 Validation Notes

Verify the new access token can be used to access a protected endpoint. Verify the old refresh token can no longer be used.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Attempt to refresh with an expired refresh token

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

An API Client possesses a refresh token that has passed its own expiration date

### 3.2.5 When

The client sends a POST request to the `/api/v1/auth/refresh` endpoint with the expired refresh token

### 3.2.6 Then

The system responds with an HTTP 401 Unauthorized status

### 3.2.7 And

The response body contains a JSON object with an error code and a message like `{"error": "Refresh token expired. Please log in again."}`.

### 3.2.8 Validation Notes

Check the HTTP status code and the error message in the response body.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempt to refresh with an invalid or malformed refresh token

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

An API Client provides a token that is not a valid refresh token (e.g., malformed, non-existent)

### 3.3.5 When

The client sends a POST request to the `/api/v1/auth/refresh` endpoint with the invalid token

### 3.3.6 Then

The system responds with an HTTP 401 Unauthorized status

### 3.3.7 And

The response body contains a JSON object with an error code and a message like `{"error": "Invalid refresh token."}`.

### 3.3.8 Validation Notes

Test with various invalid inputs: a JWT access token, a random string, a correctly formatted but non-existent token.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Attempt to reuse a refresh token after it has been used for a refresh (token rotation)

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

An API Client has successfully used a refresh token `R1` to obtain a new access token `A2` and a new refresh token `R2`

### 3.4.5 When

The client attempts to make a second request to the `/api/v1/auth/refresh` endpoint using the original, now-invalidated refresh token `R1`

### 3.4.6 Then

The system responds with an HTTP 401 Unauthorized status

### 3.4.7 And

The system revokes all active refresh tokens for that user to prevent token theft attacks, forcing a full re-authentication.

### 3.4.8 Validation Notes

Verify the 401 response. Then, attempt to use the newer refresh token `R2` and verify it also fails, confirming the token family has been revoked.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Login endpoint provides both access and refresh tokens

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

An API Client is performing a successful login

### 3.5.5 When

The client sends a POST request to the login endpoint with valid credentials

### 3.5.6 Then

The system's successful login response includes both a JWT access token and a refresh token.

### 3.5.7 Validation Notes

This is an update to the behavior of US-087. The login response must be checked to ensure both tokens are present.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Not Applicable

## 4.2.0 User Interactions

- Not Applicable

## 4.3.0 Display Requirements

- Not Applicable

## 4.4.0 Accessibility Needs

- Not Applicable

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Refresh tokens must have a longer lifespan than access tokens. Default: Access Token = 60 minutes, Refresh Token = 7 days. Both must be configurable.

### 5.1.3 Enforcement Point

System configuration and token generation service.

### 5.1.4 Violation Handling

System will fail to start if configuration is invalid. Tokens are generated with configured lifespans.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Refresh tokens must be single-use. Upon successful use, a refresh token is immediately invalidated and replaced by a new one (token rotation).

### 5.2.3 Enforcement Point

Token refresh endpoint logic.

### 5.2.4 Violation Handling

Attempting to reuse an invalidated token results in an HTTP 401 error and revocation of the entire token family for that user.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

Refresh tokens must be stored securely in the database (e.g., hashed) and associated with a specific user and an expiration date.

### 5.3.3 Enforcement Point

Data Access Layer and Authentication Service.

### 5.3.4 Violation Handling

Code reviews and security scans must verify that plaintext tokens are not stored.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

- {'story_id': 'US-087', 'dependency_reason': 'The login mechanism from US-087 must be implemented first. This story modifies that mechanism to issue a refresh token alongside the JWT, which is the foundation for the refresh capability.'}

## 6.2.0 Technical Dependencies

- ASP.NET Core Identity for user management.
- Entity Framework Core for database interaction to store and manage refresh tokens.
- A JWT generation and validation library.
- A secure hashing algorithm (e.g., SHA256) for storing refresh tokens in the database.

## 6.3.0 Data Dependencies

- A new or modified database table to store user-associated refresh tokens, their hashes, expiration dates, and revocation status.

## 6.4.0 External Dependencies

*No items available*

# 7.0.0 Non Functional Requirements

## 7.1.0 Performance

- The P99 latency for the token refresh endpoint must be under 150ms under normal load.

## 7.2.0 Security

- Refresh tokens must be cryptographically random and sufficiently long to be unguessable.
- Refresh tokens must be stored hashed in the database, never in plaintext.
- The system must implement refresh token rotation to mitigate the risk of token leakage.
- The system must implement token family revocation upon detection of refresh token reuse to protect against token theft.
- All communication must be over HTTPS.

## 7.3.0 Usability

- Not Applicable (API-only feature).

## 7.4.0 Accessibility

- Not Applicable (API-only feature).

## 7.5.0 Compatibility

- The endpoint must adhere to standard RESTful principles and be consumable by any standard HTTP client.

# 8.0.0 Implementation Considerations

## 8.1.0 Complexity Assessment

Medium

## 8.2.0 Complexity Factors

- Requires database schema changes to store refresh tokens.
- Involves security-critical logic for token rotation and revocation that must be implemented correctly.
- Requires modification of the existing login endpoint (from US-087).
- Requires creation of a new, dedicated API endpoint for token refreshing.

## 8.3.0 Technical Risks

- Risk of security vulnerabilities if token rotation, storage, or revocation logic is implemented incorrectly.
- Potential for race conditions if a client makes multiple refresh requests in parallel with the same token.

## 8.4.0 Integration Points

- Login Endpoint (`/api/v1/auth/login`): Must be modified to issue refresh tokens.
- New Refresh Endpoint (`/api/v1/auth/refresh`): The core of this story.
- Database: For storing and managing refresh token state.

# 9.0.0 Testing Requirements

## 9.1.0 Testing Types

- Unit
- Integration
- Security

## 9.2.0 Test Scenarios

- Verify successful token refresh and subsequent access to a protected resource.
- Verify failure when using an expired refresh token.
- Verify failure when using an invalid/malformed refresh token.
- Verify failure and token family revocation when reusing a refresh token.
- Verify the login endpoint returns both token types.

## 9.3.0 Test Data Needs

- User accounts with known credentials.
- Expired and non-expired refresh tokens.
- Invalid token strings.

## 9.4.0 Testing Tools

- xUnit/Moq for backend unit tests.
- An API testing tool like Postman or a dedicated integration test suite using `HttpClient` to simulate the client flow.

# 10.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team, with special attention to security aspects
- Unit tests implemented for the token service and refresh logic, achieving >80% coverage
- Integration tests for the full login-refresh-access flow are implemented and passing
- The new `/api/v1/auth/refresh` endpoint is documented in the OpenAPI/Swagger specification
- The documentation for the login endpoint is updated to reflect the new response format
- Security requirements for token storage and handling are validated
- Story deployed and verified in the staging environment

# 11.0.0 Planning Information

## 11.1.0 Story Points

5

## 11.2.0 Priority

🔴 High

## 11.3.0 Sprint Considerations

- This story is a prerequisite for any robust, long-term API integration. It should be prioritized early in the API development cycle.
- Must be scheduled in a sprint after US-087 is completed.

## 11.4.0 Release Impact

- This is a critical feature for the v1 release of the public API, enabling secure and stable third-party integrations.

