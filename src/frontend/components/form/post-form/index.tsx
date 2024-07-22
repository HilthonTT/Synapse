"use client";

import { z } from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useRouter } from "next/navigation";
import { useMemo } from "react";

import { cn } from "@/lib/utils";
import { PostValidation } from "@/lib/validation";
import { useCreatePost, useUpdatePost } from "@/lib/react-query/mutations";
import { Form } from "@/components/ui/form";
import { STEPS } from "@/components/modals/form-post-modal";
import { ImageStep } from "@/components/form/post-form/image-step";
import { InfoStep } from "@/components/form/post-form/info-step";
import { useToast } from "@/components/ui/use-toast";
import { Loader } from "@/components/loader";
import { useModal } from "@/components/ui/animated-modal";

type Props = {
  step: STEPS;
  setStep: (step: STEPS) => void;
  post?: Post;
};

export const PostForm = ({ step, setStep, post }: Props) => {
  const router = useRouter();

  const { setOpen } = useModal();
  const { toast } = useToast();

  const { mutateAsync: createPost, isPending: isCreatingPost } =
    useCreatePost();

  const { mutateAsync: updatePost, isPending: isUpdatingPost } = useUpdatePost(
    post?.id || ""
  );

  const form = useForm<z.infer<typeof PostValidation>>({
    resolver: zodResolver(PostValidation),
    defaultValues: {
      title: post?.title || "",
      imageUrl: post?.imageUrl || "",
      location: post?.location || "",
      tags: post?.tags || "",
    },
  });

  const onPrevious = () => {
    setStep(STEPS.IMAGE);
  };

  const onNext = () => {
    setStep(STEPS.INFO);
  };

  const onSubmit = async (values: z.infer<typeof PostValidation>) => {
    if (post) {
      await updatePost({ ...values });
    } else {
      const postId = await createPost({ ...values });

      if (!postId) {
        return toast({
          title: "Something went wrong",
          description: "Please try again later.",
        });
      }

      router.push(`/posts/${postId}`);
    }

    setOpen(false);
  };

  const title = useMemo(() => {
    if (post) {
      return "Update your post 👋";
    }

    return "Upload your post today! 👍";
  }, [post]);

  const description = useMemo(() => {
    if (post) {
      return "Be up to date with your post!";
    }

    return "Show everyone what you're up to!";
  }, [post]);

  const submitButtonLabel = useMemo(() => {
    if (post) {
      return "Update";
    }

    return "Post";
  }, [post]);

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-6 flex-1">
        <section className="mb-12 space-y-4">
          <h1 className="h3-bold md:h2-bold">{title}</h1>
          <p className="base-medium">{description}</p>
        </section>

        <ImageStep
          form={form}
          className={cn(step === STEPS.IMAGE ? "block" : "hidden")}
        />

        <InfoStep
          form={form}
          disabled={isCreatingPost || isUpdatingPost}
          className={cn(step === STEPS.IMAGE ? "hidden" : "block")}
        />

        <div className="mt-auto w-full space-x-4 flex justify-end items-center">
          {step === STEPS.INFO && (
            <button
              disabled={isCreatingPost || isUpdatingPost}
              type="button"
              onClick={onPrevious}
              className="inline-flex h-12 animate-shimmer items-center justify-center rounded-md border border-slate-800 bg-[linear-gradient(110deg,#000103,45%,#1e2631,55%,#000103)] bg-[length:200%_100%] px-6 font-medium text-slate-400 transition-colors focus:outline-none focus:ring-2 focus:ring-slate-400 focus:ring-offset-2 focus:ring-offset-slate-50">
              Previous
            </button>
          )}
          <button
            disabled={isCreatingPost || isUpdatingPost}
            type="button"
            onClick={onNext}
            className={cn(
              "inline-flex h-12 animate-shimmer items-center justify-center rounded-md border border-slate-800 bg-[linear-gradient(110deg,#000103,45%,#1e2631,55%,#000103)] bg-[length:200%_100%] px-6 font-medium text-slate-400 transition-colors focus:outline-none focus:ring-2 focus:ring-slate-400 focus:ring-offset-2 focus:ring-offset-slate-50",
              step === STEPS.INFO && "hidden"
            )}>
            Next
          </button>

          <button
            disabled={isCreatingPost || isUpdatingPost}
            type="submit"
            className={cn(
              " h-12 animate-shimmer items-center justify-center rounded-md border border-slate-800 bg-[linear-gradient(110deg,#000103,45%,#1e2631,55%,#000103)] bg-[length:200%_100%] px-6 font-medium text-slate-400 transition-colors focus:outline-none focus:ring-2 focus:ring-slate-400 focus:ring-offset-2 focus:ring-offset-slate-50 hidden",
              step === STEPS.INFO && "inline-flex"
            )}>
            {isCreatingPost || isUpdatingPost ? <Loader /> : submitButtonLabel}
          </button>
        </div>
      </form>
    </Form>
  );
};
