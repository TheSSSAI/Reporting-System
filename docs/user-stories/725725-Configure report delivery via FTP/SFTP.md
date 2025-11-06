# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-066 |
| Elaboration Date | 2025-01-20 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Configure report delivery via FTP/SFTP |
| As A User Story | As an Administrator, I want to configure a report ... |
| User Persona | Administrator |
| Business Value | Enables automated report distribution to a wide ra... |
| Functional Area | Report Delivery Configuration |
| Story Theme | Report Distribution and Integration |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Administrator configures a new FTP/SFTP delivery destination

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

An Administrator is editing a report configuration in the Control Panel

### 3.1.5 When

they add a new delivery destination and select 'FTP/SFTP' as the type

### 3.1.6 Then

a form is displayed with fields for Protocol (FTP/SFTP), Host, Port, Username, Password, Remote Path, and Filename Pattern.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Administrator successfully tests an FTP connection

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

the FTP/SFTP configuration form is filled with valid credentials for an FTP server and a valid, writable remote path

### 3.2.5 When

the Administrator clicks the 'Test Connection' button

### 3.2.6 Then

a success message is displayed, confirming that the connection, authentication, and write permissions are valid.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Administrator successfully tests an SFTP connection

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

the FTP/SFTP configuration form is filled with valid credentials for an SFTP server and a valid, writable remote path

### 3.3.5 When

the Administrator clicks the 'Test Connection' button

### 3.3.6 Then

a success message is displayed, confirming that the connection, authentication, and write permissions are valid.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

A report is successfully delivered to an FTP server

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

a report is configured with a valid FTP delivery destination

### 3.4.5 When

the report generation job is executed successfully

### 3.4.6 Then

the generated report file is uploaded to the specified directory on the FTP server with the correct filename, and the job log indicates a successful delivery.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

A report is successfully delivered to an SFTP server

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

a report is configured with a valid SFTP delivery destination

### 3.5.5 When

the report generation job is executed successfully

### 3.5.6 Then

the generated report file is uploaded to the specified directory on the SFTP server with the correct filename, and the job log indicates a successful delivery.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Connection test fails due to invalid host

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

the FTP/SFTP configuration form is filled out with an unreachable hostname

### 3.6.5 When

the Administrator clicks the 'Test Connection' button

### 3.6.6 Then

a specific error message is displayed, such as 'Connection failed: Host not found or unreachable'.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Connection test fails due to invalid credentials

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

the FTP/SFTP configuration form is filled out with an incorrect username or password

### 3.7.5 When

the Administrator clicks the 'Test Connection' button

### 3.7.6 Then

a specific error message is displayed, such as 'Authentication failed. Please check username and password'.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Connection test fails due to lack of write permissions

### 3.8.3 Scenario Type

Error_Condition

### 3.8.4 Given

the FTP/SFTP configuration form is filled out with valid credentials but for a user who lacks write permissions to the specified remote path

### 3.8.5 When

the Administrator clicks the 'Test Connection' button

### 3.8.6 Then

a specific error message is displayed, such as 'Permission denied. The user does not have write access to the specified directory'.

## 3.9.0 Criteria Id

### 3.9.1 Criteria Id

AC-009

### 3.9.2 Scenario

Connection test fails due to non-existent remote path

### 3.9.3 Scenario Type

Error_Condition

### 3.9.4 Given

the FTP/SFTP configuration form is filled out with a remote path that does not exist on the server

### 3.9.5 When

the Administrator clicks the 'Test Connection' button

### 3.9.6 Then

a specific error message is displayed, such as 'Remote directory not found'.

## 3.10.0 Criteria Id

### 3.10.1 Criteria Id

AC-010

### 3.10.2 Scenario

A scheduled delivery fails at runtime

### 3.10.3 Scenario Type

Error_Condition

### 3.10.4 Given

a report is configured with an FTP/SFTP destination whose server becomes unavailable

### 3.10.5 When

the scheduled report job runs

### 3.10.6 Then

the delivery step fails after the configured number of retries, the overall job status is marked as 'Failed', and the job execution log contains a detailed error message explaining the failure.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A 'FTP/SFTP' option in the delivery destination type dropdown.
- A radio button or dropdown to select 'Protocol' (FTP or SFTP).
- Text input for 'Host' (hostname or IP address).
- Number input for 'Port', auto-populated to 21 for FTP and 22 for SFTP.
- Text input for 'Username'.
- Password input for 'Password'.
- Text input for 'Remote Path'.
- Text input for 'Filename Pattern' with help text showing available placeholders (e.g., {{ReportName}}, {{Timestamp}}).
- A 'Test Connection' button.
- A status area to display success or error messages from the connection test.

