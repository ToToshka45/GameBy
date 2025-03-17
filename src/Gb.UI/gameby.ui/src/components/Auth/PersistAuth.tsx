import { useEffect, useState } from "react";
import { Outlet } from "react-router";
import AuthData from "../../interfaces/AuthData";
import useRefreshToken from "../../hooks/useRefreshToken";
import { Typography } from "@mui/material";
import useAuth from "../../hooks/useAuth";

export const PersistAuth = () => {
  const { userAuth } = useAuth() as AuthData;
  const refresh = useRefreshToken();
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const persist = async () => {
      try {
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
    console.log("Updated AccessToken");
  }, [userAuth]);

  return isLoading ? <Typography>Loading...</Typography> : <Outlet />;
};
