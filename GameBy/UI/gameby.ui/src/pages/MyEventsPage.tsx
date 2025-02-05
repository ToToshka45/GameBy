import { Box, Card, CardMedia, Grid, Typography } from "@mui/material";
import { OccuringEvent } from "../interfaces/OccuringEvent";
import { useEffect, useState } from "react";
import axios from "axios";
import { events } from "../consts/testOccuringEvents";
import { lightBlue } from "@mui/material/colors";
import { StyledCardHeader } from "../components/Events/OccuringEventsGrid";
import { EventCategory } from "../enums/EventEnums";
import { handleNavigateEvent } from "../common/functions";
import { useNavigate } from "react-router-dom";

export default function MyEventsPage() {
  const [organizerEvents, setOrganizerEvents] = useState<OccuringEvent[]>([]);
  const [gamerEvents, setGamerEvents] = useState<OccuringEvent[]>([]);

  const navigate = useNavigate();

  // fetch events on the page load
  useEffect(() => {
    try {
      // TODO: fetch from the server
      // await fetchByUserId();
      setGamerEvents(events);
      setOrganizerEvents(events);
    } catch (e) {
      console.log("Error occured while fetching user events:", e);
    }

    // const fetchByUserId = async () => {
    //   const response = await axios.get("");

    //   const {
    //     data: { fetchedGamerEvents, fetchedOrganizerEvents },
    //   } = response;

    //   setGamerEvents(fetchedGamerEvents);
    //   setOrganizerEvents(fetchedOrganizerEvents);
    // };
  }, []);

  return (
    <Box height="90vh" mb={2}>
      {organizerEvents.length > 0 ? (
        <Box height="50%">
          <Typography
            p={2}
            bgcolor={lightBlue[50]}
            fontSize={{ md: 18 }}
            gutterBottom
          >
            YOU ORGANIZED
          </Typography>
          <Grid container gap={3} px={4} overflow="scroll" wrap="nowrap">
            {gamerEvents.map((event) => (
              <Grid item key={event.id}>
                <Box
                  sx={{ cursor: "pointer" }}
                  onClick={() => handleNavigateEvent(event, navigate)}
                >
                  <Card sx={{ height: 200, width: 200 }}>
                    <StyledCardHeader
                      title={event.title}
                      subheader={`${event.date} - ${
                        EventCategory[event.category]
                      }`}
                    />
                    <CardMedia
                      component="img"
                      image={event.avatar}
                      src={"src/assets/event-pics/event_default.jpg"}
                    />
                    {/* <CardContent></CardContent> */}
                  </Card>
                </Box>
              </Grid>
            ))}
          </Grid>
        </Box>
      ) : (
        <Typography variant="h4" textAlign="center" mt={3}>
          No Event organized by you at the moment
        </Typography>
      )}

      {gamerEvents.length > 0 ? (
        <Box height="50%">
          <Typography
            p={2}
            bgcolor={lightBlue[50]}
            fontSize={{ md: 18 }}
            gutterBottom
          >
            YOU ARE PARTICIPATING
          </Typography>
          <Grid container gap={3} px={4} overflow="scroll" wrap="nowrap">
            {gamerEvents.map((event) => (
              <Grid item>
                <Box
                  sx={{ cursor: "pointer" }}
                  onClick={() => handleNavigateEvent(event, navigate)}
                >
                  <Card sx={{ height: 200, width: 200 }}>
                    <StyledCardHeader
                      title={event.title}
                      subheader={`${event.date} - ${
                        EventCategory[event.category]
                      }`}
                    />
                    <CardMedia
                      component="img"
                      image={event.avatar}
                      src={"src/assets/event-pics/event_default.jpg"}
                    />
                    {/* <CardContent></CardContent> */}
                  </Card>
                </Box>
              </Grid>
            ))}
          </Grid>
        </Box>
      ) : (
        <Typography variant="h4" textAlign="center" mt={3}>
          You are not participating in any Events at the moment
        </Typography>
      )}
    </Box>
  );
}
