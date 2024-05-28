import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { IconProp } from '@fortawesome/fontawesome-svg-core';
import { squareCheckSolidIcon, squareCheckRegularIcon} from "./../../utils/enums/sidebarMenu";
import styles from './DailyTasksSidebar.module.scss';
import { shortenString } from '../../utils/helpers/shortenString';

function DailyTasksSidebar() {
  const x = true;
  
  return (
    <div className={styles.todays_challanges}>
      <div className={styles.emoji}>😎</div>
      <div className={styles.header_container}>
        <h1 className={styles.todays_challanges_header}>
          Daily tasks
        </h1>
      </div>
    
      <Link to={`/tasks`} className={styles.task_item}>
        <p className={styles.task_item_header}>
          {shortenString("Create first python project", 30).toLowerCase()}
        </p>
        <div className={styles.task_item_checkbox}>
          { x ? 
            <FontAwesomeIcon icon={squareCheckSolidIcon as IconProp} style={{ color: "#32ff54"}} /> :
            <FontAwesomeIcon icon={squareCheckRegularIcon as IconProp} style={{ color: "#6b6b6b"}}/>
          }
        </div>
      </Link>

      <Link to={`/tasks`} className={styles.task_item}>
        <p className={styles.task_item_header}>
          {shortenString("Do a 5km run", 30).toLowerCase()}
        </p>
        <div className={styles.task_item_checkbox}>
          { !x ? 
            <FontAwesomeIcon icon={squareCheckSolidIcon as IconProp} style={{ color: "#32ff54"}} /> :
            <FontAwesomeIcon icon={squareCheckRegularIcon as IconProp} style={{ color: "#6b6b6b"}}/>
          }
        </div>
      </Link>

      <Link to={`/tasks`} className={styles.task_item}>
        <p className={styles.task_item_header}>
          {shortenString("Make a first carbonara", 30).toLowerCase()}
        </p>
        <div className={styles.task_item_checkbox}>
          { x ? 
            <FontAwesomeIcon icon={squareCheckSolidIcon as IconProp} style={{ color: "#32ff54"}} /> :
            <FontAwesomeIcon icon={squareCheckRegularIcon as IconProp} style={{ color: "#6b6b6b"}}/>
          }
        </div>
      </Link>
    </div> 
  );
}

export default DailyTasksSidebar;
