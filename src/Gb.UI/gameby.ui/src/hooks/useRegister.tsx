import { useAuth } from "../contexts/AuthContext";
import AuthData from "../types/AuthData";
import SignUpFormData from "../schemas/SignUpForm";
import { jwtDecode } from "jwt-decode";
import ExtendedJwtPayload from "../interfaces/ExtendedJwtPayload";
import axios from "../services/axios";

const useRegister = () => {
  const { userAuth, setUserAuth } = useAuth() as AuthData;
  const ac = new AbortController();

  const register = async (dto: SignUpFormData) => {
    try {
      const stringified = JSON.stringify(dto);
      console.log("Sending a register payload: ", stringified);
      const res = await axios.post("register/new", stringified, {
        signal: ac.signal,
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
      });

      if (res.data) {
        const { id, userName, email, password, accessToken, refreshToken } =
          res.data;
        const decoded = jwtDecode<ExtendedJwtPayload>(accessToken);
        const roles = decoded?.roles;

        setUserAuth!({
          id,
          userName,
          email,
          password,
          accessToken,
          refreshToken,
          roles,
        });
      }
      console.log("User data: ", userAuth);
    } catch (err) {
      console.error("Error has occured while a user register: ", err);
      // if (!err?.response)
      //   console.error("Unknown error. Please, try again later.");
      // if (err.response.status == 409)
      //   console.error("Username is taken. Provide another.");
      ac.abort;
    }
  };

  return register;
};

export default useRegister;
