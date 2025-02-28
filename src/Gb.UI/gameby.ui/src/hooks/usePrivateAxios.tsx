import { useEffect } from "react";
import { axiosPrivate } from "../services/axios";
import useRefreshToken from "./useRefreshToken";
import { useAuth } from "../contexts/AuthContext";
import AuthData from "../types/AuthData";
import axios from "axios";

/** Responsible for intercepting a response, recieved by axios.
 * If the response returned 403 (Forbidden) we can retry by reinitizalixing an Access Token. */
const usePrivateAxios = () => {
  const refresh = useRefreshToken();
  const { userAuth } = useAuth() as AuthData;

  useEffect(() => {
    const requestIntercept = axiosPrivate.interceptors.request.use(
      (config) => {
        // if Authorization header is not set - we should do it before a request is sent
        if (!config.headers["Authorization"]) {
          config.headers["Authorization"] = `Bearer ${userAuth?.accessToken}`;
        }
        return config;
      },
      (error) => {
        return Promise.reject(error);
      }
    );

    // we set a callback which will occur in the case of an error. The error we`re looking for is 403, meaning our AccessToken is expired.
    const responseIntercept = axiosPrivate.interceptors.response.use(
      (response) => response,
      async (error: unknown) => {
        if (!axios.isAxiosError(error)) return;

        // we get the request we sent
        const prevReq = error?.config;
        // we try to resend the request only if it exists, if an error if 403 Forbidden and if we haven`t done it yet (defined with the 'sent' header)
        if (
          prevReq &&
          error.status === 403 &&
          prevReq.headers &&
          // 'sent' is a custom property which allows us to track the number of retries, which we want to be 1 exactly
          prevReq.headers["sent"] === false
        ) {
          prevReq.headers["sent"] = true; // now we set it to true, so we`re not going to send it again if something goes wrong
          const accessToken = await refresh();
          // set the refreshed token
          prevReq.headers["Authorization"] = `Bearer ${accessToken}`;
          return axiosPrivate(prevReq);
        }
        return Promise.reject(error);
      }
    );

    return () => {
      axiosPrivate.interceptors.request.eject(requestIntercept);
      axiosPrivate.interceptors.response.eject(responseIntercept);
    };
  }, [userAuth, refresh]);

  return axiosPrivate;
};

export default usePrivateAxios;
