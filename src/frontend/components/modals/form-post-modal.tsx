"use client";

import { useState } from "react";

import { PostForm } from "@/components/form/post-form";
import {
  Modal,
  ModalBody,
  ModalContent,
  ModalTrigger,
} from "@/components/ui/animated-modal";

export enum STEPS {
  IMAGE = 0,
  INFO = 1,
}

type Props = {
  post?: Post;
  children: React.ReactNode;
};

export const FormPostModal = ({ post, children }: Props) => {
  const [step, setStep] = useState<STEPS>(STEPS.IMAGE);

  return (
    <Modal>
      <ModalTrigger className="p-0">{children}</ModalTrigger>
      <ModalBody>
        <ModalContent>
          <PostForm post={post} step={step} setStep={setStep} />
        </ModalContent>
      </ModalBody>
    </Modal>
  );
};
