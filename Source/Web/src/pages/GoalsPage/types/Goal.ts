export interface Goal {
    category: string;
    timeAvailability: string;
    duration: Date;
    experience: string;
    learningType: string;
    tasks: GoalTask[];
}

export interface GoalTask {
    content: string;
    estimatedDuration: Date;
    isCompleted: boolean;
    date: Date;
}