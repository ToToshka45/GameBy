import { useEffect, useState } from "react";
import { Outlet } from "react-router";
import AuthData from "../../interfaces/AuthData";
import useRefreshToken from "../../hooks/useRefreshToken";
import { Typography } from "@mui/material";
import useAuth from "../../hooks/useAuth";
import { authAxios } from "../../services/axios";
import axios from "axios";

export const PersistLogin = () => {
  const { userAuth } = useAuth() as AuthData;
  const refresh = useRefreshToken();
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const persist = async () => {
      try {
        await authAxios.get("auth/validate-token", {
          withCredentials: true,
          headers: { Authorization: `Bearer ${userAuth?.accessToken}` },
        });
      } catch (err) {
        if (axios.isAxiosError(err)) {
          console.error(err);
          if (err.response?.status === 401) {
            console.log(
              `Received ${err.response?.status} status code. Refreshing a token.`
            );
            await refresh();
          }
        }
      } finally {
        setIsLoading(false);
      }
    };

    !userAuth?.accessToken ? persist() : setIsLoading(false);
  }, []);

  return isLoading ? <Typography>Loading...</Typography> : <Outlet />;
};
