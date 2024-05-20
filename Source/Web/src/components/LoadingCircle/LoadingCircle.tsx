import React, { useState, useEffect } from "react";
import { CircularProgress, Typography } from '@mui/material';
import styles from "./LoadingCircle.module.scss";
interface Props {
  timeout: number;
  heading?: string;
  errorMessage: string;
}

const LoadingWithError: React.FC<Props> = ({ timeout, heading, errorMessage }) => {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);

  useEffect(() => {
    const timeoutId = setTimeout(() => {
      setError(true);
    }, timeout); // Show error after timeout

    return () => clearTimeout(timeoutId);
  }, []);

  useEffect(() => {
    if (!error) {
      const loadingTimeout = setTimeout(() => {
        setLoading(false);
      }, timeout); // Hide CircularProgress after timeout

      return () => clearTimeout(loadingTimeout);
    }
  }, [error]);

  if (loading) {
    return (
      <div className={styles.loading_container}>
        <CircularProgress color={"secondary"} />
        <p className={styles.loading_heading}>{heading}</p>
      </div>
    );
  }

  if (error) {
    return (
      <div className={styles.loading_container}>
        <Typography>{errorMessage}</Typography>
      </div>
    );
  }

  return null;
};

export default LoadingWithError;
