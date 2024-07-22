"use server";

import qs from "query-string";

import { createAxiosInstance } from "@/lib/axios.config";
import { getUserFromAuth } from "@/actions/user";

export const likePost = async (postId: string) => {
  try {
    const currentUser = await getUserFromAuth();
    if (!currentUser) {
      throw new Error("Unauthorized");
    }

    const api = await createAxiosInstance();

    const response = await api.post(`/api/v1/posts/${postId}/likes`, {
      userId: currentUser.id,
    });

    const likeId = response.data as string;

    return likeId;
  } catch (error) {
    console.error("LIKE_POST", error);
    throw new Error(`Failed to like post with id '${postId}'`);
  }
};

export const unlikePost = async (postId: string) => {
  try {
    const currentUser = await getUserFromAuth();
    if (!currentUser) {
      throw new Error("Unauthorized");
    }

    const api = await createAxiosInstance();

    const url = qs.stringifyUrl({
      url: `/api/v1/posts/${postId}/likes`,
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

export const getLikesByPostId = async (postId: string) => {
  try {
    const api = await createAxiosInstance();

    const response = await api.get(`/api/v1/posts/${postId}/likes`);

    const likes = response.data as Like[];

    return likes;
  } catch (error) {
    console.error("GET_LIKES_BY_POST_ID", error);
    throw new Error(`Failed get the likes post with id '${postId}'`);
  }
};
