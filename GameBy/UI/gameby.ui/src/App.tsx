import { Box, CssBaseline } from "@mui/material";
import { NavbarV2 } from "./components/NavbarV2";
import { FiltersMenu } from "./components/FiltersMenu";
import { OccuringEventsPanel } from "./components/OccuringEventsPanel";

function App() {
  return (
    <Box>
      <CssBaseline />
      <NavbarV2 />
      <FiltersMenu />
      <OccuringEventsPanel />
    </Box>
  );
}

export default App;
