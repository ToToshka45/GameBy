import { EventCategory, EventProgressionState, ParticipationState } from "../../enums/EventEnums";
import { OccuringEvent } from "../../../interfaces/OccuringEvent";
import dayjs from "dayjs";
import EventStateDetails from "../eventStateDetails";
import { loremIpsum } from "./defaults";

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
    stateDetails: EventStateDetails[EventProgressionState.RegistrationInProcess],
    participants: []
  },
  {
    id: 2,
    avatar: "src/assets/event-pics/event_default.jpg",
    title: "Starcraft 3: Epic starship battles",
    content: loremIpsum,
    category: EventCategory.Strategy,
    date: dayjs().add(+3, "day"),
    stateDetails: EventStateDetails[EventProgressionState.RegistrationClosed],
    participants: [
      { id: 1, userId: 8, userName: 'Random Guy Sam', eventId: 2, participationState: ParticipationState.Registered },
      { id: 2, userId: 12, userName: 'CoolGirlAlice', eventId: 2, participationState: ParticipationState.PendingAcceptance },
      { id: 3, userId: 420, userName: 'DoomGuy__69', eventId: 2, participationState: ParticipationState.Registered },
      { id: 4, userId: 11, userName: 'Dunno',  eventId: 2, participationState: ParticipationState.Declined },
      {
        id: 5, 
        userId: 5,
        userName: "Bobby Boy", eventId: 2,
        participationState: ParticipationState.Registered
      },
      {
        id: 6,
        userId: 6,
        userName: "HailMarry", eventId: 2,
        participationState: ParticipationState.PendingAcceptance
      },
    ]
  },
  {
    id: 3,
    avatar: "src/assets/event-pics/event_default.jpg",
    title: "Event3",
    content: loremIpsum,
    category: EventCategory.Sports,
    date: dayjs().add(+4, "day"),
    stateDetails: EventStateDetails[EventProgressionState.RegistrationInProcess],
    participants: [],
    pendingParticipants: [],
  },
  {
    id: 4,
    avatar: "src/assets/event-pics/event_default.jpg",
    title: "Event4",
    content: loremIpsum,
    category: EventCategory.Strategy,
    date: dayjs().add(+11, "day"),
    stateDetails: EventStateDetails[EventProgressionState.Announced],
    participants: [],
    pendingParticipants: [],
  },
  {
    id: 5,
    avatar: "src/assets/event-pics/event_default.jpg",
    title: "Event5",
    content: loremIpsum,
    category: EventCategory.Quiz,
    date: dayjs().add(-1, "day"),
    stateDetails: EventStateDetails[EventProgressionState.Finished],
    participants: [],
    pendingParticipants: [],
  },
  {
    id: 6,
    avatar: "src/assets/event-pics/event_default.jpg",
    title: "Event6",
    content: loremIpsum,
    category: EventCategory.Sports,
    date: dayjs().add(+2, "day"),
    stateDetails: EventStateDetails[EventProgressionState.RegistrationClosed],
    participants: [],
    pendingParticipants: [],
  },
].sort((a, b) => a.date.date() - b.date.date());