# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-058 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Define a schedule for a report using a CRON expres... |
| As A User Story | As an Administrator, I want to define an automated... |
| User Persona | Administrator: A technical user responsible for co... |
| Business Value | Enables the core automation capability of the syst... |
| Functional Area | Report Configuration & Scheduling |
| Story Theme | Report Automation and Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successfully schedule a report with a valid CRON expression

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

An Administrator is creating or editing a report configuration in the Control Panel

### 3.1.5 When

The Administrator enters a valid 5-part CRON expression (e.g., '0 22 * * 1-5') into the 'Schedule' input field and saves the configuration

### 3.1.6 Then

The system validates the expression, saves the report configuration with the schedule, and the backend scheduling service (Quartz.NET) creates a trigger for the report job to run at 10 PM on every weekday.

### 3.1.7 Validation Notes

Verify the CRON string is saved correctly in the 'ReportConfiguration' table. Check scheduler logs or use a monitoring tool to confirm the job is scheduled with the correct trigger. The report should execute at the next scheduled time.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Attempt to save a report with an invalid CRON expression

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

An Administrator is creating or editing a report configuration

### 3.2.5 When

The Administrator enters a syntactically incorrect CRON expression (e.g., '60 * * * *', 'a b c d e', '* * *') and attempts to save

### 3.2.6 Then

The UI displays a clear, user-friendly validation error message like 'Invalid CRON expression format.' and the report configuration is not saved.

### 3.2.7 Validation Notes

This is a direct implementation of the validation requirement in US-060. Test with multiple invalid formats. The save button should be disabled or the API call should return a 400 Bad Request with a descriptive error.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Save a report with no schedule for on-demand execution

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

An Administrator is creating or editing a report configuration

### 3.3.5 When

The Administrator leaves the 'Schedule' input field empty and saves the configuration

### 3.3.6 Then

The system saves the report configuration successfully without an associated schedule, making it available for on-demand execution only.

### 3.3.7 Validation Notes

Verify the schedule field in the database for this report is NULL or empty. Confirm that no job trigger is created in the scheduler for this report. The report should be triggerable via the API or a 'Run Now' button.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Update an existing report schedule

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

A report is already configured with a daily schedule (e.g., '0 8 * * *')

### 3.4.5 When

An Administrator edits the report and changes the CRON expression to a weekly schedule (e.g., '0 9 * * 1') and saves

### 3.4.6 Then

The system updates the job in the scheduler, removing the old daily trigger and applying the new weekly trigger.

### 3.4.7 Validation Notes

Verify the scheduler no longer attempts to run the job at 8 AM daily and successfully runs it at 9 AM on the next Monday.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Remove a schedule from an existing report

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

A report is already configured with a schedule

### 3.5.5 When

An Administrator edits the report, clears the 'Schedule' input field, and saves

### 3.5.6 Then

The system removes the corresponding trigger from the scheduler, converting the report to on-demand only.

### 3.5.7 Validation Notes

Verify the scheduler no longer has a trigger for this report job. The report should not run automatically anymore but should still be available for on-demand execution.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Attempt to save a schedule that is too frequent

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

An Administrator is creating or editing a report configuration

### 3.6.5 When

The Administrator enters a CRON expression that would execute more frequently than once per minute (e.g., using a seconds field if the parser supports it, like '*/30 * * * * *')

### 3.6.6 Then

The system displays a validation error like 'Schedules cannot be more frequent than once per minute.' and prevents the configuration from being saved.

### 3.6.7 Validation Notes

This directly implements the business rule from US-061. The validation logic must specifically check the frequency implied by the CRON expression.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A text input field labeled 'Schedule (CRON Expression)' within the report configuration form.
- Helper text or a tooltip next to the input field explaining the 5-part CRON format: 'Minute (0-59) Hour (0-23) Day-of-Month (1-31) Month (1-12) Day-of-Week (0-6)'
- A validation message area to display errors related to the CRON expression.

## 4.2.0 User Interactions

- The user can type or paste a CRON string into the input field.
- The system provides validation feedback upon attempting to save the configuration.
- The field can be left blank to indicate no schedule.

## 4.3.0 Display Requirements

- When editing an existing report, the field must be pre-populated with the currently saved CRON expression.
- Validation errors must be clear and specific.

## 4.4.0 Accessibility Needs

- The input field must have a proper `<label>` tag for screen readers.
- Helper text should be associated with the input using `aria-describedby`.
- Validation error messages must be programmatically associated with the input and announced by screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A report schedule, if provided, must be a valid 5-part CRON expression.

### 5.1.3 Enforcement Point

Backend API upon saving a report configuration; Frontend UI for user feedback.

