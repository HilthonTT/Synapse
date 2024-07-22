import Image from "next/image";
import { IconHeartFilled, IconMessageDots } from "@tabler/icons-react";

import { PinContainer } from "@/components/ui/3d-pin";

type Props = {
  post: SearchPost;
};

export const PostCard = ({ post }: Props) => {
  const url = `${process.env.NEXT_PUBLIC_BASE_URL!}/posts/${post.id}`;

  return (
    <PinContainer title={post.title} href={url}>
      <div className="size-[16rem] relative">
        <Image src={post.imageUrl} alt={post.title} unoptimized fill />

        <div className="absolute top-2 left-2 p-2 gap-6 items-center flex">
          <div className="flex items-center gap-1">
            <IconHeartFilled className="text-red-500" />
            <p className="text-white font-bold">{post?.likesCount}</p>
          </div>

          <div className="flex items-center gap-1">
            <IconMessageDots />
            <p className="text-white font-bold">{post?.commentsCount}</p>
          </div>
        </div>
      </div>
    </PinContainer>
  );
};
