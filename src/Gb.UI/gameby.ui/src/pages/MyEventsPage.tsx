import { Box, Card, CardMedia, Grid, Typography } from "@mui/material";
import { DisplayEvent } from "../interfaces/EventEntities";
import { useEffect, useState } from "react";
import { lightBlue } from "@mui/material/colors";
import { StyledCardHeader } from "../components/Events/OccuringEventsGrid";
import { EventCategory } from "../common/enums/EventEnums";
import { handleNavigateEvent } from "../common/functions";
import { useNavigate } from "react-router-dom";
import useEventProcessing from "../hooks/useEventProcessing";
import { DATE_FORMAT } from "../common/consts/fakeData/testOccuringEvents";
import AuthData from "../interfaces/AuthData";
import useAuth from "../hooks/useAuth";

export default function MyEventsPage() {
  const [gamerEvents, setGamerEvents] = useState<DisplayEvent[]>([]);
  const [organizerEvents, setOrganizerEvents] = useState<DisplayEvent[]>([]);
  const { fetchUserEvents } = useEventProcessing();
  const navigate = useNavigate();
  const { userAuth } = useAuth() as AuthData;

  // fetch events on the page load
  useEffect(() => {
    const fetch = async () => {
      const response = await fetchUserEvents();
      console.log(
        `Fetched events of the user '${userAuth?.username}' with id ${userAuth?.id}: `,
        response
      );
      if (response) {
        setGamerEvents(response.gamerEvents);
        setOrganizerEvents(response.organizerEvents);
      }
    };

    fetch();
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
          <Grid
            container
            gap={3}
            px={4}
            overflow="scroll"
            wrap="nowrap"
            height="70%"
          >
            {organizerEvents.map((event) => (
              <Grid item key={event.id}>
                <Box
                  sx={{ cursor: "pointer" }}
                  onClick={() => handleNavigateEvent(event, navigate)}
                >
                  <Card sx={{ height: 250, width: 300 }}>
                    <StyledCardHeader
                      title={event.title}
                      subheader={`${event.eventDate?.format(DATE_FORMAT)} - ${
                        EventCategory[event.eventCategory]
                      }`}
                    />
                    <CardMedia
                      component="img"
                      image={event.eventAvatarUrl}
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
          <Grid
            container
            gap={3}
            px={4}
            overflow="scroll"
            wrap="nowrap"
            height="70%"
          >
            {gamerEvents.map((event) => (
              <Grid item key={event.id}>
                <Box
                  sx={{ cursor: "pointer" }}
                  onClick={() => handleNavigateEvent(event, navigate)}
                >
                  <Card sx={{ height: 250, width: 300 }}>
                    <StyledCardHeader
                      title={event.title}
                      subheader={`${event.eventDate?.format(DATE_FORMAT)} - ${
                        EventCategory[event.eventCategory]
                      }`}
                    />
                    <CardMedia
                      component="img"
                      image={event.eventAvatarUrl}
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
