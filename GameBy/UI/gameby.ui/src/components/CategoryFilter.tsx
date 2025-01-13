import { Avatar, Box, Button, Typography } from "@mui/material";
import { useState } from "react";

const categoriesList = [
  {
    name: "Strategy1",
    pic: "src/assets/categories/strategy.png",
    isActive: false,
  },
  { name: "Quiz1", pic: "src/assets/categories/quiz.png", isActive: false },
  {
    name: "Strategy2",
    pic: "src/assets/categories/strategy.png",
    isActive: false,
  },
  { name: "Quiz2", pic: "src/assets/categories/quiz.png", isActive: false },
  {
    name: "Strategy3",
    pic: "src/assets/categories/strategy.png",
    isActive: false,
  },
  { name: "Quiz3", pic: "src/assets/categories/quiz.png", isActive: false },
  {
    name: "Strategy4",
    pic: "src/assets/categories/strategy.png",
    isActive: false,
  },
  { name: "Quiz4", pic: "src/assets/categories/quiz.png", isActive: false },
];

export const CategoryFilter = () => {
  const [categories, setCategories] = useState(categoriesList);

  const setActive = (key: String) => {
    setCategories((categories) =>
      categories.map((category) =>
        category.name == key
          ? { ...category, isActive: !category.isActive }
          : category
      )
    );
  };

  return (
    <Box>
      <Box
        display={"flex"}
        flexWrap={"wrap"}
        justifyContent={"space-evenly"}
        marginTop={10}
        width={"auto"}
      >
        {categories.map((el) => (
          <Box display={"flex"} flexDirection={"column"} alignItems={"center"}>
            <Button
              key={el.name}
              size="small"
              sx={{ borderRadius: "50%" }}
              onClick={() => setActive(el.name)}
            >
              <Avatar
                sx={{
                  width: { xs: 60, sm: 90 },
                  height: { xs: 60, sm: 90 },
                  border: "1px solid gray",
                  bgcolor: el.isActive ? "orange" : "transparent", // Change the color as needed
                  "& img": {
                    height: "60%",
                    width: "60%",
                  },
                }}
                src={el.pic} // Use this to display the image
              />
            </Button>
            <Typography variant="caption" sx={{ marginTop: 1 }}>
              {el.name || "Unknown"}
            </Typography>
          </Box>
        ))}
      </Box>
      {/* <Box display={"flex"}>
        <Button
          variant="outlined"
          color="primary"
          size="medium"
          sx={{ marginRight: "auto", marginTop: 3, width: "150px" }}
        >
          Reset
        </Button>
      </Box> */}
    </Box>
  );
};
