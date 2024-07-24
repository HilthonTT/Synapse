import https from "https";
import axios from "axios";
import { auth } from "@clerk/nextjs/server";

import { BASE_API_URL, JWT_TEMPLATE } from "@/constants";

export const createAxiosInstance = async (url?: string) => {
  const { getToken } = auth();

  const token = await getToken({ template: JWT_TEMPLATE });

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
    baseURL: url || BASE_API_URL,
  });

  return instance;
};
