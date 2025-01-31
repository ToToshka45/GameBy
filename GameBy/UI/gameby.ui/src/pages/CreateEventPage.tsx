import {
  Box,
  Button,
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
import { green, orange, pink } from "@mui/material/colors";
import { zodResolver } from "@hookform/resolvers/zod";
import EventCreationFields, { eventCreationSchema } from "../interfaces/Event";

// TODO: Add saving of the typed values to the Local Storage

export default function CreateEventPage() {
  const {
    control,
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<EventCreationFields>({
    resolver: zodResolver(eventCreationSchema),
  });

  const onFileAccept = useCallback((acceptedFiles: any) => {
    console.log(acceptedFiles);
  }, []);

  const onSubmit: SubmitHandler<EventCreationFields> = (data) => {
    console.log(data);
  };

  return (
    <Box
      mx="3%"
      position="relative"
      maxHeight="100%"
      height="94.5vh"
      bgcolor={orange[300]}
    >
      <Typography variant="h5" pt={2} pl={3} gutterBottom>
        Create Event
      </Typography>
      <Paper
        elevation={2}
        sx={{
          px: { xs: "4%", md: "2%" },
          py: { xs: "4%", md: "1%" },
        }}
      >
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
          {/* TODO: add a real-time map API*/}
          <Dropzone onDrop={onFileAccept} minSize={1024} maxSize={3072000}>
            {({ getRootProps, getInputProps, isDragActive }) => (
              <Box
                border="1px solid gray"
                textAlign="center"
                py={5}
                sx={{ bgcolor: isDragActive ? pink[100] : "white" }}
              >
                <div {...getRootProps()}>
                  <input {...getInputProps()} />
                  {isDragActive ? (
                    <Typography
                      sx={{
                        fontSize: { xs: 11, sm: 14, lg: 18 },
                        color: "white",
                      }}
                    >
                      Drop down the file to upload it...
                    </Typography>
                  ) : (
                    <Typography
                      sx={{
                        fontSize: { xs: 11, md: 14, lg: 18 },
                        color: "lightcoral",
                      }}
                    >
                      Drag 'n' drop some files here, or click to select files
                    </Typography>
                  )}
                </div>
              </Box>
            )}
          </Dropzone>
          <Box>
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
          </Box>
        </form>
      </Paper>
    </Box>
  );
}
