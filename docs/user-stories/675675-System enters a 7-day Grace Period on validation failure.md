# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-016 |
| Elaboration Date | 2025-01-17 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | System enters a 7-day Grace Period on validation f... |
| As A User Story | As an Administrator, I want the system to enter a ... |
| User Persona | Administrator |
| Business Value | Ensures business continuity by preventing service ... |
| Functional Area | System Licensing & Activation |
| Story Theme | Licensing Workflow Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Entering the Grace Period upon first validation failure

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

The system is in an 'Active' state with a valid license

### 3.1.5 When

The periodic license validation check fails for the first time (e.g., due to network error)

### 3.1.6 Then

The system's internal state is updated to 'Grace Period'.

### 3.1.7 And

The system remains fully functional, with no Trial Mode limitations applied.

### 3.1.8 Validation Notes

Verify by mocking the license service to return a failure. Check the database for the updated state and timestamp. Confirm that creating a new report (exceeding trial limits) is still possible.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Successful license re-validation during the Grace Period

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

The system is in the 'Grace Period' state

### 3.2.5 When

A subsequent periodic or manual license validation check succeeds

### 3.2.6 Then

The system's internal state is updated back to 'Active'.

### 3.2.7 And

The Grace Period notification in the UI is removed (as per US-014).

### 3.2.8 Validation Notes

While in a grace period, mock the license service to return a success. Verify the state change in the database and that the UI notification disappears.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Repeated validation failures within the Grace Period

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

The system is in the 'Grace Period' state with a recorded 'GracePeriodStartDate'

### 3.3.5 When

Another periodic license validation check fails

### 3.3.6 Then

The system's internal state remains 'Grace Period'.

### 3.3.7 And

The original 'GracePeriodStartDate' is NOT updated or extended.

### 3.3.8 Validation Notes

Check the database to ensure the timestamp for the grace period start has not changed after a subsequent failure.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Grace Period state persists after a system restart

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

The system is in the 'Grace Period' state

### 3.4.5 When

The Windows service is stopped and then started again

### 3.4.6 Then

The system correctly re-initializes into the 'Grace Period' state.

### 3.4.7 And

The system correctly retains the original 'GracePeriodStartDate'.

### 3.4.8 Validation Notes

Set the system to Grace Period, restart the service, and query the system's state via an API or log file to confirm it has resumed the correct state and timer.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Grace Period expiration leads to Trial Mode

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

The system is in the 'Grace Period' state

### 3.5.5 When

The current UTC time is more than 7 days past the recorded 'GracePeriodStartDate'

### 3.5.6 Then

The system's internal state is automatically updated to 'Trial Mode'.

### 3.5.7 And

All Trial Mode limitations are immediately enforced (as per US-010, US-011, US-012, US-013).

### 3.5.8 Validation Notes

This must be tested by either mocking the system clock or setting the 'GracePeriodStartDate' in the database to more than 7 days in the past and triggering a state check.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- No direct UI elements are created in this story. However, it provides the backend state ('Grace Period', days remaining) necessary for US-014 to display its notification banner.

## 4.2.0 User Interactions

- The user does not directly interact with this feature; it is a system-automated process.

## 4.3.0 Display Requirements

- The system must expose its current license state (Active, Grace Period, Trial) and the grace period expiry date via an API endpoint for the frontend to consume.

## 4.4.0 Accessibility Needs

- Not applicable for this backend-focused story.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

The Grace Period duration is fixed at 7 days (168 hours).

### 5.1.3 Enforcement Point

During the system's state check that runs on service start and periodically.

### 5.1.4 Violation Handling

If the duration has elapsed, the system state transitions to 'Trial Mode'.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The Grace Period timer does not reset upon subsequent validation failures.

### 5.2.3 Enforcement Point

During the license validation failure logic.

### 5.2.4 Violation Handling

The system ignores the failure if already in a grace period, allowing the original timer to continue.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-006

#### 6.1.1.2 Dependency Reason

The system must be able to have an 'Active' license state before a validation can fail.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-015

#### 6.1.2.2 Dependency Reason

This story defines the periodic validation mechanism that triggers the entry into the Grace Period.

## 6.2.0.0 Technical Dependencies

- A persistent data store (SQLite) for storing license state and grace period start time.
- The job scheduling component (Quartz.NET) to run the periodic check.
- A secure method for storing the license state to prevent tampering (e.g., database encryption).

## 6.3.0.0 Data Dependencies

- The system must have access to its own configuration database to read and write the license status.

## 6.4.0.0 External Dependencies

- The cloud-based licensing service, which must be reachable for validation attempts.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The state transition logic should execute in under 50ms and have no noticeable impact on the system's overall performance.

## 7.2.0.0 Security

- The license state and grace period start date must be stored persistently and securely, protected against unauthorized modification (covered by database encryption at rest).

## 7.3.0.0 Usability

- The system should behave predictably, providing a seamless experience during the grace period without any loss of functionality.

## 7.4.0.0 Accessibility

- Not applicable.

## 7.5.0.0 Compatibility

- Not applicable.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires implementing a robust state machine for license status (e.g., Active, GracePeriod, TrialMode).
- State and timestamp must be persisted in the database and handled correctly across service restarts.
- Time calculations must be handled carefully, using UTC to avoid timezone-related issues.
- Requires schema modification to the configuration database to store the new state and timestamp.

## 8.3.0.0 Technical Risks

- Incorrectly handling time zones or clock drift could lead to the grace period ending prematurely or late.
- Failure to persist the state correctly could cause the system to revert to Trial Mode immediately after a restart.

## 8.4.0.0 Integration Points

- The job scheduler (Quartz.NET) which triggers the validation.
- The configuration database (SQLite) for state persistence.
- A new or existing API endpoint to expose the current license status to the frontend.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify correct state transition from Active to Grace Period on failure.
- Verify correct state transition from Grace Period back to Active on success.
- Verify correct state transition from Grace Period to Trial Mode on expiration.
- Verify state persistence after the service is restarted.
- Verify the grace period timer is not extended by subsequent failures.

## 9.3.0.0 Test Data Needs

- A valid license key for activation.
- Ability to modify the 'GracePeriodStartDate' in the test database to simulate time passing.

## 9.4.0.0 Testing Tools

- xUnit for unit tests.
- Moq for mocking the external licensing service and system clock.
- A test harness for restarting the service during integration tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for the state machine logic with >80% coverage
- Integration testing completed to verify database persistence and restart behavior
- User interface reviewed and approved
- Performance requirements verified
- Security requirements validated
- Documentation updated to reflect the Grace Period behavior
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a core part of the licensing logic. It should be developed alongside US-014 (UI Notification) and US-017 (Revert to Trial) as they are tightly coupled.
- Requires coordination with the frontend developer to define the API for exposing the license state.

## 11.4.0.0 Release Impact

Critical for a positive user experience and system reliability. Prevents support issues related to temporary network outages.

