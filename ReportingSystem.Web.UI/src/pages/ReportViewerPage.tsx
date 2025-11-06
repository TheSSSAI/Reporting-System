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
import { useGeneratedReportsApi } from '../features/reports/api/useGeneratedReportsApi';
import { useDebounce } from '../shared/lib/hooks/useDebounce';

const ReportViewerPage: React.FC = () => {
    const [searchTerm, setSearchTerm] = useState('');
    const debouncedSearchTerm = useDebounce(searchTerm, 300);

    // Placeholder for filter state
    const [filters, setFilters] = useState({});

    const { getGeneratedReports } = useGeneratedReportsApi({
        search: debouncedSearchTerm,
        ...filters,
    });
    const { data, isLoading, isError, error } = getGeneratedReports;
    
    const columns: GridColDef[] = [
        { field: 'reportName', headerName: 'Report Name', flex: 2 },
        { field: 'status', headerName: 'Generation Status', flex: 1 },
        { field: 'format', headerName: 'Format', flex: 0.5 },
        {
            field: 'generationTime',
            headerName: 'Generated At',
            flex: 1.5,
            valueFormatter: (value) => new Date(value as string).toLocaleString(),
        },
        // Actions column would be added here (Download, Re-deliver, etc.)
    ];
    
    if (isError) {
        return <Alert severity="error">Error fetching reports: {error.message}</Alert>;
    }
    
    return (
        <Container maxWidth={false}>
            <PageHeader title="Report Viewer" />
            
            <Stack direction="row" spacing={2} sx={{ mb: 2 }}>
                <TextField
                    label="Search by report name"
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
                {/* Placeholder for filter components */}
                <Button variant="outlined">Filters</Button>
            </Stack>

            <Box sx={{ height: '75vh', width: '100%' }}>
                <DataGrid
                    rows={data?.items || []}
                    columns={columns}
                    rowCount={data?.totalCount || 0}
                    loading={isLoading}
                    paginationMode="server"
                    // onPaginationModelChange={...}
                    pageSizeOptions={[10, 25, 50]}
                    initialState={{
                        pagination: {
                            paginationModel: { page: 0, pageSize: 25 }
                        }
                    }}
                />
            </Box>
        </Container>
    );
};

export default ReportViewerPage;