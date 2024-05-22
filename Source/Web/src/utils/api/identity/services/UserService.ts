/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CreateUserProfileCommand } from '../models/CreateUserProfileCommand';
import type { EditUserProfileCommand } from '../models/EditUserProfileCommand';
import type { UserProfileDtoApiResponse } from '../models/UserProfileDtoApiResponse';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class UserService {
    /**
     * @returns UserProfileDtoApiResponse Success
     * @throws ApiError
     */
    public static postApiUserProfile({
        requestBody,
    }: {
        requestBody?: CreateUserProfileCommand,
    }): CancelablePromise<UserProfileDtoApiResponse> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/User/Profile',
            body: requestBody,
            mediaType: 'application/json',
        });
    }
    /**
     * @returns UserProfileDtoApiResponse Success
     * @throws ApiError
     */
    public static putApiUserProfile({
        requestBody,
    }: {
        requestBody?: EditUserProfileCommand,
    }): CancelablePromise<UserProfileDtoApiResponse> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/api/User/Profile',
            body: requestBody,
            mediaType: 'application/json',
        });
    }
}
