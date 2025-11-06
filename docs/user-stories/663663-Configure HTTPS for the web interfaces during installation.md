# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-004 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Configure HTTPS for the web interfaces during inst... |
| As A User Story | As an IT Support user, I want to configure HTTPS s... |
| User Persona | IT Support: A user responsible for deploying, conf... |
| Business Value | Ensures all web-based interactions with the system... |
| Functional Area | System Installation and Deployment |
| Story Theme | System Setup and Security |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Configure HTTPS using a certificate from the Windows Certificate Store

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

The IT Support user is running the MSI installer with administrator privileges on a supported Windows OS and has reached the 'HTTPS Configuration' step

### 3.1.5 When

The user selects the option 'Use an existing certificate from the Windows Certificate Store', chooses a valid certificate with a private key from the populated dropdown list, enters a valid hostname and an available port (e.g., 443), and successfully completes the installation

### 3.1.6 Then

The application's web server is configured to use the selected certificate for the specified hostname and port, the installer creates a corresponding URL ACL entry (e.g., via 'netsh http add urlacl'), and a Windows Firewall rule is created to allow inbound traffic on the specified port.

### 3.1.7 Validation Notes

Verify by browsing to https://<hostname>:<port>. The browser should show a secure connection using the correct certificate. Verify the URL ACL and firewall rule exist using command-line tools.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Happy Path: Configure HTTPS using a PFX certificate file

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The IT Support user is running the MSI installer with administrator privileges and has reached the 'HTTPS Configuration' step

### 3.2.5 When

The user selects the option 'Use a certificate file (PFX)', provides a valid path to a PFX file, enters the correct password for the file, specifies a valid hostname and an available port, and successfully completes the installation

### 3.2.6 Then

The application's configuration is updated with the path to the PFX file and the encrypted password, and the web server is bound to the specified hostname and port using that certificate.

### 3.2.7 Validation Notes

Verify by browsing to https://<hostname>:<port>. The browser should show a secure connection. The application's configuration file should reference the PFX file path.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Alternative Flow: Generate a self-signed certificate for testing

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

The IT Support user is running the MSI installer with administrator privileges and has reached the 'HTTPS Configuration' step

### 3.3.5 When

The user selects the option 'Generate a self-signed certificate (for testing only)' and successfully completes the installation

### 3.3.6 Then

The installer generates a new self-signed certificate for 'localhost', installs it into the Windows Certificate Store (Local Machine\My), and configures the application to use it on a default high-numbered port (e.g., 8443).

### 3.3.7 Validation Notes

Verify by browsing to https://localhost:<port>. The browser will display a security warning, which is expected. After bypassing the warning, the site should load correctly over a secure connection.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Error Condition: Port is already in use

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

The IT Support user is on the 'HTTPS Configuration' step of the installer

### 3.4.5 When

The user enters a port number that is already being used by another application on the server and attempts to proceed

### 3.4.6 Then

The installer displays a clear validation error message stating 'Port [port number] is already in use. Please choose another port.' and prevents the user from continuing to the next step.

### 3.4.7 Validation Notes

Test by running a web server (e.g., IIS) on port 443 and then attempting to use port 443 in the installer.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Error Condition: Invalid PFX file password

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

The IT Support user is on the 'HTTPS Configuration' step and has selected the PFX file option

### 3.5.5 When

The user provides a valid PFX file path but enters an incorrect password and attempts to proceed

### 3.5.6 Then

The installer displays a validation error message stating 'The password for the selected certificate file is incorrect.' and prevents the user from continuing.

### 3.5.7 Validation Notes

Use a known PFX file and deliberately enter the wrong password to trigger the validation.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Error Condition: Insufficient permissions

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

The user attempts to run the MSI installer without administrator privileges

### 3.6.5 When

The installation process begins

### 3.6.6 Then

The installer should detect the lack of administrative rights and either fail immediately with a message 'This installation requires administrator privileges.' or have the UAC prompt appear to request elevation.

### 3.6.7 Validation Notes

Attempt to run the MSI from a standard user account.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Edge Case: Uninstallation cleanly removes configuration

### 3.7.3 Scenario Type

Edge_Case

### 3.7.4 Given

The application was successfully installed and configured with HTTPS

### 3.7.5 When

The IT Support user uninstalls the application via the Control Panel or MSI

### 3.7.6 Then

The uninstallation process must remove the URL ACL entry and the Windows Firewall rule that were created during installation.

### 3.7.7 Validation Notes

After uninstalling, use command-line tools ('netsh http show urlacl', 'netsh advfirewall firewall show rule name=all') to confirm the entries have been removed.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated wizard page within the MSI installer for 'HTTPS Configuration'.
- Radio buttons to select the certificate source: 'Windows Certificate Store', 'PFX File', 'Generate Self-Signed'.
- A dropdown list to select a certificate from the Windows Store, displaying friendly names (e.g., Issued To, Expiration Date).
- A file browser button for selecting a PFX file.
- A password input field for the PFX file password.
- Text input fields for 'Hostname' (e.g., reports.company.com) and 'Port' (e.g., 443).
- A 'Validate Settings' button to test the configuration before proceeding.

