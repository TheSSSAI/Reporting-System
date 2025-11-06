# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-018 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Create new user accounts |
| As A User Story | As an Administrator, I want to create a new user a... |
| User Persona | Administrator: A user with full CRUD access to all... |
| Business Value | Enables controlled onboarding of new personnel, su... |
| Functional Area | User Management |
| Story Theme | System Administration & Security |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful creation of a new user with a 'Viewer' role

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am logged in as an Administrator and am on the 'Create User' page within the Control Panel

### 3.1.5 When

I enter a unique username, a unique and valid email address, select the 'Viewer' role from the available options, and click the 'Create User' button

### 3.1.6 Then

The system creates a new user account with the provided details and assigned role, a success notification is displayed stating 'User created successfully', and I am redirected to the user list where the new user is now visible.

### 3.1.7 Validation Notes

Verify the user record exists in the database with the correct role assignment. Verify the UI redirects and shows the success message.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Attempt to create a user with a username that already exists

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

A user with the username 'existinguser' already exists in the system

### 3.2.5 When

I attempt to create a new user with the username 'existinguser'

### 3.2.6 Then

The system prevents the creation of the user, the form is not submitted, and a validation error message 'Username is already taken' is displayed next to the username field.

### 3.2.7 Validation Notes

Check that no new user record is created in the database. Verify the specific error message is displayed on the UI.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempt to create a user with an email address that already exists

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

A user with the email 'existing@example.com' already exists in the system

### 3.3.5 When

I attempt to create a new user with the email 'existing@example.com'

### 3.3.6 Then

The system prevents the creation of the user, and a validation error message 'Email is already in use' is displayed next to the email field.

### 3.3.7 Validation Notes

Check that no new user record is created in the database. Verify the specific error message is displayed on the UI.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Attempt to create a user with missing required fields

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am on the 'Create User' page

### 3.4.5 When

I leave the 'Username' and 'Role' fields empty and click 'Create User'

### 3.4.6 Then

The system prevents the form submission, and validation error messages 'Username is required' and 'Role is required' are displayed next to their respective fields.

### 3.4.7 Validation Notes

Verify this for all required fields (username, email, role). Ensure the form submission is blocked.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Attempt to create a user with an invalid email format

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am on the 'Create User' page

### 3.5.5 When

I enter 'not-an-email' in the email address field and click 'Create User'

### 3.5.6 Then

The system prevents the form submission, and a validation error message 'Please enter a valid email address' is displayed next to the email field.

### 3.5.7 Validation Notes

Test with various invalid email formats to ensure robust client-side and server-side validation.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Cancel the user creation process

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

I am on the 'Create User' page and have filled in some data

### 3.6.5 When

I click the 'Cancel' button

### 3.6.6 Then

No user is created, the form data is discarded, and I am redirected back to the main user list page.

### 3.6.7 Validation Notes

Verify no new user is persisted in the database and the UI navigates correctly.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A 'Create New User' button on the main user management page.
- A dedicated 'Create User' page/modal with a clear title.
- Text input field for 'Username'.
- Text input field for 'Email'.
- A dropdown or radio button group for 'Role' selection, populated with existing roles.
- A primary action button, e.g., 'Create User'.
- A secondary action button, e.g., 'Cancel'.

## 4.2.0 User Interactions

- Clicking 'Create New User' navigates to the creation form.
- The system provides real-time or on-submit validation feedback for form fields.
- Upon successful creation, a non-intrusive success notification (e.g., toast message) is displayed.
- Clicking 'Cancel' discards changes and returns the user to the previous screen.

## 4.3.0 Display Requirements

- All input fields must have clear, visible labels.
- Validation error messages must be displayed in close proximity to the invalid field.
- The list of roles in the dropdown must be dynamically fetched from the system.

## 4.4.0 Accessibility Needs

- The form must be fully keyboard navigable.
- All form inputs must be associated with a `<label>` tag.
- Error messages must be programmatically associated with their respective inputs using `aria-describedby`.
- The UI must comply with WCAG 2.1 Level AA standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Usernames must be unique across the entire system.

### 5.1.3 Enforcement Point

Server-side, during the API request to create a user.

