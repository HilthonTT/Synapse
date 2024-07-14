"use server";

import { BaseApiUrl } from "@/constants";
import { createAxiosInstance } from "@/lib/axios.config";
import { base64ToFile } from "@/lib/utils";

type UploadFileProps = {
  base64: string;
  fileName: string;
};

export const uploadFile = async ({ base64, fileName }: UploadFileProps) => {
  try {
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
  } catch (error) {
    console.log("BLOB_UPLOAD", error);
    return null;
  }
};
