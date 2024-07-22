"use client";

import { ImageUploader } from "@/components/image-uploader";
import {
  FormControl,
  FormField,
  FormItem,
  FormMessage,
} from "@/components/ui/form";
import { cn } from "@/lib/utils";

type Props = {
  form: any;
  className?: string;
};

export const ImageStep = ({ form, className }: Props) => {
  return (
    <div className={cn(className)}>
      <FormField
        control={form.control}
        name="imageUrl"
        render={({ field }) => (
          <FormItem>
            <FormControl>
              <ImageUploader value={field.value} fieldChange={field.onChange} />
            </FormControl>

            <FormMessage />
          </FormItem>
        )}
      />
    </div>
  );
};
