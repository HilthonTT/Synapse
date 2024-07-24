"use client";

import { useSearchPosts } from "@/features/posts/api/queries/use-search-posts";
import { SortColumn } from "@/features/posts/api/sort-column";
import { SortOrder } from "@/features/posts/api/sort-order";

import { Loader } from "@/components/loader";
import { PostCard } from "@/components/post-card";
import { SearchInput } from "@/components/search-input";

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
