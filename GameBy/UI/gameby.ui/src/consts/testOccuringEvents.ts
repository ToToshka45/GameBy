import { EventCategory, EventProgressionState } from "../enums/EventEnums";
import { OccuringEvent } from "../interfaces/OccuringEvent";
import dayjs from "dayjs";
import EventStateDetails from "./eventStateDetails";

interface TestOccuringEvent extends OccuringEvent { } 

export default TestOccuringEvent;

export const DATE_FORMAT = "DD/MM/YYYY";

export const events: TestOccuringEvent[] = [
  {
    id: 1,
    avatar: "src/assets/event-pics/mafia-pic.webp",
    title: "Mafia 2025",
    content: "Мафия — салонная командная психологическая пошаговая ролевая игра с детективным сюжетом, моделирующая борьбу информированных друг о друге членов организованного меньшинства с неорганизованным большинством." 
    + " Завязка сюжета: Жители города, обессилевшие от разгула мафии, выносят решение пересажать в тюрьму всех мафиози до единого. В ответ мафия объявляет войну до полного уничтожения всех порядочных горожан.",
    category: EventCategory.Mafia,
    date: dayjs().add(+7, "day"),
    stateDetails: EventStateDetails[EventProgressionState.RegistrationInProcess]
  },
  {
    id: 2,
    avatar: "src/assets/event-pics/event_default.jpg",
    title: "Event2",
    content: "Basic description",
    category: EventCategory.Strategy,
    date: dayjs().add(+3, "day"),
    stateDetails: EventStateDetails[EventProgressionState.RegistrationClosed]
  },
  {
    id: 3,
    avatar: "src/assets/event-pics/event_default.jpg",
    title: "Event3",
    content: "Basic description",
    category: EventCategory.Sports,
    date: dayjs().add(+4, "day"),
    stateDetails: EventStateDetails[EventProgressionState.RegistrationInProcess]
  },
  {
    id: 4,
    avatar: "src/assets/event-pics/event_default.jpg",
    title: "Event4",
    content: "Basic description",
    category: EventCategory.Strategy,
    date: dayjs().add(+11, "day"),
    stateDetails: EventStateDetails[EventProgressionState.Announced]
  },
  {
    id: 5,
    avatar: "src/assets/event-pics/event_default.jpg",
    title: "Event5",
    content: "Basic description",
    category: EventCategory.Quiz,
    date: dayjs().add(-1, "day"),
    stateDetails: EventStateDetails[EventProgressionState.Finished]
  },
  {
    id: 6,
    avatar: "src/assets/event-pics/event_default.jpg",
    title: "Event6",
    content: "Basic description",
    category: EventCategory.Sports,
    date: dayjs().add(+2, "day"),
    stateDetails: EventStateDetails[EventProgressionState.RegistrationClosed]
  },
].sort((a, b) => a.date.date() - b.date.date());