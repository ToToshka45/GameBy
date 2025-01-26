import { Box, CssBaseline } from "@mui/material";
import { Navbar } from "./components/Navbar.tsx";
import MainPage from "./pages/MainPage.tsx";
import Footer from "./components/Footer.tsx";

function App() {
  return (
    <Box>
      <CssBaseline />
      <Navbar />
      <MainPage />
      {/* <Footer /> */}
    </Box>
  );
}

export default App;
