import React from 'react';
import { Box, Container, Typography, Button } from '@mui/material';
import { Link } from 'react-router-dom';
import { ReportProblem as ReportProblemIcon } from '@mui/icons-material';

const NotFoundPage: React.FC = () => {
    return (
        <Container component="main" maxWidth="sm" sx={{ mt: 8, mb: 4 }}>
            <Box
                sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                    textAlign: 'center',
                    p: 4,
                }}
            >
                <ReportProblemIcon color="error" sx={{ fontSize: 80, mb: 2 }} />
                <Typography variant="h3" component="h1" gutterBottom>
                    404 - Page Not Found
                </Typography>
                <Typography variant="h6" color="text.secondary" paragraph>
                    Sorry, the page you are looking for does not exist.
                </Typography>
                <Typography variant="body1" color="text.secondary" paragraph>
                    It might have been moved or deleted. Please check the URL or go back to the homepage.
                </Typography>
                <Button
                    component={Link}
                    to="/"
                    variant="contained"
                    color="primary"
                    sx={{ mt: 4 }}
                >
                    Go to Homepage
                </Button>
            </Box>
        </Container>
    );
};

export default NotFoundPage;