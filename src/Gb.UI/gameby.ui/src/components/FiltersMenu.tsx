import { Box, Container } from "@mui/material";
import { CategoryFilter } from "./Filters/CategoryFilter";
import { EventDateFilter } from "./Filters/EventDateFilter";
import EventTitleFilter from "./Filters/EventTitleFilter";

export const FiltersMenu = () => {
  return (
    <Container>
      <Box display={"flex"} flexDirection={"column"} rowGap={4}>
        <EventTitleFilter />
        <CategoryFilter />
        <EventDateFilter />
      </Box>
    </Container>
  );
};
