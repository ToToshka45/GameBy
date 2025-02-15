import { Avatar, Box, Button, Typography } from "@mui/material";
import { useContext, useState } from "react";
import { EventCategory } from "../../enums/EventEnums";
import { EventCategoryFilterUnit } from "../../interfaces/EventCategoryFilterUnit";
import FiltersProps from "../../types/FiltersProps";
import FiltersPropsContext from "../../contexts/FiltersPropsContext";

const categoriesList: EventCategoryFilterUnit[] = [
  {
    name: "Strategy",
    img: "src/assets/categories/strategy.png",
    category: EventCategory.Strategy,
    isActive: false,
  },
  {
    name: "Quiz",
    img: "src/assets/categories/quiz.png",
    category: EventCategory.Quiz,
    isActive: false,
  },
  {
    name: "Sports",
    img: "src/assets/categories/football_ball.png",
    category: EventCategory.Sports,
    isActive: false,
  },
  {
    name: "Mafia",
    img: "src/assets/categories/mafia_fedora.png",
    category: EventCategory.Mafia,
    isActive: false,
  },
];

export const CategoryFilter = () => {
  const [categories, setCategories] = useState(categoriesList);
  const categoryFilterUseState = useContext<FiltersProps | undefined>(
    FiltersPropsContext
  );

  const filteringCategories = categoryFilterUseState!.filteringCategories!;
  const setFilteringCategories =
    categoryFilterUseState!.setFilteringCategories!;

  const setActive = (key: string) => {
    let filterBy = categories.find((c) => c.name === key);
    if (!filterBy) return;
    filterBy.isActive = !filterBy?.isActive;

    setCategories((categories) =>
      categories.map((category) =>
        category.name == filterBy?.name ? (category = filterBy) : category
      )
    );

    if (filterBy.isActive)
      setFilteringCategories([...filteringCategories, filterBy.category]);
    else
      setFilteringCategories(
        filteringCategories.filter((c) => c !== filterBy?.category)
      );
    // setCategories((categories) =>
    //   categories.map((category) =>
    //     category.name == key
    //       ? { ...category, isActive: !category.isActive }
    //       : category
    //   )
    // );
  };

  return (
    <Box
      display="flex"
      flexWrap="wrap"
      justifyContent="space-evenly"
      sx={{ gap: { sm: 2, md: 5 } }}
    >
      {categories.map((el) => (
        <Box display="flex" flexDirection="column" alignItems="center">
          <Button
            key={el.name}
            size="small"
            sx={{ borderRadius: "50%" }}
            onClick={() => setActive(el.name)}
          >
            <Avatar
              sx={{
                width: { sm: 50, md: 70, lg: 90 },
                height: { sm: 50, md: 70, lg: 90 },
                border: "1px solid gray",
                bgcolor: el.isActive ? "orange" : "transparent", // Change the color as needed
                "& img": {
                  height: "60%",
                  width: "60%",
                },
              }}
              src={el.img} // Use this to display the image
            />
          </Button>
          <Typography
            variant="caption"
            sx={{ marginTop: 1, fontSize: { sm: 12, md: 14 } }}
          >
            {el.name || "Unknown"}
          </Typography>
        </Box>
      ))}
    </Box>
  );
};
