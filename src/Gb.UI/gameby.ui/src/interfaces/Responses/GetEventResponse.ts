import { FileWithPath } from "react-dropzone";
import { EventCategory, EventStatus } from "../../common/enums/EventEnums";
import Participant from "../EventParticipant";

export default interface GetEventResponse {
    id: number;
    organizerId: number;
    title: string;
    description: string;
    location: string;
    eventAvatarUrl: string;
    eventAvatarFile: EventAvatar | undefined;
    eventDate: Date;
    eventCategory: EventCategory;
    eventStatus: EventStatus;
    maxParticipants: number;
    minParticipants: number;
    participants: Participant[];
    isParticipant: boolean;
    isOrganizer: boolean;
}

export interface EventAvatar {
    contentType: string;
    content: string;
}