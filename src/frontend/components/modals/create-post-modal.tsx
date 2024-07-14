"use client";

import { useState } from "react";
import { IconCirclePlus } from "@tabler/icons-react";

import { BaseModal } from "@/components/modals/base-modal";
import { PostForm } from "@/components/form/post-form";

export enum STEPS {
  IMAGE = 0,
  INFO = 1,
}

export const CreatePostModal = () => {
  const [step, setStep] = useState<STEPS>(STEPS.IMAGE);

  const onPrevious = () => {
    setStep(STEPS.IMAGE);
  };

  const onNext = () => {
    setStep(STEPS.INFO);
  };

  const body = <PostForm step={step} />;

  const trigger = (
    <li className="sidebar-link">
      <div className="flex gap-4 items-center p-3">
        <IconCirclePlus />
        <p className="base-medium">Create</p>
      </div>
    </li>
  );

  const footer = (
    <>
      {step === STEPS.INFO && (
        <button
          type="button"
          onClick={onPrevious}
          className="inline-flex h-12 animate-shimmer items-center justify-center rounded-md border border-slate-800 bg-[linear-gradient(110deg,#000103,45%,#1e2631,55%,#000103)] bg-[length:200%_100%] px-6 font-medium text-slate-400 transition-colors focus:outline-none focus:ring-2 focus:ring-slate-400 focus:ring-offset-2 focus:ring-offset-slate-50">
          Previous
        </button>
      )}
      <button
        type="button"
        onClick={onNext}
        className="inline-flex h-12 animate-shimmer items-center justify-center rounded-md border border-slate-800 bg-[linear-gradient(110deg,#000103,45%,#1e2631,55%,#000103)] bg-[length:200%_100%] px-6 font-medium text-slate-400 transition-colors focus:outline-none focus:ring-2 focus:ring-slate-400 focus:ring-offset-2 focus:ring-offset-slate-50">
        Next
      </button>
    </>
  );

  return <BaseModal body={body} trigger={trigger} footer={footer}></BaseModal>;
};
