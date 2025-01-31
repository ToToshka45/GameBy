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
      startDate: z
        .date()
        .refine((date) => date > dayjs().endOf("day").toDate(), {
          message:
            "The Start date must be at least 1 day after the current date.",
        }),
      endDate: z.date(),
      participants: z.number().min(1).default(0),
    })
    .refine((data) => {
      data.endDate < data.startDate,
        {
          message: "The End date can`t be before the Start date.",
          path: ["endDate"],
        };
    });

  type EventCreationFields = z.infer<typeof eventCreationSchema>;

  export default EventCreationFields;

// type Event = {
//     title: string;
//     description: string;
//     minParticipants: number;
//     maxParticipants: number;
//     location: string; // TODO: change to lat-long
//     img: ArrayBuffer
// }

// export default Event;