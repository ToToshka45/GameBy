import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import Layout from "./pages/Layout";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import CreateEventPage from "./pages/CreateEventPage";
import HomePage from "./pages/HomePage";
import EventPage from "./pages/EventPage";
import NotFoundPage from "./pages/NotFoundPage";
import MyEventsPage from "./pages/MyEventsPage";
import OAuthSignInPage from "./pages/OAuthSignInPage";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Layout />,
    errorElement: <NotFoundPage />,
    children: [
      {
        path: "/sign-in",
        element: <OAuthSignInPage />,
      },
      {
        path: "/",
        element: <HomePage />,
      },
      {
        path: "/create-event",
        element: <CreateEventPage />,
      },
      {
        path: "/my-events",
        element: <MyEventsPage />,
      },
      {
        path: "/event/:id",
        element: <EventPage />,
      },
    ],
  },
]);
createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>
);
