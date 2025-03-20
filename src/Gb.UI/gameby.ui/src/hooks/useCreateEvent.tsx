import AuthData from "../interfaces/AuthData";
import CreateEventRequest from "../interfaces/Requests/CreateEventRequest";
import CreateEventData from "../schemas/EventCreationForm";
import useAuth from "./useAuth";
import usePrivateAxios from "./usePrivateAxios";

// TODO: think of idempotency
const useCreateEvent = () => {
  const { userAuth } = useAuth() as AuthData;
  const privateAxios = usePrivateAxios();

  const createEvent = async (
    event: CreateEventData
  ): Promise<number | undefined> => {
    try {
      const createEventRequest: CreateEventRequest = {
        // ...event,
        title: event.title,
        description: event.description,
        location: event.location,
        eventCategory: event.eventCategory,
        maxParticipants: event.maxParticipants,
        minParticipants: event.minParticipants,
        eventDate: event.startDate,
        endDate: event.endDate,
        creationDate: new Date(),
        organizerId: userAuth?.id,
      };
      console.log("Sending a request: ", createEventRequest);
      const res = await privateAxios.post("events/create", createEventRequest);
      if (res && res.data) {
        const eventId: number = res.data.eventId;
        return eventId;
      }
    } catch (err) {
      console.log("Event creation error: ", err);
    }
  };

  return createEvent;
};

export default useCreateEvent;
