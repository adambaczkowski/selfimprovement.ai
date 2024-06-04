/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { Education } from './Education';
import type { Sex } from './Sex';
export type UserProfileDto = {
    id?: string;
    profileImageData?: string | null;
    sex?: Sex;
    weight?: number | null;
    height?: number | null;
    age?: number | null;
    educationLevel?: Education;
};

