# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-097 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | View interactive API documentation |
| As A User Story | As an API User (such as a System Integrator or Adm... |
| User Persona | System Integrator, Administrator (API User) |
| Business Value | Accelerates third-party integration and automation... |
| Functional Area | API and Integrations |
| Story Theme | Secure External API |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Accessing the API documentation page while authenticated

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an authenticated user with the 'Administrator' role and I am logged into the system

### 3.1.5 When

I navigate to the designated API documentation URL (e.g., '/swagger')

### 3.1.6 Then

The interactive API documentation UI (Swagger UI) loads successfully and displays a list of all available API endpoint groups.

### 3.1.7 Validation Notes

Verify that the page loads without errors and the main UI components are visible.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Viewing endpoint details and schemas

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The API documentation page is loaded

### 3.2.5 When

I expand a specific endpoint (e.g., 'POST /api/v1/reports')

### 3.2.6 Then

I can see its details, including the HTTP method, a description, all required parameters, and the expected request and response body schemas in a clear, structured format.

### 3.2.7 Validation Notes

Check that the schemas accurately reflect the data models used in the code, including data types and required fields.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Authorizing API calls within the interactive UI

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I have a valid JWT obtained from the login endpoint

### 3.3.5 When

I click the 'Authorize' button, enter my JWT in the format 'Bearer <token>', and save it

### 3.3.6 Then

The UI indicates that my subsequent requests will be sent with the Authorization header, and a lock icon appears closed.

### 3.3.7 Validation Notes

Use browser developer tools to confirm that subsequent API calls from the UI include the 'Authorization: Bearer <token>' header.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Successfully executing a protected API call

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

I am on the API documentation page and have successfully authorized using my JWT

### 3.4.5 When

I fill in the required parameters for a protected endpoint, click 'Execute', and the request is sent

### 3.4.6 Then

The UI displays the successful response from the server, including the 2xx status code, the response body, and response headers.

### 3.4.7 Validation Notes

Test with a simple GET endpoint and a more complex POST endpoint to ensure both work correctly.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Attempting to access the API documentation page while unauthenticated

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am not logged into the system

### 3.5.5 When

I attempt to navigate directly to the API documentation URL

### 3.5.6 Then

I am redirected to the system's login page.

### 3.5.7 Validation Notes

Verify the HTTP redirect (302) to the login URL.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Attempting to execute a protected API call without authorization

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

I am on the API documentation page but have not provided a JWT via the 'Authorize' button

### 3.6.5 When

I attempt to execute a protected endpoint

### 3.6.6 Then

The UI displays a 401 Unauthorized response from the server.

### 3.6.7 Validation Notes

Verify the status code in the response section of the UI is 401.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Executing an API call with invalid parameters

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

I am on the API documentation page and am authorized

### 3.7.5 When

I execute an endpoint with missing required fields or incorrectly formatted data

### 3.7.6 Then

The UI displays a 400 Bad Request response from the server, and the response body contains a descriptive validation error message.

### 3.7.7 Validation Notes

Test a POST or PUT endpoint by omitting a required field from the JSON body.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A main page served at a dedicated URL (e.g., /swagger).
- An 'Authorize' button to open a dialog for JWT input.
- Collapsible sections for each API controller/group.
- Expandable entries for each endpoint showing details.
- 'Try it out' button to enable parameter input fields.
- 'Execute' button to send the API request.
- Display areas for response code, body, and headers.

## 4.2.0 User Interactions

- User navigates to a URL to access the documentation.
- User clicks to expand/collapse endpoint details.
- User enters a Bearer token to authorize the session.
- User fills forms to provide parameters and request bodies.
- User clicks a button to execute the request and view the live response.

## 4.3.0 Display Requirements

- The system's API version and title must be displayed.
- All public API endpoints must be listed.
- Endpoint descriptions, parameters, and schemas must be sourced from code comments/attributes.
- Example values for schemas should be displayed where available.

## 4.4.0 Accessibility Needs

- The generated UI must conform to WCAG 2.1 Level AA standards, consistent with the rest of the application.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'Access to the API documentation UI must be restricted to authenticated users.', 'enforcement_point': 'ASP.NET Core middleware pipeline, before the Swagger UI middleware is invoked.', 'violation_handling': 'Unauthenticated requests to the documentation URL are redirected to the login page.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-087

#### 6.1.1.2 Dependency Reason

The JWT authentication mechanism must be implemented to allow authorization within the interactive UI.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-089

#### 6.1.2.2 Dependency Reason

The CRUD API endpoints for system configuration must exist to be documented.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-090

#### 6.1.3.2 Dependency Reason

The on-demand report generation API endpoint must exist to be documented.

## 6.2.0.0 Technical Dependencies

- Swashbuckle.AspNetCore library for OpenAPI specification generation.
- ASP.NET Core Identity for authenticating access to the documentation page.
- Proper use of XML comments and data annotations on API controllers and models.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API documentation page should achieve a Largest Contentful Paint (LCP) of under 2.5 seconds.

## 7.2.0.0 Security

- The API documentation endpoint must not be publicly accessible and must require user authentication.
- The UI should not permanently store the JWT; it should be held in-session only.
- The system must be protected against Cross-Site Scripting (XSS) within the documentation UI.

## 7.3.0.0 Usability

- The documentation should be clear and easy to navigate for a developer audience.

## 7.4.0.0 Accessibility

- The UI must meet WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The documentation UI must render correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Configuring Swashbuckle to correctly integrate with ASP.NET Core Identity to secure the UI endpoint.
- Implementing the JWT Bearer token authentication flow within the Swagger UI.
- Ensuring consistent and comprehensive XML documentation across all public API controllers and DTOs is a code-wide discipline.

## 8.3.0.0 Technical Risks

- Incomplete or inaccurate documentation if developers fail to properly annotate their code.
- Misconfiguration could accidentally expose the documentation page publicly.

## 8.4.0.0 Integration Points

- Integrates with the ASP.NET Core authentication and authorization middleware.
- Dynamically inspects all registered API controllers to build the documentation.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify unauthenticated access is blocked.
- Verify authenticated access is allowed.
- Test the full interactive flow: authorize, fill parameters, execute, and verify response for a GET, POST, PUT, and DELETE endpoint.
- Test error handling for unauthorized (401) and bad request (400) scenarios from within the UI.
- Verify that endpoint descriptions and schemas displayed in the UI match the source code.

## 9.3.0.0 Test Data Needs

- A valid test user account with 'Administrator' role.
- Valid and invalid JSON payloads for testing POST/PUT endpoints.

## 9.4.0.0 Testing Tools

- Standard web browser with developer tools.
- Automated E2E testing framework (e.g., Playwright, Cypress) could be used for regression.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing for any new logic
- Integration testing completed successfully to ensure middleware is configured correctly
- User interface reviewed and approved
- Security requirements validated, especially restricted access to the documentation page
- Documentation updated to inform administrators of the documentation URL
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🟡 Medium

## 11.3.0.0 Sprint Considerations

- This story should be scheduled after the core API endpoints and authentication are in place.
- Requires coordination with backend developers to ensure they are correctly annotating their API code.

## 11.4.0.0 Release Impact

Provides critical developer-facing documentation necessary for API adoption and integration. Essential for any release targeting System Integrators.

