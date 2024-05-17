/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { GoalDetailsDto } from './GoalDetailsDto';

export type GoalDetailsDtoApiResponse = {
    success?: boolean;
    data?: GoalDetailsDto;
    errorMessage?: string | null;
};
