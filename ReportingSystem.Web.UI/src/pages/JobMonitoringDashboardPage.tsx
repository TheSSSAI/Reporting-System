import React from 'react';
import { Container, Box, CircularProgress, Alert, Tooltip, IconButton, Chip } from '@mui/material';
import { DataGrid, GridColDef, GridRenderCellParams } from '@mui/x-data-grid';
import { PageHeader } from '../widgets/PageHeader';
import { useJobsApi } from '../features/jobs/api/useJobsApi';
import { formatDistanceToNow } from 'date-fns';
import { useNavigate } from 'react-router-dom';
import { CheckCircle, Cancel, HourglassEmpty, PlayCircle, StopCircle } from '@mui/icons-material';
import { JobExecutionLog } from '../shared/api/types';

const getStatusChip = (status: JobExecutionLog['status']) => {
    switch (status) {
        case 'Succeeded':
            return <Chip icon={<CheckCircle />} label="Succeeded" color="success" size="small" />;
        case 'Failed':
            return <Chip icon={<Cancel />} label="Failed" color="error" size="small" />;
        case 'Running':
            return <Chip icon={<PlayCircle />} label="Running" color="info" size="small" />;
        case 'Queued':
            return <Chip icon={<HourglassEmpty />} label="Queued" color="default" size="small" />;
        case 'Cancelled':
            return <Chip icon={<StopCircle />} label="Cancelled" color="warning" size="small" />;
        default:
            return <Chip label="Unknown" size="small" />;
    }
};

const JobMonitoringDashboardPage: React.FC = () => {
    const navigate = useNavigate();
    const { getJobs } = useJobsApi({ refetchInterval: 5000 }); // Poll every 5 seconds
    const { data: jobs, isLoading, isError, error } = getJobs;

    const columns: GridColDef[] = [
        { 
            field: 'reportName', 
            headerName: 'Report Name', 
            flex: 2 
        },
        {
            field: 'status',
            headerName: 'Status',
            flex: 1,
            renderCell: (params: GridRenderCellParams) => getStatusChip(params.value as JobExecutionLog['status']),
        },
        {
            field: 'startTime',
            headerName: 'Started',
            flex: 1.5,
            renderCell: (params: GridRenderCellParams) => (
                <Tooltip title={new Date(params.value as string).toLocaleString()}>
                    <span>{formatDistanceToNow(new Date(params.value as string), { addSuffix: true })}</span>
                </Tooltip>
            ),
        },
        {
            field: 'endTime',
            headerName: 'Finished',
            flex: 1.5,
            renderCell: (params: GridRenderCellParams) =>
                params.value ? (
                    <Tooltip title={new Date(params.value as string).toLocaleString()}>
                        <span>{formatDistanceToNow(new Date(params.value as string), { addSuffix: true })}</span>
                    </Tooltip>
                ) : (
                    'N/A'
                ),
        },
        {
            field: 'duration',
            headerName: 'Duration',
            flex: 1,
            valueGetter: (value, row) => {
                if (!row.startTime || !row.endTime) return null;
                const durationMs = new Date(row.endTime).getTime() - new Date(row.startTime).getTime();
                return `${(durationMs / 1000).toFixed(2)}s`;
            },
        },
    ];

    if (isError) {
        return <Alert severity="error">Error fetching job data: {error.message}</Alert>;
    }

    const handleRowClick = (params: any) => {
        navigate(`/jobs/${params.id}`);
    };

    return (
        <Container maxWidth={false}>
            <PageHeader title="Job Monitoring Dashboard" />
            <Box sx={{ height: '80vh', width: '100%' }}>
                <DataGrid
                    rows={jobs || []}
                    columns={columns}
                    loading={isLoading}
                    pageSizeOptions={[25, 50, 100]}
                    initialState={{
                        pagination: {
                          paginationModel: { page: 0, pageSize: 25 },
                        },
                        sorting: {
                            sortModel: [{ field: 'startTime', sort: 'desc' }],
                        },
                    }}
                    onRowClick={handleRowClick}
                    sx={{
                        '& .MuiDataGrid-row:hover': {
                            cursor: 'pointer',
                        },
                    }}
                    disableRowSelectionOnClick
                />
            </Box>
        </Container>
    );
};

export default JobMonitoringDashboardPage;