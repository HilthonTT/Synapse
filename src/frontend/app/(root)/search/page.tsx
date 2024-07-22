"use client";

import { Loader } from "@/components/loader";
import { SearchInput } from "@/components/search-input";
import {
  SortColumn,
  SortOrder,
  useSearchPosts,
} from "@/lib/react-query/queries";

import { PostCard } from "./_components/post-card";

type Props = {
  searchParams: {
    searchTerm?: string;
    sortOrder?: number;
    sortColumn?: number;
  };
};

const SearchPage = ({
  searchParams: { searchTerm, sortOrder, sortColumn },
}: Props) => {
  const { data: posts, isLoading } = useSearchPosts(
    searchTerm,
    sortOrder || SortOrder.DESCENDING,
    sortColumn || SortColumn.LIKES
  );

  return (
    <div className="flex flex-col flex-1 items-center size-full overflow-y-auto custom-scrollbar">
      <h2 className="h3-bold md:h2-bold w-full px-5 py-4">Search posts</h2>
      <SearchInput />

      {isLoading ? (
        <div className="flex-center my-8 h-full">
          <Loader />
        </div>
      ) : (
        <div className="flex flex-wrap items-center justify-center md:justify-normal w-full">
          {posts?.map((post) => (
            <PostCard key={post.id} post={post} />
          ))}
        </div>
      )}
    </div>
  );
};

export default SearchPage;
