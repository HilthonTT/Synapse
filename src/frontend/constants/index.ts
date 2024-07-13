import {
  IconHome,
  IconSearch,
  IconDeviceFloppy,
  IconUsers,
} from "@tabler/icons-react";

export const LeftSidebarLinks: NavLink[] = [
  {
    href: "/",
    label: "Home",
    icon: IconHome,
  },
  {
    href: "/search",
    label: "Search",
    icon: IconSearch,
  },
  {
    href: "/saved",
    label: "Saves",
    icon: IconDeviceFloppy,
  },
  {
    href: "/people",
    label: "People",
    icon: IconUsers,
  },
];
