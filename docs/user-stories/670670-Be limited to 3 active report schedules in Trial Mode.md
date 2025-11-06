# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-011 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Be limited to 3 active report schedules in Trial M... |
| As A User Story | As an Administrator using the system in Trial Mode... |
| User Persona | Administrator |
| Business Value | This enforces a key limitation of the Trial Mode, ... |
| Functional Area | System Licensing & Report Configuration |
| Story Theme | Trial Mode Experience and Licensing Enforcement |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successfully create schedules up to the limit

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an Administrator, the system is in Trial Mode, and there are 2 existing report configurations with active schedules

### 3.1.5 When

I create a new report configuration with an active schedule and save it

### 3.1.6 Then

The new report configuration is saved successfully, and the total count of active schedules in the system is now 3.

### 3.1.7 Validation Notes

Verify via the UI and by checking the database that the new report is saved and its schedule is marked as active.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Blocked from creating a new report with a schedule when at the limit

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

I am an Administrator, the system is in Trial Mode, and there are already 3 report configurations with active schedules

### 3.2.5 When

I attempt to create and save a new report configuration with an active schedule

### 3.2.6 Then

The system prevents the report from being saved, and a clear error message is displayed in the UI stating, 'Trial Mode Limit Reached: You can only have a maximum of 3 active report schedules. Please upgrade your license or disable an existing schedule.'

### 3.2.7 Validation Notes

Verify that the UI shows the specified error message and that no new report configuration is created in the database.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Blocked from enabling an existing schedule when at the limit

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am an Administrator, the system is in Trial Mode, and there are 3 report configurations with active schedules

### 3.3.5 When

I edit a fourth, unscheduled report and attempt to enable its schedule and save

### 3.3.6 Then

The system prevents the changes from being saved, and the same error message from AC-002 is displayed.

### 3.3.7 Validation Notes

Verify that the UI shows the error and the 'IsScheduleEnabled' flag for the fourth report remains false in the database.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Able to enable a schedule after disabling another

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

I am an Administrator, the system is in Trial Mode, and there are 3 active report schedules

### 3.4.5 When

I disable the schedule for one report, and then enable the schedule for a different, previously unscheduled report

### 3.4.6 Then

The actions are successful, and the total count of active schedules remains 3.

### 3.4.7 Validation Notes

This can be tested as a two-step E2E scenario. First, confirm a schedule is disabled. Second, confirm another can now be enabled.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

System reverts to Trial Mode from a licensed state with more than 3 active schedules

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

The system has an active license, and an Administrator has configured 5 reports with active schedules

### 3.5.5 When

The license expires and the system automatically reverts to Trial Mode

### 3.5.6 Then

All 5 report schedules are automatically set to 'disabled', and a persistent notification is displayed in the Control Panel dashboard informing the Administrator that schedules were disabled due to the license change.

### 3.5.7 Validation Notes

This requires a test setup to simulate license expiration. Verify the database state of all schedules and the presence of the UI notification.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Proactive UI feedback on schedule limit

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

I am an Administrator and the system is in Trial Mode

### 3.6.5 When

I view the list of report configurations or the main dashboard

### 3.6.6 Then

A visual indicator is displayed showing the current usage against the limit (e.g., 'Active Schedules: 2/3').

### 3.6.7 Validation Notes

Verify the UI element exists and accurately reflects the count of active schedules.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- An inline error message container on the report configuration page.
- A visual counter element on the report list page/dashboard (e.g., 'Active Schedules: X/3').
- A persistent notification banner on the dashboard for the license reversion scenario (AC-005).

## 4.2.0 User Interactions

- The check is triggered when the user clicks the 'Save' button on the report configuration page.
- The error message should prevent the save action and remain visible until the user corrects the issue.

## 4.3.0 Display Requirements

- Error Message Text: 'Trial Mode Limit Reached: You can only have a maximum of 3 active report schedules. Please upgrade your license or disable an existing schedule.'
- License Reversion Notification Text: 'Your license has changed. To comply with Trial Mode limits, all active report schedules have been disabled.'

