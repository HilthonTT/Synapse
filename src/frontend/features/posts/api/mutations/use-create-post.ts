import { useMutation, useQueryClient } from "@tanstack/react-query";

import { createPost } from "@/actions/posts/create-post";
import { QUERY_KEYS } from "@/lib/react-query/query-keys";

export const useCreatePost = () => {
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: async (post: NewPost) => await createPost(post),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [QUERY_KEYS.GET_POSTS],
      });
      queryClient.invalidateQueries({
        queryKey: [QUERY_KEYS.INFINITE_POSTS],
      });
    },
  });

  return mutation;
};
