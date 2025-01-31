import { Box, Paper } from "@mui/material";
import { FiltersMenu } from "../components/FiltersMenu";
import { OccuringEventsPanel } from "../components/Events/OccuringEventsPanel";
import { useState } from "react";
import { Category } from "../enums/Category";
import dayjs, { Dayjs } from "dayjs";
import FiltersPropsContext from "../contexts/FiltersPropsContext";
import FiltersProps from "../interfaces/FiltersProps";
import { DateRange } from "@mui/x-date-pickers-pro";

export default function HomePage() {
  const [filteringTitle, setFilteringTitle] = useState<string>("");
  const [filteringCategories, setFilteringCategories] = useState<Category[]>(
    []
  );
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
          <OccuringEventsPanel />
        </FiltersPropsContext.Provider>
      </Paper>
    </Box>
  );
}
