import { Box, Container, Typography } from "@mui/material";
import DisplayEventsGrid from "../components/Events/OccuringEventsGrid";
import { DisplayEvent } from "../interfaces/EventEntities";
import { EventCategory } from "../common/enums/EventEnums";
import { useContext, useMemo, useState } from "react";
import FiltersPropsContext from "../contexts/FiltersPropsContext";
import FiltersProps from "../interfaces/FiltersProps";
// import { DATE_FORMAT } from "../common/consts/fakeData/testOccuringEvents";
import AuthData from "../interfaces/AuthData";
import useAuth from "../hooks/useAuth";
import useEventProcessing from "../hooks/useEventProcessing";

const OccuringEventsPage = () => {
  const [events, setEvents] = useState<DisplayEvent[]>([]);
  // const [eventsFiltered, setEventsFiltered] = useState<OccuringEvent[]>(events);
  const { userAuth } = useAuth() as AuthData;
  const filterProps = useContext<FiltersProps | undefined>(FiltersPropsContext);

  const filteringTitle = filterProps!.filteringTitle;
  let filteringCategories: EventCategory[] = filterProps!.filteringCategories;
  const [afterDate, beforeDate] = filterProps!.filteringDates;
  const { fetchEvents } = useEventProcessing();

  useMemo(() => {
    const fetch = async () => {
      const eventFilters = {
        title: filterProps?.filteringTitle,
        afterDate: filterProps?.filteringDates[0],
        beforeDate: filterProps?.filteringDates[1],
        eventCategories: filterProps?.filteringCategories,
        userId: userAuth?.id,
      };
      const occuringEvents = await fetchEvents(eventFilters);
      setEvents(occuringEvents);
    };

    fetch();
  }, [filteringCategories, afterDate, beforeDate, filteringTitle]);

  // useMemo(() => {
  //   let filtered = events.filter((ev: OccuringEvent) => {
  //     const dayjsDate = dayjs(ev.eventDate, DATE_FORMAT);
  //     console.log("Date: ", dayjsDate);
  //     return dayjsDate.isAfter(afterDate) && dayjsDate.isBefore(beforeDate);
  //   });
  //   if (filteringCategories && filteringCategories.length > 0) {
  //     filtered = filtered.filter((event) =>
  //       filteringCategories?.includes(event.eventCategory)
  //     );
  //   }

  //   if (filteringTitle) {
  //     filtered = filtered.filter((event) =>
  //       event.title.includes(filteringTitle)
  //     );
  //   }

  //   setEventsFiltered(filtered);
  //   console.log("Events filtered: ", eventsFiltered);
  // }, [events, filteringCategories, afterDate, beforeDate, filteringTitle]);

  return (
    <Box marginTop={3} display="flex" flexDirection="column">
      <Container>
        <Typography variant="h4" color="secondary" gutterBottom>
          Upcoming events
        </Typography>
        <DisplayEventsGrid events={events} />
      </Container>
    </Box>
  );
};

export default OccuringEventsPage;
