import { Button, ButtonProps } from "@mui/material";
import React from "react";

interface Props extends ButtonProps {
  text: string;
}

const CustomButton = (props: Props) => {
  return <Button {...props}>{props.text}</Button>;
};

export default CustomButton;
