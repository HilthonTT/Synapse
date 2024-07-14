"use server";

import { createAxiosInstance } from "@/lib/axios.config";

import { getUserFromAuth } from "@/actions/user";

export const createPost = async (
  title: string,
  imageUrl: string,
  location?: string,
  tags?: string
) => {
  const currentUser = await getUserFromAuth();

  const api = await createAxiosInstance();

  const response = await api.post("/api/v1/posts", {
    userId: currentUser.id,
    title,
    imageUrl,
    location,
    tags,
  });

  const postId = response.data;

  return postId;
};
