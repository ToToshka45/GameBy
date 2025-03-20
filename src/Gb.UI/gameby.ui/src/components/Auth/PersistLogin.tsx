import { useEffect, useState } from "react";
import { Outlet } from "react-router";
import AuthData from "../../interfaces/AuthData";
import useRefreshToken from "../../hooks/useRefreshToken";
import { Typography } from "@mui/material";
import useAuth from "../../hooks/useAuth";

export const PersistLogin = () => {
  const { userAuth } = useAuth() as AuthData;
  const refresh = useRefreshToken();
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const persist = async () => {
      try {
        console.log("Refreshing a previous token: ", userAuth?.accessToken);
        await refresh();
      } catch (err) {
        console.error(err);
      } finally {
        setIsLoading(false);
      }
    };

    !userAuth?.accessToken ? persist() : setIsLoading(false);
  }, []);

  useEffect(() => {
    console.log("Updated AccessToken: ", userAuth?.accessToken);
  }, [isLoading]);

  return isLoading ? <Typography>Loading...</Typography> : <Outlet />;
};
