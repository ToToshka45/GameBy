import { EventCategory } from "../enums/EventEnums";

/** Represents a particular Filter for displaying on the Main page of the app. */
export interface EventCategoryFilterUnit {
    name: string,
    img: string,
    category: EventCategory,
    isActive: boolean,   
}