import { configureStore } from "@reduxjs/toolkit";
import { authAPI } from "./features/auth/authAPI";

export const store = configureStore({
  reducer: {
    [authAPI.reducerPath]: authAPI.reducer,
    // other reducers here...
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(authAPI.middleware),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
