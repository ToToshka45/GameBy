import CreateEventData from "../../schemas/EventCreationForm";

export default interface CreateEventRequest extends CreateEventData {
    organizerId: number | undefined;
}