"use server";

import { API_VERSION } from "@/constants";
import { getUserFromAuth } from "@/actions/users/get-user-from-auth";
import { createAxiosInstance } from "@/lib/axios.config";

export const createComment = async (postId: string, content: string) => {
  try {
    const user = await getUserFromAuth();
    if (!user) {
      throw new Error("Unauthorized");
    }

    const api = await createAxiosInstance();

    const response = await api.post(`/api/${API_VERSION}/comments`, {
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
