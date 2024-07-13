import { IconLoader2 } from "@tabler/icons-react";

import { cn } from "@/lib/utils";

type Props = {
  className?: string;
};

export const Loader = ({ className }: Props) => {
  return (
    <div className={cn("flex items-center", className)}>
      <IconLoader2 className="animate-spin" />
    </div>
  );
};
