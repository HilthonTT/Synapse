import { useQuery } from "@tanstack/react-query";

import { getUserPosts } from "@/actions/posts/get-user-posts";
import { QUERY_KEYS } from "@/lib/react-query/query-keys";

export const useGetUserPosts = (userId: string) => {
  const query = useQuery({
    enabled: !!userId,
    queryKey: [QUERY_KEYS.GET_USER_POSTS, { userId }],
    queryFn: async () => await getUserPosts(userId),
  });

  return query;
};
