import { CreateGoalCommand, GoalService } from "../api/goal"

export const createGoal = async (command : CreateGoalCommand) => {
    return await GoalService.post({requestBody: command});
}

export const fetchGoals = async () => {
    return await GoalService.get({})
}