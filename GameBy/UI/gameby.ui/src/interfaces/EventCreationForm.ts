import dayjs from "dayjs";
import { z } from "zod";

export const eventCreationSchema = z
    .object({
      title: z
        .string()
        .min(1, { message: "The title must contain at least 1 character" }),
      description: z
        .string()
        .min(20, {
          message: "The description must contain at least 20 characters",
        })
        .max(250, {
          message: "The description must contain maximum 250 characters",
        }),
      startDate: z.date()
        .refine((date) => date > dayjs().endOf("day").toDate(), {
          message:
            "The Start date must be at least 1 day after the current date",
        }),
      endDate: z.date().refine((date) => date > dayjs().endOf("day").toDate(), {
        message:
          "The End date can`t be before the Start date",
      }),
      location: z.string().min(1, { message: "Define the event`s location" }),
      minParticipants: z.coerce.number().min(1, { message: "The minimal number of Participants must be 1 or more" }),
      maxParticipants: z.coerce.number().min(1, { message: "The maximum number of Participants must be 1 or more" }),
    })
      .refine((data) => data.maxParticipants >= data.minParticipants,
      {
        message: "The maximum number of participant should equal or more then minimum number",
        path: ["maxParticipants"],
      })
      .refine((data) => data.endDate >= data.startDate,
      {
        message: "The End date should be equal or after the Start date",
        path: ["endDate"],
      });

  type EventCreationForm = z.infer<typeof eventCreationSchema>;

  export default EventCreationForm;

  export function createDefaultEvent() : EventCreationForm {
    return {
      title: "",
      description: "",
      startDate: dayjs().add(1, "day").toDate(),
      endDate: dayjs().add(1, "day").toDate(),
      location: "",
      minParticipants: 1,
      maxParticipants: 0
    }
  }

// type Event = {
//     title: string;
//     description: string;
//     minParticipants: number;
//     maxParticipants: number;
//     location: string; // TODO: change to lat-long
//     img: ArrayBuffer
// }

// export default Event;