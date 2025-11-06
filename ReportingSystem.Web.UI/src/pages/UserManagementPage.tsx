import React, { useState } from 'react';
import {
    Box,
    Button,
    Container,
    Typography,
    CircularProgress,
    Alert,
    IconButton,
    Tooltip,
} from '@mui/material';
import { DataGrid, GridColDef, GridRenderCellParams } from '@mui/x-data-grid';
import { Add as AddIcon, Edit as EditIcon, Delete as DeleteIcon, LockOpen, VpnKey } from '@mui/icons-material';
import { useUsersApi } from '../features/users/api/useUsersApi';
import { PageHeader } from '../widgets/PageHeader';
import { User } from '../shared/api/types';
import { ConfirmationModal } from '../shared/ui/ConfirmationModal';
// Assume a UserFormModal component exists for create/edit operations
// import { UserFormModal } from '../features/users/components/UserFormModal';

const UserManagementPage: React.FC = () => {
    const { getUsers, deleteUser, unlockUser, resetPassword } = useUsersApi();
    const { data: users, isLoading, isError, error } = getUsers();

    const [modalState, setModalState] = useState<{
        delete?: User;
        unlock?: User;
        resetPassword?: User;
    }>({});

    const [userToEdit, setUserToEdit] = useState<User | null>(null);
    const [isCreateModalOpen, setCreateModalOpen] = useState(false);
    
    const handleDelete = (user: User) => {
        deleteUser.mutate(user.id, {
            onSuccess: () => {
                setModalState({});
                // In a real app, you'd show a success toast
            },
            onError: (e) => {
                console.error("Delete failed:", e);
                // show error toast
            }
        });
    };

    const handleUnlock = (user: User) => {
        unlockUser.mutate(user.id, {
            onSuccess: () => setModalState({}),
        });
    };

    const handleResetPassword = (user: User) => {
        resetPassword.mutate(user.id, {
            onSuccess: (tempPassword) => {
                // In a real app, you'd show this in a more secure way
                alert(`Temporary password for ${user.username}: ${tempPassword}`);
                setModalState({});
            }
        });
    };
    
    const columns: GridColDef[] = [
        { field: 'username', headerName: 'Username', flex: 1 },
        { field: 'fullName', headerName: 'Full Name', flex: 1.5 },
        { field: 'email', headerName: 'Email', flex: 2 },
        { field: 'role', headerName: 'Role', flex: 1 },
        {
            field: 'status',
            headerName: 'Status',
            flex: 1,
            renderCell: (params: GridRenderCellParams) => (
                <Typography color={params.value === 'Locked' ? 'error' : 'success'}>
                    {params.value}
                </Typography>
            ),
        },
        {
            field: 'actions',
            headerName: 'Actions',
            sortable: false,
            filterable: false,
            disableColumnMenu: true,
            flex: 2,
            renderCell: (params: GridRenderCellParams) => {
                const user = params.row as User;
                // Simplified logic: assume primary admin has id '1' and cannot be deleted/role changed
                const isDeletable = user.id !== '1' && user.username !== 'admin';

                return (
                    <Box>
                        <Tooltip title="Edit">
                            <IconButton onClick={() => setUserToEdit(user)}>
                                <EditIcon />
                            </IconButton>
                        </Tooltip>
                        {isDeletable && (
                             <Tooltip title="Delete">
                                <IconButton onClick={() => setModalState({ delete: user })}>
                                    <DeleteIcon />
                                </IconButton>
                            </Tooltip>
                        )}
                        <Tooltip title="Reset Password">
                            <IconButton onClick={() => setModalState({ resetPassword: user })}>
                                <VpnKey />
                            </IconButton>
                        </Tooltip>
                        {user.status === 'Locked' && (
                             <Tooltip title="Unlock Account">
                                <IconButton onClick={() => setModalState({ unlock: user })}>
                                    <LockOpen />
                                </IconButton>
                            </Tooltip>
                        )}
                    </Box>
                );
            },
        },
    ];

    if (isLoading) {
        return <CircularProgress />;
    }

    if (isError) {
        return <Alert severity="error">Error fetching users: {error.message}</Alert>;
    }

    // Map data for DataGrid, including a dummy status for now
    const rows = users?.map(u => ({...u, status: u.lockoutEnd ? 'Locked' : 'Active'})) || [];

    return (
        <Container maxWidth={false}>
            <PageHeader
                title="User Management"
                actions={
                    <Button
                        variant="contained"
                        startIcon={<AddIcon />}
                        onClick={() => setCreateModalOpen(true)}
                    >
                        Create User
                    </Button>
                }
            />
            <Box sx={{ height: '75vh', width: '100%' }}>
                <DataGrid
                    rows={rows}
                    columns={columns}
                    pageSizeOptions={[10, 25, 50]}
                    initialState={{
                        pagination: {
                          paginationModel: { page: 0, pageSize: 10 },
                        },
                      }}
                    disableRowSelectionOnClick
                    loading={isLoading}
                />
            </Box>
            
            {/* Confirmation Modals */}
            <ConfirmationModal
                open={!!modalState.delete}
                title="Delete User"
                message={`Are you sure you want to permanently delete the user "${modalState.delete?.username}"? This action cannot be undone.`}
                onConfirm={() => modalState.delete && handleDelete(modalState.delete)}
                onCancel={() => setModalState({})}
                confirmButtonText="Delete"
                confirmButtonColor="error"
            />
            <ConfirmationModal
                open={!!modalState.unlock}
                title="Unlock User Account"
                message={`Are you sure you want to unlock the account for user "${modalState.unlock?.username}"?`}
                onConfirm={() => modalState.unlock && handleUnlock(modalState.unlock)}
                onCancel={() => setModalState({})}
                confirmButtonText="Unlock"
            />
             <ConfirmationModal
                open={!!modalState.resetPassword}
                title="Reset User Password"
                message={`Are you sure you want to reset the password for user "${modalState.resetPassword?.username}"? A new temporary password will be generated.`}
                onConfirm={() => modalState.resetPassword && handleResetPassword(modalState.resetPassword)}
                onCancel={() => setModalState({})}
                confirmButtonText="Reset"
            />

            {/*
            // Create/Edit Modal would be rendered here
            <UserFormModal
                open={isCreateModalOpen || !!userToEdit}
                userToEdit={userToEdit}
                onClose={() => {
                    setCreateModalOpen(false);
                    setUserToEdit(null);
                }}
            />
            */}
        </Container>
    );
};

export default UserManagementPage;