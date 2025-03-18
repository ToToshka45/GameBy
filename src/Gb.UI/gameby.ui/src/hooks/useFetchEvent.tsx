import dayjs from "dayjs";
import EventStateDetails from "../common/consts/eventStateDetails";
import { OccuringEvent } from "../interfaces/EventEntities";
import GetEventResponse from "../interfaces/Responses/GetEventResponse";
import { axiosPrivate } from "../services/axios";
import useAuth from "./useAuth";
import AuthData from "../interfaces/AuthData";

const useFetchEvent = () => {
  const { userAuth } = useAuth() as AuthData;

  const fetchEvent = async (
    eventId: number
  ): Promise<OccuringEvent | undefined> => {
    // test logic -> event should be fetched from the server
    // const _event = events.find((e) => e.id === eventId);
    //

    try {
      console.log("Trying to fetch an event data...");

      const res = await axiosPrivate.post(`events/${eventId}`, {
        userId: userAuth?.id,
      });

      if (res && res.data) {
        console.log("Fetched a response: ", res.data);
        const responseBody = res.data as GetEventResponse;
        const event: OccuringEvent = {
          id: responseBody.id,
          organizerId: responseBody.organizerId,
          title: responseBody.title,
          description: responseBody.description,
          location: responseBody.location,
          maxParticipants: responseBody.maxParticipants,
          minParticipants: responseBody.minParticipants,
          participants: responseBody.participants,
          eventCategory: responseBody.eventCategory,
          eventAvatarUrl: responseBody.eventAvatarUrl,
          eventDate: dayjs(responseBody.eventDate),
          stateDetails: EventStateDetails[responseBody.eventStatus],
          isParticipant: responseBody.isParticipant,
          isOrganizer: responseBody.isOrganizer,
        };

        return event;
      }
      console.log("Fetching is unsuccesful.");
    } catch (err) {
      console.error("Error has occured while fetching an event: ", err);
    }
  };

  return fetchEvent;
};

export default useFetchEvent;
