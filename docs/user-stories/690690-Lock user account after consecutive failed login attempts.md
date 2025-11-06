# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-031 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Lock user account after consecutive failed login a... |
| As A User Story | As a User, I want my account to be automatically l... |
| User Persona | Any system user with login credentials (e.g., Admi... |
| Business Value | Enhances system security by mitigating brute-force... |
| Functional Area | User Management and Security |
| Story Theme | Authentication and Access Control |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Account is locked on the 5th consecutive failed login attempt

### 3.1.3 Scenario Type

Error_Condition

### 3.1.4 Given

A user with a valid account has 4 consecutive failed login attempts recorded

### 3.1.5 When

The user attempts to log in with an incorrect password for the 5th consecutive time

### 3.1.6 Then

The login attempt fails AND the user's account status is set to 'locked' in the system AND the user is shown a specific message: 'Your account has been locked due to too many failed login attempts. Please contact an administrator.'

### 3.1.7 Validation Notes

Verify the 'LockoutEnd' timestamp in the user's database record is set to a future date. Verify the UI displays the correct lockout message.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

A locked account cannot be accessed even with correct credentials

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

A user's account is in a 'locked' state

### 3.2.5 When

The user attempts to log in with the correct password

### 3.2.6 Then

The login attempt is rejected AND the user is shown the specific 'Your account has been locked...' message.

### 3.2.7 Validation Notes

This must be tested to ensure a locked status supersedes a correct password.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

The failed login counter is reset after a successful login

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

A user has 3 consecutive failed login attempts

### 3.3.5 When

The user successfully logs in with the correct password

### 3.3.6 Then

The user is granted access to the system AND their consecutive failed login attempt counter is reset to 0.

### 3.3.7 Validation Notes

Verify the 'AccessFailedCount' in the user's database record is reset to 0.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Login attempts for non-existent users do not trigger lockout logic

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

An attacker is attempting to guess usernames

### 3.4.5 When

A login attempt is made with a username that does not exist in the system

### 3.4.6 Then

A generic 'Invalid username or password' message is displayed AND no user's failed login counter is incremented.

### 3.4.7 Validation Notes

This prevents username enumeration attacks from affecting valid accounts.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Each failed login attempt is recorded in the audit log

### 3.5.3 Scenario Type

Security

### 3.5.4 Given

A user with a valid username exists

### 3.5.5 When

A login attempt is made for that user with an incorrect password

### 3.5.6 Then

A 'Failed Login' event is recorded in the audit log, including the timestamp, username, and source IP address.

### 3.5.7 Validation Notes

Check the audit log file or database table for the corresponding entry.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Account lockout event is recorded in the audit log

### 3.6.3 Scenario Type

Security

### 3.6.4 Given

A user is about to be locked out on their next failed attempt

### 3.6.5 When

The user's 5th consecutive failed login attempt occurs, triggering the lockout

### 3.6.6 Then

An 'Account Locked' event is recorded in the audit log, including the timestamp, username, and source IP address.

### 3.6.7 Validation Notes

Verify the specific lockout event is logged, distinct from a standard failed login.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Error message display area on the login page.

## 4.2.0 User Interactions

- User enters credentials and submits the login form.
- System displays feedback message upon login failure.

## 4.3.0 Display Requirements

- For a standard failed login (before lockout), a generic message like 'Invalid username or password' must be shown.
- When an account becomes locked or a locked user tries to log in, a specific message must be shown: 'Your account has been locked due to too many failed login attempts. Please contact an administrator.'

## 4.4.0 Accessibility Needs

- Error messages must be associated with the relevant form fields and announced by screen readers using ARIA attributes.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-SEC-01

### 5.1.2 Rule Description

An account shall be locked after 5 consecutive failed login attempts.

### 5.1.3 Enforcement Point

During the authentication process, after validating credentials.

### 5.1.4 Violation Handling

The user's account status is changed to 'locked', preventing further login attempts.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-SEC-02

### 5.2.2 Rule Description

The consecutive failed login attempt counter for a user must be reset to zero upon a successful login.

### 5.2.3 Enforcement Point

Immediately after a successful authentication event for the user.

### 5.2.4 Violation Handling

N/A - This is a system action.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-027

#### 6.1.1.2 Dependency Reason

A functional login system must exist to add lockout logic to it.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-018

#### 6.1.2.2 Dependency Reason

The ability to create user accounts is required for testing this feature.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-101

#### 6.1.3.2 Dependency Reason

The audit logging framework must be in place to record failed login and lockout events.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-025

#### 6.1.4.2 Dependency Reason

A mechanism for an administrator to unlock an account is critical for the feature to be usable. A locked user needs a path to resolution.

## 6.2.0.0 Technical Dependencies

- ASP.NET Core Identity for user management and authentication.
- Entity Framework Core for data access to user records.
- Serilog for structured audit logging.

## 6.3.0.0 Data Dependencies

- User table in the SQLite database must support fields for `AccessFailedCount` and `LockoutEnd`.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The login process, including the check for lockout status, must complete within 500ms.

## 7.2.0.0 Security

- The system must not indicate whether the username or the password was incorrect on a failed login attempt.
- Account lockout must be enforced server-side and cannot be bypassed by client-side manipulation.
- Failed login and account lockout events must be logged for security auditing as per SRS section 6.4.

## 7.3.0.0 Usability

- The error message for a locked account must be clear and provide the user with the next step (contacting an administrator).

## 7.4.0.0 Accessibility

- All UI feedback must be compliant with WCAG 2.1 Level AA.

## 7.5.0.0 Compatibility

- The functionality must work consistently across all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- This is a standard feature with built-in support in ASP.NET Core Identity.
- Complexity is low assuming the Identity framework is configured correctly.
- Integration with the audit logging system adds a minor point of complexity.

## 8.3.0.0 Technical Risks

- Incorrect configuration of `IdentityOptions.Lockout` could lead to the feature not working as expected (e.g., locking out too soon, or not at all).
- Failure to provide distinct UI messages for generic failure vs. lockout could confuse users.

## 8.4.0.0 Integration Points

- Authentication Controller: The core logic will be handled here.
- ASP.NET Core Identity Configuration: Settings for `MaxFailedAccessAttempts` will be configured in `Program.cs` or `Startup.cs`.
- Audit Logging Service: Must be called from the authentication logic to log relevant events.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify account locks after exactly 5 failed attempts.
- Verify a locked user cannot log in with the correct password.
- Verify a successful login resets the failure count.
- Verify that attempting to log in with a non-existent username does not affect any valid user's failure count.
- Verify the correct audit logs are generated for both failed attempts and the final lockout event.

## 9.3.0.0 Test Data Needs

- A test user account with a known password.
- Ability to manipulate a user's `AccessFailedCount` and `LockoutEnd` status in the test database for specific scenarios.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit/integration tests.
- React Testing Library/Jest for frontend component tests.
- A browser automation tool like Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and integration tests implemented for the lockout logic, achieving >80% coverage
- E2E tests for the lockout user flow are passing
- UI messages for generic failure and lockout are distinct and approved by the PO/UX
- Security requirements, including audit logging, are validated
- Documentation for this security feature is updated in the Administration Guide
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a high-priority security feature and should be completed early in the development cycle.
- Must be planned in a sprint after US-027 (Login) is complete.
- Should be planned in conjunction with US-025 (Admin Unlock) to provide a complete workflow.

## 11.4.0.0 Release Impact

This is a critical feature for the initial release (MVP) to ensure baseline system security.

