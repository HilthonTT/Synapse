"use client";

import Image from "next/image";
import { IconDots } from "@tabler/icons-react";
import { redirect, useRouter } from "next/navigation";

import { useGetPostBydId, useGetPostComments } from "@/lib/react-query/queries";
import { DirectionAwareHover } from "@/components/ui/direction-aware-hover";
import { Button } from "@/components/ui/button";
import { ScrollArea } from "@/components/ui/scroll-area";
import { Loader } from "@/components/loader";

import { CommentInput } from "./_components/comment-input";

type Props = {
  params: {
    postId: string;
  };
};

const PostIdPage = ({ params: { postId } }: Props) => {
  const router = useRouter();

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

  const loadCreatorPage = () => {
    router.push(`/user/${post.creator.userId}`);
  };

  return (
    <div className="flex-center w-full mx-4">
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
          <div className="flex items-center gap-4">
            <Image
              src={post.creator.imageUrl}
              alt={post.creator.username || "creator"}
              width={32}
              height={32}
            />
            <p
              onClick={loadCreatorPage}
              className="capitalize font-semibold text-sm cursor-pointer hover:text-neutral-400 transition">
              {post.creator.username}
            </p>
            <div className="ml-auto">
              <Button variant="ghost" className="hover:opacity-75">
                <IconDots size={24} />
                <p className="sr-only">options</p>
              </Button>
            </div>
          </div>
          <hr className="my-4 border-white/20" />

          <ScrollArea className="h-60 2xl:h-[500px]">
            {comments?.map((comment) => (
              <p key={comment.id}>{comment.content}</p>
            ))}
          </ScrollArea>

          <CommentInput postId={postId} />
        </div>
      </div>
    </div>
  );
};

export default PostIdPage;
