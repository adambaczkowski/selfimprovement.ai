/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { UserProfileDto } from './UserProfileDto';

export type UserProfileDtoApiResponse = {
    success?: boolean;
    data?: UserProfileDto;
    errorMessage?: string | null;
};
