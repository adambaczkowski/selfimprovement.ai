import { MenuItem, FormControl, InputLabel, ThemeProvider, Select, SelectChangeEvent  } from "@mui/material";
import { createTheme } from "@mui/material/styles";
import { useField, useFormikContext } from "formik";

const theme = createTheme({
  palette: {
    primary: {
      main: "#7e68ba",
    },
  },
});

interface Props {
  label: string;
  name: string;
  options: { label: string; value: string | number }[];
}

const FormSelectInput = (props: Props) => {
  const { name, label, options } = props;
  const [field, meta] = useField(name);
  const { setFieldValue } = useFormikContext(); // Get setFieldValue from Formik context

  const handleChange = (event: SelectChangeEvent) => {
    const value = event.target.value as string | number;
    setFieldValue(name, value); // Update Formik's state
  };

  return (
    <ThemeProvider theme={theme}>
      <FormControl style={{ width: "16rem" }} variant="outlined">
        <InputLabel htmlFor={name}>{label}</InputLabel>
        <Select
          labelId={`${name}-label`}
          id={name}
          value={field.value}
          label={label}
          onChange={handleChange}
          error={meta.touched && !!meta.error}
        >
          {options.map((option) => (
            <MenuItem key={option.value} value={option.value}>
              {option.label}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
    </ThemeProvider>
  );
};

export default FormSelectInput;
