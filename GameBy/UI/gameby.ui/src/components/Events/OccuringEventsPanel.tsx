import { Box, Container, Typography } from "@mui/material";
import OccuringEventsGrid from "./OccuringEventsGrid";
import { OccuringEvent } from "../../interfaces/OccuringEvent";
import { EventCategory } from "../../enums/EventEnums";
import dayjs from "dayjs";
import { useContext, useEffect, useState } from "react";
import FiltersPropsContext from "../../contexts/FiltersPropsContext";
import FiltersProps from "../../types/FiltersProps";
import { DATE_FORMAT, events } from "../../common/consts/testOccuringEvents";

export const OccuringEventsPanel = () => {
  const [eventsFiltered, setEventsFiltered] = useState<OccuringEvent[]>(events);
  const filterProps = useContext<FiltersProps | undefined>(FiltersPropsContext);

  const filteringTitle = filterProps!.filteringTitle;
  let filteringCategories: EventCategory[] = filterProps!.filteringCategories;
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
        event.title.includes(filteringTitle)
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
