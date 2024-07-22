"use server";

import qs from "query-string";
import { v4 as uuid } from "uuid";

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
    if (!currentUser) {
      throw new Error("Unauthorized");
    }

    const api = await createAxiosInstance();

    const idempotencyKey = uuid();

    const response = await api.post(
      "/api/v1/posts",
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

export const getPostById = async (postId: string) => {
  try {
    const api = await createAxiosInstance();

    const response = await api.get(`/api/v1/posts/${postId}`);

    const post = response.data as Post;

    return post;
  } catch (error) {
    console.error("GET_POST_BY_ID", error);
    throw new Error(`Failed to fetch post with id '${postId}'`);
  }
};

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

    await api.patch(`/api/v1/posts/${postId}`, {
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

export const deletePost = async (postId: string) => {
  try {
    const currentUser = await getUserFromAuth();
    if (!currentUser) {
      throw new Error("Unauthorized");
    }

    const api = await createAxiosInstance();

    const url = qs.stringifyUrl({
      url: `/api/v1/posts/${postId}`,
      query: {
        userId: currentUser.id,
      },
    });

    await api.delete(url);
  } catch (error) {
    console.error("UPDATE_POST", error);
    throw new Error("Failed to delete post");
  }
};
