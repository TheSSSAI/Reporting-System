# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-013 |
| Elaboration Date | 2025-01-17 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Be blocked from using custom connectors in Trial M... |
| As A User Story | As an Administrator, I want the use of custom-deve... |
| User Persona | Administrator |
| Business Value | Clearly delineates the features of the trial versu... |
| Functional Area | Licensing and System Configuration |
| Story Theme | System Licensing & Activation |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

UI prevents selection of custom connectors in Trial Mode

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

The system is operating in Trial Mode and a custom connector DLL has been deployed

### 3.1.5 When

An Administrator navigates to the 'Create New Connector' page in the Control Panel

### 3.1.6 Then

The list of available connector types does not include the custom connector.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Existing custom connector configurations are disabled upon reverting to Trial Mode

### 3.2.3 Scenario Type

Edge_Case

### 3.2.4 Given

The system was in an 'Active' state and has a configured connector using a custom type

### 3.2.5 When

The system's license expires and it reverts to Trial Mode

### 3.2.6 Then

The existing custom connector configuration is visually disabled (e.g., greyed out) in the connectors list

### 3.2.7 Validation Notes

The UI should also display a tooltip or icon explaining that the connector is disabled due to the system being in Trial Mode.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Report jobs using custom connectors are blocked from execution in Trial Mode

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

The system is in Trial Mode and a report is configured to use a custom connector

### 3.3.5 When

The report job is triggered, either manually or by its schedule

### 3.3.6 Then

The job execution immediately fails with a 'Failed' status

### 3.3.7 Validation Notes

The job execution log must contain a clear error message, such as 'Job failed: Custom connectors are a licensed feature. Please activate the system to run this report.'

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Custom connectors become available after activating a license

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

The system is in Trial Mode, and a custom connector configuration is disabled

### 3.4.5 When

An Administrator successfully applies a valid license key, transitioning the system to the 'Active' state

### 3.4.6 Then

The previously disabled custom connector configuration becomes active and usable

### 3.4.7 Validation Notes

The custom connector type should also become available for selection when creating new connectors.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

API endpoints enforce trial mode restrictions for custom connectors

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

The system is in Trial Mode

### 3.5.5 When

An API client requests the list of available connector types

### 3.5.6 Then

The API response payload does not include any custom connector types.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Visual indicator (e.g., icon, greyed-out style) on the connector list for disabled connectors.
- Tooltip on disabled connectors explaining the reason ('Disabled in Trial Mode').

## 4.2.0 User Interactions

- Custom connector types are not present in the dropdown when creating a new connector.
- The 'Run' button for a report using a custom connector might be disabled, or clicking it should produce an immediate UI notification about the trial limitation.

## 4.3.0 Display Requirements

- The system must provide clear, non-technical feedback to the user explaining why a feature is unavailable.
- Error messages related to this restriction must be consistent across the UI and job logs.

## 4.4.0 Accessibility Needs

- Any visual-only indicators (like color changes) must have a text-based alternative (e.g., `aria-label`, tooltip) for screen readers, compliant with WCAG 2.1 AA.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'The use of custom-developed data connectors is a premium feature, only available with a valid, active license.', 'enforcement_point': 'Enforced at the API level when fetching connector types, at the job scheduler before job execution, and reflected in the Control Panel UI.', 'violation_handling': 'The action (creating connector, running report) is blocked, and an informative error message is presented to the user.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-006

#### 6.1.1.2 Dependency Reason

The core system activation and license state management must be implemented to determine if the system is in Trial Mode.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-017

#### 6.1.2.2 Dependency Reason

The logic for the system to revert to Trial Mode after a grace period must exist to test the transition scenario.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-041

#### 6.1.3.2 Dependency Reason

The system must be able to discover and load custom connector DLLs before it can restrict their use.

## 6.2.0.0 Technical Dependencies

- A centralized `LicensingService` that can be queried by other components to get the current license status (Trial, Active, Grace Period).
- The `ConnectorDiscoveryService` must be able to differentiate between built-in and custom connectors.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The license check must be highly performant (<5ms) and not introduce any noticeable delay to API responses or job start times.

## 7.2.0.0 Security

- The enforcement of this restriction must be handled on the backend. The client-side UI changes are for user experience only and should not be the sole enforcement mechanism.

## 7.3.0.0 Usability

- The system should clearly and proactively inform the user about the limitation, rather than letting them configure something that will fail later without explanation.

## 7.4.0.0 Accessibility

- All UI elements indicating a disabled state must be accessible to users with disabilities, adhering to WCAG 2.1 Level AA.

## 7.5.0.0 Compatibility

*No items available*

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires changes in multiple system areas: API controllers, core job execution engine (Quartz.NET), and frontend components.
- Logic must correctly handle state transitions between 'Active' and 'Trial' modes, disabling/enabling features dynamically.
- The connector discovery mechanism needs to be enhanced to tag connectors as 'custom' vs 'built-in'.

## 8.3.0.0 Technical Risks

- Risk of inconsistent enforcement if the license check is not applied in all relevant code paths (e.g., API vs. scheduler).
- Potential for a poor user experience if an Administrator loses access to configured connectors after a license lapse without clear warnings.

## 8.4.0.0 Integration Points

- Licensing Service: To check the current license status.
- Connector Configuration API: To filter the list of available connector types.
- Job Scheduler (Quartz.NET): To perform a pre-flight check before executing a job.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify custom connectors are hidden in the UI when in Trial Mode.
- Verify jobs with custom connectors fail to run in Trial Mode.
- Verify that transitioning from Active to Trial mode correctly disables existing custom connector configurations.
- Verify that transitioning from Trial to Active mode correctly re-enables custom connectors and configurations.
- Verify API endpoints correctly filter responses based on license state.

## 9.3.0.0 Test Data Needs

- A sample custom connector DLL for deployment.
- System configurations representing both built-in and custom connectors.
- License keys (or a mock service) to simulate Trial, Active, and expired states.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- Postman or an equivalent tool for API integration testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >80% coverage for new logic
- Integration testing completed successfully for API and job scheduler changes
- User interface reviewed and approved for clarity and accessibility
- Performance requirements verified
- Security requirements validated (backend enforcement)
- Documentation updated to reflect that custom connectors are a licensed feature
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is dependent on the completion of core licensing and custom connector discovery features.
- Requires coordination between backend and frontend developers to ensure the API contract and UI behavior are aligned.

## 11.4.0.0 Release Impact

- This is a critical feature for the product's monetization and go-to-market strategy. It must be included in any release that offers a trial version.

