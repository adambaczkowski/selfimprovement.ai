import { Link } from 'react-router-dom';
import styles from './SmallerGoalItem.module.scss';
import dayjs from 'dayjs';

interface Props {
  id: string;
  name: string;
  category: string;
  timeAvailability: string;
  startDate: string;
  endDate: string;
  experience: string;
  learningType: string;
}

function Smaller({ id, category, name, timeAvailability, startDate, endDate, experience, learningType }: Props) {
  return (
    <Link to={`/goal/${id}`} className={styles.goal_item}>
      <div className={styles.goal_header}>
        {/* <h1>{category}</h1> */}
        <h1>{name}</h1>
        <p className={styles.description}><span>Category: </span>{category}</p>
      </div>
      <div className={styles.goal_description}>
        <p className={styles.description}><span>Start date: </span>{dayjs(startDate).format("MM-DD-YYYY")}</p>
        <p className={styles.description}><span>End date: </span>{dayjs(endDate).format("MM-DD-YYYY")}</p>
        <p className={styles.description}><span>Experience: </span>{experience}</p>
      </div>
    </Link>
  );
}

export default Smaller;
