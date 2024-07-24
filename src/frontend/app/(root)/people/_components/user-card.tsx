"use client";

import Link from "next/link";

import { useGetUserFollowerStats } from "@/features/users/api/queries/use-get-user-follower-stats";

import { Loader } from "@/components/loader";
import { Button } from "@/components/ui/button";
import { EvervaultCard, Icon } from "@/components/ui/evervault-card";
import { formatFollowerCountLabel } from "@/lib/utils";

type Props = {
  user: User;
};

export const UserCard = ({ user }: Props) => {
  const { data: followerStats, isLoading } = useGetUserFollowerStats(user.id);

  return (
    <div className="border border-black/[0.2] dark:border-white/[0.2] flex flex-col items-start max-w-[20rem] p-4 relative h-[24rem]">
      <Icon className="absolute h-6 w-6 -top-3 -left-3 dark:text-white text-black" />
      <Icon className="absolute h-6 w-6 -bottom-3 -left-3 dark:text-white text-black" />
      <Icon className="absolute h-6 w-6 -top-3 -right-3 dark:text-white text-black" />
      <Icon className="absolute h-6 w-6 -bottom-3 -right-3 dark:text-white text-black" />

      <EvervaultCard imageUrl={user.imageUrl} />

      <div className="w-full text-center">
        <p className="mt-4 font-semibold tracking-wider capitalize">
          {user.username}
        </p>
        {isLoading ? (
          <div className="flex-center my-2">
            <Loader />
          </div>
        ) : (
          <p className="text-neutral-300 mt-2 text-xs tracking-wide">
            {formatFollowerCountLabel(followerStats!)}
          </p>
        )}
      </div>

      <div className="flex-center w-full px-2 mt-4">
        <Button
          className="bg-sky-500 hover:bg-sky-600 transition w-1/2"
          asChild>
          <Link href={`/users/${user.id}`}>
            <span className="font-bold tracking-wider">Visit Profile</span>
          </Link>
        </Button>
      </div>
    </div>
  );
};
