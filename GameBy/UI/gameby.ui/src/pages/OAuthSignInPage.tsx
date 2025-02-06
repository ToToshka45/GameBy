import { AppProvider } from "@toolpad/core/AppProvider";
import {
  AuthResponse,
  SignInPage,
  type AuthProvider,
} from "@toolpad/core/SignInPage";
import { useTheme } from "@mui/material/styles";

const providers = [
  { id: "github", name: "GitHub" },
  { id: "google", name: "Google" },
  { id: "credentials", name: "Email and Password" },
];

const signIn: (provider: AuthProvider) => void | Promise<AuthResponse> = async (
  provider
) => {
  const promise = new Promise<AuthResponse>((resolve) => {
    setTimeout(() => {
      console.log(`Sign in with ${provider.id}`);
      resolve({
        error: "Not implemented yet. Please enter your Email and Password.",
      });
    }, 500);
  });
  return promise;
};

export default function OAuthSignInPage() {
  const theme = useTheme();
  return (
    // preview-start
    <AppProvider theme={theme}>
      <SignInPage
        signIn={signIn}
        providers={providers}
        slotProps={{ emailField: { autoFocus: false } }}
      />
    </AppProvider>
    // preview-end
  );
}
