import { Dayjs } from "dayjs";
import { EventCategory } from "../common/enums/EventEnums";
import EventStateWithColor from "./EventStateWithColor";
import EventParticipant from "./EventParticipant";

export interface DisplayEventProps {
    events: DisplayEvent[]
}

export interface OccuringEvent {
    id: number,
    organizerId: number;
    eventAvatarUrl: string;
    title: string;
    description: string;
    eventCategory: EventCategory;
    eventDate: Dayjs;
    location: string;
    stateDetails: EventStateWithColor;
    maxParticipants: number;
    minParticipants: number;
    participants: EventParticipant[];
  }

export interface DisplayEvent {
    id: number,
    eventAvatarUrl: string;
    title: string;
    eventCategory: EventCategory;
    eventDate: Dayjs;
    stateDetails: EventStateWithColor;
}
