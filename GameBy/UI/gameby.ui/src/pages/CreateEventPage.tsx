import {
  Box,
  Button,
  Container,
  Paper,
  Stack,
  TextField,
  Typography,
} from "@mui/material";
import { DateTimePicker, LocalizationProvider } from "@mui/x-date-pickers";
import dayjs from "dayjs";
import { Controller, SubmitHandler, useForm } from "react-hook-form";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import Dropzone from "react-dropzone";
import { useCallback } from "react";
import { green, pink } from "@mui/material/colors";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";

// TODO: Add saving of the typed values to the Local Storage

export default function CreateEventPage() {
  const eventSchema = z
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

  type EventCreationFields = z.infer<typeof eventSchema>;

  const {
    control,
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<EventCreationFields>({
    resolver: zodResolver(eventSchema),
  });

  const onFileAccept = useCallback((acceptedFiles: any) => {
    console.log(acceptedFiles);
  }, []);

  const onSubmit: SubmitHandler<EventCreationFields> = (data) => {
    console.log(data);
  };

  return (
    <Box mx="3%" height="100vh" bgcolor="orange">
      {" "}
      <Paper
        elevation={2}
        sx={{
          padding: { xs: "4%", md: "2%" },
          paddingBottom: { xs: "4%", md: "2%" },
        }}
      >
        <Typography variant="h4" gutterBottom>
          Create Event
        </Typography>
        <form
          onSubmit={handleSubmit(onSubmit)}
          style={{
            display: "flex",
            flexDirection: "column",
            gap: 10,
          }}
        >
          <TextField
            {...register("title")}
            label="Title"
            name="title"
            variant="outlined"
          />
          {errors.title && (
            <Typography variant="body2" color="error">
              {errors.title?.message}
            </Typography>
          )}
          <TextField
            {...register("description")}
            label="Description"
            name="description"
            variant="outlined"
            multiline
            rows={4}
          />
          {errors.description && (
            <Typography variant="body2" color="error">
              {errors.description?.message}
            </Typography>
          )}
          <Stack direction="row" gap={3} my={2}>
            <Box>
              <LocalizationProvider dateAdapter={AdapterDayjs}>
                <Controller
                  name="startDate"
                  control={control}
                  rules={{ required: true }}
                  render={({ field }) => (
                    <DateTimePicker
                      name="startDate"
                      value={dayjs(field.value)}
                      inputRef={field.ref}
                      defaultValue={dayjs()}
                      label="Start Date"
                    />
                  )}
                />
              </LocalizationProvider>
              {errors.startDate && (
                <Typography variant="body2" color="error">
                  {errors.startDate?.message}
                </Typography>
              )}
            </Box>

            <Box>
              <LocalizationProvider dateAdapter={AdapterDayjs}>
                <Controller
                  name="endDate"
                  control={control}
                  rules={{ required: true }}
                  render={({ field }) => (
                    <DateTimePicker
                      name="endDate"
                      value={dayjs(field.value)}
                      inputRef={field.ref}
                      defaultValue={dayjs().add(3, "day")}
                      label="End Date"
                    />
                  )}
                />
              </LocalizationProvider>
              {errors.endDate && (
                <Typography variant="body2" color="error">
                  {errors.endDate?.message}
                </Typography>
              )}
            </Box>
          </Stack>
          <TextField label="Location" name="location" variant="outlined" />
          <Dropzone onDrop={onFileAccept} minSize={1024} maxSize={3072000}>
            {({
              getRootProps,
              getInputProps,
              isDragActive,
              isDragAccept,
              isDragReject,
            }) => (
              <Box
                border="1px solid black"
                sx={{ bgcolor: isDragActive ? pink[200] : "white" }}
              >
                <div {...getRootProps()}>
                  <input {...getInputProps()} />
                  {isDragActive ? (
                    <Typography variant="body2">
                      Drop down the file to upload it...
                    </Typography>
                  ) : (
                    <Typography variant="body2">
                      Drag 'n' drop some files here, or click to select files
                    </Typography>
                  )}
                </div>
              </Box>
            )}
          </Dropzone>

          <Button
            type="submit"
            variant="contained"
            sx={{
              bgcolor: green[400],
              marginTop: 2,
              width: { sm: "60%", md: "30%" },
            }}
          >
            Create Event
          </Button>
        </form>
      </Paper>
    </Box>
  );
}
