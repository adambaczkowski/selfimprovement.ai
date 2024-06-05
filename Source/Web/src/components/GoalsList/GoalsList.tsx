import { GoalItem } from "../componentsIndex";
import styles from "./GoalsList.module.scss";
import { GoalDto } from "../../utils/api/goal";

interface Props {
  title: string;
  goals: GoalDto[];
}

function GoalsList({ title, goals }: Props) {
  return (
    <div className={styles.background_container}>
      <h1 className={styles.page_header}>{title}</h1>
      <div className={styles.goals_list}>
        {goals.map((goal: any) => (
          <GoalItem
            key={goal.id}
            id={goal.id}
            name={goal.name}
            category={goal.category}
            timeAvailability={goal.timeAvailability}
            startDate={goal.startDate}
            endDate={goal.endDate}
            experience={goal.experience}
            learningType={goal.learningType}
          />
        ))}
      </div>
    </div>
  );
}

export default GoalsList;
