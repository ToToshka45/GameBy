import axios from "axios";
import { getViteEnv } from "../common/utils";

const BASE_URL = getViteEnv("BASE_URL");

export default axios.create({
    baseURL: BASE_URL
});

export const axiosPrivate = axios.create({
    baseURL: BASE_URL
})