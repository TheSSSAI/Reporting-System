import axios, { AxiosError, InternalAxiosRequestConfig } from 'axios';
import { authStore } from '../../app/store/authStore';

// Define a structured error type for consistent API error handling
export class ApiError extends Error {
  statusCode: number;
  errors: Record<string, string[]> | null;

  constructor(message: string, statusCode: number, errors: Record<string, string[]> | null = null) {
    super(message);
    this.name = 'ApiError';
    this.statusCode = statusCode;
    this.errors = errors;
  }
}

const apiClient = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || '/api/v1',
  headers: {
    'Content-Type': 'application/json',
  },
});

// Request interceptor to inject the JWT token
apiClient.interceptors.request.use(
  (config: InternalAxiosRequestConfig) => {
    const token = authStore.getState().token;
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Response interceptor for centralized error handling
apiClient.interceptors.response.use(
  (response) => {
    // Pass through successful responses
    return response;
  },
  (error: AxiosError) => {
    // Handle specific error cases
    if (error.response) {
      const { status, data } = error.response;
      
      // Critical: Handle session expiration/unauthorized access
      if (status === 401) {
        // Trigger global logout. This will clear user state and redirect to login.
        // This is a critical security feature to prevent the app from operating
        // in a broken state with an invalid token.
        authStore.getState().logout();
        // The RouterProvider will handle the redirect to the login page.
        // Returning a specific error for this case.
        return Promise.reject(new ApiError('Your session has expired. Please log in again.', status));
      }

      // Handle other known error structures from our backend (e.g., validation errors)
      const errorMessage = (data as any)?.message || (data as any)?.error || error.message || 'An unexpected error occurred.';
      const validationErrors = (data as any)?.errors || null;

      return Promise.reject(new ApiError(errorMessage, status, validationErrors));
    } else if (error.request) {
      // The request was made but no response was received (e.g., network error)
      return Promise.reject(new ApiError('Could not connect to the server. Please check your network connection.', 0));
    } else {
      // Something happened in setting up the request that triggered an Error
      return Promise.reject(new ApiError(error.message || 'An unknown error occurred.', -1));
    }
  }
);

export default apiClient;