import { createContext } from "react";
import { Category } from "../enums/Category";

const CategoryFilterContext = createContext<Category[]>([]);

export default CategoryFilterContext;
