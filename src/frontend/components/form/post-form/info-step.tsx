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
  className?: string;
};

export const InfoStep = ({ form, className }: Props) => {
  return (
    <div className={cn("space-y-4", className)}>
      <FormField
        control={form.control}
        name="title"
        render={({ field }) => (
          <FormItem className="flex-1 w-full">
            <FormLabel>Title</FormLabel>

            <FormControl>
              <Input {...field} placeholder="My best holiday!" />
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
            <FormLabel>Tags</FormLabel>

            <FormControl>
              <Input {...field} placeholder="Holiday, Trip, Friends" />
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
            <FormLabel>Location</FormLabel>

            <FormControl>
              <Input {...field} placeholder="Wall Street" />
            </FormControl>

            <FormMessage />
          </FormItem>
        )}
      />
    </div>
  );
};
