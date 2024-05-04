import React from "react";
import TextInput from "../../TextInput/TextInput";
import { FormControl,InputLabel } from "@mui/material";
import { useField } from "formik";
import { CustomFormHelperText } from "../FormHelperText";

interface Props {
  label: string;
  name: string;
  type?: string;
  onChange?: (e: any) => void;
}

const FormTextInput = (props: Props) => {
  const { name, label } = props;
  const [field, meta] = useField(name);

  return (
    <FormControl>
      <InputLabel htmlFor={name}>{label}</InputLabel>
      <TextInput id={name} datatestId={name} {...field} {...props} error={meta.touched && !!meta.error} />
      <CustomFormHelperText errorText={meta.error} />
    </FormControl>
  );
};

export default FormTextInput;
