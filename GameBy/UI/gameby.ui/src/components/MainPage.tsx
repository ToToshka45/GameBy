import { Box } from "@mui/material";
import { FiltersMenu } from "./FiltersMenu";
import { OccuringEventsPanel } from "./Events/OccuringEventsPanel";
import { createContext, useState } from "react";
import { Category } from "../enums/Category";
import CategoryFilterContext from "../contexts/CategoryFilterContext";

export default function MainPage() {
  const [filteringCategories, setFilteringCategories] = useState<Category[]>(
    []
  );

  const contextValue: CategoryFilterUseStateProps = {
    filteringCategories,
    setFilteringCategories,
  };

  return (
    <Box>
      <CategoryFilterUseStateContext.Provider value={contextValue}>
        <FiltersMenu />
      </CategoryFilterUseStateContext.Provider>
      <CategoryFilterContext.Provider value={filteringCategories}>
        <OccuringEventsPanel />
      </CategoryFilterContext.Provider>
    </Box>
  );
}

export interface CategoryFilterUseStateProps {
  filteringCategories: Category[];
  setFilteringCategories: React.Dispatch<React.SetStateAction<Category[]>>;
}

/** Provides a filterving Value and a useState function to set the collection of EventCategories, which events have to filtered by. */
export const CategoryFilterUseStateContext = createContext<
  CategoryFilterUseStateProps | undefined
>(undefined);
