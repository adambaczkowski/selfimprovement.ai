/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { GoalTaskDetailsDto } from './GoalTaskDetailsDto';

export type GoalTaskDetailsDtoApiResponse = {
    success?: boolean;
    data?: GoalTaskDetailsDto;
    errorMessage?: string | null;
};
