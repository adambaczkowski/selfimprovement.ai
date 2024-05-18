import { useState, useEffect } from "react";
import { MenuItem, FormControl, InputLabel, ThemeProvider } from "@mui/material";
import Select, { SelectChangeEvent } from '@mui/material/Select';
import { createTheme } from "@mui/material/styles";
import { useField } from "formik";

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
  options: { label: string; value: string | number }[];
  value?: number | null | "";
  onChange?: (value: number) => void;
}

const FormSelectInput = (props: Props) => {
  const { name, label, options } = props;
  const [field, meta, helpers] = useField(name);
  const [selectedOption, setSelectedOption] = useState<number | null>(null); // Initialize state

  const handleChange = (event: SelectChangeEvent) => {
    setSelectedOption(event.target.value as unknown as number);
  };

  return (
    <ThemeProvider theme={theme}>
      <FormControl style={{ width: "16rem" }}>
        <InputLabel htmlFor={name}>{label}</InputLabel>
        <Select
          labelId={`${name}-label`}
          id={name}
          value={selectedOption?.toString()} // Convert selectedOption to a string
          label={label}
          onChange={handleChange}
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
