import { Box, Container, Typography, Link, Grid2 } from "@mui/material";

const Footer = () => {
  return (
    <Box sx={{ backgroundColor: "primary.main", color: "white", py: 2 }}>
      <Container maxWidth="lg">
        <Grid2 container spacing={4}>
          <Grid2>
            <Typography variant="h6" gutterBottom>
              Company
            </Typography>
            <Typography variant="body2">
              © {new Date().getFullYear()} GameBy
            </Typography>
          </Grid2>
          <Grid2>
            <Typography variant="h6" gutterBottom>
              Contact
            </Typography>
            <Typography variant="body2">
              Email:{" "}
              <Link href="mailto:info@gameby.com" color="inherit">
                info@gameby.com
              </Link>
            </Typography>
            <Typography variant="body2">
              Phone:{" "}
              <Link href="tel:+1234567890" color="inherit">
                +1234567890
              </Link>
            </Typography>
          </Grid2>
          <Grid2>
            <Typography variant="h6" gutterBottom>
              Follow Us
            </Typography>
            <Typography variant="body2">
              <Link href="#" color="inherit">
                Facebook
              </Link>
            </Typography>
            <Typography variant="body2">
              <Link href="#" color="inherit">
                Twitter
              </Link>
            </Typography>
          </Grid2>
        </Grid2>
      </Container>
    </Box>
  );
};

export default Footer;
