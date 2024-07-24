"use client";

import { useEffect } from "react";
import { useInView } from "react-intersection-observer";

import { useGetInfinitePosts } from "@/features/posts/api/queries/use-get-infinite-posts";

import { LayoutGrid } from "@/components/ui/layout-grid";
import { SearchInput } from "@/components/search-input";
import { Loader } from "@/components/loader";

const HomePage = () => {
  const { ref, inView } = useInView();
  const { data, fetchNextPage, hasNextPage, isFetchingNextPage, isLoading } =
    useGetInfinitePosts();

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
        creatorName: post.creator.username,
        creatorImageUrl: post.creator.imageUrl,
        className:
          index === 0 || (index + 1) % 4 === 0 ? "md:col-span-2" : "col-span-1",
        thumbnail: post.imageUrl,
        likesCount: post.likesCount,
        commentsCount: post.commentsCount,
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
      {(isFetchingNextPage || isLoading) && (
        <div className="flex-center my-8">
          <Loader />
        </div>
      )}
    </div>
  );
};

export default HomePage;
