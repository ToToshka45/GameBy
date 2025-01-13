import { Box, Typography } from "@mui/material";
import { OccuringEventsGrid } from "./OccuringEventsGrid";
import { OccuringEvent } from "../interfaces/OccuringEvent";
import dayjs from "dayjs";

export const OccuringEventsPanel = () => {
  const DATE_FORMAT = "DD/MM/YYYY";

  const events: OccuringEvent[] = [
    {
      id: 1,
      avatar: "src/assets/event-pics/mafia-pic.webp",
      name: "Mafia 2025",
      content: "Basic description",
      date: dayjs().format(DATE_FORMAT),
    },
    {
      id: 2,
      avatar: "",
      name: "Event2",
      content: "Basic description",
      date: dayjs().format(DATE_FORMAT),
    },
    {
      id: 3,
      avatar: "",
      name: "Event3",
      content: "Basic description",
      date: dayjs().format(DATE_FORMAT),
    },
    {
      id: 4,
      avatar: "",
      name: "Event4",
      content: "Basic description",
      date: dayjs().format(DATE_FORMAT),
    },
    {
      id: 5,
      avatar: "",
      name: "Event5",
      content: "Basic description",
      date: dayjs().format(DATE_FORMAT),
    },
    {
      id: 6,
      avatar: "",
      name: "Event6",
      content: "Basic description",
      date: dayjs().format(DATE_FORMAT),
    },
  ];

  return (
    <Box marginTop={3} display="flex" flexDirection="column">
      <Typography variant="h3" color="secondary" gutterBottom>
        Upcoming events
      </Typography>
      <OccuringEventsGrid events={events} />
    </Box>
  );
};
