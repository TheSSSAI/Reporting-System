# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-005 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Upgrade an existing installation in-place using a ... |
| As A User Story | As an IT Support user, I want to run a new version... |
| User Persona | IT Support |
| Business Value | Enables seamless and safe adoption of new features... |
| Functional Area | System Deployment and Maintenance |
| Story Theme | Installation and Lifecycle Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful in-place upgrade with data and configuration preservation

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

An older version of the application is installed and running, with existing user accounts, report configurations, generated reports, logs, and custom plug-ins.

### 3.1.5 When

The IT Support user runs the MSI installer for a newer version of the application.

### 3.1.6 Then

The installer detects the existing installation and initiates an upgrade workflow.

### 3.1.7 Validation Notes

Verify the installer UI shows 'Upgrading...' or similar text.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Application binaries are updated

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The upgrade process has been initiated.

### 3.2.5 When

The installer's file copy operation completes.

### 3.2.6 Then

The application's program files (DLLs, EXEs) in the installation directory are replaced with the files from the new version.

### 3.2.7 Validation Notes

Check file versions and modification dates in the installation folder post-upgrade.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User-generated data and configuration are preserved

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

The upgrade process is running.

### 3.3.5 When

The installer completes.

### 3.3.6 Then

The SQLite database file, generated report files, log files, and any custom plug-in DLLs are not deleted or overwritten and remain in their respective directories.

### 3.3.7 Validation Notes

Compare the contents and timestamps of the data, logs, and plugins directories before and after the upgrade. They should be unchanged or appended to, not replaced.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Database schema is migrated automatically

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

The new application version requires a database schema update.

### 3.4.5 When

The Windows Service starts for the first time after the upgrade.

### 3.4.6 Then

The application automatically applies the necessary database migrations (e.g., using EF Core Migrations) to update the schema without data loss.

### 3.4.7 Validation Notes

Inspect the database schema after upgrade to confirm new tables/columns exist. Query existing data to ensure it was preserved.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

System is fully operational after upgrade

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

The MSI upgrade process has completed successfully.

### 3.5.5 When

An Administrator logs into the Control Panel.

### 3.5.6 Then

They can access all pre-existing configurations, and the system's 'About' page or version indicator shows the new version number.

### 3.5.7 Validation Notes

Perform a smoke test: log in, view a report configuration, and trigger a report generation.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Attempting to downgrade is blocked

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

Version 2.0 of the application is installed.

### 3.6.5 When

The IT Support user attempts to run the MSI installer for version 1.5.

### 3.6.6 Then

The installer immediately stops the process and displays a clear error message stating that a newer version is already installed.

### 3.6.7 Validation Notes

Verify the exact error message is shown and the installation is blocked.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Upgrade is rolled back if database migration fails

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

An upgrade is initiated, but the database migration script fails due to an error.

### 3.7.5 When

The installer attempts to finalize the installation.

### 3.7.6 Then

The installer must perform a full rollback, restoring the previous version's application files and service configuration.

### 3.7.7 Validation Notes

The Windows Service should be configured to point to the old binaries and should be runnable. The database schema should remain in its pre-upgrade state.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Upgrade is rolled back if user cancels mid-process

### 3.8.3 Scenario Type

Edge_Case

### 3.8.4 Given

An upgrade is in progress.

### 3.8.5 When

The user clicks the 'Cancel' button in the MSI wizard.

### 3.8.6 Then

The installer cleanly rolls back all changes, leaving the original installation intact and fully functional.

### 3.8.7 Validation Notes

After cancellation, verify the service is running the old version and all data is accessible.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Standard MSI wizard screens (Welcome, Progress, Finish)
- Error message dialog box

## 4.2.0 User Interactions

- User launches the MSI and follows the wizard prompts.
- User confirms the upgrade action.

## 4.3.0 Display Requirements

- The installer UI must clearly indicate that an 'Upgrade' is being performed.
- Error messages must be clear and actionable (e.g., 'A newer version of this product is already installed.').

## 4.4.0 Accessibility Needs

- The MSI installer UI should adhere to standard Windows accessibility features.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

