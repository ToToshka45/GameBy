import axios from "axios";
import { getViteEnv } from "../common/utils";

const AUTH_URL = getViteEnv("AUTH_HTTP_URL");
const EVENTS_URL = getViteEnv("EVENTS_HTTP_URL");

export const axiosAuth = axios.create({
    baseURL: AUTH_URL
});

export const axiosPrivate = axios.create({
    baseURL: EVENTS_URL
})