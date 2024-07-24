import { useQuery } from "@tanstack/react-query";

import { getCommentsByPostId } from "@/actions/comments/get-comments-by-post-id";

import { QUERY_KEYS } from "@/lib/react-query/query-keys";

export const useGetPostComments = (postId: string) => {
  const query = useQuery({
    enabled: !!postId,
    queryKey: [QUERY_KEYS.GET_POST_COMMENTS, { postId }],
    queryFn: async () => await getCommentsByPostId(postId),
  });

  return query;
};
