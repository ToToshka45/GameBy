import {
  Card,
  CardHeader,
  CardContent,
  CardMedia,
  Typography,
  Box,
  styled,
  Grid,
  Chip,
} from "@mui/material";
import { OccuringEventProps } from "../../interfaces/OccuringEvent";
import { blue, orange } from "@mui/material/colors";
import { EventCategory } from "../../common/enums/EventEnums";
import { useNavigate } from "react-router-dom";
import { DATE_FORMAT } from "../../common/consts/fakeData/testOccuringEvents";
import { handleNavigateEvent } from "../../common/functions";

export const StyledCardHeader = styled(CardHeader)(({ theme }) => ({
  backgroundColor: orange[200],
  height: "25px",
  "& .MuiCardHeader-title": {
    fontSize: "1.1rem",
  },
  "& .MuiCardHeader-subheader": {
    fontSize: "0.8rem",
    color: theme.palette.text.secondary,
  },
}));

const OccuringEventsGrid = ({ events }: OccuringEventProps) => {
  const navigate = useNavigate();

  return (
    <Grid container spacing={2}>
      {events.length === 0 ? (
        <Box
          display={"flex"}
          justifyContent={"center"}
          flexGrow={1}
          marginTop={3}
        >
          <Typography variant="h5" align="center" color="textSecondary">
            NO EVENTS ARE AVAILABLE AT THE MOMENT
          </Typography>
        </Box>
      ) : (
        events.map((event) => (
          <Grid item key={event.id} md={6} lg={4}>
            <Box
              sx={{ cursor: "pointer" }}
              onClick={() => handleNavigateEvent(event, navigate)}
            >
              <Card>
                <StyledCardHeader
                  title={event.title}
                  subheader={`${event.eventDate.format(DATE_FORMAT)} - ${
                    EventCategory[event.eventCategory]
                  }`}
                  action={
                    <Chip
                      size="medium"
                      label={event.stateDetails.state}
                      sx={{ bgcolor: event.stateDetails.color }}
                    />
                  }
                />
                <CardMedia
                  component="img"
                  image={event.eventAvatarUrl}
                  src={"src/assets/event-pics/event_default.jpg"}
                />
                {/* <CardContent sx={{ height: "100%", bgcolor: blue[100] }}>
                  <Typography variant="caption">{event.description}</Typography>
                </CardContent> */}
                {/* <CardActions /> */}
              </Card>
            </Box>
          </Grid>
        ))
      )}
    </Grid>
  );
};

export default OccuringEventsGrid;
