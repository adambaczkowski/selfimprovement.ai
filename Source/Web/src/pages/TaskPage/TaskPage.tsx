import {useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { useQuery } from "react-query";
import { Typography } from '@mui/material';
import { LoadingCircle, GoBackButton } from "../../components/componentsIndex"
import { GoalTaskDto } from "../../utils/api/goal";
import { fetchTask } from "../../utils/services/goalTaskService";
import styles from './TaskPage.module.scss';
import { DailyTask } from './types/DailyTask';
import dayjs from 'dayjs';

type Props = {};

function TaskPage({}: Props) {
  const { id } = useParams();
  const [dailyTask, setDailyTask] = useState<DailyTask | null>();
  const [task, setTask] = useState<GoalTaskDto>();

  // useEffect(() => {
  //   setDailyTask({
  //     weekTitle: "Week 1: January 17-21, 2024",
  //     date: "January 17, 2024",
  //     tasks: [
  //       "Set Up Development Environment",
  //       "Install Python and a Code Editor (e.g., VS Code)",
  //       "Explore basic features of the chosen code editor Explore basic features of the chosen code editor Explore basic features of the chosen code editor",
  //     ]
  //   });
  // }, []);

  useQuery({
    queryKey: ["getTask"],
    queryFn: async () => {
      const response = await fetchTask(id || "");
      const task = response.data;
      if (task != null) {
        setTask(task);
      }
      return response.data;
    },
    refetchOnWindowFocus: false,
  });
  
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
      <div className={styles.center_container}>
        <div className={styles.glass_container}>
          <Typography variant="h5" className={styles.info_heading}>
            {dayjs(task.date).format("MM-DD-YYYY")}
          </Typography>
          <Typography className={styles.info_subheading}>
            {task.content}:
          </Typography>
        </div>
        {/* <DailyTaskList items={task.content} /> */}
        <p>{task.content}</p>
      </div>
    </div>
  );
}

export default TaskPage;
