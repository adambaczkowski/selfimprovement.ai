import {useState } from "react";
import { fetchTasks } from "../../utils/services/goalTaskService";
import { GoalTaskDto } from "../../utils/api/goal";
import { useQuery } from "react-query";
import { ItemsGrid } from "../../components/componentsIndex"

function CompletedTasksPage() {
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
      const response = await fetchTasks();
      const tasks = response.data;
      if (tasks != null) {
        setTasks(tasks);
      }
      return response.data;
    },
    refetchOnWindowFocus: false,
  });

  return (
    <div className="background_container">
      <ItemsGrid title={"Completed tasks"} tasks={tasks} />
    </div>
  );
}

export default CompletedTasksPage;
