import { CreateGoalCommand, GoalService } from "../api/goal"

export const createGoal = async (command : CreateGoalCommand) => {
    const response = await GoalService.post({requestBody: command});
    return response;
}

export const fetchGoals = async () => {
    return await GoalService.getUserGoals({})
}

export const fetchGoal = async (id: string) => {
    return await GoalService.getGoalDetails({id: id})
}