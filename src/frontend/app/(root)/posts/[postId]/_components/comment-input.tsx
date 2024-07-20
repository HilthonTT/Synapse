"use client";

import { z } from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { IconHeart, IconHeartFilled } from "@tabler/icons-react";

import { hasLiked } from "@/lib/utils";
import {
  useCreateComment,
  useLikePost,
  useUnlikePost,
} from "@/lib/react-query/mutations";
import { CommentValidation } from "@/lib/validation";
import { useGetPostLikes, useGetUserFromAuth } from "@/lib/react-query/queries";
import { Loader } from "@/components/loader";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { EmojiPicker } from "@/components/emoji-picker";
import { useToast } from "@/components/ui/use-toast";
import { Form, FormControl, FormField, FormItem } from "@/components/ui/form";

type Props = {
  postId: string;
};

export const CommentInput = ({ postId }: Props) => {
  const { toast } = useToast();

  const form = useForm<z.infer<typeof CommentValidation>>({
    resolver: zodResolver(CommentValidation),
    defaultValues: {
      content: "",
    },
  });

  const { data: user, isLoading: userLoading } = useGetUserFromAuth();
  const { data: likes, isLoading: likeLoading } = useGetPostLikes(postId);

  const { mutateAsync: likePost, isPending: isLiking } = useLikePost(postId);
  const { mutateAsync: unlikePost, isPending: isUnliking } =
    useUnlikePost(postId);

  const { mutateAsync: createComment, isPending: isCreatingComment } =
    useCreateComment(postId);

  const isLoggedOut = !user;
  const isLoading = userLoading || likeLoading;
  const isLiked = hasLiked(user?.id, likes || []);

  const toggleLike = async () => {
    if (isLoggedOut) {
      return;
    }

    try {
      if (isLiked) {
        await unlikePost();
      } else {
        await likePost();
      }
    } catch (error) {
      toast({
        title: "Something went wrong",
        description: "Please try again",
      });
    }
  };

  const onSubmit = async (values: z.infer<typeof CommentValidation>) => {
    await createComment(values.content);

    form.reset();
  };

  return (
    <div className="mt-20">
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)}>
          <FormField
            control={form.control}
            name="content"
            render={({ field }) => (
              <FormItem>
                <FormControl>
                  <div className="relative">
                    {isLoading ? (
                      <Loader className="absolute top-2.5 left-3" />
                    ) : (
                      <Button
                        disabled={isLiking || isUnliking || isLoggedOut}
                        onClick={toggleLike}
                        variant="ghost"
                        type="button"
                        className="hover:opacity-75 absolute top-1 left-0">
                        {isLiked ? (
                          <IconHeartFilled className="text-rose-600" />
                        ) : (
                          <IconHeart className="text-zinc-400" />
                        )}
                        <span className="sr-only">
                          {isLiked ? "Unlike" : "Like"}
                        </span>
                      </Button>
                    )}
                    <Input
                      placeholder="Add a comment..."
                      className="px-14"
                      disabled={isLoggedOut || isCreatingComment}
                      {...field}
                    />
                    <div className="absolute top-3 right-3">
                      <EmojiPicker
                        onChange={(emoji) =>
                          field.onChange(`${field.value} ${emoji}`)
                        }
                        disabled={isLoggedOut}
                      />
                    </div>
                  </div>
                </FormControl>
              </FormItem>
            )}
          />
        </form>
      </Form>
    </div>
  );
};
