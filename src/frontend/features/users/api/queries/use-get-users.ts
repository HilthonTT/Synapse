import { useQuery } from "@tanstack/react-query";

import { getUsers } from "@/actions/users/get-users";
import { QUERY_KEYS } from "@/lib/react-query/query-keys";

export const useGetUsers = () => {
  const query = useQuery({
    queryKey: [QUERY_KEYS.GET_USERS],
    queryFn: async () => await getUsers(),
  });

  return query;
};
