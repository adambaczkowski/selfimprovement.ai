import { 
  signOut,
  check, 
  squareCheckSolid, 
  squareCheckRegular,
  home,
  calendarSolid, 
  calendarRegular, 
  todo, 
  goals,
  upload
 } from "./icons";

const menu = [
  {
    id: 1,
    title: "home",
    icon: home,
    link: "/",
  },
  {
    id: 2,
    title: "all tasks",
    icon: check,
    link: "/tasks",
  },
  {
    id: 3,
    title: "calendar",
    icon: calendarSolid,
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

export { signOut as signOutIcon }
export { squareCheckSolid as squareCheckSolidIcon }
export { squareCheckRegular as squareCheckRegularIcon }
export { calendarSolid as calendarSolid }
export { calendarRegular as calendarRegular }
export { menu };
export { upload };