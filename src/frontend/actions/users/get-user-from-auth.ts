"use server";

import { API_VERSION } from "@/constants";
import { createAxiosInstance } from "@/lib/axios.config";

export const getUserFromAuth = async () => {
  try {
    const api = await createAxiosInstance();

    const response = await api.get(`/api/${API_VERSION}/users/auth`);

    const user = response.data as UserFromAuth;

    if (!user) {
      return null;
    }

    return user;
  } catch (error) {
    console.log("GET_USER_FROM_AUTH", error);
    return null;
  }
};
