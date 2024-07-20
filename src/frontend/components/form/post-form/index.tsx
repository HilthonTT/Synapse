"use client";

import { z } from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useRouter } from "next/navigation";

import { cn } from "@/lib/utils";
import { PostValidation } from "@/lib/validation";
import { Form } from "@/components/ui/form";
import { STEPS } from "@/components/modals/create-post-modal";
import { ImageStep } from "@/components/form/post-form/image-step";
import { InfoStep } from "@/components/form/post-form/info-step";
import { useToast } from "@/components/ui/use-toast";
import { useCreatePost } from "@/lib/react-query/mutations";
import { Loader } from "@/components/loader";

type Props = {
  step: STEPS;
  setStep: (step: STEPS) => void;
};

export const PostForm = ({ step, setStep }: Props) => {
  const router = useRouter();

  const { toast } = useToast();

  const { mutateAsync: createPost, isPending: isCreatingPost } =
    useCreatePost();

  const form = useForm<z.infer<typeof PostValidation>>({
    resolver: zodResolver(PostValidation),
    defaultValues: {
      title: "",
      imageUrl: "",
      location: "",
      tags: "",
    },
  });

  const onPrevious = () => {
    setStep(STEPS.IMAGE);
  };

  const onNext = () => {
    setStep(STEPS.INFO);
  };

  const onSubmit = async (values: z.infer<typeof PostValidation>) => {
    const postId = await createPost({ ...values });

    if (!postId) {
      return toast({
        title: "Something went wrong",
        description: "Please try again later.",
      });
    }

    router.push(`/posts/${postId}`);
  };

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-6 flex-1">
        <section className="mb-12 space-y-4">
          <h1 className="h3-bold md:h2-bold">Upload your post today! 👍</h1>
          <p className="base-medium">Show everyone what you&apos;re up to!</p>
        </section>

        <ImageStep
          form={form}
          className={cn(step === STEPS.IMAGE ? "block" : "hidden")}
        />

        <InfoStep
          form={form}
          className={cn(step === STEPS.IMAGE ? "hidden" : "block")}
        />

        <div className="mt-auto w-full space-x-4 flex justify-end items-center">
          {step === STEPS.INFO && (
            <button
              disabled={isCreatingPost}
              type="button"
              onClick={onPrevious}
              className="inline-flex h-12 animate-shimmer items-center justify-center rounded-md border border-slate-800 bg-[linear-gradient(110deg,#000103,45%,#1e2631,55%,#000103)] bg-[length:200%_100%] px-6 font-medium text-slate-400 transition-colors focus:outline-none focus:ring-2 focus:ring-slate-400 focus:ring-offset-2 focus:ring-offset-slate-50">
              Previous
            </button>
          )}
          <button
            disabled={isCreatingPost}
            type="button"
            onClick={onNext}
            className={cn(
              "inline-flex h-12 animate-shimmer items-center justify-center rounded-md border border-slate-800 bg-[linear-gradient(110deg,#000103,45%,#1e2631,55%,#000103)] bg-[length:200%_100%] px-6 font-medium text-slate-400 transition-colors focus:outline-none focus:ring-2 focus:ring-slate-400 focus:ring-offset-2 focus:ring-offset-slate-50",
              step === STEPS.INFO && "hidden"
            )}>
            Next
          </button>

          <button
            disabled={isCreatingPost}
            type="submit"
            className={cn(
              " h-12 animate-shimmer items-center justify-center rounded-md border border-slate-800 bg-[linear-gradient(110deg,#000103,45%,#1e2631,55%,#000103)] bg-[length:200%_100%] px-6 font-medium text-slate-400 transition-colors focus:outline-none focus:ring-2 focus:ring-slate-400 focus:ring-offset-2 focus:ring-offset-slate-50 hidden",
              step === STEPS.INFO && "inline-flex"
            )}>
            {isCreatingPost ? <Loader /> : "Post"}
          </button>
        </div>
      </form>
    </Form>
  );
};
