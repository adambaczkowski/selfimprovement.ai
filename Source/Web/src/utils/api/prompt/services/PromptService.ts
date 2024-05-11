/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { GoalTaskResource } from '../models/GoalTaskResource';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class PromptService {

    /**
     * @returns GoalTaskResource Success
     * @throws ApiError
     */
    public static postApiPromptTest(): CancelablePromise<Array<GoalTaskResource>> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/Prompt/Test',
        });
    }

}
