"use client";

import { useState } from "react";
import { IconCirclePlus } from "@tabler/icons-react";

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

export const CreatePostModal = () => {
  const [step, setStep] = useState<STEPS>(STEPS.IMAGE);

  return (
    <Modal>
      <ModalTrigger className="p-0">
        <li className="sidebar-link">
          <div className="flex gap-4 items-center p-3">
            <IconCirclePlus />
            <p className="base-medium">Create</p>
          </div>
        </li>
      </ModalTrigger>
      <ModalBody>
        <ModalContent>
          <PostForm step={step} setStep={setStep} />
        </ModalContent>
      </ModalBody>
    </Modal>
  );
};
