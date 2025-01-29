import { Box, TextField } from "@mui/material";
import { useState } from "react";

export default function EventTitleFilter() {
  const [titleFilter, setTitleFilter] = useState<string>();

  return (
    <Box display="flex" justifyContent="flex-end">
      <TextField
        label="Event name..."
        variant="outlined"
        size="medium"
        fullWidth
        value={titleFilter}
        onChange={(e) => setTitleFilter(e.target.value)}
        sx={{ width: { sm: "70%", md: "50%", lg: "70%" } }}
      />
    </Box>
  );
}
