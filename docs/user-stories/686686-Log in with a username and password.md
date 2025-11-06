# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-027 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Log in with a username and password |
| As A User Story | As a Registered User (Administrator or Viewer), I ... |
| User Persona | Registered User (Administrator, End-User/Viewer) |
| Business Value | Provides the primary security gate for the applica... |
| Functional Area | User Management and Authentication |
| Story Theme | System Access and Security |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful login for an Administrator

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

an Administrator user exists with a known username and password, and the user is on the login page

### 3.1.5 When

the user enters their correct username and password and submits the form

### 3.1.6 Then

the system validates the credentials, issues a JWT, and redirects the user to the Control Panel dashboard

### 3.1.7 Validation Notes

Verify redirection to the Control Panel URL. The client application should store the received JWT for subsequent API calls. The UI should reflect a logged-in state.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Successful login for a Viewer

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

an End-User (Viewer) exists with a known username and password, and the user is on the login page

### 3.2.5 When

the user enters their correct username and password and submits the form

### 3.2.6 Then

the system validates the credentials, issues a JWT, and redirects the user to the Report Viewer page

### 3.2.7 Validation Notes

Verify redirection to the Report Viewer URL. The client application should store the received JWT.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Login attempt with an incorrect password

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

a user exists with a known username, and the user is on the login page

### 3.3.5 When

the user enters their correct username but an incorrect password and submits the form

### 3.3.6 Then

the system rejects the authentication and displays a generic error message, 'Invalid username or password.'

### 3.3.7 Validation Notes

The user must remain on the login page. The password field should be cleared for security. The error message must not indicate whether the username or password was the incorrect part.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Login attempt with a non-existent username

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

the user is on the login page

### 3.4.5 When

the user enters a username that does not exist in the system and submits the form

### 3.4.6 Then

the system rejects the authentication and displays a generic error message, 'Invalid username or password.'

### 3.4.7 Validation Notes

To prevent username enumeration, the response time and error message must be indistinguishable from an incorrect password attempt (AC-003).

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Login attempt with empty credentials

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

the user is on the login page

### 3.5.5 When

the user attempts to submit the form without entering a username and/or password

### 3.5.6 Then

the UI displays client-side validation errors indicating that the required fields are empty, and no API call is made to the server

### 3.5.7 Validation Notes

Check for messages like 'Username is required' and 'Password is required' next to the respective fields.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Login attempt for a locked account

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

a user's account has been locked due to multiple failed login attempts (as per US-031)

### 3.6.5 When

the user enters their correct username and password and submits the form

### 3.6.6 Then

the system rejects the authentication and displays a specific error message, 'Your account is locked. Please contact an administrator.'

### 3.6.7 Validation Notes

This scenario depends on the implementation of US-031. The error message should be distinct from the generic invalid credentials message.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Username is case-insensitive

### 3.7.3 Scenario Type

Alternative_Flow

### 3.7.4 Given

a user exists with the username 'AdminUser', and the user is on the login page

### 3.7.5 When

the user enters 'adminuser' as the username and their correct password and submits the form

### 3.7.6 Then

the system successfully authenticates the user and redirects them to the appropriate page

### 3.7.7 Validation Notes

Test with various casings (e.g., 'ADMINUSER', 'adminUser') to confirm case-insensitivity.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Input field for 'Username' with a corresponding label.
- Input field for 'Password' with a corresponding label.
- A primary action button labeled 'Log In'.
- An area to display login error messages.

## 4.2.0 User Interactions

- The password field must mask character input.
- The 'Log In' button should be disabled until both username and password fields contain text.
- Pressing 'Enter' in the password field should trigger the form submission.

## 4.3.0 Display Requirements

- The login page should be clean, centered, and follow the application's overall design language (MUI v5).
- Error messages should be displayed clearly and close to the form.

## 4.4.0 Accessibility Needs

- All form fields must have associated labels for screen readers.
- The page must be navigable using only a keyboard.
- Color contrast for text and UI elements must meet WCAG 2.1 AA standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

User authentication is the prerequisite for accessing any protected system resource.

### 5.1.3 Enforcement Point

API middleware and frontend routing.

### 5.1.4 Violation Handling

User is redirected to the login page if they attempt to access a protected resource without a valid session.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Usernames are treated as case-insensitive for login purposes but are stored with their original casing.

### 5.2.3 Enforcement Point

During the authentication process in the backend.

### 5.2.4 Violation Handling

N/A - This is a processing rule.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-003

#### 6.1.1.2 Dependency Reason

A primary administrator account must exist in the database to be able to log in.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-018

#### 6.1.2.2 Dependency Reason

The ability to create additional users is required to test login with different roles.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-019

#### 6.1.3.2 Dependency Reason

Role assignment is necessary to test the role-based redirection after login (Admin to Control Panel, Viewer to Report Viewer).

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity framework for user management and password hashing.
- Entity Framework Core for accessing the user store in the SQLite database.
- JWT generation and validation library.
- React frontend framework for the UI.
- Client-side state management (Zustand) for handling authentication state.

## 6.3.0.0 Data Dependencies

- Requires access to the 'Users' and 'Roles' tables in the application's SQLite database.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- P95 latency for the login API endpoint must be under 500ms.
- The login page UI must achieve a Largest Contentful Paint (LCP) of under 2.5 seconds.

## 7.2.0.0 Security

- All login traffic must be encrypted using HTTPS/TLS 1.2+.
- Passwords must be hashed using a strong, salted, one-way algorithm (provided by ASP.NET Core Identity).
- The system must return generic error messages to prevent username enumeration.
- The issued JWT must be digitally signed and have a configurable, short-lived expiration (e.g., 60 minutes).

## 7.3.0.0 Usability

- The login process should be simple and intuitive, requiring minimal user effort.

## 7.4.0.0 Accessibility

- The login page must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The login page must function correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Initial setup and configuration of ASP.NET Core Identity with EF Core and a custom user model.
- Implementation of JWT generation on the backend.
- Secure handling and storage of the JWT on the client-side.
- Creating protected routes in the React application that enforce authentication.
- Coordinating state between the client and server for login status.

## 8.3.0.0 Technical Risks

- Improper JWT handling on the client could expose security vulnerabilities (e.g., XSS attacks stealing the token).
- Misconfiguration of ASP.NET Core Identity could weaken security.

## 8.4.0.0 Integration Points

- Backend: ASP.NET Core Identity service.
- Backend: Database context for user data.
- Frontend: API client for making the authentication request.
- Frontend: Routing library to handle redirection based on authentication status and user role.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Successful login for each user role.
- Failed login due to incorrect password.
- Failed login due to non-existent username.
- Failed login due to locked account.
- Client-side validation for empty fields.
- Attempting to access a protected page without being logged in.

## 9.3.0.0 Test Data Needs

- Test user accounts for each role (Administrator, Viewer).
- A user account that can be programmatically locked for testing.
- A set of credentials that are known to be invalid.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- An E2E testing framework like Playwright or Cypress.
- Security scanning tools to check for common vulnerabilities.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for backend and frontend logic, achieving >80% coverage
- Integration testing for the authentication endpoint completed successfully
- E2E tests for the full login flow are passing
- User interface reviewed and approved for responsiveness and adherence to design
- Performance requirements for API latency and page load are verified
- Security requirements validated, including HTTPS enforcement and secure password handling
- Documentation for the authentication API endpoint is created/updated in the OpenAPI spec
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational story that unblocks a majority of other user-facing features.
- It should be prioritized for an early sprint in the development cycle.
- Requires both backend and frontend development effort.

## 11.4.0.0 Release Impact

- Critical for the first release. The application is not usable without this functionality.

