"use server";

import { API_VERSION } from "@/constants";
import { createAxiosInstance } from "@/lib/axios.config";

export const getCommentsByPostId = async (postId: string) => {
  try {
    const api = await createAxiosInstance();

    const response = await api.get(
      `/api/${API_VERSION}/posts/${postId}/comments`
    );

    const comments = response.data as PostComment[];

    return comments;
  } catch (error) {
    console.error("GET_COMMENTS_BY_POST_ID", error);
    throw new Error(`Failed to fetch comments with post id '${postId}'`);
  }
};
