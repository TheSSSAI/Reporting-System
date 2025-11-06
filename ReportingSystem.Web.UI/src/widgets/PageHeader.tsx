import React, { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import {
  AppBar,
  Toolbar,
  Typography,
  IconButton,
  Box,
  Menu,
  MenuItem,
  Avatar,
  ListItemIcon,
  Divider,
} from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import AccountCircle from '@mui/icons-material/AccountCircle';
import Logout from '@mui/icons-material/Logout';
import { useAuthStore } from '../app/store/authStore';
import { useAuthApi } from '../features/authentication/api/useAuthApi';

interface PageHeaderProps {
  onToggleSidebar: () => void;
  drawerWidth: number;
}

const getPageTitle = (pathname: string): string => {
  if (pathname.startsWith('/scripts/edit')) return 'Edit Transformation Script';
  if (pathname.startsWith('/scripts/create')) return 'Create Transformation Script';
  if (pathname.startsWith('/reports/edit')) return 'Edit Report Configuration';
  if (pathname.startsWith('/reports/create')) return 'Create Report Configuration';
  if (pathname.startsWith('/jobs/')) return 'Job Execution Log';

  switch (pathname) {
    case '/':
    case '/dashboard':
      return 'Job Monitoring Dashboard';
    case '/reports':
      return 'Report Configurations';
    case '/connectors':
      return 'Data Connectors';
    case '/scripts':
      return 'Transformation Scripts';
    case '/templates':
      return 'Handlebars Templates';
    case '/users':
      return 'User Management';
    case '/audit':
      return 'Audit Log';
    case '/settings':
      return 'System Settings';
    case '/viewer':
      return 'Report Viewer';
    default:
      return 'Dashboard';
  }
};

const PageHeader: React.FC<PageHeaderProps> = ({ onToggleSidebar, drawerWidth }) => {
  const { user, logout: logoutFromStore } = useAuthStore();
  const { logoutMutation } = useAuthApi();
  const navigate = useNavigate();
  const location = useLocation();

  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const isMenuOpen = Boolean(anchorEl);

  const handleProfileMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
  };

  const handleLogout = () => {
    handleMenuClose();
    logoutMutation.mutate(undefined, {
      onSuccess: () => {
        logoutFromStore();
        navigate('/login');
      },
      onError: () => {
        // Even if API fails, log out on client for security
        logoutFromStore();
        navigate('/login');
      }
    });
  };
  
  const pageTitle = getPageTitle(location.pathname);

  return (
    <AppBar
      position="fixed"
      sx={{
        width: { md: `calc(100% - ${drawerWidth}px)` },
        ml: { md: `${drawerWidth}px` },
        zIndex: (theme) => theme.zIndex.drawer + 1,
        backgroundColor: 'background.paper',
        color: 'text.primary',
        boxShadow: '0 1px 4px 0 rgba(0,0,0,0.1)',
      }}
    >
      <Toolbar>
        <IconButton
          color="inherit"
          aria-label="open drawer"
          edge="start"
          onClick={onToggleSidebar}
          sx={{ mr: 2, display: { md: 'none' } }}
        >
          <MenuIcon />
        </IconButton>
        <Typography variant="h6" noWrap component="div" sx={{ flexGrow: 1 }}>
          {pageTitle}
        </Typography>

        {user && (
          <Box sx={{ display: 'flex', alignItems: 'center' }}>
            <Typography sx={{ display: { xs: 'none', sm: 'block' }, mr: 1.5 }}>
              {user.username} ({user.role})
            </Typography>
            <IconButton
              size="large"
              edge="end"
              aria-label="account of current user"
              aria-controls="primary-search-account-menu"
              aria-haspopup="true"
              onClick={handleProfileMenuOpen}
              color="inherit"
            >
              <Avatar sx={{ width: 32, height: 32, bgcolor: 'primary.main' }}>
                {user.username.charAt(0).toUpperCase()}
              </Avatar>
            </IconButton>
          </Box>
        )}
      </Toolbar>
      <Menu
        anchorEl={anchorEl}
        id="primary-search-account-menu"
        keepMounted
        open={isMenuOpen}
        onClose={handleMenuClose}
        transformOrigin={{ horizontal: 'right', vertical: 'top' }}
        anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
        PaperProps={{
          elevation: 0,
          sx: {
            overflow: 'visible',
            filter: 'drop-shadow(0px 2px 8px rgba(0,0,0,0.32))',
            mt: 1.5,
            '& .MuiAvatar-root': {
              width: 32,
              height: 32,
              ml: -0.5,
              mr: 1,
            },
            '&:before': {
              content: '""',
              display: 'block',
              position: 'absolute',
              top: 0,
              right: 14,
              width: 10,
              height: 10,
              bgcolor: 'background.paper',
              transform: 'translateY(-50%) rotate(45deg)',
              zIndex: 0,
            },
          },
        }}
      >
        <Box sx={{ px: 2, py: 1 }}>
          <Typography variant="body1" fontWeight="bold">{user?.username}</Typography>
          <Typography variant="body2" color="text.secondary">{user?.role}</Typography>
        </Box>
        <Divider />
        <MenuItem onClick={handleLogout}>
          <ListItemIcon>
            <Logout fontSize="small" />
          </ListItemIcon>
          Logout
        </MenuItem>
      </Menu>
    </AppBar>
  );
};

export default PageHeader;