User-generated content must never be deleted during an upgrade.

### 5.1.3 Enforcement Point

WiX installer configuration.

### 5.1.4 Violation Handling

The installer build process should fail if data directories are not marked for preservation.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Downgrading the application via the installer is strictly prohibited.

### 5.2.3 Enforcement Point

MSI launch condition check.

### 5.2.4 Violation Handling

The installation is blocked, and an error message is displayed to the user.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

- {'story_id': 'US-001', 'dependency_reason': 'A base MSI installation package must exist before upgrade logic can be added to it.'}

## 6.2.0 Technical Dependencies

- WiX Toolset: Required for building the MSI package and implementing upgrade logic.
- Entity Framework Core Migrations: The application must have a robust, automated database migration strategy that can be triggered on service startup.
- Windows Service Control: The installer needs permissions and logic to stop the service before updating files and restart it after.

## 6.3.0 Data Dependencies

*No items available*

## 6.4.0 External Dependencies

*No items available*

# 7.0.0 Non Functional Requirements

## 7.1.0 Performance

- The upgrade process on recommended hardware should complete in under 5 minutes.

## 7.2.0 Security

- The MSI package must be digitally signed to ensure authenticity and integrity.
- The installer must require administrator privileges to execute.

## 7.3.0 Usability

- The upgrade process should require minimal user interaction (e.g., 'Next', 'Upgrade', 'Finish').

## 7.4.0 Accessibility

- N/A (Handled by standard Windows Installer UI)

## 7.5.0 Compatibility

- The upgrade logic must be compatible with all supported Windows versions (Windows Server 2019/2022, Windows 10/11).

# 8.0.0 Implementation Considerations

## 8.1.0 Complexity Assessment

Medium

## 8.2.0 Complexity Factors

- Configuring the WiX `MajorUpgrade` element correctly to handle version detection and file replacement.
- Ensuring all user data/config/log/plugin directories are correctly marked to be preserved across upgrades.
- Implementing and testing robust rollback custom actions for failed upgrades (e.g., failed DB migration).
- The application's startup logic must be hardened to handle database migrations transactionally and report failures clearly.

## 8.3.0 Technical Risks

- A failed database migration could leave the application in a non-functional state if the rollback is not handled perfectly.
- Incorrectly configured WiX project could accidentally wipe user data, requiring a full restore from backup.

## 8.4.0 Integration Points

- Windows Services Manager (for stopping/starting the service).
- Filesystem (for replacing binaries and preserving data).
- SQLite database engine (via EF Core for migrations).

# 9.0.0 Testing Requirements

## 9.1.0 Testing Types

- Integration
- E2E
- Regression

## 9.2.0 Test Scenarios

- Upgrade from version N to N+1 (minor release).
- Upgrade from version N to N+2 (skipping a version).
- Upgrade from version 1.x to 2.0 (major release with significant schema changes).
- Verify downgrade from 2.0 to 1.x is blocked.
- Simulate a failed database migration and verify a clean rollback to the previous version.
- Cancel the upgrade mid-process and verify a clean rollback.

## 9.3.0 Test Data Needs

- A pre-upgrade instance populated with a representative set of data: multiple users, complex report configurations, various connector types, and existing generated reports.

## 9.4.0 Testing Tools

- Virtualization software (e.g., Hyper-V, VMware) to create clean test environments for installation and upgrades.

# 10.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for any new application logic (e.g., migration handling) and passing
- Integration testing of the MSI upgrade process completed successfully across all defined scenarios
- MSI package is digitally signed
- Upgrade procedure is documented in the Installation and Administration Guide
- Story deployed and verified in a staging environment that mirrors production

# 11.0.0 Planning Information

## 11.1.0 Story Points

8

## 11.2.0 Priority

🔴 High

## 11.3.0 Sprint Considerations

- This story requires significant testing effort on clean virtual machines. The testing time should be factored into the sprint capacity.
- This is a foundational feature for the product's long-term maintainability and should be prioritized before the first public release.

## 11.4.0 Release Impact

- Critical for any release after v1.0. Without this, customers have no supported path to update the software.

