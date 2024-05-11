/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { Experience } from './Experience';
import type { GoalCategories } from './GoalCategories';
import type { LearningType } from './LearningType';
import type { TimeAvailability } from './TimeAvailability';

export type GoalDto = {
    category?: GoalCategories;
    timeAvailability?: TimeAvailability;
    startDate?: string;
    endDate?: string;
    experience?: Experience;
    learningType?: LearningType;
};
