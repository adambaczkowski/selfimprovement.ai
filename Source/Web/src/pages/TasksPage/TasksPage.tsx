import { useState } from "react";
import { ItemsGrid } from "../../components/componentsIndex"
import { fetchTasks } from "../../utils/services/goalTaskService";
import { GoalTaskDto } from "../../utils/api/goal";
import { useQuery } from "react-query";
import styles from './TasksPage.module.scss';

function TasksPage() {
  const [tasks, setTasks] = useState<GoalTaskDto[]>([]);

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
