import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { apiClient } from '@/shared/api/axios';
import { 
    ReportConfiguration, 
    CreateReportConfiguration, 
    UpdateReportConfiguration, 
    ApiError, 
    JobInitiationResponse 
} from '@/shared/api/types';
import { AxiosError } from 'axios';

const REPORTS_QUERY_KEY = 'reports';

/**
 * @description Fetches a list of all report configurations.
 * @returns A TanStack Query object for the list of report configurations.
 */
export const useGetReportConfigurations = () => {
  return useQuery<ReportConfiguration[], AxiosError<ApiError>>({
    queryKey: [REPORTS_QUERY_KEY],
    queryFn: async () => {
      const response = await apiClient.get<ReportConfiguration[]>('/api/v1/reports');
      return response.data;
    },
  });
};

/**
 * @description Fetches a single report configuration by its ID.
 * @param reportId The ID of the report configuration to fetch.
 * @returns A TanStack Query object for the report configuration details.
 */
export const useGetReportConfigurationById = (reportId: string | undefined) => {
  return useQuery<ReportConfiguration, AxiosError<ApiError>>({
    queryKey: [REPORTS_QUERY_KEY, reportId],
    queryFn: async () => {
      if (!reportId) throw new Error('Report ID is required.');
      const response = await apiClient.get<ReportConfiguration>(`/api/v1/reports/${reportId}`);
      return response.data;
    },
    enabled: !!reportId,
  });
};

/**
 * @description Mutation to create a new report configuration.
 * Invalidates the report configurations list query on success.
 * @returns A TanStack Query mutation object for creating a report configuration.
 */
export const useCreateReportConfiguration = () => {
  const queryClient = useQueryClient();
  return useMutation<ReportConfiguration, AxiosError<ApiError>, CreateReportConfiguration>({
    mutationFn: async (newReport) => {
      const response = await apiClient.post<ReportConfiguration>('/api/v1/reports', newReport);
      return response.data;
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [REPORTS_QUERY_KEY] });
    },
  });
};

/**
 * @description Mutation to update an existing report configuration.
 * Invalidates relevant queries on success.
 * @returns A TanStack Query mutation object for updating a report configuration.
 */
export const useUpdateReportConfiguration = () => {
  const queryClient = useQueryClient();
  return useMutation<ReportConfiguration, AxiosError<ApiError>, { reportId: string; data: UpdateReportConfiguration }>({
    mutationFn: async ({ reportId, data }) => {
      const response = await apiClient.put<ReportConfiguration>(`/api/v1/reports/${reportId}`, data);
      return response.data;
    },
    onSuccess: (data) => {
      queryClient.invalidateQueries({ queryKey: [REPORTS_QUERY_KEY] });
      queryClient.setQueryData([REPORTS_QUERY_KEY, data.id], data);
    },
  });
};

/**
 * @description Mutation to delete a report configuration.
 * Invalidates the report configurations list query on success.
 * @returns A TanStack Query mutation object for deleting a report configuration.
 */
export const useDeleteReportConfiguration = () => {
  const queryClient = useQueryClient();
  return useMutation<void, AxiosError<ApiError>, string>({
    mutationFn: async (reportId) => {
      await apiClient.delete(`/api/v1/reports/${reportId}`);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [REPORTS_QUERY_KEY] });
    },
  });
};

/**
 * @description Mutation to trigger an on-demand report generation.
 * Handles both synchronous (blob response) and asynchronous (JSON response) cases.
 * @returns A TanStack Query mutation object for generating a report.
 */
export const useGenerateReport = () => {
    return useMutation<Blob | JobInitiationResponse, AxiosError<ApiError>, string>({
      mutationFn: async (reportId) => {
        const response = await apiClient.post(`/api/v1/reports/${reportId}/generate`, {}, {
          // We expect either a file blob or a JSON object, so responseType needs to be flexible.
          // Let axios decide based on Content-Type, or handle blob specifically.
          responseType: 'blob', // A good default for file downloads
        });
        
        // If the response is JSON (async case), it will be a blob that we need to parse.
        if (response.headers['content-type']?.includes('application/json')) {
            const jsonText = await response.data.text();
            return JSON.parse(jsonText) as JobInitiationResponse;
        }

        // Otherwise, it's the file content (sync case)
        return response.data as Blob;
      },
    });
};