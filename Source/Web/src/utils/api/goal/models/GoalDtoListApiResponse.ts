/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { GoalDto } from './GoalDto';

export type GoalDtoListApiResponse = {
    success?: boolean;
    data?: Array<GoalDto> | null;
    errorMessage?: string | null;
};
