"use client";

import { Fragment, useEffect } from "react";
import { useInView } from "react-intersection-observer";

import { getInfinitePosts } from "@/lib/react-query/queries";

const HomePage = () => {
  const { ref, inView } = useInView();

  const { data, fetchNextPage, hasNextPage, isFetchingNextPage, status } =
    getInfinitePosts();

  useEffect(() => {
    if (inView && hasNextPage) {
      fetchNextPage();
    }
  }, [inView, hasNextPage, fetchNextPage]);

  return (
    <div>
      <h1>Home</h1>
      {status === "pending" && <p>Loading...</p>}
      {status === "error" && <p>Error loading posts</p>}
      {data?.pages.map((page, pageIndex) => (
        <Fragment key={pageIndex}>
          {page.posts.map((post) => (
            <div key={post.id}>{post.title}</div>
          ))}
        </Fragment>
      ))}
      <div
        ref={ref}
        style={{ height: "1px", backgroundColor: "transparent" }}
      />
      {isFetchingNextPage && <p>Loading more...</p>}
    </div>
  );
};

export default HomePage;
