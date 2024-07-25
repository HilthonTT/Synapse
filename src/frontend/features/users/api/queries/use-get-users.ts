import { useQuery } from "@tanstack/react-query";

import { getUsers } from "@/actions/users/get-users";
import { QUERY_KEYS } from "@/lib/react-query/query-keys";

export const useGetUsers = (limit?: number) => {
  const query = useQuery({
    queryKey: [QUERY_KEYS.GET_USERS, { limit }],
    queryFn: async () => await getUsers(limit),
  });

  return query;
};
