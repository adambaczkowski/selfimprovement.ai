import { list, check, todo, home, calendar } from "./icons";

const menu = [
  {
    id: 1,
    title: "all tasks",
    icon: home,
    link: "/tasks",
  },
  {
    id: 2,
    title: "goals",
    icon: list,
    link: "/goals",
  },
  {
    id: 3,
    title: "calendar",
    icon: calendar,
    link: "/calendar",
  },
  {
    id: 4,
    title: "completed",
    icon: check,
    link: "/completed",
  },
  {
    id: 4,
    title: "new goal",
    icon: todo,
    link: "/newGoal",
  },
];

export default menu;