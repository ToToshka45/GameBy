import { Box } from "@mui/material";
import { useContext } from "react";
import { Dayjs } from "dayjs";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { DateRangePicker } from "@mui/x-date-pickers-pro/DateRangePicker";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import FiltersProps from "../../types/FiltersProps";
import FiltersPropsContext from "../../contexts/FiltersPropsContext";
import { DateRange } from "@mui/x-date-pickers-pro";

export const EventDateFilter = () => {
  const filtersProps = useContext<FiltersProps | undefined>(
    FiltersPropsContext
  );

  const dateRange = filtersProps!.filteringDates;
  const setFilteringDates = filtersProps!.setFilteringDates!;

  const handleDates = (dateRange: DateRange<Dayjs>) => {
    setFilteringDates(dateRange);
  };

  return (
    <Box display={"flex"} justifyContent="flex-end" gap={2}>
      <LocalizationProvider dateAdapter={AdapterDayjs}>
        {/* <DateTimePicker
          label="After"
          value={afterDate}
          onChange={(date) => handleAfterDate(date)}
          sx={{
            maxWidth: { xs: "50%", sm: "30%" },
            maxHeight: "80%",
          }}
          // renderLoading={(params) => <TextField {...params} />}
        />
        <DateTimePicker
          label="Before"
          value={beforeDate}
          onChange={(date) => handleBeforeDate(date)}
          sx={{
            maxWidth: { xs: "50%", sm: "30%" },
            maxHeight: "80%",
          }}
          // renderLoading={(params) => <TextField {...params} />}
        /> */}
        <DateRangePicker
          localeText={{ start: "After", end: "Before" }}
          value={dateRange}
          onChange={(dates) => handleDates(dates)}
        />
      </LocalizationProvider>
    </Box>
  );
};
