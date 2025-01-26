import { DateRange } from "@mui/x-date-pickers-pro";
import dayjs, { Dayjs } from "dayjs";
import { Category } from "../enums/Category";

type FiltersProps {
  filteringCategories: Category[];
  setFilteringCategories:
    | React.Dispatch<React.SetStateAction<Category[]>>
    | undefined;
  filteringDates: DateRange<Dayjs>;
  setFilteringDates:
    | React.Dispatch<React.SetStateAction<DateRange<Dayjs>>>
    | undefined;
}

export default FiltersProps;
