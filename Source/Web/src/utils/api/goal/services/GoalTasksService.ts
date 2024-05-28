/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CompleteGoalTaskCommand } from '../models/CompleteGoalTaskCommand';
import type { GoalTaskDetailsDto } from '../models/GoalTaskDetailsDto';
import type { GoalTaskDto } from '../models/GoalTaskDto';
import type { GoalTasksForDayDto } from '../models/GoalTasksForDayDto';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class GoalTasksService {

    /**
     * @returns GoalTaskDetailsDto Success
     * @throws ApiError
     */
    public static putApiGoalTasksTasks({
requestBody,
}: {
requestBody?: CompleteGoalTaskCommand,
}): CancelablePromise<GoalTaskDetailsDto> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/api/GoalTasks/Tasks',
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * @returns GoalTaskDto Success
     * @throws ApiError
     */
    public static getApiGoalTasksTasks({
isCompleted,
goalId,
dayFrom,
dayTo,
userId,
}: {
isCompleted?: boolean,
goalId?: string,
dayFrom?: string,
dayTo?: string,
userId?: string,
}): CancelablePromise<Array<GoalTaskDto>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/GoalTasks/Tasks',
            query: {
                'IsCompleted': isCompleted,
                'GoalId': goalId,
                'DayFrom': dayFrom,
                'DayTo': dayTo,
                'UserId': userId,
            },
        });
    }

    /**
     * @returns GoalTasksForDayDto Success
     * @throws ApiError
     */
    public static getApiGoalTasksTasksCalendar({
userId,
goalId,
}: {
userId?: string,
goalId?: string,
}): CancelablePromise<Array<GoalTasksForDayDto>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/GoalTasks/Tasks/Calendar',
            query: {
                'UserId': userId,
                'GoalId': goalId,
            },
        });
    }

    /**
     * @returns GoalTaskDetailsDto Success
     * @throws ApiError
     */
    public static getApiGoalTasksTasksDetails({
id,
}: {
id: string,
}): CancelablePromise<GoalTaskDetailsDto> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/GoalTasks/Tasks/{id}/Details',
            path: {
                'id': id,
            },
        });
    }

}
