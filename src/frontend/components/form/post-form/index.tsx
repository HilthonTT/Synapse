"use client";

import { z } from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";

import { cn } from "@/lib/utils";
import { PostValidation } from "@/lib/validation";
import { Form } from "@/components/ui/form";
import { Info } from "@/components/form/post-form/info";
import { STEPS } from "@/components/modals/create-post-modal";
import { Image } from "@/components/form/post-form/image";

type Props = {
  step: STEPS;
};

export const PostForm = ({ step }: Props) => {
  const form = useForm<z.infer<typeof PostValidation>>({
    resolver: zodResolver(PostValidation),
    defaultValues: {},
  });

  const onSubmit = () => {};

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-6 flex-1">
        <section className="mb-12 space-y-4">
          <h1 className="h3-bold md:h2-bold">Upload your post today! 👍</h1>
          <p className="base-medium">Show everyone what you&apos;re up to!</p>
        </section>

        <Image
          form={form}
          className={cn(step === STEPS.IMAGE ? "block" : "hidden")}
        />

        <Info
          form={form}
          className={cn(step === STEPS.INFO ? "block" : "hidden")}
        />
      </form>
    </Form>
  );
};
