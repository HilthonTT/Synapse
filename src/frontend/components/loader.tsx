import { IconLoader2 } from "@tabler/icons-react";

import { cn } from "@/lib/utils";

type Props = {
  className?: string;
  size?: number;
};

export const Loader = ({ className, size }: Props) => {
  return (
    <div className={cn("flex items-center", className)}>
      <IconLoader2 className="animate-spin" size={size} />
    </div>
  );
};
