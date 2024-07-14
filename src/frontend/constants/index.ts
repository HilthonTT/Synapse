import {
  IconHome,
  IconSearch,
  IconDeviceFloppy,
  IconUsers,
  IconCirclePlus,
} from "@tabler/icons-react";

export const BaseApiUrl = process.env.BASE_API_URL!;

export const jwtTemplate = process.env.CLERK_TEMPLATE!;

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

export const BottomBarLinks: NavLink[] = [
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
  {
    href: "/create",
    label: "Create",
    icon: IconCirclePlus,
  },
];
