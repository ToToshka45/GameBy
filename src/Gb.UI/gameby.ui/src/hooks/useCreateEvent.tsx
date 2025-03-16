import { useAuth } from "../contexts/AuthContext";
import AuthData from "../interfaces/AuthData";
import CreateEventRequest from "../interfaces/Requests/CreateEventRequest";
import CreateEventData from "../schemas/EventCreationForm";
import usePrivateAxios from "./usePrivateAxios";

// TODO: think of idempotency
const useCreateEvent = () => {
  const { userAuth } = useAuth() as AuthData;
  const axiosPrivate = usePrivateAxios();

  const createEvent = async (
    event: CreateEventData
  ): Promise<number | undefined> => {
    try {
      const createEventRequest: CreateEventRequest = {
        ...event,
        creationDate: Date.now().toString(),
        organizerId: userAuth?.id,
      };
      const res = await axiosPrivate.post("events/create", createEventRequest);
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
