# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-061 |
| Elaboration Date | 2024-05-22 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Prevent saving report schedules that run more freq... |
| As A User Story | As an Administrator, I want to be prevented from s... |
| User Persona | Administrator |
| Business Value | Enhances system stability and reliability by preve... |
| Functional Area | Report Configuration & Scheduling |
| Story Theme | System Stability and Resource Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Saving a valid schedule with a one-minute frequency

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

An Administrator is on the report configuration page, defining a schedule

### 3.1.5 When

The Administrator enters a valid CRON expression for a one-minute frequency (e.g., '* * * * *')

### 3.1.6 Then

The system accepts the expression without error and allows the report configuration to be saved successfully.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Saving a valid schedule with a frequency longer than one minute

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

An Administrator is on the report configuration page, defining a schedule

### 3.2.5 When

The Administrator enters a valid CRON expression for a frequency longer than one minute (e.g., '0 * * * *' for hourly)

### 3.2.6 Then

The system accepts the expression without error and allows the report configuration to be saved successfully.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempting to save an invalid schedule with a sub-minute frequency

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

An Administrator is on the report configuration page, defining a schedule

### 3.3.5 When

The Administrator enters a CRON expression for a sub-minute frequency (e.g., '*/30 * * * * *' for every 30 seconds) and attempts to save

### 3.3.6 Then

The system prevents the configuration from being saved.

### 3.3.7 And

A clear, user-friendly error message is displayed next to the schedule input field, stating 'Schedule frequency cannot be less than one minute.'

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

CRON UI builder prevents creation of sub-minute schedules

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

An Administrator is using the CRON UI builder to create a schedule

### 3.4.5 When

The Administrator navigates the UI builder's options

### 3.4.6 Then

The UI builder does not present any options that would allow the creation of a schedule with a frequency of less than one minute (e.g., the 'seconds' field is not available or is constrained).

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Server-side API validation rejects sub-minute schedules

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

An authenticated API user is creating or updating a report configuration

### 3.5.5 When

The user sends a POST or PUT request to the '/api/v1/reports' endpoint with a schedule payload containing a CRON expression more frequent than once per minute

### 3.5.6 Then

The API responds with an HTTP 400 (Bad Request) status code.

### 3.5.7 And

The response body contains a JSON object with an error message explaining that the schedule frequency is invalid.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Validation applies when editing an existing report schedule

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

An Administrator is editing an existing report configuration with a valid schedule

### 3.6.5 When

The Administrator changes the CRON expression to one with a sub-minute frequency and attempts to save

### 3.6.6 Then

The system prevents the configuration from being saved and displays the validation error message.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- An inline error message container associated with the CRON expression input field.

## 4.2.0 User Interactions

- Validation should ideally trigger on-blur of the input field to provide immediate feedback.
- The 'Save' button for the report configuration should be disabled if the CRON expression is invalid.
- If the 'Save' button is clicked with invalid input, it must trigger the validation and display the error without submitting the form.

## 4.3.0 Display Requirements

- The error message must explicitly state the nature of the violation (e.g., 'Schedule frequency cannot be less than one minute').

## 4.4.0 Accessibility Needs

- The error message must be programmatically linked to the input field using 'aria-describedby' to be accessible to screen readers.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'The minimum allowed frequency for any scheduled report job is once per minute.', 'enforcement_point': 'This rule is enforced on both the client-side (UI) during configuration and on the server-side (API) upon submission of any report configuration create/update request.', 'violation_handling': 'A violation prevents the saving of the report configuration and results in a user-facing error message in the UI or a 400-level error response from the API.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-058

#### 6.1.1.2 Dependency Reason

This story implements the validation logic for the scheduling feature defined in US-058. The UI and backend for setting a CRON schedule must exist first.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-051

#### 6.1.2.2 Dependency Reason

The validation logic must be integrated into the report creation/editing workflow established by US-051.

## 6.2.0.0 Technical Dependencies

- A backend CRON parsing library (e.g., Quartz.NET, Cronos) capable of calculating the next scheduled occurrences from an expression.
- A frontend form validation library or custom hook in React to handle real-time input validation.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The CRON validation logic, both on the client and server, must execute in under 50ms to avoid any perceptible lag in the user interface.

## 7.2.0.0 Security

- Server-side validation is mandatory to prevent circumvention of the UI rule via direct API calls. The system must not trust client-side validation alone.

## 7.3.0.0 Usability

- Feedback for invalid input must be immediate and clear, guiding the user to correct their mistake without confusion.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA, particularly concerning form validation and error feedback.

## 7.5.0.0 Compatibility

- The validation logic must function correctly in all supported browsers (Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires implementation of validation logic in two places: frontend (React) and backend (ASP.NET Core).
- Relies on a robust third-party library to parse CRON expressions and calculate the interval between job runs. The core logic involves getting the next 2-3 run times and ensuring the delta is >= 60 seconds.

## 8.3.0.0 Technical Risks

- The CRON parsing library might not handle all edge-case expressions correctly. The chosen library must be thoroughly vetted.
- Ensuring consistent validation behavior between the client-side JavaScript implementation and the server-side C# implementation.

## 8.4.0.0 Integration Points

- React component for report scheduling.
- ASP.NET Core API controller action for creating/updating report configurations.
- CRON UI builder component (from US-059).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Test with a wide range of valid CRON expressions (every minute, every 5 minutes, hourly, daily).
- Test with a variety of invalid, sub-minute CRON expressions (e.g., '*/10 * * * * *', '0,30 * * * * *').
- Test API endpoint directly with both valid and invalid payloads.
- Test the full UI flow: enter invalid CRON, see error, correct it, save successfully.
- Test both creating a new report and editing an existing one.

## 9.3.0.0 Test Data Needs

- A list of valid and invalid CRON expressions for automated tests.

## 9.4.0.0 Testing Tools

- xUnit/Moq for backend unit tests.
- Jest/React Testing Library for frontend unit tests.
- An E2E testing framework like Playwright or Cypress.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests for both frontend and backend validation logic implemented and passing with sufficient coverage
- Integration testing for the API endpoint completed successfully
- User interface reviewed and approved for clarity of error messaging
- Security requirement for mandatory server-side validation is met
- Documentation for the scheduling feature is updated to mention this limitation
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

2

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a critical system guardrail and should be prioritized shortly after the basic scheduling feature (US-058) is complete.
- The developer should have access to a robust CRON parsing library.

## 11.4.0.0 Release Impact

- Improves the overall stability and robustness of the product, reducing the risk of customer-induced performance issues.

