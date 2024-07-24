"use server";

import qs from "query-string";

import { API_VERSION } from "@/constants";
import { createAxiosInstance } from "@/lib/axios.config";
import { getUserFromAuth } from "@/actions/users/get-user-from-auth";

export const unlikePost = async (postId: string) => {
  try {
    const currentUser = await getUserFromAuth();
    if (!currentUser) {
      throw new Error("Unauthorized");
    }

    const api = await createAxiosInstance();

    const url = qs.stringifyUrl({
      url: `/api/${API_VERSION}/posts/${postId}/likes`,
      query: {
        userId: currentUser.id,
      },
    });

    await api.delete(url);
  } catch (error) {
    console.error("UNLIKE_POST", error);
    throw new Error(`Failed to like post with id '${postId}'`);
  }
};
