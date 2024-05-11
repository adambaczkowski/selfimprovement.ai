import React from "react";

import { Navigate, Outlet } from "react-router-dom";

const NoTokenRoutes = () => {
  const localStorageToken = localStorage.getItem("userToken");

  return !localStorageToken ? <Navigate to="/tasks" replace /> : <Outlet />;
};

export default NoTokenRoutes;
