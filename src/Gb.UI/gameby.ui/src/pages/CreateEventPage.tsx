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
import { FileWithPath, useDropzone } from "react-dropzone";
import { useCallback, useState } from "react";
import { green, orange, pink } from "@mui/material/colors";
import { zodResolver } from "@hookform/resolvers/zod";
import EventCreationForm, {
  createDefaultEvent,
  eventCreationSchema,
} from "../schemas/EventCreationForm";

export default function CreateEventPage() {
  const [image, setImage] = useState<FileWithPath | undefined>();
  const [imgPreview, setImgPreview] = useState<string | ArrayBuffer | null>(
    null
  );

  const {
    control,
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<EventCreationForm>({
    resolver: zodResolver(eventCreationSchema),
    defaultValues: createDefaultEvent(),
  });

  const onDrop = useCallback((acceptedFiles: FileWithPath[]) => {
    const imageUploaded = acceptedFiles[0];
    if (imageUploaded === null) {
      console.log("No image received.");
      return;
    }

    setImage(imageUploaded);
    console.log(URL.createObjectURL(imageUploaded));
    setImgPreview(URL.createObjectURL(imageUploaded));

    // const fileReader = new FileReader();

    // // here we register an event callback which pops off on the onload
    // fileReader.onloadend = () => {
    //   // and it sets the img preview to the URL result, which then can be used in the img tag
    //   setImgPreview(fileReader.result);
    // };

    // // here we allow fileReader to read the set image as URL
    // fileReader.readAsDataURL(imageUploaded);
  }, []);

  const { getRootProps, getInputProps, isDragActive } = useDropzone({
    onDrop,
  });

  const onSubmit: SubmitHandler<EventCreationForm> = (data) => {
    console.log(data);
  };

  return (
    <Box
      mx="3%"
      bgcolor={orange[300]}
      sx={{
        height: "calc(100vh - 50px)",
      }}
    >
      <Typography
        pt={1.5}
        variant="body2"
        pl={3}
        gutterBottom
        sx={{ fontSize: { xs: 16, md: 24 } }}
      >
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
            sx={{ width: { sx: "100%", md: "50%" } }}
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

          <Stack direction="row" gap={1}>
            <Box sx={{ width: { sx: "100%", md: "40%" } }}>
              <TextField
                {...register("location")}
                label="Location"
                name="location"
                variant="outlined"
                fullWidth
              />
              {errors.location && (
                <Typography variant="body2" color="error">
                  {errors.location?.message}
                </Typography>
              )}
            </Box>

            <Box width="250px">
              <TextField
                {...register("minParticipants")}
                label="Minimal amount of Participants"
                name="minParticipants"
                variant="outlined"
                fullWidth
              />
              {errors.minParticipants && (
                <Typography variant="body2" color="error">
                  {errors.minParticipants?.message}
                </Typography>
              )}
            </Box>

            <Box width="250px">
              <TextField
                {...register("maxParticipants")}
                label="Maximum amount of Participants"
                name="maxParticipants"
                variant="outlined"
                fullWidth
              />
              {errors.maxParticipants && (
                <Typography variant="body2" color="error">
                  {errors.maxParticipants?.message}
                </Typography>
              )}
            </Box>
          </Stack>

          <Stack direction="row" gap={10} my={2}>
            <Box>
              <LocalizationProvider dateAdapter={AdapterDayjs}>
                <Controller
                  name="startDate"
                  control={control}
                  render={({ field }) => (
                    <DateTimePicker
                      value={field.value ? dayjs(field.value) : null}
                      onChange={(date) => {
                        field.onChange(date?.toDate());
                      }}
                      inputRef={field.ref}
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
                  render={({ field }) => (
                    <DateTimePicker
                      value={field.value ? dayjs(field.value) : null}
                      onChange={(date) => {
                        field.onChange(date?.toDate());
                      }}
                      inputRef={field.ref}
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

          {/* TODO: add a real-time map API*/}

          <Box
            border="0.5px solid gray"
            textAlign="center"
            alignSelf="center"
            py={5}
            sx={{
              bgcolor: isDragActive ? pink[100] : "white",
              width: { xs: "100%", md: "50%" },
            }}
          >
            <div {...getRootProps()}>
              <input
                {...getInputProps({
                  accept: "image/*",
                  type: "file",
                  name: "image",
                })}
              />
              {isDragActive ? (
                <Typography
                  sx={{
                    fontSize: { xs: 14, md: 16 },
                    color: "white",
                  }}
                >
                  Drop down the file to upload it...
                </Typography>
              ) : (
                <Typography
                  sx={{
                    fontSize: { xs: 14, md: 16 },
                    color: "lightcoral",
                  }}
                >
                  Drag 'n' drop some files here, or click to select files
                </Typography>
              )}
            </div>
          </Box>
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
        {image !== null && <img src={imgPreview as string} />}
      </Paper>
    </Box>
  );
}
