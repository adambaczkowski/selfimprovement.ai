import {useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { useQuery } from "react-query";
import { LoadingCircle, GoBackButton, SmallerGoalItem } from "../../components/componentsIndex"
import { GoalTaskDto, GoalDetailsDto } from "../../utils/api/goal";
import { fetchTask, completeTask } from "../../utils/services/goalTaskService";
import { fetchGoal } from "../../utils/services/goalService";
import styles from './TaskPage.module.scss';
import dayjs from 'dayjs';

type Props = {};

function TaskPage({}: Props) {
  const { id } = useParams();
  const [isLoading, setIsLoading] = useState(false);
  const [isProfileCreationSuccess, setIsProfileCreationSuccess] = useState<boolean>(false);
  const [task, setTask] = useState<GoalTaskDto>();
  const [goal, setGoal] = useState<GoalDetailsDto>();

  useQuery({
    queryKey: ["getTask"],
    queryFn: async () => {
      const task = await fetchTask(id || "");
      if (task != null) {
        setTask(task);
        const goal = await fetchGoal(task?.goalId || "");
        if (goal != null) {
          setGoal(goal);
        }
      }
      return task;
    },
    refetchOnWindowFocus: false,
  });

  useQuery({
    queryKey: ["getGoal"],
    queryFn: async () => {
      const goal = await fetchGoal(task?.goalId || "");
      if (goal != null) {
        setGoal(goal);
      }
      return goal;
    },
    refetchOnWindowFocus: false,
  });

  const updateTask = async () => {
    setIsLoading(true);
    try {
      await completeTask(task?.id || ""); 
      setIsProfileCreationSuccess(true);
      window.location.reload();
    } catch (error) {
      console.error('Error editing profile:', error);
    } finally {
      setIsLoading(false);
    }
  };
  
  if (!task) {
    return (
      <div className={styles.background_container}>
        <LoadingCircle timeout={10000} errorMessage="Something went wrong" />
      </div>
    );
  }

  return (
    <div className={styles.background_container}>
      <GoBackButton />
      <div className={styles.task_item}>
        <div className={styles.task_header}>
          <h1>{task.title}</h1>
          <p className={styles.task_date}><span>Date: </span>{dayjs(task.date).format("MM-DD-YYYY")}</p>
        </div>
        <div className={styles.task_description}>
          <p className={styles.description}><span>Description: </span>{task.content}</p>
          {task?.isCompleted ? (
          <button 
            className={styles.complete_button}
          >
            Completed
          </button>
        ) : (
          <button
            className={styles.incomplete_button}
            onClick={() => {
              console.log("completed");
              updateTask();
            }}
          >
            Incomplete
          </button>
        )}
        </div>
      </div>
      <p className={styles.go_to_goals}>Go to goals page <span>↓</span></p>
      <SmallerGoalItem
        key={goal?.id}
        id={goal?.id || ""}
        name={goal?.name || ""}
        category={goal?.category || ""}
        timeAvailability={goal?.timeAvailabilityPerDay || ""}
        startDate={goal?.startDate || ""}
        endDate={goal?.endDate || ""}
        experience={goal?.userAdvancement || ""}
        learningType={goal?.learningType || ""}
      />
    </div>
  );
}

export default TaskPage;