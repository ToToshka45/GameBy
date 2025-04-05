import { Dayjs } from "dayjs";
import { ParticipationState } from "../common/enums/EventEnums";

export default interface Participant { 
    id: number;
    userId: number;
    username: string;
    eventId: number;
    state: ParticipationState | string;
    applyDate: Dayjs;
    acceptedDate: Dayjs;
    leaveDate: Dayjs;
}