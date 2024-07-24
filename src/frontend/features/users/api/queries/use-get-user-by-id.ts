import { useQuery } from "@tanstack/react-query";

import { getUserById } from "@/actions/users/get-user-by-id";
import { QUERY_KEYS } from "@/lib/react-query/query-keys";

export const useGetUserById = (id: string) => {
  const query = useQuery({
    enabled: !!id,
    queryKey: [QUERY_KEYS.GET_USER_BY_ID, { id }],
    queryFn: async () => await getUserById(id),
  });

  return query;
};
