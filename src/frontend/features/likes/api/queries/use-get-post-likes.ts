import { useQuery } from "@tanstack/react-query";

import { getLikesByPostId } from "@/actions/likes/get-likes-by-post-id";

import { QUERY_KEYS } from "@/lib/react-query/query-keys";

export const useGetPostLikes = (postId: string) => {
  const query = useQuery({
    enabled: !!postId,
    queryKey: [QUERY_KEYS.GET_POST_LIKES, { postId }],
    queryFn: async () => await getLikesByPostId(postId),
  });

  return query;
};
