import React, {useState, useEffect } from "react";
import { Link } from 'react-router-dom';
import { Paper, Typography } from '@mui/material';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import { LoadingCircle, DailyTaskList } from "../../components/componentsIndex"
import styles from './TaskPage.module.scss';
import { DailyTask } from './types/DailyTask';

type Props = {};

function TaskPage({}: Props) {
  const [dailyTask, setDailyTask] = useState<DailyTask | null>();
  useEffect(() => {
    setDailyTask({
      weekTitle: "Week 1: January 17-21, 2024",
      date: "January 17, 2024",
      tasks: [
        "Set Up Development Environment",
        "Install Python and a Code Editor (e.g., VS Code)",
        "Explore basic features of the chosen code editor Explore basic features of the chosen code editor Explore basic features of the chosen code editor",
      ]
    });
  }, []);
  
  if (!dailyTask) {
    return (
      <div className={styles.background_container}>
        <LoadingCircle timeout={10000} errorMessage="Something went wrong" />
      </div>
    );
  }

  return (
    <div className={styles.background_container}>
        <Link className={styles.go_back_button} to='/tasks'>
          <ArrowBackIcon />
        </Link>
        <Paper elevation={3} className={styles.glass_container}>
          <Typography variant="h5" className={styles.info_heading}>
            {dailyTask.weekTitle}
          </Typography>
          <Typography className={styles.info_subheading}>
            {dailyTask.date}:
          </Typography>
        </Paper>
        <DailyTaskList items={dailyTask.tasks} />
    </div>
  );
}

export default TaskPage;
