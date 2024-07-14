"use server";

import { createAxiosInstance } from "@/lib/axios.config";

import { getUserFromAuth } from "@/actions/user";

export const createPost = async ({
  title,
  imageUrl,
  location,
  tags,
}: NewPost) => {
  try {
    const currentUser = await getUserFromAuth();

    const api = await createAxiosInstance();

    const response = await api.post("/api/v1/posts", {
      userId: currentUser.id,
      title,
      imageUrl,
      location,
      tags,
    });

    const postId = response.data as string;

    return postId;
  } catch (error) {
    console.log("CREATE_POST", error);
    return null;
  }
};
