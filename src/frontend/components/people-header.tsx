"use client";

import { useGetUsers } from "@/features/users/api/queries/use-get-users";

import { AnimatedTooltip } from "@/components/ui/animated-tooltip";
import { Skeleton } from "./ui/skeleton";

type Props = {
  limit?: number;
};

export const PeopleHeader = ({ limit = 18 }: Props) => {
  const { data: users, isLoading } = useGetUsers(limit);

  if (isLoading) {
    return <PeopleHeaderSkeleton />;
  }

  const people =
    users?.map((user) => {
      return {
        id: user.id,
        name: user.username,
        designation: `@${user.name}`,
        image: user.imageUrl,
      };
    }) || [];

  return (
    <div className="flex flex-row items-center justify-center mb-10 w-full mt-4">
      <AnimatedTooltip items={people} />
    </div>
  );
};

const PeopleHeaderSkeleton = () => {
  return (
    <div className="flex flex-row items-center justify-center mb-10 w-full mt-4 gap-4">
      {Array(18)
        .fill(null)
        .map((_, idx) => (
          <Skeleton key={idx} className="p-8 rounded-full bg-neutral-900" />
        ))}
    </div>
  );
};
