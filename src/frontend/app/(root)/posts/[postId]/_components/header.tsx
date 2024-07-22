"use client";

import Image from "next/image";
import {
  IconDots,
  IconHeartFilled,
  IconMessageDots,
  IconTrash,
} from "@tabler/icons-react";
import { useRouter } from "next/navigation";

import { Button } from "@/components/ui/button";
import { Loader } from "@/components/loader";
import { useGetUserFromAuth } from "@/lib/react-query/queries";
import { FormPostModal } from "@/components/modals/form-post-modal";
import { DeletePostModal } from "@/components/modals/delete-post-modal";

type Props = {
  post: Post;
};

export const Header = ({ post }: Props) => {
  const router = useRouter();

  const { data: user, isLoading } = useGetUserFromAuth();

  const loadCreatorPage = () => {
    router.push(`/user/${post.creator.userId}`);
  };

  const isOwner = user?.id === post.creator.userId;

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
      {isOwner && (
        <div className="ml-auto flex items-center">
          <FormPostModal post={post}>
            <Button variant="ghost" className="hover:opacity-75 P-1">
              <IconDots size={20} />
              <p className="sr-only">options</p>
            </Button>
          </FormPostModal>
          <DeletePostModal postId={post.id}>
            <Button variant="ghost" className="hover:opacity-75">
              <IconTrash size={20} />
              <p className="sr-only">delete</p>
            </Button>
          </DeletePostModal>
        </div>
      )}
      {isLoading && (
        <div className="ml-auto mr-3.5">
          <Loader size={20} />
        </div>
      )}
    </div>
  );
};
