import { useQuery } from "@tanstack/react-query";

import { SortColumn } from "@/features/posts/api/sort-column";
import { SortOrder } from "@/features/posts/api/sort-order";

import { searchPosts } from "@/actions/posts/search-posts";
import { QUERY_KEYS } from "@/lib/react-query/query-keys";

export const useSearchPosts = (
  searchTerm?: string,
  sortOrder = SortOrder.DESCENDING,
  sortColumn = SortColumn.LIKES,
  limit = 10
) => {
  const query = useQuery({
    queryKey: [
      QUERY_KEYS.GET_SEARCH_POSTS,
      {
        searchTerm,
        sortOrder,
        sortColumn,
      },
    ],
    queryFn: async () =>
      await searchPosts(sortOrder, sortColumn, searchTerm, limit),
  });

  return query;
};
