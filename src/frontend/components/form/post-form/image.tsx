"use client";

import { ImageUploader } from "@/components/image-uploader";
import { FormControl, FormField, FormItem } from "@/components/ui/form";
import { cn } from "@/lib/utils";

type Props = {
  form: any;
  className?: string;
};

export const Image = ({ form, className }: Props) => {
  return (
    <div className={cn(className)}>
      <FormField
        control={form.control}
        name="imageUrl"
        render={({ field }) => (
          <FormItem>
            <FormControl>
              <ImageUploader fieldChange={field.onChange} />
            </FormControl>
          </FormItem>
        )}
      />
    </div>
  );
};
