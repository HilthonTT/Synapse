import { useMutation, useQueryClient } from "@tanstack/react-query";

import { likePost } from "@/actions/likes/like-post";

import { QUERY_KEYS } from "@/lib/react-query/query-keys";

export const useLikePost = (postId: string) => {
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: async () => await likePost(postId),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [QUERY_KEYS.GET_POST_LIKES, { postId }],
      });
    },
  });

  return mutation;
};
