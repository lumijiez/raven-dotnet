import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

export const authAPI = createApi({
  reducerPath: "authAPI",
  baseQuery: fetchBaseQuery({
    baseUrl: "https://8cb7e414-1096-4b49-80e5-4211a71b2f7c.mock.pstmn.io/auth",
  }),
  endpoints: (builder) => ({
    loginUser: builder.mutation<
      { accessToken: string; refreshToken: string },
      { username: string; password: string }
    >({
      query: (credentials) => ({
        url: "/login",
        method: "POST",
        body: credentials,
      }),
    }),
    registerUser: builder.mutation<
      void,
      { username: string; email: string; password: string }
    >({
      query: (userData) => ({
        url: "/register",
        method: "POST",
        body: userData,
      }),
    }),
  }),
});

export const { useLoginUserMutation, useRegisterUserMutation } = authAPI;
