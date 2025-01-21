import { Avatar, Box, Button, Typography } from "@mui/material";
import { useContext, useState } from "react";
import { Category } from "../../enums/Category";
import { EventCategoryFilterUnit } from "../../interfaces/EventCategoryFilterUnit";
import CategoryFilterContext from "../../contexts/CategoryFilterContext";
import {
  CategoryFilterUseStateProps,
  CategoryFilterUseStateContext,
} from "../MainPage";

const categoriesList: EventCategoryFilterUnit[] = [
  {
    name: "Strategy",
    img: "src/assets/categories/strategy.png",
    category: Category.Strategy,
    isActive: false,
  },
  {
    name: "Quiz",
    img: "src/assets/categories/quiz.png",
    category: Category.Quiz,
    isActive: false,
  },
  {
    name: "Sports",
    img: "src/assets/categories/football_ball.png",
    category: Category.Sports,
    isActive: false,
  },
  {
    name: "Mafia",
    img: "src/assets/categories/mafia_fedora.png",
    category: Category.Mafia,
    isActive: false,
  },
];

export const CategoryFilter = () => {
  const [categories, setCategories] = useState(categoriesList);
  const categoryFilterUseState = useContext<
    CategoryFilterUseStateProps | undefined
  >(CategoryFilterUseStateContext);

  const filteringCategories = categoryFilterUseState?.filteringCategories!;
  const setFilteringCategories =
    categoryFilterUseState?.setFilteringCategories!;

  const setActive = (key: String) => {
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

  console.log("From Filter: " + [...filteringCategories]);

  return (
    <CategoryFilterContext.Provider value={filteringCategories}>
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
                src={el.img} // Use this to display the image
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
    </CategoryFilterContext.Provider>
  );
};
