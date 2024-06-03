import { useState } from "react";
import { GoalsList } from "../../components/componentsIndex";
import { fetchGoals } from "../../utils/services/goalService";
import { GoalDto } from "../../utils/api/goal";
import { useQuery } from "react-query";

const GoalsPage = () => {
  const [goals, setGoals] = useState<GoalDto[]>([]);

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

  return <GoalsList title={"Goals"} goals={goals} />;
};

export default GoalsPage;
