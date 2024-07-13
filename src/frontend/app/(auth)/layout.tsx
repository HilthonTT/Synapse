import Image from "next/image";

import { BackgroundBeams } from "@/components/ui/background-beams";

type Props = {
  children: React.ReactNode;
};

const AuthLayout = ({ children }: Props) => {
  return (
    <div className="h-screen w-full rounded-md bg-neutral-950 relative flex flex-col items-center justify-center antialiased">
      <section className="container my-auto flex h-full">
        {/* Left side for children */}
        <div className="flex-1 flex items-center justify-center">
          <div className="flex flex-col gap-6 items-center justify-center">
            {children}
          </div>
        </div>
      </section>

      <BackgroundBeams />
    </div>
  );
};

export default AuthLayout;
