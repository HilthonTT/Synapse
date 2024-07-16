"use client";

import { useEffect } from "react";
import { useInView } from "react-intersection-observer";

import { getInfinitePosts } from "@/lib/react-query/queries";
import { LayoutGrid } from "@/components/ui/layout-grid";
import { SearchInput } from "@/components/search-input";

const HomePage = () => {
  const { ref, inView } = useInView();
  const { data, fetchNextPage, hasNextPage, isFetchingNextPage, status } =
    getInfinitePosts();

  useEffect(() => {
    if (inView && hasNextPage) {
      fetchNextPage();
    }
  }, [inView, hasNextPage, fetchNextPage]);

  const cards: Card[] =
    data?.pages.flatMap((page) =>
      page.posts.map((post, index) => ({
        id: post.id,
        content: post.title,
        className:
          index === 0 || (index + 1) % 4 === 0 ? "md:col-span-2" : "col-span-1",
        thumbnail: post.imageUrl,
      }))
    ) || [];

  return (
    <div className="flex flex-col flex-1 items-center size-full overflow-y-auto custom-scrollbar">
      <h2 className="h3-bold md:h2-bold w-full px-5 py-4">Explore posts</h2>
      <SearchInput />
      <LayoutGrid cards={cards} />
      <div
        ref={ref}
        style={{ height: "1px", backgroundColor: "transparent" }}
      />
      {isFetchingNextPage && <p>Loading more...</p>}
    </div>
  );
};

export default HomePage;
