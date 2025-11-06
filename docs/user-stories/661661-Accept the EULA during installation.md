# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-002 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Accept the EULA during installation |
| As A User Story | As an IT Support user, I want to be presented with... |
| User Persona | IT Support User: A technical user responsible for ... |
| Business Value | Ensures legal compliance for both the vendor and t... |
| Functional Area | System Installation and Deployment |
| Story Theme | Initial Setup and Onboarding |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: User accepts the EULA and proceeds with installation

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

The IT Support user is running the MSI installer and has navigated to the EULA screen

### 3.1.5 When

The user checks the 'I accept the terms in the License Agreement' checkbox and then clicks the 'Next' button

### 3.1.6 Then

The 'Next' button is enabled upon checking the box, and the installer proceeds to the subsequent installation step.

### 3.1.7 Validation Notes

Manually test the installer flow. Verify that clicking 'Next' after acceptance moves the wizard to the next screen (e.g., installation directory selection).

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Progression is blocked until EULA is accepted

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

The IT Support user is on the EULA screen of the installer

### 3.2.5 When

The 'I accept...' checkbox is in its default, unchecked state

### 3.2.6 Then

The 'Next' button must be disabled and unclickable.

### 3.2.7 Validation Notes

Verify the initial state of the EULA screen upon loading. The 'Next' button should be visually greyed out and non-functional.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User declines the EULA by canceling the installation

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

The IT Support user is on the EULA screen

### 3.3.5 When

The user clicks the 'Cancel' button

### 3.3.6 Then

A confirmation dialog is displayed asking if they are sure they want to exit, and upon confirmation, the installer terminates cleanly without making any changes to the system.

### 3.3.7 Validation Notes

Check the file system and registry to ensure no application files or settings were created after canceling the installation.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

User toggles the acceptance state

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

The IT Support user is on the EULA screen

### 3.4.5 When

The user first checks the 'I accept...' checkbox, and then un-checks it

### 3.4.6 Then

The 'Next' button becomes enabled when the box is checked, and becomes disabled again when the box is un-checked.

### 3.4.7 Validation Notes

Verify that the state of the 'Next' button is correctly bound to the state of the acceptance checkbox in real-time.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

EULA text is scrollable

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

The IT Support user is on the EULA screen

### 3.5.5 When

The EULA text is longer than the visible display area

### 3.5.6 Then

A vertical scrollbar is present, allowing the user to scroll and read the entire agreement.

### 3.5.7 Validation Notes

Use a long EULA text file (in RTF format) to test that the scrollbar appears and is functional.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Keyboard navigation for accessibility

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

The IT Support user is on the EULA screen

### 3.6.5 When

The user presses the 'Tab' key

### 3.6.6 Then

The checkbox's state is toggled.

### 3.6.7 And When

The focus is on the checkbox and the user presses the 'Spacebar'

### 3.6.8 Validation Notes

Perform keyboard-only testing to ensure full accessibility of the screen.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A standard installer wizard window.
- A title for the screen, e.g., 'End-User License Agreement'.
- A scrollable rich text box to display the EULA.
- A checkbox with a label like 'I accept the terms in the License Agreement'.
- Standard navigation buttons: '< Back', 'Next >', 'Cancel'.

## 4.2.0 User Interactions

- The 'Next' button's enabled state is directly controlled by the acceptance checkbox.
- The EULA text can be scrolled using a mouse wheel or scrollbar.
- The checkbox can be toggled with a mouse click or by pressing the spacebar when focused.

## 4.3.0 Display Requirements

- The EULA text must be displayed with its original formatting (e.g., bolding, paragraphs) as provided in the source RTF file.

## 4.4.0 Accessibility Needs

- All interactive elements (checkbox, buttons) must be accessible via keyboard.
- The EULA text should be readable by standard Windows screen reader software.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'Installation cannot proceed without explicit acceptance of the EULA.', 'enforcement_point': 'During the installation wizard, on the EULA screen.', 'violation_handling': "The 'Next' button remains disabled, effectively blocking the installation path until the rule is satisfied."}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

- {'story_id': 'US-001', 'dependency_reason': 'This story adds a screen to the MSI installer. The basic MSI package and installation framework must be created first.'}

## 6.2.0 Technical Dependencies

- WiX Toolset: The specified technology for creating the MSI package. This story will be implemented using WiX UI dialog sets.

## 6.3.0 Data Dependencies

- Final EULA Text: The legally approved End-User License Agreement text, formatted as a Rich Text File (RTF), must be provided.

## 6.4.0 External Dependencies

- Legal Department: The EULA text must be sourced from and approved by the legal department before it can be integrated into the installer.

# 7.0.0 Non Functional Requirements

## 7.1.0 Performance

- The EULA screen must load instantly as part of the installer sequence.

## 7.2.0 Security

- The EULA text must be embedded directly within the MSI package to prevent it from being modified or tampered with post-build.

## 7.3.0 Usability

- The process of accepting the EULA should be familiar and intuitive to users accustomed to standard Windows software installations.

## 7.4.0 Accessibility

- The installer UI must adhere to Microsoft's accessibility guidelines for desktop applications.

## 7.5.0 Compatibility

- The installer and its EULA screen must render and function correctly on all target operating systems: Windows Server 2019/2022 and Windows 10/11 (64-bit).

# 8.0.0 Implementation Considerations

## 8.1.0 Complexity Assessment

Low

## 8.2.0 Complexity Factors

- This is a standard, built-in feature of the WiX Toolset.
- The primary effort is configuration, not custom development.
- The main dependency is external (waiting for the EULA text).

## 8.3.0 Technical Risks

- Minor risk of formatting issues if the provided EULA text is not a valid RTF file. The file should be validated before inclusion.

## 8.4.0 Integration Points

- Integrates into the UI sequence of the WiX installer project.

# 9.0.0 Testing Requirements

## 9.1.0 Testing Types

- Integration
- E2E
- Accessibility

## 9.2.0 Test Scenarios

- Verify the complete installation flow when accepting the EULA.
- Verify the installation is aborted correctly when canceling from the EULA screen.
- Verify the 'Next' button logic by toggling the checkbox multiple times.
- Verify the installer on all supported Windows versions.

## 9.3.0 Test Data Needs

- A sample EULA.rtf file for development.
- The final, legally-approved EULA.rtf file for release candidate builds.

## 9.4.0 Testing Tools

- Manual testing on virtual machines or physical hardware for each supported OS.

# 10.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code (WiX XML configuration) reviewed and approved by team
- The final, legally-approved EULA text is embedded in the installer
- Integration testing of the installer flow completed successfully
- User interface reviewed and approved by the Product Owner
- Accessibility requirements (keyboard navigation) validated
- Story deployed and verified in a staging environment via a successful installation of the built MSI

# 11.0.0 Planning Information

## 11.1.0 Story Points

1

## 11.2.0 Priority

🔴 High

## 11.3.0 Sprint Considerations

- This is a blocking requirement for any release. It should be prioritized early in the project.
- Confirm receipt of the final EULA text from the legal department before starting development to avoid delays.

## 11.4.0 Release Impact

This feature is mandatory for the first and all subsequent releases of the software.

