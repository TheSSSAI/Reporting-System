# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-012 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Be limited to 3 active connectors in Trial Mode |
| As A User Story | As an Administrator evaluating the software in Tri... |
| User Persona | Administrator |
| Business Value | Enforces trial limitations to encourage license up... |
| Functional Area | System Licensing & Data Ingestion |
| Story Theme | Licensing and Trial Mode Enforcement |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Attempting to create a 4th connector when the limit is reached

### 3.1.3 Scenario Type

Error_Condition

### 3.1.4 Given

The system is operating in Trial Mode and there are already 3 data connectors configured

### 3.1.5 When

The Administrator attempts to save a new, 4th connector configuration

### 3.1.6 Then

The system must prevent the connector from being saved, and the API must return an HTTP 403 Forbidden status with a clear error message.

### 3.1.7 Validation Notes

Verify via API call and UI interaction. The UI should display a user-friendly error message like 'Trial Mode Limit Reached: You can only configure a maximum of 3 data connectors. Please delete an existing connector or upgrade your license.'

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Creating connectors when below the trial limit

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The system is operating in Trial Mode and there are 2 data connectors configured

### 3.2.5 When

The Administrator creates and saves a new connector configuration

### 3.2.6 Then

The new connector is saved successfully, and the total count of configured connectors becomes 3.

### 3.2.7 Validation Notes

Verify that the connector appears in the list and the total count is updated.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

UI state when the connector limit is reached in Trial Mode

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

The system is operating in Trial Mode and there are 3 data connectors configured

### 3.3.5 When

The Administrator navigates to the connector management page

### 3.3.6 Then

The 'Add New Connector' button must be disabled, and a tooltip on the button must explain the trial limitation.

### 3.3.7 Validation Notes

Inspect the UI to confirm the button is in a disabled state and the tooltip text is present and informative (e.g., 'Upgrade to add more connectors. Trial is limited to 3.').

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Creating a new connector after deleting one at the limit

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

The system is in Trial Mode with 3 connectors configured

### 3.4.5 When

The Administrator deletes one of the existing connectors, reducing the count to 2

### 3.4.6 Then

The 'Add New Connector' button becomes enabled, and the Administrator is able to create and save one more connector.

### 3.4.7 Validation Notes

Perform the delete action, verify the UI updates, and then successfully create a new connector.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

System transitions from Active to Trial Mode with more than 3 connectors

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

The system is in an 'Active' licensed state and has 5 connectors configured

### 3.5.5 When

The license expires and the system reverts to Trial Mode

### 3.5.6 Then

All 5 existing connectors remain functional, but the Administrator is prevented from creating any new connectors until the count is below 3.

### 3.5.7 Validation Notes

Simulate license expiration. Verify all 5 connectors are listed and the 'Add New Connector' button is disabled.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

System transitions from Trial to Active Mode

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

The system is in Trial Mode with 3 connectors configured

### 3.6.5 When

The Administrator successfully activates the system with a valid license key

### 3.6.6 Then

The 3-connector limit is immediately removed, the 'Add New Connector' button is enabled, and the Administrator can create a 4th connector.

### 3.6.7 Validation Notes

Activate the product and verify that the UI restriction is lifted and a new connector can be created.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Backend API enforces the connector limit

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

The system is in Trial Mode with 3 connectors configured

### 3.7.5 When

A user makes a direct API call to the `POST /api/v1/connectors` endpoint to create a 4th connector

### 3.7.6 Then

The API must reject the request with an HTTP 403 Forbidden response and a JSON body containing the error message.

### 3.7.7 Validation Notes

Use an API client like Postman or curl to test this. This ensures the limitation cannot be bypassed by the client-side UI.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A disabled 'Add New Connector' button on the connector list page when the limit is reached.
- A tooltip for the disabled button.
- A non-modal notification (toast) for displaying the error message upon a failed save attempt.

## 4.2.0 User Interactions

- The system should prevent the user from navigating to the 'create connector' form if the limit is already met, ideally by disabling the entry point button.
- If the user accesses the creation form via a direct URL while at the limit, the 'Save' button should be disabled.

## 4.3.0 Display Requirements

