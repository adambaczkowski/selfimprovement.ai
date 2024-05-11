/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CreateGoalCommand } from '../models/CreateGoalCommand';
import type { DeleteGoalCommand } from '../models/DeleteGoalCommand';
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
    public static get({
userId,
}: {
userId?: string,
}): CancelablePromise<GoalDtoListApiResponse> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/',
            query: {
                'UserId': userId,
            },
        });
    }

}
