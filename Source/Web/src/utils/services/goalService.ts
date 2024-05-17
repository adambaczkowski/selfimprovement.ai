import { CreateGoalCommand, GoalService } from "../api/goal"

export const createGoal = async (command : CreateGoalCommand) => {
    const userToken = JSON.parse(localStorage.getItem("userToken") || '""');
    console.log('#usertkn', userToken);
    const response = await GoalService.post({requestBody: command});
    console.log('#response', response);
    return response;
}

export const fetchGoals = async () => {
    return await GoalService.getUserGoals({})
}