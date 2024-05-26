import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { IconProp } from '@fortawesome/fontawesome-svg-core';
import { squareCheckSolidIcon, squareCheckRegularIcon} from "./../../utils/enums/sidebarMenu";
import styles from './SmallGoalsList.module.scss';
import { shortenString } from '../../utils/helpers/shortenString';

function SmallGoalsList() {
  const x = true;
  const done_1 = 8;
  const done_2 = 10;
  const all = 10

  return (
    <div className={styles.goals}>
      <div className={styles.emoji}>🎯</div>
      <div className={styles.header_container}>
        <h1 className={styles.goal_header}>
          Goals
        </h1>
      </div>
      <div className={styles.goal_items_container}>
        <Link to={`/tasks`} className={styles.goal_item}>
          <p className={styles.goal_header}>
            {shortenString("Programming goal", 30).toLowerCase()}
          </p>
          <p className={styles.goal_counter}>
            {done_1}/{all} 
          </p>
          <div className={styles.goal_item_checkbox}>
            {Number(done_1) === Number(all) ? 
              <FontAwesomeIcon icon={squareCheckSolidIcon as IconProp} style={{ color: "#32ff54"}}/> : 
              <FontAwesomeIcon icon={squareCheckRegularIcon as IconProp} style={{ color: "#6b6b6b"}} />}
          </div>
        </Link>
        
        <Link to={`/tasks`} className={styles.goal_item}>
          <p className={styles.goal_header}>
            {shortenString("Running goal", 30).toLowerCase()}
          </p>
          <p className={styles.goal_counter}>
            {done_2}/{all} 
          </p>
          <div className={styles.goal_item_checkbox}>
            {done_2 === Number(all) ? 
              <FontAwesomeIcon icon={squareCheckSolidIcon as IconProp} style={{ color: "#32ff54"}}/> : 
              <FontAwesomeIcon icon={squareCheckRegularIcon as IconProp} style={{ color: "#6b6b6b"}} />}
          </div>
        </Link>

        <Link to={`/tasks`} className={styles.goal_item}>
          <p className={styles.goal_header}>
            {shortenString("Running goal", 30).toLowerCase()}
          </p>
          <p className={styles.goal_counter}>
            {done_2}/{all} 
          </p>
          <div className={styles.goal_item_checkbox}>
            {done_2 === Number(all) ? 
              <FontAwesomeIcon icon={squareCheckSolidIcon as IconProp} style={{ color: "#32ff54"}}/> : 
              <FontAwesomeIcon icon={squareCheckRegularIcon as IconProp} style={{ color: "#6b6b6b"}} />}
          </div>
        </Link>

        <Link to={`/tasks`} className={styles.goal_item}>
          <p className={styles.goal_header}>
            {shortenString("Running goal", 30).toLowerCase()}
          </p>
          <p className={styles.goal_counter}>
            {done_2}/{all} 
          </p>
          <div className={styles.goal_item_checkbox}>
            {done_2 === Number(all) ? 
              <FontAwesomeIcon icon={squareCheckSolidIcon as IconProp} style={{ color: "#32ff54"}}/> : 
              <FontAwesomeIcon icon={squareCheckRegularIcon as IconProp} style={{ color: "#6b6b6b"}} />}
          </div>
        </Link>
      </div>
    </div>
  );
}

export default SmallGoalsList;
