/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { TimeSpan } from './TimeSpan';
export type GoalTaskDto = {
    id?: string;
    goalId?: string;
    title?: string | null;
    content?: string | null;
    estimatedDuration?: TimeSpan;
    isCompleted?: boolean;
    date?: string;
};

