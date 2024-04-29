import React from "react";
import { BrowserRouter } from 'react-router-dom';
import "./App.scss";
import Routes from "./routes";
import axios from "axios";
import Sidebar from "./components/Sidebar/Sidebar";

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
      <Routes />
    </>
  );
}

export default App;
