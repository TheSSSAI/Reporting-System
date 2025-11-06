import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { apiClient } from '@/shared/api/axios';
import {
  ConnectorConfiguration,
  CreateConnectorConfiguration,
  UpdateConnectorConfiguration,
  TestConnectionRequest,
  TestConnectionResponse,
  ApiError,
  ConnectorType,
} from '@/shared/api/types';
import { AxiosError } from 'axios';

const CONNECTORS_QUERY_KEY = 'connectors';
const CONNECTOR_TYPES_QUERY_KEY = 'connectorTypes';

/**
 * @description Fetches a list of all configured data connectors.
 * @returns A TanStack Query object for the list of connectors.
 */
export const useGetConnectors = () => {
  return useQuery<ConnectorConfiguration[], AxiosError<ApiError>>({
    queryKey: [CONNECTORS_QUERY_KEY],
    queryFn: async () => {
      const response = await apiClient.get<ConnectorConfiguration[]>('/api/v1/connectors');
      return response.data;
    },
  });
};

/**
 * @description Fetches the details of a single connector by its ID.
 * @param connectorId The ID of the connector to fetch.
 * @returns A TanStack Query object for the connector details.
 */
export const useGetConnectorById = (connectorId: string | undefined) => {
  return useQuery<ConnectorConfiguration, AxiosError<ApiError>>({
    queryKey: [CONNECTORS_QUERY_KEY, connectorId],
    queryFn: async () => {
      if (!connectorId) throw new Error('Connector ID is required.');
      const response = await apiClient.get<ConnectorConfiguration>(`/api/v1/connectors/${connectorId}`);
      return response.data;
    },
    enabled: !!connectorId,
  });
};

/**
 * @description Mutation to create a new data connector.
 * Invalidates the connectors list query on success.
 * @returns A TanStack Query mutation object for creating a connector.
 */
export const useCreateConnector = () => {
  const queryClient = useQueryClient();
  return useMutation<ConnectorConfiguration, AxiosError<ApiError>, CreateConnectorConfiguration>({
    mutationFn: async (newConnector) => {
      const response = await apiClient.post<ConnectorConfiguration>('/api/v1/connectors', newConnector);
      return response.data;
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [CONNECTORS_QUERY_KEY] });
    },
  });
};

/**
 * @description Mutation to update an existing data connector.
 * Invalidates relevant queries on success.
 * @returns A TanStack Query mutation object for updating a connector.
 */
export const useUpdateConnector = () => {
  const queryClient = useQueryClient();
  return useMutation<ConnectorConfiguration, AxiosError<ApiError>, { connectorId: string; data: UpdateConnectorConfiguration }>({
    mutationFn: async ({ connectorId, data }) => {
      const response = await apiClient.put<ConnectorConfiguration>(`/api/v1/connectors/${connectorId}`, data);
      return response.data;
    },
    onSuccess: (data) => {
      queryClient.invalidateQueries({ queryKey: [CONNECTORS_QUERY_KEY] });
      queryClient.setQueryData([CONNECTORS_QUERY_KEY, data.id], data);
    },
  });
};

/**
 * @description Mutation to delete a data connector.
 * Invalidates the connectors list query on success.
 * @returns A TanStack Query mutation object for deleting a connector.
 */
export const useDeleteConnector = () => {
  const queryClient = useQueryClient();
  return useMutation<void, AxiosError<ApiError>, string>({
    mutationFn: async (connectorId) => {
      await apiClient.delete(`/api/v1/connectors/${connectorId}`);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [CONNECTORS_QUERY_KEY] });
    },
  });
};

/**
 * @description Mutation to test a connector's connection settings.
 * This is a transient operation and does not affect query caches.
 * @returns A TanStack Query mutation object for testing a connection.
 */
export const useTestConnection = () => {
  return useMutation<TestConnectionResponse, AxiosError<ApiError>, TestConnectionRequest>({
    mutationFn: async (testRequest) => {
      const response = await apiClient.post<TestConnectionResponse>('/api/v1/connectors/test', testRequest);
      return response.data;
    },
  });
};

/**
 * @description Fetches the available connector types (built-in and custom).
 * @returns A TanStack Query object for the list of connector types.
 */
export const useGetConnectorTypes = () => {
    return useQuery<ConnectorType[], AxiosError<ApiError>>({
        queryKey: [CONNECTOR_TYPES_QUERY_KEY],
        queryFn: async () => {
            const response = await apiClient.get<ConnectorType[]>('/api/v1/connectors/types');
            return response.data;
        },
    });
};