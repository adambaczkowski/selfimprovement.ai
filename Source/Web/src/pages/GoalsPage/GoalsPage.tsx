import {useState, useEffect } from "react";
import { GoalsList } from "../../components/componentsIndex"
import { Goal, GoalTask } from './types/Goal';

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

function GaolsPage() {
  const [goals, setGoals] = useState<Goal[]>([]);

  useEffect(() => {
    setGoals([
      {
        category: "Coding",
        timeAvailability: "1 hour - per week",
        duration: new Date(2024, 3, 20),
        experience: "Amatour",
        learningType: "Fast",
        tasks: exampleTasks,
      },
      {
        category: "Running",
        timeAvailability: "3 hour - per week",
        duration: new Date(2024, 4, 22),
        experience: "Amatour",
        learningType: "Fast",
        tasks: exampleTasks,
      },
    ]);
  }, []);

  return (
    <GoalsList title={"Goals"} goals={goals} />
  );
}

export default GaolsPage;
