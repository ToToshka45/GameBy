import { Box, TextField } from "@mui/material";
import { useContext } from "react";
import FiltersPropsContext from "../../contexts/FiltersPropsContext";
import FiltersProps from "../../types/FiltersProps";

export default function EventTitleFilter() {
  const filtersProps = useContext<FiltersProps | undefined>(
    FiltersPropsContext
  );

  const setFilteringTitle = filtersProps!.setFilteringTitle!;

  return (
    <Box display="flex" justifyContent="flex-end">
      <TextField
        label="Event name..."
        variant="outlined"
        size="medium"
        onChange={(e) => setFilteringTitle(e.target.value)}
        sx={{ width: { xs: "100%", md: "70%" } }}
      />
    </Box>
  );
}
