"use server";

import { API_VERSION } from "@/constants";
import { createAxiosInstance } from "@/lib/axios.config";

export const getPostById = async (postId: string) => {
  try {
    const api = await createAxiosInstance();

    const response = await api.get(`/api/${API_VERSION}/posts/${postId}`);

    const post = response.data as Post;

    if (!post) {
      return null;
    }

    return post;
  } catch (error) {
    console.error("GET_POST_BY_ID", error);
    throw new Error(`Failed to fetch post with id '${postId}'`);
  }
};
