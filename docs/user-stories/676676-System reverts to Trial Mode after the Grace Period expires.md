# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-017 |
| Elaboration Date | 2025-01-17 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | System reverts to Trial Mode after the Grace Perio... |
| As A User Story | As an Administrator, I want the system to automati... |
| User Persona | Administrator |
| Business Value | Ensures automated enforcement of the software lice... |
| Functional Area | System Licensing & Activation |
| Story Theme | Licensing Lifecycle Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

System automatically transitions to Trial Mode when the grace period expires during normal operation.

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

The system's license state is 'Grace Period' and the grace period expiration timestamp is set in the system's configuration database.

### 3.1.5 When

A scheduled system task runs and detects that the current system time is past the grace period expiration timestamp.

### 3.1.6 Then

The system's license state is immediately updated to 'Trial Mode' in the database.

### 3.1.7 And

Any Administrator logged into the Control Panel will see the persistent 'Trial Mode' indicator instead of the 'Grace Period' notification on the next UI refresh.

### 3.1.8 Validation Notes

Test by setting the grace period expiration to a time in the near past and triggering the scheduled check. Verify the state change in the database, the log file, and the UI.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

System transitions to Trial Mode on startup if the grace period expired while the service was offline.

### 3.2.3 Scenario Type

Edge_Case

### 3.2.4 Given

The system's license state is 'Grace Period' with an expiration timestamp that is now in the past.

### 3.2.5 And

A system log event is created with level 'Warning' stating 'Grace period expired while offline. System starting in Trial Mode.'

### 3.2.6 When

The Windows service is started.

### 3.2.7 Then

The system, during its startup sequence, detects the expired grace period.

### 3.2.8 Validation Notes

Set the grace period expiration to a past time in the database, stop the service, and then restart it. Verify the system starts in Trial Mode.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

System remains in 'Active' state if the license is validated before the grace period expires.

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

The system's license state is 'Grace Period'.

### 3.3.5 And

The system does not revert to Trial Mode when the original expiration timestamp passes.

### 3.3.6 When

An Administrator successfully activates a valid license key.

### 3.3.7 Then

The system's license state is updated to 'Active'.

### 3.3.8 Validation Notes

Trigger the grace period, then successfully activate the license. Advance the system clock past the original expiration time and verify the system state remains 'Active'.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

System state is preserved across restarts within the grace period.

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

The system's license state is 'Grace Period' with an expiration timestamp 3 days in the future.

### 3.4.5 When

The Windows service is restarted.

### 3.4.6 Then

The system correctly identifies its state as 'Grace Period' on startup.

### 3.4.7 And

The original grace period expiration timestamp is preserved and remains unchanged.

### 3.4.8 Validation Notes

Trigger the grace period, note the expiration time, restart the service, and verify the state and expiration time are correct.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- The persistent 'Trial Mode' indicator (defined in US-009).

## 4.2.0 User Interactions

- Upon transition, the 'Grace Period' notification (US-014) must be removed and replaced by the 'Trial Mode' indicator without requiring the user to log out and back in (e.g., on the next API call or page navigation).

## 4.3.0 Display Requirements

- The UI must immediately reflect the functional limitations of Trial Mode, such as disabling the 'Add New Connector' button if the limit of 3 is reached.

## 4.4.0 Accessibility Needs

- The 'Trial Mode' indicator must be accessible and clearly communicate the system's state to users of assistive technologies.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

The transition from 'Grace Period' to 'Trial Mode' is irreversible without a new, successful license activation.

### 5.1.3 Enforcement Point

Licensing state management service.

### 5.1.4 Violation Handling

The system will remain in Trial Mode. Manual re-validation or activation is the only path back to an 'Active' state.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The grace period expiration check must be tamper-resistant. The expiration date must be stored securely within the encrypted application database.

### 5.2.3 Enforcement Point

Data storage and retrieval layer.

### 5.2.4 Violation Handling

System should treat tampered data as an invalid state and default to Trial Mode.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-016

#### 6.1.1.2 Dependency Reason

The system must be able to enter a Grace Period and record its expiration time before it can expire.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-009

#### 6.1.2.2 Dependency Reason

Requires the existence of a persistent UI indicator for Trial Mode to display upon reversion.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-010

#### 6.1.3.2 Dependency Reason

Requires the report watermarking functionality to be available to be enforced.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-011

#### 6.1.4.2 Dependency Reason

Requires the schedule limit functionality to be available to be enforced.

### 6.1.5.0 Story Id

#### 6.1.5.1 Story Id

US-012

#### 6.1.5.2 Dependency Reason

Requires the connector limit functionality to be available to be enforced.

### 6.1.6.0 Story Id

#### 6.1.6.1 Story Id

US-013

#### 6.1.6.2 Dependency Reason

Requires the custom connector disabling functionality to be available to be enforced.

## 6.2.0.0 Technical Dependencies

- Job Scheduling Framework (Quartz.NET) for periodic checks.
- Centralized Licensing State Management Service to handle state transitions.
- Encrypted SQLite database for secure storage of the expiration timestamp.

## 6.3.0.0 Data Dependencies

- Requires a persisted record of the current license state and the grace period expiration timestamp.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The grace period expiration check must be a lightweight process with negligible impact on overall system performance.

## 7.2.0.0 Security

- The grace period expiration timestamp must be stored in the encrypted SQLite database to prevent tampering.

## 7.3.0.0 Usability

- The transition in the UI from 'Grace Period' to 'Trial Mode' should be clear and immediate to avoid user confusion.

## 7.4.0.0 Accessibility

- N/A

## 7.5.0.0 Compatibility

- N/A

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires a reliable, persistent scheduled task.
- Involves state management that must be consistent, especially across service restarts.
- Requires a robust check during application startup to handle the 'expired while offline' scenario.

## 8.3.0.0 Technical Risks

- If the system clock is changed significantly, it could affect the expiration check. The logic should be based on a stored UTC timestamp.
- A failure in the job scheduler could prevent the check from running. The startup check provides a fallback mitigation.

## 8.4.0.0 Integration Points

- Licensing Module: To update the system state.
- Job Scheduler (Quartz.NET): To trigger the periodic check.
- Application Startup Logic: To perform the check when the service starts.
- Logging Framework (Serilog): To record the state transition event.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify state transition from 'Grace Period' to 'Trial Mode' after time expires.
- Verify state transition on service startup if expiration occurred while offline.
- Verify state remains 'Active' if re-licensed during the grace period.
- Verify all Trial Mode limitations are correctly enforced immediately after transition.
- Verify correct log messages are generated for each transition scenario.

## 9.3.0.0 Test Data Needs

- A method to manipulate the grace period expiration timestamp in the test database.
- A mechanism to mock or control the system clock for unit and integration tests.

## 9.4.0.0 Testing Tools

- xUnit for unit tests.
- Moq for mocking dependencies like the system clock.
- A test harness to start/stop the service for E2E testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for the state transition logic and startup check, achieving >80% coverage
- Integration testing completed for the scheduled job and its interaction with the database
- User interface reviewed and approved to correctly reflect the state change
- Performance requirements verified
- Security requirements validated, ensuring expiration data is encrypted at rest
- Documentation updated to explain the Grace Period and reversion to Trial Mode behavior
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a critical part of the licensing epic and should be prioritized after the stories for entering the grace period and defining trial mode limitations are complete.

## 11.4.0.0 Release Impact

- Completes the core license enforcement lifecycle. Essential for the commercial viability of the product.

