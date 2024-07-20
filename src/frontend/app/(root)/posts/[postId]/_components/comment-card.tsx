"use client";

import Image from "next/image";
import Link from "next/link";
import { formatDistanceToNow } from "date-fns";
import { IconDots } from "@tabler/icons-react";

import { useGetUserFromAuth } from "@/lib/react-query/queries";
import { Loader } from "@/components/loader";
import { Button } from "@/components/ui/button";

type Props = {
  comment: PostComment;
};

export const CommentCard = ({ comment }: Props) => {
  const { data: user, isLoading } = useGetUserFromAuth();

  const isOwner = user?.id === comment.user.userId;

  return (
    <div className="flex items-center mb-8">
      <Link
        href={`/users/${comment.user.userId}`}
        className="hover:opacity-75 transition">
        <Image
          src={comment.user.imageUrl}
          alt={comment.user.username}
          width={32}
          height={32}
          className="object-cover rounded-full"
        />
      </Link>
      <div className="flex items-start justify-start ml-2 gap-2 w-full">
        <p className="text-xs font-bold tracking-wider">
          {comment.user.username}
        </p>
        <p className="text-xs tracking-wide line-clamp-1">{comment.content}</p>
        <p className="text-xs text-gray-500 shrink-0">
          {formatDistanceToNow(comment.createdOnUtc, { addSuffix: true })}
        </p>
      </div>

      {isLoading && <Loader className="size-4 mr-2" />}
      {isOwner && (
        <Button variant="ghost" className="hover:opacity-75">
          <IconDots size={18} />
          <span className="sr-only">Comment Options</span>
        </Button>
      )}
    </div>
  );
};
