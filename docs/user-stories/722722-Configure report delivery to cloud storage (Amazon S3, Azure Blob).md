# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-063 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Configure report delivery to cloud storage (Amazon... |
| As A User Story | As an Administrator, I want to configure a report ... |
| User Persona | Administrator |
| Business Value | Automates the distribution of reports to scalable ... |
| Functional Area | Report Delivery Configuration |
| Story Theme | Report Generation & Delivery |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

UI for selecting cloud storage provider

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

An Administrator is on the 'Delivery Destinations' step of the report configuration wizard

### 3.1.5 When

the Administrator adds a new destination and selects 'Cloud Storage'

### 3.1.6 Then

a secondary choice (e.g., radio buttons or dropdown) appears to select either 'Amazon S3' or 'Azure Blob'.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Configure and successfully test Amazon S3 delivery

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The Administrator has selected 'Amazon S3' as the provider

### 3.2.5 When

they enter a valid Access Key ID, Secret Access Key, Bucket Name, and Region, and then click 'Test Connection'

### 3.2.6 Then

a success message is displayed, and the configuration can be saved.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Configure and successfully test Azure Blob delivery

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

The Administrator has selected 'Azure Blob' as the provider

### 3.3.5 When

they enter a valid Connection String and Container Name, and then click 'Test Connection'

### 3.3.6 Then

a success message is displayed, and the configuration can be saved.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Successful report delivery to Amazon S3

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

A report is configured with a valid Amazon S3 delivery destination

### 3.4.5 When

the report generation job is executed successfully

### 3.4.6 Then

the generated report file is present in the specified S3 bucket and path.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Successful report delivery to Azure Blob

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

A report is configured with a valid Azure Blob delivery destination

### 3.5.5 When

the report generation job is executed successfully

### 3.5.6 Then

the generated report file is present in the specified Azure Blob container and path.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Test connection failure due to invalid credentials

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

The Administrator is configuring an Amazon S3 destination

### 3.6.5 When

they enter an invalid Secret Access Key and click 'Test Connection'

### 3.6.6 Then

a user-friendly error message is displayed, indicating an authentication failure.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Test connection failure due to insufficient permissions

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

The Administrator is configuring an Amazon S3 destination with valid credentials that lack 's3:PutObject' permissions

### 3.7.5 When

they click 'Test Connection'

### 3.7.6 Then

a user-friendly error message is displayed, indicating a permissions issue (e.g., 'Access Denied').

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Test connection failure due to non-existent bucket/container

### 3.8.3 Scenario Type

Error_Condition

### 3.8.4 Given

The Administrator is configuring an Azure Blob destination

### 3.8.5 When

they enter a container name that does not exist and click 'Test Connection'

### 3.8.6 Then

a user-friendly error message is displayed, indicating the container was not found.

## 3.9.0 Criteria Id

### 3.9.1 Criteria Id

AC-009

### 3.9.2 Scenario

Report delivery failure is logged

### 3.9.3 Scenario Type

Error_Condition

### 3.9.4 Given

A report is configured with a cloud storage destination whose credentials have been revoked

### 3.9.5 When

the report generation job runs

### 3.9.6 Then

the delivery step fails, the overall job status is marked as 'Failed', and the job execution log contains a detailed error message about the delivery failure.

## 3.10.0 Criteria Id

### 3.10.1 Criteria Id

AC-010

### 3.10.2 Scenario

Use of dynamic filename for uploaded report

### 3.10.3 Scenario Type

Happy_Path

### 3.10.4 Given

A cloud storage destination is configured with a filename pattern like 'Monthly-Report-{{Timestamp}}.pdf'

### 3.10.5 When

the report is generated

### 3.10.6 Then

the file uploaded to the cloud storage has a name that matches the resolved pattern, e.g., 'Monthly-Report-20250115T103000Z.pdf'.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- In the 'Delivery' configuration section, a dropdown to select destination type ('Cloud Storage').
- Radio buttons or a dropdown to select the cloud provider ('Amazon S3', 'Azure Blob').
- Dynamically displayed form fields based on provider selection.
- For S3: Text inputs for 'Access Key ID', 'Bucket Name', 'Region', 'Optional Folder Path', 'Filename Pattern'.
- For S3: Password input for 'Secret Access Key'.
- For Azure: Text inputs for 'Container Name', 'Optional Folder Path', 'Filename Pattern'.
- For Azure: Password input for 'Connection String'.
- A 'Test Connection' button for each cloud configuration.
- A non-intrusive loading indicator (e.g., spinner) while the connection test is in progress.
- Clear, dismissible feedback messages (e.g., toast notifications) for connection test success or failure.

## 4.2.0 User Interactions

