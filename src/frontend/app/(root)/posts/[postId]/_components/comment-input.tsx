"use client";

import { IconHeart, IconHeartFilled } from "@tabler/icons-react";

import { hasLiked } from "@/lib/utils";
import { useLikePost, useUnlikePost } from "@/lib/react-query/mutations";
import { useGetPostLikes, useGetUserFromAuth } from "@/lib/react-query/queries";
import { Loader } from "@/components/loader";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { EmojiPicker } from "@/components/emoji-picker";
import { useToast } from "@/components/ui/use-toast";

type Props = {
  postId: string;
};

export const CommentInput = ({ postId }: Props) => {
  const { toast } = useToast();

  const { data: user, isLoading: userLoading } = useGetUserFromAuth();
  const { data: likes, isLoading: likeLoading } = useGetPostLikes(postId);

  const { mutateAsync: likePost, isPending: isLiking } = useLikePost(postId);
  const { mutateAsync: unlikePost, isPending: isUnliking } =
    useUnlikePost(postId);

  const isLoading = userLoading || likeLoading;
  const isLiked = hasLiked(user?.id, likes || []);

  const toggleLike = async () => {
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

  return (
    <div className="mt-20">
      <div className="relative">
        {isLoading ? (
          <Loader className="absolute top-2.5 left-3" />
        ) : (
          <Button
            disabled={isLiking || isUnliking}
            onClick={toggleLike}
            variant="ghost"
            className="hover:opacity-75 absolute top-1 left-0">
            {isLiked ? (
              <IconHeartFilled className="text-rose-600" />
            ) : (
              <IconHeart className="text-zinc-400" />
            )}
            <span className="sr-only">{isLiked ? "Unlike" : "Like"}</span>
          </Button>
        )}
        <Input placeholder="Add a comment..." className="px-14" />
        <div className="absolute top-3 right-3">
          <EmojiPicker onChange={() => {}} />
        </div>
      </div>
    </div>
  );
};
