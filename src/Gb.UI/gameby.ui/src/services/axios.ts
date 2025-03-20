import axios from "axios";
import { getViteEnv } from "../common/utils";

const EVENTS_URL = getViteEnv("EVENTS_HTTP_URL");
const AUTH_URL = getViteEnv("AUTH_HTTP_URL");

export const baseAxios = axios.create({
    baseURL: AUTH_URL
});

export const privateAxios = axios.create({
  baseURL: EVENTS_URL,
  withCredentials: true,
});