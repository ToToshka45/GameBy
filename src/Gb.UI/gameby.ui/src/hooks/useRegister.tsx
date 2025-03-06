import { useAuth } from "../contexts/AuthContext";
import AuthData from "../types/AuthData";
import SignUpFormData from "../schemas/SignUpForm";
import { jwtDecode } from "jwt-decode";
import ExtendedJwtPayload from "../interfaces/ExtendedJwtPayload";
import axios from "../services/axios";
import RegisterUserRequest from "../interfaces/Requests/RegisterUserRequest";

const useRegister = () => {
  const { userAuth, setUserAuth } = useAuth() as AuthData;
  const ac = new AbortController();

  const register = async (data: SignUpFormData) => {
    try {
      const registerUserRequest: RegisterUserRequest = {
        username: data.username,
        email: data.email,
        password: data.password,
      };
      const stringified = JSON.stringify(registerUserRequest);
      console.log("Sending a register payload: ", stringified);
      const res = await axios.post("register/new", stringified, {
        signal: ac.signal,
        headers: { "Content-Type": "application/json" },
        // withCredentials: true,
      });

      if (res.data) {
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
      console.log("User data: ", userAuth);

      return userAuth;
    } catch (err) {
      console.error("Error has occured while a user register: ", err);
      ac.abort;
    }
    return null;
  };

  return register;
};

export default useRegister;
