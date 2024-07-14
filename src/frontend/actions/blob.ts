"use server";

import { BaseApiUrl } from "@/constants";
import { createAxiosInstance } from "@/lib/axios.config";

type UploadFileProps = {
  base64: string;
  fileName: string;
};

const base64ToFile = (base64: string, fileName: string) => {
  const arr = base64.split(",");
  const mime = arr[0].match(/:(.*?);/)?.[1];
  const bstr = atob(arr[1]);

  let n = bstr.length;
  const u8arr = new Uint8Array(n);

  while (n--) {
    u8arr[n] = bstr.charCodeAt(n);
  }

  return new File([u8arr], fileName, {
    type: mime,
  });
};

export const uploadFile = async ({ base64, fileName }: UploadFileProps) => {
  const file = base64ToFile(base64, fileName);

  const formData = new FormData();
  formData.append("file", file);

  const api = await createAxiosInstance();

  const response = await api.post("/api/v1/files", formData, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });

  const fileId = response.data as string;

  const fileUrl = `${BaseApiUrl}/api/v1/files/${fileId}`;

  return fileUrl;
};
