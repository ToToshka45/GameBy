import { Box, Container } from "@mui/material";
import { CategoryFilter } from "./CategoryFilter";
import { EventDateFilter } from "./EventDateFilter";

export const FiltersMenu = () => {
  return (
    <Container>
      <Box display={"flex"} flexDirection={"column"} rowGap={4}>
        <CategoryFilter />
        <EventDateFilter />
      </Box>
    </Container>
  );
};
