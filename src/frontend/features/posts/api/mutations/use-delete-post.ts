import { useMutation, useQueryClient } from "@tanstack/react-query";

import { deletePost } from "@/actions/posts/delete-post";
import { QUERY_KEYS } from "@/lib/react-query/query-keys";

export const useDeletePost = (postId: string) => {
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: async () => await deletePost(postId),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [QUERY_KEYS.GET_POST_BY_ID, { postId }],
      });
      queryClient.invalidateQueries({
        queryKey: [QUERY_KEYS.GET_POSTS],
      });
    },
  });

  return mutation;
};
