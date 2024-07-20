import { useInfiniteQuery, useQuery } from "@tanstack/react-query";

import { getPostById, getPosts } from "@/actions/post";
import { QUERY_KEYS } from "@/lib/react-query/query-keys";
import { getCommentsByPostId } from "@/actions/comments";

export const getInfinitePosts = () => {
  const query = useInfiniteQuery<CursorPaginationPost, Error>({
    queryKey: [QUERY_KEYS.INFINITE_POSTS],
    queryFn: ({ pageParam = null }) => getPosts(pageParam as string),
    getNextPageParam: (lastPage: CursorPaginationPost) =>
      lastPage.nextCursor ?? null,
    getPreviousPageParam: (firstPage: CursorPaginationPost) =>
      firstPage.previousCursor ?? null,
    initialPageParam: null,
  });

  return query;
};

export const getPostBydId = (postId: string) => {
  const query = useQuery({
    enabled: !!postId,
    queryKey: [QUERY_KEYS.GET_POST_BY_ID, { postId }],
    queryFn: () => getPostById(postId),
  });

  return query;
};

export const getPostComments = (postId: string) => {
  const query = useQuery({
    enabled: !!postId,
    queryKey: [QUERY_KEYS.GET_POST_COMMENTS, { postId }],
    queryFn: () => getCommentsByPostId(postId),
  });

  return query;
};
