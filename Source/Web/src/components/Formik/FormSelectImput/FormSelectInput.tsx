import React, { useState } from "react";
import { Select, MenuItem, FormControl, InputLabel } from "@mui/material";
import { useField } from "formik";

interface Props {
  label: string;
  name: string;
  options: { label: string; value: string | number }[];
  value: number | null | '';
  onChange?: (value: number) => void;
}

const FormSelectInput = (props: Props) => {
  const { name, label, options, value, onChange } = props;
  const [field, meta] = useField(name);
  const [selectedOption, setSelectedOption] = useState(0);

  return (
    <FormControl style={{width: "81%"}}>
      <InputLabel htmlFor={name} >{label}</InputLabel>
      <br />
      <Select
        labelId={`${name}-label`}
        id={name}
        value={selectedOption | 0}
        onChange={e => {setSelectedOption(e.target.value as number)}}
        error={meta.touched && !!meta.error}
      >
        {options.map((option, index) => (
          <MenuItem key={index} value={option.value}>
            {option.label}
          </MenuItem>
        ))}
      </Select>
    </FormControl>
  );
};

export default FormSelectInput;
