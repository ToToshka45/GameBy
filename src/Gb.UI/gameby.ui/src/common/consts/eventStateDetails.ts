import { blue, green, orange, pink, red, yellow } from "@mui/material/colors";
import { EventStatus } from "../../common/enums/EventEnums";
import EventStateWithColor from "../../interfaces/EventStateWithColor";

// const EventStateDetails: Record<EventProgressionState, EventStateWithColor> = {
//     [EventProgressionState.Announced]: { state: "Announced", color: "primary" },
//     [EventProgressionState.RegistrationInProcess]: { state: "Registration in process", color: "warning" },
//     [EventProgressionState.RegistrationClosed]: { state: "Registration closed", color: "secondary" },
//     [EventProgressionState.Finished]: { state: "Finished", color: "error" },
// }

const EventStateDetails: Record<EventStatus, EventStateWithColor> = {
    [EventStatus.Announced]: { state: "Announced", color: green[200] },
    [EventStatus.RegistrationInProcess]: { state: "Registration in process", color: yellow[200] },
    [EventStatus.RegistrationClosed]: { state: "Registration closed", color: blue[200] },
    [EventStatus.InProgress]: { state: "InProgress", color: orange[200] },
    [EventStatus.Finished]: { state: "Finished", color: red[200] },    
    [EventStatus.Cancelled]: { state: "Cancelled", color: pink[800] },
}

export default EventStateDetails;