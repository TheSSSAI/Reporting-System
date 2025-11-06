import React, { Suspense, lazy } from 'react';
import {
  createBrowserRouter,
  RouterProvider,
  Outlet,
  Navigate,
} from 'react-router-dom';
import { useAuthStore } from '../store/authStore';
import { UserRole } from '../../shared/api/types';
import LoadingSpinner from '../../shared/ui/LoadingSpinner';
import Sidebar from '../../widgets/Sidebar';
import PageHeader from '../../widgets/PageHeader';
import Box from '@mui/material/Box';

// Lazy load page components for code-splitting and better initial load performance
const LoginPage = lazy(() => import('../../pages/LoginPage'));
const ScriptEditorPage = lazy(() => import('../../pages/ScriptEditorPage'));
const UserManagementPage = lazy(() => import('../../pages/UserManagementPage'));
const ReportConfigurationWizardPage = lazy(() => import('../../pages/ReportConfigurationWizardPage'));
const JobMonitoringDashboardPage = lazy(() => import('../../pages/JobMonitoringDashboardPage'));
const ReportViewerPage = lazy(() => import('../../pages/ReportViewerPage'));
const NotFoundPage = lazy(() => import('../../pages/NotFoundPage'));
const AuditLogViewer = lazy(() => import('../../pages/AuditLogViewer'));
// Placeholder for a future dashboard page
const DashboardPage = () => <h2>Dashboard</h2>;


/**
 * @component ProtectedRoutes
 * @description A wrapper component that protects routes requiring authentication.
 * It checks the authentication state from the global auth store.
 * If the user is authenticated, it renders the child routes.
 * If not, it redirects them to the login page.
 * It also handles the initial loading state of the authentication check.
 */
const ProtectedRoutes = () => {
  const { isAuthenticated, isAuthLoading } = useAuthStore();

  if (isAuthLoading) {
    // Show a loading spinner during the initial authentication check
    return <LoadingSpinner />;
  }

  return isAuthenticated ? <Outlet /> : <Navigate to="/login" replace />;
};

/**
 * @component RoleGuard
 * @description A wrapper component that protects routes based on user roles.
 * @param {object} props - The component props.
 * @param {UserRole[]} props.allowedRoles - An array of roles that are allowed to access the route.
 */
const RoleGuard = ({ allowedRoles }: { allowedRoles: UserRole[] }) => {
  const { user } = useAuthStore();

  if (!user) {
    // This case should theoretically be handled by ProtectedRoutes, but as a safeguard:
    return <Navigate to="/login" replace />;
  }

  return allowedRoles.includes(user.role) ? (
    <Outlet />
  ) : (
    // Redirect to a safe, default page if the role is not permitted.
    // In a more complex app, this might go to an "/unauthorized" page.
    <Navigate to="/" replace />
  );
};


/**
 * @component AppLayout
 * @description The main layout for authenticated users, including the sidebar and page header.
 * The main content of the current route is rendered through the <Outlet />.
 */
const AppLayout = () => (
  <Box sx={{ display: 'flex', height: '100vh' }}>
    <Sidebar />
    <Box
      component="main"
      sx={{
        flexGrow: 1,
        display: 'flex',
        flexDirection: 'column',
        overflow: 'hidden',
      }}
    >
      <PageHeader />
      <Box
        sx={{
          flexGrow: 1,
          p: 3,
          overflowY: 'auto',
          backgroundColor: (theme) => theme.palette.grey[100],
        }}
      >
        <Suspense fallback={<LoadingSpinner />}>
          <Outlet />
        </Suspense>
      </Box>
    </Box>
  </Box>
);

const router = createBrowserRouter([
  {
    path: '/login',
    element: (
      <Suspense fallback={<LoadingSpinner />}>
        <LoginPage />
      </Suspense>
    ),
  },
  {
    path: '/',
    element: <ProtectedRoutes />,
    children: [
      {
        element: <AppLayout />,
        children: [
          {
            index: true,
            element: <DashboardPage />, // Default authenticated page
          },
          {
            path: 'jobs',
            element: <JobMonitoringDashboardPage />,
          },
          {
            path: 'reports/viewer',
            element: <ReportViewerPage />,
          },
          // Administrator-only routes
          {
            element: <RoleGuard allowedRoles={[UserRole.Administrator]} />,
            children: [
              {
                path: 'scripts',
                element: <ScriptEditorPage />, // Placeholder for a future list view
              },
              {
                path: 'scripts/:id',
                element: <ScriptEditorPage />,
              },
              {
                path: 'scripts/new',
                element: <ScriptEditorPage />,
              },
              {
                path: 'users',
                element: <UserManagementPage />,
              },
              {
                path: 'reports/configure',
                element: <ReportConfigurationWizardPage />,
              },
              {
                path: 'reports/configure/:id',
                element: <ReportConfigurationWizardPage />,
              },
              {
                  path: 'auditing',
                  element: <AuditLogViewer />
              }
              // Add other admin-only routes for connectors, settings, etc. here
            ],
          },
        ],
      },
    ],
  },
  // Catch-all route for 404 Not Found
  {
    path: '*',
    element: (
      <Suspense fallback={<LoadingSpinner />}>
        <NotFoundPage />
      </Suspense>
    ),
  },
]);

/**
 * @component AppRouterProvider
 * @description The main provider for the application's routing.
 * It uses the configured `react-router-dom` router instance.
 */
const AppRouterProvider = () => {
  return <RouterProvider router={router} />;
};

export default AppRouterProvider;