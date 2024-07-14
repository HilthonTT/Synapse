import { z } from "zod";

export const PostValidation = z.object({
  title: z.string({ message: "Please enter a title" }).max(100, {
    message: "Title must not exceed 100 characters",
  }),
  imageUrl: z.string({
    message: "Please upload an image",
  }),
  location: z.string().optional(),
  tags: z.string().optional(),
});
