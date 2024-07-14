declare type NavLink = {
  href: string;
  label: string;
  icon?: any;
};

declare type UserFromAuth = {
  id: string;
  objectIdentifier: string;
  name: string;
  username: string;
  email: string;
  imageUrl: string;
};

declare type User = {
  userId: string;
  name: string;
  username: string;
  imageUrl: string;
};

declare type Post = {
  id: string;
  title: string;
  imageUrl: string;
  tags: string;
  creator: User;
  likesCount: number;
  commentsCount: number;
};
