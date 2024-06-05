import {useState } from "react";
import { useQuery } from "react-query";
import { useParams } from "react-router-dom";
import { LoadingCircle, GoBackButton  } from "../../components/componentsIndex"
import styles from './GoalPage.module.scss';
import { GoalDetailsDto, GoalTaskDto } from "../../utils/api/goal";
import { ItemsGrid } from "../../components/componentsIndex"
import { fetchGoal } from "../../utils/services/goalService";
import { fetchGoalTasks } from "../../utils/services/goalTaskService";
import { addSpacesBeforeCapitals } from "../../utils/helpers/addSpacesBeforeCapitals";
import dayjs from 'dayjs';

function GoalPage() {
  const { id } = useParams();
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
          <h1>{goal.name}</h1>
          <p className={styles.description}><span>Category: </span>{goal.category}</p>
          <p className={styles.description}><span>Time: </span>
            {dayjs(goal.startDate).format("MM-DD-YYYY")} {"\<----\>"} {dayjs(goal.endDate).format("MM-DD-YYYY")}
          </p>
        </div>
        <div className={styles.goal_description}>
          <p className={styles.description}><span>Specific category: </span>{goal.goalFriendlyName}</p>
          <p className={styles.description}><span>Your advancement: </span>{goal.userAdvancement}</p>
          <p className={styles.description}><span>Your learning type: </span>{goal.learningType}</p>
          <p className={styles.description}><span>Your thoughts: </span>{goal.userInput}</p>
        </div>
      </div>
      <ItemsGrid title={"Goal Tasks"} tasks={tasks} />
    </div>
  );
}

export default GoalPage;