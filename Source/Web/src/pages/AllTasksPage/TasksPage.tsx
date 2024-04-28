import React, {useState, useEffect } from "react";
import { Paper, Typography } from '@mui/material';
import { TaskItem } from "../../components/componentsIndex"
import styles from './TasksPage.module.scss';
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
    <div className={styles.task_container}>
      <h1 className="section_header">{"All tasks"}</h1>
      <div className={styles.tasks_grid}>
        {dailyTasks.map((dailyTask) => (
          dailyTask.tasks.map((task) => (
            <TaskItem
              // key={task.id}
              title={task}
              description={task}
              date={dailyTask.date}
              isCompleted={dailyTask.isCompleted}
            />
          ))))
        }
      </div>
    </div>
  );
}

export default TasksPage;
