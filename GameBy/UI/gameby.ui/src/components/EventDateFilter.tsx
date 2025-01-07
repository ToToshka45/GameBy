import { Box } from "@mui/material";
import { useState } from "react";
import dayjs, { Dayjs } from "dayjs";
import { LocalizationProvider, DateTimePicker } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";

export const EventDateFilter = () => {
  const [eventDate, setEventDate] = useState(dayjs());

  const setDate = (date: Dayjs | null) => {
    if (date) setEventDate(date);
  };

  return (
    <Box
      display={"flex"}
      justifyContent={"center"}
      // marginLeft={3}
    >
      <LocalizationProvider dateAdapter={AdapterDayjs}>
        <DateTimePicker
          label="EventDate"
          value={eventDate}
          onChange={(date) => setDate(date)}
          sx={{ maxWidth: { xs: "50%", sm: "30%" }, maxHeight: "80%" }}
          // renderLoading={(params) => <TextField {...params} />}
        />
      </LocalizationProvider>
    </Box>
  );
};
