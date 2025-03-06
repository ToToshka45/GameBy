import { useEffect } from "react";
import { useAuth } from "../contexts/AuthContext";
import LoginRequest from "../interfaces/LoginRequest";
import axios from "../services/axios";
import AuthData from "../types/AuthData";
import { jwtDecode } from "jwt-decode";
import ExtendedJwtPayload from "../interfaces/ExtendedJwtPayload";

const useLogin = () => {
  const { userAuth, setUserAuth } = useAuth() as AuthData;

  useEffect(() => {
    console.log("User data is acquired: ", userAuth);
  }, [userAuth]);

  const login = async (data: LoginRequest) => {
    const res = await axios.post("auth/login", data);
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
