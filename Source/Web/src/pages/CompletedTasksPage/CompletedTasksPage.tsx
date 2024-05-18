import {useState, useEffect } from "react";
import { ItemsGrid } from "../../components/componentsIndex"
import { DailyTask } from './types/DailyTask';

function CompletedTasksPage() {
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
    ]);
  }, []);

  return (
    <div className="background_container">
      <ItemsGrid title={"Completed tasks"} dailyTasks={dailyTasks} />
    </div>
  );
}

export default CompletedTasksPage;
