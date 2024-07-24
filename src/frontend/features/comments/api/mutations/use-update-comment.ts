import { useMutation, useQueryClient } from "@tanstack/react-query";

import { updateComment } from "@/actions/comments/update-comment";

import { QUERY_KEYS } from "@/lib/react-query/query-keys";

type Props = {
  commentId: string;
  content: string;
};

export const useUpdateComment = (postId: string) => {
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: async ({ commentId, content }: Props) =>
      await updateComment(commentId, content),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [QUERY_KEYS.GET_POST_COMMENTS, { postId }],
      });
    },
  });

  return mutation;
};
