import { useMutation, useQueryClient } from "@tanstack/react-query";

import { createPost, deletePost, updatePost } from "@/actions/post";
import { likePost, unlikePost } from "@/actions/likes";
import { QUERY_KEYS } from "@/lib/react-query/query-keys";
import {
  createComment,
  deleteComment,
  updateComment,
} from "@/actions/comments";

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
    mutationFn: async () => await likePost(postId),
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
    mutationFn: async () => await unlikePost(postId),
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
    mutationFn: async (content: string) => await createComment(postId, content),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [QUERY_KEYS.GET_POST_COMMENTS, { postId }],
      });
    },
  });

  return mutation;
};

export const useUpdateComment = (postId: string) => {
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: async ({
      commentId,
      content,
    }: {
      commentId: string;
      content: string;
    }) => await updateComment(commentId, content),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [QUERY_KEYS.GET_POST_COMMENTS, { postId }],
      });
    },
  });

  return mutation;
};

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

export const useUpdatePost = (postId: string) => {
  const queryClient = useQueryClient();

  const mutation = useMutation({
    mutationFn: async ({
      title,
      tags,
      location,
      imageUrl,
    }: {
      title: string;
      tags?: string;
      location?: string;
      imageUrl: string;
    }) => await updatePost(postId, title, imageUrl, tags, location),
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: [QUERY_KEYS.GET_POST_BY_ID, { postId }],
      });
    },
  });

  return mutation;
};

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
