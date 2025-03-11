import { useNavigate } from "react-router";
import { useAuth } from "../contexts/AuthContext";
import AuthData from "../interfaces/AuthData";
import CreateEventRequest from "../interfaces/Requests/CreateEventRequest";
import CreateEventData from "../schemas/EventCreationForm";
import usePrivateAxios from "./usePrivateAxios";

// TODO: think of idempotency
const useCreateEvent = () => {
  const { userAuth } = useAuth() as AuthData;
  const navigate = useNavigate();
  const axiosPrivate = usePrivateAxios();

  const createEvent = async (event: CreateEventData) => {
    try {
      const createEventRequest: CreateEventRequest = {
        ...event,
        organizerId: userAuth?.id,
      };
      const res = await axiosPrivate.post("events/create", createEventRequest);
      if (res && res.data) {
        const eventId: number = res.data.eventId;
        navigate(`event/${eventId}`);
      }
    } catch (err) {
      console.log("Event creation error: ", err);
    }
  };

  return createEvent;
};

export default useCreateEvent;
