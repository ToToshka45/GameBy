import { Box, Container, Typography } from "@mui/material";
import DisplayEventsGrid from "../components/Events/OccuringEventsGrid";
import { DisplayEvent } from "../interfaces/EventEntities";
import { EventCategory } from "../common/enums/EventEnums";
import dayjs from "dayjs";
import { useContext, useEffect, useState } from "react";
import FiltersPropsContext from "../contexts/FiltersPropsContext";
import FiltersProps from "../interfaces/FiltersProps";
// import { DATE_FORMAT } from "../common/consts/fakeData/testOccuringEvents";
import { axiosPrivate } from "../services/axios";
import axios from "axios";
import GetShortEventResponse from "../interfaces/Responses/GetEventResponse";
import EventStateDetails from "../common/consts/eventStateDetails";
import { useAuth } from "../contexts/AuthContext";
import AuthData from "../interfaces/AuthData";

const OccuringEventsPage = () => {
  const [events, setEvents] = useState<DisplayEvent[]>([]);
  // const [eventsFiltered, setEventsFiltered] = useState<OccuringEvent[]>(events);
  const { userAuth } = useAuth() as AuthData;
  const filterProps = useContext<FiltersProps | undefined>(FiltersPropsContext);

  const filteringTitle = filterProps!.filteringTitle;
  let filteringCategories: EventCategory[] = filterProps!.filteringCategories;
  const [afterDate, beforeDate] = filterProps!.filteringDates;

  useEffect(() => {
    const fetch = async () => {
      try {
        const eventFilters = {
          title: filterProps?.filteringTitle,
          afterDate: filterProps?.filteringDates[0],
          beforeDate: filterProps?.filteringDates[1],
          eventCategories: filterProps?.filteringCategories,
          userId: userAuth?.id,
        };
        const res = await axiosPrivate.post("events", eventFilters);
        if (res && res.data) {
          const fetchedEvents: GetShortEventResponse[] = res.data;
          const occuringEvents: DisplayEvent[] = fetchedEvents.map((ev) => {
            return {
              id: ev.id,
              title: ev.title,
              description: ev.description,
              eventAvatarUrl: ev.eventAvatarUrl,
              eventCategory: ev.eventCategory,
              eventDate: dayjs(ev.eventDate),
              stateDetails: EventStateDetails[ev.eventStatus],
            };
          });
          setEvents(occuringEvents);
        }
      } catch (err) {
        if (axios.isAxiosError(err)) {
          console.error("Fetching error: ", err);
        } else {
          console.error("Error has occured: ", err);
        }
      }
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
