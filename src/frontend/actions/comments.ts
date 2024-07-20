"use server";

import { createAxiosInstance } from "@/lib/axios.config";
import { getUserFromAuth } from "./user";

export const getCommentsByPostId = async (postId: string) => {
  try {
    const api = await createAxiosInstance();

    const response = await api.get(`/api/v1/posts/${postId}/comments`);

    const comments = response.data as PostComment[];

    return comments;
  } catch (error) {
    console.error("GET_COMMENTS_BY_POST_ID", error);
    throw new Error(`Failed to fetch comments with post id '${postId}'`);
  }
};

export const createComment = async (postId: string, content: string) => {
  try {
    const user = await getUserFromAuth();
    if (!user) {
      throw new Error("Unauthorized");
    }

    const api = await createAxiosInstance();

    const response = await api.post("/api/v1/comments", {
      postId,
      content,
      userId: user.id,
    });

    const commentId = response.data as string;

    return commentId;
  } catch (error) {
    console.error("CREATE_COMMENT", error);
    throw new Error("Failed to create comment");
  }
};