### 5.1.4 Violation Handling

The save operation is rejected with a 400 Bad Request error and a descriptive message.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Report execution frequency cannot be more than once per minute.

### 5.2.3 Enforcement Point

Backend API upon saving a report configuration.

### 5.2.4 Violation Handling

The save operation is rejected with a 400 Bad Request error and a descriptive message.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

- {'story_id': 'US-051', 'dependency_reason': 'The guided wizard for creating a report configuration must exist to provide the form where the schedule field will be added.'}

## 6.2.0 Technical Dependencies

- The Quartz.NET 3.x library must be integrated into the backend application to handle job scheduling.
- The Entity Framework Core model for 'ReportConfiguration' must be updated to include a nullable string property for the CRON expression.
- The ASP.NET Core API controller for managing report configurations must be able to interact with the scheduling service.

## 6.3.0 Data Dependencies

- Requires the existence of the 'ReportConfiguration' data entity schema.

## 6.4.0 External Dependencies

*No items available*

# 7.0.0 Non Functional Requirements

## 7.1.0 Performance

- Saving a report configuration with a new or updated schedule should complete within 500ms.
- The scheduling mechanism should not introduce significant overhead to the idle state of the Windows service.

## 7.2.0 Security

- The CRON expression input must be handled as untrusted data, though it is parsed and not executed. Standard input validation practices should apply.
- Only users with the 'Administrator' role can create or modify report schedules.

## 7.3.0 Usability

- The purpose of the field and the expected format should be immediately clear to the user. This story provides the basic input; US-059 (CRON UI builder) will enhance it.

## 7.4.0 Accessibility

- The UI elements must comply with WCAG 2.1 Level AA standards.

## 7.5.0 Compatibility

- The feature must function correctly in all supported browsers (Chrome, Firefox, Edge).

# 8.0.0 Implementation Considerations

## 8.1.0 Complexity Assessment

Medium

## 8.2.0 Complexity Factors

- Initial integration of the Quartz.NET scheduler if not already present.
- Ensuring the lifecycle of scheduled jobs is correctly managed in sync with CRUD operations on ReportConfiguration entities (e.g., deleting a report must unschedule its job).
- Implementing robust error handling for scheduler interactions (e.g., what happens if the scheduler fails to add a job?).
- The logic must be thread-safe.

## 8.3.0 Technical Risks

- Mismanagement of job lifecycles could lead to orphaned jobs or missed schedules.
- Incorrect time zone handling in CRON expressions could cause jobs to run at unexpected times. The system should standardize on UTC for scheduling.

## 8.4.0 Integration Points

- Backend API: The `POST /api/v1/reports` and `PUT /api/v1/reports/{id}` endpoints.
- Scheduling Service: A dedicated service class that abstracts Quartz.NET operations.
- Database: The `ReportConfigurations` table in the SQLite database.

# 9.0.0 Testing Requirements

## 9.1.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0 Test Scenarios

- Create a report with a valid schedule and verify it runs at the correct time.
- Attempt to create a report with various invalid CRON strings and verify save is blocked.
- Create a report without a schedule, then edit it to add one.
- Edit an existing schedule and verify the old trigger is removed and the new one is active.
- Delete a scheduled report and verify the job is unscheduled.
- Restart the Windows service and verify that all schedules are correctly re-established from the database.

## 9.3.0 Test Data Needs

- A set of valid CRON expressions covering different scheduling patterns (daily, weekly, monthly).
- A set of invalid CRON expressions to test validation logic.

## 9.4.0 Testing Tools

- xUnit/Moq for backend unit tests.
- In-memory SQLite provider for integration tests.
- Jest/React Testing Library for frontend component tests.
- A framework like Playwright or Selenium for E2E tests.

# 10.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for CRON validation and scheduler service logic, achieving >80% coverage
- Integration testing completed for API endpoints, verifying database and scheduler state changes
- E2E test scenario of scheduling and running a report is passing
- User interface reviewed and approved by the product owner
- Performance requirements verified
- Security requirements validated
- Documentation for the scheduling feature is updated in the Administrator's User Guide
- Story deployed and verified in staging environment

# 11.0.0 Planning Information

## 11.1.0 Story Points

5

## 11.2.0 Priority

🔴 High

## 11.3.0 Sprint Considerations

- This is a foundational feature for automation. It should be prioritized after the basic report configuration CRUD (US-051) is complete.
- The initial integration of Quartz.NET might be time-consuming and should be factored into the estimate.

## 11.4.0 Release Impact

This story is critical for the Minimum Viable Product (MVP) as it delivers the core 'automated reporting' value proposition.

