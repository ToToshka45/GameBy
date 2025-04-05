import { z } from "zod";

export const signInSchema = z.object({
  userCredential: z
    .string()
    .min(1, { message: "You must provide a username or an email" }),
  password: z.string().min(1, { message: "You must provide a password" }),
});

type SignInFormData = z.infer<typeof signInSchema>;

export default SignInFormData;
