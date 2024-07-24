import { useMutation, useQueryClient } from "@tanstack/react-query";

import { createComment } from "@/actions/comments/create-comment";

import { QUERY_KEYS } from "@/lib/react-query/query-keys";

export const useCreateComment = (postId: string) => {
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: async (content: string) => await createComment(postId, content),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [QUERY_KEYS.GET_POST_COMMENTS, { postId }],
      });
    },
  });

  return mutation;
};
