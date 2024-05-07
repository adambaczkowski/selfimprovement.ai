export enum EducationLevel {
  Primary = 0,
  Secondary = 1,
  PostSecondary = 2,
}

export const educationOptions = [
  { value: EducationLevel.Primary, label: 'Primary' },
  { value: EducationLevel.Secondary, label: 'Secondary' },
  { value: EducationLevel.PostSecondary, label: 'Post Secondary' },
];