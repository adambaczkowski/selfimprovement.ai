import React, { useState, useEffect } from "react";
import { CircularProgress, Typography } from '@mui/material';

interface Props {
  timeout: number;
  errorMessage: string;
}

const LoadingWithError: React.FC<Props> = ({ timeout, errorMessage }) => {
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
      <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
        <CircularProgress />
      </div>
    );
  }

  if (error) {
    return (
      <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
        <Typography>{errorMessage}</Typography>
      </div>
    );
  }

  return null;
};

export default LoadingWithError;
