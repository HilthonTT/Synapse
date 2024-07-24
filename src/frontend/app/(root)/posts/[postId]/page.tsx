"use client";

import { redirect } from "next/navigation";

import { useGetPostBydId } from "@/features/posts/api/queries/use-get-post-by-id";

import { useGetPostComments } from "@/features/comments/api/queries/use-get-post-comments";

import { DirectionAwareHover } from "@/components/ui/direction-aware-hover";
import { ScrollArea } from "@/components/ui/scroll-area";
import { Loader } from "@/components/loader";

import { CommentInput } from "./_components/comment-input";
import { CommentCard } from "./_components/comment-card";
import { Header } from "./_components/header";

type Props = {
  params: {
    postId: string;
  };
};

const PostIdPage = ({ params: { postId } }: Props) => {
  const { data: post, isLoading: postLoading } = useGetPostBydId(postId);

  const { data: comments, isLoading: commentLoading } =
    useGetPostComments(postId);

  if (postLoading || commentLoading) {
    return (
      <div className="size-full flex-center">
        <Loader />
      </div>
    );
  }

  if (!post) {
    return redirect("/");
  }

  return (
    <div className="flex-center w-full mx-4 mb-12">
      <div className="grid grid-cols-1 2xl:grid-cols-2 w-full gap-2">
        <div className="w-full">
          <div className="relative flex-center">
            <DirectionAwareHover
              imageUrl={post.imageUrl}
              className="2xl:size-[700px]">
              <p className="font-bold text-xl">{post.title}</p>
              <p className="text-xs">{post.tags}</p>
            </DirectionAwareHover>
          </div>
        </div>
        <div className="w-full">
          <Header post={post} />
          <hr className="my-4 border-white/20" />

          <ScrollArea className="h-60 2xl:h-[500px]">
            {comments?.map((comment) => (
              <CommentCard key={comment.id} comment={comment} />
            ))}
          </ScrollArea>

          <CommentInput postId={postId} />
        </div>
      </div>
    </div>
  );
};

export default PostIdPage;
