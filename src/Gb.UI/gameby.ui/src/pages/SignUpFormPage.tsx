import { useForm, Controller, SubmitHandler } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  Typography,
  TextField,
  Button,
  Box,
  Paper,
  IconButton,
} from "@mui/material";
import SignUpFormData, { signUpSchema } from "../schemas/SignUpForm";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { useState } from "react";
import { Visibility, VisibilityOff } from "@mui/icons-material";
import useRegister from "../hooks/useRegister";

// TODO: check existing users with a provided email
// TODO: check other users to have the same username as provided

const SignUpFormPage = () => {
  const [passHidden, setPassHidden] = useState(true);
  const [confirmPassHidden, setConfirmPassHidden] = useState(true);
  const location = useLocation();
  const from = location?.state?.from?.pathname ?? "/";
  const navigate = useNavigate();
  const register = useRegister();

  // define a react-hook-form, passing the zodResolver with signUpSchema type
  const {
    control,
    handleSubmit,
    formState: { errors },
  } = useForm<SignUpFormData>({
    resolver: zodResolver(signUpSchema),
    defaultValues: {
      email: "",
      userName: "",
      password: "",
      confirmPassword: "",
    },
  });

  const onSubmit: SubmitHandler<SignUpFormData> = async (
    data: SignUpFormData
  ) => {
    // handle data (send to the server)
    console.log("Sign-Up data submitted: ", data);
    await register(data);

    navigate(from, { replace: true });
  };

  const handlePassHidden = () => {
    setPassHidden(!passHidden);
  };

  const handleConfirmPassHidden = () => {
    setConfirmPassHidden(!confirmPassHidden);
  };

  return (
    <Box display="flex" flexDirection="column" alignItems="center" mt={4}>
      <Paper elevation={4} sx={{ p: 5 }}>
        <Typography variant="h4" gutterBottom>
          Sign Up
        </Typography>
        <Typography variant="body2" color="primary">
          Please, provide your credentials
        </Typography>

        <Box
          component="form"
          onSubmit={handleSubmit(onSubmit)}
          sx={{ width: 300 }}
        >
          <Controller
            name="email"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                label="Email"
                margin="normal"
                fullWidth
                error={!!errors.email}
                helperText={errors.email ? errors.email.message : ""}
              />
            )}
          />

          <Controller
            name="userName"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                label="Username"
                margin="normal"
                fullWidth
                error={!!errors.userName}
                helperText={errors.userName ? errors.userName.message : ""}
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

          <Controller
            name="confirmPassword"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                type={confirmPassHidden ? "password" : "text"}
                label="Confirm Password"
                margin="normal"
                fullWidth
                error={!!errors.confirmPassword}
                helperText={
                  errors.confirmPassword ? errors.confirmPassword.message : ""
                }
                slotProps={{
                  input: {
                    endAdornment: (
                      <IconButton onClick={handleConfirmPassHidden}>
                        {confirmPassHidden ? <VisibilityOff /> : <Visibility />}
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
          >
            Sign Up
          </Button>

          <Box
            display="flex"
            flexDirection="column"
            alignItems="center"
            gap={0.5}
          >
            <Typography variant="caption" marginTop={2}>
              Already signed up?
            </Typography>
            <Link to="/sign-in" style={{ fontSize: 16 }}>
              Sign In
            </Link>
          </Box>
        </Box>
      </Paper>
    </Box>
  );
};

export default SignUpFormPage;
