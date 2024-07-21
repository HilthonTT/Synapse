"use server";

import qs from "query-string";

import { createAxiosInstance } from "@/lib/axios.config";
import { getUserFromAuth } from "@/actions/user";

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

export const updateComment = async (commentId: string, content: string) => {
  try {
    const user = await getUserFromAuth();
    if (!user) {
      throw new Error("Unauthorized");
    }

    const api = await createAxiosInstance();

    await api.patch(`/api/v1/comments/${commentId}`, {
      userId: user.id,
      content,
    });
  } catch (error) {
    console.error("UPDATE_COMMENT", error);
    throw new Error("Failed to update comment");
  }
};

export const deleteComment = async (commentId: string) => {
  try {
    const user = await getUserFromAuth();
    if (!user) {
      throw new Error("Unauthorized");
    }

    const api = await createAxiosInstance();

    const url = qs.stringifyUrl({
      url: `/api/v1/comments/${commentId}`,
      query: {
        userId: user.id,
      },
    });

    await api.delete(url);
  } catch (error) {
    console.error("DELETE_COMMENT", error);
    throw new Error("Failed to delete comment");
  }
};
