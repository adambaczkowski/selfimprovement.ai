/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { TimeSpan } from './TimeSpan';

export type GoalTaskResource = {
    id?: string;
    goalId?: string;
    content?: string | null;
    estimatedDuration?: TimeSpan;
    isCompleted?: boolean;
    date?: string;
};
