import { Box, Container } from "@mui/material";
import { CategoryFilter } from "./Filters/CategoryFilter";
import { EventDateFilter } from "./Filters/EventDateFilter";

export const FiltersMenu = () => {
  return (
    <Container>
      <Box display={"flex"} flexDirection={"column"} rowGap={3}>
        <CategoryFilter />
        <EventDateFilter />
      </Box>
    </Container>
  );
};
