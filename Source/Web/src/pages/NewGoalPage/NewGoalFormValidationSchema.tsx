import * as Yup from "yup";

export const NewGoalFormValidation = () => {
  return Yup.object().shape({
    category: Yup.string().required("Category is required"),
    timeAvailability: Yup.string().required("Time Availability is required"),
    duration: Yup.number().typeError("Duration must be a number").required("Duration is required"),
    experience: Yup.string().required("Experience is required"),
    learningType: Yup.string().required("Learning Type is required"),
  });
};
