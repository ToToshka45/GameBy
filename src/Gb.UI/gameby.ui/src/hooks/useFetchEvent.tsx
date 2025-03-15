import { OccuringEvent } from "../interfaces/EventEntities";
import { axiosPrivate } from "../services/axios";

const useFetchEvent = () => {
  const fetchEvent = async (
    eventId: number
  ): Promise<OccuringEvent | undefined> => {
    // test logic -> event should be fetched from the server
    // const _event = events.find((e) => e.id === eventId);
    //

    try {
      const res = await axiosPrivate.get(`Events/${eventId}`);
      if (res && res.data) {
        return res.data as OccuringEvent;
      }
    } catch (err) {
      console.error("Error has occured while fetching an event: ", err);
    }
  };

  return fetchEvent;
};

export default useFetchEvent;
