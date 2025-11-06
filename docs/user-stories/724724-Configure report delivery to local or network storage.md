# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-065 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Configure report delivery to local or network stor... |
| As A User Story | As an Administrator, I want to configure a report ... |
| User Persona | Administrator |
| Business Value | Enables automated archival of reports to controlle... |
| Functional Area | Report Delivery Configuration |
| Story Theme | Report Generation & Delivery |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Configure and save a valid local path destination

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am in the report configuration wizard on the 'Delivery' step

### 3.1.5 When

I add a new destination, select 'Local/Network Storage', enter a valid local path like 'C:\Reports', provide a filename pattern 'Report_{{ExecutionDate}}.pdf', select 'Overwrite' for file conflicts, and successfully test the delivery

### 3.1.6 Then

The system saves this delivery configuration successfully as part of the report definition.

### 3.1.7 Validation Notes

Verify the delivery configuration is correctly stored in the SQLite database associated with the report configuration.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Configure and save a valid UNC path destination

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am in the report configuration wizard on the 'Delivery' step

### 3.2.5 When

I add a new destination, select 'Local/Network Storage', enter a valid UNC path like '\\fileserver\share\reports', provide a filename pattern, and successfully test the delivery

### 3.2.6 Then

The system saves this UNC path delivery configuration successfully.

### 3.2.7 Validation Notes

This requires the Windows Service account to have network access and write permissions to the specified UNC path.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Successfully test a writable path

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I have entered a valid and writable path in the 'Local/Network Storage' delivery configuration form

### 3.3.5 When

I click the 'Test Delivery' button

### 3.3.6 Then

The system attempts to write and delete a temporary file, and I see a success message like 'Connection successful. Path is writable.'

### 3.3.7 Validation Notes

The test should create a uniquely named temporary file (e.g., '.testwrite_guid') and ensure it is deleted after the write check.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Report is successfully delivered to the configured path

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

A report is configured to deliver to a valid local path 'C:\Reports' with filename pattern 'Sales_{{ReportName}}_{{ExecutionDate}}.csv'

### 3.4.5 When

The report job runs on '2025-01-15'

### 3.4.6 Then

A file named 'Sales_DailySales_2025-01-15.csv' is created in the 'C:\Reports' directory with the correct report content.

### 3.4.7 Validation Notes

Verify the file exists, the name matches the pattern with placeholders correctly substituted, and the file content is correct.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Attempt to save with a syntactically invalid path

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am configuring a 'Local/Network Storage' delivery destination

### 3.5.5 When

I enter a syntactically invalid path like 'C::\reports' or 'badpath'

### 3.5.6 Then

The UI displays an inline validation error message, and the 'Save' button for the report configuration is disabled.

### 3.5.7 Validation Notes

Test with various invalid path formats.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Test a path that does not exist

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

I have entered a syntactically valid path that does not exist, like 'C:\non_existent_dir'

### 3.6.5 When

I click the 'Test Delivery' button

### 3.6.6 Then

I see a clear error message like 'Test failed: The specified path does not exist.'

### 3.6.7 Validation Notes

The system should not create the directory automatically.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Test a path where the service account lacks write permissions

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

I have entered a valid path to a directory where the Windows Service account does not have write permissions

### 3.7.5 When

I click the 'Test Delivery' button

### 3.7.6 Then

I see a clear error message like 'Test failed: Access to the path is denied.'

### 3.7.7 Validation Notes

This requires setting up a folder with restricted permissions for the test environment.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Handle filename conflict by overwriting

### 3.8.3 Scenario Type

Alternative_Flow

### 3.8.4 Given

A report is configured to deliver to 'C:\Reports' with filename 'status.pdf' and the 'If File Exists' option is set to 'Overwrite'

### 3.8.5 And

A file named 'status.pdf' already exists in 'C:\Reports'

### 3.8.6 When

The report job runs again

### 3.8.7 Then

The existing 'status.pdf' file is replaced by the newly generated report.

### 3.8.8 Validation Notes

Check the 'Date modified' timestamp of the file to confirm it was overwritten.

## 3.9.0 Criteria Id

### 3.9.1 Criteria Id

AC-009

### 3.9.2 Scenario

Handle filename conflict by appending a unique ID

### 3.9.3 Scenario Type

Alternative_Flow

### 3.9.4 Given

A report is configured to deliver to 'C:\Reports' with filename 'status.pdf' and the 'If File Exists' option is set to 'Append Unique ID'

### 3.9.5 And

A file named 'status.pdf' already exists in 'C:\Reports'

### 3.9.6 When

The report job runs again

### 3.9.7 Then

A new file is created with a name like 'status (1).pdf' or 'status_timestamp.pdf', leaving the original file untouched.

### 3.9.8 Validation Notes

Verify both the original and the new file exist in the directory.

## 3.10.0 Criteria Id

### 3.10.1 Criteria Id

AC-010

### 3.10.2 Scenario

Delivery fails when the target disk is full

### 3.10.3 Scenario Type

Edge_Case

### 3.10.4 Given

A report is configured to deliver to a path on a storage volume that is full

### 3.10.5 When

The report job attempts to deliver the file

### 3.10.6 Then

The delivery step fails, the overall job status is marked as 'Failed', and the job execution log contains a clear error message like 'Delivery failed: There is not enough space on the disk.'

### 3.10.7 Validation Notes