- The error message must clearly state that the limit is due to Trial Mode and mention the limit of 3 connectors.
- The tooltip on the disabled button should concisely explain why the action is unavailable.

## 4.4.0 Accessibility Needs

- Disabled buttons must use `aria-disabled='true'` to be properly identified by screen readers.
- Error messages displayed in notifications must be announced to screen readers using ARIA live regions.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

In Trial Mode, the total number of configured data connectors cannot exceed 3.

### 5.1.3 Enforcement Point

Backend API endpoint for creating connectors (`POST /api/v1/connectors`).

### 5.1.4 Violation Handling

The creation request is rejected with an HTTP 403 Forbidden error and a descriptive message.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

This limit applies to the creation of new connectors only; it does not disable or delete existing connectors if the system reverts to Trial Mode with more than 3 already configured.

### 5.2.3 Enforcement Point

Business logic within the Connector Service.

### 5.2.4 Violation Handling

N/A - This rule defines behavior, not a violation.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-017

#### 6.1.1.2 Dependency Reason

The system must have a defined 'Trial Mode' state that can be queried before this limitation can be enforced.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-034

#### 6.1.2.2 Dependency Reason

The basic functionality to create a database connector must exist to apply this limitation.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-035

#### 6.1.3.2 Dependency Reason

The basic functionality to create a file system connector must exist to apply this limitation.

## 6.2.0.0 Technical Dependencies

- A centralized Licensing Service/Module that provides the current license state (e.g., 'Trial', 'Active', 'GracePeriod').
- The ASP.NET Core backend API for connector management.
- The React frontend state management (Zustand) to hold the current license status for UI updates.

## 6.3.0.0 Data Dependencies

- Access to the SQLite database to query the current count of records in the `ConnectorConfiguration` table.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The check for the connector count must execute in under 50ms to avoid adding any perceptible delay to API responses or UI rendering.

## 7.2.0.0 Security

- The enforcement of the 3-connector limit must be implemented on the server-side (backend API) and cannot be bypassed by manipulating the client-side UI.

## 7.3.0.0 Usability

- The user must receive clear, immediate, and non-intrusive feedback when they encounter the trial limitation.

## 7.4.0.0 Accessibility

- All UI changes related to this limitation (disabled buttons, tooltips, error messages) must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The UI behavior must be consistent across all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires changes in both frontend and backend.
- Backend logic involves a simple conditional check: query license state, then query connector count.
- Frontend logic involves fetching license state and conditionally rendering UI elements.
- Error handling needs to be implemented consistently between the API and the UI.

## 8.3.0.0 Technical Risks

- Risk of inconsistent state between UI and backend if the license status is not managed properly in the frontend's global state.

## 8.4.0.0 Integration Points

- The Connector Service must integrate with the Licensing Service.
- The Connector creation API controller must handle the specific exception thrown by the service and return the correct HTTP status code.
- The React Connector List component must consume the license state from the global store.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify connector creation is blocked at the API level when in Trial Mode with 3 connectors.
- Verify the 'Add New' button is disabled in the UI when in Trial Mode with 3 connectors.
- Verify the limit is removed upon successful activation.
- Verify that deleting a connector while at the limit re-enables the creation functionality.
- Verify that reverting to Trial Mode from Active with >3 connectors correctly disables new connector creation.

## 9.3.0.0 Test Data Needs

- A system state that can be programmatically set to 'Trial Mode' and 'Active Mode'.
- A database pre-populated with 0, 2, and 3 connectors for different test runs.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- An in-memory SQLite provider for backend integration tests.
- A UI E2E testing framework like Playwright or Cypress.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for both backend service logic and frontend components, achieving >80% coverage
- API integration test completed successfully, verifying the HTTP 403 response
- E2E test scenario for hitting the limit and for the limit being removed is implemented and passing
- User interface reviewed and approved by a UX designer or product owner
- Performance requirements verified
- Security requirements validated (server-side enforcement)
- Documentation updated to mention the 3-connector limit in the Trial Mode section of the user guide
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a dependency for the overall 'Trial Experience' epic.
- Should be worked on after the core licensing state mechanism is implemented.

## 11.4.0.0 Release Impact

- Critical for the initial public release as it directly relates to the monetization model.

