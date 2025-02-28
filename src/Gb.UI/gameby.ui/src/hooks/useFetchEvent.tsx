import { events } from "../common/consts/fakeData/testOccuringEvents";
import { OccuringEvent } from "../interfaces/OccuringEvent";

const useFetchEvent = () => {
  const fetchEvent = (eventId: number): OccuringEvent | undefined => {
    // test logic -> event should be fetched from the server
    const _event = events.find((e) => e.id === eventId);
    //

    // TODO: fetch

    return _event;
  };

  return fetchEvent;
};

export default useFetchEvent;
