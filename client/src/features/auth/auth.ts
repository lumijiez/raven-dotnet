// src/features/auth/auth.ts

import { RootState } from "../../store";

// Selector to get the authentication state
export const selectIsAuthenticated = (state: RootState) =>
  state.auth.isAuthenticated;
export const selectAccessToken = (state: RootState) => state.auth.accessToken;
export const selectUser = (state: RootState) => state.auth.user;
