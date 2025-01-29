import {
  AppBar,
  Toolbar,
  IconButton,
  Button,
  Stack,
  Box,
  Typography,
} from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import AccountCircle from "@mui/icons-material/AccountCircle";
import CreateEventPage from "../pages/CreateEventPage";
import { NavLink } from "react-router";
import MyEventsPage from "../pages/MyEventsPage";

const navMenu = [
  {
    name: "Create Event",
    path: "/create-event",
    component: CreateEventPage,
  },
  {
    name: "My Events",
    path: "/my-events",
    component: MyEventsPage,
  },
];

export const Navbar = () => {
  return (
    <AppBar
      position="sticky"
      sx={{
        height: "50%",
        background: "linear-gradient(to right, #a60e0e, #b22222)", // Gradient effect
      }}
    >
      <Toolbar variant="dense">
        <IconButton
          size="medium"
          edge="start"
          color="inherit"
          aria-label="menu"
          sx={{ marginRight: "10px" }}
        >
          <MenuIcon />
        </IconButton>

        <Box component="div" display="flex" flexGrow={1} alignItems="center">
          <Button
            to="/"
            component={NavLink}
            color="inherit"
            startIcon={
              <i
                className="fa-solid fa-dice"
                style={{
                  color: "yellow",
                  fontSize: "20px",
                }}
              ></i>
            }
            sx={{
              textTransform: "none",
              fontFamily: "'Roboto', sans-serif",
              fontSize: "1.2rem",
              display: "flex", // Allows button to size based on content
              // Center vertically if needed
              p: 1, // Padding for aesthetics, adjust as needed
              // minWidth: 0, // Ensures no minimum width is set by MUI
            }}
          >
            Game<span style={{ fontStyle: "italic" }}>By</span>
          </Button>
        </Box>

        <Box display="flex" alignItems="center">
          <Stack direction="row" gap={1}>
            {navMenu.map((item, idx) => (
              <Button
                to={item.path}
                component={NavLink}
                key={idx}
                size="medium"
                sx={{
                  display: { xs: "none", sm: "inline", md: "inline" },
                  color: "antiqueWhite",
                  borderRadius: 2,
                }}
              >
                <Typography variant="body2">{item?.name}</Typography>
              </Button>
            ))}
          </Stack>

          <IconButton color="inherit" sx={{ ml: 3, gap: 1 }}>
            <Typography variant="body2">LogIn</Typography>
            <AccountCircle />
          </IconButton>
        </Box>
      </Toolbar>
    </AppBar>
  );
};
