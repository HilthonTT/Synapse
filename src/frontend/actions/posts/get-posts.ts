"use server";

import qs from "query-string";

import { API_VERSION } from "@/constants";
import { createAxiosInstance } from "@/lib/axios.config";

export const getPosts = async (cursor: string) => {
  try {
    const api = await createAxiosInstance();

    const url = qs.stringifyUrl(
      {
        url: `/api/${API_VERSION}/posts`,
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
    console.error("GET_POSTS", error);
    throw new Error("Failed to fetch posts");
  }
};
