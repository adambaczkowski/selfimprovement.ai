import { useState, useEffect } from "react";
import { Select, MenuItem, FormControl, InputLabel, ThemeProvider } from "@mui/material";
import { createTheme } from '@mui/material/styles';
import { useField } from "formik";

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
  options: { label: string; value: string | number }[];
  value: number | null | '';
  onChange?: (value: number) => void;
}

const FormSelectInput = (props: Props) => {
  const { name, label, options } = props;
  const [field, meta, helpers] = useField(name); // Destructure helpers
  const [selectedOption, setSelectedOption] = useState<number | null>(null); // Initialize state

  // Set selected option when form field value changes
  useEffect(() => {
    setSelectedOption(field.value);
  }, [field.value]);

  return (
    <ThemeProvider theme={theme}>
      <FormControl style={{ width: "57%" }}>
        <InputLabel htmlFor={name}>{label}</InputLabel>
        <br />
        <Select
          labelId={`${name}-label`}
          id={name}
          value={selectedOption ?? ''}
          onChange={(e) => {
            setSelectedOption(e.target.value as number);
            helpers.setValue(e.target.value); // Set form field value
          }}
          error={meta.touched && !!meta.error}
        >
          {options.map((option, index) => (
            <MenuItem key={index} value={option.value}>
              {option.label}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
    </ThemeProvider>
  );
};


export default FormSelectInput;
