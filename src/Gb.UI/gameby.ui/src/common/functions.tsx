import { NavigateFunction } from "react-router-dom";
import { DisplayEvent } from "../interfaces/EventEntities";

export const handleNavigateEvent = (
  event: DisplayEvent,
  navigate: NavigateFunction
) => {
  navigate(`/event/${event.id}`, { state: { eventDetails: event } });
};
