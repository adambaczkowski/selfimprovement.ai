/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { EducationLevel } from '../../../enums/educationLevel';

export type ProfileCreationCommand = {
    name: string | null;
    surname: string | null;
    weight?: number | null;
    height?: number | null;
    age?: number | null;
    educationLevel?: EducationLevel | null;
    profileImage: ArrayBuffer | null;
};
