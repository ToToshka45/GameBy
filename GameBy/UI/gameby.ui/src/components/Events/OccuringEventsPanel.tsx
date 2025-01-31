import { Box, Container, Typography } from "@mui/material";
import { OccuringEventsGrid } from "./OccuringEventsGrid";
import { OccuringEvent } from "../../interfaces/OccuringEvent";
import { Category } from "../../enums/Category";
import dayjs from "dayjs";
import { useContext, useEffect, useState } from "react";
import FiltersPropsContext from "../../contexts/FiltersPropsContext";
import FiltersProps from "../../interfaces/FiltersProps";

const DATE_FORMAT = "DD/MM/YYYY";

const events: OccuringEvent[] = [
  {
    id: 1,
    avatar: "src/assets/event-pics/mafia-pic.webp",
    name: "Mafia 2025",
    content: "Basic description",
    category: Category.Mafia,
    date: dayjs().add(+7, "day").format(DATE_FORMAT),
  },
  {
    id: 2,
    avatar: "",
    name: "Event2",
    content: "Basic description",
    category: Category.Strategy,
    date: dayjs().add(+3, "day").format(DATE_FORMAT),
  },
  {
    id: 3,
    avatar: "",
    name: "Event3",
    content: "Basic description",
    category: Category.Sports,
    date: dayjs().add(+4, "day").format(DATE_FORMAT),
  },
  {
    id: 4,
    avatar: "",
    name: "Event4",
    content: "Basic description",
    category: Category.Strategy,
    date: dayjs().add(+11, "day").format(DATE_FORMAT),
  },
  {
    id: 5,
    avatar: "",
    name: "Event5",
    content: "Basic description",
    category: Category.Quiz,
    date: dayjs().add(+9, "day").format(DATE_FORMAT),
  },
  {
    id: 6,
    avatar: "",
    name: "Event6",
    content: "Basic description",
    category: Category.Sports,
    date: dayjs().add(+2, "day").format(DATE_FORMAT),
  },
];

export const OccuringEventsPanel = () => {
  const [eventsFiltered, setEventsFiltered] = useState<OccuringEvent[]>(events);
  const filterProps = useContext<FiltersProps | undefined>(FiltersPropsContext);

  const filteringTitle = filterProps!.filteringTitle;
  let filteringCategories: Category[] = filterProps!.filteringCategories;
  const [afterDate, beforeDate] = filterProps!.filteringDates;

  useEffect(() => {
    let filtered = events.filter((ev) => {
      const dayjsDate = dayjs(ev.date, DATE_FORMAT);
      return dayjsDate.isAfter(afterDate) && dayjsDate.isBefore(beforeDate);
    });

    if (filteringCategories && filteringCategories.length > 0) {
      filtered = filtered.filter((event) =>
        filteringCategories?.includes(event.category)
      );
    }

    if (filteringTitle) {
      filtered = filtered.filter((event) =>
        event.name.includes(filteringTitle)
      );
    }

    setEventsFiltered(filtered);
  }, [filteringCategories, afterDate, beforeDate, filteringTitle]);

  return (
    <Box marginTop={3} display="flex" flexDirection="column">
      <Container>
        <Typography variant="h4" color="secondary" gutterBottom>
          Upcoming events
        </Typography>
        <OccuringEventsGrid events={eventsFiltered} />
      </Container>
    </Box>
  );
};
