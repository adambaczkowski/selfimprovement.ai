import { useState } from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { IconProp } from '@fortawesome/fontawesome-svg-core';
import { squareCheckSolidIcon, squareCheckRegularIcon} from "./../../utils/enums/sidebarMenu";
import styles from './DailyTasksSidebar.module.scss';
import { fetchTasks } from "../../utils/services/goalTaskService";
import { useQuery } from "react-query";
import { shortenString } from '../../utils/helpers/shortenString';
import { GoalTaskDto } from "../../utils/api/goal";

const isToday = (dateString: string | undefined): boolean => {
  if (!dateString) return false;
  const taskDate = new Date(dateString);
  const today = new Date();
  return taskDate.getDate() === today.getDate() &&
         taskDate.getMonth() === today.getMonth() &&
         taskDate.getFullYear() === today.getFullYear();
};

  // Get today's tasks
  const getTodaysTasks = (tasks: GoalTaskDto[]): GoalTaskDto[] => {
    return tasks.filter(task => isToday(task.date));
  };

function DailyTasksSidebar() {
  const [dailyTasks, setDailyTasks] = useState<GoalTaskDto[]>([]);
  const x = true;

  useQuery({
    queryKey: ["getTasks"],
    queryFn: async () => {
      const tasks = await fetchTasks();
      if (tasks != null) {
        setDailyTasks(getTodaysTasks(tasks));
      }
      return tasks;
    },
    refetchOnWindowFocus: false,
  });
  
  return (
    <div className={styles.todays_challanges}>
      <div className={styles.emoji}>😎</div>
      <div className={styles.header_container}>
        <h1 className={styles.todays_challanges_header}>
          Daily tasks
        </h1>
      </div>
      {dailyTasks.map((dailyTask: any) => (
        <Link to={`/task/${dailyTask.id}`} className={styles.task_item}>
          <p className={styles.task_item_header}>
            {shortenString(dailyTask.title, 30).toLowerCase()}
          </p>
          <div className={styles.task_item_checkbox}>
            { dailyTask.isCompleted ? 
              <FontAwesomeIcon icon={squareCheckSolidIcon as IconProp} style={{ color: "#32ff54"}} /> :
              <FontAwesomeIcon icon={squareCheckRegularIcon as IconProp} style={{ color: "#6b6b6b"}}/>
            }
          </div>
        </Link>
        ))}
    </div> 
  );
}

export default DailyTasksSidebar;
