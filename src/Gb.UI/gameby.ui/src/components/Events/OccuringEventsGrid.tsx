import {
  Card,
  CardHeader,
  CardMedia,
  Typography,
  Box,
  styled,
  Grid,
  Chip,
} from "@mui/material";
import {
  DisplayEventProps,
  DisplayEvent,
} from "../../interfaces/EventEntities";
import { orange } from "@mui/material/colors";
import { EventCategory } from "../../common/enums/EventEnums";
import { useNavigate } from "react-router-dom";
import { DATE_FORMAT } from "../../common/consts/fakeData/testOccuringEvents";
import { handleNavigateEvent } from "../../common/functions";
import { ReactNode } from "react";

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

const renderTitle = (event: DisplayEvent): ReactNode => {
  return (
    <Box display="flex" flexDirection="row" gap={0.5}>
      <Typography>{event.title}</Typography>
      <Typography variant="caption">
        ({EventCategory[event.eventCategory]})
      </Typography>
    </Box>
  );
};

const DisplayEventsGrid = ({ events }: DisplayEventProps) => {
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
                  title={renderTitle(event)}
                  subheader={`Starting on: ${event.eventDate.format(DATE_FORMAT)}`}
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
              </Card>
            </Box>
          </Grid>
        ))
      )}
    </Grid>
  );
};

export default DisplayEventsGrid;
