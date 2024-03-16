import React from "react";
import { Route, createBrowserRouter, createRoutesFromElements, RouterProvider } from "react-router-dom";
import SignUpPage from "./pages/SignUpPage/SignUpPage";
import SignInPage from "./pages/SignInPage/SignInPage";
import ResendEmailConfirmationPage from "./pages/ResendEmailConfirmationPage/ResendEmailConfirmationPage";
import { RequestPasswordResetPage } from "./pages/RequestPasswordResetPage/RequestPasswordResetPage";
import ConfirmEmailPage from "./pages/ConfirmEmailPage/ConfirmEmailPage";
import PasswordResetPage from "./pages/PasswordResetPage/PasswordResetPage";

type Props = {};

const Routes = ({}: Props) => {
  const router = createBrowserRouter([
    {
      path: "/",
      element: <SignInPage />,
    },
    {
      path: "/signUp",
      element: <SignUpPage />,
    },
    {
      path: "/resendEmailConfirm",
      element: <ResendEmailConfirmationPage />,
    },
    {
      path: "/requestPasswordReset",
      element: <RequestPasswordResetPage />,
    },
    {
      path: "/confirmEmail",
      element: <ConfirmEmailPage />,
    },
    {
      path: "/resetPassword",
      element: <PasswordResetPage />,
    },
  ]);

  return <RouterProvider router={router} />;
};

export default Routes;
