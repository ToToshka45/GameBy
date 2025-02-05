import { blue, green, red, yellow } from "@mui/material/colors";
import { EventProgressionState } from "../enums/EventEnums";
import EventStateWithColor from "../interfaces/EventStateWithColor";

// const EventStateDetails: Record<EventProgressionState, EventStateWithColor> = {
//     [EventProgressionState.Announced]: { state: "Announced", color: "primary" },
//     [EventProgressionState.RegistrationInProcess]: { state: "Registration in process", color: "warning" },
//     [EventProgressionState.RegistrationClosed]: { state: "Registration closed", color: "secondary" },
//     [EventProgressionState.Finished]: { state: "Finished", color: "error" },
// }

const EventStateDetails: Record<EventProgressionState, EventStateWithColor> = {
    [EventProgressionState.Announced]: { state: "Announced", color: green[200] },
    [EventProgressionState.RegistrationInProcess]: { state: "Registration in process", color: yellow[200] },
    [EventProgressionState.RegistrationClosed]: { state: "Registration closed", color: blue[200] },
    [EventProgressionState.Finished]: { state: "Finished", color: red[200] },
}

export default EventStateDetails;