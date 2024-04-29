import { list, check, home, calendar, todo, goals } from "./icons";

const menu = [
  {
    id: 1,
    title: "all tasks",
    icon: home,
    link: "/tasks",
  },
  {
    id: 2,
    title: "completed",
    icon: check,
    link: "/completed",
  },
  {
    id: 3,
    title: "calendar",
    icon: calendar,
    link: "/calendar",
  },
  {
    id: 4,
    title: "goals",
    icon: goals,
    link: "/goals",
  },
  {
    id: 5,
    title: "new goal",
    icon: todo,
    link: "/newGoal",
  },
];

export default menu;