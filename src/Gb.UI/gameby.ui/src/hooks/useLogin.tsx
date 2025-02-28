import { users } from "../common/consts/fakeData/fakeUsers";
import { useAuth } from "../contexts/AuthContext";
import ExtendedJwtPayload from "../interfaces/ExtendedJwtPayload";
import LoginRequest from "../interfaces/LoginRequest";
import axios from "../services/axios";
import AuthData from "../types/AuthData";
import { jwtDecode } from "jwt-decode";

const useLogin = () => {
  const { userAuth, setUserAuth } = useAuth() as AuthData;

  const login = async (data: LoginRequest) => {
    try {
      // const res = await axios.post("/login", data);
      // test admin
      console.log("Users: ", users);
      const res = users.find(
        (u) =>
          (u.userName !== null
            ? u.userName === data.username
            : u.email === data.email) && u.password === data.password
      );
      console.log("Found a user: ", res);
      const accessToken = res?.accessToken;

      // if (accessToken) {
      //   const decoded = jwtDecode<ExtendedJwtPayload>(accessToken);
      //   const roles = decoded.roles;
      //   console.log("Acquired decoded: ", decoded);
      //   setUserAuth!({ ...prev!, roles: roles });
      // } else {
      //   throw new Error("User not found");
      // }

      const decoded = jwtDecode<ExtendedJwtPayload>(accessToken!);
      console.log("Acquired decoded: ", decoded);
      const roles = decoded.roles;
      console.log("Exctracted roles: ", roles);
      setUserAuth!({ ...res!, roles: roles });

      console.log("User is now ", userAuth);
    } catch (err) {
      console.error("Error has occured while signing in: ", err);
    }
  };

  return login;
};

export default useLogin;
