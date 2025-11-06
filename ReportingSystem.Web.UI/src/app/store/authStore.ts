import { create } from 'zustand';
import { jwtDecode } from 'jwt-decode';
import { User } from '../../shared/api/types';

interface DecodedToken {
  sub: string;
  name: string;
  role: 'Administrator' | 'Viewer';
  iat: number;
  exp: number;
}

interface AuthState {
  user: User | null;
  token: string | null;
  isAuthenticated: boolean;
  isHydrated: boolean;
}

interface AuthActions {
  login: (token: string) => void;
  logout: () => void;
  hydrate: () => void;
}

const initialState: AuthState = {
  user: null,
  token: null,
  isAuthenticated: false,
  isHydrated: false,
};

export const useAuthStore = create<AuthState & AuthActions>((set) => ({
  ...initialState,

  /**
   * Logs the user in by decoding the JWT, storing it, and updating the state.
   * @param token The JWT received from the authentication API.
   */
  login: (token: string) => {
    try {
      const decodedToken = jwtDecode<DecodedToken>(token);
      const user: User = {
        id: decodedToken.sub,
        username: decodedToken.name,
        role: decodedToken.role,
      };

      sessionStorage.setItem('authToken', token);
      set({ user, token, isAuthenticated: true });
    } catch (error) {
      console.error('Failed to decode token or login:', error);
      // Ensure clean state on login failure
      sessionStorage.removeItem('authToken');
      set(initialState);
    }
  },

  /**
   * Logs the user out by clearing all authentication state.
   */
  logout: () => {
    sessionStorage.removeItem('authToken');
    set({ ...initialState, isHydrated: true }); // Ensure isHydrated is true after logout to prevent re-hydration loops
  },

  /**
   * Hydrates the store from session storage on application load.
   * This function allows the user's session to persist across page reloads.
   */
  hydrate: () => {
    try {
      const token = sessionStorage.getItem('authToken');
      if (token) {
        const decodedToken = jwtDecode<DecodedToken>(token);
        // Check if the token is expired
        if (decodedToken.exp * 1000 > Date.now()) {
          const user: User = {
            id: decodedToken.sub,
            username: decodedToken.name,
            role: decodedToken.role,
          };
          set({ user, token, isAuthenticated: true, isHydrated: true });
        } else {
          // Token is expired, clear it
          sessionStorage.removeItem('authToken');
          set({ ...initialState, isHydrated: true });
        }
      } else {
        set({ isHydrated: true });
      }
    } catch (error) {
      console.error('Failed to hydrate authentication state:', error);
      sessionStorage.removeItem('authToken');
      set({ ...initialState, isHydrated: true });
    }
  },
}));