## 4.2.0 User Interactions

- Selecting a radio button should show/hide the relevant input fields for that option.
- The installer should perform validation checks when the user clicks 'Next' or a dedicated 'Validate Settings' button.
- Help text or tooltips should be available to explain what each option means.

## 4.3.0 Display Requirements

- The installer must clearly indicate that generating a self-signed certificate is not recommended for production environments.
- Validation errors must be displayed clearly on the wizard page, next to the problematic field.

## 4.4.0 Accessibility Needs

- All UI elements in the installer wizard must be keyboard navigable.
- All labels and instructions must be clear and readable.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "The application's web interfaces must not be configurable to run over unencrypted HTTP in a production environment.", 'enforcement_point': 'Installation and Application Configuration', 'violation_handling': 'The installer does not provide an option for HTTP. The application, once installed, should be configured to redirect any HTTP requests to HTTPS.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

- {'story_id': 'US-001', 'dependency_reason': 'This story implements a configuration step within the MSI installation process defined in US-001.'}

## 6.2.0 Technical Dependencies

- WiX Toolset: Required for creating the MSI package with custom UI and custom actions.
- .NET 8 SDK: Required for writing the custom action logic in C#.
- Windows APIs: The custom actions will need to call Windows APIs or command-line tools (like netsh.exe) to manage certificates, URL ACLs, and firewall rules.

## 6.3.0 Data Dependencies

- Access to the Windows Certificate Store (Local Machine\My) with sufficient permissions.
- For production, a valid, pre-existing SSL/TLS certificate (either in the store or as a PFX file).

## 6.4.0 External Dependencies

*No items available*

# 7.0.0 Non Functional Requirements

## 7.1.0 Performance

- The validation of settings on the installer page should complete within 3 seconds.

## 7.2.0 Security

- The installer must require administrator privileges to run.
- PFX file passwords must not be stored in plaintext in any logs or temporary files during installation.
- The application must be configured to use strong, modern TLS protocols (TLS 1.2+) by default.

## 7.3.0 Usability

- The configuration options should be clear to an IT professional, with helpful tooltips explaining technical details.

## 7.4.0 Accessibility

- The installer UI must adhere to standard Windows accessibility guidelines.

## 7.5.0 Compatibility

- The installation and configuration logic must be compatible with all specified target operating systems: Windows Server 2019/2022 and Windows 10/11 (64-bit).

# 8.0.0 Implementation Considerations

## 8.1.0 Complexity Assessment

Medium

## 8.2.0 Complexity Factors

- Creating custom UI dialogs in WiX is complex and requires specific expertise.
- Writing robust C# custom actions for the installer that handle validation, execution, and rollback is challenging.
- Ensuring the installer has the correct permissions and handles UAC elevation properly.
- The logic for a clean uninstall (rollback) must be perfect to avoid leaving orphaned system configurations.

## 8.3.0 Technical Risks

- A bug in a custom action could leave the system in a partially configured state, making manual cleanup necessary.
- Variations in Windows security policies (GPO) across different customer environments could interfere with the installer's ability to create firewall rules or URL ACLs.

## 8.4.0 Integration Points

- Windows Certificate Store
- Windows Firewall
- Windows HTTP Server API (for URL ACLs)
- The application's own configuration file (e.g., appsettings.json).

# 9.0.0 Testing Requirements

## 9.1.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0 Test Scenarios

- Test installation on a clean VM for each supported Windows version.
- Test all three certificate configuration paths (Store, PFX, Self-Signed).
- Test all defined error conditions (port in use, bad password, etc.).
- Test the uninstallation process to verify complete cleanup.
- Test upgrading from a previous version to ensure HTTPS settings are preserved or can be reconfigured.

## 9.3.0 Test Data Needs

- A valid PFX certificate file with a known password.
- A valid certificate pre-installed in the Windows Certificate Store of the test VM.
- A list of ports to test, including common ones like 80, 443, and 8080.

## 9.4.0 Testing Tools

- Virtualization software (Hyper-V, VMware) for clean test environments.
- OpenSSL or similar tools for generating test certificates.

# 10.0.0 Definition Of Done

- All acceptance criteria validated and passing on all supported OS versions.
- Code for custom actions and WiX configuration is peer-reviewed and merged.
- Unit tests for C# custom action logic achieve >80% coverage.
- The MSI package is successfully built and tested by QA.
- The uninstallation process is verified to cleanly remove all related system configurations.
- Security review confirms that sensitive data (passwords) is handled securely.
- Documentation for the installation process is updated to include the HTTPS configuration step.
- Story deployed and verified in a staging environment that mimics a clean installation.

# 11.0.0 Planning Information

## 11.1.0 Story Points

8

## 11.2.0 Priority

🔴 High

## 11.3.0 Sprint Considerations

- This is a foundational story for enabling any testing of the web UIs. It should be prioritized in an early sprint.
- Requires a developer with experience in WiX and Windows system administration concepts.

## 11.4.0 Release Impact

This feature is critical for the initial release. The product cannot be deployed securely in a production environment without it.

