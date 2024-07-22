"use client";

import { Modal, ModalBody, ModalTrigger } from "@/components/ui/animated-modal";
import { DeletePostContent } from "@/components/modals/delete-post-modal/delete-post-content";

type Props = {
  postId: string;
  children: React.ReactNode;
};

export const DeletePostModal = ({ children, postId }: Props) => {
  return (
    <Modal>
      <ModalTrigger>{children}</ModalTrigger>
      <ModalBody>
        <DeletePostContent postId={postId} />
      </ModalBody>
    </Modal>
  );
};
