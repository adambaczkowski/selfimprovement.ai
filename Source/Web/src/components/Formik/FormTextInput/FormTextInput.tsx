import TextInput from "../../TextInput/TextInput";
import { FormControl, InputLabel, ThemeProvider } from "@mui/material";
import { useField } from "formik";
import { CustomFormHelperText } from "../FormHelperText";
import { createTheme } from '@mui/material/styles';

const theme = createTheme({
  palette: {
    primary: {
      main: '#7e68ba', // Your desired color for the underline
    },
  },
});

interface Props {
  label: string;
  name: string;
  placeholderText?: string;
  type?: string;
  onChange?: (e: any) => void;
}

const FormTextInput = (props: Props) => {
  const { name, label } = props;
  const [field, meta] = useField(name);

  return (
    <ThemeProvider theme={theme}>
      <FormControl style={{ width: "16rem" }}>
        <InputLabel htmlFor={name}>{label}</InputLabel>
        <TextInput id={name} datatestId={name} placeholder={props.placeholderText} {...field} {...props} error={meta.touched && !!meta.error} />
        <CustomFormHelperText errorText={meta.error} />
      </FormControl>
    </ThemeProvider>
  );
};

export default FormTextInput;
