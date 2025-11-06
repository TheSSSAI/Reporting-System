# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-09-WEB-UI |
| Extraction Timestamp | 2024-07-27T10:00:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 98% |
| Implementation Readiness Level | High (pending backend API specification) |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

REQ-UI-DTR-002

#### 1.2.1.2 Requirement Text

The Control Panel shall provide a UI for managing transformation scripts that embeds the Monaco Editor component to offer real-time JavaScript syntax highlighting, code completion, and validation.

#### 1.2.1.3 Validation Criteria

- The script management page displays an instance of the Monaco Editor.
- As a user types JavaScript code, syntax is highlighted correctly.
- Typing an object followed by a dot triggers code completion suggestions.
- A clear visual indicator (e.g., a red squiggle) appears under a JavaScript syntax error in real-time.

#### 1.2.1.4 Implementation Implications

- The frontend application must integrate the @monaco-editor/react library or a similar wrapper.
- Configuration will be required to set up the Monaco Editor for the JavaScript language, including enabling syntax highlighting and validation features.
- The component must be able to handle getting and setting the script content from the parent form's state.

#### 1.2.1.5 Extraction Reasoning

This is a core functional requirement that directly dictates the technology choice (Monaco Editor) and key features for a major component within this UI repository.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

REQ-UI-DTR-003

#### 1.2.2.2 Requirement Text

The script editor UI shall be composed of three resizable panes for the script editor, sample JSON input, and transformed JSON output/error view.

#### 1.2.2.3 Validation Criteria

- The UI displays three distinct panes for script, input, and output.
- The user can drag the dividers between the panes to resize them.
- The output pane correctly displays formatted JSON upon successful preview execution.
- The output pane correctly displays a structured error message upon a failed preview execution.

#### 1.2.2.4 Implementation Implications

- A resizable pane layout component must be implemented or integrated (e.g., using a library like 'react-split-pane').
- State management is required to handle the content of all three panes.
- The output pane needs a component that can render formatted JSON or a structured error message.

#### 1.2.2.5 Extraction Reasoning

This requirement defines the fundamental layout and user experience of the transformation script editor, a key feature of the Control Panel UI.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

REQ-UI-DTR-001

#### 1.2.3.2 Requirement Text

The Control Panel UI shall display a persistent performance warning to the administrator when a transformation script is associated with a report configuration, including a link to best-practice documentation.

#### 1.2.3.3 Validation Criteria

- When a user selects a transformation script in the report configuration UI, a warning message is displayed.
- The warning message explicitly mentions potential increases in memory usage and processing time.
- The warning remains visible as long as a script is associated with the report.

#### 1.2.3.4 Implementation Implications

- The report configuration form component must conditionally render a warning UI element (e.g., an MUI Alert component).
- The visibility of this warning must be tied to the state of the transformation script selection field.

#### 1.2.3.5 Extraction Reasoning

This requirement specifies a key usability and user-awareness feature directly within the Control Panel UI.

### 1.2.4.0 Requirement Id

#### 1.2.4.1 Requirement Id

US-115

#### 1.2.4.2 Requirement Text

As a user with a visual or motor impairment, I want the web interfaces (Control Panel and Report Viewer) to be fully compliant with WCAG 2.1 Level AA standards, so that I can independently and effectively perceive, navigate, and operate the system using my preferred assistive technologies.

#### 1.2.4.3 Validation Criteria

- All non-text content has text alternatives.
- Sufficient color contrast is used (4.5:1 for normal text).
- Full keyboard accessibility for all interactive elements.
- Visible keyboard focus indicator is present.
- Forms have proper labels and error handling.
- Semantic HTML and ARIA are used for dynamic components.

#### 1.2.4.4 Implementation Implications

- All development must follow accessibility best practices.
- UI components (e.g., from MUI) must be used with accessibility in mind.
- Automated accessibility testing (e.g., jest-axe, axe-core) must be integrated into the development and CI pipeline.
- Manual testing with screen readers and keyboard-only navigation is mandatory.

#### 1.2.4.5 Extraction Reasoning

This is a critical, system-wide non-functional requirement that governs the implementation of every single component within this UI repository.

### 1.2.5.0 Requirement Id

#### 1.2.5.1 Requirement Id

US-114

#### 1.2.5.2 Requirement Text

As a User (Administrator or Viewer), I want the web application's interface to be responsive to my browser's window size, so that I can have a clear, usable, and consistent experience across different screen resolutions without encountering broken layouts or horizontal scrolling.

#### 1.2.5.3 Validation Criteria

