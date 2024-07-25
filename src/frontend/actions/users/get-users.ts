"use server";

import qs from "query-string";

import { API_VERSION } from "@/constants";
import { createAxiosInstance } from "@/lib/axios.config";

export const getUsers = async (limit?: number) => {
  try {
    const api = await createAxiosInstance();

    const url = qs.stringifyUrl({
      url: `/api/${API_VERSION}/users`,
      query: {
        limit,
      },
    });

    const response = await api.get(url);

    const users = response.data as User[];

    return users;
  } catch (error) {
    console.log("GET_USERS", error);
    return [];
  }
};
