import { create } from "zustand";

type CreatePostState = {
  isOpen: boolean;
  onOpen: () => void;
  onClose: () => void;
};

export const useCreatePost = create<CreatePostState>((set) => ({
  isOpen: false,
  onOpen: () => set({ isOpen: true }),
  onClose: () => set({ isOpen: false }),
}));
