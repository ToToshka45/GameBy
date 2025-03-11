import { Navigate, Outlet, useLocation } from "react-router-dom";
import AuthData from "../../interfaces/AuthData";
import { useAuth } from "../../contexts/AuthContext";

/** The Login check component. If a User is not logged in,
 * it redirects him to a LogIn page while replacing the previous browser entry with the new one. */
const RequireAuth = () => {
  const { userAuth } = useAuth() as AuthData;
  const location = useLocation();

  return userAuth ? (
    <Outlet />
  ) : (
    <Navigate to="/sign-in" state={{ from: location }} replace />
  );
};

export default RequireAuth;