- Layout is optimized for standard desktop viewports (1280px to 1919px).
- Layout on large desktops (>=1920px) is constrained for readability.
- UI reflows smoothly during dynamic browser resizing.
- The application gracefully degrades on viewports smaller than 1280px.

#### 1.2.5.4 Implementation Implications

- The application must be built using a responsive design framework, such as MUI's Grid system and breakpoints.
- CSS must use relative units and media queries where appropriate.
- All new components must be tested for responsiveness across the target viewport sizes.

#### 1.2.5.5 Extraction Reasoning

This is a foundational non-functional requirement that defines the layout and design principles for the entire UI application.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

ScriptEditorComponent

#### 1.3.1.2 Component Specification

A comprehensive UI component that provides the full editing experience for JavaScript transformation scripts. It integrates the Monaco Editor, provides input for sample data, and displays the output or errors from a preview execution. It implements the three-pane resizable layout.

#### 1.3.1.3 Implementation Requirements

- Integrate the @monaco-editor/react library.
- Configure the editor for JavaScript with syntax highlighting and real-time validation.
- Implement a resizable layout using a library like react-split-pane.
- Manage the state for the script content, sample data, and output/error.
- Invoke the API Client service to call the /api/v1/transformations/preview endpoint.

#### 1.3.1.4 Architectural Context

Belongs to the Presentation Layer (Control Panel UI). It is a primary view/page within the application.

#### 1.3.1.5 Extraction Reasoning

This component is the direct implementation of core requirements REQ-UI-DTR-002 and REQ-UI-DTR-003, representing a major feature of the Control Panel.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

ApiClientService

#### 1.3.2.2 Component Specification

A client-side service module responsible for all HTTP communication with the backend RESTful API. It encapsulates Axios (or a similar library), handles setting the Authorization header with the JWT, and provides a structured way to make API calls and handle responses/errors.

#### 1.3.2.3 Implementation Requirements

- Use axios to create a pre-configured instance.
- Implement an interceptor to automatically add the Authorization: Bearer <token> header to all outgoing requests.
- Implement logic to handle token refresh if applicable (dependency on US-088).
- Provide typed functions for each backend endpoint (e.g., fetchScripts(), createScript(data)).
- Centralize error handling to parse API error responses into a consistent format for UI components.

#### 1.3.2.4 Architectural Context

A cross-cutting concern within the Presentation Layer, acting as the data access layer for the frontend application.

#### 1.3.2.5 Extraction Reasoning

This component is essential for fulfilling the UI's primary role as a client to the backend API. It is the sole integration point with REPO-08-SERVICE-HOST.

## 1.4.0.0 Architectural Layers

- {'layer_name': 'Presentation Layer (Control Panel UI)', 'layer_responsibilities': ['Rendering the user interface for all system features, including script management, user management, report configuration, and monitoring.', 'Managing client-side application state (e.g., using Zustand).', 'Handling all user interactions and events.', 'Communicating with the backend via a RESTful API to fetch and persist data.', 'Enforcing client-side validation for improved user experience.', 'Displaying data, warnings, and error messages to the user.'], 'layer_constraints': ['Must be a Single-Page Application (SPA).', 'Must use the React 18 framework and TypeScript.', 'Must use the MUI v5 component library for the design system.', 'Must not contain any business logic that should reside on the backend.', 'Communication with the backend is strictly limited to the defined RESTful API contract.'], 'implementation_patterns': ['Component-Based Architecture (React)', 'Centralized State Management (Zustand)', 'API Client Service Pattern', 'Protected Routes for authentication'], 'extraction_reasoning': "This is the single architectural layer that this repository is responsible for implementing. Its definition, responsibilities, and constraints are central to the entire repository's context."}

## 1.5.0.0 Dependency Interfaces

### 1.5.1.0 Interface Name

#### 1.5.1.1 Interface Name

ReportingSystem RESTful API

#### 1.5.1.2 Source Repository

REPO-08-SERVICE-HOST

#### 1.5.1.3 Method Contracts

##### 1.5.1.3.1 Method Name

###### 1.5.1.3.1.1 Method Name

POST /api/v1/auth/token

###### 1.5.1.3.1.2 Method Signature

POST /api/v1/auth/token (Body: { username, password })

###### 1.5.1.3.1.3 Method Purpose

Authenticates a user and returns a JWT access token and a refresh token.

###### 1.5.1.3.1.4 Integration Context

Called by the LoginPage component when a user submits their credentials.

##### 1.5.1.3.2.0 Method Name

###### 1.5.1.3.2.1 Method Name

POST /api/v1/transformations/preview

###### 1.5.1.3.2.2 Method Signature

POST /api/v1/transformations/preview (Body: { scriptContent: string, sampleData: string | null, connectorId: string | null })

