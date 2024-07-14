"use client";

import Image from "next/image";
import Link from "next/link";
import { usePathname } from "next/navigation";
import { UserButton, useUser } from "@clerk/nextjs";

import { cn } from "@/lib/utils";
import { BottomBarLinks } from "@/constants";
import { Loader } from "@/components/loader";
import { Skeleton } from "@/components/ui/skeleton";

export const BottomBar = () => {
  const pathname = usePathname();

  const { user, isLoaded, isSignedIn } = useUser();

  return (
    <div className="bottom-bar">
      {BottomBarLinks.map(({ href, icon: Icon }) => {
        const isActive = pathname === href;

        return (
          <Link
            key={href}
            href={href}
            className={cn(
              "flex-center flex-col gap-1 p-2 transition",
              isActive && "bg-neutral-900 hover:bg-neutral-800 rounded-xl"
            )}>
            {Icon && <Icon />}
          </Link>
        );
      })}

      {isLoaded && isSignedIn && (
        <div
          className={cn(
            "flex-center flex-col gap-1 p-2 transition",
            pathname === "/profile" &&
              "bg-neutral-900 hover:bg-neutral-800 rounded-xl"
          )}>
          <Image
            src={user.imageUrl}
            alt={user.username || "user"}
            width={26}
            height={26}
          />
        </div>
      )}

      {!isLoaded && (
        <li className="rounded-lg text-sm transition gap-3 p-3 flex items-center">
          <Loader />
          <Skeleton className="h-7 w-24" />
        </li>
      )}
    </div>
  );
};
