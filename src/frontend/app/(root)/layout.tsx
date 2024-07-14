import { BottomBar } from "@/components/bottom-bar";
import { LeftSidebar } from "@/components/left-sidebar";

type Props = {
  children: React.ReactNode;
};

const RootLayout = ({ children }: Props) => {
  return (
    <div className="w-full md:flex h-full">
      <LeftSidebar />

      <div className="flex flex-1 h-full">{children}</div>

      <BottomBar />
    </div>
  );
};

export default RootLayout;