- Selecting a cloud provider dynamically updates the form to show only relevant fields.
- Sensitive fields (Secret Key, Connection String) must be masked.
- The 'Save' button for the destination is disabled until a successful connection test has been performed in the current editing session.

## 4.3.0 Display Requirements

- Error messages from connection tests should be specific and actionable (e.g., 'Authentication failed. Check keys.' vs. 'Error').
- Placeholder text in input fields should provide examples (e.g., 'us-east-1' for Region).

## 4.4.0 Accessibility Needs

- All form fields must have associated `<label>` tags.
- Error messages must be programmatically linked to their corresponding input fields using `aria-describedby`.
- UI must be keyboard navigable.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Cloud storage credentials must be stored securely and never in plaintext.

### 5.1.3 Enforcement Point

Backend configuration service, upon saving the destination.

### 5.1.4 Violation Handling

System must use the configured secret management provider (.NET Secret Manager or Windows Certificate Store) as per SRS 3.3. A failure to encrypt/store the secret results in a failed save operation and a logged error.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A cloud storage destination cannot be saved without a prior successful connection test within the same user session.

### 5.2.3 Enforcement Point

Frontend UI and Backend API validation.

### 5.2.4 Violation Handling

The 'Save' button in the UI remains disabled. If an API call is made directly, it returns a 400 Bad Request error with a descriptive message.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-051

#### 6.1.1.2 Dependency Reason

Provides the report configuration wizard where the delivery destination UI will be integrated.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-057

#### 6.1.2.2 Dependency Reason

Provides the foundational model and framework for adding any type of delivery destination.

## 6.2.0.0 Technical Dependencies

- AWS SDK for .NET (AWSSDK.S3)
- Azure Storage Blobs SDK for .NET (Azure.Storage.Blobs)
- Application's existing secret management infrastructure (as defined in SRS 3.3)
- Application's resiliency framework (Polly) for handling transient upload errors.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

- Requires network access from the host server to Amazon S3 and Azure Blob Storage endpoints over HTTPS (port 443).

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The 'Test Connection' operation must time out after 15 seconds.
- File uploads for large reports should be streamed to minimize memory consumption on the host server.

## 7.2.0.0 Security

- All communication with cloud provider APIs must use HTTPS/TLS 1.2+.
- Credentials must be encrypted at rest using the system's defined secret management strategy.
- The application's service account/permissions should be restricted to only what is necessary (e.g., PutObject).

## 7.3.0.0 Usability

- Error messages returned from the cloud providers should be translated into clear, user-friendly instructions.

## 7.4.0.0 Accessibility

- The configuration UI must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The implementation should be compatible with all generally available regions for both AWS and Azure.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Integration with two separate third-party SDKs, each with its own authentication and error handling patterns.
- Secure handling and storage of cloud credentials.
- Implementation of a dynamic UI to accommodate different configuration fields.
- Robust error handling to provide meaningful feedback from API responses.

## 8.3.0.0 Technical Risks

- Firewall or proxy configurations on the customer's network may block access to cloud endpoints, requiring clear documentation for troubleshooting.
- Changes in cloud provider SDKs may require future maintenance.

## 8.4.0.0 Integration Points

- Backend ReportConfigurationService: To save the destination settings.
- Backend JobExecutionEngine: To retrieve settings and execute the file upload.
- Frontend Report Configuration Component: To render the UI.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify successful connection and upload to a test S3 bucket.
- Verify successful connection and upload to a test Azure Blob container.
- Test failure scenarios: invalid credentials, non-existent bucket/container, insufficient permissions, network timeout.
- Verify credentials are not exposed in logs or API responses.
- E2E test: Configure a report with S3 delivery, run the report, and verify the file exists in S3 via the AWS API.

## 9.3.0.0 Test Data Needs

- Valid test credentials for an AWS account with a dedicated test S3 bucket.
- Valid test credentials (connection string) for an Azure account with a dedicated test storage container.

## 9.4.0.0 Testing Tools

- xUnit/Moq for unit tests (mocking cloud SDKs).
- A dedicated integration test project that uses real credentials (from a secure CI/CD variable store).

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for both S3 and Azure services, achieving >80% coverage
- Integration testing against live AWS S3 and Azure Blob endpoints completed successfully
- User interface reviewed and approved for usability and accessibility
- Security review of credential handling process is complete and approved
- Documentation for configuring cloud storage delivery is created in the Administrator Guide
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- Requires access to test AWS and Azure accounts for integration testing. These credentials must be securely managed in the CI/CD environment.
- This story is a key feature for enterprise customers and enables significant automation workflows.

## 11.4.0.0 Release Impact

- This is a major feature enhancement for the report delivery module and should be highlighted in release notes.

