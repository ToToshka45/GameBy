import axios from "axios";
import AuthData from "../interfaces/AuthData";
import useAuth from "./useAuth";
import usePrivateAxios from "./usePrivateAxios";

const useRefreshToken = () => {
  const { userAuth, setUserAuth } = useAuth() as AuthData;
  // const privateAxios = usePrivateAxios();
  const refresh = async (): Promise<string | undefined> => {
    try {
      if (!userAuth) {
        console.debug("UserAuth is not initialized. Not sending a refresh.");
        return;
      }
      const res = await axios.get("auth/refresh");
      if (res && res.data) {
        console.log("Received a refreshed token: ", res.data);
        setUserAuth!((prev) => {
          return { ...prev!, accessToken: res.data.accessToken };
        });
        return userAuth?.accessToken;
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
    }
    return undefined;
  };
  return refresh;
};

export default useRefreshToken;
