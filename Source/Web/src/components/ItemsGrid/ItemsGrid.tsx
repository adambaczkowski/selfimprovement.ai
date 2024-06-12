import { TaskItem } from "../../components/componentsIndex"
import styles from './ItemsGrid.module.scss';
import { GoalTaskDto } from "../../utils/api/goal";

interface Props {
  title: string,
  tasks: GoalTaskDto[]
}

function ItemsGrid({ title, tasks }: Props) {
  return (
    <div>
      <h1 className={styles.page_header}>{title}</h1>
      <div className={styles.tasks_grid}>
        {tasks.map((task: any) => (
          <TaskItem
            key={task.id}
            id={task.id}
            title={task.title}
            description={task.content}
            date={task.date}
            isCompleted={task.isCompleted}
          />
        ))}
      </div>
    </div>
  );
}

export default ItemsGrid;