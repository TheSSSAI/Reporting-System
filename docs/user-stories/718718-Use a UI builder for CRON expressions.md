# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-059 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Use a UI builder for CRON expressions |
| As A User Story | As an Administrator configuring a report schedule,... |
| User Persona | Administrator |
| Business Value | Improves usability and reduces configuration error... |
| Functional Area | Report Configuration |
| Story Theme | System Administration & Usability |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Launch the CRON builder UI

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

An Administrator is on the 'Report Configuration' page, in the scheduling section.

### 3.1.5 When

The Administrator clicks a 'Build Schedule' button next to the CRON expression input field.

### 3.1.6 Then

A modal dialog or dedicated UI component for building a schedule is displayed, showing options for common frequencies like 'Minutes', 'Hourly', 'Daily', 'Weekly', and 'Monthly'.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Generate a simple daily schedule

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The CRON builder UI is open.

### 3.2.5 When

The Administrator selects the 'Daily' frequency and sets the time to '09:30'.

### 3.2.6 Then

The builder must display the generated CRON expression '30 9 * * *' and a human-readable summary such as 'Runs every day at 9:30 AM'.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Generate a specific weekly schedule

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

The CRON builder UI is open.

### 3.3.5 When

The Administrator selects the 'Weekly' frequency, checks the boxes for 'Monday' and 'Friday', and sets the time to '17:00'.

### 3.3.6 Then

The builder must display the generated CRON expression '0 17 * * 1,5' and a human-readable summary such as 'Runs every Monday and Friday at 5:00 PM'.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Generate a specific monthly schedule

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

The CRON builder UI is open.

### 3.4.5 When

The Administrator selects the 'Monthly' frequency, chooses 'Day' 15 of the month, and sets the time to '02:00'.

### 3.4.6 Then

The builder must display the generated CRON expression '0 2 15 * *' and a human-readable summary such as 'Runs on day 15 of every month at 2:00 AM'.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Apply the generated schedule to the report configuration

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

The CRON builder has generated a valid schedule.

### 3.5.5 When

The Administrator clicks the 'Apply' or 'Save' button within the builder.

### 3.5.6 Then

The builder modal closes, and the generated CRON expression is populated into the main CRON expression text field on the report configuration form.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Builder parses an existing valid CRON expression

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

The CRON expression input field on the report configuration form already contains a valid expression, such as '0 5 * * 1-5'.

### 3.6.5 When

The Administrator opens the CRON builder.

### 3.6.6 Then

The builder UI should be pre-populated with the corresponding settings (e.g., 'Weekly' selected, 'Monday' through 'Friday' checked, time set to '05:00').

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Builder handles an existing invalid or un-parsable CRON expression

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

The CRON expression input field contains an invalid string, such as 'every tuesday'.

### 3.7.5 When

The Administrator opens the CRON builder.

### 3.7.6 Then

The builder should open in its default, empty state and may display a non-blocking notification like 'Could not parse the existing expression'.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A 'Build Schedule' button or icon next to the CRON input field.
- A modal dialog for the builder.
- Tabs or radio buttons for frequency selection (Minutes, Hourly, Daily, Weekly, Monthly).
- Time picker for selecting hour and minute.
- Checkboxes for selecting days of the week and months.
- Number inputs or dropdowns for selecting day of the month.
- A read-only text area to display the generated CRON expression in real-time.
- A text area to display a human-readable summary of the schedule in real-time.
- 'Apply' and 'Cancel' buttons in the modal.

## 4.2.0 User Interactions

- Clicking the 'Build Schedule' button opens the modal.
- Selecting options within the builder immediately updates both the CRON expression and the human-readable summary.
- Clicking 'Apply' populates the main form's input field and closes the modal.
- Clicking 'Cancel' or closing the modal discards any changes made in the builder.

## 4.3.0 Display Requirements

- The UI must clearly distinguish between different frequency options.
- The generated CRON string and its human-readable summary must always be visible within the builder.

