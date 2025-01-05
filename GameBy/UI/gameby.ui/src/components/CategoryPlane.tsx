import { Avatar, Box, Typography } from "@mui/material";

const categories = [
  { name: "Strategy", pic: "src/assets/categories/strategy.png" },
  { name: "Quiz", pic: "src/assets/categories/quiz.png" },
];

export const CategoryPlane = () => {
  return (
    <Box
      display={"flex"}
      justifyContent={"space-evenly"}
      gap={1}
      marginTop={10}
    >
      {categories.map((el, idx) => (
        <Box
          key={idx}
          sx={{
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
            m: 2,
          }}
        >
          <Avatar
            sx={{
              overflow: "hidden",
              width: 110,
              height: 110,
              border: "1px solid gray",
              bgcolor: "transparent", // Change the color as needed
              "& img": {
                height: "60%",
                width: "60%",
              },
            }}
            src={el.pic} // Use this to display the image
          />
          <Typography variant="caption" sx={{ marginTop: 1 }}>
            {el.name}
          </Typography>
        </Box>
      ))}
    </Box>
  );
};
