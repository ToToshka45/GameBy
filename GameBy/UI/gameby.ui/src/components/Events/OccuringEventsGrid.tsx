import {
  Grid2,
  Card,
  CardHeader,
  CardContent,
  CardMedia,
  CardActions,
  Typography,
  Box,
  styled,
} from "@mui/material";
import { OccuringEventProps } from "../../interfaces/OccuringEvent";
import { orange } from "@mui/material/colors";
import { Category } from "../../enums/Category";

const StyledCardHeader = styled(CardHeader)(({ theme }) => ({
  backgroundColor: orange[200],
  "& .MuiCardHeader-title": {
    fontSize: "1.2rem",
  },
  "& .MuiCardHeader-subheader": {
    fontSize: "1rem",
    color: theme.palette.text.secondary,
  },
}));

export const OccuringEventsGrid = ({ events }: OccuringEventProps) => {
  return (
    <Grid2 container spacing={2}>
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
          <Grid2 key={event.id} size={{ md: 6 }}>
            <Card>
              <StyledCardHeader
                title={event.name}
                subheader={`${event.date} - ${Category[event.category]}`}
              />
              <CardMedia
                component="img"
                image={event.avatar}
                src={"src/assets/event-pics/event_default.jpg"}
              />
              <CardContent />
              <CardActions />
            </Card>
          </Grid2>
        ))
      )}
    </Grid2>
  );
};
