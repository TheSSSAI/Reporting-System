# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-015 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | System automatically re-validates the license peri... |
| As A User Story | As an Administrator, I want the system to automati... |
| User Persona | Administrator |
| Business Value | Ensures continuous license compliance, protects re... |
| Functional Area | System Licensing & Activation |
| Story Theme | Licensing and Compliance |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful periodic re-validation of an active license

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

The system is in an 'Active' state with a valid license key and the periodic validation interval is set to 30 days

### 3.1.5 When

30 days have passed since the last successful validation

### 3.1.6 Then

The system automatically initiates a secure connection to the cloud licensing service to re-validate the key.

### 3.1.7 And

The action is logged at an 'Information' level, and no UI notification is shown to the user.

### 3.1.8 Validation Notes

Verify system logs for the validation attempt and success. Confirm the next scheduled job's timestamp in the Quartz.NET tables. The system state should remain 'Active'.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Periodic validation fails due to an invalid or expired key

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

The system is in an 'Active' state

### 3.2.5 When

The periodic validation check is triggered and the cloud service responds that the key is now invalid (e.g., subscription expired)

### 3.2.6 Then

The system's state transitions to 'Grace Period'.

### 3.2.7 And

The failure and state transition are logged as a 'Warning'.

### 3.2.8 Validation Notes

Verify the system's internal state has changed to 'Grace Period'. Check logs for the validation failure reason. This AC triggers the behavior defined in US-016.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Periodic validation fails due to a persistent network error

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

The system is in an 'Active' state and cannot connect to the cloud licensing service

### 3.3.5 When

The periodic validation check is triggered

### 3.3.6 Then

The system attempts the connection using a retry policy (e.g., 3 retries with exponential backoff).

### 3.3.7 And

The persistent connection failure is logged as an 'Error' with relevant details.

### 3.3.8 Validation Notes

Simulate network failure (e.g., using a mock HTTP client or firewall rule). Verify that retries are attempted and that the system enters the Grace Period only after all retries are exhausted.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Periodic validation succeeds while in the Grace Period

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

The system is in a 'Grace Period' state due to a previous validation failure

### 3.4.5 When

A periodic validation check is triggered and the cloud service now confirms the key is valid (e.g., issue was resolved)

### 3.4.6 Then

The system's state transitions back to 'Active'.

### 3.4.7 And

The successful validation and state change are logged as 'Information'.

### 3.4.8 Validation Notes

Set the system state to 'Grace Period', then simulate a successful API response. Verify the state transitions back to 'Active' and any UI notifications for the grace period are removed (as per US-014).

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Scheduled validation job persists across service restarts

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

The system is in an 'Active' state and a periodic validation is scheduled to run in 2 hours

### 3.5.5 When

The Windows service is restarted

### 3.5.6 Then

The scheduled validation job is preserved and will still trigger at its originally scheduled time.

### 3.5.7 Validation Notes

Check the Quartz.NET persistent job store (e.g., in the SQLite database) to confirm the job and its trigger time are present after a service restart.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- None. This is a background process.

## 4.2.0 User Interactions

- None. The process is fully automated.

## 4.3.0 Display Requirements

- This story triggers display requirements in other stories (US-014 for Grace Period notifications, US-009 for Trial Mode indicators) but has no direct display requirements itself.

## 4.4.0 Accessibility Needs

- Not applicable.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

The periodic license validation interval shall be 30 days.

### 5.1.3 Enforcement Point

Job Scheduler Configuration

### 5.1.4 Violation Handling

The system must use the default 30-day interval. This value should be configurable in system settings for future flexibility.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A single validation failure (after retries) must trigger the 7-day Grace Period, not an immediate reversion to Trial Mode.

### 5.2.3 Enforcement Point

License Validation Service Logic

### 5.2.4 Violation Handling

The system state must transition to 'Grace Period'. A direct transition from 'Active' to 'Trial Mode' due to a periodic check is not allowed.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-006

#### 6.1.1.2 Dependency Reason

The system must be able to be activated and store a license key before that key can be periodically re-validated.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-016

#### 6.1.2.2 Dependency Reason

This story provides the trigger for entering the Grace Period. US-016 implements the actual state transition and its effects.

## 6.2.0.0 Technical Dependencies

- Job Scheduling Framework (Quartz.NET)
- Resiliency Framework (Polly)
- A system-level state management service to track license status ('Active', 'Grace Period', 'Trial Mode').
- Secure HTTP client for communication.

## 6.3.0.0 Data Dependencies

- Access to the currently stored active license key.

## 6.4.0.0 External Dependencies

- Availability of the cloud-based licensing service API endpoint for validation.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The validation process must be a low-overhead background task, consuming minimal CPU and memory, and must not impact the performance of report generation or UI responsiveness.

## 7.2.0.0 Security

- All communication with the cloud licensing service must use HTTPS with TLS 1.2+.
- The license key must be transmitted securely in the request body or headers, not as a URL parameter.

## 7.3.0.0 Usability

- The process must be completely transparent to the user unless a failure occurs, at which point clear notifications (handled by other stories) should be displayed.

## 7.4.0.0 Accessibility

- Not applicable.

## 7.5.0.0 Compatibility

- The HTTP client must be compatible with the security protocols required by the cloud licensing service.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Integration with a persistent job scheduler (Quartz.NET).
- Implementation of a resilient state machine for license status (Active -> Grace Period -> Trial).
- Requires robust error handling and logging for network failures and invalid responses.
- Ensuring the scheduled job is correctly re-established after a service restart.

## 8.3.0.0 Technical Risks

- The cloud licensing service API may be unreliable, requiring robust retry and fallback logic.
- Incorrect configuration of the persistent job store could lead to missed validation checks after a service restart.

## 8.4.0.0 Integration Points

- Quartz.NET Scheduler
- Cloud Licensing Service (External API)
- Internal System State Service
- Serilog Logging Service

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration

## 9.2.0.0 Test Scenarios

- Test successful validation flow.
- Test validation failure due to invalid key.
- Test validation failure due to network errors, verifying retry logic.
- Test state transition from Active to Grace Period.
- Test state transition from Grace Period back to Active.
- Test persistence of the scheduled job across a service restart.

## 9.3.0.0 Test Data Needs

- A valid license key.
- An invalid/expired license key.
- A mock/stub of the cloud licensing service API that can be configured to return success, failure, and error responses.

## 9.4.0.0 Testing Tools

- xUnit
- Moq (for mocking dependencies like the HTTP client)
- An in-memory SQLite provider for testing scheduler persistence.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >80% coverage for the new logic
- Integration testing with a mocked external service completed successfully
- User interface reviewed and approved
- Performance requirements verified
- Security requirements validated
- Documentation updated appropriately
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a core feature for the licensing model. It should be planned in an early sprint after the initial activation story (US-006) is complete.
- Requires coordination with the team responsible for the cloud licensing service to understand the API contract.

## 11.4.0.0 Release Impact

- Essential for the commercial viability and license enforcement of the product. Cannot release without this functionality.

