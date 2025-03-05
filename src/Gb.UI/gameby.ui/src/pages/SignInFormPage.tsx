import {
  Box,
  Button,
  IconButton,
  Paper,
  TextField,
  Typography,
} from "@mui/material";
import SignInFormData, { signInSchema } from "../schemas/SignInForm";
import { Controller, SubmitHandler, useForm } from "react-hook-form";
import { VisibilityOff, Visibility } from "@mui/icons-material";
import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect, useState } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { z } from "zod";
import { isEmailSchema } from "../schemas/IsEmailSchema";
import useLogin from "../hooks/useLogin";
import LoginRequest from "../interfaces/LoginRequest";

const SignInFormPage = () => {
  const [passHidden, setPassHidden] = useState(true);

  const location = useLocation();
  const from = location?.state?.from?.pathname ?? "/";
  const navigate = useNavigate();
  const login = useLogin();

  // define a react-hook-form, passing the zodResolver with signUpSchema type
  const {
    control,
    handleSubmit,
    setError,
    formState: { errors, isSubmitting },
  } = useForm<SignInFormData>({
    resolver: zodResolver(signInSchema),
    defaultValues: {
      userCredential: "",
      password: "",
    },
  });

  const onSubmit: SubmitHandler<SignInFormData> = async (
    data: SignInFormData
  ) => {
    // handle data (send to the server)
    const isEmail = isEmailSchema.safeParse(data.userCredential);
    const loginRequest: LoginRequest = {
      username: !isEmail.success ? data.userCredential : "",
      email: isEmail.success ? data.userCredential : "",
      password: data.password,
    };

    console.log("Sign-In data submitted: ", loginRequest);

    try {
      await login(loginRequest);
    } catch (err) {
      console.error("Error occured: ", err);
      if (err instanceof z.ZodError) {
        setError("root", {
          message: err.message,
        });
      }
    }

    navigate(from, { replace: true });
  };

  const handlePassHidden = () => {
    setPassHidden(!passHidden);
  };

  return (
    <Box display="flex" flexDirection="column" alignItems="center" mt={4}>
      <Paper elevation={4} sx={{ p: 5 }}>
        <Typography variant="h4" gutterBottom>
          Sign In
        </Typography>
        <Typography variant="body2" color="primary">
          Please, provide your username/email and password
        </Typography>

        <Box
          component="form"
          onSubmit={handleSubmit(onSubmit)}
          sx={{ width: 300 }}
        >
          <Controller
            name="userCredential"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                label="Username/Email"
                margin="normal"
                placeholder="Username / Email"
                fullWidth
                error={!!errors.userCredential}
                helperText={
                  errors.userCredential ? errors.userCredential.message : ""
                }
              />
            )}
          />

          <Controller
            name="password"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                type={passHidden ? "password" : "text"}
                label="Password"
                margin="normal"
                fullWidth
                error={!!errors.password}
                helperText={errors.password ? errors.password.message : ""}
                slotProps={{
                  input: {
                    endAdornment: (
                      <IconButton onClick={handlePassHidden}>
                        {passHidden ? <VisibilityOff /> : <Visibility />}
                      </IconButton>
                    ),
                  },
                }}
              />
            )}
          />

          <Button
            type="submit"
            variant="contained"
            color="primary"
            fullWidth
            sx={{ mt: 2 }}
            disabled={isSubmitting}
          >
            Login
          </Button>

          <Box
            display="flex"
            flexDirection="column"
            alignItems="center"
            gap={0.5}
          >
            <Typography variant="caption" marginTop={2}>
              Not registered yet?
            </Typography>
            <Link to="/sign-up" style={{ fontSize: 16 }}>
              Sign Up
            </Link>
          </Box>
        </Box>
      </Paper>
    </Box>
  );
};

export default SignInFormPage;
