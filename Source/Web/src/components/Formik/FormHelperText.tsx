import React from "react";
import { FormHelperText, useFormControl } from "@mui/material";

interface Props {
  errorText?: string;
}

export const CustomFormHelperText = ({ errorText }: Props) => {
  const { error } = useFormControl() || {};

  const helperText = React.useMemo(() => {
    if (error) {
      return errorText;
    }

    return errorText;
  }, [error, errorText]);

  return <FormHelperText>{helperText}</FormHelperText>;
};
