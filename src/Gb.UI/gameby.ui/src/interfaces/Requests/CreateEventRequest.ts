export default interface CreateEventRequest {
    title: string;
    description: string;
    location: string;
    eventCategory: string;
    maxParticipants: number;
    minParticipants: number;
    eventDate: Date;
    endDate: Date;
    creationDate: Date;
    organizerId: number | undefined;
    // eventDurationInHours: number;
}