import {
  AppBar,
  Toolbar,
  IconButton,
  Button,
  Stack,
  Box,
  Typography,
  Tooltip,
  useMediaQuery,
  useTheme,
  Menu,
  MenuItem,
} from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import AccountCircle from "@mui/icons-material/AccountCircle";
import CreateEventPage from "../pages/CreateEventPage";
import { NavLink, useNavigate } from "react-router";
import EventPage from "../pages/EventPage";
import { useState } from "react";
import { useAuth } from "../contexts/AuthContext";
import AuthData from "../types/AuthData";

const navMenu = [
  {
    name: "Create Event",
    path: "/create-event",
    page: CreateEventPage,
  },
  {
    name: "My Events",
    path: "/my-events",
    page: EventPage,
  },
];

export const Navbar = () => {
  const theme = useTheme();
  const isSmallScreen = useMediaQuery(theme.breakpoints.down("sm"));
  const [anchorElNav, setAnchorElNav] = useState<null | HTMLElement>(null);

  const navigate = useNavigate();
  const { usser: authUser, isLoggedIn } = useAuth() as AuthData;

  function handleNavigateSignInPage() {
    navigate("/sign-in");
  }

  const handleCloseNavMenu = () => {
    setAnchorElNav(null);
  };

  return (
    <AppBar
      position="sticky"
      sx={{
        background: "linear-gradient(to right, #a60e0e, #b22222)", // Gradient effect
      }}
    >
      <Toolbar variant="dense">
        {isSmallScreen && (
          <Tooltip title="Show pages">
            <IconButton
              size="medium"
              edge="start"
              color="inherit"
              aria-label="menu"
              sx={{ marginRight: "10px" }}
              onClick={(ev) => setAnchorElNav(ev.currentTarget)}
            >
              <MenuIcon />
            </IconButton>
          </Tooltip>
        )}
        <Menu
          sx={{ mt: "45px" }}
          id="menu-appbar"
          anchorEl={anchorElNav}
          anchorOrigin={{
            vertical: "top",
            horizontal: "right",
          }}
          keepMounted
          transformOrigin={{
            vertical: "top",
            horizontal: "right",
          }}
          open={Boolean(anchorElNav)}
          onClose={handleCloseNavMenu}
        >
          {navMenu.map((item) => (
            <MenuItem key={item.name} onClick={handleCloseNavMenu}>
              <Button
                to={item.path}
                component={NavLink}
                sx={{ textAlign: "center" }}
              >
                {item.name}
              </Button>
            </MenuItem>
          ))}
        </Menu>
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

          <IconButton
            onClick={handleNavigateSignInPage}
            aria-label="sign-in"
            color="inherit"
            sx={{ ml: 3, gap: 1 }}
          >
            <Typography variant="body1" color="antiqueWhite">
              {isLoggedIn ? authUser?.username : "Sign Up"}
            </Typography>
            <AccountCircle />
          </IconButton>
        </Box>
      </Toolbar>
    </AppBar>
  );
};
