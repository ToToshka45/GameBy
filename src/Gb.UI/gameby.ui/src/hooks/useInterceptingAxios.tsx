import { useEffect } from "react";
import useRefreshToken from "./useRefreshToken";
import AuthData from "../interfaces/AuthData";
import axios from "axios";
import useAuth from "./useAuth";
import { authAxios, eventsAxios } from "../services/axios";

const useInterceptingAxios = () => {
  const { userAuth } = useAuth() as AuthData;
  const refresh = useRefreshToken();

  useEffect(() => {
    const requestIntercept = eventsAxios.interceptors.request.use(
      async (config) => {
        // if the Authorization header is not set - we should do it before a request is sent
        if (!config.headers["Authorization"]) {
          const bearerToken = `Bearer ${userAuth?.accessToken}`;
          console.log("Sending a bearer token: ", bearerToken);
          try {
            const response = await authAxios.get("auth/validate-token", {
              withCredentials: true,
              headers: { Authorization: bearerToken, Retries: 0 },
            });
            console.log(
              "Received a response after validating a token:",
              response
            );
          } catch (err) {
            if (axios.isAxiosError(err))
              if (err.response?.status === 401) {
                console.log(
                  "Trying to refresh after receving 401 StatusCode in Axios interceptor"
                );
                await refresh();
              }
          }
          /* temporary commented. initially we have to add it to the current request, 
             since it is going to be validated before it comes to the event service */
          // console.log("Setting the Authorization header.");
          // config.headers["Authorization"] = `Bearer ${userAuth?.accessToken}`;
        }
        return config;
      },
      (error) => {
        return Promise.reject(error);
      }
    );

    // we set a callback which will occur in the case of an error. The error we`re looking for is 403, meaning our AccessToken is expired.
    const responseIntercept = eventsAxios.interceptors.response.use(
      (response) => response,
      async (error) => {
        if (!axios.isAxiosError(error)) return;
        console.error("Received error as a response: ", error.response);
        // we get the request we sent
        const prevReq = error?.config;
        console.log("Previous request: ", prevReq);
        console.log(
          "The number of send retries: ",
          prevReq?.headers["Retries"]
        );

        // we try to resend the request only if it exists, if an error is 401 and if we haven`t done it yet (defined with the 'sent' header)
        if (
          prevReq &&
          error.response?.status === 401 &&
          prevReq.headers &&
          prevReq?.headers["Retries"] === 0
        ) {
          console.log("Received 401 response. Requesting a new access token.");
          prevReq.headers["Retries"]++;
          const accessToken = await refresh();
          // set the refreshed token
          prevReq.headers["Authorization"] = `Bearer ${accessToken}`;
          return eventsAxios(prevReq);
        }

        return Promise.reject(error);
      }
    );

    return () => {
      eventsAxios.interceptors.request.eject(requestIntercept);
      eventsAxios.interceptors.response.eject(responseIntercept);
    };
  }, [userAuth, refresh]);

  return eventsAxios;
};

export default useInterceptingAxios;
