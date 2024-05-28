/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { GoalTaskDto } from './GoalTaskDto';

export type GoalTasksForDayDto = {
    day?: string;
    goalTasks?: Array<GoalTaskDto> | null;
};
