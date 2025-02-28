import axios from "axios";
import { useAuth } from "../contexts/AuthContext";
import AuthData from "../types/AuthData";
import SignUpFormData from "../schemas/SignUpForm";

const useRegister = () => {
  const { setUserAuth, isLoggedIn } = useAuth() as AuthData;
  const ac = new AbortController();

  const register = async (dto: SignUpFormData) => {
    try {
      const res = await axios.post("/register", JSON.stringify(dto), {
        signal: ac.signal,
        headers: { "Content-Type": "Application/json" },
        withCredentials: true,
      });

      if (res.data) {
        const { id, userName, email, password, accessToken, refreshToken } =
          res.data;
        setUserAuth!({
          id,
          userName,
          email,
          password,
          accessToken,
          refreshToken,
        });
      }

      console.log(isLoggedIn ? "User is logged in" : "User is not logged in");
    } catch (err) {
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
