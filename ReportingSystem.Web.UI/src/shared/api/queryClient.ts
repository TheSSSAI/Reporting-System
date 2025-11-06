import { QueryClient } from '@tanstack/react-query';
import { AxiosError } from 'axios';
import { useAuthStore } from '../../app/store/authStore';

// Define a consistent error structure for API responses
interface ApiError {
  error?: string;
  message?: string;
  statusCode?: number;
}

/**
 * Global error handler for TanStack Query mutations.
 * This function centralizes error handling, particularly for authentication failures.
 * If a 401 Unauthorized response is received, it triggers a global logout.
 * @param error The error object, expected to be an AxiosError.
 */
const handleGlobalError = (error: unknown) => {
  if (error instanceof AxiosError) {
    const apiError = error.response?.data as ApiError;
    console.error('A global API error occurred:', apiError?.message || error.message);

    // If the error is an authentication error, log the user out.
    if (error.response?.status === 401) {
      // Use the store's `getState` method for use outside of React components.
      useAuthStore.getState().logout();
      // Optionally, you can redirect the user here, though it's often better
      // to handle this via a component that listens to `isAuthenticated`.
      // window.location.href = '/login';
    }
  } else {
    console.error('An unexpected error occurred:', error);
  }
};

/**
 * A singleton instance of QueryClient with pre-configured default options.
 * This client is used by the QueryClientProvider at the root of the application
 * to manage all server-state, including data fetching, caching, and mutations.
 */
export const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      // Set a default staleTime to prevent excessive refetching.
      // Data will be considered fresh for 5 minutes.
      staleTime: 1000 * 60 * 5,
      
      // Keep data in the cache for 1 hour.
      gcTime: 1000 * 60 * 60,

      // Disable refetching on window focus by default, which is often
      // better for admin panels to prevent unexpected data reloads.
      refetchOnWindowFocus: false,

      // Retry failed queries once by default.
      retry: 1,

      // Globally handle errors for queries.
      onError: handleGlobalError,
    },
    mutations: {
      // Globally handle errors for mutations.
      onError: handleGlobalError,
    },
  },
});