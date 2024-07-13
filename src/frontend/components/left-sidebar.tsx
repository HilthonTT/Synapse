"use client";

import Image from "next/image";
import Link from "next/link";
import { usePathname } from "next/navigation";
import { useUser } from "@clerk/nextjs";

import { cn } from "@/lib/utils";
import { LeftSidebarLinks } from "@/constants";
import { Loader } from "@/components/loader";
import { Skeleton } from "@/components/ui/skeleton";
import { Spotlight } from "@/components/ui/spotlight";

export const LeftSidebar = () => {
  const pathname = usePathname();

  const { user, isLoaded, isSignedIn } = useUser();

  return (
    <nav className="left-sidebar">
      <Spotlight className="top-10 right-2 h-[80vh] w-[50vw]" fill="white" />
      <Spotlight className="top-0 left-[80%] h-[60vh] w-[40vw]" fill="white" />

      <div className="flex flex-col gap-10 relative">
        <Link href="/" className="flex items-center justify-center">
          <Image
            src="/full-logo-white-transparent.png"
            alt="logo"
            width={150}
            height={36}
            className="object-cover"
          />
        </Link>

        <ul className="flex flex-col gap-6 mx-2">
          {LeftSidebarLinks.map(({ href, label, icon: Icon }) => {
            const isActive = pathname === href;

            return (
              <li
                key={href}
                className={cn(
                  "rounded-lg text-sm hover:bg-neutral-900 transition gap-3",
                  isActive && "bg-neutral-900 hover:bg-neutral-800"
                )}>
                <Link href={href} className="flex gap-4 items-center p-3">
                  {Icon && <Icon />}

                  <p className="base-medium">{label}</p>
                </Link>
              </li>
            );
          })}

          {isLoaded && isSignedIn && (
            <li
              className={cn(
                "rounded-lg text-sm hover:bg-neutral-900 transition gap-3",
                pathname === "/profile" && "bg-neutral-900 hover:bg-neutral-800"
              )}>
              <Link href="/profile" className="flex gap-4 items-center p-3">
                <Image
                  src={user.imageUrl}
                  alt={user.username || "user"}
                  width={30}
                  height={30}
                />
                <p className="base-medium capitalize">{user.username}</p>
              </Link>
            </li>
          )}

          {!isLoaded && (
            <li className="rounded-lg text-sm transition gap-3 p-3 flex items-center">
              <Loader />
              <Skeleton className="h-7 w-24" />
            </li>
          )}
        </ul>
      </div>
    </nav>
  );
};
