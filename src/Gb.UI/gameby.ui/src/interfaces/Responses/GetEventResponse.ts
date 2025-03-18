import { EventCategory, EventStatus } from "../../common/enums/EventEnums";
import Participant from "../EventParticipant";

export default interface GetEventResponse {
    id: number;
    organizerId: number;
    title: string;
    description: string;
    location: string;
    eventAvatarUrl: string;
    eventDate: Date;
    eventCategory: EventCategory;
    eventStatus: EventStatus;
    maxParticipants: number;
    minParticipants: number;
    participants: Participant[];
    isParticipant: boolean;
    isOrganizer: boolean;
}