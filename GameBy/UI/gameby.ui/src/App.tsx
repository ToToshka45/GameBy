import { Box, CssBaseline } from "@mui/material";
import { NavbarV2 } from "./components/NavbarV2";
import MainPage from "./components/MainPage.tsx";

function App() {
  return (
    <Box>
      <CssBaseline />
      <NavbarV2 />
      <MainPage />
    </Box>
  );
}

export default App;
