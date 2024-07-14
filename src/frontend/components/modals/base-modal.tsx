"use client";

import {
  Modal,
  ModalBody,
  ModalContent,
  ModalFooter,
  ModalTrigger,
} from "@/components/ui/animated-modal";

type Props = {
  trigger: React.ReactNode;
  body: React.ReactNode;
  footer?: React.ReactNode;
};

export const BaseModal = ({ trigger, body, footer }: Props) => {
  return (
    <Modal>
      <ModalTrigger className="p-0">{trigger}</ModalTrigger>
      <ModalBody>
        <ModalContent>{body}</ModalContent>
        {footer && <ModalFooter className="gap-4">{footer}</ModalFooter>}
      </ModalBody>
    </Modal>
  );
};
