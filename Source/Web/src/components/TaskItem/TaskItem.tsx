import { Link } from 'react-router-dom';
import styles from './TaskItem.module.scss';
import dayjs from 'dayjs';

interface Props {
  id: string;
  title: string;
  description: string;
  date: string;
  isCompleted: boolean;
}

function TaskItem({ id, title, description, date, isCompleted }: Props) {
  return (
    <Link to={`/task/${id}`} className={styles.task_item}>
      <h1>{title}</h1>
      <p className={styles.description}>{description}</p>
      <p className={styles.date}>{dayjs(date).format("MM-DD-YYYY")}</p>
      <div className={styles.task_footer}>
        {isCompleted ? (
          <button
            className={styles.complete_button}
            onClick={() => {
              const task = {
                isCompleted: !isCompleted,
              };

              // updateTask(task);
            }}
          >
            Completed
          </button>
        ) : (
          <button
            className={styles.incomplete_button}
            onClick={() => {
              const task = {
                isCompleted: !isCompleted,
              };

              // updateTask(task);
            }}
          >
            Incomplete
          </button>
        )}
      </div>
    </Link>
  );
}

export default TaskItem;
