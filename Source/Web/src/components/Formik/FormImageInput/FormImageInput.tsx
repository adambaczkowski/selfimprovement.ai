import React, { useState } from 'react';
import { useField, useFormikContext } from 'formik';
import { Button, FormControl, InputLabel, ThemeProvider, createTheme, Typography } from '@mui/material';

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
}

const FormImageUpload = (props: Props) => {
  const { label, name } = props;
  const [field, meta] = useField(name);
  const { setFieldValue } = useFormikContext();
  const [preview, setPreview] = useState<string | null>(null);

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.currentTarget.files?.[0];
    if (file) {
      const reader = new FileReader();
      reader.onloadend = () => {
        const base64 = reader.result?.toString().split(',')[1];
        setFieldValue(name, base64); // Save base64 string in form state
        setPreview(reader.result as string); // Set preview to base64 string with data URL prefix
      };
      reader.readAsDataURL(file);
    }
  };

  return (
    <ThemeProvider theme={theme}>
      <FormControl style={{ width: '16rem' }} variant="outlined">
        <InputLabel shrink htmlFor={name}>{label}</InputLabel>
        <input
          id={name}
          name={name}
          type="file"
          accept="image/*"
          onChange={handleFileChange}
          style={{ display: 'none' }}
        />
        <label htmlFor={name} style={{ marginTop: '0.8rem' }}>
          <Button
            variant="contained"
            component="span"
            color="primary"
            fullWidth
          >
            Upload {label}
          </Button>
        </label>
        {meta.touched && meta.error && (
          <Typography color="error" variant="caption">
            {meta.error}
          </Typography>
        )}
        {preview && (
          <div style={{ marginTop: '1rem' }}>
            <img src={preview} alt="preview" style={{ width: '100%' }} />
          </div>
        )}
      </FormControl>
    </ThemeProvider>
  );
};

export default FormImageUpload;
