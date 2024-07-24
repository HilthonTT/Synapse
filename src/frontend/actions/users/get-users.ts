"use server";

import { API_VERSION } from "@/constants";
import { createAxiosInstance } from "@/lib/axios.config";

export const getUsers = async () => {
  try {
    const api = await createAxiosInstance();

    const response = await api.get(`/api/${API_VERSION}/users`);

    const users = response.data as User[];

    return users;
  } catch (error) {
    console.log("GET_USERS", error);
    return [];
  }
};
