import { useMutation, useQueryClient } from "@tanstack/react-query";

import { unlikePost } from "@/actions/likes/unlike-post";

import { QUERY_KEYS } from "@/lib/react-query/query-keys";

export const useUnlikePost = (postId: string) => {
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: async () => await unlikePost(postId),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [QUERY_KEYS.GET_POST_LIKES, { postId }],
      });
    },
  });

  return mutation;
};
