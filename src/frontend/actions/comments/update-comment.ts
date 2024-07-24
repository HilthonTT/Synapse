"use server";

import { getUserFromAuth } from "@/actions/users/get-user-from-auth";
import { API_VERSION } from "@/constants";
import { createAxiosInstance } from "@/lib/axios.config";

export const updateComment = async (commentId: string, content: string) => {
  try {
    const user = await getUserFromAuth();
    if (!user) {
      throw new Error("Unauthorized");
    }

    const api = await createAxiosInstance();

    await api.patch(`/api/${API_VERSION}/comments/${commentId}`, {
      userId: user.id,
      content,
    });
  } catch (error) {
    console.error("UPDATE_COMMENT", error);
    throw new Error("Failed to update comment");
  }
};
