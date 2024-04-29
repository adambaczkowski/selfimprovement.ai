import React from "react";
import { createBrowserRouter, RouterProvider, Outlet } from "react-router-dom";
import SignUpPage from "./pages/SignUpPage/SignUpPage";
import SignInPage from "./pages/SignInPage/SignInPage";
import TaskPage from "./pages/TaskPage/TaskPage";
import TasksPage from "./pages/AllTasksPage/TasksPage";
import ProfileCreationPage from "./pages/ProfileCreationPage/ProfileCreationPage";
import ResendEmailConfirmationPage from "./pages/ResendEmailConfirmationPage/ResendEmailConfirmationPage";
import { RequestPasswordResetPage } from "./pages/RequestPasswordResetPage/RequestPasswordResetPage";
import ConfirmEmailPage from "./pages/ConfirmEmailPage/ConfirmEmailPage";
import PasswordResetPage from "./pages/PasswordResetPage/PasswordResetPage";
import { Sidebar } from "./components/componentsIndex";
import "./App.scss";
type Props = {};

const AppLayout = () => {
  return(
    <div className="app-container">
      {/* {userToken && <Sidebar />} */}
      <Sidebar />
      <div className="content-container">
        <Outlet />
      </div>
    </div>
  )
};

const Routes = ({}: Props) => {
  const router = createBrowserRouter([
    {
      element: <AppLayout />,
      children: [
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
        {
          path: "/task",
          element: <TaskPage />,
        },
        {
          path: "/profileCreation",
          element: <ProfileCreationPage />,
        },
        {
          path: "/tasks",
          element: <TasksPage />,
        }
      ]
    }
  ]);

  return <RouterProvider router={router} />;
};

export default Routes;
