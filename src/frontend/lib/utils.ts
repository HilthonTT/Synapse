import { type ClassValue, clsx } from "clsx";
import { useMemo } from "react";
import { twMerge } from "tailwind-merge";

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}

export const base64ToFile = (base64: string, fileName: string) => {
  const arr = base64.split(",");
  const mime = arr[0].match(/:(.*?);/)?.[1];
  const bstr = atob(arr[1]);

  let n = bstr.length;
  const u8arr = new Uint8Array(n);

  while (n--) {
    u8arr[n] = bstr.charCodeAt(n);
  }

  return new File([u8arr], fileName, {
    type: mime,
  });
};

export const getBase64 = (file: File): Promise<string> =>
  new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result as string);
    reader.onerror = (error) => reject(error);
  });

export const hasLiked = (userId: string | undefined, likes: Like[]) => {
  if (!userId) {
    return false;
  }

  const like = likes.find((like) => like.userId === userId);

  return !!like;
};

export const formatFollowerCountLabel = (followerStat: FollowerStats) => {
  const { followerCount } = followerStat;

  if (followerCount === 1) {
    return "1 Follower";
  }

  let formattedCount: string;

  if (followerCount >= 1_000_000) {
    formattedCount = `${(followerCount / 1_000_000).toFixed(1)}M`;
  } else if (followerCount >= 1_000) {
    formattedCount = `${(followerCount / 1_000).toFixed(1)}k`;
  } else {
    formattedCount = followerCount.toString();
  }

  // Remove any trailing '.0' for whole numbers
  formattedCount = formattedCount.replace(/\.0$/, "");

  return `${formattedCount} followers`;
};
