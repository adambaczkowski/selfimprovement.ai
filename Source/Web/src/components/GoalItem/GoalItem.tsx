import { Link } from 'react-router-dom';
import styles from './GoalItem.module.scss';

interface Props {
  category: string;
  timeAvailability: string;
  duration: Date;
  experience: string;
  learningType: string;
}

function GoalItem({ category, timeAvailability, duration, experience, learningType }: Props) {
  return (
    <Link to={`/goal`} className={styles.goal_item}>
      <div className={styles.goal_header}>
        <h1>{category}</h1>
        <p className={styles.description}><span>Learning type: </span>{learningType}</p>
      </div>
      <div className={styles.goal_description}>
        <p className={styles.description}><span>Time Availability: </span>{timeAvailability}</p>
        <p className={styles.description}><span>Duration: </span>{duration.toDateString()}</p>
        <p className={styles.description}><span>Experience: </span>{experience}</p>
      </div>
    </Link>
  );
}

export default GoalItem;
