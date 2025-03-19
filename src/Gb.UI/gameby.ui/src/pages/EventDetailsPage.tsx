import {
  Box,
  Button,
  CardMedia,
  Chip,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Grid,
  IconButton,
  List,
  ListItem,
  ListItemIcon,
  Paper,
  Typography,
} from "@mui/material";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { OccuringEvent } from "../interfaces/EventEntities";
import { useEffect, useState } from "react";
import { DATE_FORMAT } from "../common/consts/fakeData/testOccuringEvents";
import { loremIpsum } from "../common/consts/fakeData/defaults";
import AuthData from "../interfaces/AuthData";
import useEventProcessing from "../hooks/useFetchEvent";
import { ParticipationState } from "../common/enums/EventEnums";
import Participant from "../interfaces/EventParticipant";
import { ThumbUp, ThumbDown, Person, ArrowLeft } from "@mui/icons-material";
import { blue, green, red } from "@mui/material/colors";
import useAuth from "../hooks/useAuth";

export default function EventDetailsPage() {
  // this allows us to get a payload, sent alongside when we were redirected to the EventPage
  const { eventId } = useParams();

  const location = useLocation();
  const navigate = useNavigate();
  const { userAuth } = useAuth() as AuthData;
  const [occuringEvent, setOccuringEvent] = useState<
    OccuringEvent | undefined
  >();
  const [acceptedParticipants, setAcceptedParticipants] = useState<
    Participant[]
  >([]);
  const [pendingParticipants, setPendingParticipants] = useState<Participant[]>(
    []
  );
  const { fetchEvent, sendParticipantState, sendParticipationRequest } =
    useEventProcessing();
  const [isLoading, setIsLoading] = useState(false);
  const [openDialog, setOpenDialog] = useState(false);
  const [processingParticipant, setProcessingParticipant] = useState<
    any | undefined
  >();

  useEffect(() => {
    fetch();
  }, []);

  const fetch = async () => {
    try {
      if (!eventId) throw new Error("Event is not found");
      setIsLoading(true);

      const event = await fetchEvent(Number.parseInt(eventId));
      console.log("Received an occuring event: ", event);
      if (!event) throw new Error("Event is not found");
      setOccuringEvent(event);
    } catch (err) {
      console.error(
        "Error has occured while fetching the event details: ",
        err
      );
    }
  };

  useEffect(() => {
    console.log("Participants acquired: ", occuringEvent?.participants);
    const acceptedParticipants =
      occuringEvent?.participants.filter(
        (p: Participant) =>
          p.state.toString() === ParticipationState.Accepted.toString()
      ) || [];
    setAcceptedParticipants(acceptedParticipants);

    const pendingParticipants =
      occuringEvent?.participants.filter(
        (p: Participant) =>
          p.state.toString() === ParticipationState.PendingAcceptance.toString()
      ) || [];
    setPendingParticipants(pendingParticipants);

    setIsLoading(false);
  }, [occuringEvent]);

  const handleEventApply = async () => {
    try {
      await sendParticipationRequest(
        userAuth?.id!,
        userAuth?.username!,
        userAuth?.email!,
        Number.parseInt(eventId!)
      );
    } catch (err) {
      console.error(err);
    } finally {
      fetch();
    }
  };

  const handleLoginNavigate = () => {
    navigate("/sign-in", { state: { from: location, replace: true } });
  };

  const handleParticipantRequest = (
    participantId: number,
    state: ParticipationState
  ) => {
    setProcessingParticipant({ participantId, state });
    setOpenDialog(true);
  };

  const handleProcessingParticipant = async () => {
    try {
      console.log("Processing participant: ", processingParticipant);
      await sendParticipantState(
        processingParticipant.participantId!,
        processingParticipant.state,
        Number.parseInt(eventId!)
      );
      fetch();
    } catch (err) {
      console.error(
        err,
        "Error has occured while sending a participant acceptance request"
      );
    } finally {
      handleCloseModal();
    }
  };

  const handleCloseModal = () => {
    setProcessingParticipant(undefined);
    setOpenDialog(false);
  };

  const renderState = () => {
    const state = occuringEvent?.participants.find(
      (p) => p.userId === userAuth?.id
    )?.state;
    return ParticipationState[state as keyof typeof ParticipationState];
  };

  return (
    <Box mt={3}>
      {isLoading ? (
        <Typography>Loading event...</Typography>
      ) : (
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
                flexDirection="column"
                justifyItems="center"
                alignItems="center"
                // bgcolor={occuringEvent?.stateDetails.color}
              >
                <Typography variant="h6" px={5}>
                  {occuringEvent?.title}
                </Typography>
                <Typography
                  variant="body2"
                  px={2}
                  py={3}
                  borderTop="1.5px solid darkblue"
                >
                  Адрес: {occuringEvent?.location}
                </Typography>
              </Box>
              <Typography variant="caption" fontSize={{ md: 14 }}>
                <b>Start date:</b>{" "}
                {occuringEvent?.eventDate.format(DATE_FORMAT)}
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
              {occuringEvent?.description ?? loremIpsum}
            </Typography>
          </Grid>
          <Grid item xs={6}>
            <CardMedia
              component="img"
              image={"../" + occuringEvent?.eventAvatarUrl}
              // src={"src/assets/event-pics/event_default.jpg"}
              height="100%"
            />
          </Grid>

          <Grid item xs={6} textAlign="end" pr={5}>
            {!userAuth ? (
              <Button
                onClick={handleLoginNavigate}
                variant="outlined"
                size="small"
                color="warning"
                sx={{ width: { md: 180 } }}
              >
                Log In to Apply
              </Button>
            ) : occuringEvent?.isParticipant ? (
              <Box
                position="relative"
                width="auto"
                display="flex"
                flexDirection="row"
                border="1.5px solid gray"
              >
                <Typography
                  sx={{
                    textAlign: "start",
                    pl: 4,
                    py: 1,
                    width: "50%",
                  }}
                >
                  You are participating
                </Typography>{" "}
                <Typography
                  sx={{
                    textAlign: "start",
                    pl: 4,
                    py: 1,
                    width: "50%",
                  }}
                >
                  State: {renderState()}
                </Typography>
              </Box>
            ) : occuringEvent?.isOrganizer ? (
              <Box ml={1}>
                <Paper elevation={2}>
                  <Typography variant="body2" textAlign="center" mt={2} ml={1}>
                    <b>Pending participants requests</b>
                  </Typography>
                </Paper>
                <Paper elevation={1} sx={{ height: "40vh" }}>
                  <Box>
                    {!pendingParticipants ||
                      (pendingParticipants.length > 0 && (
                        <List>
                          {pendingParticipants?.map((participant, idx) => (
                            <ListItem key={idx}>
                              <IconButton
                                sx={{ color: green[500] }}
                                onClick={() =>
                                  handleParticipantRequest(
                                    participant.id,
                                    ParticipationState.Accepted
                                  )
                                }
                              >
                                <ThumbUp />
                              </IconButton>
                              <IconButton
                                sx={{ color: red[500] }}
                                onClick={() =>
                                  handleParticipantRequest(
                                    participant.id,
                                    ParticipationState.Declined
                                  )
                                }
                              >
                                <ThumbDown />
                              </IconButton>
                              <Typography variant="body2" ml={1}>
                                {participant.username}
                              </Typography>
                            </ListItem>
                          ))}
                        </List>
                      ))}
                  </Box>
                </Paper>
              </Box>
            ) : (
              <Button
                onClick={handleEventApply}
                variant="outlined"
                size="medium"
                color="primary"
                sx={{ width: { md: 220 } }}
              >
                Apply
              </Button>
            )}
          </Grid>
          <Grid item xs={6} px={1}>
            <Box mt={2}>
              <Paper elevation={1} sx={{ bgcolor: "lightcyan" }}>
                <Typography variant="body2" textAlign="center">
                  <b>Participants ({acceptedParticipants.length})</b>
                </Typography>
              </Paper>
              <Paper elevation={1} sx={{ height: "40vh" }}>
                {acceptedParticipants.length > 0 && (
                  <List>
                    {acceptedParticipants.map((participant, index) => (
                      <ListItem key={index}>
                        {occuringEvent?.isOrganizer && (
                          <IconButton
                            onClick={() =>
                              handleParticipantRequest(
                                participant.id,
                                ParticipationState.PendingAcceptance
                              )
                            }
                          >
                            <ArrowLeft />
                          </IconButton>
                        )}
                        <ListItemIcon>
                          <Person fontSize="medium" sx={{ color: blue[500] }} />
                        </ListItemIcon>
                        <Typography variant="body2">
                          {participant.username}
                        </Typography>
                      </ListItem>
                    ))}
                  </List>
                )}
              </Paper>
            </Box>
          </Grid>
        </Grid>
      )}

      <Dialog open={openDialog}>
        <DialogTitle>Confirmation</DialogTitle>
        <DialogContent>Are you sure you want to proceed?</DialogContent>
        <DialogActions>
          <Button onClick={handleProcessingParticipant} color="primary">
            Yes
          </Button>
          <Button onClick={handleCloseModal} color="secondary">
            No
          </Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
}
