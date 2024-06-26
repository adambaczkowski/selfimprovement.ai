import React, { useState, useCallback } from 'react';
import { useField, useFormikContext } from 'formik';
import { Button, FormControl, InputLabel, ThemeProvider, createTheme, Typography } from '@mui/material';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { IconProp } from '@fortawesome/fontawesome-svg-core';
import { upload } from "../../../utils/enums/sidebarMenu";
import styles from './FormImageUpload.module.scss';
import { useDropzone } from 'react-dropzone';

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

  const onDrop = useCallback((acceptedFiles: File[]) => {
    const file = acceptedFiles[0];
    if (file) {
      const reader = new FileReader();
      reader.onloadend = () => {
        const base64 = reader.result?.toString().split(',')[1];
        setFieldValue(name, base64); // Save base64 string in form state
        setPreview(reader.result as string); // Set preview to base64 string with data URL prefix
      };
      reader.readAsDataURL(file);
    }
  }, [setFieldValue, name]);

  const { getRootProps, getInputProps } = useDropzone({onDrop});

  return (
    <ThemeProvider theme={theme}>
      <FormControl style={{ width: '25rem' }} variant="outlined">
        <div {...getRootProps()} >
          <input {...getInputProps()} />
          <span className={styles.dropzone}>
            <span className={styles.dropzone_inside}>
              <svg className={styles.dropzone_icon}>
                <FontAwesomeIcon icon={upload as IconProp} />
              </svg>
              <span className={styles.dropzone_text}>
                Drop profile image
              </span>
            </span>
            {preview && (
              <div style={{ marginTop: '1rem' }}>
                <img src={preview} alt="preview" className={styles.image} />
              </div>
            )}
          </span>
        </div>
        {meta.touched && meta.error && (
          <Typography color="error" variant="caption">
            {meta.error}
          </Typography>
        )}
      </FormControl>
    </ThemeProvider>
  );
};

export default FormImageUpload;
