/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { GoalTaskDto } from './GoalTaskDto';

export type GoalTaskDtoListApiResponse = {
    success?: boolean;
    data?: Array<GoalTaskDto> | null;
    errorMessage?: string | null;
};
