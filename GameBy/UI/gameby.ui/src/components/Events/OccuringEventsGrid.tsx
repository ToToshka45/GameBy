import {
  Card,
  CardHeader,
  CardContent,
  CardMedia,
  Typography,
  Box,
  styled,
  Grid,
} from "@mui/material";
import { OccuringEventProps } from "../../interfaces/OccuringEvent";
import { blue, orange } from "@mui/material/colors";
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
              <CardContent sx={{ bgcolor: blue[100] }}>
                <Typography variant="body1">{event.content}</Typography>
              </CardContent>
              {/* <CardActions /> */}
            </Card>
          </Grid>
        ))
      )}
    </Grid>
  );
};
