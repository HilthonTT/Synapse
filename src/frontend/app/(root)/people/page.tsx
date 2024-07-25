"use client";

import { useGetUsers } from "@/features/users/api/queries/use-get-users";

import { Loader } from "@/components/loader";

import { UserCard } from "./_components/user-card";

const PeoplePage = () => {
  const { data: users, isLoading } = useGetUsers();

  return (
    <div className="flex flex-col flex-1 size-full overflow-y-auto custom-scrollbar">
      <h2 className="h3-bold md:h2-bold w-full px-5 py-4">You may know...</h2>

      {isLoading && (
        <div className="size-full flex-center">
          <Loader />
        </div>
      )}

      <div className="flex flex-wrap items-center justify-center md:justify-normal w-full p-8 gap-16">
        {users?.map((user) => (
          <UserCard key={user.id} user={user} />
        ))}
      </div>
    </div>
  );
};

export default PeoplePage;