This may require a simulated environment or a dedicated test partition that can be filled.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- In the 'Delivery' configuration section, a dropdown to select destination type, including 'Local/Network Storage'.
- A form for 'Local/Network Storage' with a text input field for 'Path (Local or UNC)'.
- A text input field for 'Filename Pattern'.
- A help icon or tooltip next to 'Filename Pattern' that lists available placeholders (e.g., {{ReportName}}, {{ExecutionDate}}, {{ExecutionTime}}, {{JobId}}).
- A radio button group or dropdown for 'If File Exists' with options: 'Overwrite', 'Append Unique ID'.
- A 'Test Delivery' button within the form.
- An inline text area for displaying success or error messages from the test.

## 4.2.0 User Interactions

- Selecting 'Local/Network Storage' from the destination type dropdown reveals the specific configuration form for it.
- The 'Save' button for the overall report configuration is disabled if the delivery configuration is incomplete or has validation errors.
- Clicking 'Test Delivery' triggers an asynchronous API call and displays a loading indicator until a success or error message is returned.

## 4.3.0 Display Requirements

- Validation errors (e.g., 'Path is required', 'Invalid path format') must be displayed clearly next to the corresponding input field.
- Success and error messages from the 'Test Delivery' action must be unambiguous.

## 4.4.0 Accessibility Needs

- All form fields must have associated `<label>` elements.
- All interactive elements (buttons, inputs) must be keyboard accessible and have clear focus indicators.
- Error messages must be associated with their respective fields programmatically (e.g., using `aria-describedby`).

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A delivery destination configuration cannot be saved as part of a report unless a successful test has been performed within the current editing session.

### 5.1.3 Enforcement Point

On attempting to save the report configuration.

### 5.1.4 Violation Handling

The UI will prevent the save operation and display a message prompting the user to test the delivery destination first.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The system must not automatically create directories. The full target path must exist prior to delivery.

### 5.2.3 Enforcement Point

During the 'Test Delivery' action and during the actual report delivery.

### 5.2.4 Violation Handling

The action fails with a 'Path not found' error.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

Filename patterns that resolve to include characters invalid for Windows filenames (e.g., \ / : * ? " < > |) must be automatically sanitized.

### 5.3.3 Enforcement Point

During the report delivery step, just before writing the file.

### 5.3.4 Violation Handling

Invalid characters are replaced with an underscore ('_') and a warning is written to the job execution log.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-051

#### 6.1.1.2 Dependency Reason

This story implements a specific delivery option within the report configuration wizard created in US-051.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-057

#### 6.1.2.2 Dependency Reason

This story provides a concrete implementation for the generic delivery destination framework defined in US-057.

## 6.2.0.0 Technical Dependencies

- The backend report generation engine must provide the generated report as a file stream or byte array to the delivery module.
- The ASP.NET Core backend must have an API endpoint to handle the 'Test Delivery' request.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

- The Windows Service's runtime user account must have appropriate file system and network permissions to write to the target directories. This is an operational dependency managed by the IT Support user.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- For reports larger than 10MB, the file should be written to disk using a stream to minimize memory consumption during the delivery phase.

## 7.2.0.0 Security

- All user-provided path inputs must be validated and sanitized on the backend to prevent path traversal attacks (e.g., '../../'). The resolved path must be confirmed to be an absolute path.
- The application must not store any user credentials for file system access; it must rely solely on the permissions of the Windows Service account.

## 7.3.0.0 Usability

- Error messages related to path or permission issues must be clear and actionable for an Administrator.

## 7.4.0.0 Accessibility

- The UI for this feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must support both standard Windows paths (e.g., 'C:\folder') and UNC paths (e.g., '\\server\share').

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Handling file system permissions under the context of a Windows Service can be complex to debug.
- Testing UNC path functionality requires a specific network environment setup.
- Ensuring robust error handling for various I/O exceptions (disk full, path not found, access denied) is critical.
- Frontend state management to track the 'tested' status of a delivery destination.

## 8.3.0.0 Technical Risks

- Misconfiguration of the service account permissions by the IT Support user is a high-probability operational risk that will cause this feature to fail. Documentation must be very clear.
- Network latency or instability could cause intermittent failures when writing to UNC paths.

## 8.4.0.0 Integration Points

- Integrates with the core report configuration data model.
- Integrates with the Quartz.NET job execution pipeline, being one of the final steps after report generation.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Successful delivery to a local path.
- Successful delivery to a UNC path.
- Failed delivery due to non-existent directory.
- Failed delivery due to insufficient permissions (both local and UNC).
- Filename conflict handling for both 'Overwrite' and 'Append Unique ID' options.
- Validation of all filename placeholders.
- UI validation for invalid path formats.

## 9.3.0.0 Test Data Needs

- A dedicated local folder with full permissions for the test runner.
- A dedicated local folder with read-only permissions.
- A network share with full permissions.
- A network share with read-only permissions.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- A test environment with a configured network share for integration and E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >80% coverage for new code
- Integration testing for local and UNC paths completed successfully
- User interface reviewed and approved by UX/Product Owner
- Security requirements (path sanitization) validated via code review and testing
- Documentation for Administrators and IT Support updated to explain configuration and permission requirements
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- Requires a test environment with a configurable network share. The availability of this environment should be confirmed before starting the sprint.
- Requires both backend (.NET) and frontend (React) development work that can be done in parallel.

## 11.4.0.0 Release Impact

This is a core delivery feature. Its absence would be a major gap in functionality for the initial release.

