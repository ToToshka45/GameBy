import { Box, Button, CardMedia, Chip, Grid, Typography } from "@mui/material";
import { loremIpsum } from "../consts/defaults";
import { useLocation } from "react-router-dom";
import { OccuringEvent } from "../interfaces/OccuringEvent";
import { useEffect } from "react";
import { green } from "@mui/material/colors";
import { DATE_FORMAT } from "../consts/testOccuringEvents";

export default function EventPage() {
  const location = useLocation();
  const { eventDetails } =
    (location.state as { eventDetails: OccuringEvent }) || {};

  useEffect(() => {
    console.log(eventDetails);
  }, []);

  return (
    <Grid container direction={{ xs: "column", md: "row" }} height="150px">
      <Grid item xs={6}>
        <Box display="flex" gap={4} alignItems="center" height="60px">
          <Box
            height="100%"
            display="flex"
            justifyItems="center"
            alignItems="center"
            bgcolor={eventDetails.stateDetails.color}
          >
            <Typography
              variant="h5"
              bgcolor="inherit"
              sx={{ color: "black" }}
              px={5}
            >
              {eventDetails.title}
            </Typography>
          </Box>
          <Typography variant="caption" fontSize={{ md: 14 }}>
            <b>Start date:</b> {eventDetails.date.format(DATE_FORMAT)}
          </Typography>
          <Chip
            label={eventDetails.stateDetails.state}
            sx={{
              fontSize: { md: 14 },
              bgcolor: eventDetails.stateDetails.color,
            }}
          />
        </Box>
        <Typography
          variant="body2"
          px={1}
          py={3}
          borderTop="3px solid darkblue"
          borderBottom="3px solid darkblue"
          // bgcolor="lightyellow"
        >
          {eventDetails.content ?? loremIpsum}
        </Typography>
      </Grid>
      <Grid item xs={6}>
        <CardMedia
          component="img"
          image={"../" + eventDetails.avatar}
          // src={"src/assets/event-pics/event_default.jpg"}
          height="100%"
        />
      </Grid>
      <Grid item xs={6}>
        <Typography>Action Buttons</Typography>
      </Grid>
      <Grid item xs={6}>
        <Button
          variant="contained"
          size="large"
          sx={{ width: { md: 150 }, bgcolor: green[300] }}
        >
          Apply
        </Button>
      </Grid>
    </Grid>
  );
}
