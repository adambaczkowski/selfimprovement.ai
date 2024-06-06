/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CreateGoalCommand } from '../models/CreateGoalCommand';
import type { DeleteGoalCommand } from '../models/DeleteGoalCommand';
import type { GoalDetailsDto } from '../models/GoalDetailsDto';
import type { GoalDto } from '../models/GoalDto';
import type { GoalHomeDto } from '../models/GoalHomeDto';
import type { UserTasksRatioDto } from '../models/UserTasksRatioDto';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class GoalService {
    /**
     * @returns GoalDto Success
     * @throws ApiError
     */
    public static post({
        requestBody,
    }: {
        requestBody?: CreateGoalCommand,
    }): CancelablePromise<GoalDto> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/',
            body: requestBody,
            mediaType: 'application/json',
        });
    }
    /**
     * @returns any Success
     * @throws ApiError
     */
    public static delete({
        requestBody,
    }: {
        requestBody?: DeleteGoalCommand,
    }): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/',
            body: requestBody,
            mediaType: 'application/json',
        });
    }
    /**
     * @returns GoalDto Success
     * @throws ApiError
     */
    public static getUserGoals({
        userId,
    }: {
        userId?: string,
    }): CancelablePromise<Array<GoalDto>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/UserGoals',
            query: {
                'UserId': userId,
            },
        });
    }
    /**
     * @returns GoalHomeDto Success
     * @throws ApiError
     */
    public static getUserHomeGoals({
        userId,
    }: {
        userId?: string,
    }): CancelablePromise<Array<GoalHomeDto>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/UserHomeGoals',
            query: {
                'UserId': userId,
            },
        });
    }
    /**
     * @returns UserTasksRatioDto Success
     * @throws ApiError
     */
    public static getDoneToOverallTasksRatio({
        userId,
    }: {
        userId?: string,
    }): CancelablePromise<UserTasksRatioDto> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/DoneToOverallTasksRatio',
            query: {
                'UserId': userId,
            },
        });
    }
    /**
     * @returns GoalDetailsDto Success
     * @throws ApiError
     */
    public static getDetails({
        id,
    }: {
        id: string,
    }): CancelablePromise<GoalDetailsDto> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/{id}/Details',
            path: {
                'id': id,
            },
        });
    }
}