## 4.4.0 Accessibility Needs

- All interactive elements (buttons, inputs, checkboxes) must be keyboard-navigable and have appropriate focus states.
- All controls must have associated labels for screen reader compatibility (e.g., using `aria-label`).
- The modal must properly trap focus when open.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'The system shall not support schedules that run more frequently than once per minute.', 'enforcement_point': 'The CRON builder UI.', 'violation_handling': "The UI must be designed to prevent the user from creating a CRON expression that would violate this rule (e.g., no options for seconds, minimum interval for 'every X minutes' is 1)."}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-051

#### 6.1.1.2 Dependency Reason

This story provides the Report Configuration UI where the CRON builder will be integrated.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-058

#### 6.1.2.2 Dependency Reason

This story establishes the requirement for a CRON expression field that this builder will populate.

## 6.2.0.0 Technical Dependencies

- A third-party JavaScript library for CRON parsing, generation, and human-readable text conversion (e.g., `cron-parser`, `cronstrue`) is highly recommended to ensure correctness and handle edge cases.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The CRON expression and human-readable summary must update in real-time with no perceivable latency as the user interacts with the builder UI.

## 7.2.0.0 Security

- While this is a client-side feature, the final CRON expression must still be validated on the backend (as per US-060) to prevent malicious or invalid input.

## 7.3.0.0 Usability

- The builder must be intuitive and significantly easier to use than manually writing a CRON expression for common scheduling scenarios.

## 7.4.0.0 Accessibility

- The CRON builder component must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The component must function correctly on the latest stable versions of Google Chrome, Mozilla Firefox, and Microsoft Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Selection and integration of a suitable third-party CRON library.
- Designing an intuitive UI/UX that covers common use cases without becoming overly complex.
- Implementing the two-way binding logic: UI state to CRON string (generation) and CRON string to UI state (parsing).
- Ensuring robust handling of all CRON syntax edge cases, which is why a library is recommended.

## 8.3.0.0 Technical Risks

- Finding a single third-party library that handles generation, parsing, and human-readable descriptions well may be difficult, potentially requiring multiple libraries.
- The logic for parsing an arbitrary CRON string back into a UI state can be complex and may only support a subset of all possible CRON expressions.

## 8.4.0.0 Integration Points

- The React component for the builder will be integrated into the Report Configuration form.
- The component's state will interact with the parent form's state management (Zustand).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify CRON generation for each frequency tab (Hourly, Daily, Weekly, etc.) with various inputs.
- Verify the human-readable summary is accurate for all generated expressions.
- Verify that applying the schedule correctly updates the parent form.
- Test the parsing of at least 10 different valid CRON expressions to ensure the UI state is set correctly.
- Test the behavior when an invalid CRON string is present before opening the builder.
- Verify keyboard navigation and screen reader announcements for the entire component.

## 9.3.0.0 Test Data Needs

- A list of valid CRON expressions and their expected UI state representations.
- A list of invalid CRON expressions.

## 9.4.0.0 Testing Tools

- Jest
- React Testing Library
- An E2E testing framework (e.g., Playwright, Cypress)
- Browser accessibility audit tools (e.g., Lighthouse, Axe).

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing.
- Code reviewed and approved by team.
- Unit tests for the builder component achieve >= 80% coverage.
- Integration testing with the report configuration form is completed successfully.
- User interface has been reviewed and approved by the Product Owner.
- Performance requirements for real-time updates are verified.
- Accessibility audit (WCAG 2.1 AA) has been performed and any issues resolved.
- Documentation for the scheduling feature is updated to include instructions for the builder.
- Story deployed and verified in the staging environment.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- A time-boxed spike (1-2 hours) may be needed at the start of the sprint to evaluate and select the best third-party CRON library.
- This is primarily a frontend task that can be developed once the report configuration API contract is stable.

## 11.4.0.0 Release Impact

This is a significant usability improvement for a core administrative task and should be highlighted in release notes.

