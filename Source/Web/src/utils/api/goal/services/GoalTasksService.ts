/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CompleteGoalTaskCommand } from '../models/CompleteGoalTaskCommand';
import type { GoalTaskDetailsDtoApiResponse } from '../models/GoalTaskDetailsDtoApiResponse';
import type { GoalTaskDtoListApiResponse } from '../models/GoalTaskDtoListApiResponse';
import type { GoalTasksForDayDtoListApiResponse } from '../models/GoalTasksForDayDtoListApiResponse';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class GoalTasksService {
  /**
   * @returns GoalTaskDetailsDtoApiResponse Success
   * @throws ApiError
   */
  public static putApiGoalTasksTasks({
    requestBody,
  }: {
    requestBody?: CompleteGoalTaskCommand,
  }): CancelablePromise<GoalTaskDetailsDtoApiResponse> {
    return __request(OpenAPI, {
      method: 'PUT',
      url: '/api/GoalTasks/Tasks',
      body: requestBody,
      mediaType: 'application/json',
    });
  }
  /**
   * @returns GoalTaskDtoListApiResponse Success
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
  }): CancelablePromise<GoalTaskDtoListApiResponse> {
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
   * @returns GoalTasksForDayDtoListApiResponse Success
   * @throws ApiError
   */
  public static getApiGoalTasksTasksCalendar({
    userId,
    goalId,
  }: {
    userId?: string,
    goalId?: string,
  }): CancelablePromise<GoalTasksForDayDtoListApiResponse> {
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
   * @returns GoalTaskDetailsDtoApiResponse Success
   * @throws ApiError
   */
  public static getApiGoalTasksTasksDetails({
    id,
  }: {
    id: string,
  }): CancelablePromise<GoalTaskDetailsDtoApiResponse> {
    return __request(OpenAPI, {
      method: 'GET',
      url: '/api/GoalTasks/Tasks/{id}/Details',
      path: {
        'id': id,
      },
    });
  }
}