import { useMutation, useQueryClient } from "@tanstack/react-query";

import { updatePost } from "@/actions/posts/update-post";
import { QUERY_KEYS } from "@/lib/react-query/query-keys";

type Props = {
  title: string;
  tags?: string;
  location?: string;
  imageUrl: string;
};

export const useUpdatePost = (postId: string) => {
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: async ({ title, tags, location, imageUrl }: Props) =>
      await updatePost(postId, title, imageUrl, tags, location),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [QUERY_KEYS.GET_POST_BY_ID, { postId }],
      });
    },
  });

  return mutation;
};
