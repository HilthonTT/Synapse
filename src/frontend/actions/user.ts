"use server";

import { createAxiosInstance } from "@/lib/axios.config";

export const getUserFromAuth = async () => {
  try {
    const api = await createAxiosInstance();

    const response = await api.get("/api/v1/users/auth");

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

export const getUsers = async () => {
  try {
    const api = await createAxiosInstance();

    const response = await api.get("/api/v1/users");

    const users = response.data as User[];

    return users;
  } catch (error) {
    console.log("GET_USERS", error);
    return [];
  }
};

export const getFollowerStatsByUserId = async (userId: string) => {
  try {
    const api = await createAxiosInstance();

    const response = await api.get(`/api/v1/users/${userId}/followers/stats`);

    const stats = response.data as FollowerStats;

    return stats;
  } catch (error) {
    console.log("GET_USER_FOLLOWER_STATS", error);
    return null;
  }
};
