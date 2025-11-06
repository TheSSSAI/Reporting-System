import React from 'react';
import ReactDOM from 'react-dom/client';
import { App } from './app/App';

// MUI's CSS baseline for a consistent, normalized look across browsers.
// This is a common pattern for MUI applications.
import CssBaseline from '@mui/material/CssBaseline';

// It is a common practice to have a global stylesheet for base styles
// that are not component-specific. While not explicitly defined in the
// file structure, it's a standard part of a React/Vite project.
// If this file is not created, this import can be safely removed, but
// for a production-ready application, it's a sensible inclusion.
// import './index.css';

/**
 * The main entry point for the React application.
 * This file is responsible for:
 * 1. Finding the root DOM element in the `index.html` file.
 * 2. Creating a React root for concurrent rendering (React 18+).
 * 3. Rendering the main `App` component into the root.
 * 4. Wrapping the `App` component in `React.StrictMode` for development-time checks.
 * 5. Applying global styles like MUI's CssBaseline.
 */

const rootElement = document.getElementById('root');

// Enterprise-grade check: Ensure the root element exists before trying to render.
// In a standard Vite + React setup, this should never fail, but it's a robust
// safeguard against misconfiguration of the public/index.html file.
if (!rootElement) {
  throw new Error('Fatal Error: The root element with ID "root" was not found in the DOM. The application cannot be mounted.');
}

// Create the React root using the new React 18 API.
const root = ReactDOM.createRoot(rootElement);

// Render the application.
// React.StrictMode is a tool for highlighting potential problems in an application.
// It activates additional checks and warnings for its descendants. It only runs in
// development mode and does not impact the production build.
root.render(
  <React.StrictMode>
    {/* 
      CssBaseline is a component from MUI that kicks-starts an elegant, 
      consistent, and simple baseline to build upon. It normalizes styles.
    */}
    <CssBaseline />
    {/*
      The App component is the root of the application's component tree.
      It contains all the global providers (Theme, Router, QueryClient, etc.)
      and the main application layout.
    */}
    <App />
  </React.StrictMode>
);