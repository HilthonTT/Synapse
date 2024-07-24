import { useQuery } from "@tanstack/react-query";

import { getPostById } from "@/actions/posts/get-post-by-id";
import { QUERY_KEYS } from "@/lib/react-query/query-keys";

export const useGetPostBydId = (postId: string) => {
  const query = useQuery({
    enabled: !!postId,
    queryKey: [QUERY_KEYS.GET_POST_BY_ID, { postId }],
    queryFn: async () => await getPostById(postId),
  });

  return query;
};
