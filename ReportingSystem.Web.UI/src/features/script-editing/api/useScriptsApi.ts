import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { apiClient } from '@/shared/api/axios';
import {
  TransformationScript,
  CreateTransformationScript,
  UpdateTransformationScript,
  ScriptPreviewRequest,
  ScriptPreviewResponse,
  ApiError,
  ScriptVersion,
} from '@/shared/api/types';
import { AxiosError } from 'axios';

const SCRIPTS_QUERY_KEY = 'scripts';

/**
 * @description Fetches a list of all transformation scripts.
 * @returns A TanStack Query object for the list of scripts.
 */
export const useGetScripts = () => {
  return useQuery<TransformationScript[], AxiosError<ApiError>>({
    queryKey: [SCRIPTS_QUERY_KEY],
    queryFn: async () => {
      const response = await apiClient.get<TransformationScript[]>('/api/v1/transformations');
      return response.data;
    },
  });
};

/**
 * @description Fetches the details of a single transformation script by its ID.
 * @param scriptId The ID of the script to fetch.
 * @returns A TanStack Query object for the script details.
 */
export const useGetScriptById = (scriptId: string | undefined) => {
  return useQuery<TransformationScript, AxiosError<ApiError>>({
    queryKey: [SCRIPTS_QUERY_KEY, scriptId],
    queryFn: async () => {
      if (!scriptId) throw new Error('Script ID is required.');
      const response = await apiClient.get<TransformationScript>(`/api/v1/transformations/${scriptId}`);
      return response.data;
    },
    enabled: !!scriptId, // Only run the query if scriptId is provided
  });
};

/**
 * @description Mutation to create a new transformation script.
 * Invalidates the scripts list query on success.
 * @returns A TanStack Query mutation object for creating a script.
 */
export const useCreateScript = () => {
  const queryClient = useQueryClient();
  return useMutation<TransformationScript, AxiosError<ApiError>, CreateTransformationScript>({
    mutationFn: async (newScript) => {
      const response = await apiClient.post<TransformationScript>('/api/v1/transformations', newScript);
      return response.data;
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [SCRIPTS_QUERY_KEY] });
    },
  });
};

/**
 * @description Mutation to update an existing transformation script.
 * Invalidates the scripts list and specific script detail queries on success.
 * @returns A TanStack Query mutation object for updating a script.
 */
export const useUpdateScript = () => {
  const queryClient = useQueryClient();
  return useMutation<TransformationScript, AxiosError<ApiError>, { scriptId: string; data: UpdateTransformationScript }>({
    mutationFn: async ({ scriptId, data }) => {
      const response = await apiClient.put<TransformationScript>(`/api/v1/transformations/${scriptId}`, data);
      return response.data;
    },
    onSuccess: (data) => {
      queryClient.invalidateQueries({ queryKey: [SCRIPTS_QUERY_KEY] });
      queryClient.setQueryData([SCRIPTS_QUERY_KEY, data.id], data);
    },
  });
};

/**
 * @description Mutation to delete a transformation script.
 * Invalidates the scripts list query on success.
 * @returns A TanStack Query mutation object for deleting a script.
 */
export const useDeleteScript = () => {
  const queryClient = useQueryClient();
  return useMutation<void, AxiosError<ApiError>, string>({
    mutationFn: async (scriptId) => {
      await apiClient.delete(`/api/v1/transformations/${scriptId}`);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [SCRIPTS_QUERY_KEY] });
    },
  });
};

/**
 * @description Mutation to preview a transformation script.
 * This is a transient operation and does not affect query caches.
 * @returns A TanStack Query mutation object for script preview.
 */
export const usePreviewScript = () => {
  return useMutation<ScriptPreviewResponse, AxiosError<ApiError>, ScriptPreviewRequest>({
    mutationFn: async (previewRequest) => {
      const response = await apiClient.post<ScriptPreviewResponse>('/api/v1/transformations/preview', previewRequest);
      return response.data;
    },
  });
};

/**
 * @description Fetches the version history for a script.
 * @param scriptId The ID of the script.
 * @returns A TanStack Query object for the script's version history.
 */
export const useGetScriptVersions = (scriptId: string | undefined) => {
    return useQuery<ScriptVersion[], AxiosError<ApiError>>({
        queryKey: [SCRIPTS_QUERY_KEY, scriptId, 'versions'],
        queryFn: async () => {
            if (!scriptId) throw new Error('Script ID is required.');
            const response = await apiClient.get<ScriptVersion[]>(`/api/v1/transformations/${scriptId}/versions`);
            return response.data;
        },
        enabled: !!scriptId,
    });
};

/**
 * @description Mutation to revert a script to a previous version.
 * @returns A TanStack Query mutation object for reverting a script.
 */
export const useRevertScriptVersion = () => {
    const queryClient = useQueryClient();
    return useMutation<void, AxiosError<ApiError>, { scriptId: string; versionId: string }>({
        mutationFn: async ({ scriptId, versionId }) => {
            await apiClient.post(`/api/v1/transformations/${scriptId}/versions/${versionId}/revert`);
        },
        onSuccess: (_, { scriptId }) => {
            queryClient.invalidateQueries({ queryKey: [SCRIPTS_QUERY_KEY, scriptId] });
            queryClient.invalidateQueries({ queryKey: [SCRIPTS_QUERY_KEY, scriptId, 'versions'] });
        },
    });
};