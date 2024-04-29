import React from "react";
import { BrowserRouter } from 'react-router-dom';
import "./App.scss";
import Routes from "./routes";
import axios from "axios";
import { ErrorBoundary } from "./components/componentsIndex";

function App() {
  if (localStorage.getItem("userToken") != null) {
    const userToken = JSON.parse(localStorage.getItem("userToken") || '""');

    axios.interceptors.request.use(
      (config) => {
        config.headers.authorization = `Bearer ${userToken.token} `;
        return config;
      },
      (error) => {
        return Promise.reject(error);
      }
    );
  }

  return (
    <>
      <ErrorBoundary>
        <Routes />
      </ErrorBoundary>
    </>
  );
}

export default App;
