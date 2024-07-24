import { useMutation, useQueryClient } from "@tanstack/react-query";

import { deleteComment } from "@/actions/comments/delete-comment";

import { QUERY_KEYS } from "@/lib/react-query/query-keys";

export const useDeleteComment = (postId: string) => {
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: async (commentId: string) => await deleteComment(commentId),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [QUERY_KEYS.GET_POST_COMMENTS, { postId }],
      });
    },
  });

  return mutation;
};
