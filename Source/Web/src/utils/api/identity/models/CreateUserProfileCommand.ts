/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { Education } from './Education';

export type CreateUserProfileCommand = {
    userId?: string;
    profileImage?: Blob | null;
    name?: string | null;
    surname?: string | null;
    weight?: number | null;
    height?: number | null;
    age?: number | null;
    educationLevel?: Education;
};
