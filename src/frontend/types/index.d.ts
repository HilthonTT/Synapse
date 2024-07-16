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

declare type NewPost = {
  title: string;
  imageUrl: string;
  location?: string;
  tags?: string;
};

declare type CursorPaginationPost = {
  posts: Post[];
  nextCursor?: string;
  previousCursor?: string;
};

declare type Card = {
  id: string;
  content: string;
  className: string;
  thumbnail: string;
};
