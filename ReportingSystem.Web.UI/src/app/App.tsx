import { CssBaseline, GlobalStyles } from '@mui/material';
import { QueryClientProvider } from '@tanstack/react-query';
import { ErrorBoundary } from 'react-error-boundary';
import { HelmetProvider } from 'react-helmet-async';
import { Toaster } from 'sonner';

import { ThemeProvider } from './providers/ThemeProvider';
import RouterProvider from './providers/RouterProvider';
import { queryClient } from '../shared/api/queryClient';
import ErrorFallback from '../shared/ui/ErrorFallback';

/**
 * The root component of the application.
 *
 * This component is responsible for setting up the global context providers that wrap the entire application.
 * This includes:
 * - ErrorBoundary: Catches rendering errors in the component tree to prevent a white screen of death.
 * - HelmetProvider: Manages document head changes for SEO and accessibility.
 * - QueryClientProvider: Provides the TanStack Query client for server-state management.
 * - ThemeProvider: Provides the MUI theme and handles theme switching.
 * - CssBaseline: Applies a consistent baseline style across browsers.
 * - GlobalStyles: Applies custom global styles for the application.
 * - Toaster: A provider for showing toast notifications.
 * - RouterProvider: Renders the appropriate page based on the current URL.
 *
 * This composition follows the Provider Pattern, making essential services and state available
 * to all descendant components.
 *
 * @returns {JSX.Element} The root JSX element of the application.
 */
function App(): JSX.Element {
  return (
    <ErrorBoundary
      FallbackComponent={ErrorFallback}
      onReset={() => {
        // Here you might want to clear some cache or state before retrying
        queryClient.clear();
        window.location.reload();
      }}
    >
      <HelmetProvider>
        <QueryClientProvider client={queryClient}>
          <ThemeProvider>
            <CssBaseline />
            <GlobalStyles
              styles={{
                '#root': {
                  display: 'flex',
                  flexDirection: 'column',
                  minHeight: '100vh',
                },
              }}
            />
            <Toaster position="bottom-right" richColors />
            <RouterProvider />
          </ThemeProvider>
        </QueryClientProvider>
      </HelmetProvider>
    </ErrorBoundary>
  );
}

export default App;