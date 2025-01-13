export interface OccuringEventProps {
    events: OccuringEvent[]
}

export interface OccuringEvent {
    id: number,
    avatar: string;
    name: string;
    content: string;
    date: string;
  }