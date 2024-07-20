import { useMutation, useQueryClient } from "@tanstack/react-query";

import { createPost } from "@/actions/post";
import { QUERY_KEYS } from "@/lib/react-query/query-keys";
import { likePost, unlikePost } from "@/actions/likes";
import { createComment } from "@/actions/comments";

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

export const useLikePost = (postId: string) => {
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: async () => likePost(postId),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [QUERY_KEYS.GET_POST_LIKES, { postId }],
      });
    },
  });

  return mutation;
};

export const useUnlikePost = (postId: string) => {
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: async () => unlikePost(postId),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [QUERY_KEYS.GET_POST_LIKES, { postId }],
      });
    },
  });

  return mutation;
};

export const useCreateComment = (postId: string) => {
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: async (content: string) => createComment(postId, content),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [QUERY_KEYS.GET_POST_COMMENTS, { postId }],
      });
    },
  });

  return mutation;
};
