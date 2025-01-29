import { Box } from "@mui/material";
import { Navbar } from "../components/Navbar";
import { Outlet } from "react-router-dom";

function Layout() {
  return (
    <Box p={0} m={0}>
      <Navbar />
      <Outlet />
    </Box>
  );
}

export default Layout;
