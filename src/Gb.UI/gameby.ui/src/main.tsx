import { createRoot } from "react-dom/client";
import "./index.css";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import CreateEventPage from "./pages/CreateEventPage";
import HomePage from "./pages/HomePage";
import EventDetailsPage from "./pages/EventDetailsPage";
import NotFoundPage from "./pages/NotFoundPage";
import MyEventsPage from "./pages/MyEventsPage";
import SignInFormPage from "./pages/SignInFormPage";
import SignUpFormPage from "./pages/SignUpFormPage";
import App from "./App";
import RequireAuth from "./components/Auth/RequireAuth";
import { AuthProvider } from "./contexts/AuthContext";
import { PersistLogin } from "./components/Auth/PersistLogin";

// components, that are nested within the RequireAuth component children array, are protected with auth
const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    errorElement: <NotFoundPage />,
    children: [
      {
        element: <PersistLogin />,
        children: [
          {
            element: <RequireAuth />,
            children: [
              {
                path: "/create-event",
                element: <CreateEventPage />,
              },
              {
                path: "/my-events",
                element: <MyEventsPage />,
              },
            ],
          },
          {
            path: "/",
            element: <HomePage />,
          },
          {
            path: "/sign-up",
            element: <SignUpFormPage />,
          },
          {
            path: "/sign-in",
            element: <SignInFormPage />,
          },
          {
            path: "/event/:eventId",
            element: <EventDetailsPage />,
          },
        ],
      },
    ],
  },
]);

createRoot(document.getElementById("root")!).render(
  <AuthProvider>
    <RouterProvider router={router} />
  </AuthProvider>
);
