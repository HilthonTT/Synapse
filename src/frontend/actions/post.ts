"use server";

import qs from "query-string";

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

export const getPosts = async (cursor: string) => {
  try {
    const api = await createAxiosInstance();

    const url = qs.stringifyUrl(
      {
        url: "/api/v1/posts",
        query: {
          cursor,
        },
      },
      {
        skipEmptyString: true,
        skipNull: true,
      }
    );

    const response = await api.get(url);

    const paginatedPosts = response.data as CursorPaginationPost;

    return paginatedPosts;
  } catch (error) {
    console.error("GET_POST", error);
    throw new Error("Failed to fetch posts");
  }
};
