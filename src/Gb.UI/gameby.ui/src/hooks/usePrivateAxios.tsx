import { useEffect } from "react";
import useRefreshToken from "./useRefreshToken";
import AuthData from "../interfaces/AuthData";
import axios from "axios";
import useAuth from "./useAuth";
import { privateAxios } from "../services/axios";

const usePrivateAxios = () => {
  const { userAuth } = useAuth() as AuthData;
  const refresh = useRefreshToken();

  useEffect(() => {
    const requestIntercept = privateAxios.interceptors.request.use(
      (config) => {
        // if the Authorization header is not set - we should do it before a request is sent
        if (!config.headers["Authorization"]) {
          console.log("Setting the Authorization header.");
          config.headers["Authorization"] = `Bearer ${userAuth?.accessToken}`;
        }
        return config;
      },
      (error) => {
        return Promise.reject(error);
      }
    );

    // we set a callback which will occur in the case of an error. The error we`re looking for is 403, meaning our AccessToken is expired.
    const responseIntercept = privateAxios.interceptors.response.use(
      (response) => response,
      async (error) => {
        if (!axios.isAxiosError(error)) return;
        console.error("Received error as a result of the request: ", error);
        console.error("Received error as a response: ", error.response);
        // we get the request we sent
        const prevReq = error?.config;

        // if a header "sent" is absent, we add it with the false value
        if (prevReq && prevReq.headers && !prevReq?.headers["sent"]) {
          prevReq.headers["sent"] = false;
          console.log(
            "Added a header 'sent' with the value: ",
            prevReq.headers["sent"]
          );
        }

        // we try to resend the request only if it exists, if an error is 403 Forbidden and if we haven`t done it yet (defined with the 'sent' header)
        if (
          prevReq &&
          // 'sent' is a custom property which allows us to track the number of retries, which we want to be 1 exactly
          error.response?.status === 401 &&
          prevReq.headers &&
          prevReq?.headers["sent"] === false
        ) {
          console.log("Received 401 response. Requesting a new access token.");
          prevReq.headers["sent"] = true; // now we set it to true, so we`re not going to send it again if something goes wrong
          const accessToken = await refresh();
          // set the refreshed token
          prevReq.headers["Authorization"] = `Bearer ${accessToken}`;
          return privateAxios(prevReq);
        }

        return Promise.reject(error);
      }
    );

    return () => {
      privateAxios.interceptors.request.eject(requestIntercept);
      privateAxios.interceptors.response.eject(responseIntercept);
    };
  }, [userAuth, refresh]);

  return privateAxios;
};

export default usePrivateAxios;
