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
    // eventAvatarUrl: FormData | string | undefined;
    // eventDurationInHours: number;
}