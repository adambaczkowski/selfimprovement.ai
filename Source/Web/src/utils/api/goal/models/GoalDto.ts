/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { Experience } from './Experience';
import type { GoalCategories } from './GoalCategories';
import type { LearningType } from './LearningType';
import type { TimeAvailabilityPerDay } from './TimeAvailabilityPerDay';
import type { TimeAvailabilityPerWeek } from './TimeAvailabilityPerWeek';
import type { UserAdvancement } from './UserAdvancement';

export type GoalDto = {
    id?: string;
    name?: string | null;
    category?: GoalCategories;
    userAdvancement?: UserAdvancement;
    timeAvailabilityPerDay?: TimeAvailabilityPerDay;
    timeAvailabilityPerWeek?: TimeAvailabilityPerWeek;
    startDate?: string;
    endDate?: string;
    experience?: Experience;
    learningType?: LearningType;
    userInput?: string | null;
};
