import {useState, useEffect } from "react";
import { LoadingCircle, GoBackButton  } from "../../components/componentsIndex"
import styles from './GoalPage.module.scss';
import { Goal, GoalTask } from '../GoalsPage/types/Goal';
import { DailyTask } from '../TasksPage/types/DailyTask';
import { ItemsGrid } from "../../components/componentsIndex"

const exampleTasks: GoalTask[] = [
  {
      content: "Set Up Development Environment",
      estimatedDuration: new Date(2024, 3, 20, 1, 30),
      isCompleted: false,
      date: new Date(2024, 3, 20),
  },
  {
      content: "Install Python and a Code Editor (e.g., VS Code)",
      estimatedDuration: new Date(2024, 3, 21),
      isCompleted: false,
      date: new Date(2024, 3, 21),
  },
  {
      content: "Explore basic features of the chosen code editor",
      estimatedDuration: new Date(2024, 3, 22),
      isCompleted: false,
      date: new Date(2024, 3, 22),
  },
];

const exampleGoal: Goal = 
  {
    category: "Coding",
    timeAvailability: "1 hour - per week",
    duration: new Date(2024, 3, 20),
    experience: "Amatour",
    learningType: "Fast",
    tasks: exampleTasks,
  };

const exampleDailyTasks: DailyTask[] = [
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
];

function GoalPage() {
  const [goal, setGoal] = useState<Goal>();
  const [dailyTasks, setDailyTasks] = useState<DailyTask[]>([]);

  useEffect(() => {
    return setGoal(
      exampleGoal
    );
  }, []);

  useEffect(() => {
    return setDailyTasks(
      exampleDailyTasks
    );
  }, []);

  if (!goal) {
    return (
      <div className={styles.background_container}>
        <LoadingCircle timeout={10000} errorMessage="Something went wrong" />
      </div>
    );
  }

  if (!dailyTasks) {
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
        <p className={styles.description}><span>Time Availability: </span>{goal.timeAvailability}</p>
        <p className={styles.description}><span>Duration: </span>{goal.duration.toDateString()}</p>
        <p className={styles.description}><span>Experience: </span>{goal.experience}</p>
      </div>
    </div>
      <ItemsGrid title={"Goal Tasks"} dailyTasks={exampleDailyTasks} />
    </div>
  );
}

export default GoalPage;
