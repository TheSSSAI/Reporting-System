import React, { useState } from 'react';
import {
    Container,
    Box,
    CircularProgress,
    Alert,
    TextField,
    InputAdornment,
    Stack,
    Button,
} from '@mui/material';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import { Search as SearchIcon } from '@mui/icons-material';
import { PageHeader } from '../widgets/PageHeader';
// This assumes an API hook exists in features/auditing/api/useAuditApi.ts
// import { useAuditApi } from '../features/auditing/api/useAuditApi';
import { useDebounce } from '../shared/lib/hooks/useDebounce';
import { AuditLog } from '../shared/api/types';

// Mock hook until level 3 is generated
const useAuditApi = (options: any) => {
    console.log(options);
    return {
        getAuditLogs: {
            data: { 
                items: [
                    { id: 1, timestamp: new Date().toISOString(), userId: 'user-1', username: 'admin', actionType: 'LOGIN_SUCCESS', details: 'User logged in successfully', sourceIp: '127.0.0.1' },
                    { id: 2, timestamp: new Date().toISOString(), userId: 'user-2', username: 'viewer', actionType: 'REPORT_DOWNLOAD', details: 'Downloaded report "Sales"', sourceIp: '192.168.1.100' }
                ] as AuditLog[], 
                totalCount: 2 
            },
            isLoading: false,
            isError: false,
            error: null,
        }
    }
};


const AuditLogViewer: React.FC = () => {
    const [searchTerm, setSearchTerm] = useState('');
    const debouncedSearchTerm = useDebounce(searchTerm, 300);

    const [filters, setFilters] = useState({});

    const { getAuditLogs } = useAuditApi({
        search: debouncedSearchTerm,
        ...filters,
    });

    const { data, isLoading, isError, error } = getAuditLogs;

    const columns: GridColDef<AuditLog>[] = [
        {
            field: 'timestamp',
            headerName: 'Timestamp',
            flex: 1.5,
            valueFormatter: (value) => new Date(value as string).toLocaleString(),
        },
        { field: 'username', headerName: 'User', flex: 1 },
        { field: 'actionType', headerName: 'Action', flex: 1.5 },
        { field: 'details', headerName: 'Details', flex: 3 },
        { field: 'sourceIp', headerName: 'Source IP', flex: 1 },
    ];

    if (isError) {
        return <Alert severity="error">Error fetching audit logs: {error?.message}</Alert>;
    }

    return (
        <Container maxWidth={false}>
            <PageHeader
                title="Audit Log Viewer"
                actions={
                    <Button variant="contained">
                        Export Logs
                    </Button>
                }
            />

            <Stack direction="row" spacing={2} sx={{ mb: 2 }}>
                <TextField
                    label="Search by user or action"
                    variant="outlined"
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                    InputProps={{
                        startAdornment: (
                            <InputAdornment position="start">
                                <SearchIcon />
                            </InputAdornment>
                        ),
                    }}
                    sx={{ flexGrow: 1 }}
                />
                <Button variant="outlined">Filters</Button>
            </Stack>

            <Box sx={{ height: '75vh', width: '100%' }}>
                <DataGrid
                    rows={data?.items || []}
                    columns={columns}
                    rowCount={data?.totalCount || 0}
                    loading={isLoading}
                    paginationMode="server"
                    pageSizeOptions={[25, 50, 100]}
                    initialState={{
                        pagination: {
                            paginationModel: { page: 0, pageSize: 25 },
                        },
                        sorting: {
                            sortModel: [{ field: 'timestamp', sort: 'desc' }],
                        }
                    }}
                />
            </Box>
        </Container>
    );
};

export default AuditLogViewer;