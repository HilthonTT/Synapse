"use client";

import Image from "next/image";
import {
  IconDots,
  IconHeartFilled,
  IconMessageDots,
} from "@tabler/icons-react";
import { useRouter } from "next/navigation";

import { Button } from "@/components/ui/button";

type Props = {
  post: Post;
};

export const Header = ({ post }: Props) => {
  const router = useRouter();

  const loadCreatorPage = () => {
    router.push(`/user/${post.creator.userId}`);
  };

  return (
    <div className="flex items-center gap-4">
      <Image
        src={post.creator.imageUrl}
        alt={post.creator.username || "creator"}
        width={32}
        height={32}
      />
      <div className="flex items-center gap-1 overflow-hidden">
        <p
          onClick={loadCreatorPage}
          className="capitalize font-semibold text-sm cursor-pointer hover:text-neutral-400 transition line-clamp-1 overflow-hidden text-ellipsis max-w-[150px]"
          title={post.creator.username}>
          {post.creator.username}
        </p>
        <span>-</span>
        <p
          className="font-semibold text-sm line-clamp-1 overflow-hidden text-ellipsis max-w-[200px]"
          title={post.title}>
          {post.title}
        </p>
      </div>
      <div className="flex items-center gap-1">
        <IconHeartFilled className="text-red-500" size={18} />
        <p className="text-sm font-semibold">{post.likesCount}</p>
      </div>
      <div className="flex items-center gap-1">
        <IconMessageDots size={18} />
        <p className="text-sm font-semibold">{post.commentsCount}</p>
      </div>
      <div className="ml-auto">
        <Button variant="ghost" className="hover:opacity-75">
          <IconDots size={24} />
          <p className="sr-only">options</p>
        </Button>
      </div>
    </div>
  );
};
