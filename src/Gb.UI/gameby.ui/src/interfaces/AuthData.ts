import UserAuth from "./UserAuth";

export default interface AuthData {
  userAuth: UserAuth | undefined;
  setUserAuth: React.Dispatch<React.SetStateAction<UserAuth | undefined>> | undefined;
  // isLoggedIn: boolean;
  // setIsLoggedIn: React.Dispatch<React.SetStateAction<boolean>> | undefined;
};