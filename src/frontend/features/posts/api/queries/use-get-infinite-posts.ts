import { useInfiniteQuery } from "@tanstack/react-query";

import { getPosts } from "@/actions/posts/get-posts";
import { QUERY_KEYS } from "@/lib/react-query/query-keys";

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
