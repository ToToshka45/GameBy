import {
  Grid2,
  Card,
  CardHeader,
  CardContent,
  CardMedia,
  CardActions,
  Container,
  Typography,
  Box,
  styled,
} from "@mui/material";
import { OccuringEventProps } from "../interfaces/OccuringEvent";
import { orange } from "@mui/material/colors";

const StyledCardHeader = styled(CardHeader)(({ theme }) => ({
  backgroundColor: orange[200],
  "& .MuiCardHeader-title": {
    fontSize: "1.5rem",
  },
  "& .MuiCardHeader-subheader": {
    fontSize: "1rem",
    color: theme.palette.text.secondary,
  },
}));

export const OccuringEventsGrid = ({ events }: OccuringEventProps) => {
  return (
    <Container>
      <Grid2 container spacing={2}>
        {events.length === 0 ? (
          <Box
            display={"flex"}
            justifyContent={"center"}
            flexGrow={1}
            marginTop={3}
          >
            <Typography variant="h5" align="center">
              NO EVENTS ARE AVAILABLE AT THE MOMENT
            </Typography>
          </Box>
        ) : (
          events.map((event) => (
            <Grid2 key={event.id} size={{ xs: 2, md: 4 }}>
              <Card>
                <StyledCardHeader title={event.name} subheader={event.date} />
                <CardMedia component="img" image={event.avatar} />
                <CardContent />
                <CardActions />
              </Card>
            </Grid2>
          ))
        )}
      </Grid2>
    </Container>
  );
};