### 5.1.4 Violation Handling

The API returns an HTTP 409 Conflict error with a message indicating the username is taken.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Email addresses must be unique across the entire system.

### 5.2.3 Enforcement Point

Server-side, during the API request to create a user.

### 5.2.4 Violation Handling

The API returns an HTTP 409 Conflict error with a message indicating the email is in use.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

Every new user must be assigned exactly one role upon creation.

### 5.3.3 Enforcement Point

Server-side, during the API request to create a user.

### 5.3.4 Violation Handling

The API returns an HTTP 400 Bad Request error if the role is missing or invalid.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-019

#### 6.1.1.2 Dependency Reason

The system must support the concept of roles and have a way to define them before a user can be assigned to one.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-024

#### 6.1.2.2 Dependency Reason

The mechanism for generating a temporary, one-time-use password for the new user is required upon successful account creation.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-027

#### 6.1.3.2 Dependency Reason

The core authentication and user identity framework (ASP.NET Core Identity) must be established to store and manage user credentials.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity for user management.
- Entity Framework Core for data access to the Users and Roles tables.
- A RESTful API endpoint (e.g., POST /api/v1/users) for handling the creation logic.
- React frontend component for the user creation form.

## 6.3.0.0 Data Dependencies

- Access to the system's SQLite database to read existing users/emails for validation and write the new user record.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API response time for a user creation request should be under 500ms.
- The UI for the creation form should load in under 2 seconds.

## 7.2.0.0 Security

- All data must be transmitted over HTTPS.
- The API endpoint must be protected and only accessible to users with the 'Administrator' role.
- All user input must be sanitized on the server-side to prevent XSS and other injection attacks.
- The system must not reveal whether a username or email exists during a password reset attempt, but it is acceptable to do so in this admin-only creation form for usability.

## 7.3.0.0 Usability

- The user creation process should be intuitive, requiring minimal guidance for an administrator.
- Error messages should be clear, concise, and actionable.

## 7.4.0.0 Accessibility

- The user creation interface must conform to WCAG 2.1 Level AA.

## 7.5.0.0 Compatibility

- The user interface must function correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires both frontend (React form with validation) and backend (API endpoint with business logic) development.
- Integration with ASP.NET Core Identity's `UserManager` service.
- Ensuring robust and consistent validation on both client and server.
- Coordinating the user creation with the temporary password generation from US-024.

## 8.3.0.0 Technical Risks

- Potential for race conditions if two administrators attempt to create a user with the same username simultaneously. This should be mitigated by a unique constraint on the username column in the database.
- Inconsistent validation logic between the frontend and backend could lead to a poor user experience.

## 8.4.0.0 Integration Points

- Backend: ASP.NET Core Identity user store.
- Frontend: The main user list component, which will need to be refreshed or updated after a new user is created.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify successful creation of both 'Administrator' and 'Viewer' role users.
- Test all validation rules: duplicate username, duplicate email, invalid formats, and missing fields.
- Test the 'Cancel' functionality to ensure no data is persisted.
- Verify that a non-administrator user cannot access the user creation API endpoint or UI.
- Test the form's behavior on different screen resolutions (responsive design).

## 9.3.0.0 Test Data Needs

- A pre-existing user in the test database to test duplicate username/email scenarios.
- A list of defined roles ('Administrator', 'Viewer') available in the test database.

## 9.4.0.0 Testing Tools

- xUnit and Moq for backend unit tests.
- Jest and React Testing Library for frontend unit tests.
- A framework like Playwright or Cypress for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit tests implemented for both backend and frontend, achieving >80% code coverage
- Integration tests for the API endpoint are implemented and passing
- E2E test scenario for the happy path is created and passing
- User interface is responsive and meets WCAG 2.1 AA standards
- API endpoint is secured and documented in the OpenAPI specification
- Story deployed and verified in the staging environment by a QA engineer or product owner

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational story for user management and should be prioritized early in the project.
- Dependent stories (US-019, US-024) must be completed or worked on in parallel within the same sprint.

## 11.4.0.0 Release Impact

This feature is critical for the initial release (MVP) as it is the primary mechanism for onboarding users into the system.

