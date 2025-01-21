import { Category } from "../enums/Category";

export interface EventCategoryFilterUnit {
    name: string,
    img: string,
    category: Category,
    isActive: boolean,   
}