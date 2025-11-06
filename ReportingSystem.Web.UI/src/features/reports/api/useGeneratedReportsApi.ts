import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { apiClient } from '@/shared/api/axios';
import { GeneratedReport, ApiError, PaginatedResponse } from '@/shared/api/types';
import { AxiosError } from 'axios';

const GENERATED_REPORTS_QUERY_KEY = 'generatedReports';

/**
 * @description Fetches a paginated list of generated reports for the Report Viewer.
 * @param page The page number to fetch.
 * @param pageSize The number of items per page.
 * @param filters Optional filters for status, date range, search term, etc.
 * @returns A TanStack Query object for the list of generated reports.
 */
export const useGetGeneratedReports = (page: number = 1, pageSize: number = 25, filters: object = {}) => {
  return useQuery<PaginatedResponse<GeneratedReport>, AxiosError<ApiError>>({
    queryKey: [GENERATED_REPORTS_QUERY_KEY, { page, pageSize, ...filters }],
    queryFn: async () => {
      const params = new URLSearchParams({
        page: page.toString(),
        pageSize: pageSize.toString(),
        ...filters,
      });
      const response = await apiClient.get<PaginatedResponse<GeneratedReport>>(`/api/v1/generated-reports?${params.toString()}`);
      return response.data;
    },
    placeholderData: (previousData) => previousData,
  });
};

/**
 * @description Fetches the content of a generated report (e.g., for HTML preview).
 * @param jobId The job ID of the generated report.
 * @returns A TanStack Query object for the report content.
 */
export const useGetGeneratedReportContent = (jobId: string | undefined) => {
    return useQuery<string, AxiosError<ApiError>>({
        queryKey: [GENERATED_REPORTS_QUERY_KEY, jobId, 'content'],
        queryFn: async () => {
            if (!jobId) throw new Error('Job ID is required.');
            const response = await apiClient.get<string>(`/api/v1/jobs/${jobId}/result-content`);
            return response.data;
        },
        enabled: !!jobId,
    });
};


/**
 * @description Function to get the download URL for a generated report.
 * @param jobId The job ID of the report to download.
 * @returns The full URL to trigger the file download.
 */
export const getGeneratedReportDownloadUrl = (jobId: string): string => {
    // This assumes the API base URL is relative or correctly configured in the axios instance.
    const baseUrl = apiClient.defaults.baseURL || window.location.origin;
    return `${baseUrl}/api/v1/jobs/${jobId}/result`;
};


/**
 * @description Mutation to delete multiple generated reports.
 * Invalidates the generated reports list query on success.
 * @returns A TanStack Query mutation object for bulk deletion.
 */
export const useBulkDeleteGeneratedReports = () => {
  const queryClient = useQueryClient();
  return useMutation<void, AxiosError<ApiError>, string[]>({
    mutationFn: async (jobIds: string[]) => {
      await apiClient.delete('/api/v1/generated-reports', { data: { jobIds } });
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [GENERATED_REPORTS_QUERY_KEY] });
    },
  });
};

/**
 * @description Mutation to re-deliver one or more generated reports.
 * This is a fire-and-forget operation from the UI's perspective.
 * @returns A TanStack Query mutation object for bulk re-delivery.
 */
export const useReDeliverGeneratedReports = () => {
  return useMutation<void, AxiosError<ApiError>, string[]>({
    mutationFn: async (jobIds: string[]) => {
      await apiClient.post('/api/v1/generated-reports/re-deliver', { jobIds });
    },
    // No query invalidation is strictly necessary, but you might want to show a notification.
  });
};