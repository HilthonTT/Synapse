"use client";

import Image from "next/image";
import { useDropzone, FileWithPath } from "react-dropzone";
import { useCallback, useRef, useState } from "react";

import { uploadFile } from "@/actions/blob";
import { cn } from "@/lib/utils";

type Props = {
  fieldChange: (file: File) => void;
  imageUrl?: string;
  className?: string;
};

export const ImageUploader = ({ fieldChange, imageUrl, className }: Props) => {
  const inputRef = useRef<HTMLInputElement | null>(null);

  const [fileUrl, setFileUrl] = useState(imageUrl);

  const onDrop = useCallback(
    async (acceptedFiles: FileWithPath[]) => {
      const file = acceptedFiles[0];

      fieldChange(file);
      setFileUrl(URL.createObjectURL(file));

      await uploadFile(file);
    },
    [fieldChange]
  );

  const { getRootProps, getInputProps } = useDropzone({
    onDrop,
    accept: {
      "image/*": [".png", ".jpeg", ".jpg", ".svg"],
    },
  });

  const handleButtonClick = () => {
    if (inputRef.current) {
      inputRef.current.click();
    }
  };

  return (
    <div
      {...getRootProps()}
      className={cn(
        "flex flex-center flex-col bg-dark-3 rounded-xl cursor-pointer",
        className
      )}>
      <input ref={inputRef} className="hidden" {...getInputProps()} />
      {fileUrl ? (
        <>
          <div className="flex flex-1 justify-start items-center p-5">
            <Image
              src={fileUrl}
              alt="image"
              className="object-cover rounded-xl"
              width={200}
              height={200}
            />
          </div>
        </>
      ) : (
        <div className="relative p-1 flex flex-col justify-center items-center">
          <Image
            src="/full-logo-white-transparent.png"
            alt="logo"
            width={150}
            height={150}
          />

          <div className="text-center">
            <h3 className="text-sm text-light-2 mb-2 mt-6 tracking-widest">
              Drag photo here
            </h3>
            <p className="text-xs mb-6 tracking-widest">SVG, PNG, JPG</p>
          </div>

          <button
            onClick={handleButtonClick}
            type="button"
            className="inline-flex h-12  my-2 animate-shimmer items-center justify-center rounded-md border border-slate-800 bg-[linear-gradient(110deg,#000103,45%,#1e2631,55%,#000103)] bg-[length:200%_100%] px-6 font-medium text-slate-400 transition-colors focus:outline-none focus:ring-2 focus:ring-slate-400 focus:ring-offset-2 focus:ring-offset-slate-50">
            Select from Computer
          </button>
        </div>
      )}
    </div>
  );
};
