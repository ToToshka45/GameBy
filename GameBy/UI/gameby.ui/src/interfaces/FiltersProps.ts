import { DateRange } from "@mui/x-date-pickers-pro";
import { Dayjs } from "dayjs";
import { Category } from "../enums/Category";

/** Props are used to filter out the events depending on the chosen filter settings by a User. */
type FiltersProps = {
  filteringTitle: string;
  setFilteringTitle: React.Dispatch<React.SetStateAction<string>> | undefined;
  filteringCategories: Category[];
  setFilteringCategories:
    | React.Dispatch<React.SetStateAction<Category[]>>
    | undefined;
  filteringDates: DateRange<Dayjs>;
  setFilteringDates:
    | React.Dispatch<React.SetStateAction<DateRange<Dayjs>>>
    | undefined;
};

export default FiltersProps;
