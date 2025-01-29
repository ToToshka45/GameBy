import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import App from "./App";
import Layout from "./pages/Layout";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import CreateEventPage from "./pages/CreateEventPage";
import HomePage from "./pages/HomePage";
import MyEventsPage from "./pages/MyEventsPage";
import NotFoundPage from "./pages/NotFoundPage";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Layout />,
    errorElement: <NotFoundPage />,
    children: [
      {
        path: "/",
        element: <HomePage />,
        errorElement: <NotFoundPage />,
      },
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
]);
createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>
);
