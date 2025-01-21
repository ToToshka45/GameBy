import { Category } from "../enums/Category";

export interface OccuringEventProps {
    events: OccuringEvent[]
}

export interface OccuringEvent {
    id: number,
    avatar: string;
    name: string;
    content: string;
    category: Category;
    date: string;
  }

