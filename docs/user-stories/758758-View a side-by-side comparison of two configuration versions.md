# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-099 |
| Elaboration Date | 2025-01-26 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | View a side-by-side comparison of two configuratio... |
| As A User Story | As an Administrator, I want to select any two vers... |
| User Persona | Administrator |
| Business Value | Provides a clear, visual tool for auditing configu... |
| Functional Area | Configuration Management |
| Story Theme | Configuration Versioning & Rollback |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Compare two different versions with changes

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

an Administrator is viewing the version history page for a configuration item which has at least two versions with different content

### 3.1.5 When

the Administrator selects exactly two different versions using their checkboxes and clicks the 'Compare' button

### 3.1.6 Then

a side-by-side comparison view (e.g., in a modal) is displayed, clearly labeling each side with its respective version number and timestamp

### 3.1.7 And

the comparison correctly shows differences within nested JSON objects and arrays

### 3.1.8 Validation Notes

Verify that the diff view accurately reflects a known change, such as a modified connection string or an added delivery destination. The highlighting for additions, deletions, and modifications must be visually distinct.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Compare button state with incorrect selection count

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

an Administrator is on the version history page

### 3.2.5 When

the number of selected versions is zero, one, or more than two

### 3.2.6 Then

the 'Compare' button is in a disabled state

### 3.2.7 Validation Notes

Test by selecting 0 items, then 1 item, then 3 items. The 'Compare' button should be disabled in all cases. It should only become enabled when exactly 2 items are selected.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Compare two versions with no differences

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

an Administrator is viewing the version history for a configuration item

### 3.3.5 And

two different versions have identical content

### 3.3.6 When

the Administrator selects these two identical versions and clicks 'Compare'

### 3.3.7 Then

the comparison view is displayed with a clear message indicating 'No differences found between the selected versions.'

### 3.3.8 Validation Notes

Create two versions of a configuration without any changes between them and perform the comparison to verify the message appears.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Close the comparison view

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

the side-by-side comparison view is open

### 3.4.5 When

the Administrator clicks the 'Close' button or 'X' icon

### 3.4.6 Then

the comparison view is dismissed and the user is returned to the version history list with their previous selections intact

### 3.4.7 Validation Notes

Verify that closing the modal returns the user to the previous screen without losing context.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Checkboxes next to each version in the version history list.
- A 'Compare' button, which is contextually enabled/disabled.
- A modal or dedicated view for the side-by-side comparison.
- Clear labels within the diff view for 'Version A' and 'Version B', including version number, timestamp, and author.
- A 'Close' button or 'X' icon to dismiss the diff view.

## 4.2.0 User Interactions

- User can select/deselect versions via checkboxes.
- The 'Compare' button becomes clickable only when exactly two versions are selected.
- The diff view should be vertically scrollable to accommodate large configurations.

## 4.3.0 Display Requirements

- Differences must be highlighted. Standard convention is green for additions, red for deletions, and yellow/blue for modifications.
- The entire configuration object (likely JSON) should be displayed in a readable, formatted way (e.g., with proper indentation).

## 4.4.0 Accessibility Needs

- The color contrast of the highlighting used in the diff view must meet WCAG 2.1 Level AA standards.
- All interactive elements (checkboxes, buttons, close icon) must be keyboard accessible and have appropriate focus indicators.
- The diff view content should be accessible to screen readers, which should announce changes if possible (e.g., 'line added', 'line removed').

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'Comparison is only permitted between exactly two versions at a time.', 'enforcement_point': 'User Interface (frontend)', 'violation_handling': "The 'Compare' action is disabled if the selection count is not equal to two."}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

- {'story_id': 'US-098', 'dependency_reason': 'This story requires the UI and backend functionality from US-098 to list configuration versions, which is the entry point for selecting versions to compare.'}

## 6.2.0 Technical Dependencies

- A backend API endpoint to fetch the content of two specific configuration versions by their IDs.
- A frontend library capable of generating and rendering a side-by-side diff of structured JSON data (e.g., react-diff-viewer).

## 6.3.0 Data Dependencies

- Requires access to the versioned history of configuration items (ConnectorConfiguration, TransformationScript, ReportConfiguration) stored in the SQLite database.

## 6.4.0 External Dependencies

*No items available*

# 7.0.0 Non Functional Requirements

## 7.1.0 Performance

- The diff generation and rendering for a configuration up to 50KB in size should complete in under 1 second.
- The UI must remain responsive while the diff view is open, even with large configuration files.

## 7.2.0 Security

- The API endpoint for fetching version data must be protected and only accessible to authenticated users with the 'Administrator' role.

## 7.3.0 Usability

- The visual representation of changes must be intuitive and easy to understand at a glance.
- The process of selecting and comparing versions should require minimal clicks.

## 7.4.0 Accessibility

- The entire feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0 Compatibility

- The feature must function correctly on all supported browsers (latest stable Chrome, Firefox, Edge).

# 8.0.0 Implementation Considerations

## 8.1.0 Complexity Assessment

Medium

## 8.2.0 Complexity Factors

- Integrating and potentially customizing a third-party diff viewer library to meet UI and accessibility requirements.
- Managing frontend state for selections and the contextual enabling/disabling of the compare button.
- Ensuring performant rendering of large or deeply nested JSON configuration objects.

## 8.3.0 Technical Risks

- The chosen diff library may have performance limitations with very large JSON files, requiring optimization or a different approach.
- The chosen diff library may not be fully accessible out-of-the-box, requiring custom wrappers or modifications.

## 8.4.0 Integration Points

- Frontend: Integrates with the Version History component developed for US-098.
- Backend: A new API endpoint will be needed in the relevant controller (e.g., `ConfigurationController`) to service requests for version data.

# 9.0.0 Testing Requirements

## 9.1.0 Testing Types

- Unit
- Integration
- E2E
- Performance
- Accessibility

## 9.2.0 Test Scenarios

- Verify diff of simple value changes (e.g., string, number).
- Verify diff of added/removed keys in a JSON object.
- Verify diff of changes within a nested JSON object.
- Verify diff of changes within an array (added, removed, modified elements).
- Test with a large configuration file (e.g., >100KB) to check for UI freezes or slow rendering.
- Test keyboard navigation and screen reader compatibility for the entire workflow.

## 9.3.0 Test Data Needs

- A configuration item with multiple versions containing a variety of changes: simple value changes, structural changes (added/removed keys), and nested object changes.
- A configuration item with two identical versions.
- A large, complex configuration file to use for performance testing.

## 9.4.0 Testing Tools

- Frontend: Jest, React Testing Library.
- Backend: xUnit, Moq.
- E2E: Playwright or Cypress.
- Accessibility: Axe browser extension, screen reader (NVDA/JAWS).

# 10.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for both frontend and backend logic, achieving >80% coverage
- Integration testing between UI and API completed successfully
- E2E tests for the full user workflow are passing
- User interface reviewed for usability and consistency
- Performance requirements verified against a large configuration file
- Accessibility audit (WCAG 2.1 AA) completed and any issues resolved
- Documentation for the feature (if any) is updated
- Story deployed and verified in the staging environment

# 11.0.0 Planning Information

## 11.1.0 Story Points

5

## 11.2.0 Priority

🟡 Medium

## 11.3.0 Sprint Considerations

- This story is dependent on US-098 and must be scheduled in a sprint after US-098 is complete.
- Time should be allocated for evaluating and selecting a suitable frontend diff viewer library.

## 11.4.0 Release Impact

Enhances the configuration management feature set, providing a key tool for administrators. It is a significant value-add for the versioning and rollback capability.

