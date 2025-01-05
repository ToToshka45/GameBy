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
            sx={{
              textTransform: "none",
              fontFamily: "'Roboto', sans-serif",
              fontSize: "1.5rem",
              display: "flex", // Allows button to size based on content
              // Center vertically if needed
              p: 1, // Padding for aesthetics, adjust as needed
              minWidth: 0, // Ensures no minimum width is set by MUI
            }}
          >
            <i
              className="fa-solid fa-dice"
              style={{
                color: "yellow",
                fontSize: "36px",
                marginRight: 6,
              }}
            ></i>
            Game<span style={{ fontStyle: "italic" }}>By</span>
          </Button>
        </Box>

        {/* 
        <TextField
          variant="outlined"
          placeholder="Search..."
          size="small"
          sx={{
            bgcolor: "white",
            borderRadius: 4,
            width: 400,
          }}
          InputProps={{
            startAdornment: (
              <InputAdornment position="start">
                <SearchIcon />
              </InputAdornment>
            ),
          }}
        /> */}
        <Box display={"flex"}>
          <Stack direction="row" gap={1}>
            {navMenu.map((item, idx) => (
              <Button
                key={idx}
                size="large"
                // sx={{
                //   display: { xs: "none", sm: "inline", md: "inline" },
                //   color: "#fff",
                //   background: "linear-gradient(145deg, #8bc34a, #388e3c)", // Gradient from lighter to deeper color
                //   borderRadius: "8px", // Rounded corners for better appearance
                //   border: "1px solid #000000", // Slightly darker border color
                //   // boxShadow:
                //   //   "3px 3px 6px rgba(0,0,0,0.2), -3px -3px 6px rgba(255,255,255,0.5)", // Dual direction shadow
                //   transition: "all 0.2s ease-in-out", // Smooth transition for interactivity

                //   ":hover": {
                //     background: "linear-gradient(145deg, #FFC107, #FFCA28)", // Slightly change gradient on hover
                //     boxShadow: "inset 3px 3px 5px rgba(0,0,0,0.2)", // Inner shadow effect on hover
                //   },

                //   ":active": {
                //     boxShadow: "inset 2px 2px 3px rgba(0,0,0,0.3)", // More pronounced inner shadow on click
                //     transform: "translateY(1px)", // Small movement for press effect
                //   },
                // }}
                sx={{
                  display: { xs: "none", sm: "inline", md: "inline" },
                  color: "antiqueWhite",
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
