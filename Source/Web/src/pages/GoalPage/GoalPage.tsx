import {useState } from "react";
import { useQuery } from "react-query";
import { useParams } from "react-router-dom";
import { LoadingCircle, GoBackButton  } from "../../components/componentsIndex"
import styles from './GoalPage.module.scss';
import { GoalDetailsDto, GoalTaskDto } from "../../utils/api/goal";
import { ItemsGrid } from "../../components/componentsIndex"
import { fetchGoal } from "../../utils/services/goalService";
import { fetchGoalTasks } from "../../utils/services/goalTaskService";
import dayjs from 'dayjs';

// const exampleTasks: GoalTask[] = [
//   {
//       content: "Set Up Development Environment",
//       estimatedDuration: new Date(2024, 3, 20, 1, 30),
//       isCompleted: false,
//       date: new Date(2024, 3, 20),
//   },
// ];

// const exampleDailyTasks: DailyTask[] = [
//   {
//     weekTitle: "Week 1: January 17-21, 2024",
//     date: "January 17, 2024",
//     tasks: [
//       "Set Up Development Environment",
//       "Install Python and a Code Editor (e.g., VS Code)",
//       "Explore basic features of the chosen code editor Explore basic features of the chosen code editor Explore basic features of the chosen code editor",
//     ],
//     isCompleted: true
//   },
// ];

function GoalPage() {
  const { id } = useParams();
  // const [dailyTasks, setDailyTasks] = useState<DailyTask[]>([]);
  const [tasks, setTasks] = useState<GoalTaskDto[]>([]);
  const [goal, setGoal] = useState<GoalDetailsDto>();

  useQuery({
    queryKey: ["getGoal"],
    queryFn: async () => {
      const goal = await fetchGoal(id || "");
      if (goal != null) {
        setGoal(goal);
      }
      return goal;
    },
    refetchOnWindowFocus: false,
  });

  useQuery({
    queryKey: ["getTasks"],
    queryFn: async () => {
      const tasks = await fetchGoalTasks(id ?? "");
      if (tasks != null) {
        setTasks(tasks);
      }
      return tasks;
    },
    refetchOnWindowFocus: false,
  });

  // useEffect(() => {
  //   return setDailyTasks(
  //     exampleDailyTasks
  //   );
  // }, []);

  if (!goal) {
    return (
      <div className={styles.background_container}>
        <LoadingCircle timeout={10000} errorMessage="Something went wrong" />
      </div>
    );
  }

  if (!tasks) {
    return (
      <div className={styles.background_container}>
        <LoadingCircle timeout={10000} errorMessage="Something went wrong" />
      </div>
    );
  }

  return (
    <div className={styles.background_container}>
      <GoBackButton />
      <div className={styles.goal_item}>
      <div className={styles.goal_header}>
        <h1>{goal.category}</h1>
        <p className={styles.description}><span>Learning type: </span>{goal.learningType}</p>
      </div>
      <div className={styles.goal_description}>
        <p className={styles.description}><span>Time Availability Per Day: </span>{goal.timeAvailabilityPerDay}</p>
        <p className={styles.description}><span>Time Availability Per Week: </span>{goal.timeAvailabilityPerWeek}</p>
        <p className={styles.description}><span>Start date: </span>{dayjs(goal.startDate).format("MM-DD-YYYY")}</p>
        <p className={styles.description}><span>End date: </span>{dayjs(goal.endDate).format("MM-DD-YYYY")}</p>
        <p className={styles.description}><span>User advancement: </span>{goal.userAdvancement}</p>
      </div>
    </div>
      <ItemsGrid title={"Goal Tasks"} tasks={tasks} />
    </div>
  );
}

export default GoalPage;
