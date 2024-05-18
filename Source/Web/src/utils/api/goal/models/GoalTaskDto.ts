/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { TimeSpan } from './TimeSpan';

export type GoalTaskDto = {
    id?: string;
    goalId?: string;
    content?: string | null;
    estimatedDuration?: TimeSpan;
    isCompleted?: boolean;
    date?: string;
};
