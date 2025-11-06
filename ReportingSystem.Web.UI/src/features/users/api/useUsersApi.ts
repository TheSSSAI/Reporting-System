import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { apiClient } from '@/shared/api/axios';
import { User, CreateUser, UpdateUser, ApiError } from '@/shared/api/types';
import { AxiosError } from 'axios';

const USERS_QUERY_KEY = 'users';

/**
 * @description Fetches a list of all users.
 * @returns A TanStack Query object for the list of users.
 */
export const useGetUsers = () => {
  return useQuery<User[], AxiosError<ApiError>>({
    queryKey: [USERS_QUERY_KEY],
    queryFn: async () => {
      const response = await apiClient.get<User[]>('/api/v1/users');
      return response.data;
    },
  });
};

/**
 * @description Fetches a single user by their ID.
 * @param userId The ID of the user to fetch.
 * @returns A TanStack Query object for the user's details.
 */
export const useGetUserById = (userId: string | undefined) => {
  return useQuery<User, AxiosError<ApiError>>({
    queryKey: [USERS_QUERY_KEY, userId],
    queryFn: async () => {
      if (!userId) throw new Error('User ID is required.');
      const response = await apiClient.get<User>(`/api/v1/users/${userId}`);
      return response.data;
    },
    enabled: !!userId,
  });
};

/**
 * @description Mutation to create a new user.
 * Invalidates the users list query on success.
 * @returns A TanStack Query mutation object for creating a user.
 */
export const useCreateUser = () => {
  const queryClient = useQueryClient();
  return useMutation<User, AxiosError<ApiError>, CreateUser>({
    mutationFn: async (newUser) => {
      const response = await apiClient.post<User>('/api/v1/users', newUser);
      return response.data;
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [USERS_QUERY_KEY] });
    },
  });
};

/**
 * @description Mutation to update an existing user.
 * Invalidates the users list and specific user detail queries on success.
 * @returns A TanStack Query mutation object for updating a user.
 */
export const useUpdateUser = () => {
  const queryClient = useQueryClient();
  return useMutation<User, AxiosError<ApiError>, { userId: string; data: UpdateUser }>({
    mutationFn: async ({ userId, data }) => {
      const response = await apiClient.put<User>(`/api/v1/users/${userId}`, data);
      return response.data;
    },
    onSuccess: (data) => {
      queryClient.invalidateQueries({ queryKey: [USERS_QUERY_KEY] });
      queryClient.setQueryData([USERS_QUERY_KEY, data.id], data);
    },
  });
};

/**
 * @description Mutation to delete a user.
 * Invalidates the users list query on success.
 * @returns A TanStack Query mutation object for deleting a user.
 */
export const useDeleteUser = () => {
  const queryClient = useQueryClient();
  return useMutation<void, AxiosError<ApiError>, string>({
    mutationFn: async (userId: string) => {
      await apiClient.delete(`/api/v1/users/${userId}`);
    },
    onSuccess: (_, userId) => {
      queryClient.invalidateQueries({ queryKey: [USERS_QUERY_KEY] });
      // Optionally remove the user from the cache immediately
      queryClient.removeQueries({ queryKey: [USERS_QUERY_KEY, userId] });
    },
  });
};