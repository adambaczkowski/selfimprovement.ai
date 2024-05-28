/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { Education } from '../models/Education';
import type { Sex } from '../models/Sex';
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
formData,
}: {
formData?: {
UserId?: string;
ProfileImage?: Blob;
Name?: string;
Surname?: string;
Sex?: Sex;
Weight?: number;
Height?: number;
Age?: number;
EducationLevel?: Education;
},
}): CancelablePromise<UserProfileDto> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/Profile',
            formData: formData,
            mediaType: 'multipart/form-data',
        });
    }

    /**
     * @returns UserProfileDto Success
     * @throws ApiError
     */
    public static putProfile({
formData,
}: {
formData?: {
UserId?: string;
ProfileImage?: Blob;
Name?: string;
Surname?: string;
Sex?: Sex;
Weight?: number;
Height?: number;
Age?: number;
EducationLevel?: Education;
},
}): CancelablePromise<UserProfileDto> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/Profile',
            formData: formData,
            mediaType: 'multipart/form-data',
        });
    }

    /**
     * @returns UserProfileDto Success
     * @throws ApiError
     */
    public static getProfile(): CancelablePromise<UserProfileDto> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/Profile',
        });
    }

    /**
     * @returns UserProfileDto Success
     * @throws ApiError
     */
    public static getApiUserProfile({
userId,
}: {
userId: string,
}): CancelablePromise<UserProfileDto> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/User/{userId}/Profile',
            path: {
                'userId': userId,
            },
        });
    }

}
