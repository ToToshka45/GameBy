import { NavigateFunction } from "react-router-dom";
import { OccuringEvent } from "../interfaces/OccuringEvent";

export const handleNavigateEvent = (
  event: OccuringEvent,
  navigate: NavigateFunction
) => {
  navigate(`/event/${event.id}}`, { state: { eventDetails: event } });
};
