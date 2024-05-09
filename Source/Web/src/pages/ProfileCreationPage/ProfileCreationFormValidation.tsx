import * as Yup from "yup";

export const ProfileCreationFormValidation = () => {
  return Yup.object().shape({
    name: Yup.string()
      .required("Name is required"),
    surname: Yup.string()
      .required("Surname is required"),
    weight: Yup.number()
      .typeError("Weight must be a number")
      .required("Weight is required"),
    height: Yup.number()
      .typeError("Height must be a number")
      .required("Height is required"),
    age: Yup.number()
      .typeError("Age must be a number")
      .required("Age is required"),
  });
};
