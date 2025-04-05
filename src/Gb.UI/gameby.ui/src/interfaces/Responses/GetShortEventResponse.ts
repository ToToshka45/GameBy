import { EventCategory, EventStatus } from "../../common/enums/EventEnums";

export default interface GetShortEventResponse {
    id: number;
    organizerId: number;
    title: string;
    eventAvatarUrl: string;
    eventDate: Date;
    eventCategory: EventCategory;
    eventStatus: EventStatus;
}