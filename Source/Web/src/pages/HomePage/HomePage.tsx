import {useState } from "react";
import { useQuery } from "react-query";
import { Link } from 'react-router-dom';
import { LoadingCircle, DailyTasksSidebar } from "../../components/componentsIndex"
import { GoalTaskDto } from "../../utils/api/goal";
import { fetchTasks } from "../../utils/services/goalTaskService";
import styles from './HomePage.module.scss';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { IconProp } from '@fortawesome/fontawesome-svg-core';
import dayjs from 'dayjs';

type Props = {};

function HomePage({}: Props) {
  const [tasks, setTasks] = useState<GoalTaskDto []>([]);

  useQuery({
    queryKey: ["getTask"],
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
  
//   if (!tasks) {
//     return (
//       <div className={styles.background_container}>
//         <LoadingCircle timeout={10000} errorMessage="Something went wrong" />
//       </div>
//     );
//   }

  return (
    <div className={styles.background_container}>
      <div className={styles.home_grid}>
        <div className={styles.overview}>
          <h1>overview</h1>
        </div>
        <DailyTasksSidebar />
      </div>
    </div>
  );
}

export default HomePage;
