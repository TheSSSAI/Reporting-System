# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-003 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Create the primary administrator account during in... |
| As A User Story | As an IT Support user, I want to define the userna... |
| User Persona | IT Support: This user is responsible for deploying... |
| Business Value | Establishes the foundational security and access c... |
| Functional Area | System Installation and Setup |
| Story Theme | Initial Deployment Experience |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Successful creation of the primary administrator account

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

The IT Support user is running the MSI installer and has reached the 'Create Administrator Account' step

### 3.1.5 When

The user enters a valid username, a password that meets the default complexity policy, and enters the same password in the confirmation field, then proceeds with the installation

### 3.1.6 Then

The installation completes successfully, the specified user account is created in the database with the 'Administrator' role, the password is saved as a secure hash, and the user can immediately log in to the Control Panel with these credentials.

### 3.1.7 Validation Notes

Verify by logging into the Control Panel post-installation. Also, inspect the application's SQLite database to confirm the user record exists in the 'Users' table, is linked to the 'Administrator' role, and the password field contains a hash, not plaintext.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Error Condition: Passwords do not match

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

The user is on the 'Create Administrator Account' installer screen

### 3.2.5 When

The user enters a password in the 'Password' field and a different password in the 'Confirm Password' field

### 3.2.6 Then

An inline error message stating 'Passwords do not match' is displayed, and the user is prevented from proceeding to the next step of the installation.

### 3.2.7 Validation Notes

The 'Next' button in the installer should be disabled or should not advance the wizard when clicked.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Error Condition: Password does not meet complexity requirements

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

The user is on the 'Create Administrator Account' installer screen

### 3.3.5 When

The user enters a password that does not meet the default policy (minimum 12 characters, 1 uppercase, 1 lowercase, 1 number, 1 special character)

### 3.3.6 Then

An inline error message is displayed detailing the password complexity requirements, and the user is prevented from proceeding.

### 3.3.7 Validation Notes

Test with several non-compliant passwords, such as 'password', 'Password123', 'short', and 'longpasswordwithoutspecialchar'.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Error Condition: Required fields are empty

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

The user is on the 'Create Administrator Account' installer screen

### 3.4.5 When

The user attempts to proceed without filling in the username, password, or confirmation fields

### 3.4.6 Then

An error message indicating that all fields are required is displayed, and the user is prevented from proceeding.

### 3.4.7 Validation Notes

The 'Next' button should be disabled by default and only become enabled when all fields contain input.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Edge Case: Installation is cancelled or fails after account creation step

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

The user has successfully provided valid administrator credentials in the installer

### 3.5.5 When

The user cancels the installation OR the installation fails at a subsequent step

### 3.5.6 Then

The installation process is fully rolled back, and no administrator account exists in the application's database.

### 3.5.7 Validation Notes

After a cancelled/failed installation, inspect the SQLite database file location. The file should not exist, or if it does, the 'Users' table should be empty.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Security: Primary administrator account is non-deletable

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

The primary administrator account has been successfully created via the installer

### 3.6.5 When

Another administrator logs in and navigates to the user management screen

### 3.6.6 Then

The primary administrator account is visibly distinct or lacks a 'Delete' option, preventing its deletion as per the requirements of US-022.

### 3.6.7 Validation Notes

This links to the implementation of US-022, but the property making the account non-deletable must be set during creation in this story.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated screen within the WiX installer wizard with the title 'Create Primary Administrator Account'.
- Text input field for 'Username'.
- Password input field for 'Password' (masked input).
- Password input field for 'Confirm Password' (masked input).
- A 'Show/Hide Password' toggle (e.g., an eye icon) for the password fields.
- Navigation buttons ('Back', 'Next', 'Cancel').
- Inline text area for displaying validation error messages.

## 4.2.0 User Interactions

- The 'Next' button is disabled until all three input fields are populated.
- Real-time validation for password match and complexity should occur on input change or when focus leaves a field.
- Error messages appear immediately upon validation failure.

## 4.3.0 Display Requirements

- Clear instructions on the screen explaining the purpose of creating the account.
- Password policy requirements must be clearly displayed on the screen as helper text.

## 4.4.0 Accessibility Needs

- All installer screen elements must be keyboard navigable (Tab, Shift+Tab, Enter).
- Input fields must have correctly associated labels for screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

