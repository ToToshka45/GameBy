import { Dayjs } from "dayjs";
import { EventCategory } from "../enums/EventEnums";
import EventStateWithColor from "./EventStateWithColor";

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
  }

