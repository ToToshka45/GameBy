import dayjs from "dayjs";
import EventStateDetails from "../common/consts/eventStateDetails";
import { DisplayEvent, OccuringEvent } from "../interfaces/EventEntities";
import GetEventResponse from "../interfaces/Responses/GetEventResponse";
import useAuth from "./useAuth";
import AuthData from "../interfaces/AuthData";
import { ParticipationState } from "../common/enums/EventEnums";
import useInterceptingAxios from "./useInterceptingAxios";
import GetShortEventResponse from "../interfaces/Responses/GetShortEventResponse";

const useEventProcessing = () => {
  const { userAuth } = useAuth() as AuthData;
  const eventsAxios = useInterceptingAxios();

  const fetchEvents = async (eventFilters: any) => {
    const res = await eventsAxios.post("events", eventFilters);

    if (res && res.data) {
      const fetchedEvents: GetShortEventResponse[] = res.data;
      const occuringEvents: DisplayEvent[] = fetchedEvents.map((ev) => {
        return {
          id: ev.id,
          title: ev.title,
          eventAvatarUrl: ev.eventAvatarUrl,
          eventCategory: ev.eventCategory,
          eventDate: dayjs(ev.eventDate),
          stateDetails: EventStateDetails[ev.eventStatus],
        };
      });
      return occuringEvents;
    }
    return [];
  };

  const fetchEvent = async (
    eventId: number
  ): Promise<OccuringEvent | undefined> => {
    // test logic -> event should be fetched from the server
    // const _event = events.find((e) => e.id === eventId);
    //

    try {
      console.log("Trying to fetch an event data...");

      const res = await eventsAxios.post(`events/${eventId}`, {
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

  const sendParticipantState = async (
    participantId: number,
    newState: ParticipationState,
    eventId: number,
    acceptedDate?: Date
  ) => {
    try {
      await eventsAxios.post(
        `events/${eventId}/participants/${participantId}?state=${newState}`,
        { acceptedDate }
      );
    } catch (err) {
      console.error(err);
    }
  };

  const sendParticipationRequest = async (
    userId: number,
    username: string,
    // email: string,
    eventId: number
  ) => {
    console.log(`Sending a participantion request for a user ${username}...`);
    await eventsAxios.post(`/events/${eventId}/participants/add`, {
      userId,
      username,
      applyDate: new Date(),
    });
  };

  return {
    fetchEvents,
    fetchEvent,
    sendParticipantState,
    sendParticipationRequest,
  };
};

export default useEventProcessing;
