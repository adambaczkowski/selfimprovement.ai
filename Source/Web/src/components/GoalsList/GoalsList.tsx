import { GoalItem } from "../componentsIndex"
import styles from './GoalsList.module.scss';
import { Goal } from './../../pages/GoalsPage/types/Goal'; 

interface Props {
  title: string;
  goals: Goal[];
}

function GoalsList({ title, goals }: Props) {
  return (
    <div className={styles.background_container}>
      <h1 className={styles.page_header}>{title}</h1>
      <div className={styles.goals_list}>
        {goals.map((goal: any) => (
            <GoalItem
              // key={task.id}
              category={goal.category}
              timeAvailability={goal.timeAvailability}
              duration={goal.duration}
              experience={goal.experience}
              learningType={goal.learningType}
            />
        ))}
      </div>
    </div>
  );
}

export default GoalsList;
