"use client";

import Image from "next/image";
import { useDropzone, FileWithPath } from "react-dropzone";
import { useCallback, useRef, useState, useTransition } from "react";

import { uploadFile } from "@/actions/blob/upload-file";

import { cn, getBase64 } from "@/lib/utils";

type Props = {
  fieldChange: (fileUrl: string) => void;
  imageUrl?: string;
  value?: string;
  className?: string;
};

export const ImageUploader = ({
  fieldChange,
  imageUrl,
  className,
  value,
}: Props) => {
  const [pending, startTransition] = useTransition();

  const inputRef = useRef<HTMLInputElement | null>(null);

  const [fileUrl, setFileUrl] = useState(imageUrl);

  const onDrop = useCallback(
    (acceptedFiles: FileWithPath[]) => {
      startTransition(async () => {
        const file = acceptedFiles[0];

        const base64 = await getBase64(file);
        const fileUrl = await uploadFile({ base64, fileName: file.name });

        if (fileUrl) {
          setFileUrl(fileUrl);
          fieldChange(fileUrl);
        }
      });
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
      {fileUrl && (
        <div className="flex flex-1 justify-start items-center p-5">
          <Image
            src={fileUrl}
            alt="image"
            className="object-cover rounded-xl"
            width={200}
            height={200}
            unoptimized
          />
        </div>
      )}

      {!fileUrl && value && (
        <div className="flex flex-1 justify-start items-center p-5">
          <Image
            src={value}
            alt="image"
            className="object-cover rounded-xl"
            width={200}
            height={200}
            unoptimized
          />
        </div>
      )}

      {!fileUrl && !value && (
        <div className="relative p-1 flex flex-col justify-center items-center">
          <Image
            src="/full-logo-white-transparent.png"
            alt="logo"
            width={150}
            height={150}
            className={cn("object-cover", pending && "animate-spin")}
          />

          {!pending && (
            <>
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
            </>
          )}
        </div>
      )}
    </div>
  );
};
