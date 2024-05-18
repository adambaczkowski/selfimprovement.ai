/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CreateGoalCommand } from '../models/CreateGoalCommand';
import type { DeleteGoalCommand } from '../models/DeleteGoalCommand';
import type { GoalDetailsDtoApiResponse } from '../models/GoalDetailsDtoApiResponse';
import type { GoalDtoApiResponse } from '../models/GoalDtoApiResponse';
import type { GoalDtoListApiResponse } from '../models/GoalDtoListApiResponse';
import type { StringApiResponse } from '../models/StringApiResponse';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class GoalService {

    /**
     * @returns GoalDtoApiResponse Success
     * @throws ApiError
     */
    public static post({
requestBody,
}: {
requestBody?: CreateGoalCommand,
}): CancelablePromise<GoalDtoApiResponse> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/',
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * @returns StringApiResponse Success
     * @throws ApiError
     */
    public static delete({
requestBody,
}: {
requestBody?: DeleteGoalCommand,
}): CancelablePromise<StringApiResponse> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/',
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * @returns GoalDtoListApiResponse Success
     * @throws ApiError
     */
    public static getUserGoals({
userId,
}: {
userId?: string,
}): CancelablePromise<GoalDtoListApiResponse> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/UserGoals',
            query: {
                'UserId': userId,
            },
        });
    }

    /**
     * @returns GoalDetailsDtoApiResponse Success
     * @throws ApiError
     */
    public static getDetails({
id,
}: {
id: string,
}): CancelablePromise<GoalDetailsDtoApiResponse> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/{id}/Details',
            path: {
                'id': id,
            },
        });
    }

}
