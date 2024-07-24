"use server";

import { API_VERSION } from "@/constants";
import { createAxiosInstance } from "@/lib/axios.config";

export const getUserPosts = async (userId: string) => {
  try {
    const api = await createAxiosInstance();

    const response = await api.get(`/api/${API_VERSION}/users/${userId}/posts`);

    const posts = response.data as Post[];

    return posts;
  } catch (error) {
    console.log("GET_USER_POSTS", error);
    return [];
  }
};
