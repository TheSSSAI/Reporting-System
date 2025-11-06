import React from 'react';
import { Box, CircularProgress, CircularProgressProps, Typography } from '@mui/material';

interface LoadingSpinnerProps extends CircularProgressProps {
  /**
   * If true, the spinner will cover the entire container.
   * If a string is provided, it will be displayed below the spinner.
   * If false or undefined, the spinner will be inline.
   */
  fullscreen?: boolean | string;
}

/**
 * A consistent, accessible loading spinner component for the application.
 * Can be used inline or as a fullscreen overlay with an optional message.
 *
 * @param {LoadingSpinnerProps} props - The component props.
 * @returns {React.ReactElement} The rendered loading spinner.
 */
const LoadingSpinner: React.FC<LoadingSpinnerProps> = ({ fullscreen, ...props }) => {
  const spinner = (
    <>
      <CircularProgress role="status" {...props}>
        <span className="visually-hidden">Loading...</span>
      </CircularProgress>
      {typeof fullscreen === 'string' && (
        <Typography variant="body1" sx={{ mt: 2, color: 'text.secondary' }}>
          {fullscreen}
        </Typography>
      )}
    </>
  );

  if (fullscreen) {
    return (
      <Box
        sx={{
          position: 'absolute',
          top: 0,
          left: 0,
          right: 0,
          bottom: 0,
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'center',
          alignItems: 'center',
          backgroundColor: 'rgba(255, 255, 255, 0.7)',
          zIndex: (theme) => theme.zIndex.drawer + 1,
        }}
        aria-label={typeof fullscreen === 'string' ? fullscreen : "Loading content"}
        aria-live="polite"
      >
        {spinner}
      </Box>
    );
  }

  return spinner;
};

// Simple inline spinner for smaller contexts
export const InlineSpinner: React.FC<CircularProgressProps> = (props) => (
    <CircularProgress size={20} role="status" {...props}>
        <span className="visually-hidden">Loading...</span>
    </CircularProgress>
);


export default LoadingSpinner;