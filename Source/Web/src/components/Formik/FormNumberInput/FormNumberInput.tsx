import TextInput from "../../TextInput/TextInput";
import { FormControl, InputLabel, ThemeProvider } from "@mui/material";
import { useField } from "formik";
import { CustomFormHelperText } from "../FormHelperText";
import { createTheme } from "@mui/material/styles";

const theme = createTheme({
  palette: {
    primary: {
      main: "#7e68ba", // Your desired color for the underline
    },
  },
});

interface Props {
  label: string;
  name: string;
  type?: string;
  onChange?: (e: any) => void;
}

const FormNumberInput = (props: Props) => {
  const { name, label } = props;
  const [field, meta] = useField(name);

  return (
    <ThemeProvider theme={theme}>
      <FormControl style={{ width: "25rem" }}>
        <InputLabel htmlFor={name}>{label}</InputLabel>
        <TextInput id={name} placeholderText="eg. 4" datatestId={name} {...field} {...props} error={meta.touched && !!meta.error} />
        <CustomFormHelperText errorText={meta.error} />
      </FormControl>
    </ThemeProvider>
  );
};

export default FormNumberInput;
