"use client";

import qs from "query-string";
import { useRouter } from "next/navigation";
import { useState } from "react";

import { cn } from "@/lib/utils";
import { PlaceholdersVanishInput } from "@/components/ui/placeholders-vanish-input";

type Props = {
  className?: string;
};

export const SearchInput = ({ className }: Props) => {
  const router = useRouter();

  const [value, setValue] = useState("");

  const placeholders = [
    "Search...",
    "Find friends, posts, or groups",
    "Search by name, hashtag, or topic",
    "Explore trending topics",
    "Discover new communities",
    "Look for events near you",
    "Search by location",
    "Find photos and videos",
    "Search by date or time",
    "Explore popular tags",
    "Find friends by interests",
    "Search for businesses or services",
    "Discover trending news",
  ];

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setValue(e.target.value);
  };

  const onSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    const url = qs.stringifyUrl(
      {
        url: "/search",
        query: {
          searchTerm: value,
        },
      },
      { skipEmptyString: true, skipNull: true }
    );

    router.push(url);
  };

  return (
    <div className={cn("w-full", className)}>
      <PlaceholdersVanishInput
        placeholders={placeholders}
        onChange={handleChange}
        onSubmit={onSubmit}
      />
    </div>
  );
};
