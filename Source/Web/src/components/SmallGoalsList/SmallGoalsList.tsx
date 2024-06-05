import { useState } from 'react';
import { useQuery } from "react-query";
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { IconProp } from '@fortawesome/fontawesome-svg-core';
import { squareCheckSolidIcon, squareCheckRegularIcon} from "./../../utils/enums/sidebarMenu";
import styles from './SmallGoalsList.module.scss';
import { shortenString } from '../../utils/helpers/shortenString';
import { fetchGoals } from "../../utils/services/goalService";
import { GoalDto } from "../../utils/api/goal";

function SmallGoalsList() {
  const [goals, setGoals] = useState<GoalDto[]>([]);

  const x = true;
  const done_1 = 8;
  const done_2 = 10;
  const all = 10


  useQuery({
    queryKey: ["getGoals"],
    queryFn: async () => {
      const goals = await fetchGoals();
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
          <Link to={`/tasks`} className={styles.goal_item}>
            <p className={styles.goal_header}>
              {shortenString(goal.name, 30).toLowerCase()}
            </p>
            <p className={styles.goal_counter}>
              {goal.tasks?.length}/{goal.tasks?.length} 
              {/* {done_1}/{all}  */}
            </p>
            <div className={styles.goal_item_checkbox}>
              {Number(done_1) === Number(all) ? 
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
