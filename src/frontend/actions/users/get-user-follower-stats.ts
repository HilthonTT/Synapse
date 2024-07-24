"use server";

import { API_VERSION } from "@/constants";
import { createAxiosInstance } from "@/lib/axios.config";

export const getFollowerStatsByUserId = async (userId: string) => {
  try {
    const api = await createAxiosInstance();

    const response = await api.get(
      `/api/${API_VERSION}/users/${userId}/followers/stats`
    );

    const stats = response.data as FollowerStats;

    return stats;
  } catch (error) {
    console.log("GET_USER_FOLLOWER_STATS", error);
    return null;
  }
};
