import { PlaceholdersVanishInput } from "@/components/ui/placeholders-vanish-input";
import { cn } from "@/lib/utils";

type Props = {
  className?: string;
};

export const SearchInput = ({ className }: Props) => {
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
    console.log(e.target.value);
  };

  const onSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    console.log("submitted");
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
