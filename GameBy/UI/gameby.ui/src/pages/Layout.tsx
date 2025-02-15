import { Box } from "@mui/material";
import { Navbar } from "../components/Navbar";
import { PropsWithChildren } from "react";

function Layout({ children }: PropsWithChildren) {
  return (
    <Box p={0} m={0}>
      <Navbar />
      {children}
    </Box>
  );
}

export default Layout;
