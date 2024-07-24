"use server";

import qs from "query-string";

import { API_VERSION } from "@/constants";
import { getUserFromAuth } from "@/actions/users/get-user-from-auth";
import { createAxiosInstance } from "@/lib/axios.config";

export const deleteComment = async (commentId: string) => {
  try {
    const user = await getUserFromAuth();
    if (!user) {
      throw new Error("Unauthorized");
    }

    const api = await createAxiosInstance();

    const url = qs.stringifyUrl({
      url: `/api/${API_VERSION}/comments/${commentId}`,
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
