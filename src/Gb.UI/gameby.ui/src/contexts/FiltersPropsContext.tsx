import { createContext } from "react";
import FiltersProps from "../types/FiltersProps";

/** Provides filterving Values and a useState function to set the collection of EventCategories, which events have to be filtered by. */
const FiltersPropsContext = createContext<FiltersProps | undefined>(undefined);

export default FiltersPropsContext;
