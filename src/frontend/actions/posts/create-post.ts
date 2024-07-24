"use server";

import { v4 as uuid } from "uuid";

import { API_VERSION } from "@/constants";
import { createAxiosInstance } from "@/lib/axios.config";
import { getUserFromAuth } from "@/actions/users/get-user-from-auth";

export const createPost = async ({
  title,
  imageUrl,
  location,
  tags,
}: NewPost) => {
  try {
    const currentUser = await getUserFromAuth();
    if (!currentUser) {
      throw new Error("Unauthorized");
    }

    const api = await createAxiosInstance();

    const idempotencyKey = uuid();

    const response = await api.post(
      `/api/${API_VERSION}/posts`,
      {
        userId: currentUser.id,
        title,
        imageUrl,
        location,
        tags,
      },
      {
        headers: {
          "X-Idempotency-Key": idempotencyKey,
        },
      }
    );

    const postId = response.data as string;

    return postId;
  } catch (error) {
    console.log("CREATE_POST", error);
    return null;
  }
};
