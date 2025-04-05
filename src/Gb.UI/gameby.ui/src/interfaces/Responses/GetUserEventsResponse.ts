import GetShortEventResponse from "./GetShortEventResponse";

export default interface GetUserEventsResponse {
    userId: number;
    gamerEvents: GetShortEventResponse[],
    organizerEvents: GetShortEventResponse[]
}