## 4.2.0 User Interactions

- Selecting the protocol should update the default port number.
- Clicking 'Test Connection' should disable the button and show a loading indicator until a result is returned.
- The form must not be savable if required fields are empty.

## 4.3.0 Display Requirements

- Clear and concise success and error messages must be displayed to the user after a connection test.
- Passwords must be masked in the UI at all times.

## 4.4.0 Accessibility Needs

- All form fields must have associated labels.
- Feedback from the 'Test Connection' button must be available to screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

FTP connections must use Passive Mode (PASV) by default to improve compatibility with firewalls.

### 5.1.3 Enforcement Point

Backend FTP client implementation.

### 5.1.4 Violation Handling

N/A - this is an implementation detail.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

All credentials (passwords) for FTP/SFTP destinations must be stored encrypted at rest.

### 5.2.3 Enforcement Point

Configuration saving process.

### 5.2.4 Violation Handling

System must prevent saving of credentials in plaintext. Failure to encrypt is a critical system failure.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

A connection test must time out after 20 seconds to prevent the UI from becoming unresponsive.

### 5.3.3 Enforcement Point

Backend connection testing logic.

### 5.3.4 Violation Handling

The test is aborted and a timeout error message is returned to the user.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-051

#### 6.1.1.2 Dependency Reason

This story requires the report configuration wizard to exist as a container for the new UI elements.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-057

#### 6.1.2.2 Dependency Reason

This story extends the delivery destination framework established in US-057.

## 6.2.0.0 Technical Dependencies

- A vetted third-party .NET library for SFTP (e.g., SSH.NET) and FTP communication.
- Integration with the system's secret management service for secure credential storage (as per SRS 3.3).
- Integration with the Polly resiliency library for implementing retry logic on delivery attempts.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The 'Test Connection' API call must respond within 20 seconds.
- File uploads should be performed asynchronously to avoid blocking the main job processing thread.

## 7.2.0.0 Security

- SFTP connections must use secure, modern cryptographic algorithms and reject legacy, insecure ones.
- FTP/SFTP credentials must never be written to logs in plaintext.
- The selected third-party library must be scanned for known vulnerabilities.

## 7.3.0.0 Usability

- Error messages for connection failures must be user-friendly and provide actionable advice (e.g., 'check firewall', 'verify credentials').

## 7.4.0.0 Accessibility

- The configuration form must adhere to WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The implementation should be compatible with common FTP and SFTP server software (e.g., vsftpd, ProFTPD, OpenSSH, FileZilla Server).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires both frontend (React form) and backend (.NET service) development.
- Need to select, vet, and integrate a third-party library for FTP/SFTP.
- Robust error handling is required for a variety of network and permission-related failure modes.
- Requires secure handling and storage of user-provided credentials.

## 8.3.0.0 Technical Risks

- The chosen library may have limitations or bugs affecting compatibility with certain FTP/SFTP servers.
- Firewall configurations in customer environments can make troubleshooting difficult.

## 8.4.0.0 Integration Points

- The ASP.NET Core backend API for saving and testing the configuration.
- The Quartz.NET job execution engine for performing the file delivery.
- The SQLite database via EF Core for storing the encrypted configuration.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Configure and save an FTP destination.
- Configure and save an SFTP destination.
- Successfully test connection and deliver a file via FTP.
- Successfully test connection and deliver a file via SFTP.
- Test all specified error conditions (bad host, bad credentials, bad path, no permissions, timeout).
- Verify that a runtime delivery failure correctly marks the parent job as 'Failed'.

## 9.3.0.0 Test Data Needs

- Access to a dedicated FTP server and a dedicated SFTP server for integration testing.
- Test accounts on these servers with both read/write and read-only permissions.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend component tests.
- An integration test suite that connects to live test FTP/SFTP servers.
- A database inspection tool to verify that credentials in the SQLite DB are encrypted.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >= 80% coverage for new code
- Integration testing against live test FTP and SFTP servers completed successfully
- User interface reviewed and approved by UX/Product Owner
- Security requirements validated, including verification of encrypted credentials
- Documentation for configuring FTP/SFTP delivery is updated in the User Guide
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- Requires setup of FTP and SFTP servers in the CI/CD environment for automated integration testing.
- Time should be allocated for researching and selecting the appropriate .NET libraries for FTP/SFTP.

## 11.4.0.0 Release Impact

This is a key feature for enterprise integration and should be included in the next major or minor release.

