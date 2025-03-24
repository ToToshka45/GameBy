import { FileWithPath } from "react-dropzone";
import AuthData from "../interfaces/AuthData";
import CreateEventRequest from "../interfaces/Requests/CreateEventRequest";
import CreateEventData from "../schemas/EventCreationForm";
import useAuth from "./useAuth";
import useInterceptingAxios from "./useInterceptingAxios";

// TODO: think of idempotency
const useCreateEvent = () => {
  const { userAuth } = useAuth() as AuthData;
  const privateAxios = useInterceptingAxios();

  const createEvent = async (
    event: CreateEventData,
    image: FileWithPath | undefined
  ): Promise<number | undefined> => {
    try {
      const formData = new FormData();
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
      for (const key in createEventRequest) {
        const data = (createEventRequest as any)[key];
        if (key.toLowerCase().includes("date"))
          formData.append(key, data.toISOString());
        else formData.append(key, data);
      }
      if (image) formData.append("eventAvatar", image);

      console.log(...formData);
      const res = await privateAxios.post("events/create", formData, {
        headers: { "Content-Type": "multipart/form-data" },
      });
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
