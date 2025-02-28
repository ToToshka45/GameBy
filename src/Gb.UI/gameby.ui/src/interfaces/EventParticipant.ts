import { ParticipationState } from "../common/enums/EventEnums";

interface EventParticipant { 
    id: number;
    userId: number;
    userName: string;
    eventId: number;
    participationState: ParticipationState
}

export default EventParticipant;