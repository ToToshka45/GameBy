import { useEffect } from "react";
import LoginRequest from "../interfaces/Requests/LoginRequest";
import { authAxios } from "../services/axios";
import AuthData from "../interfaces/AuthData";
import { jwtDecode } from "jwt-decode";
import ExtendedJwtPayload from "../interfaces/ExtendedJwtPayload";
import useAuth from "./useAuth";

const useLogin = () => {
  const { userAuth, setUserAuth } = useAuth() as AuthData;

  useEffect(() => {
    console.log("User data is acquired: ", userAuth);
  }, [userAuth]);

  const login = async (data: LoginRequest) => {
    const res = await authAxios.post("auth/login", data, {
      withCredentials: true,
    });

    console.log("Login response: ", res.data);
    if (res) {
      const { id, username, email, accessToken, refreshToken } = res.data;
      const decoded = jwtDecode<ExtendedJwtPayload>(accessToken);
      const roles = decoded?.roles;

      setUserAuth!({
        id,
        username,
        email,
        accessToken,
        refreshToken,
        roles,
      });
    }
  };

  return login;
};

export default useLogin;
