/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CreateUserProfileCommand } from '../models/CreateUserProfileCommand';
import type { EditUserProfileCommand } from '../models/EditUserProfileCommand';
import type { UserProfileDto } from '../models/UserProfileDto';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class UserService {

    /**
     * @returns UserProfileDto Success
     * @throws ApiError
     */
    public static postProfile({
requestBody,
}: {
requestBody?: CreateUserProfileCommand,
}): CancelablePromise<UserProfileDto> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/Profile',
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * @returns UserProfileDto Success
     * @throws ApiError
     */
    public static putProfile({
requestBody,
}: {
requestBody?: EditUserProfileCommand,
}): CancelablePromise<UserProfileDto> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/Profile',
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * @returns UserProfileDto Success
     * @throws ApiError
     */
    public static getProfile({
id,
}: {
id: string,
}): CancelablePromise<UserProfileDto> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/{id}/Profile',
            path: {
                'id': id,
            },
        });
    }

}
