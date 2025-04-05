import React, {
  createContext,
  PropsWithChildren,
  useEffect,
  useState,
} from "react";
import AuthData from "../interfaces/AuthData";
import UserAuth from "../interfaces/UserAuth";

const AuthContext = createContext<AuthData | undefined>(undefined);

export const AuthProvider: React.FC<PropsWithChildren> = ({ children }) => {
  const [userAuth, setUserAuth] = useState<UserAuth | undefined>();

  useEffect(() => {
    // set the auth context on component load
  }, []);

  return (
    <AuthContext.Provider
      value={{
        userAuth,
        setUserAuth,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContext;
