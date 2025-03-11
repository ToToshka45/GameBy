import { DateRange } from "@mui/x-date-pickers-pro";
import { Dayjs } from "dayjs";
import { EventCategory } from "../common/enums/EventEnums";

/** Props are used to filter out events, depending on the chosen filter settings by a User. */
export default interface FiltersProps {
  filteringTitle: string;
  setFilteringTitle: React.Dispatch<React.SetStateAction<string>> | undefined;
  filteringCategories: EventCategory[];
  setFilteringCategories:
    | React.Dispatch<React.SetStateAction<EventCategory[]>>
    | undefined;
  filteringDates: DateRange<Dayjs>;
  setFilteringDates:
    | React.Dispatch<React.SetStateAction<DateRange<Dayjs>>>
    | undefined;
};