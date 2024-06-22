import { CreateGoalCommand, GoalService } from "../api/goal"

export const createGoal = async (command : CreateGoalCommand) => {
    const response = await GoalService.post({requestBody: command});
    return response;
}

export const fetchGoals = async () => {
    return await GoalService.getUserGoals({})
}

export const fetchHomeGoals = async () => {
    return await GoalService.getUserHomeGoals({})
}

export const fetchGoal = async (id: string) => {
    return await GoalService.getDetails({id: id})
}

export const getDoneToOverallTasksRatio = async () => {
    return await GoalService.getDoneToOverallTasksRatio({})
}