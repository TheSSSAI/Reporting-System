# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-060 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Receive a validation error for an invalid CRON exp... |
| As A User Story | As an Administrator, I want the system to validate... |
| User Persona | Administrator |
| Business Value | Ensures the reliability of scheduled reports by pr... |
| Functional Area | Report Configuration & Scheduling |
| Story Theme | System Configuration Integrity |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Saving with a valid 6-part CRON expression

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

an Administrator is on the report configuration page and has entered all required information

### 3.1.5 When

they enter a syntactically valid 6-part CRON expression (e.g., '0 15 10 * * ?') into the schedule field and click 'Save'

### 3.1.6 Then

the system saves the report configuration successfully and no validation error related to the CRON expression is displayed.

### 3.1.7 Validation Notes

Verify that the configuration is persisted in the database with the correct CRON string.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Saving with a valid 7-part CRON expression (with year)

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

an Administrator is on the report configuration page and has entered all required information

### 3.2.5 When

they enter a syntactically valid 7-part CRON expression (e.g., '0 15 10 * * ? 2025') into the schedule field and click 'Save'

### 3.2.6 Then

the system saves the report configuration successfully.

### 3.2.7 Validation Notes

Verify that the configuration is persisted in the database. The system should support standard Quartz.NET CRON format.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempting to save with a syntactically invalid CRON expression (out-of-range value)

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

an Administrator is on the report configuration page

### 3.3.5 When

they enter a CRON expression with an out-of-range value (e.g., '0 0 25 * * ?') and click 'Save'

### 3.3.6 Then

the system prevents the configuration from being saved.

### 3.3.7 And

a clear, user-friendly error message is displayed adjacent to the input field, such as 'Invalid CRON expression: Hour value must be between 0 and 23.'

### 3.3.8 Validation Notes

Check that no new record is created in the database and the UI displays the specified error message.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Attempting to save with a CRON expression having the wrong number of fields

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

an Administrator is on the report configuration page

### 3.4.5 When

they enter a CRON expression with too few fields (e.g., '0 15 10 * *') and click 'Save'

### 3.4.6 Then

the system prevents the configuration from being saved.

### 3.4.7 And

an error message is displayed, such as 'Invalid CRON expression: Expression must contain 6 or 7 fields.'

### 3.4.8 Validation Notes

Test with both fewer than 6 and more than 7 fields.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Attempting to save with an empty schedule field

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

an Administrator is on the report configuration page and scheduling is enabled

### 3.5.5 When

they leave the CRON expression field empty and click 'Save'

### 3.5.6 Then

the system prevents the configuration from being saved.

### 3.5.7 And

a validation error is displayed indicating the field is required, such as 'Schedule expression is required.'

### 3.5.8 Validation Notes

This assumes scheduling is an enabled feature for the report. If scheduling is optional, this may not apply.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Error message provides real-time feedback (on blur)

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

an Administrator is editing the CRON expression field on the report configuration page

### 3.6.5 When

they type an invalid expression and then click or tab out of the field (on blur)

### 3.6.6 Then

the validation error message appears immediately, without waiting for the 'Save' action.

### 3.6.7 And

the invalid input remains in the field, allowing the user to correct it.

### 3.6.8 Validation Notes

This provides a better user experience than waiting for the final save attempt.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A text input field for the CRON expression.
- A dedicated area for displaying validation error messages, positioned directly below or beside the input field.
- Optionally, a link to a help document or tooltip explaining CRON syntax.

## 4.2.0 User Interactions

- When the user attempts to save with an invalid CRON string, the form submission is halted.
- The input field with the invalid CRON expression should be visually highlighted (e.g., with a red border).
- The invalid text entered by the user must be preserved in the input field to allow for easy correction.

## 4.3.0 Display Requirements

- Error messages must be specific and helpful, guiding the user to the source of the error (e.g., 'Invalid value for Day-of-Week').
- The error message must be cleared once the user corrects the input to be valid.

## 4.4.0 Accessibility Needs

- The error message must be programmatically associated with the input field using `aria-describedby` to be accessible to screen readers.
- The color used for error text and field highlighting must have a contrast ratio of at least 4.5:1 against the background.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'A report configuration with an enabled schedule must have a syntactically valid CRON expression.', 'enforcement_point': 'Client-side upon field blur and form submission; Server-side upon receiving the save request.', 'violation_handling': 'The save operation is rejected and a specific error message is returned to the user.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-051

#### 6.1.1.2 Dependency Reason

Provides the report configuration wizard/form where the schedule field will be located.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-058

#### 6.1.2.2 Dependency Reason

Defines the requirement for the CRON expression input field itself, which this story validates.

## 6.2.0.0 Technical Dependencies

- A frontend CRON validation library (e.g., 'cron-parser', 'cron-validator') for real-time feedback.
- Backend validation using the built-in capabilities of the Quartz.NET library to ensure data integrity.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- Client-side validation should execute in under 50ms to provide instantaneous feedback.
- Server-side validation as part of the form submission API call should not add more than 100ms to the total response time.

## 7.2.0.0 Security

- All user input, including the CRON string, must be treated as untrusted and properly handled on the server to prevent any form of injection attacks, even if the risk is minimal for this specific field.

## 7.3.0.0 Usability

- Error messages should be clear and avoid technical jargon where possible.
- The system should provide immediate feedback on the validity of the expression to reduce user frustration.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards, particularly for form validation and error feedback.

## 7.5.0.0 Compatibility

- Validation logic must work consistently across all supported browsers (latest stable Chrome, Firefox, Edge).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires selection and integration of a third-party library for both frontend and backend.
- Logic must handle both client-side (for UX) and server-side (for security/integrity) validation.
- Mapping technical validation errors from the library to user-friendly messages.

## 8.3.0.0 Technical Risks

- The chosen frontend and backend validation libraries might have slight differences in their interpretation of the CRON standard. Using Quartz.NET's logic as the source of truth is recommended.

## 8.4.0.0 Integration Points

- Frontend: React form component state management.
- Backend: ASP.NET Core model validation pipeline within the ReportConfiguration save endpoint.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Test with a comprehensive set of valid CRON expressions, including all special characters (?, *, L, W, #).
- Test with a set of known invalid expressions: out-of-range values, invalid characters, incorrect number of parameters.
- Test the full user flow: enter invalid data, see error, correct data, save successfully.
- Verify accessibility using a screen reader to ensure error messages are announced correctly.

## 9.3.0.0 Test Data Needs

- A list of at least 10 valid and 10 invalid CRON expressions covering various edge cases.

## 9.4.0.0 Testing Tools

- Jest/React Testing Library for frontend unit tests.
- xUnit for backend unit tests.
- Playwright or Cypress for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests for both client-side and server-side validation logic are implemented and passing with >80% coverage
- Integration testing of the report configuration save endpoint completed successfully
- User interface reviewed and approved for clarity and usability of error messages
- Performance requirements verified
- Security requirements validated
- Accessibility checks (automated and manual) have been performed and passed
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

2

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a critical validation story that should be implemented alongside the feature for adding a schedule. It prevents a major source of user error and subsequent support issues.

## 11.4.0.0 Release Impact

- Improves the stability and user-friendliness of the report scheduling feature.

