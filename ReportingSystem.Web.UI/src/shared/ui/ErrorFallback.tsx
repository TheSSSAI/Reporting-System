import React from 'react';
import { Box, Typography, Button, Paper, Alert, AlertTitle } from '@mui/material';
import { FallbackProps } from 'react-error-boundary';
import ReportProblemIcon from '@mui/icons-material/ReportProblem';

/**
 * A generic, user-friendly UI component to display when a React rendering error occurs.
 * Meant to be used with react-error-boundary to prevent the entire application from crashing.
 *
 * @param {FallbackProps} props - Props provided by react-error-boundary, including the error and a reset function.
 * @returns {React.ReactElement} The rendered error fallback component.
 */
const ErrorFallback: React.FC<FallbackProps> = ({ error, resetErrorBoundary }) => {
  return (
    <Box
      sx={{
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        height: '100vh',
        p: 2,
      }}
      role="alert"
    >
      <Paper elevation={3} sx={{ p: 4, maxWidth: '600px', textAlign: 'center' }}>
        <ReportProblemIcon color="error" sx={{ fontSize: 48, mb: 2 }} />
        <Typography variant="h5" component="h1" gutterBottom>
          Something went wrong
        </Typography>
        <Typography variant="body1" color="text.secondary" sx={{ mb: 3 }}>
          An unexpected error occurred in the application. You can try to recover by clicking the button below.
        </Typography>
        
        <Alert severity="error" sx={{ textAlign: 'left', mb: 3 }}>
          <AlertTitle>Error Details</AlertTitle>
          <Typography variant="caption" component="pre" sx={{ whiteSpace: 'pre-wrap', wordBreak: 'break-all' }}>
            {error.message || 'No error message available.'}
          </Typography>
        </Alert>

        <Button
          variant="contained"
          onClick={resetErrorBoundary}
          color="primary"
        >
          Try Again
        </Button>
      </Paper>
    </Box>
  );
};

export default ErrorFallback;