"use server";

import { API_VERSION } from "@/constants";
import { createAxiosInstance } from "@/lib/axios.config";

export const getUserById = async (userId: string) => {
  try {
    const api = await createAxiosInstance();

    const response = await api.get(`/api/${API_VERSION}/users/${userId}`);

    const user = response.data as User;

    if (!user) {
      return null;
    }

    return user;
  } catch (error) {
    console.log("GET_USER_BY_ID", error);
    return null;
  }
};
