/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { Experience } from './Experience';
import type { GoalCategories } from './GoalCategories';
import type { LearningType } from './LearningType';
import type { TimeAvailability } from './TimeAvailability';

export type CreateGoalCommand = {
    userId?: string;
    category?: GoalCategories;
    timeAvailability?: TimeAvailability;
    duration?: number;
    experience?: Experience;
    learningType?: LearningType;
};