The primary administrator account created during installation cannot be deleted through the application's user interface.

### 5.1.3 Enforcement Point

During the account creation process within the installer's custom action.

### 5.1.4 Violation Handling

N/A - The account is flagged as non-deletable upon creation. The UI for user management (US-021) will enforce this by hiding or disabling the delete action for this specific user.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The password for the primary administrator must adhere to the system's default password policy.

### 5.2.3 Enforcement Point

During the validation step within the installer UI before allowing the user to proceed.

### 5.2.4 Violation Handling

Display a specific error message detailing the policy requirements and block installation progress.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-001

#### 6.1.1.2 Dependency Reason

This story defines a step within the MSI installation process. The core installer framework must exist first.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-023

#### 6.1.2.2 Dependency Reason

The default password policy logic must be defined and accessible to the installer's custom action for validation.

## 6.2.0.0 Technical Dependencies

- WiX Toolset: Required for building the MSI package and custom UI.
- ASP.NET Core Identity: The underlying framework for user creation, password hashing, and role management.
- Entity Framework Core & SQLite Provider: Required for the installer's custom action to interact with the application database.
- Database Schema: The database with 'Users' and 'Roles' tables must be created by the installer before this account creation step runs.

## 6.3.0.0 Data Dependencies

- The 'Administrator' role must be seeded into the database by the installer prior to or during the execution of the user creation logic.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- Input validation on the installer screen must feel instantaneous to the user (<100ms response).

## 7.2.0.0 Security

- Passwords must never be written to logs or stored in plaintext, even temporarily. They must be passed securely to the custom action and hashed immediately.
- The installer must use the application's standard password hashing mechanism (ASP.NET Core Identity's default).
- The installation process must be transactional; a failure at any point must roll back the user creation.

## 7.3.0.0 Usability

- Error messages must be clear, specific, and actionable.
- The purpose of this step and the password requirements should be obvious to the user without needing external documentation.

## 7.4.0.0 Accessibility

- The installer UI must adhere to standard Windows accessibility guidelines, ensuring it is usable with assistive technologies.

## 7.5.0.0 Compatibility

- The installer must function correctly on all specified target operating systems (Windows Server 2019/2022, Windows 10/11).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires creating a custom UI within the WiX Toolset, which is non-trivial.
- Requires a C# Custom Action to execute application logic from within the MSI context.
- Securely passing credentials from the installer UI to the custom action.
- Ensuring the custom action can correctly instantiate and use the application's DbContext and UserManager, including resolving dependencies and configuration.

## 8.3.0.0 Technical Risks

- Debugging custom actions in an MSI can be difficult and time-consuming.
- Ensuring the transactional integrity of the installation if the custom action fails.
- Potential for dependency conflicts when the custom action tries to load application assemblies.

## 8.4.0.0 Integration Points

- WiX Installer UI -> C# Custom Action
- C# Custom Action -> Application's Data Access Layer (EF Core)
- C# Custom Action -> Application's Identity Logic (ASP.NET Core Identity)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E (Manual Installation)

## 9.2.0.0 Test Scenarios

- Full, successful installation on a clean target OS.
- Verification of login and admin privileges after successful installation.
- Testing all validation rules on the installer screen (mismatched password, weak password, empty fields).
- Testing the installation rollback by cancelling the install after the admin creation step.
- Testing the installation rollback by forcing a later custom action to fail.

## 9.3.0.0 Test Data Needs

- A set of compliant and non-compliant passwords to test the validation logic.

## 9.4.0.0 Testing Tools

- Virtualization software (Hyper-V, VMware) for clean OS environments.
- DB Browser for SQLite to inspect the database post-installation.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code for the WiX UI and C# Custom Action is reviewed and approved
- Unit tests for the custom action's validation and user creation logic are implemented and passing with 80%+ coverage
- Manual end-to-end installation testing is completed successfully on a representative target OS
- Post-installation login is verified
- Database state is verified to be correct after successful and failed/cancelled installations
- Security requirements for password handling are validated
- Story deployed and verified as part of a complete build of the MSI installer

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational story for the application's deployment. It is a blocker for any story that requires an authenticated administrator.
- Requires a developer with experience in WiX Toolset and C# Custom Actions or allocation of time for research and learning.

## 11.4.0.0 Release Impact

Critical for the first release. The application cannot be shipped without this functionality.

