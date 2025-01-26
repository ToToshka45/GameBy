import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import App from "./App";
import CreateEventPage from "./pages/CreateEventPage";
import NotFoundPage from "./pages/NotFoundPage";
import MyEventsPage from "./pages/MyEventsPage";

const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
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
]);

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>
);
