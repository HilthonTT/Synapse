"use client";

import Image from "next/image";
import { useState } from "react";
import { motion } from "framer-motion";
import { useRouter } from "next/navigation";
import { IconHeartFilled, IconMessageDots } from "@tabler/icons-react";

import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";

export const LayoutGrid = ({ cards }: { cards: Card[] }) => {
  const [selected, setSelected] = useState<Card | null>(null);
  const [lastSelected, setLastSelected] = useState<Card | null>(null);

  const handleClick = (card: Card) => {
    setLastSelected(selected);
    setSelected(card);
  };

  const handleOutsideClick = () => {
    setLastSelected(selected);
    setSelected(null);
  };

  return (
    <div className="w-full min-h-full p-10 grid grid-cols-1 md:grid-cols-3 max-w-[90rem] mx-auto gap-4 relative">
      {cards.map((card, i) => (
        <div
          key={i}
          className={cn(
            card.className,
            !selected && "cursor-pointer hover:opacity-75 transition"
          )}>
          <motion.div
            onClick={() => handleClick(card)}
            className={cn(
              card.className,
              "relative overflow-hidden",
              selected?.id === card.id
                ? "rounded-lg cursor-pointer absolute inset-0 h-1/2 w-full md:w-1/2 m-auto z-50 flex justify-center items-center flex-wrap flex-col"
                : lastSelected?.id === card.id
                ? "z-40 bg-white rounded-xl h-full w-full"
                : "bg-white rounded-xl h-full w-full"
            )}
            layout>
            {selected?.id === card.id && <SelectedCard selected={selected} />}
            <BlurImage card={card} />
          </motion.div>
        </div>
      ))}
      <motion.div
        onClick={handleOutsideClick}
        className={cn(
          "absolute h-full w-full left-0 top-0 bg-black opacity-0 z-10",
          selected?.id ? "pointer-events-auto" : "pointer-events-none"
        )}
        animate={{ opacity: selected?.id ? 0.3 : 0 }}
      />
    </div>
  );
};

const BlurImage = ({ card }: { card: Card }) => {
  const [loaded, setLoaded] = useState(false);

  return (
    <Image
      src={card.thumbnail}
      height="500"
      width="500"
      onLoad={() => setLoaded(true)}
      className={cn(
        "object-cover object-top md:absolute inset-0 size-full transition duration-200",
        loaded ? "blur-none" : "blur-md"
      )}
      alt="thumbnail"
      unoptimized
    />
  );
};

const SelectedCard = ({ selected }: { selected: Card | null }) => {
  const router = useRouter();

  const onClick = () => {
    if (!selected) {
      return;
    }

    router.push(`/posts/${selected.id}`);
  };

  return (
    <div className="bg-transparent h-full w-full flex flex-col justify-end rounded-lg shadow-2xl relative z-[60]">
      <motion.div
        initial={{
          opacity: 0,
        }}
        animate={{
          opacity: 0.6,
        }}
        className="absolute inset-0 h-full w-full bg-black/40 opacity-60 z-10"
      />

      <div className="absolute top-2 left-2 p-2 gap-6 flex items-center">
        <div className="flex items-center gap-1">
          <IconHeartFilled className="text-red-500" />
          <p className="text-white font-bold">{selected?.likesCount}</p>
        </div>

        <div className="flex items-center gap-1">
          <IconMessageDots />
          <p className="text-white font-bold">{selected?.commentsCount}</p>
        </div>
      </div>
      <motion.div
        initial={{
          opacity: 0,
          y: 100,
        }}
        animate={{
          opacity: 1,
          y: 0,
        }}
        transition={{
          duration: 0.3,
          ease: "easeInOut",
        }}
        className="relative px-8 pb-4 z-[70]">
        <div className="flex-between">
          <div className="flex items-center gap-2">
            {selected && (
              <Image
                src={selected?.creatorImageUrl}
                alt={selected?.creatorName || "creator"}
                width={32}
                height={32}
              />
            )}
            <p className="font-bold tracking-widest line-clamp-1">
              {selected?.content}
            </p>
          </div>
          <Button
            onClick={onClick}
            variant="outline"
            className="font-bold tracking-widest hover:opacity-75">
            Visit
          </Button>
        </div>
      </motion.div>
    </div>
  );
};
