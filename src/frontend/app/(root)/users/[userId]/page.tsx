"use client";

import Image from "next/image";
import { UserButton } from "@clerk/nextjs";

import { useGetUserFromAuth } from "@/features/users/api/queries/use-get-user-from-auth";
import { useGetUserFollowerStats } from "@/features/users/api/queries/use-get-user-follower-stats";
import { useGetUserById } from "@/features/users/api/queries/use-get-user-by-id";

import { useGetUserPosts } from "@/features/posts/api/queries/use-get-user-posts";

import { Loader } from "@/components/loader";
import { Button } from "@/components/ui/button";
import { PostCard } from "@/components/post-card";
import { cn } from "@/lib/utils";

import NotFound from "@/app/not-found";

const StatBlock = ({
  value,
  label,
}: {
  value: number | string;
  label: string;
}) => {
  return (
    <div className="flex-center gap-2">
      <p className="small-semibold lg:body-bold">{value}</p>
      <p className="small-medium lg:body-medium">{label}</p>
    </div>
  );
};

type Props = {
  params: {
    userId: string;
  };
};

const UserIdPage = ({ params }: Props) => {
  const { data: currentUser, isLoading: isCurrentUserLoading } =
    useGetUserFromAuth();

  const { data: user, isLoading: isUserLoading } = useGetUserById(
    params.userId
  );

  const { data: followerStats, isLoading: isStatsLoading } =
    useGetUserFollowerStats(params.userId);

  const { data: userPosts, isLoading: isUserPostsLoading } = useGetUserPosts(
    params.userId
  );

  if (
    isUserLoading ||
    isStatsLoading ||
    isCurrentUserLoading ||
    isUserPostsLoading
  ) {
    return (
      <div className="size-full flex-center">
        <Loader />
      </div>
    );
  }

  if (!user || !followerStats) {
    return <NotFound />;
  }

  return (
    <div className="profile-container">
      <div className="profile-inner_container">
        <div className="relative flex xl:flex-row flex-col max-xl:items-center flex-1 gap-7">
          {currentUser?.id !== user.id && (
            <Image
              src={user?.imageUrl || "/profile-image.png"}
              alt={user?.username}
              width={140}
              height={140}
              className={cn(
                "rounded-full object-cover",
                !user.imageUrl && "bg-white p-0.5"
              )}
            />
          )}
          {currentUser?.id === user.id && (
            <div className="mt-4">
              <UserButton
                appearance={{
                  elements: {
                    userButtonAvatarBox: {
                      width: 140,
                      height: 140,
                    },
                    userButtonAvatarImage: {
                      width: 140,
                      height: 140,
                    },
                    rootBox: {
                      width: 140,
                      height: 140,
                      padding: 0,
                    },
                  },
                }}
                afterSwitchSessionUrl="/"
              />
            </div>
          )}
          <div className="flex flex-col flex-1 justify-between md:mt-2">
            <div className="flex flex-col w-full">
              <h1 className="text-center xl:text-left h3-bold md:h1-semibold w-full capitalize">
                {user.username}
              </h1>
              <p className="small-regular text-neutral-400 text-center xl:text-left">
                @{user.name}
              </p>
            </div>
            <div className="flex flex-wrap items-center gap-8 mt-10 justify-center xl:justify-start z-20">
              <StatBlock
                value={followerStats.followerCount}
                label="Followers"
              />
              <StatBlock
                value={followerStats.followingCount}
                label="Following"
              />
            </div>
          </div>
          {currentUser?.id !== user.id && (
            <div className="flex justify-center mt-auto xl:w-auto w-full">
              <Button className="bg-sky-500 hover:bg-sky-600 transition w-full xl:w-auto">
                Follow
              </Button>
            </div>
          )}
        </div>
      </div>

      <div className="flex flex-col items-center">
        <h2 className="text-center small-semibold lg:body-bold">
          Uploaded posts
        </h2>
        <div className="flex flex-wrap items-center justify-center w-full">
          {userPosts?.map((post) => (
            <PostCard key={post.id} post={post} />
          ))}
        </div>
      </div>
    </div>
  );
};

export default UserIdPage;
