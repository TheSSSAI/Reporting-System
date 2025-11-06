import { useMutation } from '@tanstack/react-query';
import { apiClient } from '@/shared/api/axios';
import { useAuthStore } from '@/app/store/authStore';
import { ApiError, LoginRequest, LoginResponse, User } from '@/shared/api/types';
import { AxiosError } from 'axios';

// This function decodes the JWT to extract user information.
// In a real application, you might want a more robust library like 'jwt-decode'.
const decodeJwt = (token: string): User | null => {
  try {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split('')
        .map((c) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
        .join('')
    );

    const decoded = JSON.parse(jsonPayload);
    // Adjust keys to match the User type
    return {
      id: decoded.sub,
      username: decoded.name,
      role: decoded.role,
    };
  } catch (error) {
    console.error('Failed to decode JWT:', error);
    return null;
  }
};

/**
 * @description Custom hook for handling user authentication API calls.
 * Provides mutations for login and logout.
 */
export const useAuthApi = () => {
  const { login: storeLogin, logout: storeLogout } = useAuthStore();

  /**
   * @description Mutation for user login.
   * On success, it decodes the JWT, updates the global auth store, and configures the API client.
   * @returns A TanStack Query mutation object for login.
   */
  const useLogin = () =>
    useMutation<LoginResponse, AxiosError<ApiError>, LoginRequest>({
      mutationFn: async (credentials: LoginRequest) => {
        const response = await apiClient.post<LoginResponse>('/api/v1/auth/token', credentials);
        return response.data;
      },
      onSuccess: (data) => {
        const user = decodeJwt(data.accessToken);
        if (user) {
          storeLogin(data.accessToken, data.refreshToken, user);
        } else {
          // Handle case where token is invalid or can't be decoded
          storeLogout();
          throw new Error('Invalid token received from server.');
        }
      },
    });

  /**
   * @description Function to handle user logout.
   * It calls the auth store to clear the session state.
   * In a real app, this could also call a backend endpoint to invalidate the refresh token.
   */
  const logout = () => {
    // Here you could also have a mutation to call a backend logout endpoint
    // e.g., to invalidate the refresh token on the server side.
    storeLogout();
  };

  return { useLogin, logout };
};