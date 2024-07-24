"use server";

import { createAxiosInstance } from "@/lib/axios.config";

import { getUserFromAuth } from "@/actions/users/get-user-from-auth";
import { API_VERSION } from "@/constants";

export const updatePost = async (
  postId: string,
  title: string,
  imageUrl: string,
  tags?: string,
  location?: string
) => {
  try {
    const currentUser = await getUserFromAuth();
    if (!currentUser) {
      throw new Error("Unauthorized");
    }

    const api = await createAxiosInstance();

    await api.patch(`/api/${API_VERSION}/posts/${postId}`, {
      userId: currentUser.id,
      title,
      imageUrl,
      location,
      tags,
    });
  } catch (error) {
    console.error("UPDATE_POST", error);
    throw new Error("Failed to update post");
  }
};
