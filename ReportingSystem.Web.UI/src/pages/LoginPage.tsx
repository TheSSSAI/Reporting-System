import React, { useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { useForm, SubmitHandler } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import * as z from 'zod';
import {
    Container,
    Paper,
    Typography,
    TextField,
    Button,
    Box,
    Alert,
    CircularProgress,
    Stack,
} from '@mui/material';
import { useAuthApi } from '../features/authentication/api/useAuthApi';
import { LoginRequest } from '../shared/api/types';
import { useAuthStore } from '../app/store/authStore';

const loginSchema = z.object({
    username: z.string().min(1, 'Username is required'),
    password: z.string().min(1, 'Password is required'),
});

type LoginFormValues = z.infer<typeof loginSchema>;

const LoginPage: React.FC = () => {
    const navigate = useNavigate();
    const location = useLocation();
    const { login, isLoggingIn } = useAuthApi();
    const [loginError, setLoginError] = useState<string | null>(null);
    const { isAuthenticated } = useAuthStore();

    const from = location.state?.from?.pathname || '/';

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<LoginFormValues>({
        resolver: zodResolver(loginSchema),
    });

    const onSubmit: SubmitHandler<LoginFormValues> = async (data: LoginRequest) => {
        setLoginError(null);
        login(data, {
            onSuccess: (authData) => {
                if (authData.user.role === 'Administrator') {
                    navigate('/dashboard', { replace: true });
                } else {
                    navigate('/reports/viewer', { replace: true });
                }
            },
            onError: (error) => {
                const errorMessage =
                    error.response?.data?.error ||
                    'An unexpected error occurred. Please try again.';
                setLoginError(errorMessage);
            },
        });
    };
    
    // Redirect if already authenticated
    React.useEffect(() => {
        if (isAuthenticated) {
            navigate(from, { replace: true });
        }
    }, [isAuthenticated, navigate, from]);

    return (
        <Container component="main" maxWidth="xs" sx={{ mt: 8 }}>
            <Paper elevation={3} sx={{ padding: 4, display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
                <Typography component="h1" variant="h5">
                    Sign in
                </Typography>
                <Box component="form" onSubmit={handleSubmit(onSubmit)} sx={{ mt: 1, width: '100%' }}>
                    <Stack spacing={2}>
                        {loginError && <Alert severity="error">{loginError}</Alert>}
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            id="username"
                            label="Username"
                            autoComplete="username"
                            autoFocus
                            {...register('username')}
                            error={!!errors.username}
                            helperText={errors.username?.message}
                            disabled={isLoggingIn}
                        />
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            label="Password"
                            type="password"
                            id="password"
                            autoComplete="current-password"
                            {...register('password')}
                            error={!!errors.password}
                            helperText={errors.password?.message}
                            disabled={isLoggingIn}
                        />
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                            disabled={isLoggingIn}
                        >
                            {isLoggingIn ? <CircularProgress size={24} /> : 'Sign In'}
                        </Button>
                    </Stack>
                </Box>
            </Paper>
        </Container>
    );
};

export default LoginPage;