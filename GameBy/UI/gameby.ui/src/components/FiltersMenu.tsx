import { Box, Container, CssBaseline } from "@mui/material";
import { CategoryPlane } from "./CategoryFilter";
import { EventDateFilter } from "./EventDateFilter";

export const FiltersMenu = () => {
  return (
    <Container>
      <Box display={"flex"} flexDirection={"column"} rowGap={4}>
        <CssBaseline>
          <CategoryPlane />
          <EventDateFilter />
        </CssBaseline>
      </Box>
    </Container>
  );
};
