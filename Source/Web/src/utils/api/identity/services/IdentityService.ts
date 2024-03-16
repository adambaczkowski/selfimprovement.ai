/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ConfirmEmailCommand } from '../models/ConfirmEmailCommand';
import type { RequestPasswordResetCommand } from '../models/RequestPasswordResetCommand';
import type { ResendConfirmationEmailCommand } from '../models/ResendConfirmationEmailCommand';
import type { ResetPasswordCommand } from '../models/ResetPasswordCommand';
import type { SignInCommand } from '../models/SignInCommand';
import type { SignInResponse } from '../models/SignInResponse';
import type { SignUpCommand } from '../models/SignUpCommand';
import type { SignUpResponse } from '../models/SignUpResponse';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class IdentityService {

    /**
     * @returns SignInResponse Success
     * @throws ApiError
     */
    public static postApiIdentitySignIn({
requestBody,
}: {
requestBody?: SignInCommand,
}): CancelablePromise<SignInResponse> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/Identity/SignIn',
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * @returns SignUpResponse Success
     * @throws ApiError
     */
    public static postApiIdentitySignUp({
requestBody,
}: {
requestBody?: SignUpCommand,
}): CancelablePromise<SignUpResponse> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/Identity/SignUp',
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * @returns any Success
     * @throws ApiError
     */
    public static postApiIdentityEmailConfirm({
requestBody,
}: {
requestBody?: ConfirmEmailCommand,
}): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/Identity/Email/Confirm',
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * @returns any Success
     * @throws ApiError
     */
    public static postApiIdentityEmailResendConfirmation({
requestBody,
}: {
requestBody?: ResendConfirmationEmailCommand,
}): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/Identity/Email/ResendConfirmation',
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * @returns any Success
     * @throws ApiError
     */
    public static postApiIdentityPasswordRequestReset({
requestBody,
}: {
requestBody?: RequestPasswordResetCommand,
}): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/Identity/Password/RequestReset',
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * @returns any Success
     * @throws ApiError
     */
    public static postApiIdentityPasswordReset({
requestBody,
}: {
requestBody?: ResetPasswordCommand,
}): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/Identity/Password/Reset',
            body: requestBody,
            mediaType: 'application/json',
        });
    }

}
