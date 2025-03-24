import { useEffect, useState } from "react";
import AuthData from "../interfaces/AuthData";
import { authAxios } from "../services/axios";
import useAuth from "./useAuth";

const useRefreshToken = () => {
  const { userAuth, setUserAuth } = useAuth() as AuthData;
  const [isDataLoaded, setIsDataLoaded] = useState(false);

  // const privateAxios = usePrivateAxios();
  const refresh = async (): Promise<string | undefined> => {
    try {
      const res = await authAxios.get("auth/refresh", {
        withCredentials: true,
      });

      if (res && res.data) {
        console.log("Received a refreshed token: ", res.data);
        setUserAuth!({ ...res.data });
        setIsDataLoaded(true);
      } else {
        throw new Error(
          "Unknown error has occured while fetching a new AccessToken."
        );
      }
    } catch (err) {
      console.error(
        "Error has occured while refreshing the access token: ",
        err
      );
      setUserAuth!(undefined);
    }
    return undefined;
  };

  useEffect(() => {
    if (isDataLoaded) console.log("Received new userAuth data: ", userAuth);
    setIsDataLoaded(false);
  }, [isDataLoaded]);

  return refresh;
};

export default useRefreshToken;
