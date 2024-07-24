"use server";

import qs from "query-string";

import { SortColumn } from "@/features/posts/api/sort-column";
import { SortOrder } from "@/features/posts/api/sort-order";

import { API_VERSION } from "@/constants";
import { createAxiosInstance } from "@/lib/axios.config";

export const searchPosts = async (
  sortOrder: SortOrder,
  sortColumn: SortColumn,
  searchTerm?: string,
  limit = 10
) => {
  try {
    const api = await createAxiosInstance();

    const url = qs.stringifyUrl({
      url: `/api/${API_VERSION}/posts/search`,
      query: {
        searchTerm,
        sortOrder,
        sortColumn,
        limit,
      },
    });

    const response = await api.get(url);

    const posts = response.data as SearchPost[];

    return posts;
  } catch (error) {
    console.error("SEARCH_POSTS", error);
    return [];
  }
};
