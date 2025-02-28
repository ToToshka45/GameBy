import { Dayjs } from "dayjs";
import { EventCategory } from "../common/enums/EventEnums";
import EventStateWithColor from "./EventStateWithColor";
import EventParticipant from "./EventParticipant";

export interface OccuringEventProps {
    events: OccuringEvent[]
}

export interface OccuringEvent {
    id: number,
    avatar: string;
    title: string;
    content: string;
    category: EventCategory;
    date: Dayjs;
    stateDetails: EventStateWithColor;
    participants: EventParticipant[];
  }

