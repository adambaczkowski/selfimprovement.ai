import { createBrowserRouter, RouterProvider, Outlet } from "react-router-dom";
import SignUpPage from "./pages/SignUpPage/SignUpPage";
import SignInPage from "./pages/SignInPage/SignInPage";
import TaskPage from "./pages/TaskPage/TaskPage";
import CalendarPage from "./pages/CalendarPage/CalendarPage";
import TasksPage from "./pages/TasksPage/TasksPage";
import HomePage from "./pages/HomePage/HomePage";
import GoalsPage from "./pages/GoalsPage/GoalsPage";
import GoalPage from "./pages/GoalPage/GoalPage";
import NewGoalPage from "./pages/NewGoalPage/NewGoalPage";
import ProfileCreationPage from "./pages/ProfileCreationPage/ProfileCreationPage";
import ResendEmailConfirmationPage from "./pages/ResendEmailConfirmationPage/ResendEmailConfirmationPage";
import { RequestPasswordResetPage } from "./pages/RequestPasswordResetPage/RequestPasswordResetPage";
import ConfirmEmailPage from "./pages/ConfirmEmailPage/ConfirmEmailPage";
import PasswordResetPage from "./pages/PasswordResetPage/PasswordResetPage";
import { Sidebar, ErrorBoundary } from "./components/componentsIndex";
import "./App.scss";
import ProtectedRoutes from "./ProtectedRoutes";
import NoTokenRoutes from "./NoTokenRoutes";
type Props = {};

const AppLayout = () => {
  return (
    <div className="app-container">
      <Sidebar />
      <div className="content-container">
        <ProtectedRoutes />
      </div>
    </div>
  );
};

const Routes = ({}: Props) => {
  const router = createBrowserRouter([
    {
      element: <NoTokenRoutes />,
      children: [
        {
          path: "/signIn",
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
      ],
    },
    {
      element: <AppLayout />,
      children: [
        {
          path: "/",
          element: <HomePage />,
        },
        {
          path: "/task/:id",
          element: <TaskPage />,
        },
        {
          path: "/tasks",
          element: <TasksPage />,
        },
        {
          path: "/calendar",
          element: <CalendarPage />,
        },
        {
          path: "/goals",
          element: <GoalsPage />,
        },
        {
          path: "/goal/:id",
          element: <GoalPage />,
        },
        {
          path: "/newGoal",
          element: <NewGoalPage />,
        },
        {
          path: "/profileCreation/:mode",
          element: <ProfileCreationPage />,
        },
      ],
    },
  ]);

  return <RouterProvider router={router} />;
};

export default Routes;
