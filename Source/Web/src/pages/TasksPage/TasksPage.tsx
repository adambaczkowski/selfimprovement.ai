import {useState, useEffect } from "react";
import { ItemsGrid } from "../../components/componentsIndex"
import { DailyTask } from './types/DailyTask';


function TasksPage() {
  const [dailyTasks, setDailyTasks] = useState<DailyTask[]>([]);

  useEffect(() => {
    setDailyTasks([
      {
        weekTitle: "Week 1: January 17-21, 2024",
        date: "January 17, 2024",
        tasks: [
          "Set Up Development Environment",
          "Install Python and a Code Editor (e.g., VS Code)",
          "Explore basic features of the chosen code editor Explore basic features of the chosen code editor Explore basic features of the chosen code editor",
        ],
        isCompleted: true
      },
      {
        weekTitle: "Week 2: January 17-21, 2024",
        date: "January 20, 2024",
        tasks: [
          "Set Up Development Environment",
          "Install Python and a Code Editor (e.g., VS Code)",
          "Explore basic features of the chosen code editor Explore basic features of the chosen code editor Explore basic features of the chosen code editor",
        ],
        isCompleted: false
      },
      {
        weekTitle: "Week 2: January 17-21, 2024",
        date: "January 20, 2024",
        tasks: [
          "Set Up Development Environment",
          "Install Python and a Code Editor (e.g., VS Code)",
          "Explore basic features of the chosen code editor Explore basic features of the chosen code editor Explore basic features of the chosen code editor",
        ],
        isCompleted: false
      },
      {
        weekTitle: "Week 2: January 17-21, 2024",
        date: "January 20, 2024",
        tasks: [
          "Set Up Development Environment",
          "Install Python and a Code Editor (e.g., VS Code)",
          "Explore basic features of the chosen code editor Explore basic features of the chosen code editor Explore basic features of the chosen code editor",
        ],
        isCompleted: false
      }
    ]);
  }, []);

  return (
    <ItemsGrid title={"All tasks"} dailyTasks={dailyTasks} />
  );
}

export default TasksPage;
