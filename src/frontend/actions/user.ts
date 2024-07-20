"use server";

import { createAxiosInstance } from "@/lib/axios.config";

export const getUserFromAuth = async () => {
  const api = await createAxiosInstance();

  const response = await api.get("/api/v1/users/auth");

  const user = response.data as UserFromAuth;

  if (!user) {
    return null;
  }

  return user;
};
