import { Box, Button, Typography } from "@mui/material";
import { Link as RouterLink } from "react-router";

const NotFoundPage = () => {
  return (
    <Box
      display="flex"
      flexDirection="column"
      alignItems="center"
      justifyItems="center"
    >
      <Box
        component="img"
        src={"src/assets/404-page-not-found.svg"}
        alt="Description of SVG"
        sx={{
          width: "400px", // Customize width as needed
          height: "400px", // Customize height as needed
        }}
      />
      <Typography variant="h5" color="darkOrange" gutterBottom>
        Sorry! The page doesn`t exist.
      </Typography>
      <Button
        to="/"
        component={RouterLink}
        variant="contained"
        color="primary"
        size="large"
        sx={{ width: "200px" }}
      >
        BACK TO HOME PAGE
      </Button>
    </Box>
  );
};

export default NotFoundPage;
