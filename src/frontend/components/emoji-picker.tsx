"use client";

import { IconMoodSmileDizzy } from "@tabler/icons-react";
import Picker from "@emoji-mart/react";
import data from "@emoji-mart/data";
import { useTheme } from "next-themes";

import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { cn } from "@/lib/utils";

type Props = {
  onChange: (value: string) => void;
  disabled?: boolean;
};

export const EmojiPicker = ({ onChange, disabled }: Props) => {
  const { resolvedTheme } = useTheme();

  return (
    <Popover>
      <PopoverTrigger disabled={disabled} className={cn(disabled && "hidden")}>
        <IconMoodSmileDizzy className="text-zinc-400 hover:text-zinc-300 transition" />
      </PopoverTrigger>
      <PopoverContent
        side="right"
        sideOffset={40}
        className="bg-transparent border-none shadow-none drop-shadow-none mb-16">
        <Picker
          theme={resolvedTheme}
          data={data}
          onEmojiSelect={(emoji: any) => onChange(emoji.native)}
        />
      </PopoverContent>
    </Popover>
  );
};
