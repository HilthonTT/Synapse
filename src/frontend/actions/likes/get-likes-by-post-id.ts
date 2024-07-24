"use server";

import { API_VERSION } from "@/constants";
import { createAxiosInstance } from "@/lib/axios.config";

export const getLikesByPostId = async (postId: string) => {
  try {
    const api = await createAxiosInstance();

    const response = await api.get(`/api/${API_VERSION}/posts/${postId}/likes`);

    const likes = response.data as Like[];

    return likes;
  } catch (error) {
    console.error("GET_LIKES_BY_POST_ID", error);
    throw new Error(`Failed get the likes post with id '${postId}'`);
  }
};
