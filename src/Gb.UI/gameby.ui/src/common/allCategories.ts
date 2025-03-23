import { EventCategoryFilterUnit } from "../interfaces/EventCategoryFilterUnit";
import { EventCategory } from "./enums/EventEnums";

export const categoriesList: EventCategoryFilterUnit[] = [
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
  {
    name: "Poker",
    img: "src/assets/categories/poker3.png",
    category: EventCategory.Poker,
    isActive: false,
  }
];