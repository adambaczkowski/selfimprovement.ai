import TextBoxInput from "../../TextBoxInput/TextBoxInput";
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

const FormTextBoxInput = (props: Props) => {
  const { name, label } = props;
  const [field, meta] = useField(name);

  return (
    <ThemeProvider theme={theme}>
      <FormControl style={{ width: "25rem" }}>
        <InputLabel htmlFor={name}>{label}</InputLabel>
        <TextBoxInput 
          id={name}
          datatestId={name} 
          placeholder={props.placeholderText} 
          {...field} {...props} 
          error={meta.touched && !!meta.error}
          multiline
          maxRows={4}
        />
        <CustomFormHelperText errorText={meta.error} />
      </FormControl>
    </ThemeProvider>
  );
};

export default FormTextBoxInput;