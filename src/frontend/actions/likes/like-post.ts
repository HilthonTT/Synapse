"use server";

import { API_VERSION } from "@/constants";
import { getUserFromAuth } from "@/actions/users/get-user-from-auth";
import { createAxiosInstance } from "@/lib/axios.config";

export const likePost = async (postId: string) => {
  try {
    const currentUser = await getUserFromAuth();
    if (!currentUser) {
      throw new Error("Unauthorized");
    }

    const api = await createAxiosInstance();

    const response = await api.post(
      `/api/${API_VERSION}/posts/${postId}/likes`,
      {
        userId: currentUser.id,
      }
    );

    const likeId = response.data as string;

    return likeId;
  } catch (error) {
    console.error("LIKE_POST", error);
    throw new Error(`Failed to like post with id '${postId}'`);
  }
};
