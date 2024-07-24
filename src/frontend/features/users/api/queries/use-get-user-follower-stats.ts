import { useQuery } from "@tanstack/react-query";

import { getFollowerStatsByUserId } from "@/actions/users/get-user-follower-stats";
import { QUERY_KEYS } from "@/lib/react-query/query-keys";

export const useGetUserFollowerStats = (userId: string) => {
  const query = useQuery({
    queryKey: [QUERY_KEYS.GET_USER_FOLLOWER_STATS],
    queryFn: async () => await getFollowerStatsByUserId(userId),
  });

  return query;
};
