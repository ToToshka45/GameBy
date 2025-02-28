import {
  Box,
  Button,
  CardMedia,
  Chip,
  Grid,
  List,
  ListItem,
  ListItemIcon,
  Paper,
  Typography,
} from "@mui/material";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { OccuringEvent } from "../interfaces/OccuringEvent";
import { useEffect, useState } from "react";
import { DATE_FORMAT } from "../common/consts/fakeData/testOccuringEvents";
import { loremIpsum } from "../common/consts/fakeData/defaults";
import { useAuth } from "../contexts/AuthContext";
import AuthData from "../types/AuthData";
import useFetchEvent from "../hooks/useFetchEvent";
import EventParticipant from "../interfaces/EventParticipant";
import { ParticipationState } from "../common/enums/EventEnums";

export default function EventDetailsPage() {
  // this allows us to get a payload, sent alongside when we were redirected to the EventPage
  const { eventId } = useParams();

  const location = useLocation();
  const navigate = useNavigate();
  const { userAuth } = useAuth() as AuthData;
  const [occuringEvent, setOccuringEvent] = useState<
    OccuringEvent | undefined
  >();
  const fetchEvent = useFetchEvent();
  const [pendingParticipants, setPendingParticipants] = useState<
    EventParticipant[]
  >([]);

  useEffect(() => {
    // TODO: how to process such case, when there is not eventId somehow?
    if (!eventId) return;

    const event = fetchEvent(Number.parseInt(eventId));
    setOccuringEvent(event);

    const pending =
      occuringEvent?.participants.filter(
        (p) => p.participationState === ParticipationState.PendingAcceptance
      ) || [];
    setPendingParticipants(pending);

    // retrieve data about the event
  }, []);

  const handleEventApply = () => {
    console.log(
      "User with Id ",
      `\"${userAuth!.id}\"`,
      " applied to the event"
    );
  };

  const handleLoginNavigate = () => {
    navigate("/sign-in", { state: { from: location, replace: true } });
  };

  return (
    <Grid container direction={{ xs: "column", md: "row" }} height="90vh">
      <Grid item xs={6}>
        <Box
          display="flex"
          gap={3}
          alignItems="center"
          justifyContent="space-evenly"
          height="70px"
        >
          <Box
            display="flex"
            justifyItems="center"
            alignItems="center"
            // bgcolor={occuringEvent?.stateDetails.color}
          >
            <Typography variant="h6" px={5}>
              {occuringEvent?.title}
            </Typography>
          </Box>
          <Typography variant="caption" fontSize={{ md: 14 }}>
            <b>Start date:</b> {occuringEvent?.date.format(DATE_FORMAT)}
          </Typography>
          <Chip
            label={occuringEvent?.stateDetails.state}
            sx={{
              fontSize: { md: 13 },
              bgcolor: occuringEvent?.stateDetails.color,
            }}
          />
        </Box>
        <Typography
          variant="body2"
          px={2}
          py={3}
          borderTop="1.5px solid darkblue"
        >
          {occuringEvent?.content ?? loremIpsum}
        </Typography>
      </Grid>
      <Grid item xs={6}>
        <CardMedia
          component="img"
          image={"../" + occuringEvent?.avatar}
          // src={"src/assets/event-pics/event_default.jpg"}
          height="100%"
        />
      </Grid>

      <Grid item xs={6} textAlign="end" pr={5}>
        {userAuth ? (
          userAuth.roles.includes("gamer") && (
            <Button
              onSubmit={handleEventApply}
              variant="outlined"
              size="medium"
              color="primary"
              sx={{ width: { md: 220 } }}
            >
              Apply
            </Button>
          )
        ) : (
          <Button
            onClick={handleLoginNavigate}
            variant="outlined"
            size="small"
            color="warning"
            sx={{ width: { md: 180 } }}
          >
            Log In to Apply
          </Button>
        )}

        {userAuth?.roles.includes("admin") && (
          <Box ml={1}>
            <Paper elevation={2}>
              <Typography variant="body2" textAlign="center" mt={2} ml={1}>
                <b>Pending participant requests</b>
              </Typography>
            </Paper>
            <Paper elevation={1} sx={{ height: "40vh" }}>
              {occuringEvent?.participants?.length === 0 ? (
                <Typography variant="body2">No participants yet</Typography>
              ) : (
                <List>
                  {occuringEvent?.participants!.map((participant, index) => (
                    <ListItem key={index}>
                      <ListItemIcon></ListItemIcon>
                      {participant.userName}
                    </ListItem>
                  ))}
                </List>
              )}
            </Paper>
          </Box>
        )}
      </Grid>
      <Grid item xs={6} px={1}>
        <Box mt={2}>
          <Paper elevation={1} sx={{ bgcolor: "lightcyan" }}>
            <Typography variant="body2" textAlign="center">
              <b>Participants</b>
            </Typography>
          </Paper>
          <Paper elevation={1} sx={{ height: "40vh" }}>
            {occuringEvent?.participants?.length === 0 ? (
              <Typography variant="body2">No participants yet</Typography>
            ) : (
              <List>
                {occuringEvent?.participants!.map((participant, index) => (
                  <ListItem key={index}>
                    <ListItemIcon></ListItemIcon>
                    {participant.userName}
                  </ListItem>
                ))}
              </List>
            )}
          </Paper>
        </Box>
      </Grid>
    </Grid>
  );
}
