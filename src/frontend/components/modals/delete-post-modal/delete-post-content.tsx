"use client";

import { useRouter } from "next/navigation";

import { useDeletePost } from "@/features/posts/api/mutations/use-delete-post";

import { Loader } from "@/components/loader";
import {
  ModalContent,
  ModalFooter,
  useModal,
} from "@/components/ui/animated-modal";
import { TextGenerateEffect } from "@/components/ui/text-generate-effect";
import { useToast } from "@/components/ui/use-toast";

type Props = {
  postId: string;
};

export const DeletePostContent = ({ postId }: Props) => {
  const router = useRouter();

  const { toast } = useToast();
  const { setOpen } = useModal();

  const { mutateAsync: deletePost, isPending: isPostDeleting } =
    useDeletePost(postId);

  const handleDelete = async () => {
    try {
      await deletePost();

      router.push("/");
    } catch (error) {
      toast({
        title: "Something went wrong",
        description: "Please try again",
      });
    } finally {
      setOpen(false);
    }
  };

  const firstLabel =
    "This action cannot be reversed. Are you sure you want to delete your post?";

  const secondLabel = `
    Are you absolutely sure you want to delete this post? 
    This action cannot be undone`;

  return (
    <>
      <ModalContent>
        <h4 className="text-lg md:text-2xl text-neutral-600 dark:text-neutral-100 font-bold text-center mb-8">
          Delete your{" "}
          <span className="px-1 py-0.5 rounded-md bg-gray-100 dark:bg-neutral-800 dark:border-neutral-700 border border-gray-200">
            post
          </span>{" "}
          now? 🗑️
        </h4>
        <div className="flex items-center justify-between gap-8 h-full">
          <div className="bg-neutral-900 p-4 size-full flex-1 rounded-xl hover:bg-neutral-800 transition">
            <div className="flex-center">
              <h2 className="text-[32px] md:text-[60px] text-center">😥</h2>
            </div>
            <TextGenerateEffect words={firstLabel} className="text-center" />
          </div>
          <div className="bg-neutral-900 p-4 h-full flex-1 rounded-xl hover:bg-neutral-800 transition">
            <div className="flex items-center justify-center flex-col h-full">
              <h2 className="text-[32px] md:text-[60px] text-center">❓</h2>
              <TextGenerateEffect words={secondLabel} className="text-center" />
            </div>
          </div>
        </div>
      </ModalContent>
      <ModalFooter className="gap-4">
        <button
          disabled={isPostDeleting}
          onClick={() => setOpen(false)}
          className="inline-flex h-12 animate-shimmer items-center justify-center rounded-md border border-slate-800 bg-[linear-gradient(110deg,#000103,45%,#1e2631,55%,#000103)] bg-[length:200%_100%] px-6 font-medium text-slate-400 transition-colors focus:outline-none focus:ring-2 focus:ring-slate-400 focus:ring-offset-2 focus:ring-offset-slate-50">
          Cancel
        </button>

        <button
          disabled={isPostDeleting}
          onClick={handleDelete}
          className="h-12 animate-shimmer items-center justify-center rounded-md border border-slate-800 bg-[linear-gradient(110deg,#000103,45%,#1e2631,55%,#000103)] bg-[length:200%_100%] px-6 font-medium text-slate-400 transition-colors focus:outline-none focus:ring-2 focus:ring-slate-400 focus:ring-offset-2 focus:ring-offset-slate-50">
          {isPostDeleting ? <Loader /> : "Remove"}
        </button>
      </ModalFooter>
    </>
  );
};
