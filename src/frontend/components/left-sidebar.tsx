"use client";

import Image from "next/image";
import Link from "next/link";
import { usePathname } from "next/navigation";
import { SignInButton, useUser } from "@clerk/nextjs";
import { IconCirclePlus, IconLogin } from "@tabler/icons-react";

import { useGetUserFromAuth } from "@/features/users/api/queries/use-get-user-from-auth";

import { cn } from "@/lib/utils";
import { LeftSidebarLinks } from "@/constants";
import { Loader } from "@/components/loader";
import { Skeleton } from "@/components/ui/skeleton";
import { Spotlight } from "@/components/ui/spotlight";
import { FormPostModal } from "@/components/modals/form-post-modal";

export const LeftSidebar = () => {
  const pathname = usePathname();

  const { data: user, isLoading } = useGetUserFromAuth();
  const { isLoaded, isSignedIn } = useUser();

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
                  "sidebar-link",
                  isActive && "bg-neutral-900 hover:bg-neutral-800"
                )}>
                <Link href={href} className="flex gap-4 items-center p-3">
                  {Icon && <Icon />}

                  <p className="base-medium">{label}</p>
                </Link>
              </li>
            );
          })}

          <FormPostModal>
            <li className="sidebar-link">
              <div className="flex gap-4 items-center p-3">
                <IconCirclePlus />
                <p className="base-medium">Create</p>
              </div>
            </li>
          </FormPostModal>

          {isLoaded && isSignedIn && !isLoading && (
            <li
              className={cn(
                "sidebar-link",
                pathname === "/profile" && "bg-neutral-900 hover:bg-neutral-800"
              )}>
              <Link
                href={`/users/${user?.id}`}
                className="flex gap-4 items-center p-3">
                <Image
                  src={user?.imageUrl || "/profile-image.png"}
                  alt={user?.username || "user"}
                  width={30}
                  height={30}
                  className={cn(
                    "rounded-full object-cover",
                    !user?.imageUrl && "bg-white p-0.5"
                  )}
                />
                <p className="base-medium capitalize">{user?.username}</p>
              </Link>
            </li>
          )}

          {!isLoaded && (
            <li className="sidebar-link flex p-3">
              <Loader />
              <Skeleton className="h-7 w-24" />
            </li>
          )}

          {isLoaded && !isSignedIn && (
            <li className="sidebar-link">
              <SignInButton mode="modal">
                <div className="flex gap-4 items-center p-3">
                  <IconLogin />
                  <p className="base-medium">Login</p>
                </div>
              </SignInButton>
            </li>
          )}
        </ul>
      </div>
    </nav>
  );
};
