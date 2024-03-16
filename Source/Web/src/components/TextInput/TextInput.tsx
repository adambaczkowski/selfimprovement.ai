import { OutlinedInputProps, OutlinedInput } from "@mui/material";
import React from "react";
import styles from "./TextInput.module.scss";

interface Props extends OutlinedInputProps {
  datatestId: string;
}

const TextInput = (props: Props) => {
  return <OutlinedInput value={props.value} data-testid={props.datatestId} placeholder="test" {...props} />;
};

export default TextInput;
