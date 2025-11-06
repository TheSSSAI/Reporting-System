# 1 Id

REPO-09-WEB-UI

# 2 Name

ReportingSystem.Web.UI

# 3 Description

This repository contains the entire frontend application, built with React and TypeScript. It is a completely self-contained Single-Page Application (SPA) responsible for rendering the Control Panel and Report Viewer. It communicates with the backend exclusively through the RESTful API defined by the `ReportingSystem.Shared.Contracts` library. This clean separation allows for a dedicated frontend development workflow, tooling (Vite, NPM), and deployment pipeline. The UI code can be developed, tested, and versioned independently of the backend, enabling a specialized frontend team to work with maximum velocity. The final build artifacts (HTML, JS, CSS) are served by the ASP.NET Core server in the `ReportingSystem.Service` repository.

# 4 Type

🔹 Application Services

# 5 Namespace

N/A

# 6 Output Path

src/ui/ReportingSystem.Web.UI

# 7 Framework

React 18

# 8 Language

TypeScript

# 9 Technology

React, TypeScript, Vite, MUI, Zustand, Monaco Editor

# 10 Thirdparty Libraries

- react
- typescript
- @mui/material
- zustand
- axios
- monaco-editor

# 11 Layer Ids

- presentation-layer-ui

# 12 Dependencies

*No items available*

# 13 Requirements

*No items available*

# 14 Generate Tests

✅ Yes

# 15 Generate Documentation

✅ Yes

# 16 Architecture Style

Single-Page Application (SPA)

# 17 Architecture Map

- frontend-application

# 18 Components Map

- script-editor-view-008
- monaco-editor-component-009
- api-client-010

# 19 Requirements Map

- 5.1 User Interfaces

# 20 Decomposition Rationale

## 20.1 Operation Type

NEW_DECOMPOSED

## 20.2 Source Repository

REPO-01-MONO

## 20.3 Decomposition Reasoning

Extracted to create a strict separation between the frontend and backend technology stacks and development workflows. This allows a dedicated frontend team to use their own tools and processes (NPM, Vite) and iterate on the UI independently of the backend release cycle, communicating only via a versioned API contract.

## 20.4 Extracted Responsibilities

- All React components and UI logic.
- Frontend state management.
- API client for communicating with the backend.
- Frontend build and dependency management.

## 20.5 Reusability Scope

- This is a specific UI for this application and is not intended for reuse.

## 20.6 Development Benefits

- Enables parallel, independent development for frontend and backend teams.
- Allows for a specialized, optimized frontend toolchain.
- Clear separation of concerns improves code maintainability.

# 21.0 Dependency Contracts

## 21.1 Repo-08-Service-Host

### 21.1.1 Required Interfaces

- {'interface': 'RESTful API', 'methods': ['Consumes all endpoints exposed by the backend, such as GET /api/v1/reports.'], 'events': [], 'properties': []}

### 21.1.2 Integration Pattern

Request-Reply via HTTP/REST.

### 21.1.3 Communication Protocol

HTTPS

# 22.0.0 Exposed Contracts

## 22.1.0 Public Interfaces

*No items available*

# 23.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | N/A |
| Event Communication | Uses frontend state management (Zustand) for inter... |
| Data Flow | Follows a standard React data flow (props down, ev... |
| Error Handling | Displays user-friendly error messages based on API... |
| Async Patterns | Uses async/await with fetch/Axios for all API call... |

# 24.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Follows React best practices for component design ... |
| Performance Considerations | Optimize bundle size using code splitting. Use vir... |
| Security Considerations | Implement secure handling of JWTs. Prevent XSS att... |
| Testing Approach | Use Jest and React Testing Library for component u... |

# 25.0.0 Scope Boundaries

## 25.1.0 Must Implement

- The entire user interface for the Control Panel and Report Viewer.

## 25.2.0 Must Not Implement

- Any business logic that should reside on the backend.

## 25.3.0 Extension Points

- New pages and components can be added to support new features.

## 25.4.0 Validation Rules

- Implement client-side validation for better user experience, but always rely on backend validation as the source of truth.

