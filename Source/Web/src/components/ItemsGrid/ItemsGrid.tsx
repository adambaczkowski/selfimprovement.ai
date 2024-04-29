import { TaskItem } from "../../components/componentsIndex"
import styles from './ItemsGrid.module.scss'; // Import the SCSS file
import { DailyTask } from './../../pages/TasksPage/types/DailyTask'; 

interface Props {
  title: string,
  dailyTasks: DailyTask[]
}

function ItemsGrid({ title, dailyTasks }: Props) {
  return (
    <div className={styles.background_container}>
      <h1 className={styles.header}>{title}</h1>
      <div className={styles.tasks_grid}>
        {dailyTasks.map((dailyTask: any) => (
          dailyTask.tasks.map((task: any) => (
            <TaskItem
              // key={task.id}
              title={task}
              description={task}
              date={dailyTask.date}
              isCompleted={dailyTask.isCompleted}
            />
          ))
        ))}
      </div>
    </div>
  );
}

export default ItemsGrid;
