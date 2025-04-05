import { Box, Paper } from "@mui/material";
import { FiltersMenu } from "../components/FiltersMenu";
import OccuringEventsPage from "./OccuringEventsPage";
import { useState } from "react";
import { EventCategory } from "../common/enums/EventEnums";
import dayjs, { Dayjs } from "dayjs";
import FiltersPropsContext from "../contexts/FiltersPropsContext";
import FiltersProps from "../interfaces/FiltersProps";
import { DateRange } from "@mui/x-date-pickers-pro";

export default function HomePage() {
  const [filteringTitle, setFilteringTitle] = useState<string>("");
  const [filteringCategories, setFilteringCategories] = useState<
    EventCategory[]
  >([]);
  const [filteringDates, setFilteringDates] = useState<DateRange<Dayjs>>([
    dayjs(),
    dayjs().add(+3, "day"),
  ]);

  const contextValue: FiltersProps = {
    filteringTitle,
    setFilteringTitle,
    filteringCategories,
    setFilteringCategories,
    filteringDates,
    setFilteringDates,
  };

  return (
    <Box
      paddingX={{ xs: "4%", md: "3%" }}
      paddingBottom={{ xs: "2%", md: "1%" }}
    >
      <Paper
        elevation={4}
        sx={{
          padding: { xs: "4%", md: "2%" },
          paddingBottom: { xs: "4%", md: "2%" },
        }}
      >
        <FiltersPropsContext.Provider value={contextValue}>
          <FiltersMenu />
          <OccuringEventsPage />
        </FiltersPropsContext.Provider>
      </Paper>
    </Box>
  );
}
