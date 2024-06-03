import {useState } from "react";
import { ItemsGrid } from "../../components/componentsIndex"
import { fetchTasks } from "../../utils/services/goalTaskService";
import { GoalTaskDto } from "../../utils/api/goal";
import { useQuery } from "react-query";
import styles from './TasksPage.module.scss';

function TasksPage() {
  const [tasks, setTasks] = useState<GoalTaskDto[]>([]);
  // const [dailyTasks, setDailyTasks] = useState<DailyTask[]>([]);

  // useEffect(() => {
  //   setDailyTasks([
  //     {
  //       weekTitle: "Week 1: January 17-21, 2024",
  //       date: "January 17, 2024",
  //       tasks: [
  //         "Set Up Development Environment",
  //         "Install Python and a Code Editor (e.g., VS Code)",
  //         "Explore basic features of the chosen code editor Explore basic features of the chosen code editor Explore basic features of the chosen code editor",
  //       ],
  //       isCompleted: true
  //     },
  //   ]);
  // }, []);

  useQuery({
    queryKey: ["getTasks"],
    queryFn: async () => {
      const tasks = await fetchTasks();
      if (tasks != null) {
        setTasks(tasks);
      }
      return tasks;
    },
    refetchOnWindowFocus: false,
  });

  return (
    <div className={styles.background_container}>
      <ItemsGrid title={"All tasks"} tasks={tasks} />
    </div>
  );
}

export default TasksPage;
