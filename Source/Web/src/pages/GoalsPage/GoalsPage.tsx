import { useState, useEffect } from "react";
import { GoalsList } from "../../components/componentsIndex";
import { Goal, GoalTask } from "./types/Goal";
import { fetchGoals } from "../../utils/services/goalService";
import { GoalDto } from "../../utils/api/goal";
import { useQuery } from "react-query";

const exampleTasks: GoalTask[] = [
  {
    content: "Set Up Development Environment",
    estimatedDuration: new Date(2024, 3, 20, 1, 30),
    isCompleted: false,
    date: new Date(2024, 3, 20),
  },
  {
    content: "Install Python and a Code Editor (e.g., VS Code)",
    estimatedDuration: new Date(2024, 3, 21),
    isCompleted: false,
    date: new Date(2024, 3, 21),
  },
  {
    content: "Explore basic features of the chosen code editor",
    estimatedDuration: new Date(2024, 3, 22),
    isCompleted: false,
    date: new Date(2024, 3, 22),
  },
];

const GoalsPage = () => {
  const [goals, setGoals] = useState<GoalDto[]>([]);

  useQuery({
    queryKey: ["getGoals"],
    queryFn: async () => {
      const response = await fetchGoals();
      const goals = response.data;
      if (goals != null) {
        setGoals(goals);
      }
      return response.data;
    },
    refetchOnWindowFocus: false,
  });

  return <GoalsList title={"Goals"} goals={goals} />;
};

export default GoalsPage;
