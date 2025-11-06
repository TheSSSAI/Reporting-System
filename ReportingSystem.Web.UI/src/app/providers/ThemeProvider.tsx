import React, { ReactNode } from 'react';
import {
  createTheme,
  ThemeProvider as MuiThemeProvider,
  CssBaseline,
} from '@mui/material';

// Define a professional, accessible theme that aligns with the application's goals.
// This theme will be available to all components within the application.
// It enforces consistency in design and supports responsiveness (US-114) and accessibility (US-115).
const theme = createTheme({
  palette: {
    primary: {
      main: '#1976d2', // A professional blue
      light: '#42a5f5',
      dark: '#1565c0',
      contrastText: '#fff',
    },
    secondary: {
      main: '#9c27b0', // A contrasting purple
      light: '#ba68c8',
      dark: '#7b1fa2',
      contrastText: '#fff',
    },
    error: {
      main: '#d32f2f',
    },
    warning: {
      main: '#ed6c02',
    },
    info: {
      main: '#0288d1',
    },
    success: {
      main: '#2e7d32',
    },
    background: {
      default: '#f4f6f8', // A light grey background for a modern feel
      paper: '#ffffff',
    },
  },
  typography: {
    fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif',
    h1: {
      fontSize: '2.5rem',
      fontWeight: 500,
    },
    h2: {
      fontSize: '2rem',
      fontWeight: 500,
    },
    h3: {
      fontSize: '1.75rem',
      fontWeight: 500,
    },
    // ... other typography settings
  },
  // Define responsive breakpoints as per US-114
  breakpoints: {
    values: {
      xs: 0,
      sm: 600,
      md: 900,
      lg: 1280, // Minimum supported desktop viewport
      xl: 1920,
    },
  },
  components: {
    // Component overrides for consistency and accessibility
    MuiButton: {
      defaultProps: {
        disableElevation: true,
        variant: 'contained',
      },
      styleOverrides: {
        root: {
          textTransform: 'none', // More readable button text
          borderRadius: '8px',
        },
      },
    },
    MuiTextField: {
      defaultProps: {
        variant: 'outlined',
        size: 'small',
      },
    },
    MuiPaper: {
      defaultProps: {
        elevation: 1,
      },
      styleOverrides: {
        root: {
          borderRadius: '8px',
        },
      },
    },
    // Ensure tooltips are accessible
    MuiTooltip: {
      defaultProps: {
        arrow: true,
      },
    },
  },
});

interface AppThemeProviderProps {
  children: ReactNode;
}

/**
 * A provider component that applies the custom MUI theme to the entire application.
 * It also includes CssBaseline to normalize browser styles for a consistent look.
 * This component should be placed near the root of the React component tree.
 */
export const AppThemeProvider: React.FC<AppThemeProviderProps> = ({ children }) => {
  return (
    <MuiThemeProvider theme={theme}>
      <CssBaseline />
      {children}
    </MuiThemeProvider>
  );
};