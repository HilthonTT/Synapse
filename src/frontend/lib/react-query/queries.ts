import { useInfiniteQuery, useQuery } from "@tanstack/react-query";

import { QUERY_KEYS } from "@/lib/react-query/query-keys";
import { getPostById, getPosts, searchPosts } from "@/actions/post";
import { getCommentsByPostId } from "@/actions/comments";
import { getLikesByPostId } from "@/actions/likes";
import { getUserFromAuth } from "@/actions/user";

export enum SortColumn {
  LIKES = 0,
  COMMENTS = 1,
  CREATED_On_UTC = 2,
  TITLE = 3,
}

export enum SortOrder {
  ASCENDING = 0,
  DESCENDING = 1,
}

export const useGetInfinitePosts = () => {
  const query = useInfiniteQuery<CursorPaginationPost, Error>({
    queryKey: [QUERY_KEYS.INFINITE_POSTS],
    queryFn: async ({ pageParam = null }) =>
      await getPosts(pageParam as string),
    getNextPageParam: (lastPage: CursorPaginationPost) =>
      lastPage.nextCursor ?? null,
    getPreviousPageParam: (firstPage: CursorPaginationPost) =>
      firstPage.previousCursor ?? null,
    initialPageParam: null,
  });

  return query;
};

export const useGetPostBydId = (postId: string) => {
  const query = useQuery({
    enabled: !!postId,
    queryKey: [QUERY_KEYS.GET_POST_BY_ID, { postId }],
    queryFn: async () => await getPostById(postId),
  });

  return query;
};

export const useGetPostComments = (postId: string) => {
  const query = useQuery({
    enabled: !!postId,
    queryKey: [QUERY_KEYS.GET_POST_COMMENTS, { postId }],
    queryFn: async () => await getCommentsByPostId(postId),
  });

  return query;
};

export const useGetPostLikes = (postId: string) => {
  const query = useQuery({
    enabled: !!postId,
    queryKey: [QUERY_KEYS.GET_POST_LIKES, { postId }],
    queryFn: async () => await getLikesByPostId(postId),
  });

  return query;
};

export const useGetUserFromAuth = () => {
  const query = useQuery({
    queryKey: [QUERY_KEYS.GET_USER_FROM_AUTH],
    queryFn: async () => await getUserFromAuth(),
  });

  return query;
};

export const useSearchPosts = (
  searchTerm?: string,
  sortOrder = SortOrder.DESCENDING,
  sortColumn = SortColumn.LIKES,
  limit = 10
) => {
  const query = useQuery({
    queryKey: [
      QUERY_KEYS.GET_SEARCH_POSTS,
      {
        searchTerm,
        sortOrder,
        sortColumn,
      },
    ],
    queryFn: async () =>
      await searchPosts(sortOrder, sortColumn, searchTerm, limit),
  });

  return query;
};
