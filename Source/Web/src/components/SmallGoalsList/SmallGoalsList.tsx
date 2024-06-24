import { useState } from 'react';
import { useQuery } from "react-query";
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { IconProp } from '@fortawesome/fontawesome-svg-core';
import { squareCheckSolidIcon, squareCheckRegularIcon} from "./../../utils/enums/sidebarMenu";
import styles from './SmallGoalsList.module.scss';
import { shortenString } from '../../utils/helpers/shortenString';
import { fetchHomeGoals } from "../../utils/services/goalService";
import { GoalHomeDto } from "../../utils/api/goal";

function SmallGoalsList() {
  const [goals, setGoals] = useState<GoalHomeDto[]>([]);
  
  useQuery({
    queryKey: ["fetchHomeGoals"],
    queryFn: async () => {
      const goals = await fetchHomeGoals();
      if (goals != null) {
        setGoals(goals);
      }
      return goals;
    },
    refetchOnWindowFocus: false,
  });

  return (
    <div className={styles.goals}>
      <div className={styles.emoji}>🎯</div>
      <div className={styles.header_container}>
        <h1 className={styles.goal_header}>
          Goals
        </h1>
      </div>
      <div className={styles.goal_items_container}>
        {goals.map((goal: any) => (
          <Link to={`/goal/${goal.id}`} className={styles.goal_item}>
            <p className={styles.goal_header}>
              {shortenString(goal.name, 30).toLowerCase()}
            </p>
            <p className={styles.goal_counter}>
              {goal?.completedTasksCount}/{goal?.allTasksCount}
            </p>
            <div className={styles.goal_item_checkbox}>
              {Number(goal?.completedTasksCount) === Number(goal?.allTasksCount) ? 
                <FontAwesomeIcon icon={squareCheckSolidIcon as IconProp} style={{ color: "#32ff54"}}/> : 
                <FontAwesomeIcon icon={squareCheckRegularIcon as IconProp} style={{ color: "#6b6b6b"}} />}
            </div>
          </Link>
        ))}
      </div>
    </div>
  );
}

export default SmallGoalsList;
