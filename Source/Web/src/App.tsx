import React from "react";
import { BrowserRouter } from "react-router-dom";
import "./App.scss";
import Routes from "./routes";
import axios from "axios";
import { ErrorBoundary } from "./components/componentsIndex";
import { QueryClient, QueryClientProvider } from "react-query";

function App() {
  const queryClient = new QueryClient();
  if (localStorage.getItem("userToken") != null) {
    const userToken = JSON.parse(localStorage.getItem("userToken") || '""');

    axios.interceptors.request.use(
      (config) => {
        config.headers.authorization = `Bearer ${userToken} `;
        return config;
      },
      (error) => {
        return Promise.reject(error);
      }
    );
  }

  return (
    <QueryClientProvider client={queryClient}>
      <Routes />
    </QueryClientProvider>
  );
}

export default App;
