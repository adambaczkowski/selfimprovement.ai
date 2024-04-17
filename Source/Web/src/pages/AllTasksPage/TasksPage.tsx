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
      }
    ]);
  }, []);

  return (
    <div className={styles.taskStyled}>
      <h1>{"All tasks"}</h1>

      {/* <button className="btn-rounded" onClick={openModal}>
        {plus}
      </button> */}

      <div className={styles.tasks}>
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
        {/* <button className="create-task" onClick={openModal}> */}
        {/* <button className={styles.create_task}>
          {"add"}
          Add New Task
        </button> */}
      </div>
    </div>
  );
}

export default TasksPage;
