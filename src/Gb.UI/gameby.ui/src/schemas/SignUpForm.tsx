import { z } from "zod";

export const signUpSchema = z
  .object({
    email: z.string().email({ message: "Invalid email address" }),
    userName: z
      .string()
      .min(1, { message: "Username must be at least 1 character long" })
      .regex(/^[a-zA-Z0-9]*$/, {
        message: "Username must contain only Latin letters and numbers",
      }),
    password: z
      .string()
      .min(8, { message: "Password must be at least 8 characters long" })
      .regex(/^(?=.*[0-9]).+$/, {
        message: "Password must contain at least one number",
      })
      .regex(/^(?=.*[A-Z]).+$/, {
        message: "Password must contain at least one upper-case letter",
      })
      .regex(/^(?=.*[\W_]).+$/, {
        message: "Password must contain at least one special symbol",
      }),
    confirmPassword: z.string(),
  })
  .refine((data) => data.password === data.confirmPassword, {
    message: "Passwords do not match",
    path: ["confirmPassword"],
  });

type SignUpFormData = z.infer<typeof signUpSchema>;

export default SignUpFormData;
