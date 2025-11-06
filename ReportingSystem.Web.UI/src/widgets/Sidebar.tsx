import React from 'react';
import { NavLink, useLocation } from 'react-router-dom';
import {
  Drawer,
  Toolbar,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Divider,
  Box,
  Typography,
} from '@mui/material';
import DashboardIcon from '@mui/icons-material/Dashboard';
import DescriptionIcon from '@mui/icons-material/Description';
import SettingsInputComponentIcon from '@mui/icons-material/SettingsInputComponent';
import CodeIcon from '@mui/icons-material/Code';
import ViewQuiltIcon from '@mui/icons-material/ViewQuilt';
import PeopleIcon from '@mui/icons-material/People';
import GavelIcon from '@mui/icons-material/Gavel';
import SettingsIcon from '@mui/icons-material/Settings';
import ArticleIcon from '@mui/icons-material/Article';
import { useAuthStore } from '../app/store/authStore';

interface SidebarProps {
  isOpen: boolean;
  onClose: () => void;
  drawerWidth: number;
}

const adminNavItems = [
  { text: 'Dashboard', icon: <DashboardIcon />, path: '/dashboard' },
  { text: 'Report Viewer', icon: <ArticleIcon />, path: '/viewer' },
  { text: 'Audit Log', icon: <GavelIcon />, path: '/audit' },
];

const configurationNavItems = [
    { text: 'Reports', icon: <DescriptionIcon />, path: '/reports' },
    { text: 'Connectors', icon: <SettingsInputComponentIcon />, path: '/connectors' },
    { text: 'Transformations', icon: <CodeIcon />, path: '/scripts' },
    { text: 'Templates', icon: <ViewQuiltIcon />, path: '/templates' },
];

const managementNavItems = [
  { text: 'User Management', icon: <PeopleIcon />, path: '/users' },
  { text: 'System Settings', icon: <SettingsIcon />, path: '/settings' },
];

const viewerNavItems = [
  { text: 'Report Viewer', icon: <ArticleIcon />, path: '/viewer' },
];

const Sidebar: React.FC<SidebarProps> = ({ isOpen, onClose, drawerWidth }) => {
  const user = useAuthStore((state) => state.user);
  const location = useLocation();

  const isAdministrator = user?.role === 'Administrator';

  const navList = isAdministrator ? adminNavItems : viewerNavItems;
  
  const drawerContent = (
    <div>
      <Toolbar>
        <Typography variant="h6" noWrap component="div" color="primary">
          Reporting System
        </Typography>
      </Toolbar>
      <Divider />
      <List>
        {navList.map((item) => (
          <ListItem key={item.text} disablePadding>
            <ListItemButton
              component={NavLink}
              to={item.path}
              onClick={onClose}
              selected={location.pathname === item.path}
              sx={{
                '&.active': {
                  backgroundColor: 'action.selected',
                  color: 'primary.main',
                  '& .MuiListItemIcon-root': {
                    color: 'primary.main',
                  },
                },
              }}
            >
              <ListItemIcon>{item.icon}</ListItemIcon>
              <ListItemText primary={item.text} />
            </ListItemButton>
          </ListItem>
        ))}
      </List>
      {isAdministrator && (
        <>
            <Divider sx={{ my: 1 }} />
            <Typography variant="overline" sx={{ px: 2, color: 'text.secondary' }}>
                Configuration
            </Typography>
            <List>
                {configurationNavItems.map((item) => (
                <ListItem key={item.text} disablePadding>
                    <ListItemButton
                    component={NavLink}
                    to={item.path}
                    onClick={onClose}
                    selected={location.pathname.startsWith(item.path)}
                    sx={{
                        '&.active': {
                        backgroundColor: 'action.selected',
                        color: 'primary.main',
                        '& .MuiListItemIcon-root': {
                            color: 'primary.main',
                        },
                        },
                    }}
                    >
                    <ListItemIcon>{item.icon}</ListItemIcon>
                    <ListItemText primary={item.text} />
                    </ListItemButton>
                </ListItem>
                ))}
            </List>
            <Divider sx={{ my: 1 }} />
            <Typography variant="overline" sx={{ px: 2, color: 'text.secondary' }}>
                Management
            </Typography>
            <List>
            {managementNavItems.map((item) => (
              <ListItem key={item.text} disablePadding>
                <ListItemButton
                  component={NavLink}
                  to={item.path}
                  onClick={onClose}
                  selected={location.pathname.startsWith(item.path)}
                  sx={{
                    '&.active': {
                      backgroundColor: 'action.selected',
                      color: 'primary.main',
                      '& .MuiListItemIcon-root': {
                        color: 'primary.main',
                      },
                    },
                  }}
                >
                  <ListItemIcon>{item.icon}</ListItemIcon>
                  <ListItemText primary={item.text} />
                </ListItemButton>
              </ListItem>
            ))}
          </List>
        </>
      )}
    </div>
  );

  return (
    <Box
      component="nav"
      sx={{ width: { md: drawerWidth }, flexShrink: { md: 0 } }}
      aria-label="main navigation"
    >
      {/* Temporary Drawer for mobile/tablet */}
      <Drawer
        variant="temporary"
        open={isOpen}
        onClose={onClose}
        ModalProps={{
          keepMounted: true, // Better open performance on mobile.
        }}
        sx={{
          display: { xs: 'block', md: 'none' },
          '& .MuiDrawer-paper': { boxSizing: 'border-box', width: drawerWidth },
        }}
      >
        {drawerContent}
      </Drawer>
      {/* Permanent Drawer for desktop */}
      <Drawer
        variant="permanent"
        sx={{
          display: { xs: 'none', md: 'block' },
          '& .MuiDrawer-paper': { boxSizing: 'border-box', width: drawerWidth },
        }}
        open
      >
        {drawerContent}
      </Drawer>
    </Box>
  );
};

export default Sidebar;