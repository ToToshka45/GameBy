import { Box, Container, Typography } from "@mui/material";
import { OccuringEventsGrid } from "./OccuringEventsGrid";
import { OccuringEvent } from "../../interfaces/OccuringEvent";
import { Category } from "../../enums/Category";
import dayjs from "dayjs";
import { useContext, useEffect, useState } from "react";
import CategoryFilterContext from "../../contexts/CategoryFilterContext";

const DATE_FORMAT = "DD/MM/YYYY";

const events: OccuringEvent[] = [
  {
    id: 1,
    avatar: "src/assets/event-pics/mafia-pic.webp",
    name: "Mafia 2025",
    content: "Basic description",
    category: Category.Mafia,
    date: dayjs().format(DATE_FORMAT),
  },
  {
    id: 2,
    avatar: "",
    name: "Event2",
    content: "Basic description",
    category: Category.Strategy,
    date: dayjs().add(-3, "day").format(DATE_FORMAT),
  },
  {
    id: 3,
    avatar: "",
    name: "Event3",
    content: "Basic description",
    category: Category.Sports,
    date: dayjs().format(DATE_FORMAT),
  },
  {
    id: 4,
    avatar: "",
    name: "Event4",
    content: "Basic description",
    category: Category.Strategy,
    date: dayjs().format(DATE_FORMAT),
  },
  {
    id: 5,
    avatar: "",
    name: "Event5",
    content: "Basic description",
    category: Category.Quiz,
    date: dayjs().format(DATE_FORMAT),
  },
  {
    id: 6,
    avatar: "",
    name: "Event6",
    content: "Basic description",
    category: Category.Sports,
    date: dayjs().format(DATE_FORMAT),
  },
];

export const OccuringEventsPanel = () => {
  const [eventsFiltered, setEventsFiltered] = useState<OccuringEvent[]>(events);
  const filterByCategories = useContext<Category[]>(CategoryFilterContext);

  useEffect(() => {
    if (filterByCategories && filterByCategories.length > 0) {
      const filtered = events.filter((event) =>
        filterByCategories?.includes(event.category)
      );
      setEventsFiltered(filtered);
    } else {
      setEventsFiltered(events);
    }
  }, [filterByCategories]);

  if (filterByCategories) console.log("From Panel: " + [...filterByCategories]);

  return (
    <Container>
      <Box marginTop={3} display="flex" flexDirection="column">
        <Typography variant="h5" color="secondary" gutterBottom>
          Upcoming events
        </Typography>
        <OccuringEventsGrid events={eventsFiltered} />
      </Box>
    </Container>
  );
};
