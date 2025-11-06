# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-010 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Experience report watermarking in Trial Mode |
| As A User Story | As an Administrator evaluating the software, I wan... |
| User Persona | Administrator |
| Business Value | Provides a clear indicator of the software's trial... |
| Functional Area | Report Generation |
| Story Theme | System Licensing & Activation |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

PDF reports are watermarked in Trial Mode

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

the system is operating in 'Trial Mode'

### 3.1.5 When

a report is generated with the output format set to PDF

### 3.1.6 Then

the resulting PDF document must contain a visible, semi-transparent 'Trial Version' watermark diagonally across each page, and the watermark must not completely obscure the report's primary content.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

HTML reports are watermarked in Trial Mode

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

the system is operating in 'Trial Mode'

### 3.2.5 When

a report is generated with the output format set to HTML

### 3.2.6 Then

the resulting HTML document must display a fixed-position, semi-transparent 'Trial Version' watermark overlay that remains visible during scrolling, and the watermark should not be trivially removable via browser developer tools.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

JSON reports are watermarked in Trial Mode

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

the system is operating in 'Trial Mode'

### 3.3.5 When

a report is generated with the output format set to JSON

### 3.3.6 Then

the root of the output JSON structure must contain a top-level key-value pair: `"watermark": "This report was generated using a Trial Version of the software."`.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

CSV reports are watermarked in Trial Mode

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

the system is operating in 'Trial Mode'

### 3.4.5 When

a report is generated with the output format set to CSV

### 3.4.6 Then

the very first line of the CSV file must be a commented line stating the trial status (e.g., `# This report was generated using a Trial Version of the software.`), and the second line must contain the report's header row.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

TXT reports are watermarked in Trial Mode

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

the system is operating in 'Trial Mode'

### 3.5.5 When

a report is generated with the output format set to TXT

### 3.5.6 Then

the very first line of the TXT file must be a commented line stating the trial status (e.g., `# This report was generated using a Trial Version of the software.`).

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Reports are not watermarked in 'Active' licensed state

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

the system is operating in a fully licensed 'Active' state

### 3.6.5 When

a report is generated in any format (PDF, HTML, JSON, CSV, TXT)

### 3.6.6 Then

the generated report must not contain any trial watermark or trial-related metadata.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Reports are not watermarked during the Grace Period

### 3.7.3 Scenario Type

Edge_Case

### 3.7.4 Given

the system is operating in the license validation 'Grace Period'

### 3.7.5 When

a report is generated in any format (PDF, HTML, JSON, CSV, TXT)

### 3.7.6 Then

the generated report must not contain any trial watermark or trial-related metadata.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- No direct UI elements are created in this story.

## 4.2.0 User Interactions

- The user interaction is indirect: generating a report while the system is in Trial Mode triggers this functionality.

## 4.3.0 Display Requirements

- PDF: Diagonal, semi-transparent text overlay on each page.
- HTML: Fixed-position, semi-transparent overlay.
- JSON: A specific key-value pair at the root of the document.
- CSV/TXT: A commented line at the beginning of the file.

## 4.4.0 Accessibility Needs

- For HTML reports, the watermark overlay must not interfere with screen reader access to the underlying report content.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "Watermarking is applied only when the system's license state is explicitly 'Trial Mode'.", 'enforcement_point': 'During the report generation process, before final serialization or rendering.', 'violation_handling': "If the state is 'Active' or 'Grace Period', the watermarking step must be skipped entirely."}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-017

#### 6.1.1.2 Dependency Reason

This story depends on the existence of a 'Trial Mode' state, which is the outcome of the Grace Period expiring.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-055

#### 6.1.2.2 Dependency Reason

This story modifies the output of all report formats defined in US-055.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-016

#### 6.1.3.2 Dependency Reason

This story must correctly differentiate between 'Trial Mode' and the 'Grace Period' defined in US-016.

## 6.2.0.0 Technical Dependencies

- A centralized `LicensingService` that can be queried by the report generation engine to reliably determine the current system state (Trial, Active, Grace Period).
- Report generation modules for PDF (Puppeteer Sharp), HTML (Handlebars.Net), JSON, CSV, and TXT.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The watermarking process should add negligible overhead (<5%) to the total report generation time.

## 7.2.0.0 Security

- The HTML watermark implementation should avoid techniques that could be exploited for cross-site scripting (XSS).

## 7.3.0.0 Usability

- Visual watermarks (PDF, HTML) must be designed to be noticeable without making the underlying report content unreadable.

## 7.4.0.0 Accessibility

- WCAG 2.1 AA: HTML watermark must not trap keyboard focus or interfere with screen reader navigation.

## 7.5.0.0 Compatibility

- PDF watermark should render correctly in common PDF viewers (Adobe Acrobat Reader, Chrome/Firefox built-in viewers).
- HTML watermark should render correctly in the latest stable versions of Chrome, Firefox, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The logic is a cross-cutting concern that must be implemented across five different report generation pipelines.
- Each file format requires a different technical approach for watermarking (visual overlay vs. metadata injection).
- Requires careful handling of the three distinct system states (Trial, Grace Period, Active) to apply the logic correctly.

## 8.3.0.0 Technical Risks

- Risk of corrupting machine-readable formats (JSON, CSV) if the watermark is not added correctly. The specified approaches (top-level key, commented first line) mitigate this risk.
- Visual watermarks may have rendering inconsistencies across different client software (browsers, PDF readers).

## 8.4.0.0 Integration Points

- The report generation engine must integrate with the `LicensingService` at the start of every generation job.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify watermark presence for each of the 5 report types in Trial Mode.
- Verify watermark absence for each of the 5 report types in Active Mode.
- Verify watermark absence for each of the 5 report types in Grace Period.
- Verify that the content of the report is not corrupted by the watermarking process.

## 9.3.0.0 Test Data Needs

- A simple, standard dataset that can be rendered into all 5 report formats.
- A mechanism to mock or force the system's license state into 'Trial', 'Active', and 'Grace Period' for testing purposes.

## 9.4.0.0 Testing Tools

- xUnit and Moq for backend unit tests.
- Automated file content validators for JSON, CSV, and TXT.
- A PDF parsing library to programmatically check for watermark text.
- A browser automation tool (e.g., Playwright, Selenium) to verify the HTML watermark.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for all 5 report generators and the licensing state logic, achieving >80% coverage
- Integration testing completed successfully for all scenarios (Trial, Active, Grace Period) across all report formats
- User interface reviewed and approved
- Performance requirements verified
- Security requirements validated
- Documentation updated appropriately to mention the trial watermark
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a core business requirement for the trial version and should be prioritized early.
- The developer should ensure the `LicensingService` dependency is stable and provides a clear API before starting implementation.

## 11.4.0.0 Release Impact

- Essential for any public release that includes a trial or evaluation mode.

