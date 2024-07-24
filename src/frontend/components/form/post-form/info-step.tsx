"use client";

import {
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { cn } from "@/lib/utils";

type Props = {
  form: any;
  disabled: boolean;
  className?: string;
};

export const InfoStep = ({ form, className, disabled }: Props) => {
  return (
    <div className={cn("space-y-4", className)}>
      <FormField
        control={form.control}
        name="title"
        render={({ field }) => (
          <FormItem className="flex-1 w-full">
            <FormLabel>Make up a title</FormLabel>

            <FormControl>
              <Input
                {...field}
                placeholder="My best holiday!"
                disabled={disabled}
              />
            </FormControl>

            <FormMessage />
          </FormItem>
        )}
      />

      <FormField
        control={form.control}
        name="tags"
        render={({ field }) => (
          <FormItem className="flex-1 w-full">
            <FormLabel>
              Add Tags (separated by comma &apos; , &apos; )
            </FormLabel>

            <FormControl>
              <Input
                {...field}
                placeholder="Holiday, Trip, Friends"
                disabled={disabled}
              />
            </FormControl>

            <FormMessage />
          </FormItem>
        )}
      />

      <FormField
        control={form.control}
        name="location"
        render={({ field }) => (
          <FormItem className="flex-1 w-full">
            <FormLabel>Add Location</FormLabel>

            <FormControl>
              <Input {...field} placeholder="Wall Street" disabled={disabled} />
            </FormControl>

            <FormMessage />
          </FormItem>
        )}
      />
    </div>
  );
};
