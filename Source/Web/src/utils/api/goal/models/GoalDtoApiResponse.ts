/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { GoalDto } from './GoalDto';

export type GoalDtoApiResponse = {
    success?: boolean;
    data?: GoalDto;
    errorMessage?: string | null;
};
