import { z } from "zod";

export const isEmailSchema = z.string().email();
