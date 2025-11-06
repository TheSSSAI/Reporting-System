# Reporting System - Web UI

This repository contains the frontend Single-Page Application (SPA) for the Reporting System. It is built with React, TypeScript, and Vite, and serves as the user interface for both the Control Panel (for administrators) and the Report Viewer (for end-users).

## Project Overview

The Web UI is a modern, responsive, and accessible frontend that communicates with the backend exclusively through a secure RESTful API. This clean separation allows for independent development, testing, and deployment cycles.

### Core Technologies

- **React 18**: For building the user interface.
- **TypeScript**: For type safety and improved developer experience.
- **Vite**: For fast development server and optimized production builds.
- **Material-UI (MUI)**: For a comprehensive suite of UI components that follow Material Design.
- **Zustand**: For lightweight, simple global state management.
- **TanStack Query**: For server-state management, including data fetching, caching, and mutations.
- **Monaco Editor**: For the rich code editor used in the transformation script management feature.
- **Playwright**: for End-to-End testing.

## Prerequisites

- [Node.js](https://nodejs.org/) (version 18.x or later)
- [pnpm](https://pnpm.io/) (or npm/yarn) package manager

## Getting Started

### 1. Installation

Clone the repository and install the dependencies:

```bash
git clone <repository_url>
cd reporting-system-web-ui
pnpm install
```

### 2. Environment Configuration

Copy the example environment file and configure it for your local development setup.

```bash
cp .env.example .env.local
```

Open `.env.local` and set `VITE_API_BASE_URL` to the URL of your running backend service (e.g., `http://localhost:5000`).

### 3. Running the Development Server

To start the Vite development server with hot-reloading:

```bash
pnpm dev
```

The application will be available at `http://localhost:5173`.

## Available Scripts

- `pnpm dev`: Starts the development server.
- `pnpm build`: Compiles TypeScript and builds the application for production in the `dist/` directory.
- `pnpm lint`: Runs ESLint to analyze the code for potential errors and style issues.
- `pnpm preview`: Starts a local server to preview the production build.
- `pnpm test`: Runs unit and integration tests using Vitest.
- `pnpm test:e2e`: Runs end-to-end tests using Playwright.
- `pnpm test:e2e:report`: Shows the HTML report for the last Playwright test run.

## Architectural Principles

This project follows the **Feature-Sliced Design (FSD)** methodology to organize code for scalability and maintainability. The main layers are:

- `src/app`: Application-wide setup (routing, providers, global stores).
- `src/pages`: Page-level components that compose features and widgets.
- `src/features`: Encapsulated business logic for specific features (e.g., user authentication, script editing).
- `src/widgets`: Larger UI blocks composed of multiple features (e.g., Sidebar, PageHeader).
- `src/shared`: Reusable, generic components, hooks, and utilities with no business logic.

All communication with the backend is abstracted through custom hooks that use TanStack Query and a centralized `axios` instance.

## Testing

- **Unit/Integration Tests**: Written with [Vitest](https://vitest.dev/) and [React Testing Library](https://testing-library.com/docs/react-testing-library/intro/).
- **End-to-End Tests**: Written with [Playwright](https://playwright.dev/).

## Deployment

The `pnpm build` command generates a `dist` directory containing static HTML, CSS, and JavaScript files. These assets are intended to be served by the backend `ReportingSystem.Service` application.