"use server";

import { createAxiosInstance } from "@/lib/axios.config";

export const getUserFromAuth = async () => {
  const api = await createAxiosInstance();

  const response = await api.get("/api/v1/users/auth");

  return response.data as UserFromAuth;
};
