# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-087 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Authenticate with the API to receive a JWT |
| As A User Story | As an API User (representing an external system or... |
| User Persona | API User (e.g., System Integrator, automated scrip... |
| Business Value | Enables secure, programmatic access to the system'... |
| Functional Area | API Security & Authentication |
| Story Theme | Secure External API |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful authentication with valid credentials

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

An active user with the role 'Administrator' exists in the system

### 3.1.5 When

The API User sends a POST request to the '/api/v1/auth/token' endpoint with a valid username and password in the JSON body

### 3.1.6 Then

The system responds with an HTTP 200 OK status code.

### 3.1.7 And

The decoded JWT payload contains standard claims 'sub' (user ID), 'iat' (issued at), 'exp' (expiration), and a custom claim 'role' with the user's assigned role.

### 3.1.8 Validation Notes

Verify the HTTP status code and the structure of the JSON response. Decode the JWT (using a tool like jwt.io and the public key if using asymmetric crypto) to inspect its payload and verify the claims and expiration time.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Authentication attempt with an incorrect password

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

An active user exists in the system

### 3.2.5 When

The API User sends a POST request to the '/api/v1/auth/token' endpoint with the correct username but an incorrect password

### 3.2.6 Then

The system responds with an HTTP 401 Unauthorized status code.

### 3.2.7 And

The response body contains a generic error message, such as '{"error": "Invalid credentials"}', to prevent account enumeration.

### 3.2.8 Validation Notes

Check the status code and ensure the error message does not differentiate between a bad username and a bad password.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Authentication attempt with a non-existent username

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

No user with the specified username exists in the system

### 3.3.5 When

The API User sends a POST request to the '/api/v1/auth/token' endpoint with a non-existent username

### 3.3.6 Then

The system responds with an HTTP 401 Unauthorized status code.

### 3.3.7 And

The response body contains a generic error message, such as '{"error": "Invalid credentials"}'.

### 3.3.8 Validation Notes

Confirm the response is identical to the incorrect password scenario to prevent user enumeration.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Authentication attempt with missing credentials in the request body

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

The authentication endpoint is available

### 3.4.5 When

The API User sends a POST request to the '/api/v1/auth/token' endpoint with a JSON body missing the 'username' or 'password' field

### 3.4.6 Then

The system responds with an HTTP 400 Bad Request status code.

### 3.4.7 And

The response body contains a descriptive error message indicating which field is missing.

### 3.4.8 Validation Notes

Test with a missing 'username', a missing 'password', and an empty request body. Verify the 400 status and the specific error message in each case.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Authentication attempt for a locked user account

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

A user account exists but is in a 'locked' state (as per US-031)

### 3.5.5 When

The API User sends a POST request to the '/api/v1/auth/token' endpoint with the correct credentials for the locked account

### 3.5.6 Then

The system responds with an HTTP 403 Forbidden status code.

### 3.5.7 And

The response body contains a specific error message, such as '{"error": "Account is locked"}'.

### 3.5.8 Validation Notes

Requires a test setup where an account can be programmatically locked. Verify the 403 status and the distinct error message.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Using an incorrect HTTP method for the authentication endpoint

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

The authentication endpoint '/api/v1/auth/token' exists

### 3.6.5 When

The API User sends a GET, PUT, DELETE, or PATCH request to the endpoint

### 3.6.6 Then

The system responds with an HTTP 405 Method Not Allowed status code.

### 3.6.7 Validation Notes

Test each disallowed HTTP method and confirm the 405 response.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Not applicable. This is an API-only story.

## 4.2.0 User Interactions

- The user (a machine client) interacts by sending an HTTPS POST request to a defined endpoint.

## 4.3.0 Display Requirements

- The API contract must be clearly defined and documented in the OpenAPI specification.
- Request Body Contract: JSON object with 'username' (string) and 'password' (string).
- Success Response Body Contract: JSON object with 'accessToken' (string), 'tokenType' (string, fixed to 'Bearer'), 'expiresIn' (integer, seconds), 'refreshToken' (string).
- Error Response Body Contract: JSON object with 'error' (string).

## 4.4.0 Accessibility Needs

- Not applicable. This is an API-only story.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

JWT expiration must be configurable, with a default of 60 minutes (3600 seconds).

### 5.1.3 Enforcement Point

During JWT generation upon successful authentication.

### 5.1.4 Violation Handling

Not applicable. This is a system configuration rule.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Failed login attempts must be tracked per user to support account lockout policies.

### 5.2.3 Enforcement Point

During the authentication process when credentials fail validation.

### 5.2.4 Violation Handling

The user's failed attempt counter is incremented. If the threshold is reached, the account status is changed to 'locked'.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-018

#### 6.1.1.2 Dependency Reason

A mechanism to create users is required before they can authenticate.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-031

#### 6.1.2.2 Dependency Reason

The authentication logic must check if an account is locked, which is the functionality defined in this story.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-004

#### 6.1.3.2 Dependency Reason

The authentication endpoint must be served over HTTPS for security, which is configured during installation.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity for user store and password hashing.
- A JWT library (e.g., System.IdentityModel.Tokens.Jwt) for token generation.
- .NET Configuration Provider for secure management of the JWT signing key.
- Swashbuckle.AspNetCore for generating OpenAPI documentation for the endpoint.

## 6.3.0.0 Data Dependencies

- Access to the User and Role tables in the SQLite configuration database.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The P95 latency for the authentication endpoint must be under 200ms under a load of 50 concurrent requests.

## 7.2.0.0 Security

- The endpoint must only be accessible via HTTPS (TLS 1.2+).
- Passwords must never be stored in plaintext; they must be hashed using a strong, salted algorithm (provided by ASP.NET Core Identity).
- The JWT signing key must be stored securely using the .NET Secret Manager for development and Windows Certificate Store for production, not in configuration files.
- The system must implement rate limiting on the authentication endpoint to mitigate brute-force attacks (e.g., max 10 requests per minute from a single IP).
- Error messages for failed authentication must be generic to prevent user enumeration attacks.

## 7.3.0.0 Usability

- The API endpoint must be well-documented in the OpenAPI specification, including request/response schemas and potential error codes.

## 7.4.0.0 Accessibility

- Not applicable.

## 7.5.0.0 Compatibility

- The endpoint must be consumable by any standard HTTP client.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Correctly configuring ASP.NET Core Identity and its integration with the API.
- Implementing secure storage and retrieval of the JWT signing key.
- Defining and creating the JWT with the correct claims and expiration.
- Implementing robust error handling for all authentication failure scenarios.
- Adding rate-limiting and other security middleware.

## 8.3.0.0 Technical Risks

- Insecure storage or exposure of the JWT signing key could compromise the entire API.
- Incorrectly configured claims could lead to authorization issues in subsequent API calls.
- Weak rate-limiting or lockout policies could leave the system vulnerable to brute-force attacks.

## 8.4.0.0 Integration Points

- This endpoint is the primary integration point for any external system that needs to interact with the application's API.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- Security

## 9.2.0.0 Test Scenarios

- Successful login for an Administrator user.
- Successful login for a Viewer user.
- Login attempt with a valid username and invalid password.
- Login attempt with an invalid username.
- Login attempt with a locked account.
- Login attempt with an empty or malformed request body.
- Verify JWT payload contents (claims, exp) after a successful login.
- Security testing for user enumeration, credential stuffing, and rate limit bypass.

## 9.3.0.0 Test Data Needs

- A set of test users with known credentials and roles (e.g., admin_user, viewer_user).
- A test user account that is in a 'locked' state.

## 9.4.0.0 Testing Tools

- xUnit for unit tests.
- Moq for mocking dependencies.
- ASP.NET Core TestServer for in-memory integration testing.
- A REST client like Postman or curl for manual verification.
- A security scanning tool to check for common vulnerabilities.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing.
- Code reviewed by at least one other developer and approved.
- Unit tests implemented for the token generation logic, achieving >80% coverage.
- Integration tests implemented for the API endpoint, covering all acceptance criteria.
- Security review completed, and all identified vulnerabilities are addressed.
- Rate limiting and account lockout policies are verified.
- The endpoint is fully documented in the auto-generated OpenAPI (Swagger) specification.
- Story deployed and verified in the staging environment.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational story for the entire API. It is a blocker for almost all other API-related user stories (US-088, US-089, US-090, etc.).
- It should be prioritized in an early sprint of the API development effort.

## 11.4.0.0 Release Impact

- The Secure External API feature cannot be released without this functionality.

