import { GoalTasksService } from "../api/goal"

export const fetchTasks = async () => {
    return await GoalTasksService.getApiGoalTasksTasks({})
}

export const fetchGoalTasks = async (id: string) => {
    return await GoalTasksService.getApiGoalTasksTasks({goalId: id})
}

export const fetchTask = async (id: string) => {
    return await GoalTasksService.getApiGoalTasksTasksDetails({id: id})
}