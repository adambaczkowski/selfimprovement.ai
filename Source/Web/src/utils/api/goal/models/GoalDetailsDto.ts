/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { GoalCategories } from './GoalCategories';
import type { Goals } from './Goals';
import type { LearningType } from './LearningType';
import type { TimeAvailabilityPerDay } from './TimeAvailabilityPerDay';
import type { TimeAvailabilityPerWeek } from './TimeAvailabilityPerWeek';
import type { UserAdvancement } from './UserAdvancement';
export type GoalDetailsDto = {
    id?: string;
    userId?: string | null;
    name?: string | null;
    goalFriendlyName?: Goals;
    category?: GoalCategories;
    userAdvancement?: UserAdvancement;
    timeAvailabilityPerDay?: TimeAvailabilityPerDay;
    timeAvailabilityPerWeek?: TimeAvailabilityPerWeek;
    startDate?: string;
    endDate?: string;
    learningType?: LearningType;
    userInput?: string | null;
};

