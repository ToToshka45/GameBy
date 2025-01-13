import { AppBar, Toolbar, IconButton, Button, Stack, Box } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import AccountCircle from "@mui/icons-material/AccountCircle";

const navMenu = ["New Event", "My Events"];

export const NavbarV2 = () => {
  return (
    <AppBar
      sx={{
        // backgroundColor: "royalBlue",
        // background: "linear-gradient(to right, #135dcd, #2b89d6)", // Gradient effect
        background: "linear-gradient(to right, #8b0000, #b22222)", // Gradient effect
      }}
    >
      <Toolbar>
        <IconButton
          size="large"
          edge="start"
          color="inherit"
          aria-label="menu"
          // sx={{
          //   border: "2px solid lightblue", // Add a solid border
          //   borderRadius: "50%", // Make sure it's circular if needed
          //   "&:hover": {
          //     borderColor: "black", // Ensure the border color stays visible on hover
          //   },
          // }}
        >
          <MenuIcon />
        </IconButton>

        <Box component="div" display="flex" sx={{ flexGrow: 1 }}>
          <Button
            size="large"
            color="inherit"
            startIcon={
              <i
                className="fa-solid fa-dice"
                style={{
                  color: "yellow",
                  fontSize: "28px",
                }}
              ></i>
            }
            sx={{
              textTransform: "none",
              fontFamily: "'Roboto', sans-serif",
              fontSize: "1.5rem",
              display: "flex", // Allows button to size based on content
              // Center vertically if needed
              p: 1, // Padding for aesthetics, adjust as needed
              // minWidth: 0, // Ensures no minimum width is set by MUI
            }}
          >
            Game<span style={{ fontStyle: "italic" }}>By</span>
          </Button>
        </Box>

        <Box display={"flex"}>
          <Stack direction="row" gap={1}>
            {navMenu.map((item, idx) => (
              <Button
                key={idx}
                size="medium"
                sx={{
                  display: { xs: "none", sm: "inline", md: "inline" },
                  color: "antiqueWhite",
                  borderRadius: 2,
                }}
              >
                {item}
              </Button>
            ))}
          </Stack>

          <IconButton size="large" color="inherit" sx={{ ml: 3 }}>
            <AccountCircle />
          </IconButton>
        </Box>
      </Toolbar>
    </AppBar>
  );
};
