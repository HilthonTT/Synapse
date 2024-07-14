import https from "https";
import axios from "axios";
import { auth } from "@clerk/nextjs/server";

import { BaseApiUrl, jwtTemplate } from "@/constants";

export const createAxiosInstance = async (url?: string) => {
  const { getToken } = auth();

  const token = await getToken({ template: jwtTemplate });

  let headers;
  if (token) {
    headers = {
      Authorization: `Bearer ${token}`,
    };
  }

  const instance = axios.create({
    headers,
    httpsAgent: new https.Agent({
      rejectUnauthorized: false,
    }),
    baseURL: url || BaseApiUrl,
  });

  return instance;
};
