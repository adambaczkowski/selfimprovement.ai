/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { Education } from './Education';
export type EditUserProfileCommand = {
    userId?: string;
    profileImage?: Blob | null;
    name?: string | null;
    surname?: string | null;
    weight?: number | null;
    height?: number | null;
    age?: number | null;
    educationLevel?: Education;
};

