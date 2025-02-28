import { useAuth } from "../contexts/AuthContext";
import axios from "../services/axios";
import AuthData from "../types/AuthData";

const useRefreshToken = () => {
  const { userAuth, setUserAuth } = useAuth() as AuthData;

  const refresh = async (): Promise<string | undefined> => {
    try {
      const res = await axios.get("/RefreshToken", {
        withCredentials: true,
      });

      if (res && res.data) {
        setUserAuth!((prev) => {
          console.info(
            "Access token is ",
            !res.data.accessToken && "not ",
            "received"
          );
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
