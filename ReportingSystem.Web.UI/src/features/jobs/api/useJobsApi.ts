import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { apiClient } from '@/shared/api/axios';
import { JobExecutionLog, ApiError, PaginatedResponse } from '@/shared/api/types';
import { AxiosError } from 'axios';

const JOBS_QUERY_KEY = 'jobs';

/**
 * @description Fetches a paginated list of job execution logs.
 * @param page The page number to fetch.
 * @param pageSize The number of items per page.
 * @param filters Optional filters for status, date range, etc.
 * @returns A TanStack Query object for the list of jobs.
 */
export const useGetJobs = (page: number = 1, pageSize: number = 25, filters: object = {}) => {
  return useQuery<PaginatedResponse<JobExecutionLog>, AxiosError<ApiError>>({
    queryKey: [JOBS_QUERY_KEY, { page, pageSize, ...filters }],
    queryFn: async () => {
      const params = new URLSearchParams({
        page: page.toString(),
        pageSize: pageSize.toString(),
        ...filters,
      });
      const response = await apiClient.get<PaginatedResponse<JobExecutionLog>>(`/api/v1/jobs?${params.toString()}`);
      return response.data;
    },
    placeholderData: (previousData) => previousData,
  });
};

/**
 * @description Fetches the details of a single job execution log by its ID.
 * @param jobId The ID of the job to fetch.
 * @returns A TanStack Query object for the job's details.
 */
export const useGetJobById = (jobId: string | undefined) => {
  return useQuery<JobExecutionLog, AxiosError<ApiError>>({
    queryKey: [JOBS_QUERY_KEY, jobId],
    queryFn: async () => {
      if (!jobId) throw new Error('Job ID is required.');
      const response = await apiClient.get<JobExecutionLog>(`/api/v1/jobs/${jobId}`);
      return response.data;
    },
    enabled: !!jobId,
  });
};

/**
 * @description Fetches the detailed log entries for a specific job execution.
 * @param jobId The ID of the job.
 * @returns A TanStack Query object for the job's logs.
 */
export const useGetJobLogs = (jobId: string | undefined) => {
    // Assuming the detailed logs are part of the JobExecutionLog object or a separate endpoint
    // Let's assume a separate endpoint for this example.
    return useQuery<string[], AxiosError<ApiError>>({
        queryKey: [JOBS_QUERY_KEY, jobId, 'logs'],
        queryFn: async () => {
            if (!jobId) throw new Error('Job ID is required.');
            const response = await apiClient.get<string[]>(`/api/v1/jobs/${jobId}/logs`);
            return response.data;
        },
        enabled: !!jobId,
    });
};

/**
 * @description Mutation to cancel a running or queued job.
 * Invalidates the jobs list query on success.
 * @returns A TanStack Query mutation object for cancelling a job.
 */
export const useCancelJob = () => {
  const queryClient = useQueryClient();
  return useMutation<void, AxiosError<ApiError>, string>({
    mutationFn: async (jobId: string) => {
      await apiClient.post(`/api/v1/jobs/${jobId}/cancel`);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [JOBS_QUERY_KEY] });
    },
  });
};

/**
 * @description Mutation to retry a failed job.
 * Invalidates the jobs list query on success to show the new queued job.
 * @returns A TanStack Query mutation object for retrying a job.
 */
export const useRetryJob = () => {
  const queryClient = useQueryClient();
  return useMutation<JobExecutionLog, AxiosError<ApiError>, string>({
    mutationFn: async (jobId: string) => {
      const response = await apiClient.post<JobExecutionLog>(`/api/v1/jobs/${jobId}/retry`);
      return response.data;
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [JOBS_QUERY_KEY] });
    },
  });
};

/**
 * @description Fetches the status of an asynchronous job.
 * This is used for polling after an async report generation is triggered.
 * @param jobId The ID of the job to poll.
 * @returns A TanStack Query object for the job's status.
 */
export const useGetJobStatus = (jobId: string | null | undefined) => {
    return useQuery<JobExecutionLog, AxiosError<ApiError>>({
        queryKey: [JOBS_QUERY_KEY, 'status', jobId],
        queryFn: async () => {
            if (!jobId) throw new Error('Job ID is required.');
            const response = await apiClient.get<JobExecutionLog>(`/api/v1/jobs/${jobId}`);
            return response.data;
        },
        enabled: !!jobId,
        // Optional: configure refetching for polling
        refetchInterval: (query) => {
            const status = query.state.data?.status;
            if (status === 'Succeeded' || status === 'Failed' || status === 'Cancelled') {
                return false; // Stop polling on terminal status
            }
            return 5000; // Poll every 5 seconds
        },
    });
};