###### 1.5.1.3.2.3 Method Purpose

Executes a given JavaScript transformation script against either provided sample data or a live sample from a connector, returning the transformed output or an error.

###### 1.5.1.3.2.4 Integration Context

Called by the ScriptEditorComponent when the user clicks the 'Preview' button.

##### 1.5.1.3.3.0 Method Name

###### 1.5.1.3.3.1 Method Name

GET /api/v1/transformations

###### 1.5.1.3.3.2 Method Signature

GET /api/v1/transformations

###### 1.5.1.3.3.3 Method Purpose

Retrieves a list of all configured transformation scripts.

###### 1.5.1.3.3.4 Integration Context

Called by the transformation script list view to populate the list of available scripts.

##### 1.5.1.3.4.0 Method Name

###### 1.5.1.3.4.1 Method Name

PUT /api/v1/transformations/{id}

###### 1.5.1.3.4.2 Method Signature

PUT /api/v1/transformations/{id} (Body: { name: string, content: string })

###### 1.5.1.3.4.3 Method Purpose

Updates an existing transformation script.

###### 1.5.1.3.4.4 Integration Context

Called by the ScriptEditorComponent when the user saves changes to an existing script.

##### 1.5.1.3.5.0 Method Name

###### 1.5.1.3.5.1 Method Name

GET /api/v1/jobs

###### 1.5.1.3.5.2 Method Signature

GET /api/v1/jobs (Query Params: pagination, filtering, search)

###### 1.5.1.3.5.3 Method Purpose

Retrieves the list of report job executions for the Job Monitoring Dashboard.

###### 1.5.1.3.5.4 Integration Context

Called by the JobMonitoringDashboard to display the list of recent jobs.

#### 1.5.1.4.0.0 Integration Pattern

Request-Reply

#### 1.5.1.5.0.0 Communication Protocol

HTTPS. Data is exchanged in JSON format. Authentication is handled via JWT Bearer tokens in the Authorization header.

#### 1.5.1.6.0.0 Extraction Reasoning

This is the sole dependency for the UI repository. The UI's primary function is to provide a graphical interface for this API, making its contract the most critical piece of dependency context. The listed endpoints represent a sample of the full API surface required by the UI based on its core requirements.

### 1.5.2.0.0.0 Interface Name

#### 1.5.2.1.0.0 Interface Name

Shared Data Contracts

#### 1.5.2.2.0.0 Source Repository

REPO-03-SHARED-CONTRACTS

#### 1.5.2.3.0.0 Method Contracts

*No items available*

#### 1.5.2.4.0.0 Integration Pattern

Code Generation / Type Mirroring

#### 1.5.2.5.0.0 Communication Protocol

N/A

#### 1.5.2.6.0.0 Extraction Reasoning

The TypeScript interfaces used within this repository for all API communication (e.g., UserDto, ReportConfigurationDto) must be a direct reflection of the C# DTOs defined in REPO-03-SHARED-CONTRACTS. The integration is achieved by generating a TypeScript API client and types from the OpenAPI specification provided by REPO-08-SERVICE-HOST, which in turn is built using the DTOs from REPO-03. This ensures type safety and contract consistency between the frontend and backend.

## 1.6.0.0.0.0 Exposed Interfaces

*No items available*

## 1.7.0.0.0.0 Technology Context

### 1.7.1.0.0.0 Framework Requirements

The application must be built using React 18 and TypeScript 5.4+. The Vite build tool is specified for development and bundling. The MUI v5 component library must be used for all UI elements to ensure a consistent design system.

### 1.7.2.0.0.0 Integration Technologies

- axios: For making HTTP requests to the backend RESTful API.
- @monaco-editor/react: For implementing the rich code editor required for script management.
- zustand: For lightweight, centralized client-side state management for global state like authentication.
- tanstack-query: For managing server state, including data fetching, caching, and mutations.

### 1.7.3.0.0.0 Performance Constraints

The application must be responsive, with page loads and UI interactions feeling instantaneous. Bundle size should be optimized via code-splitting. Long lists must use virtualization to maintain performance.

### 1.7.4.0.0.0 Security Requirements

The application must securely handle JWTs, storing them in a safe manner (e.g., in-memory state). All data rendered in the UI must be properly sanitized to prevent XSS attacks (largely handled by React by default). All communication with the backend must be over HTTPS.

## 1.8.0.0.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | All repository mappings (layers, components) have ... |
| Cross Reference Validation | The repository's dependencies, technology stack, a... |
| Implementation Readiness Assessment | The repository is ready for implementation. The te... |
| Quality Assurance Confirmation | The extracted context has been systematically revi... |

