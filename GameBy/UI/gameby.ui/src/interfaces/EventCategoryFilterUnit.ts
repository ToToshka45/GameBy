import { Category } from "../enums/Category";

/** Represents a particular Filter for displaying on the Main page of the app. */
export interface EventCategoryFilterUnit {
    name: string,
    img: string,
    category: Category,
    isActive: boolean,   
}