## 4.4.0 Accessibility Needs

- Error messages must be programmatically linked to the form submission button using `aria-describedby` to be announced by screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

In Trial Mode, the maximum number of report configurations with an enabled schedule is 3.

### 5.1.3 Enforcement Point

Backend validation during the creation or update of a ReportConfiguration entity.

### 5.1.4 Violation Handling

The create/update operation is rejected with a specific error code and message.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A schedule is considered 'active' if its 'enabled' flag is true. A report with a defined CRON expression but an 'enabled' flag set to false does not count towards the limit.

### 5.2.3 Enforcement Point

The database query that counts active schedules.

### 5.2.4 Violation Handling

N/A - This is a definition.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

When the system's license state transitions into Trial Mode, if the number of active schedules exceeds the limit, all active schedules must be programmatically disabled.

### 5.3.3 Enforcement Point

An event handler that triggers on a license state change.

### 5.3.4 Violation Handling

All schedules are disabled, and a system notification is generated for the Administrator.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-017

#### 6.1.1.2 Dependency Reason

The system must have the capability to revert to Trial Mode for the edge case (AC-005) to be implemented.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-058

#### 6.1.2.2 Dependency Reason

The data model and UI for creating a report with a schedule must exist before a limit can be placed upon it.

## 6.2.0.0 Technical Dependencies

- A centralized Licensing Service/Module that provides the current license state (e.g., 'Trial', 'Active').
- An eventing mechanism to broadcast license state changes throughout the application.

## 6.3.0.0 Data Dependencies

- The `ReportConfiguration` entity in the database must have a boolean field to indicate if a schedule is enabled.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The backend check for the active schedule count must complete in under 50ms.

## 7.2.0.0 Security

- The enforcement of the limit must be performed on the backend to prevent client-side bypass.
- The license state itself must be securely stored and validated.

## 7.3.0.0 Usability

- The error message must be clear, concise, and provide a path to resolution (disable another schedule or upgrade).

## 7.4.0.0 Accessibility

- All UI elements, including error messages and notifications, must comply with WCAG 2.1 Level AA.

## 7.5.0.0 Compatibility

- The functionality must work consistently across all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Backend logic for create/update is simple.
- The primary complexity comes from implementing the robust, event-driven handler for the license state change (AC-005), which must perform a batch update on existing data.
- Requires coordination between the licensing module and the report configuration module.

## 8.3.0.0 Technical Risks

- If an event-driven architecture is not used for license state changes, the system could enter an inconsistent state where it's in Trial Mode but has more than 3 active schedules.
- The batch update of schedules on license reversion could fail, requiring transactional integrity.

## 8.4.0.0 Integration Points

- The Report Configuration service must integrate with the Licensing Service.
- The UI components must fetch the active schedule count for the proactive counter.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify an admin can create exactly 3 active schedules in Trial Mode.
- Verify an attempt to create a 4th active schedule is blocked with the correct error.
- Verify an attempt to enable a 4th schedule is blocked with the correct error.
- Verify that disabling one schedule allows another to be enabled.
- Simulate a license expiration and verify that all schedules are disabled and a notification appears.

## 9.3.0.0 Test Data Needs

- A system state with 0, 2, and 3 active schedules.
- A system state with an active license and >3 active schedules to test the reversion scenario.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- In-memory SQLite provider for integration tests.
- Jest/React Testing Library for frontend component tests.
- A browser automation framework (e.g., Playwright, Cypress) for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for all new logic (service layer, event handlers) with >80% coverage
- Integration testing completed for the create/update endpoints and the license reversion flow
- E2E tests for the user-facing scenarios are implemented and passing
- User interface changes reviewed and approved for usability and accessibility
- Security requirements validated (backend enforcement)
- Documentation for Trial Mode limitations is updated
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story should be prioritized early in the development of licensing features as it's a core business requirement for the trial.
- Requires the prerequisite stories (US-017, US-058) to be completed in a prior sprint or early in the same sprint.

## 11.4.0.0 Release Impact

- Essential for the initial public release of the product to properly gate features in the trial version.

