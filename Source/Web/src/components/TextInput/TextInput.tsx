import { OutlinedInputProps, OutlinedInput, ThemeProvider } from "@mui/material";
import { createTheme } from "@mui/material/styles";

const theme = createTheme({
  palette: {
    primary: {
      main: "#7e68ba", // Your desired color for the underline
    },
  },
});

interface Props extends OutlinedInputProps {
  datatestId: string;
}

const TextInput = (props: Props) => {
  return (
    <ThemeProvider theme={theme}>
      <OutlinedInput value={props.value} data-testid={props.datatestId} placeholder="test" {...props} />
    </ThemeProvider>
  );
};

export default TextInput;
