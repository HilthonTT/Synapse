import { useQuery } from "@tanstack/react-query";

import { getUserFromAuth } from "@/actions/users/get-user-from-auth";
import { QUERY_KEYS } from "@/lib/react-query/query-keys";

export const useGetUserFromAuth = () => {
  const query = useQuery({
    queryKey: [QUERY_KEYS.GET_USER_FROM_AUTH],
    queryFn: async () => await getUserFromAuth(),
  });

  return query;
};
